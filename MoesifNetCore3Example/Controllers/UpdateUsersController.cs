using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MoesifNetCore3Example.Models;
using Moesif.Middleware.Helpers;
using Moesif.Middleware;
using MoesifNetCore3Example.Settings;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoesifNetCore3Example.Controllers
{
    [Route("api/users")]
    public class UpdateUsersController : ControllerBase
    {
        public MoesifMiddleware moesifMiddleware = new MoesifMiddleware(next: (innerHttpContext) => Task.FromResult(0), _middleware: MoesifOptions.moesifOptions);

        [HttpPost("{id}")]
        public IActionResult UpdateUserWithID(string id)
        {
            // Campaign object is optional, but useful if you want to track ROI of acquisition channels
            // See https://www.moesif.com/docs/api#users for campaign schema
            Dictionary<string, object> campaign = new Dictionary<string, object>
            {
                {"utm_source", "google"},
                {"utm_medium", "cpc"},
                {"utm_campaign", "adwords"},
                {"utm_term", "api+tooling"},
                {"utm_content", "landing"}
            };

            // metadata can be any custom dictionary
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"email", "john@acmeinc.com"},
                {"first_name", "John"},
                {"last_name", "Doe"},
                {"title", "Software Engineer"},
                {"sales_info", new Dictionary<string, object> {
                        {"stage", "Customer"},
                        {"lifetime_value", 24000},
                        {"account_owner", "mary@contoso.com"}
                    }
                }
            };

            // Only user_id is required
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                {"user_id", id},
                {"company_id", "67890"}, // If set, associate user with a company object
                {"campaign", campaign},
                {"metadata", metadata},
            };

            // Update the user asynchronously
            moesifMiddleware.UpdateUser(user);

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status201Created);
        }
    }
}
