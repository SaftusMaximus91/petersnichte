namespace WinFormsApp1;

internal static class Program
{
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
      
    }
}