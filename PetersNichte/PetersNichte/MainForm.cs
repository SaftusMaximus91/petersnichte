using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp1;

public partial class MainForm : Form
{
    private const int WM_HOTKEY = 0x0312;
    private const int HOTKEY_IDF7 = 1;
    private const int HOTKEY_IDF6 = 2;
    private const int HOTKEY_NumPad1 = 3;
    private const int HOTKEY_NumPad2 = 4;
    private const int HOTKEY_NumPad3 = 5;
    private const int HOTKEY_NumPad4 = 6;
    private const int HOTKEY_NumPad5 = 7;
    private const int HOTKEY_NumPad6 = 8;

    private const byte VK_F11 = 0x7A; // Virtueller Tastencode für F11
    private const uint KEYEVENTF_KEYDOWN = 0x0000;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    private const int SW_RESTORE = 9;

    private readonly string mousecsv = @"C:\Users\schmi\Pictures\AutoClicker\Mouse\";

    private readonly MouseHook mouseHook = new();
    private readonly MousePlayer mousePlayer = new();

    private readonly string whiteoutSurvivalName = "Whiteout Survival";
    private bool isLoopRunning;
    private JobConfig jobConfig;
    private List<ImageFinder.Job> jobs;
    private Task loopTask;

    private bool shutdownTimerEnabled;
    private int stopLoopRemainingTime;
    private Timer stopLoopTimer;

    private int? x1;

    private TextBox x1TextBox;
    private int? x2;
    private TextBox x2TextBox;
    private int? y1;
    private TextBox y1TextBox;
    private int? y2;
    private TextBox y2TextBox;
    public TextBox speedFactorTB;
    private OverlayForm overlay;


    public MainForm()
    {
        jobConfig = GetDefaultJobConfig();
        mouseHook = new MouseHook();
        mousePlayer = new MousePlayer();
        InitializeComponent();
     }

    

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    protected override void WndProc(ref Message m)
    {
        var sleepDuration = 450;
        var sleepDuration2 = 300;
        var mousePosition = Cursor.Position;

        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_NumPad1)
            SendTroopInBearRally(mousePosition, sleepDuration, sleepDuration2, 52, 106);
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_NumPad2)
            SendTroopInBearRally(mousePosition, sleepDuration, sleepDuration2, 115, 106);
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_NumPad3)
            SendTroopInBearRally(mousePosition, sleepDuration, sleepDuration2, 186, 106);
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_NumPad4)
            SendTroopInBearRally(mousePosition, sleepDuration, sleepDuration2, 242, 106);
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_NumPad5)
            SendTroopInBearRally(mousePosition, sleepDuration, sleepDuration2, 299, 106);
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_NumPad6)
            //   SendTroopInBearRally(mousePosition, sleepDuration, sleepDuration2, 368, 106);
            DoSoeldner(mousePosition, sleepDuration, sleepDuration2, 1113, 898);


        // Handle hotkey presses
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_IDF6) SavePlayground(false);

        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_IDF7) SavePoint(true);

        base.WndProc(ref m);
    }

    private static void DoSoeldner(Point mousePosition, int sleepDuration, int sleepDuration2, int x, int y)
    {
        //TODO HEAL ERWEITERN, TRUPPEN ROTA AUSWÄHLEN
        var i = 0;
        while (i < 10)
        {
            ImageFinder.SimulateClick(mousePosition.X, mousePosition.Y);
            Thread.Sleep(1500);//600
            ImageFinder.SimulateClick(x, y);
            Thread.Sleep(3000);//1200
            ImageFinder.SimulateClick(x, y);
            Thread.Sleep(sleepDuration);
            //     ImageFinder.SimulateClick(1020, 100);
            Thread.Sleep(2000);//600
            ImageFinder.SimulateClick(1140, 1020);
            Thread.Sleep(2000);//600
            ImageFinder.SetCursorPos(mousePosition.X, mousePosition.Y);
            Thread.Sleep(35000);//35
            i++;
        }
    }

    private void SendTroopInBearRally(Point mousePosition, int sleepDuration, int sleepDuration2, int x, int y)
    {
        ImageFinder.SimulateClick(mousePosition.X, mousePosition.Y);
        Thread.Sleep(sleepDuration);
        ImageFinder.SimulateClick(506 + jobConfig.ScreenDefaultStart.X, 434 + jobConfig.ScreenDefaultStart.Y);
        Thread.Sleep(sleepDuration2);
        ImageFinder.SimulateClick(x + jobConfig.ScreenDefaultStart.X,
            y + jobConfig.ScreenDefaultStart.Y); //Trupp Koords
        Thread.Sleep(sleepDuration2);
        ImageFinder.SimulateClick(474 + jobConfig.ScreenDefaultStart.X, 1020 + jobConfig.ScreenDefaultStart.Y);
        Thread.Sleep(sleepDuration2);
        ImageFinder.SimulateClick(30 + jobConfig.ScreenDefaultStart.X, 15 + jobConfig.ScreenDefaultStart.Y);
        Thread.Sleep(sleepDuration);
        ImageFinder.SetCursorPos(mousePosition.X, mousePosition.Y);
    }


    private void btnStartRecording_Click(object sender, EventArgs e)
    {
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        mouseHook.XOffSet = x1;
        mouseHook.YOffSet = y1;
        mouseHook.Start();
    }

    private void btnStopRecording_Click(object sender, EventArgs e)
    {
        mouseHook.Stop();
    }

    private void btnSaveToCsv_Click(object sender, EventArgs e)
    {
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        mouseHook.XOffSet = x1;
        mouseHook.YOffSet = y1;
        mouseHook.SaveToCsv(mousecsv + "mausaktionen.csv");
    }

    private void btnLoadFromCsv_Click(object sender, EventArgs e)
    {
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        mouseHook.XOffSet = x1;
        mouseHook.YOffSet = y1;
        mouseHook.LoadFromCsv(mousecsv + "mausaktionen.csv");
    }

    private void btnReplay_Click(object sender, EventArgs e)
    {
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        double.TryParse(speedFactorTB.Text, out var speedFactor);
        mouseHook.XOffSet = x1;
        mouseHook.YOffSet = y1;
        
        
        mousePlayer.Play(mouseHook.MouseEvents,speedFactor);
        
        
       // MessageBox.Show("Wiedergabe abgeschlossen.");
    }

    private static LustGoddess GetDefaultJobConfig()
    {
        return new LustGoddess();
    }

    private void StopLoopTimer_Tick(object sender, EventArgs e)
    {
        if (stopLoopRemainingTime > 0)
        {
            stopLoopRemainingTime--;
        }
        else
        {
            stopLoopTimer.Stop();
            StopLoop();
        }
    }

    private void ShutdownTimer_Tick(object sender, EventArgs e)
    {
        if (remainingTime > 0)
        {
            remainingTime--;

            // Aktualisiere die Countdown-Anzeige
            var timeSpan = TimeSpan.FromSeconds(remainingTime);
            countdownLabel.Text = $"Shutdown in {timeSpan:mm\\:ss} Minuten";
        }
        else
        {
            shutdownTimer.Stop();
            ExecuteShutdown();
        }
    }

    private void ExecuteShutdown()
    {
        try
        {
            TakeScreenshotFromGame();
            // Befehl zum Herunterfahren des Computers
            Process.Start("shutdown", "/s /t 0");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Herunterfahren: {ex.Message}", "Fehler");
        }
    }


    private void ChoseGame_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (auswahlComboBox.SelectedItem?.ToString() == whiteoutSurvivalName)
        {
            jobConfig = new WhiteoutSurvival();
            SetupWhiteOutElements();
        }
        else if (auswahlComboBox.SelectedItem?.ToString() == "Lust Goddess")
        {
            jobConfig = new LustGoddess();
            SetupLustGoddessElements();
        }

        checkBoxPanel.Visible = jobConfig.hasOptions;
        Text = jobConfig.Name;
        SetScreenCords();
    }

    private void SavePlayground(bool saveImage)
    {
        var mousePosition = Cursor.Position;
        var savedPoint = mousePosition;
        if (noCordsSet())
        {
            x1TextBox.Text = savedPoint.X.ToString();
            y1TextBox.Text = savedPoint.Y.ToString();
        }
        else if (firstCordisSet())
        {
            x2TextBox.Text = savedPoint.X.ToString();
            y2TextBox.Text = savedPoint.Y.ToString();

            if (saveImage) ImageFinder.TakeScreenshotAndSave(x1.Value, y1.Value, x2.Value, y2.Value);

            UnregisterHotKey(Handle, HOTKEY_IDF6);
            UnregisterHotKey(Handle, HOTKEY_IDF7);
        }
        else if (allCordFieldsSet())
        {
            x1TextBox.Text = savedPoint.X.ToString();
            y1TextBox.Text = savedPoint.Y.ToString();
            x2TextBox.Text = "";
            y2TextBox.Text = "";
        }
    }

    private void SavePoint(bool saveImage)
    {
        var mousePosition = Cursor.Position;
        var savedPoint = mousePosition;
        if (noCordsSetScreenshot())
        {
            x1 = savedPoint.X;
            y1 = savedPoint.Y;
        }
        else if (firstCordisSetScreenshot())
        {
            x2 = savedPoint.X;
            y2 = savedPoint.Y;
            if (saveImage) ImageFinder.TakeScreenshotAndSave(x1.Value, y1.Value, x2.Value, y2.Value);

            UnregisterHotKey(Handle, HOTKEY_IDF6);
            UnregisterHotKey(Handle, HOTKEY_IDF7);
        }
        else if (allCordFieldsSetScreenshot())
        {
            x2 = null;
            y2 = null;
            x1 = savedPoint.X;
            y1 = savedPoint.Y;
        }
    }

    private void TakeScreenshotFromGame()
    {
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        int.TryParse(x2TextBox.Text, out var x2);
        int.TryParse(y2TextBox.Text, out var y2);
        ImageFinder.TakeScreenshotAndSave(x1, y1, x2, y2);
    }

    private bool noCordsSetScreenshot()
    {
        return !x1.HasValue && !y1.HasValue && !y2.HasValue && !x2.HasValue;
    }

    private bool firstCordisSetScreenshot()
    {
        return x1.HasValue && y1.HasValue && !y2.HasValue && !x2.HasValue;
    }

    private bool allCordFieldsSetScreenshot()
    {
        return x1.HasValue && y1.HasValue && y2.HasValue && x2.HasValue;
    }

    private bool noCordsSet()
    {
        return x1TextBox.Text == "" && y1TextBox.Text == "" && y2TextBox.Text == "" && x2TextBox.Text == "";
    }

    private bool firstCordisSet()
    {
        return x1TextBox.Text != "" && y1TextBox.Text != "" && y2TextBox.Text == "" && x2TextBox.Text == "";
    }

    private bool allCordFieldsSet()
    {
        return x1TextBox.Text != "" && y1TextBox.Text != "" && y2TextBox.Text != "" && x2TextBox.Text != "";
    }

    private void ResetOnHoldState(object sender, EventArgs e)
    {
        ImageFinder.ResetOnHold();
    }

    // Event-Handler für die Tasteneingabe
    // Event-Handler für Button-Click
    private void ToggleLoopState(object sender, EventArgs e)
    {
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        int.TryParse(x2TextBox.Text, out var x2);
        int.TryParse(y2TextBox.Text, out var y2);

        jobConfig.ScreenDefaultStart = new Point(x1, y1);
        jobConfig.ScreenDefaultEnd = new Point(x2, y2);


        //statusLabel.Text = "Aktueller Status: " + (isLoopRunning ? "gestoppt" : "läuft");
        // MessageBox.Show("Button wurde geklickt!");
        ((Button)sender).Text = !isLoopRunning ? "Stop" : "Start";
        if (isLoopRunning)
            StopLoop(); // Wenn die Schleife läuft, stoppe sie
        else
        {
            StartLoop();
   /*         System.Threading.Tasks.Task.Run(async () =>
            {
              
            });*/
        }
             // Wenn die Schleife nicht läuft, starte sie
    }

    private static Bitmap CaptureScreenArea(Rectangle area)
    {
        // Erstelle ein Bitmap mit der Größe des angegebenen Bereichs
        var bitmap = new Bitmap(area.Width, area.Height);

        // Erstelle ein Graphics-Objekt, um den Screenshot zu zeichnen
        using (var g = Graphics.FromImage(bitmap))
        {
            // Screenshot des angegebenen Bereichs aufnehmen
            g.CopyFromScreen(area.Location, Point.Empty, area.Size);
        }

        return bitmap;
    }

    private void TakeScreenCords(object sender, EventArgs e)
    {
        UnregisterHotKey(Handle, HOTKEY_IDF6);
        RegisterHotKey(Handle, HOTKEY_IDF6, 0, (uint)Keys.F6);
    }

    private void ActivateScreenshotMode(object sender, EventArgs e)
    {
        UnregisterHotKey(Handle, HOTKEY_IDF7);
        RegisterHotKey(Handle, HOTKEY_IDF7, 0, (uint)Keys.F7);
    }

    private void StopLoop()
    {
        UnregisterHotKey(Handle, HOTKEY_NumPad1);
        UnregisterHotKey(Handle, HOTKEY_NumPad2);
        UnregisterHotKey(Handle, HOTKEY_NumPad3);
        UnregisterHotKey(Handle, HOTKEY_NumPad4);
        UnregisterHotKey(Handle, HOTKEY_NumPad5);
        UnregisterHotKey(Handle, HOTKEY_NumPad6);
        if (shutdownTimerEnabled)
            shutdownTimer.Stop();
        Console.WriteLine("StopLoop");
        isLoopRunning = false;
        loopTask?.Wait(); // Warten auf den Abschluss der aktuellen Schleifen-Task

        // Zeigt dem Benutzer an, dass die Schleife gestoppt wurde
    }


    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    private async void StartLoop()
    {
        if (shutdownTimerEnabled)
            shutdownTimer.Start();
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        int.TryParse(x2TextBox.Text, out var x2);
        int.TryParse(y2TextBox.Text, out var y2);
        var loopCounter = 0;
        var fastForward = 1;

        while (auswahlComboBox.SelectedItem?.ToString() == whiteoutSurvivalName &&
               (GetControlByName("dailyStuff") as CheckBox).Checked &&
               !(GetControlByName("littleAccounts") as CheckBox).Checked)
        {
            await RunSequenceStepForIntervallOrMissAttempts(
                WhiteoutSurvival.SequenceSteps.CloseAds,
                5,
                x1,
                y1, x2, y2, 2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.FromStartDialogToAllianzTab, 2, x1, y1, x2,
                y2);
            RunMouseMacroForDaily("1_InAllianzTabAlleToDosUndRaus");
            RunMouseMacroForDaily("2_OeffneBestienRoute");

            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.StartBeastJob, 40, x1, y1, x2, y2);
            await GetWaitJob(10, x1, y1, x2, y2);
            RunMouseMacroForDaily("3_BestienKistenundVerlassenZurStadt");
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CollectExploration, 5, x1, y1, x2, y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.FromExplorationBackToCity, 8, x1, y1, x2,
                y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.FromCityToWorldIfNot, 8, x1, y1, x2, y2);
            await RunSequenceStepForIntervallOrMissAttempts(WhiteoutSurvival.SequenceSteps.CollectIntels, 1600, x1, y1,
                x2, y2, 10);
            await GetWaitJob(5, x1, y1, x2, y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.WaitForPotentialMarching, 5, x1, y1, x2,
                y2);
            await GetWaitJob(5, x1, y1, x2, y2);
            if ((GetControlByName("doGathering") as CheckBox).Checked)
                RunMouseMacroForDaily("10_NachIntelSammeln"); //TODO Wenn keine Schleifen mehr frei? 

            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.IfStillInSearchWindowThanBackToCity, 5, x1,
                y1, x2, y2);
            await GetWaitJob(10, x1, y1, x2, y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.GoToCityFromWorld, 5, x1, y1, x2, y2);
            RunMouseMacroForDaily("45_StartAutobeitritt");

            RunMouseMacroForDaily("5_StadtZuInf");
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CollectInfAndSetupQueue, 15, x1, y1, x2,
                y2);
            RunMouseMacroForDaily("7_LancerMM");
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CollectMMAndSetupQueue, 15, x1, y1, x2,
                y2);
            RunMouseMacroForDaily("6_InfZuLancer");
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CollectLancerAndSetupQueue, 15, x1, y1, x2,
                y2);

            RunMouseMacroForDaily("8_LancerZuFCLab");
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.DoFCLabTasks, 35, x1, y1, x2, y2);
            RunMouseMacroForDaily("8.5_FCLabraus");

            RunMouseMacroForDaily("9_OpenMenuAndScrollDown");
            await GetWaitJob(5, x1, y1, x2, y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CollectIsland, 20, x1, y1, x2, y2);
            await GetWaitJob(10, x1, y1, x2, y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CheckForOnlineRewardByMenu, 15, x1, y1, x2,
                y2);
            await GetWaitJob(10, x1, y1, x2, y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CloseMenuIfOpen, 10, x1, y1, x2, y2);

            RunMouseMacroForDaily("9_OpenMenuAndScrollDown");
            await GetWaitJob(5, x1, y1, x2, y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CheckForOnlineRewardByMenu, 15, x1, y1, x2,
                y2);
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CloseMenuIfOpen, 10, x1, y1, x2, y2);

            //Loop OnlineBelohnung
            for (var i = 0; i < 2; i++) await CheckForOnlineRewardOrRecruite(x1, y1, x2, y2);

            RunMouseMacroForDaily("Change_ACC");
            await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.SwitchAccWhenInMenuDialog, 30, x1, y1, x2,
                y2);
            loopCounter = loopCounter + 1;
        }

      /*  overlay = new OverlayForm();
        overlay.Show();
        */
        if (auswahlComboBox.SelectedItem?.ToString() == whiteoutSurvivalName &&
            (GetControlByName("startDorf") as CheckBox).Checked && !(GetControlByName("shieldUp") as CheckBox).Checked)
        {
            var emus = new List<string>
            {
                "Pie64_9", //1_Spoder_Atgeir 
                "Pie64_10",//2_Mimox_Öttl
                "Pie64_11",//3_Bubi_Leoni
                "Pie64_30",//4_Vortex_Hildö
                "Pie64_18",//5_Kolala_Tritz 
                "Pie64_19",//6_HeMan_Feiar  
                "Pie64_20",//7_Kathii_Ulmer
                "Pie64_21",//8_Lima_SnowOwl
                "Pie64_28",//D_Eisboy_Infamous2k9  
                "Pie64_1", //S_Black_Katha
            };
            bool mitClicker = (GetControlByName("mitClicker") as CheckBox).Checked;
            foreach (var emulator in emus)
            {
                if (!mitClicker)
                {
                    StarteEmulatorMitApp(emulator);
                }
                else
                {
                    startEmulatorByID(emulator, false);
                    StarteEmulatorMitApp(emulator, "com.truedevelopersstudio.automatictap.autoclicker");
                    Thread.Sleep(3000);
                    keybd_event(VK_F11, 0, KEYEVENTF_KEYDOWN, 0);
                    keybd_event(VK_F11, 0, KEYEVENTF_KEYUP, 0);
                    Thread.Sleep(3000);
                    await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.StartAutoClicker, 5, x1, y1, x2,
                        y2);
                    StarteEmulatorMitApp(emulator);
                    Thread.Sleep(2000);
                    keybd_event(VK_F11, 0, KEYEVENTF_KEYDOWN, 0);
                }
            }

            StopLoop();
            return;
        }

        if (auswahlComboBox.SelectedItem?.ToString() == whiteoutSurvivalName &&
            (GetControlByName("startDorf") as CheckBox).Checked && (GetControlByName("shieldUp") as CheckBox).Checked)
        {
            var emus = new List<string>
            {
                "Pie64_9", //1_Spoder_Atgeir 
                "Pie64_10",//2_Mimox_Öttl
                "Pie64_11",//3_Bubi_Leoni
                "Pie64_30",//4_Vortex_Hildö
                "Pie64_18",//5_Kolala_Tritz 
                "Pie64_19",//6_HeMan_Feiar  
                "Pie64_20",//7_Kathii_Ulmer
                "Pie64_21",//8_Lima_SnowOwl
                "Pie64_28",//D_Eisboy_Infamous2k9  
                "Pie64_1", //S_Black_Katha
            };
            foreach (var emulator in emus)
            {
                startEmulatorByID(emulator,true,0);
                for (var i = 0; i <= 1; i++)
                {
                    await RunSequenceStepForIntervallOrMissAttempts(
                        WhiteoutSurvival.SequenceSteps.CloseAds,
                        5,
                        x1,
                        y1, x2, y2, 2);
                    await GetWaitJob(5, x1, y1, x2, y2);
                    RunMouseMacroForDaily("100_shieldUp8Hours");

                    if (i == 0)
                    {
                        RunMouseMacroForDaily("Change_ACC");
                        await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.SwitchAccWhenInMenuDialog,
                            20,
                            x1, y1, x2, y2);
                    }
                }
            }

            StopLoop();
        }

        if (auswahlComboBox.SelectedItem?.ToString() == whiteoutSurvivalName &&
            (GetControlByName("dailyStuff") as CheckBox).Checked &&
            (GetControlByName("littleAccounts") as CheckBox).Checked)
        {
            var emus = new List<string>
            {
                
                "Pie64_30",//4_Vortex_Hildö
                "Pie64_18",//5_Kolala_Tritz 
                "Pie64_19",//6_HeMan_Feiar  
                "Pie64_20",//7_Kathii_Ulmer
                "Pie64_21",//8_Lima_SnowOwl
                "Pie64_28",//D_Eisboy_Infamous2k9  
                "Pie64_1", //S_Black_Katha
                "Pie64_9", //1_Spoder_Atgeir 
                "Pie64_10",//2_Mimox_Öttl
                "Pie64_11",//3_Bubi_Leoni
            };

            var a = DateTime.Now < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0, 0);

            bool openDailyMission = (GetControlByName("doMails") as CheckBox).Checked;
            bool collectExploration = (GetControlByName("doErkundung") as CheckBox).Checked;
            bool collectTroops = (GetControlByName("collectTroops") as CheckBox).Checked;
            bool collectOnlineRewardAndStamm = (GetControlByName("collectStam") as CheckBox).Checked;
            bool collectRecrut = (GetControlByName("collectRecrute") as CheckBox).Checked;
            bool sendGathering = (GetControlByName("doGathering") as CheckBox).Checked;
            bool doIntels = (GetControlByName("doIntels") as CheckBox).Checked;
            bool doAlliTab = (GetControlByName("doAllianzTab") as CheckBox).Checked;
            bool doBeasts = (GetControlByName("polarTerrorHunt") as CheckBox).Checked;
            bool alliHelp = (GetControlByName("alliHelp") as CheckBox).Checked;
            bool alliResearch = (GetControlByName("alliResearch") as CheckBox).Checked;
            bool alliAutoJoin = (GetControlByName("alliAutoJoin") as CheckBox).Checked;
            bool alliChest = (GetControlByName("alliChest") as CheckBox).Checked;
            bool doArena = (GetControlByName("doArena") as CheckBox).Checked;
            bool doBaum = (GetControlByName("doBaum") as CheckBox).Checked;
            bool activatePetBuffs = (GetControlByName("activatePetBuffs") as CheckBox).Checked;
            bool sammelnZurueck = (GetControlByName("stopGathering") as CheckBox).Checked;
            bool heilen = (GetControlByName("heal") as CheckBox).Checked;
            bool script = (GetControlByName("script") as CheckBox).Checked;
            bool forschen = (GetControlByName("forschen") as CheckBox).Checked;
            bool startEmulator = (GetControlByName("startEmulator") as CheckBox).Checked;
            bool doMine = (GetControlByName("doMine") as CheckBox).Checked;

/*
            foreach (var emulator in emus)
            {
                 var startCode = startEmulatorByID(emulator, true, 0);
                      if (startCode == 99)
                          continue;
                for (var i = 0; i <= 1; i++)
                {
                    await RunSequenceStepForIntervall(
                        WhiteoutSurvival.SequenceSteps.GoToCityFromWorld, 5,
                        x1, y1, x2, y2);
                    await RunSequenceStepForIntervallOrMissAttempts(
                        WhiteoutSurvival.SequenceSteps.CloseAds,
                        15,
                        x1,
                        y1, x2, y2, 2);
                    await GetWaitJob(5, x1, y1, x2, y2);
                    //RunMouseMacroForDaily("Mausaktionen");
                    RunMouseMacroForDaily("RallyStarten_2x3");
                    if (i == 0)
                    {
                        RunMouseMacroForDaily("Change_ACC");
                        await RunSequenceStepForIntervall(
                            WhiteoutSurvival.SequenceSteps.SwitchAccWhenInMenuDialog,
                            20,
                            x1, y1, x2, y2);
                    }
                }
            }soeldso
            
            */


          
              while (true)
              {
                  foreach (var emulator in emus)
                  {
                      if (startEmulator)
                      {
                          var startCode = startEmulatorByID(emulator, true, 0);
                          if (startCode == 99)
                              continue;
                      }
                      for (var i = 0; i <= 1; i++)
                      {
                          {
                              {
                                  await RunSequenceStepForIntervallOrMissAttempts(
                                      WhiteoutSurvival.SequenceSteps.CloseAds,
                                      5,
                                      x1,
                                      y1, x2, y2, 2);
                                  await GetWaitJob(5, x1, y1, x2, y2);
                                  
                                  {
                                      if (doAlliTab)
                                      {
                                          await RunSequenceStepForIntervallOrMissAttempts(
                                              WhiteoutSurvival.SequenceSteps.FromStartDialogToAllianzTab,
                                              2,
                                              x1,
                                              y1, x2, y2, 2);

                                          if (alliAutoJoin)
                                              RunMouseMacroForDaily("ActivateAutoJoin");

                                          if (alliChest)
                                              RunMouseMacroForDaily("AlliTabChest");

                                          if (alliHelp)
                                              RunMouseMacroForDaily("AlliHelp");

                                          if (alliResearch)
                                          {
                                              await RunSequenceStepForIntervallOrMissAttempts(
                                                  WhiteoutSurvival.SequenceSteps.ChooseAlliForschung,
                                                  4,
                                                  x1,
                                                  y1, x2, y2, 2);
                                          }

                                          await RunSequenceStepForIntervall(
                                              WhiteoutSurvival.SequenceSteps.LeaveAlliTabIfStuck, 1,
                                              x1, y1,
                                              x2,
                                              y2);
                                      }

                                      if (openDailyMission)
                                      {
                                          RunMouseMacroForDaily("VIPsammeln");
                                          RunMouseMacroForDaily("Mail");
                                          RunMouseMacroForDaily("OpenDailyMission_CapterMission");
                                          RunMouseMacroForDaily("GoToGrowMission");
                                          await RunSequenceStepForIntervallOrMissAttempts(
                                              WhiteoutSurvival.SequenceSteps.CollectDailyMission,
                                              30000,
                                              x1,
                                              y1, x2, y2, 3);
                                          RunMouseMacroForDaily("GoToDailyMission");
                                          await RunSequenceStepForIntervallOrMissAttempts(
                                              WhiteoutSurvival.SequenceSteps.CollectDailyMission,
                                              30000,
                                              x1,
                                              y1, x2, y2, 3);
                                          RunMouseMacroForDaily("LeaveDailyMission");
                                      }

                                      if (doBeasts)
                                      {
                                          RunMouseMacroForDaily("OeffneBestien");
                                          if (activatePetBuffs)
                                          {
                                              await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.PetBuffs,
                                                  20,
                                                  x1,
                                                  y1, x2,
                                                  y2);
                                          }

                                          RunMouseMacroForDaily("ZuBestienAbenteuer");
                                          await RunSequenceStepForIntervall(
                                              WhiteoutSurvival.SequenceSteps.StartBeastJob,
                                              40,
                                              x1,
                                              y1, x2,
                                              y2);
                                          await GetWaitJob(5, x1, y1, x2, y2);
                                          RunMouseMacroForDaily("OpenBeast");
                                          await RunSequenceStepForIntervall(
                                              WhiteoutSurvival.SequenceSteps.StartBeastJob,
                                              10,
                                              x1,
                                              y1, x2, y2);
                                          RunMouseMacroForDaily("BeastCollectChestAndLeave");
                                          await GetWaitJob(1, x1, y1, x2, y2);
                                      }

                                      if (collectExploration)
                                      {
                                          await RunSequenceStepForIntervall(
                                              WhiteoutSurvival.SequenceSteps.CollectExploration,
                                              2,
                                              x1, y1,
                                              x2,
                                              y2);
                                          await RunSequenceStepForIntervall(
                                              WhiteoutSurvival.SequenceSteps.FromExplorationBackToCity, 1,
                                              x1,
                                              y1,
                                              x2, y2);
                                      }

                                      if (heilen)
                                      {
                                          RunMouseMacroForDaily("5_StadtZuInf");
                                          RunMouseMacroForDaily("Inf_Zu_KH");
                                          await RunSequenceStepForIntervallOrMissAttempts(
                                              WhiteoutSurvival.SequenceSteps.StartHeal,
                                              15,
                                              x1,
                                              y1, x2, y2, 3);
                                      }
                                      
                                      if (forschen)
                                      {
                                          await DoResearch(x1, y1, x2, y2);
                                      }

                                      if (collectTroops)
                                      {
                                          RunMouseMacroForDaily("5_StadtZuInf");
                                          await RunSequenceStepForIntervallOrMissAttempts(
                                              WhiteoutSurvival.SequenceSteps.CollectInfAndSetupQueue,
                                              15,
                                              x1,
                                              y1, x2, y2, 3);
                                          RunMouseMacroForDaily("7_LancerMM");
                                          await RunSequenceStepForIntervallOrMissAttempts(
                                              WhiteoutSurvival.SequenceSteps.CollectMMAndSetupQueue,
                                              15,
                                              x1,
                                              y1, x2, y2, 3);
                                          RunMouseMacroForDaily("6_InfZuLancer");
                                          await RunSequenceStepForIntervallOrMissAttempts(
                                              WhiteoutSurvival.SequenceSteps.CollectLancerAndSetupQueue,
                                              15,
                                              x1,
                                              y1, x2, y2, 3);
                                      }


                                      if (doArena)
                                      {
                                          RunMouseMacroForDaily("7_LancerMM");
                                          RunMouseMacroForDaily("ArenaVonMMAus");
                                      }

                                      //TODO hier erweitern wenn danach noch sachen passierne
                                      if(   sendGathering || doIntels || collectOnlineRewardAndStamm || doBaum)
                                      RunMouseMacroForDaily("6_InfZuLancer");
                                      
                                      if (doMine)
                                      {
                                          await DoMine(x1, y1, x2, y2);
                                      }

                                      if (doBaum)
                                      {
                                          RunMouseMacroForDaily("9_OpenMenuAndScrollDown");
                                          await GetWaitJob(1, x1, y1, x2, y2);
                                          await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.BaumLow,
                                              15,
                                              x1,
                                              y1,
                                              x2,
                                              y2);
                                          await GetWaitJob(1, x1, y1, x2, y2);
                                      }
                                   
                                      if (collectOnlineRewardAndStamm)
                                      {
                                          RunMouseMacroForDaily("9_OpenMenuAndScrollDown");
                                          await GetWaitJob(1, x1, y1, x2, y2);
                                          if (collectRecrut && collectOnlineRewardAndStamm)
                                          {
                                              await RunSequenceStepForIntervall(
                                                  WhiteoutSurvival.SequenceSteps.CheckForOnlineRewardByMenu,
                                                  15,
                                                  x1,
                                                  y1,
                                                  x2,
                                                  y2);
                                              await RunSequenceStepForIntervall(
                                                  WhiteoutSurvival.SequenceSteps.CloseMenuIfOpen,
                                                  10, x1,
                                                  y1,
                                                  x2,
                                                  y2);

                                              await CheckForOnlineRewardOrRecruite(x1, y1, x2, y2);
                                          }
                                          else if (collectOnlineRewardAndStamm)
                                          {
                                              await RunSequenceStepForIntervallOrMissAttempts(
                                                  WhiteoutSurvival.SequenceSteps.CheckForOnlineRewardByMenu,
                                                  15,
                                                  x1,
                                                  y1, x2, y2, 3);
                                              await RunSequenceStepForIntervallOrMissAttempts(
                                                  WhiteoutSurvival.SequenceSteps.CloseMenuIfOpen,
                                                  5,
                                                  x1,
                                                  y1, x2, y2, 3);
                                              await RunSequenceStepForIntervallOrMissAttempts(
                                                  WhiteoutSurvival.SequenceSteps.CollectOnlineRewardOrStamina,
                                                  15,
                                                  x1,
                                                  y1, x2, y2, 3);
                                          }
                                      }
                                
                                      //RAUS AUS CITY
                                      if (HaveToLeaveCity(sendGathering, doIntels, sammelnZurueck))
                                      {
                                          await GetWaitJob(5, x1, y1, x2, y2);
                                          await RunSequenceStepForIntervall(
                                              WhiteoutSurvival.SequenceSteps.FromCityToWorldIfNot,
                                              8,
                                              x1,
                                              y1,
                                              x2,
                                              y2);
                                      }
                                      
                                      if (doIntels)
                                      {
                                          await SammelnZurückOderWarten(sammelnZurueck, x1, y1, x2, y2);

                                          await RunSequenceStepForIntervallOrMissAttempts(
                                              WhiteoutSurvival.SequenceSteps.CollectIntels,
                                              30000,
                                              x1,
                                              y1, x2, y2, 10);
                                          await RunSequenceStepForIntervall(
                                              WhiteoutSurvival.SequenceSteps.WaitForPotentialMarching,
                                              10, x1, y1, x2,
                                              y2);
                                      }

                                      if (!sendGathering)
                                      {
                                          if (!doIntels && sammelnZurueck)
                                          {
                                              await SammelnZurückOderWarten(sammelnZurueck, x1, y1, x2, y2);
                                          }
                                      }

                                      if (sendGathering)
                                      {
                                          if (!doIntels && sammelnZurueck)
                                          {
                                              await SammelnZurückOderWarten(sammelnZurueck, x1, y1, x2, y2);
                                              Thread.Sleep(70000);
                                          }
                                          Thread.Sleep(50000);
                                          RunMouseMacroForDaily("Sammeln4x7");
                                      }
                                  }

                                  if (script)
                                  {
                                      await RunSequenceStepForIntervall(
                                          WhiteoutSurvival.SequenceSteps.GoToCityFromWorld, 5,
                                          x1, y1, x2, y2);
                                      await RunSequenceStepForIntervallOrMissAttempts(
                                          WhiteoutSurvival.SequenceSteps.CloseAds,
                                          15,
                                          x1,
                                          y1, x2, y2, 2);
                                      await GetWaitJob(5, x1, y1, x2, y2);
                                      //RunMouseMacroForDaily("Mausaktionen");
                                      RunMouseMacroForDaily("RallyStarten_3x3");
                                  }
                              }
                          }
                          if (i == 0)
                          {
                              RunMouseMacroForDaily("Change_ACC");
                              await RunSequenceStepForIntervall(
                                  WhiteoutSurvival.SequenceSteps.SwitchAccWhenInMenuDialog,
                                  20,
                                  x1, y1, x2, y2);
                          }
                      }

                      stoppEmulator();
                  }
              }      
        }

        if (auswahlComboBox.SelectedItem?.ToString() == whiteoutSurvivalName &&
            (GetControlByName("doBearHunt") as CheckBox).Checked)
        {
            isLoopRunning = true;
            UnregisterHotKey(Handle, HOTKEY_NumPad1);
            UnregisterHotKey(Handle, HOTKEY_NumPad2);
            UnregisterHotKey(Handle, HOTKEY_NumPad3);
            UnregisterHotKey(Handle, HOTKEY_NumPad4);
            UnregisterHotKey(Handle, HOTKEY_NumPad5);
            UnregisterHotKey(Handle, HOTKEY_NumPad6);
            RegisterHotKey(Handle, HOTKEY_NumPad1, 0, (uint)Keys.NumPad1);
            RegisterHotKey(Handle, HOTKEY_NumPad2, 0, (uint)Keys.NumPad2);
            RegisterHotKey(Handle, HOTKEY_NumPad3, 0, (uint)Keys.NumPad3);
            RegisterHotKey(Handle, HOTKEY_NumPad4, 0, (uint)Keys.NumPad4);
            RegisterHotKey(Handle, HOTKEY_NumPad5, 0, (uint)Keys.NumPad5);
            RegisterHotKey(Handle, HOTKEY_NumPad6, 0, (uint)Keys.NumPad6);
        }

        if (auswahlComboBox.SelectedItem?.ToString() != whiteoutSurvivalName ||
            (!(GetControlByName("dailyStuff") as CheckBox).Checked &&
             !(GetControlByName("doBearHunt") as CheckBox).Checked))
        {
            isLoopRunning = true;
            Console.WriteLine("Starting loop");
            SetConfigOptionsFromCheckbox();
            await RunLoop(x1, y1, x2, y2);
        }
    }

    private async Task DoMine(int x1, int y1, int x2, int y2)
    {
        RunMouseMacroForDaily("9_OpenMenuAndScrollDown");
        await GetWaitJob(1, x1, y1, x2, y2);
        bool done = false;
        if (CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps.Mine))
        {
            await GetWaitJob(3, x1, y1, x2, y2);
            if (DateTime.Today.DayOfWeek == DayOfWeek.Friday || DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
            {
                if (CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps.IsDunkleSchmiede))
                {
                    await RunSequenceStepForIntervallOrMissAttempts(WhiteoutSurvival.SequenceSteps.Mine,
                        200,
                        x1,
                        y1,
                        x2,
                        y2, 15);
                    await GetWaitJob(3, x1, y1, x2, y2);
                }

                if (CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps.IsErdlabor))
                {
                    await RunSequenceStepForIntervallOrMissAttempts(WhiteoutSurvival.SequenceSteps.Mine,
                        200,
                        x1,
                        y1,
                        x2,
                        y2, 8);
                    await GetWaitJob(3, x1, y1, x2, y2);
                }
            }

            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday|| DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
            {
                if (CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps.IsLandDerTapferen))
                {
                    await RunSequenceStepForIntervallOrMissAttempts(WhiteoutSurvival.SequenceSteps.Mine,
                        200,
                        x1,
                        y1,
                        x2,
                        y2, 8);
                    await GetWaitJob(3, x1, y1, x2, y2);
                }
            }

            if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday || DateTime.Today.DayOfWeek == DayOfWeek.Thursday)
            {
                if (CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps.HoehleDerMonster))
                {
                    await RunSequenceStepForIntervallOrMissAttempts(WhiteoutSurvival.SequenceSteps.Mine,
                        200,
                        x1,
                        y1,
                        x2,
                        y2, 8);
                    await GetWaitJob(3, x1, y1, x2, y2);
                }

                if (CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps.Leuchstein))
                {
                    await RunSequenceStepForIntervallOrMissAttempts(WhiteoutSurvival.SequenceSteps.Mine,
                        200,
                        x1,
                        y1,
                        x2,
                        y2, 8);
                    await GetWaitJob(3, x1, y1, x2, y2);
                }
            }

            RunMouseMacroForDaily("LeaveArrowClick");
            await GetWaitJob(5, x1, y1, x2, y2);
        }

        await RunSequenceStepForIntervall(
            WhiteoutSurvival.SequenceSteps.CloseMenuIfOpen,
            5, x1,
            y1,
            x2,
            y2);
    }

    private async Task DoResearch(int x1, int y1, int x2, int y2)
    {
        RunMouseMacroForDaily("5_StadtZuInf");
        RunMouseMacroForDaily("Inf_Zu_KH");

        if (CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps.StartForschung))
        {
            for (int f = 0; f < 15; f++)
            {
                if (f == 5)
                {
                    RunMouseMacroForDaily("ResearchLeaveDetailClick");
                    RunMouseMacroForDaily("SwitchResearchEconomy");
                    RunMouseMacroForDaily("ResearchScrollUp");
                }
                if (f == 10)
                    {
                        RunMouseMacroForDaily("ResearchLeaveDetailClick");
                        RunMouseMacroForDaily("SwitchResearchWar");
                        RunMouseMacroForDaily("ResearchScrollUp");
                    }
                   
                if (f != 0)
                {
                await RunSequenceStepForIntervallOrMissAttempts(
                    WhiteoutSurvival.SequenceSteps.StartForschung,
                    10,
                    x1,
                    y1, x2, y2, 3);
            }

            else
                {
                    await RunSequenceStepForIntervallOrMissAttempts(
                        WhiteoutSurvival.SequenceSteps.StartForschung,
                        10,
                        x1,
                        y1, x2, y2, 3);
                    RunMouseMacroForDaily("ResearchLeaveDetailClick");
                    RunMouseMacroForDaily("ResearchGrowClick");
                    RunMouseMacroForDaily("ResearchScrollUp");
                }
                RunMouseMacroForDaily("ResearchLeaveDetailClick");
                if (IsResearchRunning())
                {
                    break;
                }
                RunMouseMacroForDaily("ScrollResearch");
            }
                          
        RunMouseMacroForDaily("LeaveForschung");

        await RunSequenceStepForIntervallOrMissAttempts(
            WhiteoutSurvival.SequenceSteps.LeaveForschung,
            5,
            x1,
            y1, x2, y2, 5);
        }  
    }

    private async Task SammelnZurückOderWarten(bool sammelnZurueck, int x1, int y1, int x2, int y2)
    {
        if (sammelnZurueck)
        {
            await RunSequenceStepForIntervallOrMissAttempts(
                WhiteoutSurvival.SequenceSteps.SammelnZurückholen,
                10,
                x1,
                y1, x2, y2, 20);
        }
        else
        {
            await GetWaitJob(5, x1, y1, x2, y2);
        }
    }

    private static bool HaveToLeaveCity(bool sendGathering, bool doIntels, bool maerscheZurueck)
    {
        return sendGathering || doIntels || maerscheZurueck;
    } 

    private static void stoppEmulator(string processName = "HD-Player")
    {
        Thread.Sleep(5000);

        // Alle Prozesse mit dem Namen beenden
        var processes = Process.GetProcessesByName(processName);
        foreach (var pro in processes)
        {
            pro.Kill(); // Prozess beenden
            Console.WriteLine($"Prozess {pro.ProcessName} wurde beendet.");
        }

        if (processes.Length == 0) Console.WriteLine($"Kein {processName}-Prozess gefunden.");
    }

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    private int  startEmulatorByID(string emulator, bool restartIfNotFullScreen = true, int counter = 0)
    {
        if (counter == 3)
            return 99;
        var processes = StarteEmulatorMitApp(emulator);
        
        /*
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => overlay.NamensLabel.Text = processes.FirstOrDefault()?.MainWindowTitle));
        }
        else
        {
            overlay.NamensLabel.Text = processes.FirstOrDefault()?.MainWindowTitle;
        }
        */
        
        

        // Warten, bis das Fenster erstellt ist
        //
        Thread.Sleep(15000);
        stoppEmulator("BlueStacks Browser");
        Thread.Sleep(10000);

        keybd_event(VK_F11, 0, KEYEVENTF_KEYDOWN, 0);
        keybd_event(VK_F11, 0, KEYEVENTF_KEYUP, 0);
        Thread.Sleep(1000);
        keybd_event(VK_F11, 0, KEYEVENTF_KEYDOWN, 0);
        keybd_event(VK_F11, 0, KEYEVENTF_KEYUP, 0);
        Thread.Sleep(4000);
        if (!IsEmulatorIsRunningInFulLScreen() && restartIfNotFullScreen)
        {
            counter = counter + 1;
            stoppEmulator();
            return startEmulatorByID(emulator,true, counter);
        }

        Thread.Sleep(2000);
        return 0;
    }
    
    [DllImport("user32.dll")]
static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
private const uint SWP_NOSIZE = 0x0001;
private const uint SWP_NOMOVE = 0x0002;
private const uint SWP_SHOWWINDOW = 0x0040;

    private static Process[] StarteEmulatorMitApp(string emulator, string package = "com.gof.global")
    {
        var bluestacksPath = @"C:\Program Files\BlueStacks_nxt\HD-Player.exe";
        var arguments = $"--instance {emulator} --cmd launchAppWithBsx --package \"{package}\"";
      //  var process = Process.Start(@"C:\Program Files\BlueStacks_bgp64\HD-RunApp.exe", "-json \"{\\\"app_icon_url\\\":\\\"\\\",\\\"app_name\\\":\\\"Whiteout Survival\\\",\\\"app_url\\\":\\\"\\\",\\\"app_pkg\\\":\\\"com.gof.global\\\"}\"");
        var process = Process.Start(bluestacksPath, arguments);
        Thread.Sleep(2000);
        var processes = Process.GetProcessesByName("HD-Player");
    //    var processes = Process.GetProcessesByName("HD-RunApp");
           if (processes.Length > 0)
              {
                  IntPtr hWnd = processes[0].MainWindowHandle;
                  SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
              }
              /*       if (processes.Length > 0)
                  {
                      var hWnd = processes[0].MainWindowHandle;
                      ShowWindow(hWnd, SW_RESTORE);
                      SetForegroundWindow(hWnd);
                  }*/
        
        return processes;
    }
    
    private bool CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps step)
    {
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        int.TryParse(x2TextBox.Text, out var x2);
        int.TryParse(y2TextBox.Text, out var y2);
        ((WhiteoutSurvival)jobConfig).sequenceStep = step;
        var result = ImageFinder.RunJobs(jobConfig, x1, y1, x2, y2);
        return result;
    }
    
    
    private bool IsResearchRunning()
    {
        return CheckIfSpecificTemplatesFound(WhiteoutSurvival.SequenceSteps.CheckIfResearchRunning);
    }

    private bool IsEmulatorIsRunningInFulLScreen()
    {
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        int.TryParse(x2TextBox.Text, out var x2);
        int.TryParse(y2TextBox.Text, out var y2);
        ((WhiteoutSurvival)jobConfig).sequenceStep = WhiteoutSurvival.SequenceSteps.CloseAds;
        var result = ImageFinder.RunJobs(jobConfig, x1, y1, x2, y2);
        return result;
    }

    private async Task GetWaitJob(int seconds, int x1, int y1, int x2, int y2)
    {
        await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.Wait, 10, x1, y1, x2, y2);
    }

    private void RunMouseMacroForDaily(string macroName)
    {
        Console.WriteLine(macroName);
        int.TryParse(x1TextBox.Text, out var x1);
        int.TryParse(y1TextBox.Text, out var y1);
        mouseHook.XOffSet = x1;
        mouseHook.YOffSet = y1;
        mouseHook.LoadFromCsv(@"C:\Users\schmi\Pictures\AutoClicker\WhiteoutSurvival_NEW\MouseRouten\daily\" +
                              macroName + ".csv");
        mousePlayer.PlayAndResetPosition(mouseHook.MouseEvents);
    }

    private async Task RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps step, int seconds, int x1, int y1,
        int x2, int y2)
    {
        ((WhiteoutSurvival)jobConfig).sequenceStep = step;
        isLoopRunning = true;
        StopLoopAfterWhile(seconds);
        await RunLoop(x1, y1, x2, y2);
    }

    private async Task RunSequenceStepForIntervallOrMissAttempts(WhiteoutSurvival.SequenceSteps step, int seconds,
        int x1, int y1, int x2, int y2, int failTrys)
    {
        ((WhiteoutSurvival)jobConfig).sequenceStep = step;
        isLoopRunning = true;
        StopLoopAfterWhile(seconds);
        await RunLoop2(x1, y1, x2, y2, failTrys);
        StopLoop();
        stopLoopTimer.Stop();
        stopLoopRemainingTime = 0;
    }

    private async Task CheckForOnlineRewardOrRecruite(int x1, int y1, int x2, int y2)
    {
        await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CollectOnlineRewardOrStamina, 30, x1, y1, x2,
            y2);
        await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CloseMenuIfOpen, 5, x1, y1, x2, y2);
        RunMouseMacroForDaily("9_OpenMenuAndScrollDown");
        await GetWaitJob(5, x1, y1, x2, y2);
        await RunSequenceStepForIntervall(WhiteoutSurvival.SequenceSteps.CollectFreeRecrutement, 30, x1, y1, x2, y2);
    }

    private void StopLoopAfterWhile(int i)
    {
        stopLoopRemainingTime = i;
        stopLoopTimer = new Timer
        {
            Interval = 1000 // 1 Sekunde
        };
        stopLoopTimer.Tick += StopLoopTimer_Tick;
        stopLoopTimer.Start();
    }


    private async Task RunLoop2(int x1, int y1, int x2, int y2, int failTrys)
    {
        var i = 0;
        while (isLoopRunning)
        {
            if (i > failTrys)
            {
                Console.Write("Weiter wegen Fehlversuche: Anzahl " + i);
                return;
            }

            if (ImageFinder.RunJobs(jobConfig, x1, y1, x2, y2))
            {
                if (shutdownTimerEnabled)
                    resetTimer();
                i = 0;
            }
            else
            {
                i = i + 1;
            }

            var random = new Random();
            var randomNumber = random.Next(2, 4);
            await Task.Delay(1 * 1000); // 10 Sekunden warten
        }
    }

    private async Task RunLoop(int x1, int y1, int x2, int y2)
    {
        while (isLoopRunning)
        {
            if (ImageFinder.RunJobs(jobConfig, x1, y1, x2, y2))
                if (shutdownTimerEnabled)
                    resetTimer();

            var random = new Random();
            var randomNumber = random.Next(2, 4);
            await Task.Delay(1 * 1000); // 10 Sekunden warten
        }
    }

    private void SetConfigOptionsFromCheckbox()
    {
        if (auswahlComboBox.SelectedItem?.ToString() == "Lust Goddess")
        {
            var skipLedgyCb = GetControlByName("skipLedgy") as CheckBox;
            if (skipLedgyCb != null) ((LustGoddess)jobConfig).skipLedgy = skipLedgyCb.Checked;
            var openDailyChests = GetControlByName("openDailyChests") as CheckBox;
            if (openDailyChests != null) ((LustGoddess)jobConfig).openDailyChests = openDailyChests.Checked;
            var openChests = GetControlByName("openChests") as CheckBox;
            if (openChests != null) ((LustGoddess)jobConfig).openChests = openChests.Checked;
        }

        if (auswahlComboBox.SelectedItem?.ToString() == whiteoutSurvivalName)
        {
            var beastHuntPolarTerror = GetControlByName("polarTerrorHunt") as CheckBox;
            if (beastHuntPolarTerror != null)
                ((WhiteoutSurvival)jobConfig).doBeastHuntPolarTerror = beastHuntPolarTerror.Checked;
            var intel = GetControlByName("doIntels") as CheckBox;
            if (intel != null) ((WhiteoutSurvival)jobConfig).doIntels = intel.Checked;
            var doMails = GetControlByName("doMails") as CheckBox;
            if (doMails != null) ((WhiteoutSurvival)jobConfig).doMails = doMails.Checked;
            var doBearHunt = GetControlByName("doBearHunt") as CheckBox;
            if (doBearHunt != null) ((WhiteoutSurvival)jobConfig).doBearHunt = doBearHunt.Checked;
            var doAlliMob = GetControlByName("doAlliMob") as CheckBox;
            if (doAlliMob != null) ((WhiteoutSurvival)jobConfig).doAlliMob = doAlliMob.Checked;
            var doErkundung = GetControlByName("doErkundung") as CheckBox;
            if (doErkundung != null) ((WhiteoutSurvival)jobConfig).doErkundung = doErkundung.Checked;
            var doCollectStuff = GetControlByName("doGathering") as CheckBox;
            if (doCollectStuff != null) ((WhiteoutSurvival)jobConfig).collectStuff = doCollectStuff.Checked;
            var littleAccounts = GetControlByName("littleAccounts") as CheckBox;
            if (littleAccounts != null) ((WhiteoutSurvival)jobConfig).littleAccounts = littleAccounts.Checked;
            var doDailyStuff = GetControlByName("dailyStuff") as CheckBox;
            if (doDailyStuff != null) ((WhiteoutSurvival)jobConfig).doDailyStuff = doDailyStuff.Checked;
            var doTroops = GetControlByName("collectTroops") as CheckBox;
            if (doTroops != null) ((WhiteoutSurvival)jobConfig).doTroops = doTroops.Checked;
        }
    }


    public void resetTimer()
    {
        // Stoppe den Timer
        shutdownTimer.Stop();

        // Zeige eine Nachricht an, dass der Shutdown abgebrochen wurde
        // Setze den verbleibenden Countdown zurück auf 30 Minuten
        remainingTime = 30 * 60;

        // Starte den Timer neu, um den Countdown fortzusetzen
        shutdownTimer.Start();
    }

    private void SetScreenCords()
    {
        Text = jobConfig.Name;
        x1TextBox.Text = jobConfig.ScreenDefaultStart.X.ToString();
        y1TextBox.Text = jobConfig.ScreenDefaultStart.Y.ToString();
        x2TextBox.Text = jobConfig.ScreenDefaultEnd.X.ToString();
        y2TextBox.Text = jobConfig.ScreenDefaultEnd.Y.ToString();
    }

    private void SetupWhiteOutElements()
    {
        checkBoxPanel.Controls.Clear();
         var color = Color.LightCoral;
        var color2 = Color.LightBlue;
        var color3 = Color.LightGreen;
        CreateGameOptionCB("Daily Stuff", "dailyStuff", true,color);
        CreateGameOptionCB("JUC Dorf", "littleAccounts", true,color);
        CreateGameOptionCB("Polar Terror", "polarTerrorHunt", false,color3);
        CreateGameOptionCB("Intel", "doIntels", false,color2);
        CreateGameOptionCB("Mail", "doMails", false);
        CreateGameOptionCB("Bear Hunt", "doBearHunt", false);
        CreateGameOptionCB("Alli Mob", "doAlliMob", false);
        CreateGameOptionCB("Erkundungen", "doErkundung", true,color);
        CreateGameOptionCB("Truppen", "collectTroops", true,color);
        CreateGameOptionCB("AllianzTab", "doAllianzTab", true,color);
        CreateGameOptionCB("RekrutStam", "collectStam", true,color);
        CreateGameOptionCB("Farmen", "doGathering", true,color3);
        CreateGameOptionCB("Dorf starten", "startDorf", false);
        CreateGameOptionCB("mit Clicker", "mitClicker", false);
        CreateGameOptionCB("Rekrutieren", "collectRecrute", true,color);
        CreateGameOptionCB("alliHelp", "alliHelp", true,color);
        CreateGameOptionCB("alliResearch", "alliResearch", true,color);
        CreateGameOptionCB("alliChest", "alliChest", true,color);
        CreateGameOptionCB("alliAutoJoin", "alliAutoJoin", true,color);
        CreateGameOptionCB("doArena", "doArena", true,color3);
        CreateGameOptionCB("doBaum", "doBaum", true,color3);
        CreateGameOptionCB("shieldUp", "shieldUp", false);
        CreateGameOptionCB("activatePetBuffs", "activatePetBuffs", true,color3);
        CreateGameOptionCB("Mausscript", "script", false);
        CreateGameOptionCB("Märsche zurück", "stopGathering", true);
        CreateGameOptionCB("Heilen", "heal", true,color3);
        CreateGameOptionCB("Forschen", "forschen", true,color);
        CreateGameOptionCB("startEmulator", "startEmulator", true,color);
        CreateGameOptionCB("doMine", "doMine", true,color3);
    }

    private void CreateGameOptionCB(string text, string name, bool checkedValue,Color backColor = default)
    {
        var cbStartDorf = new CheckBox
        {
            Text = text,
            Name = name,
            AutoSize = true,
            BackColor = backColor,
            Checked = checkedValue
        };
        checkBoxesContainer.Add(cbStartDorf);
        checkBoxPanel.Controls.Add(cbStartDorf);
    }

    private void SetupLustGoddessElements()
    {
        checkBoxPanel.Controls.Clear();
        var ledgySkippen = new CheckBox
        {
            Text = "Ledgy skippen",
            Name = "skipLedgy",
            AutoSize = true,
            Checked = true
        };
        checkBoxesContainer.Add(ledgySkippen);
        checkBoxPanel.Controls.Add(ledgySkippen);

        var cbTruhenoOffnen = new CheckBox
        {
            Text = "Truhen",
            Name = "openChests",
            AutoSize = true,
            Checked = true
        };
        checkBoxesContainer.Add(cbTruhenoOffnen);
        checkBoxPanel.Controls.Add(cbTruhenoOffnen);
        var cbDailyTruhenOeffnen = new CheckBox
        {
            Text = "Gewinn-Truhen",
            Name = "openDailyChests",
            AutoSize = true,
            Checked = true
        };
        checkBoxesContainer.Add(cbDailyTruhenOeffnen);
        checkBoxPanel.Controls.Add(cbDailyTruhenOeffnen);
    }

    private IComponent GetControlByName(string name)
    {
        return checkBoxesContainer.Components
            .Cast<IComponent>()
            .FirstOrDefault(c =>
            {
                var control = c as Control;
                return control != null && control.Name == name;
            });
    }

    private void IdleTimer(object sender, EventArgs e)
    {
        lamp.Toggle();
        shutdownTimerEnabled = !shutdownTimerEnabled;
    }

    private void stoppEmulator(object? sender, EventArgs e)
    {
        stoppEmulator();
    }
}