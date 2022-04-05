using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using MoesifNetCore3Example.Models;
using System.Text.Json;


namespace MoesifNetCore3Example.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var employee = new Employee()
            {
                ID = id,
                FirstName = "firstName",
                LastName = "lastName",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            string jsonString = JsonSerializer.Serialize(employee);

            return Ok(jsonString);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {

            return Ok(employee);
        }

        [HttpPost("batch")]
        public IActionResult Post([FromBody] List<Employee> employee)
        {

            string jsonString = JsonSerializer.Serialize(employee);
            return Ok(jsonString);
        }

        [HttpPost("xml/{value}")]
        public ContentResult GetXml(string value)
        {
            var xml = $"<result><value>{value}</value></result>";
            return new ContentResult
            {
                Content = xml,
                ContentType = "application/xml",
                StatusCode = 200
            };
        }
    }
}
