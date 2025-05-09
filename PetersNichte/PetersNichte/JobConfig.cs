namespace WinFormsApp1;

public abstract class JobConfig
{
    public bool hasOptions;
    public string Name;
    public Point ScreenDefaultEnd = new(1920, 1080);
    public Point ScreenDefaultStart = new(0, 0);
    public int turns = 0;
    public List<JobInfo> Jobs => GetLeftclickJobs();
    public abstract List<JobInfo> GetLeftclickJobs();
    public abstract List<JobInfo> GetWaitJobs();
}