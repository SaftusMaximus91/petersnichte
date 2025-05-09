using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp1;

partial class MainForm
{
    private Timer shutdownTimer;
    private IContainer components;
    private IContainer checkBoxesContainer;
    private ComboBox auswahlComboBox;
    private FlowLayoutPanel checkBoxPanel;
    private Label countdownLabel;
    private int remainingTime;
    private LampControl lamp;
 
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        if (disposing && (checkBoxesContainer != null))
        {
            checkBoxesContainer.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
           private void InitializeComponent()
        {
            // Initialisiere Komponenten
            this.components = new Container();
            this.checkBoxesContainer = new Container();

            // Fenster konfigurieren
            this.Text = "Windows Forms Oberfläche mit Container";
            this.Width = 600;
            this.Height = 600;
            
       
            
            var namensLabel = new Label
            {
                Text = "Peters Nichte",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(180, 20),
                AutoSize = true,
                
            };
            this.components.Add(namensLabel);
            this.Controls.Add(namensLabel);

            // Starte den Timer
            // Sektion 1: Spielfeld
            var spielfeldLabel = new Label
            {
                Text = "Spielfeld",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            this.components.Add(spielfeldLabel);
            this.Controls.Add(spielfeldLabel);

            // Koordinaten
            var x1Label = new Label { Text = "X1:", Location = new System.Drawing.Point(20, 50), AutoSize = true  };
            x1TextBox = new TextBox { Location = new System.Drawing.Point(60, 50), Width = 50 };
            var y1Label = new Label { Text = "Y1:", Location = new System.Drawing.Point(120, 50), AutoSize = true };
            y1TextBox = new TextBox { Location = new System.Drawing.Point(160, 50), Width = 50 };
            var x2Label = new Label { Text = "X2:", Location = new System.Drawing.Point(20, 80), AutoSize = true };
            x2TextBox = new TextBox { Location = new System.Drawing.Point(60, 80), Width = 50 };
            var y2Label = new Label { Text = "Y2:", Location = new System.Drawing.Point(120, 80), AutoSize = true };
            y2TextBox = new TextBox { Location = new System.Drawing.Point(160, 80), Width = 50 };
            
            
            var speedFactorLabel = new Label { Text = "Speed:", Location = new System.Drawing.Point(220, 50), AutoSize = true };
            speedFactorTB = new TextBox { Location = new System.Drawing.Point(260, 50), Width = 50 };
            speedFactorTB.Text = "1,0";

            // Hinzufügen der Steuerelemente zu components und Controls
            this.components.Add(x1Label);
            this.Controls.Add(x1Label);
            this.components.Add(x1TextBox);
            this.Controls.Add(x1TextBox);
            this.components.Add(y1Label);
            this.Controls.Add(y1Label);
            this.components.Add(y1TextBox);
            this.Controls.Add(y1TextBox);
            this.components.Add(x2Label);
            this.Controls.Add(x2Label);
            this.components.Add(x2TextBox);
            this.Controls.Add(x2TextBox);
            this.components.Add(y2Label);
            this.Controls.Add(y2Label);
            this.components.Add(y2TextBox);
            this.Controls.Add(y2TextBox);
            this.components.Add(speedFactorTB);
            this.Controls.Add(speedFactorTB);
            this.components.Add(speedFactorLabel);
            this.Controls.Add(speedFactorLabel);

            // Sektion 2: Auswahl
            var auswahlLabel = new Label
            {
                Text = "Auswahl",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(20, 120),
                AutoSize = true
            };
            this.components.Add(auswahlLabel);
            this.Controls.Add(auswahlLabel);

            auswahlComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(20, 150),
                Width = 200
            };
            auswahlComboBox.Items.AddRange(new string[] { "Whiteout Survival", "Lust Goddess", });
            auswahlComboBox.SelectedIndexChanged += ChoseGame_SelectedIndexChanged;
            this.components.Add(auswahlComboBox);
            this.Controls.Add(auswahlComboBox);

            // Sektion für dynamische Checkboxen
            checkBoxPanel = new FlowLayoutPanel
            {
                Location = new System.Drawing.Point(20, 190),
                Width = 400,
                Height = 100,
                AutoScroll = true,
                Visible = false
            };
            this.components.Add(checkBoxPanel);
            this.Controls.Add(checkBoxPanel);

            // Sektion 3: Buttons
            var button1 = new Button { Text = "Start", Location = new System.Drawing.Point(20, 350), Width = 100 };
            var button2 = new Button { Text = "Reset OnHold", Location = new System.Drawing.Point(130, 350), Width = 100 };
            var button3 = new Button { Text = "Screenshot (F7)", Location = new System.Drawing.Point(240, 350), Width = 100 };
            var button4 = new Button { Text = "Spielfeld  (F6)", Location = new System.Drawing.Point(350, 350), Width = 100 };
            var button6 = new Button { Text = "Stop BS", Location = new System.Drawing.Point(460, 350), Width = 100 };
            var button5 = new Button { Text = "Idle-Timer", Location = new System.Drawing.Point(50, 400), Width = 100 };
            lamp = new LampControl
            {
                Location = new Point(20, 400) // Position der Lampe im Fenster
            };
            this.Controls.Add(lamp);
            
            countdownLabel = new Label
            {
                Text = "Shutdown in 30:00 Minuten",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(155, 400)
            };
            this.Controls.Add(countdownLabel);
            shutdownTimer = new Timer
            {
                Interval = 1000 // 1 Sekunde
            };
            shutdownTimer.Tick += ShutdownTimer_Tick;

            remainingTime = 30 * 60;
            
            button6.Click += stoppEmulator;
            button5.Click += IdleTimer;
            button4.Click += TakeScreenCords;
            button3.Click += ActivateScreenshotMode; 
            button2.Click += ResetOnHoldState;
            button1.Click += ToggleLoopState;
            
            this.components.Add(button1);
            this.Controls.Add(button1);
            this.components.Add(button2);
            this.Controls.Add(button2);
            this.components.Add(button3);
            this.Controls.Add(button3);
            this.components.Add(button4);
            this.Controls.Add(button4);
            this.components.Add(button5);
            this.Controls.Add(button5);
            this.components.Add(button6);
            this.Controls.Add(button6);
            
            var mouseStartRecording = new Button { Text = "Record start", Location = new System.Drawing.Point(20, 300), Width = 100 };
            mouseStartRecording.Click += btnStartRecording_Click;
            this.Controls.Add(mouseStartRecording);
            var mouseStopRecording = new Button { Text = "Record stop", Location = new System.Drawing.Point(130, 300), Width = 100 };
            mouseStopRecording.Click += btnStopRecording_Click;
            this.Controls.Add(mouseStopRecording);
            var btnSaveToCsv = new Button { Text = "Record save", Location = new System.Drawing.Point(240, 300), Width = 100 };
            btnSaveToCsv.Click += btnSaveToCsv_Click;
            this.Controls.Add(btnSaveToCsv);
            var btnLoadFromCsv = new Button { Text = "Record load", Location = new System.Drawing.Point(350, 300), Width = 100 };
            btnLoadFromCsv.Click += btnLoadFromCsv_Click;
            this.Controls.Add(btnLoadFromCsv);
            var replayMouse = new Button { Text = "Replay Mouse", Location = new System.Drawing.Point(460, 300), Width = 100 };
            replayMouse.Click += btnReplay_Click;
            this.Controls.Add(replayMouse);
            
            auswahlComboBox.SelectedIndex = 1;
        }

    #endregion
}
