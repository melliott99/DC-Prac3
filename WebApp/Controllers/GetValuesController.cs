using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using APIClasses;
using Server;
using WebApp.Models;
using System.Web.Http;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using System.Web;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WebApp.Controllers
{

    /*Used to get a value at a certain index by calling datamodel function*/
    public class GetValuesController : ApiController
    {
        DataModel dataObj = new DataModel();
       
        
        //Get api/<controller>/5
        [Route("api/getvalues/{index}")] [HttpGet]
        public HttpResponseMessage Get(int index)
        {
            DataIntermed data = new DataIntermed();

            HttpResponseMessage hrm = dataObj.GetValuesForEntry(index, out data.acctNo, out data.pin, out data.bal, out data.fName, out data.lName, out data.image);
        
            return hrm;
        }

    }
}
