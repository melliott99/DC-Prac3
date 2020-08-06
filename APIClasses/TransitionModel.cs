using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClasses
{
	/*Represents the data and a message to go with it this data is later deserialized into the appropriate object*/
	public class TransitionModel
	{
		public string data{get; set;}
		public string message{get; set;}

		public TransitionModel(string inData, string inMessage)
		{
			data = inData;
			message = inMessage;
		}
	}
}
