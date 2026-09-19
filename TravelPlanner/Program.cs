using TravelPlanner.Data;
using TravelPlanner.Forms;

namespace TravelPlanner;

static class Program
{
    public static int LoggedInUserID { get; set; }
    public static string LoggedInUsername { get; set; } = string.Empty;

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        DatabaseHelper.Initialize();
        Application.Run(new LoginForm());
    }
}
