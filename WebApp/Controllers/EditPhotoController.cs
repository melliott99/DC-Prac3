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
    /*Used to call edit photo in datamodel*/
    public class EditPhotoController : ApiController
    {
        //Post api/<controller>
        [Route("api/editphoto/")]
        [HttpPost]
        public HttpResponseMessage Post(SearchData editTerm)
        {
            DataModel data = new DataModel();


            HttpResponseMessage hrm = data.EditPhoto(editTerm.index);

            return hrm;
        }
    }
}
