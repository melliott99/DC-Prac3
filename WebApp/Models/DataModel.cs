using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIClasses;
using Server;
using Microsoft.Ajax.Utilities;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System;

namespace WebApp.Models
{

    /*Creates a connection to the datatier and calls the appropriate method there*/
    public class DataModel
	{

        private DataServerInterface channel;
        private LogClass logger = LogClass.GetInstance();//Singlton logger class to log 

        /*Creates a connection to the datatier*/
        public DataModel()
        {
            ChannelFactory<DataServerInterface> channelFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            tcp.MaxReceivedMessageSize = 2147483647;

            string URL = "net.tcp://localhost:8100/DataService";

            channelFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            channel = channelFactory.CreateChannel();
        }

        /*Changing the first name taking in the new first name and the users index*/
        public HttpResponseMessage EditFName(string newName, int index)
        {
            HttpRequestMessage Request = new HttpRequestMessage();


            channel.EditFName(newName, index);//Calls the datatier method
            logger.LogFunc("Changed the first name of index: " + index);//Logging to the log file
            //Never a time when this fails 
            return Request.CreateErrorResponse(HttpStatusCode.OK, newName); //Request is always going to be ok
        }

        /*Changing the photo*/
        public HttpResponseMessage EditPhoto(int index)
        {
            HttpRequestMessage Request = new HttpRequestMessage();


            channel.EditPhoto(index);//Calls the datatier method
            logger.LogFunc("Changed the photo of index: " + index);//Logging to file
            //Never a time when this fails 
            return Request.CreateErrorResponse(HttpStatusCode.OK, "Success");//Never fails
        }

        /*Changing the last name by taking in the new last name and the index*/
        public HttpResponseMessage EditLName(string newName, int index)
        {
            HttpRequestMessage Request = new HttpRequestMessage();


            channel.EditLName(newName, index);//calling the data tier function
            logger.LogFunc("Changed the last name of index: " + index);//logging
            //Never a time when this fails 
            return Request.CreateErrorResponse(HttpStatusCode.OK, newName); 
        }

        /*Changing the balance of the account*/
        public HttpResponseMessage EditBalance(string newBalance, int index)
        {
            HttpRequestMessage Request = new HttpRequestMessage();


            try
            {
                channel.EditBalance(newBalance, index);//Calling data tier function
                logger.LogFunc("Changed the balance of index: " + index);//Loggin success
                return Request.CreateErrorResponse(HttpStatusCode.OK, newBalance);//If successful
            }
            catch (FaultException<ExceptionDetail> e)//If there is an error
            {

                logger.LogFunc("Inside EditBalance: " + "Message: " + e.Message + " StackTrace: " + e.StackTrace);//Logging error
                HttpResponseMessage hrm = Request.CreateResponse(HttpStatusCode.NotFound);//Telling the caller that it failed
                hrm.Content = new StringContent("Balance was in incorrect format");//Error message for the user
                return hrm;
            }
        }


        /*Changing the pin*/
        public HttpResponseMessage EditPin(string newPin, int index)
        {
            HttpRequestMessage Request = new HttpRequestMessage();

            try
            {
                channel.EditPin(newPin, index);//Calling data tier function
                logger.LogFunc("Changed the acct of index: " + index);//Logging success
                return Request.CreateErrorResponse(HttpStatusCode.OK, newPin);//Success
            }
            catch (FaultException<ExceptionDetail> e)
            {

                logger.LogFunc("Inside EditAcct: " + "Message: " + e.Message + " StackTrace: " + e.StackTrace);//Logging fail 
                HttpResponseMessage hrm = Request.CreateResponse(HttpStatusCode.NotFound);//Failed status code
                hrm.Content = new StringContent("Pin Number was in incorrect format");//Error message for user passed into httpresponsemessage
                return hrm;
            }
        }

        /*Changing the account num*/
        public HttpResponseMessage EditAcct(string newAcc, int index)
        {
            HttpRequestMessage Request = new HttpRequestMessage();


            try
            {
                channel.EditAccount(newAcc, index);//Calling data tier function
                logger.LogFunc("Changed the acct of index: " + index);//Logging success
                return Request.CreateErrorResponse(HttpStatusCode.OK, newAcc);//Success
            }
            catch (FaultException<ExceptionDetail> e)
            {

                logger.LogFunc("Inside EditAcct: " + "Message: " + e.Message + " StackTrace: " + e.StackTrace);//Logging failure
                HttpResponseMessage hrm = Request.CreateResponse(HttpStatusCode.NotFound);//Telling caller it failed
                hrm.Content = new StringContent("Account Number was in incorrect format");//Error message for user
                return hrm;
            }
        }

        /*Getting the number of entries*/
        public int GetNumEntries()
        {

            logger.LogFunc("Inside GetNumEntries");
            return channel.GetNumEntries();
        }

        
        /*Getting a specific user at an entry*/
        public HttpResponseMessage GetValuesForEntry(int index, out uint acctNo, out uint pin, out double bal,
      out string fName, out string lName, out byte[] image)
        {
            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            image = null;
            //Initialising values
            
            DataIntermed data;
            HttpRequestMessage Request = new HttpRequestMessage();
            try
            {
                channel.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out image);//Calling the function in data tier
                data = new DataIntermed(bal, acctNo, pin, fName, lName, index, image);//making a dataintermed obj
                var json = new JavaScriptSerializer().Serialize(data);//serializing the dataintermed obj so it can be inside the httpresponsemessage
                logger.LogFunc("Inside the GetValuesForEntry inside the DataModel Class and have found " + data.fName + " " + data.lName);//Logging success
                return Request.CreateErrorResponse(HttpStatusCode.OK, json);//Returning successmessage and data
            }
            catch (FaultException<ExceptionDetail> e)//If it fails
            {
                logger.LogFunc("Inside Get Values For Entry: " + "Message: " + e.Message + " StackTrace: " + e.StackTrace);//Logging that it failed
                HttpResponseMessage hrm = Request.CreateResponse(HttpStatusCode.NotFound);//Status code for caller
                hrm.Content = new StringContent("Value was not found at that index");//Error message for user
                return hrm;
            }
        }


        /*Searching for a user by their last name*/
        public HttpResponseMessage SearchByLastName(string searchTerm, out uint acctNo, out uint pin, out double bal, out string fName, out string lName, out int index, out byte[] image)
        {
            acctNo = 0;
            pin = 0;
            bal = 0.0;
            fName = "";
            lName = "";
            image = null;
            index = 0;
            //Intializing values

            DataIntermed data;
            HttpRequestMessage Request = new HttpRequestMessage();

            try
            {
                channel.SearchByLName(searchTerm, out acctNo, out pin, out bal, out fName, out lName, out index, out image);//Call function in datatier
                data = new DataIntermed(bal, acctNo, pin, fName, lName, index, image);//Create obj
                var json = new JavaScriptSerializer().Serialize(data);//Converting js
                return Request.CreateErrorResponse(HttpStatusCode.OK, json);//Send it back with success code
            }
            catch (FaultException<ExceptionDetail> e)
            {
                logger.LogFunc("Inside Search By Last Name: " + "Message: " + e.Message + " StackTrace: " + e.StackTrace);//log error
                HttpResponseMessage hrm = Request.CreateResponse(HttpStatusCode.NotFound);//tell caller
                hrm.Content = new StringContent("Couldn't find someone with that last name");//error message for user
                return hrm;
            }
        }
    }
}