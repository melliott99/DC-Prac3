using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace APIClasses
{
    /* Class used to represent a users information with relevent constructors*/ 
    public class DataIntermed
    {
        public double bal;
        public uint acctNo;
        public uint pin;
        public string fName;
        public string lName;
        public int index;
        public byte[] image;

        public String toString()
        {
            string str = fName + " " + lName;
            return str;
        }

        public DataIntermed(double inBal, uint inAcctNo, uint inPin, string inFName, string inLName, int inIndex, byte[] inImage)
        {
            bal = inBal;
            acctNo = inAcctNo;
            pin = inPin;
            fName = inFName;
            lName = inLName;
            index = inIndex;
            image = inImage;
        }

        public DataIntermed()
        {
            bal = 0.0;
            acctNo = 0;
            pin = 0;
            fName = "";
            lName = "";
            index = -1;
        }
    }
}
