using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_Inheritance
{
    internal class PartTime : Employee
    {
        public double Rate { get; set; }

        public double Hours { get; set; }

        PartTime()
        {
            Rate = 0;
            Hours = 0;
        }

        public PartTime(string id, string name, string address, string phone, long sin, string dob,
            string dept, double rate, double hours)
            : base(id, name, address, phone, sin, dob, dept)
        {
            Rate = rate;
            Hours = hours;
        }

        public override double GetPay()
        {
            double pay = Hours * Rate;

            return pay;
        }
        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nAddress: {Address}\nPhone: {Phone}\nSIN: {Sin}\nDOB: {Dob}\nDept: {Dept}\nRate: {Rate}\nHours: {Hours}";
        }
    }
}
