using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Train2Care.Nancy.Modules
{
    public sealed class HomeModule : NancyModule
    {
        public HomeModule() : base("/api")
        {
            Get("/test", args => "Hello");
        }
    }
}