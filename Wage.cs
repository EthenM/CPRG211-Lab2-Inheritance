using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Inheritance
{
    internal class Wage : Employee
    {
        public double Rate { get; }

        public double Hours { get; }

        //some constants that won't change through the lab
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

        /// <summary>
        /// Determines the pay of the worker, including time-and-a-half for overtime
        /// </summary>
        /// <returns>The weekly pay of the employee</returns>
        public override double GetPay()
        {
            double pay = 0;

            //The overtime cost only needs to be applied if the hours is greater than 40
            if (Hours > _FOURTY_HOURS)
            {
                double hoursForOvertime = Hours - _FOURTY_HOURS;

                //sending the calculation off to another method to keep this one tidy
                pay = GetPayOvertime(hoursForOvertime);
            } else
            {
                pay = Hours * Rate;
            }

            return pay;
        }

        /// <summary>
        /// Determines the pay of the employee if there is overtime
        /// </summary>
        /// <param name="overtimeHours">The number of hours over 40 the employee worked</param>
        /// <returns>The total pay of the employee</returns>
        private double GetPayOvertime(double overtimeHours)
        {
            double totalPay = 0;

            //add the regular and overtime pay to total pay
            totalPay += Rate * _FOURTY_HOURS;
            totalPay += overtimeHours * (Rate * _TIME_AND_A_HALF);

            return totalPay;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nAddress: {Address}\nPhone: {Phone}\nSIN: {Sin}\n" +
                $"DOB: {Dob}\nDept: {Dept}\nRate: {Rate}\nHours: {Hours}";
        }
    }
}
