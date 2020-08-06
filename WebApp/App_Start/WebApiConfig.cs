﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApp.App_Start
{
	public class WebApiConfig
	{

		public static void Register(HttpConfiguration config)
		{
			//This configures the webapi to work as we want

			//This binds custom http attribute paths in controllers to their functions

			config.MapHttpAttributeRoutes();

			//This binds API routes in the way we expect. You'll need to go to
			//http://localhost:xxxx/api/whatever to get to the api.
			//Just remember! that /api/ is critical!
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
				);


		}
	}
}