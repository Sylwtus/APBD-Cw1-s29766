using APBD_Cw1_s29766.Enums;

namespace APBD_Cw1_s29766.Models;

public class Employee(string fName, string lName, string employeeNumber, string department) : User(fName, lName, UserType.Employee)
{
    public string EmployeeNumber { get; set; } = employeeNumber;
    public string Department { get; set; } = department;
}