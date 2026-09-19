using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace TravelPlanner.Data;

public static class DatabaseHelper
{
    public const string ConnectionString = "Data Source=travelplanner.db;Version=3;";

    public static void Initialize()
    {
        try
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            string createUsers = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    PasswordHash TEXT NOT NULL
                );";

            string createTrips = @"
                CREATE TABLE IF NOT EXISTS Trips (
                    TripID INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserID INTEGER,
                    Destination TEXT NOT NULL,
                    StartDate TEXT,
                    EndDate TEXT,
                    Budget REAL,
                    Status TEXT,
                    Notes TEXT,
                    ImagePath TEXT,
                    FOREIGN KEY(UserID) REFERENCES Users(UserID)
                );";

            string createExpenses = @"
                CREATE TABLE IF NOT EXISTS Expenses (
                    ExpenseID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TripID INTEGER,
                    Description TEXT,
                    Amount REAL,
                    Category TEXT,
                    ExpenseDate TEXT,
                    FOREIGN KEY(TripID) REFERENCES Trips(TripID)
                );";

            using (var cmd = new SQLiteCommand(createUsers, conn))
                cmd.ExecuteNonQuery();
            using (var cmd = new SQLiteCommand(createTrips, conn))
                cmd.ExecuteNonQuery();
            using (var cmd = new SQLiteCommand(createExpenses, conn))
                cmd.ExecuteNonQuery();

            SeedDefaultUser(conn);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Database initialization failed: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void SeedDefaultUser(SQLiteConnection conn)
    {
        using var check = new SQLiteCommand("SELECT COUNT(*) FROM Users", conn);
        long count = (long)check.ExecuteScalar()!;
        if (count > 0) return;

        string sql = "INSERT INTO Users (Username, PasswordHash) VALUES (@user, @hash)";
        using var cmd = new SQLiteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@user", "huda");
        cmd.Parameters.AddWithValue("@hash", HashPassword("huda123"));
        cmd.ExecuteNonQuery();
    }

    public static string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }

    public static bool ValidateLogin(string username, string password, out int userId)
    {
        userId = 0;
        try
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            string sql = "SELECT UserID, PasswordHash FROM Users WHERE Username = @user";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@user", username.Trim());
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return false;

            string storedHash = reader.GetString(1);
            if (storedHash != HashPassword(password)) return false;

            userId = reader.GetInt32(0);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
