namespace APBD_Cw1_s29766.Exceptions;

public class EquipmentUnavailableException : RentalSystemException
{
    public EquipmentUnavailableException(int id) : base($"Equipment with ID {id} is not available.") { }
}