namespace WinFormsApp1;

public class LustGoddess : JobConfig
{
    public readonly double factor = 0.97;
    public readonly string TemplateDirPath = @"C:\Users\schmi\Pictures\AutoClicker\LustGoddess\Templates";
    public bool openChests;
    public bool openDailyChests;
    public bool skipLedgy;


    public LustGoddess()
    {
        Name = "Lust Goddess";
        ScreenDefaultStart = new Point(189, 85);
        ScreenDefaultEnd = new Point(1727, 916);
        hasOptions = true;
    }

    private List<JobInfo> FillWaitJobs()
    {
        List<JobInfo> WaitJobs = new();
        if (!skipLedgy)
            WaitJobs.Add(new JobInfo("Warten wegen Ledgy Truhe", GetTemplatePath("ledgylock.png"), 0, false, false,
                factor));
        return WaitJobs;
    }

    private List<JobInfo> FillLeftClickJobs()
    {
        List<JobInfo> LeftClickJobs = new();
        if (turns >= 5)
        {
            LeftClickJobs.Add(GetJobLeaveHubschrauberRennenWihtoutAction("hubschrauberNach.png"));
            LeftClickJobs.Add(GetJobLeaveHubschrauberRennenWihtoutAction("HubschrauberKaufenQuestversuche.png"));
            LeftClickJobs.Add(new JobInfo("Tägliche gewinn Kiste öffnen", GetTemplatePath("fight.png"), 1, true, false,
                factor, false, 245, 122));
            LeftClickJobs.Add(
                new JobInfo("Truhe öffnen", GetTemplatePath("hubschrauber.png"), 10, false, false, factor));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("hubschrauberAbholen.png"), 10, false, false,
                factor));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("HubschrauberQuest.png"), 70, false, false,
                0.90));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("HubschrauberQuest1.png"), 70, false, false,
                0.90));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("HubschrauberQuest2.png"), 70, false, false,
                0.90));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("HubschrauberQuest3.png"), 70, false, false,
                0.90));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("HubschrauberQuest4.png"), 70, false, false,
                0.90));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("HubschrauberQuest5.png"), 70, false, false,
                0.90));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("HubschrauberStart.png"), 10, false, false,
                factor));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("HubschrauberQuestStart.png"), 5, false,
                false, factor));
        }
        else
        {
            LeftClickJobs.Add(new JobInfo("Kampf starten", GetTemplatePath("fight.png"), 99, false, false, factor, true,
                null, null, true));
        }

        if (openDailyChests)
            LeftClickJobs.Add(new JobInfo("Tägliche gewinn Kiste öffnen", GetTemplatePath("dailychestfull.png"), 1,
                true, false, factor, false, 1550, 547));
        //LeftClickJobs.Add(new JobInfo("Ledgy truhe überspringen", @"C:\Users\schmi\Pictures\AutoClicker\Vorlagen\ohneledgybelohnungfortfahren.png", 0, true, false));
        if (openChests)
        {
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("openChest.png"), 10, false, false, factor));
            LeftClickJobs.Add(new JobInfo("Truhe öffnen", GetTemplatePath("openChest2.png"), 10, false, false, factor));
        }

        LeftClickJobs.Add(new JobInfo("Truhe öffenen Ergebnis wegklicken", GetTemplatePath("clickanywhere.png"), 2,
            false, false, factor));
        LeftClickJobs.Add(new JobInfo("Kampf zuende und fortfahren", GetTemplatePath("clickanywherefightend.png"), 11,
            false, false, factor));

        if (skipLedgy)
            LeftClickJobs.Add(new JobInfo("Ledgy skip", GetTemplatePath("ledgyskip.png"), 99, false, false, factor));
        LeftClickJobs.Add(new JobInfo("Truhe hinzufügen Werbung entfernen",
            GetTemplatePath("addChestAdd.png"), 3, false, false, factor));
        return LeftClickJobs;
    }

    private JobInfo GetJobLeaveHubschrauberRennenWihtoutAction(string templatePath)
    {
        var job = new JobInfo("Tägliche gewinn Kiste öffnen", GetTemplatePath(templatePath), 1, true, false, factor,
            false, 254, 119, false, true);
        var clickActions = new List<AdditonalClickAction>();
        clickActions.Add(new AdditonalClickAction(new Point(1580, 840), 1000));
        job.ClickActions = clickActions;
        return job;
    }

    private string GetTemplatePath(string fileName)
    {
        return Path.Combine(TemplateDirPath, fileName);
    }

    public override List<JobInfo> GetLeftclickJobs()
    {
        return FillLeftClickJobs();
    }

    public override List<JobInfo> GetWaitJobs()
    {
        return FillWaitJobs();
    }
}