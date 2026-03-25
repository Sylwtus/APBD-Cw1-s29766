namespace APBD_Cw1_s29766.Models;

public abstract class  Equipment
{
    public int Id { get; set;  }
    public string Name { get; set;  }
    public string Description { get; set;  }
    public bool IsAvailable { get; set; } = true;
    public DateTime PurchaseDate { get; set; }
}