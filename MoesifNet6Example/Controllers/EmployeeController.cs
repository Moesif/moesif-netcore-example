using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using MoesifNet6Example.Models;

namespace MoesifNet6Example.Controllers
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

            return Ok(employee);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {

            return Ok(employee);
        }
    }
}
