using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using APIClasses;
using Microsoft.Cci.MutableCodeModel;

namespace Server
{
    /*Used as an interface for the DataServer class, any method declared in here
     must be implemented in the DataServer class*/

    //Make this a service contract as it is a service interface
    [ServiceContract]
    public interface DataServerInterface
    {
        //Each of these are service function contracts. They need to be tagged as


        [OperationContract]
        int GetNumEntries();
        [OperationContract]
        [FaultContract(typeof(ExceptionDetail))]//Using fault contracts to serialize errors
		void GetValuesForEntry(int index, out uint acctNo, out uint pin, out double bal, out string fName, out string lName, out byte[] image);
		[OperationContract]
        [FaultContract(typeof(ExceptionDetail))]
        void SearchByLName(string searchTerm, out uint acctNo, out uint pin, out double bal, out string fName,
        out string lName,out int index, out byte[] image);

        [OperationContract]
        void EditFName(string newnName, int index);

        [OperationContract]
        void EditPhoto(int index);

        [OperationContract]
        void EditLName(string newName, int index);

        [OperationContract]
        [FaultContract(typeof(ExceptionDetail))]
        void EditBalance(string newBalance, int index);

        [OperationContract]
        [FaultContract(typeof(ExceptionDetail))]
        void EditAccount(string newAcc, int index);

        [OperationContract]
        [FaultContract(typeof(ExceptionDetail))]
        void EditPin(string newAcc, int index);
    }
}
