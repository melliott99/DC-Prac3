using System;
using System.Drawing;

namespace DatabaseLib
{
	public class DataStruct
	{
		string firstName;
		string lastName;
		uint pin;
		uint acctNo;
		double balance;
		byte[] image;

		public DataStruct()
		{
			acctNo = 0;
			pin = 0;
			balance = 0;
			firstName = " ";
			lastName = " ";
			
		}

		public DataStruct(string inFName, string inLName, uint inPin, uint inAcctNo, double inBalance, byte[] inImage)
		{
			firstName = inFName;
			lastName = inLName;
			pin = inPin;
			acctNo = inAcctNo;
			balance = inBalance;
			image = inImage;
		}

		public void setFName(string inFName)
		{
			firstName = inFName;
		}

		public void setLName(string inLName)
		{
			lastName = inLName;
		}

		public void setPin(uint inPin)
		{
			pin = inPin;
		}

		public void setAcctNo(uint inAcctNo)
		{
			acctNo = inAcctNo;
		}

		public void setBalance(double inBalance)
		{
			balance = inBalance;
		}

		public void setImage(byte[] inImage)
		{
			image = inImage;
		}

		public byte[] getImage()
		{
			return image;
		}

		public uint getAcctNo()
		{
			return acctNo;
		}

		public uint getPin()
		{
			return pin;
		}

		public string getFName()
		{
			return firstName;
		}

		public string getLName()
		{
			return lastName;
		}

		public double getBalance()
		{
			return balance;
		}
	}
}
