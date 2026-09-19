using System.Data.SQLite;
using TravelPlanner.Data;
using TravelPlanner.Models;

namespace TravelPlanner.Forms;

public partial class TripForm : Form
{
    private readonly int? _tripId;
    private readonly bool _isEdit;

    public TripForm(int? tripId = null)
    {
        _tripId = tripId;
        _isEdit = tripId.HasValue;
        InitializeComponent();

        cboStatus.Items.AddRange(["Upcoming", "Ongoing", "Completed"]);
        cboStatus.SelectedIndex = 0;

        if (_isEdit)
        {
            Text = "Edit Trip";
            LoadTrip(tripId!.Value);
        }
        else
        {
            Text = "Add Trip";
            dateStart.Value = DateTime.Today;
            dateEnd.Value = DateTime.Today.AddDays(7);
        }
    }

    private void LoadTrip(int tripId)
    {
        try
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            string sql = @"SELECT Destination, StartDate, EndDate, Budget, Status, Notes, ImagePath
                           FROM Trips WHERE TripID = @id AND UserID = @uid";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", tripId);
            cmd.Parameters.AddWithValue("@uid", Program.LoggedInUserID);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("Trip not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            txtDestination.Text = reader.GetString(0);
            dateStart.Value = DateTime.Parse(reader.GetString(1));
            dateEnd.Value = DateTime.Parse(reader.GetString(2));
            txtBudget.Text = reader.GetDecimal(3).ToString("F2");
            string status = reader.IsDBNull(4) ? "Upcoming" : reader.GetString(4);
            cboStatus.SelectedItem = status;
            txtNotes.Text = reader.IsDBNull(5) ? "" : reader.GetString(5);
            lblImagePath.Text = reader.IsDBNull(6) ? "" : reader.GetString(6);

            if (!string.IsNullOrEmpty(lblImagePath.Text) && File.Exists(lblImagePath.Text))
            {
                pictureBoxTrip.Image = Image.FromFile(lblImagePath.Text);
                pictureBoxTrip.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading trip: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnUploadImage_Click(object sender, EventArgs e)
    {
        using OpenFileDialog ofd = new();
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            pictureBoxTrip.Image = Image.FromFile(ofd.FileName);
            pictureBoxTrip.SizeMode = PictureBoxSizeMode.Zoom;
            lblImagePath.Text = ofd.FileName;
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtDestination.Text))
        {
            MessageBox.Show("Destination cannot be empty.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (dateEnd.Value < dateStart.Value)
        {
            MessageBox.Show("End date must be after start date.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!decimal.TryParse(txtBudget.Text, out decimal budget))
        {
            MessageBox.Show("Budget must be a valid number.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            Trip t = new()
            {
                Destination = txtDestination.Text.Trim(),
                StartDate = dateStart.Value.Date,
                EndDate = dateEnd.Value.Date,
                Budget = budget,
                Notes = txtNotes.Text.Trim(),
                ImagePath = lblImagePath.Text
            };
            t.Status = cboStatus.SelectedItem?.ToString() ?? t.GetStatusLabel();

            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();

            if (_isEdit)
            {
                string sql = @"UPDATE Trips SET Destination=@dest, StartDate=@start, EndDate=@end,
                               Budget=@budget, Status=@status, Notes=@notes, ImagePath=@img
                               WHERE TripID=@id AND UserID=@uid";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", _tripId!.Value);
                cmd.Parameters.AddWithValue("@uid", Program.LoggedInUserID);
                cmd.Parameters.AddWithValue("@dest", t.Destination);
                cmd.Parameters.AddWithValue("@start", t.StartDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@end", t.EndDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@budget", t.Budget);
                cmd.Parameters.AddWithValue("@status", t.Status);
                cmd.Parameters.AddWithValue("@notes", t.Notes);
                cmd.Parameters.AddWithValue("@img", t.ImagePath);
                cmd.ExecuteNonQuery();
            }
            else
            {
                string sql = @"INSERT INTO Trips
                    (UserID, Destination, StartDate, EndDate, Budget, Status, Notes, ImagePath)
                    VALUES (@uid, @dest, @start, @end, @budget, @status, @notes, @img)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uid", Program.LoggedInUserID);
                cmd.Parameters.AddWithValue("@dest", t.Destination);
                cmd.Parameters.AddWithValue("@start", t.StartDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@end", t.EndDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@budget", t.Budget);
                cmd.Parameters.AddWithValue("@status", t.Status);
                cmd.Parameters.AddWithValue("@notes", t.Notes);
                cmd.Parameters.AddWithValue("@img", t.ImagePath);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Trip saved successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error saving trip: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
