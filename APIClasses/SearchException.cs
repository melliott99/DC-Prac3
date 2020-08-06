using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClasses
{
	/* Guided by:
	 *  https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/creating-and-throwing-exceptions
	 */

	[Serializable]
	public class SearchException : System.Exception
	{
		public SearchException() : base() { }
		public SearchException(string message) : base(message) { }
		public SearchException(string message, System.Exception inner) : base(message, inner) { }
		protected SearchException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
