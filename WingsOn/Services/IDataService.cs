using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WingsOn.Domain;

namespace WingsOn.Services
{
    public interface IDataService
    {
        Task<Person> GetPersonById(int Id);

        Task<IEnumerable<Person>> GetPassengersByFilght(string flightNumber);
        Task<IEnumerable<Person>> GetMalePassengers();
        Task<Person> UpdatePassengerAddress(string newAddress, int passengerId);
        Task<Booking> AddBookingToFlight(Booking booking, string flightNumber);
    }
}
