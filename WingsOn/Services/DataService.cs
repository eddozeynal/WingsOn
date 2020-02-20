using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Services
{
    public class DataService : IDataService
    {
        PersonRepository personRepository = null;
        FlightRepository flightRepository = null;
        BookingRepository bookingRepository = null;
        public DataService(PersonRepository _personRepository, FlightRepository _flightRepository, BookingRepository _bookingRepository)
        {
            personRepository = _personRepository;
            flightRepository = _flightRepository;
            bookingRepository = _bookingRepository;
        }

        public Task<Person> GetPersonById(int Id)
        {
            return Task.Run(() =>
            {
                Person person = personRepository.Get(Id);
                if (person == null) throw new ValueNotFoundException("Person list", Id);
                return person;
            });
        }

        public Task<IEnumerable<Person>> GetPassengersByFilght(string flightNumber)
        {
            return Task.Run(() =>
            {
                var flight = flightRepository.GetAll().SingleOrDefault(x => x.Number == flightNumber);
                if (flight == null) throw new ValueNotFoundException("Flights",flightNumber);
                var bookings = bookingRepository.GetAll().Where(x => x.Flight.Id == flight.Id);
                var passengers = bookings.SelectMany(x => x.Passengers);
                return passengers;
            });
        }

        public Task<IEnumerable<Person>> GetMalePassengers()
        {
            return Task.Run(() =>
            {
                var passengers = personRepository.GetAll().Where(x => x.Gender == Domain.GenderType.Male);
                return passengers;
            });
        }

        public Task<Person> UpdatePassengerAddress(string newAddress, int passengerId)
        {
            return Task.Run(() =>
            {
                var passenger = personRepository.Get(passengerId);
                if (passenger == null) throw new ValueNotFoundException("Person list", passengerId);
                passenger.Address = newAddress;
                return passenger;
            });

        }

        public Task<Booking> AddBookingToFlight(Booking booking, string flightNumber)
        {
            return Task.Run(() =>
            {
                var flight = flightRepository.GetAll().SingleOrDefault(x => x.Number == flightNumber);
                if (flight == null) throw new ValueNotFoundException("Flights", flightNumber);
                booking.Flight = flight;
                booking.DateBooking = DateTime.Now;
                bookingRepository.Save(booking);
                return booking;
            });
           
        }
    }
}
