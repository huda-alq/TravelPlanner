namespace TravelPlanner.Models;

public class Trip
{
    public int TripID { get; set; }
    public string Destination { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }
    public string Status { get; set; } = "Upcoming";
    public string Notes { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;

    public int GetDuration()
    {
        return (EndDate - StartDate).Days;
    }

    public string GetStatusLabel()
    {
        if (DateTime.Now.Date < StartDate.Date) return "Upcoming";
        if (DateTime.Now.Date > EndDate.Date) return "Completed";
        return "Ongoing";
    }

    public override string ToString()
    {
        return $"{Destination} ({StartDate:MMM d} – {EndDate:MMM d})";
    }
}
