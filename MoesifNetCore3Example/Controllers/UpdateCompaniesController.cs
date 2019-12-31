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
    [Route("api/companies")]
    public class UpdateCompaniesController : ControllerBase
    {
        public MoesifMiddleware moesifMiddleware = new MoesifMiddleware(next: (innerHttpContext) => Task.FromResult(0), _middleware: MoesifOptions.moesifOptions);

        [HttpPost("{id}")]
        public IActionResult UpdateCompaniesWithID(string id)
        {
            // Campaign object is optional, but useful if you want to track ROI of acquisition channels
            // See https://www.moesif.com/docs/api#update-a-company for campaign schema
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
                {"org_name", "Acme, Inc"},
                {"plan_name", "Free"},
                {"deal_stage", "Lead"},
                {"mrr", 24000},
                {"demographics", new Dictionary<string, int> {
                        {"alexa_ranking", 500000},
                        {"employee_count", 47}
                    }
                }
            };

            Dictionary<string, object> company = new Dictionary<string, object>
            {
                {"company_id", id}, // The only required field is your company id
                {"company_domain", "acmeinc.com"}, // If domain is set, Moesif will enrich your profiles with publicly available info 
                {"campaign", campaign},
                {"metadata", metadata},
            };

            // Update the company asynchronously
            moesifMiddleware.UpdateCompany(company);

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status201Created);
        }
    }
}
