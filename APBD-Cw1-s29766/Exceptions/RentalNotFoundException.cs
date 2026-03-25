namespace APBD_Cw1_s29766.Exceptions;

public class RentalNotFoundException : RentalSystemException
{
    public RentalNotFoundException(int id) : base($"Rental with ID {id} was not found.") { }
}