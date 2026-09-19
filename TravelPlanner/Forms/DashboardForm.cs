using System.Data.SQLite;
using TravelPlanner.Data;
using TravelPlanner.Models;

namespace TravelPlanner.Forms;

public partial class DashboardForm : Form
{
    public DashboardForm()
    {
        InitializeComponent();
        lblWelcome.Text = $"Welcome, {Program.LoggedInUsername}!";
        LoadTrips();
    }

    private void LoadTrips()
    {
        try
        {
            dgvTrips.Rows.Clear();
            dgvTrips.Columns.Clear();

            dgvTrips.Columns.Add("TripID", "ID");
            dgvTrips.Columns.Add("Destination", "Destination");
            dgvTrips.Columns.Add("StartDate", "Start");
            dgvTrips.Columns.Add("EndDate", "End");
            dgvTrips.Columns.Add("Duration", "Days");
            dgvTrips.Columns.Add("Budget", "Budget");
            dgvTrips.Columns.Add("Status", "Status");
            dgvTrips.Columns["TripID"].Visible = false;

            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            string sql = @"SELECT TripID, Destination, StartDate, EndDate, Budget, Status, Notes, ImagePath
                           FROM Trips WHERE UserID = @uid ORDER BY StartDate DESC";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", Program.LoggedInUserID);
            using var reader = cmd.ExecuteReader();

            decimal totalBudget = 0;
            int count = 0;

            while (reader.Read())
            {
                var trip = new Trip
                {
                    TripID = reader.GetInt32(0),
                    Destination = reader.GetString(1),
                    StartDate = DateTime.Parse(reader.GetString(2)),
                    EndDate = DateTime.Parse(reader.GetString(3)),
                    Budget = reader.GetDecimal(4),
                    Status = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    Notes = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    ImagePath = reader.IsDBNull(7) ? "" : reader.GetString(7)
                };

                string status = trip.GetStatusLabel();
                if (chkHideCompleted.Checked && status == "Completed")
                    continue;

                dgvTrips.Rows.Add(
                    trip.TripID,
                    trip.Destination,
                    trip.StartDate.ToString("yyyy-MM-dd"),
                    trip.EndDate.ToString("yyyy-MM-dd"),
                    trip.GetDuration(),
                    $"${trip.Budget:F2}",
                    status);

                totalBudget += trip.Budget;
                count++;
            }

            lblSummary.Text = $"Trips: {count}  |  Total budget: ${totalBudget:F2}";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading trips: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private int? GetSelectedTripId()
    {
        if (dgvTrips.CurrentRow == null)
        {
            MessageBox.Show("Please select a trip first.", "Selection Required",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }
        return Convert.ToInt32(dgvTrips.CurrentRow.Cells["TripID"].Value);
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var form = new TripForm();
        if (form.ShowDialog() == DialogResult.OK)
            LoadTrips();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        int? tripId = GetSelectedTripId();
        if (tripId == null) return;

        var form = new TripForm(tripId.Value);
        if (form.ShowDialog() == DialogResult.OK)
            LoadTrips();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        int? tripId = GetSelectedTripId();
        if (tripId == null) return;

        var result = MessageBox.Show("Delete this trip and all its expenses?",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes) return;

        try
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();

            using (var cmd = new SQLiteCommand("DELETE FROM Expenses WHERE TripID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", tripId.Value);
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SQLiteCommand("DELETE FROM Trips WHERE TripID = @id AND UserID = @uid", conn))
            {
                cmd.Parameters.AddWithValue("@id", tripId.Value);
                cmd.Parameters.AddWithValue("@uid", Program.LoggedInUserID);
                cmd.ExecuteNonQuery();
            }

            LoadTrips();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error deleting trip: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnExpenses_Click(object sender, EventArgs e)
    {
        int? tripId = GetSelectedTripId();
        if (tripId == null) return;

        var form = new ExpensesForm(tripId.Value);
        form.ShowDialog();
        LoadTrips();
    }

    private void btnRefresh_Click(object sender, EventArgs e) => LoadTrips();

    private void chkHideCompleted_CheckedChanged(object sender, EventArgs e) => LoadTrips();

    private void btnLogout_Click(object sender, EventArgs e) => Close();
}
