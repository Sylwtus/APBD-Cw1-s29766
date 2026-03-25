namespace APBD_Cw1_s29766.Exceptions;

public class InvalidRentalOperationException : RentalSystemException
{
    public InvalidRentalOperationException(string message) : base(message) { }
}