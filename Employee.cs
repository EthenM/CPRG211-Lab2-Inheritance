using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Inheritance
{

    public enum EmployeeType { Salaried, Wage, PartTime }

    internal class Employee
    {
        //adding nullible to all strings to ensure they don't complain if something goes wrong
        //and these aren't assigned to.
        
        //all properties will be readonly, to ensure proper encapsulation
        public string? Id { get; }

        public string? Name { get; }

        public string? Address { get; }

        public string? Phone { get; }

        public long Sin {  get; }

        public string? Dob { get; }

        public string? Dept { get; }


        public Employee()
        {
            Id = "";
            Name = "";
            Address = "";
            Phone = "";
            Sin = 0;
            Dob = "";
            Dept = "";
        }

        public Employee(string id, string name, string address, string phone, long sin, string dob, string dept)
        {
            Id = id;
            Name = name;
            Address = address;
            Phone = phone;
            Sin = sin;
            Dob = dob;
            Dept = dept;
        }

        /// <summary>
        /// determines the type of employee from the first number of the id
        /// </summary>
        /// <param name="id">The id to determine employee type with</param>
        /// <returns>The employee type</returns>
        public static EmployeeType GetEmployeeType(string id)
        {
            //if the value is not changed, then we know the value in id will lead to part time
            EmployeeType type = EmployeeType.PartTime;
            _ = int.TryParse(id[..1], out int firstNumber);

            if (firstNumber >= 0 && firstNumber <= 4)
            {
                type = EmployeeType.Salaried;
            } else if (firstNumber <= 7)
            {
                type = EmployeeType.Wage;
            }

            return type;
        }

        /// <summary>
        /// Determines the worker's weekly pay
        /// </summary>
        /// <returns>The weekly pay of the employee</returns>
        public virtual double GetPay()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nAddress: {Address}\nPhone: {Phone}\nSIN: {Sin}\nDOB: {Dob}\nDept: {Dept}";
        }
    }
}
