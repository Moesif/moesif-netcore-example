using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace MoesifNet6Example.Settings
{
    public class MoesifOptions
    {
        private readonly IConfiguration _config;

        public MoesifOptions(IConfiguration config)
        {
            _config = config;
        }
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

        public Dictionary<string, object> getMoesifOptions()
        {
            Dictionary<string, object> moesifOptions = new Dictionary<string, object>
            {
                {MoesifOptionsParamNames.ApplicationId, getConfigString(MoesifOptionsParamNames.ApplicationId)},
                {MoesifOptionsParamNames.LocalDebug, getConfigBool(MoesifOptionsParamNames.LocalDebug)},
                {MoesifOptionsParamNames.LogBody, getConfigBool(MoesifOptionsParamNames.LogBody)},
                {MoesifOptionsParamNames.LogBodyOutgoing, getConfigBool(MoesifOptionsParamNames.LogBodyOutgoing)},
                {MoesifOptionsParamNames.ApiVersion, getConfigString(MoesifOptionsParamNames.ApiVersion)},
                {MoesifOptionsParamNames.EnableBatching, getConfigBool(MoesifOptionsParamNames.EnableBatching)},
                {MoesifOptionsParamNames.BatchSize, getConfigInt(MoesifOptionsParamNames.BatchSize)},
                {MoesifOptionsParamNames.IdentifyUser, IdentifyUser},
                {MoesifOptionsParamNames.IdentifyCompany, IdentifyCompany},
                {MoesifOptionsParamNames.GetSessionToken, GetSessionToken},
                {MoesifOptionsParamNames.GetMetadata, GetMetadata},
                {MoesifOptionsParamNames.GetMetadataOutgoing, GetMetadataOutgoing}
            };
            return moesifOptions;
        }

        public string getConfigString(string paramName)
        {
            return _config.GetValue<string>(MoesifOptionsParamNames.asKey(paramName));
        }

        public bool getConfigBool(string paramName)
        {
            return _config.GetValue<bool>(MoesifOptionsParamNames.asKey(paramName));
        }
        public int getConfigInt(string paramName)
        {
            return _config.GetValue<int>(MoesifOptionsParamNames.asKey(paramName));
        }

        public bool isConfiguredMoesifApplicationId()
        {
            string appId = null;
            try {
                appId = (string) getMoesifOptions().GetValueOrDefault(MoesifOptionsParamNames.ApplicationId);
            }
            catch (Exception ex){
                Console.WriteLine("Error Reading Moesif Application Id in appsettings(.env).json: " + ex.Message );
            }
            return !string.IsNullOrWhiteSpace(appId) && !appId.StartsWith("<");
        }
    }

    public class MoesifOptionsParamNames
    {
        // Read from appsettings.json
        public static string Key = "MoesifOptions";
        // Read from appsettings.json
        public static string ApplicationId = "ApplicationId";
        // Read from appsettings.json
        public static string LocalDebug = "LocalDebug";
        // Read from appsettings.json
        public static string LogBody = "LogBody";
        // Read from appsettings.json
        public static string LogBodyOutgoing = "LogBodyOutgoing";
        // Read from appsettings.json
        public static string ApiVersion = "ApiVersion";
        // Read from appsettings.json
        public static string EnableBatching = "EnableBatching";
        // Read from appsettings.json
        public static string BatchSize = "BatchSize";

        public static string IdentifyUser = "IdentifyUser";
        public static string IdentifyCompany = "IdentifyCompany";
        public static string GetSessionToken = "GetSessionToken";
        public static string GetMetadata = "GetMetadata";
        public static string GetMetadataOutgoing = "GetMetadataOutgoing";

        public static string asKey(string suffix)
        {
            return Key + ":" + suffix;
        }
    }
}
