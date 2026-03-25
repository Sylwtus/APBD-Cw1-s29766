namespace APBD_Cw1_s29766.Exceptions;

public class UserNotFoundException : RentalSystemException
{
    public UserNotFoundException(int id) : base($"User with ID {id} was not found.") { }
}
