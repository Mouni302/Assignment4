using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

abstract class Employee
{
    private static int uniqueIdCounter = 1;
    private int id;
    private string name;
    private string reportingManager;

    public Employee(string name, string reportingManager)
    {
        this.id = uniqueIdCounter++;
        this.name = name;
        this.reportingManager = reportingManager;
    }

    public string GetDetails()
    {
        return $"ID: {id}, Name: {name}, Reporting Manager: {reportingManager}";
    }

    public abstract void DisplayDetails();
}

class OnContractEmployee : Employee
{
    private string contractDate;
    private int duration; // in months
    private double charges;

    public OnContractEmployee(string name, string reportingManager, string contractDate, int duration, double charges)
        : base(name, reportingManager)
    {
        this.contractDate = contractDate;
        this.duration = duration;
        this.charges = charges;
    }

    public override void DisplayDetails()
    {
        Console.WriteLine(GetDetails());
        Console.WriteLine($"Contract Date: {contractDate}, Duration: {duration} months, Charges: {charges}");
    }
}

class OnPayrollEmployee : Employee
{
    private string joiningDate;
    private int experience; // in years
    private double basicSalary;
    private double da;
    private double hra;
    private double pf;
    private double netSalary;

    public OnPayrollEmployee(string name, string reportingManager, string joiningDate, int experience, double basicSalary)
        : base(name, reportingManager)
    {
        this.joiningDate = joiningDate;
        this.experience = experience;
        this.basicSalary = basicSalary;
        CalculateSalaryComponents();
    }

    private void CalculateSalaryComponents()
    {
        if (experience > 10)
        {
            da = 0.1 * basicSalary;
            hra = 0.085 * basicSalary;
            pf = 6200;
        }
        else if (experience > 7)
        {
            da = 0.07 * basicSalary;
            hra = 0.065 * basicSalary;
            pf = 4100;
        }
        else if (experience > 5)
        {
            da = 0.041 * basicSalary;
            hra = 0.038 * basicSalary;
            pf = 1800;
        }
        else
        {
            da = 0.019 * basicSalary;
            hra = 0.02 * basicSalary;
            pf = 1200;
        }

        netSalary = basicSalary + da + hra - pf;
    }

    public override void DisplayDetails()
    {
        Console.WriteLine(GetDetails());
        Console.WriteLine($"Joining Date: {joiningDate}, Experience: {experience} years");
        Console.WriteLine($"Basic Salary: {basicSalary}, DA: {da:F2}, HRA: {hra:F2}, PF: {pf}");
        Console.WriteLine($"Net Salary: {netSalary:F2}");
    }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>();

        while (true)
        {
            Console.WriteLine("Enter type of employee (1 for Contract, 2 for Payroll, 0 to Exit): ");
            string empType = Console.ReadLine();

            if (empType == "1")
            {
                Console.WriteLine("Enter Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Reporting Manager: ");
                string reportingManager = Console.ReadLine();
                Console.WriteLine("Enter Contract Date (YYYY-MM-DD): ");
                string contractDate = Console.ReadLine();
                Console.WriteLine("Enter Duration (in months): ");
                int duration = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Charges: ");
                double charges = double.Parse(Console.ReadLine());

                employees.Add(new OnContractEmployee(name, reportingManager, contractDate, duration, charges));
            }
            else if (empType == "2")
            {
                Console.WriteLine("Enter Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Reporting Manager: ");
                string reportingManager = Console.ReadLine();
                Console.WriteLine("Enter Joining Date (YYYY-MM-DD): ");
                string joiningDate = Console.ReadLine();
                Console.WriteLine("Enter Experience (in years): ");
                int experience = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Basic Salary: ");
                double basicSalary = double.Parse(Console.ReadLine());

                employees.Add(new OnPayrollEmployee(name, reportingManager, joiningDate, experience, basicSalary));
            }
            else if (empType == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        Console.WriteLine("\nEmployee Details:\n");
        foreach (var emp in employees)
        {
            emp.DisplayDetails();
            Console.WriteLine(new string('-', 40));
        }

        Console.WriteLine($"Total Number of Employees: {employees.Count}");
    }
}

