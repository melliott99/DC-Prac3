using APIClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace DatabaseLib
{
	public class DatabaseClass
	{
		List<DataStruct> personStruct;
		DatabaseGenerator dbg;
		uint logNumber;
		

		private static DatabaseClass instance = null;

		//Made as a singlton because you only want to ever make the database once
		public static DatabaseClass GetInstance()
		{
			if (instance == null)
			{
				instance = new DatabaseClass();
			}
			return instance;
		}
		private DatabaseClass()
		{
			dbg = new DatabaseGenerator();
			personStruct = new List<DatabaseLib.DataStruct>();
			logNumber = 0;
			for(int i = 0; i < 1000; i++)
			{
				personStruct.Add(dbg.GetEntry());
			}
			GetNumRecords();
		}

		[MethodImpl(MethodImplOptions.Synchronized)]//Allow it to handle synchronisation
		//Only logs from data server 
		public void Log(string logString)
		{
			logNumber++;
			System.IO.File.AppendAllText(@"C:\Users\Michael (Work)\source\repos\Prac3\Logs\LogFileData.txt", "\n" + logNumber + ": " + logString);
		}

		public uint GetAcctNoByIndex(int index)
		{

			DataStruct p = personStruct[index];
			return p.getAcctNo();
		}

		public byte[] GetImageByIndex(int index)
		{
			DataStruct p = personStruct[index];
			return p.getImage();
		}

		public byte[] GetNewImage()
		{
			return dbg.GetImage();
		}

		public int FindIndexByLName(string searchTerm)
		{
			//Searches for a user with that last name and returns -1 if not or their index
			DataStruct p;
			int isFound = -1;
			for (int i = 0; i < personStruct.Count; i++)
			{
				p = personStruct[i];
				if (p.getLName().Equals(searchTerm))
				{
					isFound = i;
					return isFound;

				}
			}
			return isFound;
		}

		public uint GetPINByIndex(int index)
		{
			DataStruct p = personStruct[index];
			return p.getPin();
		}

		public string GetFirstNameByIndex(int index)
		{
			DataStruct p = personStruct[index];
			return p.getFName();
		}

		public string GetLastNameByIndex(int index)
		{
			DataStruct p = personStruct[index];
			return p.getLName();
		}

		public double GetBalanceByIndex(int index)
		{
			DataStruct p = personStruct[index];
			return p.getBalance();
		}

		public int GetNumRecords()
		{
			return personStruct.Count;
		}

		public DataStruct GetPersonByIndex(int index)
		{
			DataStruct p = personStruct[index];
			return p;

		}
	}
}
