namespace TravelPlanner.Forms;

partial class DashboardForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblWelcome;
    private Label lblSummary;
    private DataGridView dgvTrips;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnExpenses;
    private Button btnRefresh;
    private Button btnLogout;
    private CheckBox chkHideCompleted;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblWelcome = new Label();
        lblSummary = new Label();
        dgvTrips = new DataGridView();
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        btnExpenses = new Button();
        btnRefresh = new Button();
        btnLogout = new Button();
        chkHideCompleted = new CheckBox();
        ((System.ComponentModel.ISupportInitialize)dgvTrips).BeginInit();
        SuspendLayout();

        lblWelcome.AutoSize = true;
        lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblWelcome.Location = new Point(15, 15);
        lblWelcome.Text = "Welcome!";

        lblSummary.AutoSize = true;
        lblSummary.Location = new Point(15, 45);
        lblSummary.Text = "Trips: 0";

        chkHideCompleted.AutoSize = true;
        chkHideCompleted.Location = new Point(15, 70);
        chkHideCompleted.Text = "Hide completed trips";
        chkHideCompleted.CheckedChanged += chkHideCompleted_CheckedChanged;

        dgvTrips.AllowUserToAddRows = false;
        dgvTrips.AllowUserToDeleteRows = false;
        dgvTrips.ReadOnly = true;
        dgvTrips.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTrips.MultiSelect = false;
        dgvTrips.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvTrips.Location = new Point(15, 100);
        dgvTrips.Size = new Size(760, 320);

        btnAdd.Location = new Point(15, 435);
        btnAdd.Size = new Size(100, 32);
        btnAdd.Text = "Add Trip";
        btnAdd.Click += btnAdd_Click;

        btnEdit.Location = new Point(125, 435);
        btnEdit.Size = new Size(100, 32);
        btnEdit.Text = "Edit Trip";
        btnEdit.Click += btnEdit_Click;

        btnDelete.Location = new Point(235, 435);
        btnDelete.Size = new Size(100, 32);
        btnDelete.Text = "Delete";
        btnDelete.Click += btnDelete_Click;

        btnExpenses.Location = new Point(345, 435);
        btnExpenses.Size = new Size(120, 32);
        btnExpenses.Text = "View Expenses";
        btnExpenses.Click += btnExpenses_Click;

        btnRefresh.Location = new Point(475, 435);
        btnRefresh.Size = new Size(100, 32);
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += btnRefresh_Click;

        btnLogout.Location = new Point(675, 435);
        btnLogout.Size = new Size(100, 32);
        btnLogout.Text = "Logout";
        btnLogout.Click += btnLogout_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(790, 485);
        Controls.Add(lblWelcome);
        Controls.Add(lblSummary);
        Controls.Add(chkHideCompleted);
        Controls.Add(dgvTrips);
        Controls.Add(btnAdd);
        Controls.Add(btnEdit);
        Controls.Add(btnDelete);
        Controls.Add(btnExpenses);
        Controls.Add(btnRefresh);
        Controls.Add(btnLogout);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Travel Planner – Dashboard";
        ((System.ComponentModel.ISupportInitialize)dgvTrips).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
