namespace APBD_Cw1_s29766.Exceptions;

public class EquipmentNotFoundException : RentalSystemException
{
    public EquipmentNotFoundException(int id) : base($"Equipment with ID {id} was not found.") { }
}