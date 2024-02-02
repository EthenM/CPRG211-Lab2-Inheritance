using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Inheritance
{
    //String formatting found from: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            //this will allow the use of non-static methods coming from the "main" function
            EntryPoint entry = new();
            entry.Run();
        }
    }

    public class EntryPoint
    {
        //The path may be prone to changing, so const may not be the best choice.
        //However, since the text file shouldn't change locations, using readonly will work
        private readonly string EMPLOYEE_FILE_PATH = ".\\employees.txt";

        /// <summary>
        /// A non-static method to run the main program
        /// </summary>
        public void Run()
        {
            List<Employee> employees = PopulateEmployeeList();

            string averagePay = GetAveragePay(employees).ToString("C");
            Console.WriteLine($"average: {averagePay}\n");

            string highestWage = GetHighestWage(employees);
            Console.WriteLine(highestWage);

            string lowestSalary = GetLowestSalary(employees);
            Console.WriteLine(lowestSalary);

            Dictionary<EmployeeType, double> percents = GetPercents(employees);
            
            //this will loop through each dictionary entry and print the percent.
            //I can dynamically print the type due to the weird nature of enums, where they function as both ints and strings
            foreach (KeyValuePair<EmployeeType, double> pair in percents)
            {
                string percent = pair.Value.ToString("P");
                //this will ensure that the type of employee is lowercase
                string employeeType = pair.Key.ToString().ToLower();

                Console.WriteLine($"The percent of {employeeType} employees is {percent}");
            }

        }

        /// <summary>
        /// Populates a list of employees from the employees.txt
        /// </summary>
        /// <returns>The populated list of employees</returns>
        private List<Employee> PopulateEmployeeList()
        {
            List<Employee> employees = new();

            //pull the data from the file
            //took it out of the res folder, as it would not copy down.
            //Had to change the text file to always copy and compile as content
            //to ensure it transfers down upon compilation
            List<string> people = File.ReadAllLines(EMPLOYEE_FILE_PATH).ToList();

            foreach (string person in people)
            {
                List<string> values = person.Split(':').ToList();

                //pulling out the values and storing them in easy to read variables
                string id = values[0];
                string name = values[1];
                string address = values[2];
                string phone = values[3];
                _ = long.TryParse(values[4], out long sin);
                string dob = values[5];
                string dept = values[6];
                double salary = 0;
                double rate = 0;
                double hours = 0;

                //we need to fill different variables depending on which are present in the list
                switch (values.Count)
                {
                    case 8:
                        _ = double.TryParse(values[7], out salary);

                        break;
                    case 9:
                        _ = double.TryParse(values[7], out rate);
                        _ = double.TryParse(values[8], out hours);

                        break;
                }

                //this will tell us what employee to create
                EmployeeType employeeType = Employee.GetEmployeeType(id);

                //since all of the cases are apparently the same scope, I chose to instantiate the people in the add method.
                switch (employeeType)
                {
                    case EmployeeType.Salaried:
                        employees.Add(new Salaried(id, name, address, phone, sin, dob, dept, salary));

                        break;
                    case EmployeeType.Wage:
                        employees.Add(new Wage(id, name, address, phone, sin, dob, dept, rate, hours));

                        break;
                    case EmployeeType.PartTime:
                        employees.Add(new Wage(id, name, address, phone, sin, dob, dept, rate, hours));

                        break;
                }
            }

            return employees;
        }

        /// <summary>
        /// Gets the average pay of all of the employees
        /// </summary>
        /// <param name="employees">The list of employees to use</param>
        /// <returns>The average pay, in the form of a double</returns>
        private double GetAveragePay(List<Employee> employees)
        {
            double average = 0;

            foreach (Employee employee in employees)
            {
                //each employee has a method for GetPay, which gives the weekly pay
                //I converted GetPay to a virtual function to make this step easier.
                //It being virtual allows me to make the code simpler, as I do not
                //need to check wich class.
                average += employee.GetPay();
            }

            average /= employees.Count;

            return average;
        }

        /// <summary>
        /// Gets the highest wage of all the waged employees
        /// </summary>
        /// <param name="employees">The list of employees to search</param>
        /// <returns>A string stating the name and wage of the employee with the highest wage</returns>
        private string GetHighestWage(List<Employee> employees)
        {
            Employee? highest = null;

            foreach (Employee employee in employees)
            {
                if (employee is Wage)
                {
                    //if the current highest does not exist, or the current pay is higher, change it out
                    if (highest == null || employee.GetPay() > highest.GetPay())
                    {
                        highest = employee;
                    }
                }
            }

            //convert it to a formatted string, so the output looks unified
            string? highestWage = highest?.GetPay().ToString("C");

            return $"The highest wage belongs to {highest?.Name} with a wage of {highestWage}\n";
        }

        /// <summary>
        /// Gets the lowest salary of all of the salaried employees
        /// </summary>
        /// <param name="employees">The list of employees to search</param>
        /// <returns>A string stating the name and salary of the employee with the lowest salary</returns>
        private string GetLowestSalary(List<Employee> employees)
        {
            //setting this to nullible to get rid of any warnings
            Employee? lowest = null;

            foreach (Employee employee in employees)
            {
                if (employee is Salaried)
                {
                    if (lowest == null || employee.GetPay() < lowest.GetPay())
                    {
                        lowest = employee;
                    }
                }
            }

            //convert it to a formatted string, so the output looks unified
            string? lowestSalary = lowest?.GetPay().ToString("C");

            return $"The lowest salary belongs to {lowest?.Name} with a salary of {lowestSalary}\n";
        }

        /// <summary>
        /// Builds a dictionary of the percent of employees that fall in each category
        /// </summary>
        /// <param name="employees">The list of employees to get the percentage from</param>
        /// <returns>A dictionary full of percents of employees.</returns>
        private Dictionary<EmployeeType, double> GetPercents(List<Employee> employees)
        {
            //dictionaries will allow the storage of all three categories in one object
            //while keeping them separated
            Dictionary<EmployeeType, double> percents = new();

            //initializing the dictionaries here, to save space later
            percents[EmployeeType.Salaried] = 0;
            percents[EmployeeType.Wage] = 0;
            percents[EmployeeType.PartTime] = 0;

            //this will count the number of each employee and add it to the dictionary
            //to be used to calculate percents later in the method
            foreach (Employee employee in employees)
            {
                if (employee is Salaried)
                {
                    percents[EmployeeType.Salaried] += 1;
                } else if (employee is Wage)
                {
                    percents[EmployeeType.Wage] += 1;
                } else if (employee is PartTime)
                {
                    percents[EmployeeType.PartTime] += 1;
                }
            }

            //the last thing to do is set the actual percentage.

            //found the type calling from here: https://stackoverflow.com/questions/141088/how-to-iterate-over-a-dictionary
            foreach (KeyValuePair<EmployeeType, double> pair in percents)
            {
                //divide the count by the amount, multiply by 100
                double percent = pair.Value / employees.Count;

                percents[pair.Key] = percent;
            }

            return percents;
        }
    }
}
