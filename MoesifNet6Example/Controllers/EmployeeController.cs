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
            Random random = new Random();
            int sleepTime = 300; // random.Next(1, 101);
            string lastName = $"Delayed-By-{sleepTime}-MS";
            var employee = new Employee()
            {
                ID = id,
                FirstName = $"Get-Event-{id}",
                LastName = lastName,
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            Thread.Sleep(sleepTime); // Sleeps for id milliseconds
        
            return Ok(employee);
        }
        
        [HttpPost("event/{id}")]
        public IActionResult PostEventId(int id)
        {
            Random random = new Random();
            int sleepTime = 1600; // random.Next(1, 101);  // 500 - 1000 (Min - Median)
            string firstName = $"Post-Event-{id}";
            string lastName = $"Delayed-By-{sleepTime}-MS";
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
        
        [HttpPost("readunread/{id}")]
        public IActionResult PostReadUnreadId(int id)
        {
            Random random = new Random();
            int sleepTime = 1300; // random.Next(1, 101);  // 200 - 1000
            string firstName = $"Post-ReadUnRead-{id}";
            string lastName  = $"Delayed-By-{sleepTime}-MS";
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

        [HttpGet("payload75/{id}")]
        public IActionResult GetNotificationId(int id)
        {
            int sleepTime = 8000; // random.Next(1, 101);  // 300 - 9000
            var size = 1;
            string lastName = $"Delayed-By-{sleepTime}-MS";
            if (id > 10)
            {
                size = MAX_SIZE_75_KB;
                lastName = GenerateLargeResponseBody(MAX_SIZE_75_KB);
            }
            string firstName = $"Get-{id}-{size}-KB-{sleepTime}-MS";
            
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
            int sleepTime = 3500;  // random.Next(1, 101);  // 300 - 4000
            var size = 1;
            string lastName = $"Delayed-By-{sleepTime}-MS";
            if (id > 10)
            {
                size = MAX_SIZE_150_KB;
                lastName = GenerateLargeResponseBody(MAX_SIZE_150_KB);
            }
            string firstName = $"Get-{id}-{MAX_SIZE_150_KB}-KB-{sleepTime}-MS";

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
