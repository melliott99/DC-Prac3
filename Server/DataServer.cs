using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using APIClasses;
using DatabaseLib;
using Microsoft.Cci.MutableCodeModel;


namespace Server
{
    /*Has reference to the database and is used for communication to it*/

    //Allows multi threading and us to control the multiple threads
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class DataServer : DataServerInterface
    {
        private DatabaseClass db;
        public DataServer()
        {
            db = DatabaseClass.GetInstance();//Is a singleton
        }

        //Returns the number of entries
        public int GetNumEntries()
        {
            return db.GetNumRecords() - 1;//starts at 0
        }

        //Inside dataServer (Server tier)
        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out double bal,
       out string fName, out string lName, out byte[] image) 
        {
            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            image = null;

            try
            {
                //Retrieving at the specific index
                acctNo = db.GetAcctNoByIndex(index);
                pin = db.GetPINByIndex(index);
                bal = db.GetBalanceByIndex(index);
                fName = db.GetFirstNameByIndex(index);
                lName = db.GetLastNameByIndex(index);
                image = db.GetImageByIndex(index);
            }
            catch (ArgumentOutOfRangeException ex)//If they pick an index not in the database
            {
                Log("Message: " + ex.Message + "at index: " + index + "Stack Trace: " + ex.StackTrace);
                throw new FaultException<ExceptionDetail>(new ExceptionDetail(ex));//FaultExcpetion caught later
            }
            
        }

        public void EditFName(string newName, int index)
        {
            DataStruct p = db.GetPersonByIndex(index);
            p.setFName(newName);
        }

        public void EditLName(string newName, int index)
        {
         
            DataStruct p = db.GetPersonByIndex(index);
            p.setLName(newName);

        }

        public void EditPhoto(int index)
        {
            DataStruct p = db.GetPersonByIndex(index);
            p.setImage(db.GetNewImage());
        }

        /*Can choose any length pin*/
        public void EditPin(string newPin, int index)
        {
            DataStruct p = db.GetPersonByIndex(index);
            try
            {
                p.setPin(UInt32.Parse(newPin));
            }
            catch (FormatException ex)//if the userinputs words not numbers
            {
                Log("Message: " + ex.Message + "Stack Trace: " + ex.StackTrace);
                throw new FaultException<ExceptionDetail>(new ExceptionDetail(ex));
            }
            catch (OverflowException ex)//If too big
            {
                Log("Message: " + ex.Message + "Stack Trace: " + ex.StackTrace);
                throw new FaultException<ExceptionDetail>(new ExceptionDetail(ex));
            }
        }

        /*Can choose any length acc num*/
        public void EditAccount(string newAcc, int index)
        {
            DataStruct p = db.GetPersonByIndex(index);
            try
            {
                p.setAcctNo(UInt32.Parse(newAcc));
            }
            catch (FormatException ex)
            {
                Log("Message: " + ex.Message + "Stack Trace: " + ex.StackTrace);//Logging to a data tier log file
                throw new FaultException<ExceptionDetail>(new ExceptionDetail(ex));//caught later
            }
            catch (OverflowException ex)
            {
                Log("Message: " + ex.Message + "Stack Trace: " + ex.StackTrace);//logging to a data tier log file not business tier
                throw new FaultException<ExceptionDetail>(new ExceptionDetail(ex));//caught by caller
            }
        }

        public void EditBalance(string newBalance, int index)
        {
            DataStruct p = db.GetPersonByIndex(index);
            double result = 0.0;
            try
            {
                if (newBalance.Contains("$"))
                {
                    newBalance = newBalance.Replace("$", "");//Removing $ from balance
                }   
                result = Double.Parse(newBalance);
                Debug.WriteLine(result);
                p.setBalance(result);
                
            }
            catch (FormatException ex)//If they enter a string
            {
                Log("Message: " + ex.Message + "Stack Trace: " + ex.StackTrace);//Logging
                throw new FaultException<ExceptionDetail>(new ExceptionDetail(ex));//Caught later
            }
        }

        //Logger
        public void Log(string logString)
        {
            db.Log(logString);
        }


        /*Used to search for the first user with that last name(searchTerm)*/
        public void SearchByLName(string searchTerm, out uint acctNo, out uint pin, out double bal,
            out string fName, out string lName, out int index, out byte[] image)
        {

            try
            {
                index = db.FindIndexByLName(searchTerm);

                if (index != -1)//if its -1 means it wasn't found
                {
                    acctNo = db.GetAcctNoByIndex(index);
                    pin = db.GetPINByIndex(index);
                    bal = db.GetBalanceByIndex(index);
                    fName = db.GetFirstNameByIndex(index);
                    lName = db.GetLastNameByIndex(index);
                    image = db.GetImageByIndex(index);
                }
                else
                {
                    throw new SearchException();//Caught underneath
                }
               
            }
            catch (SearchException ex)
            {
                Log("Message: " + ex.Message + "Stack Trace: " + ex.StackTrace);//Logs
                throw new FaultException<ExceptionDetail>(new ExceptionDetail(ex));//Throws up
            }
        }

    }
}
