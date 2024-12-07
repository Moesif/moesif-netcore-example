using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using MoesifNet6Example.Models;
using System.Threading;

namespace MoesifNet6Example.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private const int MAX_SIZE_75_KB = 75;
        private const int MAX_SIZE_150_KB = 150;
        
        public EmployeeController()
        {
            Console.WriteLine("EmployeeController constructor called");
        }
        
        private static string GenerateLargeResponseBody(int sizeInKB)
        {
            const string baseString = "This is a test string. ";
            int repetitions = (sizeInKB * 1024); // / baseString.Length;
            return new string('A', repetitions); // Create a large string filled with 'A's
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            Console.WriteLine($"BEGIN: EmployeeController GetByID method called with id: {id}");
            var employee = new Employee()
            {
                ID = id,
                FirstName = "No Delay",
                LastName = "Very Long LastName From Moesif for API Awesomeness of Moesif",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };
            
            Console.WriteLine($"END : Employee object created: {employee.ID}, {employee.FirstName}, {employee.LastName}");

            return Ok(employee);
        }

        [HttpGet("delay_random/{id}")]
        public IActionResult GetEventId(int id)
        {
            var employee = new Employee()
            {
                ID = id,
                FirstName = $"Get-Event-{id}",
                LastName = $"Delayed-By-{id}-MS",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };
            
            Thread.Sleep(id); // Sleeps for id milliseconds
        
            return Ok(employee);
        }

        [HttpGet("payload75/{id}")]
        public IActionResult GetNotificationId(int id)
        {
            int sleepTime = 100;
            string firstName = $"Get-{id}-{MAX_SIZE_75_KB}-KB-{sleepTime}-MS";
            string lastName = $"Delayed-By-{sleepTime}-MS";
            if (id > 10)
            {
                lastName = GenerateLargeResponseBody(MAX_SIZE_75_KB);
            }
            var employee = new Employee()
            {
                ID = id,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            Thread.Sleep(sleepTime); // Sleeps for id milliseconds
        
            return Ok(employee);
        }

        [HttpGet("payload150/{id}")]
        public IActionResult GetSubsId(int id)
        {
            int sleepTime = 100; // in ms
            string firstName = $"Get-{id}-{MAX_SIZE_150_KB}-KB-{sleepTime}-MS";
            string lastName = $"Delayed-By-{sleepTime}-MS";
            if (id > 10)
            {
                lastName = GenerateLargeResponseBody(MAX_SIZE_150_KB);
            }

            var employee = new Employee()
            {
                ID = id,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = DateTime.Now.AddYears(-30)
            };
            
            Thread.Sleep(sleepTime); // Sleeps for id milliseconds
        
            return Ok(employee);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            Console.WriteLine("Post method called");
            
            return Ok(employee);
        }
    }
}
