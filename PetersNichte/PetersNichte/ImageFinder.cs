using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = System.Drawing.Point;

namespace WinFormsApp1;

public static class ImageFinder
{
    public enum Actions
    {
        None = 0, // Kein Mausklick
        LeftClick = 1, // Linksklick
        RightClick = 2, // Rechtsklick        
        Wait = 3
    }

    public const uint INPUT_MOUSE = 0;
    public const uint MOUSEEVENTF_MOVE = 0x0001;
    public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    public const uint MOUSEEVENTF_LEFTUP = 0x0004;

    private const bool continueByLedgyChest = false;

    private const bool afterClickBackToPosition = true;
    private static bool onHold;
    private static readonly string directory = @"C:\Users\schmi\Pictures\AutoClicker";

    private static readonly string logFilePath = @"C:\Users\schmi\Pictures\AutoClicker\Log\debug.txt";
    public static bool saveTemplate = false;
    public static bool whiteout = false;

    private static readonly string DebugScreenCappturePath =
        @"C:\Users\schmi\Pictures\AutoClicker\Screencapture\";

    static ImageFinder()
    {
    }

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    private static extern void SendInput(uint nInputs, INPUT[] pInputs, int cbSize);


    public static void ResetOnHold()
    {
        onHold = false;
    }

    public static Job CreateStepFindAndWait(string jobName, string templatePath, int prio, double factor,
        bool takeScreenshotIfTrue = false,
        bool takeScreenshotIfFalse = false)
    {
        var ifActions = new List<Action>();
        var ifAction = new Action();

        ifAction.ActionEvent = Actions.Wait;


        ifActions.Add(ifAction);

        var elseActions = new List<Action>();

        var a = new Job();
        a.factor = factor;
        a.TakeScreenshotIfTrue = takeScreenshotIfTrue;
        a.TakeScreenshotIfFalse = takeScreenshotIfFalse;
        a.TemplatePath = templatePath;
        a.Prio = prio;
        a.IfActions = ifActions;
        a.ElseActions = elseActions;
        a.Name = jobName;
        return a;
    }

    public static Job CreateStepFindAndLeftClickImage(string jobName, string templatePath, int prio, double factor,
        bool useImageFinterPosition, int offsetPosX, int offsetPosY, List<AdditonalClickAction> clickActions,
        int? posX = null, int? posY = null,
        bool takeScreenshotIfTrue = false,
        bool takeScreenshotIfFalse = false,
        bool raiseTurns = false,
        bool resetTurns = false)
    {
        var ifActions = new List<Action>();
        var ifAction = new Action();

        ifAction.ActionEvent = Actions.LeftClick;
        ifAction.offSetPosX = offsetPosX;
        ifAction.offSetPosY = offsetPosY;
        ifAction.UseImageFinterPosition = useImageFinterPosition;
        if (posX.HasValue && posY.HasValue)
        {
            ifAction.PositionX = posX.Value;
            ifAction.PositionY = posY.Value;
        }

        ifActions.Add(ifAction);

        foreach (var action in clickActions)
        {
            var clickactionEntry = new Action();

            clickactionEntry.DelayForClick = action.DelayForClick;
            clickactionEntry.ActionEvent = Actions.LeftClick;
            clickactionEntry.offSetPosX = offsetPosX;
            clickactionEntry.offSetPosY = offsetPosY;
            clickactionEntry.UseImageFinterPosition = false;
            clickactionEntry.PositionX = action.Position.X;
            clickactionEntry.PositionY = action.Position.Y;
            ifActions.Add(clickactionEntry);
        }

        var elseActions = new List<Action>();

        var a = new Job();
        a.factor = factor;
        a.TakeScreenshotIfTrue = takeScreenshotIfTrue;
        a.TakeScreenshotIfFalse = takeScreenshotIfFalse;
        a.TemplatePath = templatePath;
        a.Prio = prio;
        a.IfActions = ifActions;
        a.ElseActions = elseActions;
        a.Name = jobName;
        a.RaiseTurns = raiseTurns;
        a.ResetTurns = resetTurns;
        return a;
    }

    public static void SimulateClick(int x, int y)
    {
        // Bildschirmauflösung für Umrechnung ermitteln
        var screenWidth = Screen.PrimaryScreen.Bounds.Width;
        var screenHeight = Screen.PrimaryScreen.Bounds.Height;

        // Mausposition relativ zur Bildschirmauflösung skalieren (0 - 65535)
        var scaledX = x * 65535 / screenWidth;
        var scaledY = y * 65535 / screenHeight;

        var inputs = new INPUT[3];

        // Maus an Zielposition bewegen (ohne Cursor)
        inputs[0].Type = INPUT_MOUSE;
        inputs[0].U.mi.dx = scaledX;
        inputs[0].U.mi.dy = scaledY;
        inputs[0].U.mi.mouseData = 0;
        inputs[0].U.mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;
        inputs[0].U.mi.time = 0;
        inputs[0].U.mi.dwExtraInfo = IntPtr.Zero;

        // Linksklick drücken
        inputs[1].Type = INPUT_MOUSE;
        inputs[1].U.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

        // Linksklick loslassen
        inputs[2].Type = INPUT_MOUSE;
        inputs[2].U.mi.dwFlags = MOUSEEVENTF_LEFTUP;

        // Eingaben senden
        SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
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

    private static Bitmap TakeScreenshot(int x1, int y1, int x2, int y2)
    {
        var captureRectangle = new Rectangle(x1, y1, x2 - x1, y2 - y1);
        var screenCapture = CaptureScreenArea(captureRectangle);
        return screenCapture;
    }

    public static void TakeScreenshotAndSave(int x1, int y1, int x2, int y2)
    {
        var timestamp = DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss");
        var filePath = Path.Combine(directory, "Screenshot");
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        var screenCapture = TakeScreenshot(x1, y1, x2, y2);
        screenCapture?.Save(filePath + $"\\{timestamp}.png");
    }

    public static void TakeScreenshotAndSaveSpecial(int x1, int y1, int x2, int y2)
    {
        var timestamp = DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss");
        var filePath = Path.Combine(directory, "ScreenshotSpecial");
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        var screenCapture = TakeScreenshot(x1, y1, x2, y2);
        screenCapture?.Save(filePath + $"\\{timestamp}.png");
    }

    static Dictionary<string,Mat> searchTemplateDictionary = new Dictionary<string,Mat>();

    public static bool RunJobs(JobConfig jobConfig, int x1, int y1, int x2, int y2)
    {
        var jobs = new List<Job>();
        var gameImage = TakeScreenshot(x1, y1, x2, y2);
        //TakeScreenshotAndSaveSpecial(x1, y1, x2, y2);
        foreach (var job in jobConfig.GetLeftclickJobs())
            jobs.Add(CreateStepFindAndLeftClickImage(job.JobName, job.TemplatePath, job.Priority,
                job.Uebereinstimmungsfaktor,
                job.UseImageFinterPosition, x1, y1, job.ClickActions, job.FixClickPosX, job.FixClickPosY,
                job.CaptureScreenshotByIf,
                job.CaptureScreenshotByElse, job.RaiseTurns, job.ResetTurns));

        foreach (var job in jobConfig.GetWaitJobs())
            jobs.Add(CreateStepFindAndWait(job.JobName, job.TemplatePath, job.Priority, job.Uebereinstimmungsfaktor
                , job.CaptureScreenshotByIf,
                job.CaptureScreenshotByElse));

        if (onHold) return true;

        var screenGray = ConvertToGrayscale(gameImage);
        var largeImageMat = screenGray.ToMat();
        var result = false;
        foreach (var step in jobs.OrderBy(x => x.Prio))
        {
            Mat templateMat = null;
            if (!searchTemplateDictionary.ContainsKey(step.TemplatePath))
            {
                using (var searchTemplate = ConvertToGrayscale(new Bitmap(step.TemplatePath)))
                {
                    searchTemplateDictionary.Add(step.TemplatePath, searchTemplate.ToMat());
                }
            }
            templateMat = searchTemplateDictionary.GetValueOrDefault(step.TemplatePath);
            
            if(templateMat == null) continue;

            var Point = GetPointOrNull(step, largeImageMat,templateMat);
            var mousePositionForEvent = Cursor.Position;

            if (Point.HasValue)
            {
                Console.WriteLine(step.TemplatePath);
                result = true;
                foreach (var action in step.IfActions)
                {
                    if (action.ActionEvent == Actions.LeftClick)
                    {
                        var actionPoint = new Point();
                        if (action.UseImageFinterPosition)
                        {
                            Console.WriteLine("KlickaufBild");
                            actionPoint.X = Point.Value.X;
                            actionPoint.Y = Point.Value.Y;
                        }
                        else if (action.PositionY > 0 && action.PositionX > 0)
                        {
                            Console.WriteLine($"Klick auf feste Position X:{action.PositionX} Y:{action.PositionY}");
                            actionPoint.X = action.PositionX - action.offSetPosX;
                            actionPoint.Y = action.PositionY - action.offSetPosY;
                        }

                        if (action.DelayForClick > 0)
                            Thread.Sleep(action.DelayForClick);
                        SimulateClick(actionPoint.X + action.offSetPosX, actionPoint.Y + action.offSetPosY);

                        if (afterClickBackToPosition)
                            SetCursorPos(mousePositionForEvent.X, mousePositionForEvent.Y);
                    }

                    if (action.ActionEvent == Actions.Wait)
                    {
                        onHold = true;
                        Console.WriteLine(step.Name + "Wait - on Hold");
                    }
                }

                if (step.RaiseTurns)
                    jobConfig.turns = jobConfig.turns + 1;
                if (step.ResetTurns)
                    jobConfig.turns = 0;
                break;
            }

            //Console.WriteLine("Template nicht gefunden");
            foreach (var action in step.ElseActions)
            {
                if (action.ActionEvent == Actions.LeftClick)
                {
                    var actionPoint = new Point();
                    if (action.UseImageFinterPosition)
                    {
                        Console.WriteLine("KlickaufBild");
                        actionPoint.X = Point.Value.X;
                        actionPoint.Y = Point.Value.Y;
                    }
                    else if (action.PositionY > 0 && action.PositionX > 0)
                    {
                        Console.WriteLine($"Klick auf feste Position X:{action.PositionX} Y:{action.PositionY}");
                        SetCursorPos(action.PositionX - action.offSetPosX, action.PositionY - action.offSetPosY);
                        actionPoint.X = action.PositionX;
                        actionPoint.Y = action.PositionY;
                    }

                    SimulateClick(actionPoint.X + action.offSetPosX, actionPoint.Y + action.offSetPosY);

                    if (afterClickBackToPosition)
                        SetCursorPos(mousePositionForEvent.X, mousePositionForEvent.Y);
                    Console.WriteLine("LinksClick");
                }

                if (action.ActionEvent == Actions.Wait)
                {
                    onHold = true;
                    Console.WriteLine(step.Name + "Wait - on Hold");
                }
            }
        }

        return result;
    }

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);
/*
    private static Bitmap MatToBitmap(Mat mat)
    {
        // Sicherstellen, dass das Mat-Objekt im richtigen Format ist (CV_8UC3 für RGB)
        if (mat.Depth() != MatType.CV_8U || mat.Channels() != 3)
            throw new ArgumentException("Mat muss im Format CV_8UC3 (RGB) sein.");


        // Mat von BGR nach RGB konvertieren (falls nötig)
        if (mat.Empty()) throw new ArgumentNullException("Mat ist leer");

        var convertedMat = new Mat();
        Cv2.CvtColor(mat, convertedMat, ColorConversionCodes.BGR2RGB); // Umwandlung von BGR nach RGB

        // Byte-Array erstellen und die Mat-Daten in das Array kopieren
        var byteArray = new byte[convertedMat.Rows * convertedMat.Cols * convertedMat.Channels()];
        Marshal.Copy(convertedMat.Data, byteArray, 0, byteArray.Length);

        // Bitmap mit denselben Dimensionen wie Mat erstellen
        var bitmap = new Bitmap(convertedMat.Width, convertedMat.Height, PixelFormat.Format24bppRgb);

        // Pixel in das Bitmap kopieren
        var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        // Kopiere die Byte-Daten in das Bitmap
        Marshal.Copy(byteArray, 0, bitmapData.Scan0, byteArray.Length);

        bitmap.UnlockBits(bitmapData);

        return bitmap;
    } */

    private static Bitmap MatToBitmap(Mat mat)
    {
        if (mat.Empty())
            throw new ArgumentNullException("Mat ist leer");

        // Mat von BGR nach RGB konvertieren (falls nötig)
        var convertedMat = new Mat();
        Cv2.CvtColor(mat, convertedMat, ColorConversionCodes.BGR2RGB);

        // Bitmap mit denselben Dimensionen wie Mat erstellen
        var bitmap = new Bitmap(convertedMat.Cols, convertedMat.Rows, PixelFormat.Format24bppRgb);

        // Sperre die Bitmap-Daten für den Zugriff
        var bitmapData = bitmap.LockBits(
            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.WriteOnly,
            PixelFormat.Format24bppRgb);

        // Mat-Daten in ein Byte-Array kopieren
        var matData = new byte[convertedMat.Rows * convertedMat.Step()];
        Marshal.Copy(convertedMat.Data, matData, 0, matData.Length);

        // Zeile für Zeile die Daten in die Bitmap kopieren
        for (var y = 0; y < convertedMat.Rows; y++)
        {
            var sourceIndex = y * (int)convertedMat.Step(); // Zeilen-Offset in Mat
            var destPtr = IntPtr.Add(bitmapData.Scan0, y * bitmapData.Stride); // Zeilen-Offset in Bitmap
            Marshal.Copy(matData, sourceIndex, destPtr, bitmap.Width * 3); // Breite * 3 (RGB)
        }

        bitmap.UnlockBits(bitmapData);
        return bitmap;
    }


    private static Bitmap ConvertToGrayscale(Bitmap original)
    {
        // Bitmap mit denselben Dimensionen wie das Original erstellen
        var grayscaleBitmap = new Bitmap(original.Width, original.Height);

        for (var y = 0; y < original.Height; y++)
        for (var x = 0; x < original.Width; x++)
        {
            // Originalfarbe des Pixels abrufen
            var originalColor = original.GetPixel(x, y);

            // Graustufenwert berechnen (Luminanz)
            var grayValue = (int)(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B);

            // Graustufenfarbe erstellen
            var grayColor = Color.FromArgb(grayValue, grayValue, grayValue);

            // Graustufenfarbe im neuen Bitmap setzen
            grayscaleBitmap.SetPixel(x, y, grayColor);
        }

        return grayscaleBitmap;
    }

    private static OpenCvSharp.Point? GetPointOrNull(Job job, Mat screenshotMat, Mat templateMat)
    {
            Cv2.CvtColor(screenshotMat, screenshotMat, ColorConversionCodes.BGR2RGB);
            Cv2.CvtColor(templateMat, templateMat, ColorConversionCodes.BGR2RGB);

     //       SaveImagesToMatchPerJob(screenshotMat, templateMat, job.Name);

            var result = new Mat();
            Cv2.MatchTemplate(screenshotMat, templateMat, result, TemplateMatchModes.CCoeffNormed);
            var maxLoc = new OpenCvSharp.Point();
            double maxVal = 0;
            Cv2.MinMaxLoc(result, out _, out maxVal, out _, out maxLoc);

            if (maxVal >= job.factor)
            {
                Console.WriteLine("Wert: " + maxVal);
                if (job.TemplatePath.Contains("online_reward_return.png"))
                    SaveImagesToMatchPerJob(screenshotMat, templateMat, job.Name);

#if DEBUG
                using (var
                       writer = new StreamWriter(logFilePath, true)) // append: false überschreibt die Datei
                {
                    writer.WriteLine(
                        $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ssss")} Job: {job.Name} Faktor: {maxVal}");
                }
#endif
                return new OpenCvSharp.Point(maxLoc.X + templateMat.Width / 2, maxLoc.Y + templateMat.Height / 2);
            }
        


        return null;
    }

    private static void SaveImagesToMatchPerJob(Mat screenshotMat, Mat templateMat, string jobName)
    {
        var bitmapl = MatToBitmap(screenshotMat);
        var bitmaps = MatToBitmap(templateMat);
        var savePathl = Path.Combine(DebugScreenCappturePath, jobName + "_screenshot.png");
        var savePaths = Path.Combine(DebugScreenCappturePath, jobName + "_searchImg.png");
        bitmapl.Save(savePathl, ImageFormat.Png);
        bitmaps.Save(savePaths, ImageFormat.Png);
    }

    private static Bitmap TakeScreenshot()
    {
        // Bildschirmgröße abrufen
        var screenRect = SystemInformation.VirtualScreen;

        // Neues Bitmap-Objekt für den Screenshot erstellen
        var screenshot = new Bitmap(screenRect.Width, screenRect.Height);

        // Graphics-Objekt zum Zeichnen auf dem Bitmap erstellen
        using (var g = Graphics.FromImage(screenshot))
        {
            // Screenshot vom Bildschirm aufnehmen
            g.CopyFromScreen(screenRect.Left, screenRect.Top, 0, 0, screenRect.Size);
        }

        return screenshot;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public uint Type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)] public MOUSEINPUT mi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    public class Job
    {
        public double factor { get; set; }
        public string TemplatePath { get; set; }
        public int Prio { get; set; }
        public List<Action> IfActions { get; set; }
        public List<Action> ElseActions { get; set; }
        public bool TakeScreenshotIfTrue { get; set; }
        public bool TakeScreenshotIfFalse { get; set; }
        public string Name { get; set; }
        public bool ResetTurns { get; set; }
        public bool RaiseTurns { get; set; }
    }

    public class Action
    {
        public int offSetPosX { get; set; }
        public int offSetPosY { get; set; }
        public Actions ActionEvent { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool UseImageFinterPosition { get; set; }
        public int DelayForClick { get; set; }
    }
}

public class AdditonalClickAction
{
    public AdditonalClickAction(Point Position, int DelayForClick)
    {
        this.DelayForClick = DelayForClick;
        this.Position = Position;
    }

    public AdditonalClickAction(Point Position)
    {
        DelayForClick = 500;
        this.Position = Position;
    }

    public Point Position { get; set; }
    public int DelayForClick { get; set; }
}