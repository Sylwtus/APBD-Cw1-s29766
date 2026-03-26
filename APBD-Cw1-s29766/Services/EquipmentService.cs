using APBD_Cw1_s29766.Models;
using APBD_Cw1_s29766.Exceptions;
namespace APBD_Cw1_s29766.Services;

public class EquipmentService
{
    private List<Equipment> _equipments = new();
    private int _nextId = 1;

    public void AddEquipment(Equipment equipment)
    {
        if (equipment == null)
            throw new InvalidRentalOperationException("Equipment cannot be null");

        equipment.Id = _nextId++;
        equipment.IsAvailable = true;
        _equipments.Add(equipment);
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipments;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipments.Where(e => e.IsAvailable).ToList();
    }

    public Equipment GetById(int id)
    {
        return _equipments.FirstOrDefault(e => e.Id == id);
    }
}