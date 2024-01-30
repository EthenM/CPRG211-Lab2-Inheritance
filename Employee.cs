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
        
        //all setters will be private, to ensure proper encapsulation
        public string? Id { get; private set; }

        public string? Name { get; private set; }

        public string? Address { get; private set; }

        public string? Phone { get; private set; }

        public long Sin {  get; private set; }

        public string? Dob { get; private set; }

        public string? Dept { get; private set; }


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
