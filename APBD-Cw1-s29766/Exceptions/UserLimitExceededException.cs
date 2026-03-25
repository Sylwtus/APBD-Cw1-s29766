namespace APBD_Cw1_s29766.Exceptions;

public class UserLimitExceededException : RentalSystemException
{
    public UserLimitExceededException() : base("User has exceeded the maximum number of active rentals.") { }
}