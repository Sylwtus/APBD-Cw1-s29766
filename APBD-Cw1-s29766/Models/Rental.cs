namespace APBD_Cw1_s29766.Models;

public class Rental
{
    public int RentalId { get; set; }
    public Equipment Equipment { get; set; }
    public User User { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; } 
    public decimal LateFee { get; set; }
    public bool IsReturnedOnTime => ReturnDate.HasValue && ReturnDate <= DueDate;
}
