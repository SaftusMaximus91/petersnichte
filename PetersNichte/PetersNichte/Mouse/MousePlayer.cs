using System.Runtime.InteropServices;

namespace WinFormsApp1;

public class MousePlayer
{
    private const int MOUSEEVENTF_MOVE = 0x0001;
    private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const int MOUSEEVENTF_LEFTUP = 0x0004;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const int MOUSEEVENTF_RIGHTUP = 0x0010;
    private const int MOUSEEVENTF_WHEEL = 0x0800;
    private double speedUp = 0.8;

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    public void PlayAndResetPosition(List<MouseEvent> events)
    {
        var mousePosition = Cursor.Position;
        Play(events);
        SetCursorPos(mousePosition.X, mousePosition.Y);
    }

    /// <summary>
    ///     Spielt eine Liste von Mausereignissen ab.
    /// </summary>
    /// <param name="events">Die Liste der aufgezeichneten Mausereignisse.</param>
    public void Play(List<MouseEvent> events, double speedFactor = 1)
    {
        long previousTime = 0;

        foreach (var mouseEvent in events)
        {
            mouseEvent.Timestamp = Convert.ToInt64(mouseEvent.Timestamp * speedFactor);
            // Zeitdifferenz zwischen den Ereignissen simulieren
            Thread.Sleep((int)(mouseEvent.Timestamp - previousTime));
            previousTime = mouseEvent.Timestamp;

            // Setze den Cursor auf die gespeicherte Position
            SetCursorPos(mouseEvent.X, mouseEvent.Y);

            switch (mouseEvent.Action)
            {
                case "LeftMouseDown":
                    // Linksklick halten
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    break;

                case "LeftMouseUp":
                case "LeftClick":
                    // Linksklick loslassen
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;

                case "RightMouseDown":
                    // Rechtsklick halten
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    break;

                case "RightMouseUp":
                case "RightClick":
                    // Rechtsklick loslassen
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;

                case "Drag":
                    // Mausbewegungen während Drag automatisch durch SetCursorPos simuliert
                    break;

                case "Move":
                    // Mausbewegungen automatisch durch SetCursorPos simuliert
                    break;

                case "Wheel":
                    // Mausradbewegung simulieren
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, mouseEvent.WheelDelta, 0);
                    break;
            }
        }
    }
}