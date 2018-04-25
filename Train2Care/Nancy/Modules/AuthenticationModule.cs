using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Nancy;

namespace Train2Care.Nancy.Modules
{
    public class AuthenticationModule : NancyModule
    {
        public AuthenticationModule() : base("/api")
        {
            Post("/login", args =>
            {
                // Initialise the repose object so we can populate it
                var response = new LoginResponse();
                // Grab the authorisation header from the login request
                var header = Request.Headers.Authorization;
                // Check to see if there is actually a token and that it starts with the correct keyword
                if (header != null && header.StartsWith("Basic"))
                {
                    // Convert the header into something we can use (a credentials object)
                    var credentials = ConvertFromBase64(header);

                    response.StatusCode = HttpStatusCode.OK;
                    response.Token = credentials.Username + " " + credentials.Password;

                    return Response.AsJson(response);
                }

                response.StatusCode = HttpStatusCode.Unauthorized;
                response.Token = null;
                return Response.AsJson(response);

            });
            Post("/logout", args => "Hello");
        }

        // Converts endoded user credentials from base 64 to strings and packages in a credentials object
        public Credentials ConvertFromBase64(string token)
        {
            var trimmed = token.Substring("Basic ".Length).Trim();
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var details = encoding.GetString(Convert.FromBase64String(trimmed));

            var credentials = new Credentials
            {
                Username = details.Substring(0, details.IndexOf(':')),
                Password = details.Substring(details.IndexOf(':') + 1)
            };

            return credentials;
        }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Token { get; set; }
    }
}