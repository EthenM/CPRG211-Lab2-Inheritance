using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Inheritance
{
    internal class Salaried : Employee
    {
        public double Salary { get; }

        public Salaried()
        {
            Salary = 0;
        }

        public Salaried(string id, string name, string address, string phone, long sin, string dob,
            string dept, double salary)
            : base(id, name, address, phone, sin, dob, dept)
        {
            Salary = salary;
        }

        /// <summary>
        /// Gets the pay for a salaried worker
        /// </summary>
        /// <returns>The weekly pay of the employee</returns>
        public override double GetPay()
        {
            //since there is no formula, the salary itself can just be returned
            return Salary;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nAddress: {Address}\nPhone: {Phone}\nSIN: {Sin}" +
                $"\nDOB: {Dob}\nDept: {Dept}\nSalary: {Salary}";
        }
    }
}
