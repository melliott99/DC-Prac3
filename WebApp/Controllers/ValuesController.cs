using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using APIClasses;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    /*Used to retrieve the number of entries in the database by calling the function in the datamodel class*/
    public class ValuesController : ApiController
    {
        DataModel data = new DataModel();
 
        //Get api/<controller>/5
        //[Route("api/values/")][HttpGet]
        public int Get()
        {
            return data.GetNumEntries();
        }

    }
}
