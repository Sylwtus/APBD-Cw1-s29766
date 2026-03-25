using APBD_Cw1_s29766.Enums;

namespace APBD_Cw1_s29766.Models;

public class EquipmentService
{
    private List<Equipment> _equipments = new();
    private List<User> _users = new();
    private List<Rental> _rentals = new();
    private int _nextEquipmentId = 1;
    private int _nextRentalId = 1;
    
    public void AddEquipment(Equipment equipment)
    {
        equipment.Id = _nextEquipmentId++;
        equipment.IsAvailable = true;
        _equipments.Add(equipment);
    }
    public List<Equipment> GetAvailableEquipment()
    {
        return _equipments.Where(e => e.IsAvailable).ToList();
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipments;
    }
    public void RegisterUser(User user)
    {
        _users.Add(user);
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }
    public BorrowResults BorrowEquipment(int equipmentId, int userId, int days)
    {
        Equipment equipment = _equipments.FirstOrDefault(e => e.Id == equipmentId);
        if (equipment == null)
            return BorrowResults.EquipmentNotFound;

        User user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            return BorrowResults.UserNotFound;

        if (!equipment.IsAvailable)
            return BorrowResults.EquipmentNotAvailable;

        var activeRentals = _rentals
            .Count(r => r.User.Id == userId && r.ReturnDate == null);

        int limit = user.UserType switch
        {
            UserType.Student => BusinessRules.StudentMaxRentals,
            UserType.Employee => BusinessRules.EmployeeMaxRentals, 
            _ => 0
        };

        if (activeRentals >= limit)
            return BorrowResults.UserLimitExceeded;

        Rental rental = new Rental
        {
            Equipment = equipment,
            User = user,
            BorrowDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(days)
        };

        equipment.IsAvailable = false;
        _rentals.Add(rental);

        return BorrowResults.Success;
    }

    public bool ReturnEquipment(int rentalId)
    {
        Rental rental = _rentals.FirstOrDefault(r => r.RentalId == rentalId);

        if (rental == null || rental.ReturnDate != null)
            return false;

        rental.ReturnDate = DateTime.Now;
        rental.Equipment.IsAvailable = true;

        if (rental.ReturnDate > rental.DueDate)
        {
            int daysLate = (rental.ReturnDate.Value - rental.DueDate).Days;
            rental.LateFee = daysLate * BusinessRules.LateFeePerDay;
        }
        return true;
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals;
    }

    public List<Rental> GetActiveRentals()
    {
        return _rentals.Where(r => r.ReturnDate == null).ToList();
    }

    public List<Rental> GetLateRentals()
    {
        return _rentals
            .Where(r => r.ReturnDate == null && r.DueDate < DateTime.Now)
            .ToList();
    }
    public void PrintReport()
    {
        Console.WriteLine("=== RENTAL REPORT ===");

        foreach (Rental r in _rentals)
        {
            Console.WriteLine($"Rental ID: {r.RentalId}");
            Console.WriteLine($"User: {r.User.FName} {r.User.LName}");
            Console.WriteLine($"Equipment: {r.Equipment.Name}");
            Console.WriteLine($"Borrowed: {r.BorrowDate}");
            Console.WriteLine($"Due: {r.DueDate}");
            Console.WriteLine($"Returned: {r.ReturnDate}");
            Console.WriteLine($"Late fee: {r.LateFee}");
            Console.WriteLine("");
        }
    }

    public void PrintSummary()
    {
        Console.WriteLine("=== SUMMARY ===");

        Console.WriteLine($"Total equipment: {_equipments.Count}");
        Console.WriteLine($"Available: {_equipments.Count(e => e.IsAvailable)}");
        Console.WriteLine($"Rented: {_equipments.Count(e => !e.IsAvailable)}");

        Console.WriteLine($"Total users: {_users.Count}");
        Console.WriteLine($"Active rentals: {_rentals.Count(r => r.ReturnDate == null)}");
        Console.WriteLine($"Late rentals: {_rentals.Count(r => r.ReturnDate == null && r.DueDate < DateTime.Now)}");
    }
}
