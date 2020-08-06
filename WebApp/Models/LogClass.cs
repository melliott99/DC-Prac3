using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace WebApp.Models
{
	public sealed class LogClass
	{

		private static uint logNum;
		private static LogClass instance;

		private LogClass()
		{
			logNum = 1;
		}

		public static LogClass GetInstance()
		{
			if (instance == null)
			{
				instance = new LogClass();
			}
			return instance;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void LogFunc(string logString)
		{
			logNum++;
			System.IO.File.AppendAllText(@"C:\Users\Michael (Work)\source\repos\Prac3\Logs\LogFileBuiz.txt", "\n" + logNum + ": " + logString);
		}

	}
}