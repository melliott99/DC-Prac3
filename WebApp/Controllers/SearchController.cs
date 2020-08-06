using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using APIClasses;
using Newtonsoft.Json;
using Server;
using WebApp.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;


namespace WebApp.Controllers
{
    /*Used to call teh searchbylastname function in the datamodel class*/
    public class SearchController : ApiController
    {

        //Post api/<controller>
        [Route("api/search/")][HttpPost]
        public HttpResponseMessage Post(SearchData searchTerm)
        {
            DataModel data = new DataModel();
            DataIntermed person = new DataIntermed();


            HttpResponseMessage hrm = data.SearchByLastName(searchTerm.searchStr, out person.acctNo, out person.pin, out person.bal, out person.fName, out person.lName, out person.index, out person.image);

            return hrm;
        }
    }
}
