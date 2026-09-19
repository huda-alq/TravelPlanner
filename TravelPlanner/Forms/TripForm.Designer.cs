namespace TravelPlanner.Forms;

partial class TripForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblDestination;
    private Label lblStart;
    private Label lblEnd;
    private Label lblBudget;
    private Label lblStatus;
    private Label lblNotes;
    private Label lblImage;
    private TextBox txtDestination;
    private DateTimePicker dateStart;
    private DateTimePicker dateEnd;
    private TextBox txtBudget;
    private ComboBox cboStatus;
    private TextBox txtNotes;
    private PictureBox pictureBoxTrip;
    private Button btnUploadImage;
    private Label lblImagePath;
    private Button btnSave;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblDestination = new Label();
        lblStart = new Label();
        lblEnd = new Label();
        lblBudget = new Label();
        lblStatus = new Label();
        lblNotes = new Label();
        lblImage = new Label();
        txtDestination = new TextBox();
        dateStart = new DateTimePicker();
        dateEnd = new DateTimePicker();
        txtBudget = new TextBox();
        cboStatus = new ComboBox();
        txtNotes = new TextBox();
        pictureBoxTrip = new PictureBox();
        btnUploadImage = new Button();
        lblImagePath = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBoxTrip).BeginInit();
        SuspendLayout();

        lblDestination.AutoSize = true;
        lblDestination.Location = new Point(20, 20);
        lblDestination.Text = "Destination:";

        txtDestination.Location = new Point(120, 17);
        txtDestination.Size = new Size(280, 23);

        lblStart.AutoSize = true;
        lblStart.Location = new Point(20, 55);
        lblStart.Text = "Start Date:";

        dateStart.Location = new Point(120, 52);
        dateStart.Size = new Size(200, 23);

        lblEnd.AutoSize = true;
        lblEnd.Location = new Point(20, 90);
        lblEnd.Text = "End Date:";

        dateEnd.Location = new Point(120, 87);
        dateEnd.Size = new Size(200, 23);

        lblBudget.AutoSize = true;
        lblBudget.Location = new Point(20, 125);
        lblBudget.Text = "Budget ($):";

        txtBudget.Location = new Point(120, 122);
        txtBudget.Size = new Size(120, 23);

        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(20, 160);
        lblStatus.Text = "Status:";

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Location = new Point(120, 157);
        cboStatus.Size = new Size(150, 23);

        lblNotes.AutoSize = true;
        lblNotes.Location = new Point(20, 195);
        lblNotes.Text = "Notes:";

        txtNotes.Location = new Point(120, 192);
        txtNotes.Multiline = true;
        txtNotes.Size = new Size(280, 60);
        txtNotes.ScrollBars = ScrollBars.Vertical;

        lblImage.AutoSize = true;
        lblImage.Location = new Point(430, 20);
        lblImage.Text = "Destination Photo:";

        pictureBoxTrip.BorderStyle = BorderStyle.FixedSingle;
        pictureBoxTrip.Location = new Point(430, 45);
        pictureBoxTrip.Size = new Size(200, 150);

        btnUploadImage.Location = new Point(430, 205);
        btnUploadImage.Size = new Size(120, 28);
        btnUploadImage.Text = "Upload Image";
        btnUploadImage.Click += btnUploadImage_Click;

        lblImagePath.AutoSize = true;
        lblImagePath.ForeColor = Color.Gray;
        lblImagePath.Location = new Point(430, 240);
        lblImagePath.MaximumSize = new Size(200, 0);
        lblImagePath.Text = "";

        btnSave.Location = new Point(120, 270);
        btnSave.Size = new Size(100, 32);
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;

        btnCancel.Location = new Point(230, 270);
        btnCancel.Size = new Size(100, 32);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(660, 320);
        Controls.Add(lblDestination);
        Controls.Add(txtDestination);
        Controls.Add(lblStart);
        Controls.Add(dateStart);
        Controls.Add(lblEnd);
        Controls.Add(dateEnd);
        Controls.Add(lblBudget);
        Controls.Add(txtBudget);
        Controls.Add(lblStatus);
        Controls.Add(cboStatus);
        Controls.Add(lblNotes);
        Controls.Add(txtNotes);
        Controls.Add(lblImage);
        Controls.Add(pictureBoxTrip);
        Controls.Add(btnUploadImage);
        Controls.Add(lblImagePath);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Trip";
        ((System.ComponentModel.ISupportInitialize)pictureBoxTrip).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
