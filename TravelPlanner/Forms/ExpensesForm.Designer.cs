namespace TravelPlanner.Forms;

partial class ExpensesForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTripTitle;
    private Label lblBudgetTracker;
    private DataGridView dgvExpenses;
    private Label lblDescription;
    private Label lblAmount;
    private Label lblCategory;
    private Label lblDate;
    private TextBox txtDescription;
    private TextBox txtAmount;
    private ComboBox cboCategory;
    private DateTimePicker dateExpense;
    private Button btnAddExpense;
    private Button btnDeleteExpense;
    private Button btnClose;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTripTitle = new Label();
        lblBudgetTracker = new Label();
        dgvExpenses = new DataGridView();
        lblDescription = new Label();
        lblAmount = new Label();
        lblCategory = new Label();
        lblDate = new Label();
        txtDescription = new TextBox();
        txtAmount = new TextBox();
        cboCategory = new ComboBox();
        dateExpense = new DateTimePicker();
        btnAddExpense = new Button();
        btnDeleteExpense = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
        SuspendLayout();

        lblTripTitle.AutoSize = true;
        lblTripTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTripTitle.Location = new Point(15, 15);
        lblTripTitle.Text = "Expenses";

        lblBudgetTracker.AutoSize = true;
        lblBudgetTracker.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblBudgetTracker.Location = new Point(15, 45);
        lblBudgetTracker.Text = "Budget tracker";

        dgvExpenses.AllowUserToAddRows = false;
        dgvExpenses.AllowUserToDeleteRows = false;
        dgvExpenses.ReadOnly = true;
        dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvExpenses.MultiSelect = false;
        dgvExpenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvExpenses.Location = new Point(15, 75);
        dgvExpenses.Size = new Size(560, 220);

        lblDescription.AutoSize = true;
        lblDescription.Location = new Point(15, 310);
        lblDescription.Text = "Description:";

        txtDescription.Location = new Point(100, 307);
        txtDescription.Size = new Size(200, 23);

        lblAmount.AutoSize = true;
        lblAmount.Location = new Point(320, 310);
        lblAmount.Text = "Amount:";

        txtAmount.Location = new Point(380, 307);
        txtAmount.Size = new Size(80, 23);

        lblCategory.AutoSize = true;
        lblCategory.Location = new Point(15, 345);
        lblCategory.Text = "Category:";

        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.Location = new Point(100, 342);
        cboCategory.Size = new Size(120, 23);

        lblDate.AutoSize = true;
        lblDate.Location = new Point(240, 345);
        lblDate.Text = "Date:";

        dateExpense.Location = new Point(280, 342);
        dateExpense.Size = new Size(180, 23);

        btnAddExpense.Location = new Point(100, 380);
        btnAddExpense.Size = new Size(110, 32);
        btnAddExpense.Text = "Add Expense";
        btnAddExpense.Click += btnAddExpense_Click;

        btnDeleteExpense.Location = new Point(220, 380);
        btnDeleteExpense.Size = new Size(110, 32);
        btnDeleteExpense.Text = "Delete";
        btnDeleteExpense.Click += btnDeleteExpense_Click;

        btnClose.Location = new Point(475, 380);
        btnClose.Size = new Size(100, 32);
        btnClose.Text = "Close";
        btnClose.Click += btnClose_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(590, 430);
        Controls.Add(lblTripTitle);
        Controls.Add(lblBudgetTracker);
        Controls.Add(dgvExpenses);
        Controls.Add(lblDescription);
        Controls.Add(txtDescription);
        Controls.Add(lblAmount);
        Controls.Add(txtAmount);
        Controls.Add(lblCategory);
        Controls.Add(cboCategory);
        Controls.Add(lblDate);
        Controls.Add(dateExpense);
        Controls.Add(btnAddExpense);
        Controls.Add(btnDeleteExpense);
        Controls.Add(btnClose);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Trip Expenses";
        ((System.ComponentModel.ISupportInitialize)dgvExpenses).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
