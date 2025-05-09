namespace WinFormsApp1;

public class JobInfo
{
    public JobInfo(string name, string templatePath, int prio, bool caputreScreenshotByIf, bool caputreScreenshotByElse,
        double uebereinstimmungsfaktor, bool useimagefinterposition = true, int? fixClickPosX = null,
        int? fixClickPosY = null, bool raiseTurns = false, bool resetTurns = false)
    {
        ClickActions = new List<AdditonalClickAction>();
        JobName = name;
        Uebereinstimmungsfaktor = uebereinstimmungsfaktor;
        TemplatePath = templatePath;
        Priority = prio;
        CaptureScreenshotByIf = caputreScreenshotByIf;
        CaptureScreenshotByElse = caputreScreenshotByElse;
        UseImageFinterPosition = useimagefinterposition;
        FixClickPosX = fixClickPosX;
        FixClickPosY = fixClickPosY;
        RaiseTurns = raiseTurns;
        ResetTurns = resetTurns;
    }

    public string JobName { get; set; }
    public string TemplatePath { get; set; }
    public int Priority { get; set; }
    public bool CaptureScreenshotByIf { get; set; }
    public bool CaptureScreenshotByElse { get; set; }
    public bool UseImageFinterPosition { get; set; }
    public int? FixClickPosX { get; set; }
    public int? FixClickPosY { get; set; }
    public double Uebereinstimmungsfaktor { get; set; }
    public List<AdditonalClickAction> ClickActions { get; set; }
    public bool RaiseTurns { get; set; }

    public bool ResetTurns { get; set; }
}