namespace TravelPlanner.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblUsername;
    private Label lblPassword;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnExit;
    private CheckBox chkShowPassword;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitle = new Label();
        lblUsername = new Label();
        lblPassword = new Label();
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        btnExit = new Button();
        chkShowPassword = new CheckBox();
        SuspendLayout();

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(90, 25);
        lblTitle.Text = "Travel Planner Login";

        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(50, 85);
        lblUsername.Text = "Username:";

        txtUsername.Location = new Point(140, 82);
        txtUsername.Size = new Size(220, 23);

        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(50, 125);
        lblPassword.Text = "Password:";

        txtPassword.Location = new Point(140, 122);
        txtPassword.Size = new Size(220, 23);
        txtPassword.UseSystemPasswordChar = true;

        chkShowPassword.AutoSize = true;
        chkShowPassword.Location = new Point(140, 152);
        chkShowPassword.Text = "Show password";
        chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;

        btnLogin.Location = new Point(140, 190);
        btnLogin.Size = new Size(100, 32);
        btnLogin.Text = "Login";
        btnLogin.Click += btnLogin_Click;

        btnExit.Location = new Point(260, 190);
        btnExit.Size = new Size(100, 32);
        btnExit.Text = "Exit";
        btnExit.Click += btnExit_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(420, 260);
        Controls.Add(lblTitle);
        Controls.Add(lblUsername);
        Controls.Add(txtUsername);
        Controls.Add(lblPassword);
        Controls.Add(txtPassword);
        Controls.Add(chkShowPassword);
        Controls.Add(btnLogin);
        Controls.Add(btnExit);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Login";
        ResumeLayout(false);
        PerformLayout();
    }
}
