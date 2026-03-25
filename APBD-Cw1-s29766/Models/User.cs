using APBD_Cw1_s29766.Enums;

namespace APBD_Cw1_s29766.Models;

public abstract class User (string fName, string lName, UserType userType)
{
    private static int _nextId = 1;
    public int Id { get; set; } = _nextId++;
    public string FName { get; set; } = fName;
    public string LName { get; set; } = lName;
    public UserType UserType { get; set; } = userType;
}