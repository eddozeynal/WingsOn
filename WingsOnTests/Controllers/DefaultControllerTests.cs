using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WingsOn.Services;
using WingsOn.Dal;
using WingsOn.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WingsOn.Controllers.Tests
{
    [TestClass()]
    public class DefaultControllerTests
    {
        static DefaultController defaultController = null;

        static PersonRepository personRepository = null;
        static FlightRepository flightRepository = null;
        static BookingRepository bookingRepository = null;

        [ClassInitialize]
        public static void InitValues(TestContext tc)
        {
            personRepository = new PersonRepository();
            flightRepository = new FlightRepository();
            bookingRepository = new BookingRepository();
            DataService dataService = new DataService(personRepository,flightRepository, bookingRepository);
            defaultController = new DefaultController(dataService);
        }

        [TestMethod()]
        public void DefaultController_Init_Test()
        {
            Assert.IsNotNull(defaultController);
        }

        [TestMethod()]
        public async Task GetPersonByIdTest()
        {   
            var person =await defaultController.GetPersonById(25);
            Assert.AreEqual(25, person.Id);
        }

        [TestMethod()]
        public async Task GetMalePassengersTest()
        {
            var malePassengers = await defaultController.GetMalePassengers();
            Assert.IsTrue(malePassengers.Count() > 0);
        }

        [TestMethod()]
        public async Task UpdatePassengerAddressTest()
        {
            string newAddress = "Dallas, USA";
            var updatedPassenger =await defaultController.UpdatePassengerAddress(newAddress, 91);
            Assert.AreEqual(newAddress, updatedPassenger.Address);
        }

        [TestMethod()]
        public async Task AddBookingToFligtTest()
        {
            var person = personRepository.Get(24);
            var flight = flightRepository.Get(31);

            var booking = new Booking();
            booking.Customer = person;
            booking.Number = "WO-434728";
            booking.Flight = flight;
            booking.Passengers = new List<Person>() { person };
            int countOfPassengersBefore = (await defaultController.GetPassengersByFilght(flight.Number)).Count();
            var addedBooking = await defaultController.AddBookingToFligt(booking, flight.Number);
            int countOfPassengersAfter = (await defaultController.GetPassengersByFilght(flight.Number)).Count();
            Assert.AreEqual(countOfPassengersAfter, countOfPassengersBefore + 1);
        }

        [TestMethod()]
        public async Task GetPassengersByFilghtTest()
        {
            string flightNumber = "PZ696";
            var passengers = await defaultController.GetPassengersByFilght(flightNumber);
            int countOfPassengers = passengers.Count();
            Assert.IsTrue(countOfPassengers > 0);
        }



        [TestMethod()]
        [ExpectedException(typeof(ValueNotFoundException))]
        public async Task GetPersonById_NotFound_Test()
        {
            var person =await defaultController.GetPersonById(1);
        }



        [TestMethod()]
        [ExpectedException(typeof(ValueNotFoundException))]
        public async Task UpdatePassengerAddress_NotFound_Test()
        {
            string newAddress = "Dallas, USA";
            var updatedPassenger = await defaultController.UpdatePassengerAddress(newAddress, 50);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValueNotFoundException))]
        public async Task AddBookingToFligt_NotFound_Test()
        {
            var person = personRepository.Get(24);
            var flight = flightRepository.Get(31);
            var booking = new Booking();
            booking.Customer = person;
            booking.Number = "WO-434728";
            booking.Flight = flight;
            booking.Passengers = new List<Person>() { person };
            var addedBooking =await defaultController.AddBookingToFligt(booking, "NM304");
        }

        [TestMethod()]
        [ExpectedException(typeof(ValueNotFoundException))]
        public async Task GetPassengersByFilght_FlightNumberNotFound_Test()
        {
            string flightNumber = "JL856";
            var passengers = await defaultController.GetPassengersByFilght(flightNumber);
        }
    }
}