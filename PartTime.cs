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
        public double Rate { get; }

        public double Hours { get; }

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

        /// <summary>
        /// Gets the pay of part time workers, by multiplying rate and hours, with no overtime.
        /// </summary>
        /// <returns>The weekly pay</returns>
        public override double GetPay()
        {
            //since overtime doesn't apply to parttime employees, the pay can just use the one formula
            double pay = Hours * Rate;

            return pay;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nAddress: {Address}\nPhone: {Phone}\nSIN: {Sin}\n" +
                $"DOB: {Dob}\nDept: {Dept}\nRate: {Rate}\nHours: {Hours}";
        }
    }
}
