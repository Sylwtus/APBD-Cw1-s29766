namespace APBD_Cw1_s29766.Models;

public class Report
{
    private List<Equipment> _equipments;
    private List<User> _users;
    private List<Rental> _rentals;
    
    public Report(List<Equipment> equipments, List<User> users, List<Rental> rentals)
    {
        _equipments = equipments;
        _users = users;
        _rentals = rentals;
    }

    public void PrintSummary()
    {
        int available = 0;
        int rented = 0;
        int activeRentals = 0;
        int lateRentals = 0;

        foreach (Equipment e in _equipments)
        {
            if (e.IsAvailable)
                available++;
            else
                rented++;
        }

        foreach (Rental r in _rentals)
        {
            if (r.ReturnDate == null)
            {
                activeRentals++;
                if (r.DueDate < DateTime.Now)
                    lateRentals++;
            }
        }
        Console.WriteLine("Report");
        Console.WriteLine("Total equipment: " + _equipments.Count);
        Console.WriteLine("Available: " + available);
        Console.WriteLine("Rented: " + rented);
        Console.WriteLine("Total users: " + _users.Count);
        Console.WriteLine("Active rentals: " + activeRentals);
        Console.WriteLine("Late rentals: " + lateRentals);
    }
}