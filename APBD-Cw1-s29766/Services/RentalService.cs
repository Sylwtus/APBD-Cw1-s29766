using APBD_Cw1_s29766.Exceptions;
using APBD_Cw1_s29766.Models;
using APBD_Cw1_s29766.Enums;

namespace APBD_Cw1_s29766.Services;


public class RentalService
{
    private List<Rental> _rentals = new();
    private int _nextRentalId = 1;

    private EquipmentService _equipmentService;
    private UserService _userService;

    public RentalService(EquipmentService equipmentService, UserService userService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
    }

    public BorrowResults BorrowEquipment(int equipmentId, int userId, int days)
    {
        if (days <= 0)
            throw new InvalidRentalOperationException("Days must be greater than 0");

        Equipment equipment = _equipmentService.GetById(equipmentId);
        if (equipment == null)
            return BorrowResults.EquipmentNotFound;

        User user = _userService.GetById(userId);
        if (user == null)
            return BorrowResults.UserNotFound;

        if (!equipment.IsAvailable)
            return BorrowResults.EquipmentNotAvailable;

        int active = _rentals.Count(r => r.User.Id == userId && r.ReturnDate == null);

        int limit = user.UserType switch
        {
            UserType.Student => BusinessRules.StudentMaxRentals,
            UserType.Employee => BusinessRules.EmployeeMaxRentals,
            _ => 0
        };

        if (active >= limit)
            return BorrowResults.UserLimitExceeded;

        Rental rental = new Rental
        {
            RentalId = _nextRentalId++,
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

        if (rental == null)
            throw new RentalNotFoundException(rentalId);

        if (rental.ReturnDate != null)
            throw new InvalidRentalOperationException("Already returned");

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
}