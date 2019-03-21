using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp.Controllers
{
    public class BaseController : Controller
    {
        internal string ApiEndpoint = "https://rndincoming.azurewebsites.net";
        //internal string ApiEndpoint = "https://localhost:44384"
    }
}
