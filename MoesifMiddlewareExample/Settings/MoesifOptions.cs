using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using Moesif.Api.Models;

namespace MoesifMiddlewareExample.Settings
{
    public class MoesifOptions
    {
        public static Func<HttpRequest, HttpResponse, string> IdentifyUser = (HttpRequest req, HttpResponse res) => {
            return "my_user_id";
        };

        public static Func<HttpRequest, HttpResponse, string> GetSessionToken = (HttpRequest req, HttpResponse res) => {
            return "23jdf0owekfmcn4u3qypxg09w4d8ayrcdx8nu2ng]s98y18cx98q3yhwmnhcfx43f";
        };

        public static Func<HttpRequest, HttpResponse, Dictionary<string, object>> GetMetadata = (HttpRequest req, HttpResponse res) => {
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"email", "johndoe@acmeinc.com"},
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

        public static Func<HttpRequest, HttpResponse, bool> Skip = (HttpRequest req, HttpResponse res) => {
            Console.WriteLine("Trying should skip");
            string uri = new Uri(req.GetDisplayUrl()).ToString();
            if (uri.Contains("skip"))
            {
                return true;
            }
            return false;
        };

        public static Func<EventModel, EventModel> MaskEventModel = (EventModel event_model) => {
            event_model.UserId = "masked_user_id";
            return event_model;
        };

        static public Dictionary<string, object> moesifOptions = new Dictionary<string, object>
        {
            {"ApplicationId", "Your Application ID Found in Settings on Moesif"},
            {"LocalDebug", true},
            {"ApiVersion", "1.1.0"},
            {"IdentifyUser", IdentifyUser},
            {"GetSessionToken", GetSessionToken},
            {"GetMetadata", GetMetadata},
            {"Skip", Skip},
            {"MaskEventModel", MaskEventModel}
        };
    }
}
