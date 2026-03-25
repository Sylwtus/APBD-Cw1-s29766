namespace APBD_Cw1_s29766.Exceptions;

public abstract class RentalSystemException : Exception
{
    public RentalSystemException(string text) : base(text) { }
}