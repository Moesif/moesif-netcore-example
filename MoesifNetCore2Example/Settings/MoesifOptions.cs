using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using Moesif.Api.Models;
using System.Net.Http;

namespace MoesifNetCore2Example.Settings
{
    public class MoesifOptions
    {
        public static Func<HttpRequest, HttpResponse, string> IdentifyUser = (HttpRequest req, HttpResponse res) => {
            // Implement your custom logic to return user id
            return req.HttpContext?.User?.Identity?.Name;
        };

        public static Func<HttpRequest, HttpResponse, string> IdentifyCompany = (HttpRequest req, HttpResponse res) => {
            return req.Headers["X-Organization-Id"];
        };

        public static Func<HttpRequest, HttpResponse, string> GetSessionToken = (HttpRequest req, HttpResponse res) => {
            return req.Headers["Authorization"];
        };

        public static Func<HttpRequest, HttpResponse, Dictionary<string, object>> GetMetadata = (HttpRequest req, HttpResponse res) => {
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"string_field", "value_1"},
                {"number_field", 0},
                {"object_field", new Dictionary<string, string> {
                    {"field_a", "value_a"},
                    {"field_b", "value_b"}
                    }
                }
            };
            return metadata;
        };

        public static Func<HttpRequestMessage, HttpResponseMessage, Dictionary<string, object>> GetMetadataOutgoing = (HttpRequestMessage req, HttpResponseMessage res) => {
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"string_field", "value_1"},
                {"number_field", 0},
                {"object_field", new Dictionary<string, string> {
                    {"field_a", "value_a"},
                    {"field_b", "value_b"}
                    }
                }
            };
            return metadata;
        };

        static public Dictionary<string, object> moesifOptions = new Dictionary<string, object>
        {
            {"ApplicationId", "Your Moesif Application Id"},
            {"LocalDebug", true},
            {"LogBody", true},
            {"LogBodyOutgoing", true},
            {"ApiVersion", "1.1.0"},
            {"IdentifyUser", IdentifyUser},
            {"IdentifyCompany", IdentifyCompany},
            {"GetSessionToken", GetSessionToken},
            {"GetMetadata", GetMetadata},
            {"GetMetadataOutgoing", GetMetadataOutgoing},
            {"EnableBatching", true},
            {"BatchSize", 25}
        };
    }
}
