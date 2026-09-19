namespace TravelPlanner.Models;

public class Expense
{
    public int ExpenseID { get; set; }
    public int TripID { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Category { get; set; } = "Other";
    public DateTime ExpenseDate { get; set; }

    public string GetFormattedAmount()
    {
        return $"${Amount:F2}";
    }
}
