using System.Data.SQLite;
using TravelPlanner.Data;
using TravelPlanner.Models;

namespace TravelPlanner.Forms;

public partial class ExpensesForm : Form
{
    private readonly int _tripId;
    private decimal _budget;

    public ExpensesForm(int tripId)
    {
        _tripId = tripId;
        InitializeComponent();

        cboCategory.Items.AddRange(["Food", "Transport", "Stay", "Activity", "Other"]);
        cboCategory.SelectedIndex = 0;
        dateExpense.Value = DateTime.Today;

        LoadTripInfo();
        LoadExpenses();
    }

    private void LoadTripInfo()
    {
        try
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            string sql = "SELECT Destination, Budget FROM Trips WHERE TripID = @id";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", _tripId);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string destination = reader.GetString(0);
                _budget = reader.GetDecimal(1);
                lblTripTitle.Text = $"Expenses – {destination}";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading trip: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadExpenses()
    {
        try
        {
            dgvExpenses.Rows.Clear();
            dgvExpenses.Columns.Clear();

            dgvExpenses.Columns.Add("ExpenseID", "ID");
            dgvExpenses.Columns.Add("Description", "Description");
            dgvExpenses.Columns.Add("Amount", "Amount");
            dgvExpenses.Columns.Add("Category", "Category");
            dgvExpenses.Columns.Add("ExpenseDate", "Date");
            dgvExpenses.Columns["ExpenseID"].Visible = false;

            decimal totalSpent = 0;

            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            string sql = @"SELECT ExpenseID, Description, Amount, Category, ExpenseDate
                           FROM Expenses WHERE TripID = @tid ORDER BY ExpenseDate DESC";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@tid", _tripId);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var expense = new Expense
                {
                    ExpenseID = reader.GetInt32(0),
                    Description = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Amount = reader.GetDecimal(2),
                    Category = reader.IsDBNull(3) ? "Other" : reader.GetString(3),
                    ExpenseDate = DateTime.Parse(reader.GetString(4))
                };

                dgvExpenses.Rows.Add(
                    expense.ExpenseID,
                    expense.Description,
                    expense.GetFormattedAmount(),
                    expense.Category,
                    expense.ExpenseDate.ToString("yyyy-MM-dd"));

                totalSpent += expense.Amount;
            }

            decimal remaining = _budget - totalSpent;
            lblBudgetTracker.Text = $"Budget: ${_budget:F2}  |  Spent: ${totalSpent:F2}  |  Remaining: ${remaining:F2}";
            lblBudgetTracker.ForeColor = remaining < 0 ? Color.Red : Color.DarkGreen;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading expenses: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnAddExpense_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtDescription.Text))
        {
            MessageBox.Show("Description cannot be empty.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
        {
            MessageBox.Show("Amount must be a valid positive number.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            Expense expense = new()
            {
                TripID = _tripId,
                Description = txtDescription.Text.Trim(),
                Amount = amount,
                Category = cboCategory.SelectedItem?.ToString() ?? "Other",
                ExpenseDate = dateExpense.Value.Date
            };

            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            string sql = @"INSERT INTO Expenses (TripID, Description, Amount, Category, ExpenseDate)
                           VALUES (@tid, @desc, @amt, @cat, @date)";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@tid", expense.TripID);
            cmd.Parameters.AddWithValue("@desc", expense.Description);
            cmd.Parameters.AddWithValue("@amt", expense.Amount);
            cmd.Parameters.AddWithValue("@cat", expense.Category);
            cmd.Parameters.AddWithValue("@date", expense.ExpenseDate.ToString("yyyy-MM-dd"));
            cmd.ExecuteNonQuery();

            txtDescription.Clear();
            txtAmount.Clear();
            LoadExpenses();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error adding expense: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDeleteExpense_Click(object sender, EventArgs e)
    {
        if (dgvExpenses.CurrentRow == null)
        {
            MessageBox.Show("Please select an expense to delete.", "Selection Required",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var result = MessageBox.Show("Delete this expense?", "Confirm Delete",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes) return;

        try
        {
            int expenseId = Convert.ToInt32(dgvExpenses.CurrentRow.Cells["ExpenseID"].Value);
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            using var cmd = new SQLiteCommand("DELETE FROM Expenses WHERE ExpenseID = @id", conn);
            cmd.Parameters.AddWithValue("@id", expenseId);
            cmd.ExecuteNonQuery();
            LoadExpenses();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error deleting expense: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnClose_Click(object sender, EventArgs e) => Close();
}
