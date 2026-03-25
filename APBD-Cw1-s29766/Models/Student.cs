using APBD_Cw1_s29766.Enums;

namespace APBD_Cw1_s29766.Models;

public class Student (string fName, string lName, string studentNumber) : User(fName, lName, UserType.Student)
{
    public string StudentNumber { get; set; } = studentNumber;
}