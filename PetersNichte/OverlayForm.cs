using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class OverlayForm : Form
{
    private IContainer components;
    private IContainer checkBoxesContainer;
    public Label NamensLabel;

    public OverlayForm()
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Color.DarkGreen;  // Platzhalterfarbe für Transparenz
        this.TransparencyKey = Color.DarkGreen; // Macht die BackColor unsichtbar
        //this.TopMost = true;
        this.ShowInTaskbar = false;
        this.WindowState = FormWindowState.Maximized;

        // Klicks durchlassen
        SetClickThrough();
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        int screenWidth = Screen.PrimaryScreen.Bounds.Width-500;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        // Initialisiere Komponenten
        this.components = new Container();
        this.checkBoxesContainer = new Container();

        // Fenster konfigurieren
        this.Text = "Windows Forms Oberfläche mit Container";
        this.Width = 600;
        this.Height = 600;        

        NamensLabel = new Label
        {
            ForeColor = Color.Green,
            Text = "Peters Nichte",
            Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
            Location = new System.Drawing.Point(screenWidth, 20),
            AutoSize = true,
        };
        this.components.Add(NamensLabel);
        this.Controls.Add(NamensLabel);
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x80000; // WS_EX_LAYERED (Erlaubt Transparenz)
            cp.ExStyle |= 0x20;    // WS_EX_TRANSPARENT (Macht Klicks durchlässig)
            return cp;
        }
    }

    private void SetClickThrough()
    {
        int exStyle = WinAPI.GetWindowLong(this.Handle, WinAPI.GWL_EXSTYLE);
        WinAPI.SetWindowLong(this.Handle, WinAPI.GWL_EXSTYLE, exStyle | WinAPI.WS_EX_TRANSPARENT);
    }
}

internal static class WinAPI
{
    public const int GWL_EXSTYLE = -20;
    public const int WS_EX_LAYERED = 0x80000;
    public const int WS_EX_TRANSPARENT = 0x20;

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
}