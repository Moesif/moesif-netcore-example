using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using MoesifNetCore3Example.Models;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace MoesifNetCore3Example.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {

        public static Student GetStudentAsync(int id)
        {
            var student = new Student()
            {
                ID = id,
                FirstName = "firstName",
                LastName = "lastName"
            };
            return student;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var t1 = Task.Run(() => GetStudentAsync(id));
            await Task.WhenAll(t1);

            // Perform async operation
            var data = t1.Status == TaskStatus.RanToCompletion ? t1.Result : null;

            // Return the status
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            // Return the status
            return Ok();
        }
    }
}
