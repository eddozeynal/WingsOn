using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Services;

namespace WingsOn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
       IDataService dataService = null;
        public DefaultController(IDataService _dataService )
        {
            dataService = _dataService;
        }

        [HttpGet("Person/{Id}")]
        public async Task<Person> GetPersonById(int Id)
        {
            var person = await dataService.GetPersonById(Id);
            return person;
        }


        [HttpGet("GetMalePassengers")]
        public Task<IEnumerable<Person>> GetMalePassengers()
        {
            var passengers = dataService.GetMalePassengers() ;
            return passengers;
        }

        [HttpPost("UpdatePassengerAddress/{passengerId}")]
        public async Task<Person> UpdatePassengerAddress([FromBody] string newAddress, int passengerId)
        {
            var passenger = await dataService.UpdatePassengerAddress(newAddress, passengerId);
            return passenger;
        }

        [HttpPut("AddBookingToFligt/{flightNumber}")]
        public async Task<Booking> AddBookingToFligt([FromBody] Booking booking, string flightNumber)
        {
            var result = await dataService.AddBookingToFlight(booking, flightNumber);
            return result;
        }

        [HttpGet("PassengersByFilght/{flightNumber}")]
        public async Task<IEnumerable<Person>> GetPassengersByFilght(string flightNumber)
        {
            var passengers = await dataService.GetPassengersByFilght(flightNumber);
            return passengers;
        }
        
    }
}
