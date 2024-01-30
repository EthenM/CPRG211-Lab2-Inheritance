using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Inheritance
{
    internal class Wage : Employee
    {
        public double Rate { get; set; }

        public double Hours { get; set; }

        private const int _FOURTY_HOURS = 40;
        private const double _TIME_AND_A_HALF = 1.5;

        Wage()
        {
            Rate = 0;
            Hours = 0;
        }

        public Wage(string id, string name, string address, string phone, long sin, string dob,
            string dept, double rate, double hours)
            :base(id, name, address, phone, sin, dob, dept)
        {
            Rate = rate;
            Hours = hours;
        }

        public override double GetPay()
        {
            double pay = 0;

            if (Hours > _FOURTY_HOURS)
            {
                double hoursForOvertime = Hours - _FOURTY_HOURS;
                pay = GetPayOvertime(hoursForOvertime);
            } else
            {
                pay = Hours * Rate;
            }

            return pay;
        }

        private double GetPayOvertime(double overtimeHours)
        {
            double totalPay = 0;

            totalPay += Rate * _FOURTY_HOURS;
            totalPay += overtimeHours * (Rate * _TIME_AND_A_HALF);

            return totalPay;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nAddress: {Address}\nPhone: {Phone}\nSIN: {Sin}\nDOB: {Dob}\nDept: {Dept}\nRate: {Rate}\nHours: {Hours}";
        }
    }
}
