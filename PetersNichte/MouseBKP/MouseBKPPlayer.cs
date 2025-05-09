using System.Runtime.InteropServices;

public class MousePlayer
{
    private const int MOUSEEVENTF_MOVE = 0x0001;
    private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const int MOUSEEVENTF_LEFTUP = 0x0004;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const int MOUSEEVENTF_RIGHTUP = 0x0010;
    private const int MOUSEEVENTF_WHEEL = 0x0800;

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public void Play(List<MouseEvent> events)
    {
        long previousTime = 0;
        foreach (var mouseEvent in events)
        {
            Thread.Sleep((int)(mouseEvent.Timestamp - previousTime));
            previousTime = mouseEvent.Timestamp;

            // Setze Mausposition
            Cursor.Position = new Point(mouseEvent.X, mouseEvent.Y);

            switch (mouseEvent.Action)
            {
                case "LeftClick":
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case "RightClick":
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                case "Wheel":
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, mouseEvent.WheelDelta, 0);
                    break;
            }
        }
    }

    public void PlayAndResetPosition(List<MouseEvent> events)
    {
        long previousTime = 0;

        var mousePosition = Cursor.Position;

        foreach (var mouseEvent in events)
        {
            Thread.Sleep((int)((mouseEvent.Timestamp - previousTime)*0.75));
            previousTime = mouseEvent.Timestamp;

            // Setze Mausposition
            Cursor.Position = new Point(mouseEvent.X, mouseEvent.Y);

            switch (mouseEvent.Action)
            {
                case "LeftClick":
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case "RightClick":
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                case "Wheel":
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, mouseEvent.WheelDelta, 0);
                    break;
            }
        }

        Cursor.Position = new Point(mousePosition.X, mousePosition.Y);
    }
}