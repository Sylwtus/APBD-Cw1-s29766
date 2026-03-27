using APBD_Cw1_s29766.Models;
using APBD_Cw1_s29766.Enums;
using APBD_Cw1_s29766.Services;

class Program
{
    static void Main(string[] args)
    {
        EquipmentService equipmentService = new EquipmentService();
        UserService userService = new UserService();
        RentalService rentalService = new RentalService(equipmentService, userService);

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n******** MENU *********");
            Console.WriteLine("1. Add user");
            Console.WriteLine("2. Add equipment");
            Console.WriteLine("3. Show all equipment");
            Console.WriteLine("4. Show available equipment");
            Console.WriteLine("5. Borrow equipment");
            Console.WriteLine("6. Return equipment");
            Console.WriteLine("7. Show user rentals");
            Console.WriteLine("8. Show late rentals");
            Console.WriteLine("9. Show summary");
            Console.WriteLine("0. Exit");

            Console.Write("Choose option: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("First name: ");
                        string fName = Console.ReadLine();

                        Console.Write("Last name: ");
                        string lName = Console.ReadLine();

                        Console.Write("Type (1-Student, 2-Employee): ");
                        int type = int.Parse(Console.ReadLine());

                        User user;

                        if (type == 1)
                        {
                            Console.Write("Student number: ");
                            string studentNumber = Console.ReadLine();

                            user = new Student(fName, lName, studentNumber);
                        }
                        else
                        {
                            Console.Write("Employee number: ");
                            string employeeNumber = Console.ReadLine();

                            Console.Write("Department: ");
                            string department = Console.ReadLine();

                            user = new Employee(fName, lName, employeeNumber, department);
                        }

                        userService.AddUser(user);

                        Console.WriteLine("User added!");
                        break;
                        

                    case "2":

                        Console.WriteLine("1. Laptop");
                        Console.WriteLine("2. Projector");
                        Console.WriteLine("3. Camera");

                        Console.Write("Choose option: ");
                        string choiceEq = Console.ReadLine();

                        switch (choiceEq)
                        {
                            case "1":
                            Console.Write("Laptop name: ");
                            string laptopName = Console.ReadLine();

                            Console.Write("Brand: ");
                            string brand = Console.ReadLine();

                            Console.Write("Storage (GB): ");
                            int storage = int.Parse(Console.ReadLine());

                            Laptop laptop = new Laptop();
                            laptop.Name = laptopName;
                            laptop.Brand = brand;
                            laptop.Storage = storage;

                            equipmentService.AddEquipment(laptop);

                            Console.WriteLine("Laptop added!");
                            break;

                        case "2":
                            Console.Write("Projector name: ");
                            string projectorName = Console.ReadLine();

                            Console.Write("Matrix type: ");
                            string matrix = Console.ReadLine();

                            Console.Write("Resolution: ");
                            string resolution = Console.ReadLine();

                            Projector projector = new Projector();
                            projector.Name = projectorName;
                            projector.MatrixType = matrix;
                            projector.Resolutin = resolution;

                            equipmentService.AddEquipment(projector);

                            Console.WriteLine("Projector added!");
                            break;

                        case "3":
                            Console.Write("Camera name: ");
                            string cameraName = Console.ReadLine();

                            Console.Write("Megapixels: ");
                            int mp = int.Parse(Console.ReadLine());

                            Console.Write("Lens type: ");
                            string lens = Console.ReadLine();

                            Camera camera = new Camera();
                            camera.Name = cameraName;
                            camera.MegaPixels = mp;
                            camera.LensType = lens;

                            equipmentService.AddEquipment(camera);

                            Console.WriteLine("Camera added!");
                            break;

                        default:
                            Console.WriteLine("Wrong option");
                            break;
                        }

                        break; 
                        

                    case "3":
                        foreach (Equipment e in equipmentService.GetAllEquipment())
                        {
                            Console.WriteLine(e.Id + " - " + e.Name + " - Available: " + e.IsAvailable);
                        }
                        break;

                    case "4":
                        foreach (Equipment e in equipmentService.GetAvailableEquipment())
                        {
                            Console.WriteLine(e.Id + " - " + e.Name);
                        }
                        break;

                    case "5":
                        Console.Write("Equipment ID: ");
                        int eqId = int.Parse(Console.ReadLine());

                        Console.Write("User ID: ");
                        int uId = int.Parse(Console.ReadLine());

                        Console.Write("Days: ");
                        int days = int.Parse(Console.ReadLine());

                        BorrowResults result = rentalService.BorrowEquipment(eqId, uId, days);

                        switch (result)
                        {
                            case BorrowResults.Success:
                                Console.WriteLine("Borrow successful!");
                                break;
                            case BorrowResults.EquipmentNotFound:
                                Console.WriteLine("Equipment not found");
                                break;
                            case BorrowResults.UserNotFound:
                                Console.WriteLine("User not found");
                                break;
                            case BorrowResults.EquipmentNotAvailable:
                                Console.WriteLine("Equipment not available");
                                break;
                            case BorrowResults.UserLimitExceeded:
                                Console.WriteLine("User exceeded limit");
                                break;
                        }
                        break;

                    case "6":
                        Console.Write("Rental ID: ");
                        int rentalId = int.Parse(Console.ReadLine());

                        rentalService.ReturnEquipment(rentalId);
                        Console.WriteLine("Returned!");
                        break;

                    case "7":
                        Console.Write("User ID: ");
                        int userId = int.Parse(Console.ReadLine());

                        foreach (Rental r in rentalService.GetAllRentals())
                        {
                            if (r.User.Id == userId && r.ReturnDate == null)
                            {
                                Console.WriteLine("Rental " + r.RentalId + " - " + r.Equipment.Name);
                            }
                        }
                        break;

                    case "8":
                        foreach (Rental r in rentalService.GetAllRentals())
                        {
                            if (r.ReturnDate == null && r.DueDate < DateTime.Now)
                            {
                                Console.WriteLine("Late: " + r.RentalId + " - " + r.Equipment.Name);
                            }
                        }
                        break;

                    case "9":
                        Console.WriteLine("\n--- Users ---");
                        foreach (var u in userService.GetAllUsers())
                        {
                            string userType = u.UserType.ToString();
                            string details = u switch
                            {
                                Student s => $"StudentNumber: {s.StudentNumber}",
                                Employee e => $"EmployeeNumber: {e.EmployeeNumber}, Department: {e.Department}",
                                _ => ""
                            };
                            Console.WriteLine($"ID: {u.Id}, Name: {u.FName} {u.LName}, Type: {userType}, {details}");
                        }

                        Console.WriteLine("\n--- Equipment ---");
                        foreach (var eq in equipmentService.GetAllEquipment())
                        {
                            Console.WriteLine($"ID: {eq.Id}, Name: {eq.Name}, Available: {eq.IsAvailable}");
                        }

                        Console.WriteLine("\n--- Rentals ---");
                        foreach (var r in rentalService.GetAllRentals())
                        {
                            string returned = r.ReturnDate.HasValue ? $"Returned on {r.ReturnDate.Value}" : "Not returned";
                            Console.WriteLine($"Rental ID: {r.RentalId}, Equipment: {r.Equipment.Name}, User: {r.User.FName} {r.User.LName}, Due: {r.DueDate.ToShortDateString()}, {returned}");
                        }
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Wrong option");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
    }
}