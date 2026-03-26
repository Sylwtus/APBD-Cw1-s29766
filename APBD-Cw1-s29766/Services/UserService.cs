using APBD_Cw1_s29766.Exceptions;
using APBD_Cw1_s29766.Models;

namespace APBD_Cw1_s29766.Services;

public class UserService
{
    private List<User> _users = new();

    public void AddUser(User user)
    {
        if (user == null)
            throw new InvalidRentalOperationException("User cannot be null");

        _users.Add(user);
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }

    public User GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }
}