using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinFormsApp1;

public class MouseHook
{
    private const int WH_MOUSE_LL = 14;

    private readonly Stopwatch stopwatch = new();

    private IntPtr _hookID = IntPtr.Zero;
    private LowLevelMouseProc _proc;

    private bool isLeftButtonDown;
    private bool isRightButtonDown;

    public int XOffSet { get; set; }
    public int YOffSet { get; set; }
    public List<MouseEvent> MouseEvents { get; } = new();

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc callback, IntPtr hInstance,
        uint threadId);

    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hHook);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    public void Start()
    {
        _proc = HookCallback;
        _hookID = SetWindowsHookEx(WH_MOUSE_LL, _proc,
            GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
        stopwatch.Restart();
    }

    public void Stop()
    {
        UnhookWindowsHookEx(_hookID);
        stopwatch.Stop();
    }

    private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            var mouseInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            var timestamp = stopwatch.ElapsedMilliseconds;

            switch ((MouseMessages)wParam)
            {
                case MouseMessages.WM_LBUTTONDOWN:
                    if (!isLeftButtonDown)
                    {
                        isLeftButtonDown = true;
                        MouseEvents.Add(new MouseEvent("LeftMouseDown", mouseInfo.pt.x - XOffSet,
                            mouseInfo.pt.y - YOffSet, timestamp));
                    }

                    break;

                case MouseMessages.WM_LBUTTONUP:
                    if (isLeftButtonDown)
                    {
                        isLeftButtonDown = false;
                        MouseEvents.Add(new MouseEvent("LeftMouseUp", mouseInfo.pt.x - XOffSet,
                            mouseInfo.pt.y - YOffSet, timestamp));
                    }

                    break;

                case MouseMessages.WM_RBUTTONDOWN:
                    if (!isRightButtonDown)
                    {
                        isRightButtonDown = true;
                        MouseEvents.Add(new MouseEvent("RightMouseDown", mouseInfo.pt.x - XOffSet,
                            mouseInfo.pt.y - YOffSet, timestamp));
                    }

                    break;

                case MouseMessages.WM_RBUTTONUP:
                    if (isRightButtonDown)
                    {
                        isRightButtonDown = false;
                        MouseEvents.Add(new MouseEvent("RightMouseUp", mouseInfo.pt.x - XOffSet,
                            mouseInfo.pt.y - YOffSet, timestamp));
                    }

                    break;

                case MouseMessages.WM_MOUSEMOVE:
                    if (isLeftButtonDown)
                    {
                        // Wenn die linke Maustaste gedrückt ist, während sich die Maus bewegt, handelt es sich um "Drag".
                        MouseEvents.Add(new MouseEvent("Drag", mouseInfo.pt.x - XOffSet, mouseInfo.pt.y - YOffSet,
                            timestamp));
                    }

                    // Normale Mausbewegung
                    //    MouseEvents.Add(new MouseEvent("Move", mouseInfo.pt.x - XOffSet, mouseInfo.pt.y - YOffSet, timestamp));
                    break;

                case MouseMessages.WM_MOUSEWHEEL:
                    // Extrahiere das Mausrad-Drehen (high-order word des mouseData)
                    int wheelDelta = (short)((mouseInfo.mouseData >> 16) & 0xFFFF);
                    MouseEvents.Add(new MouseEvent("Wheel", mouseInfo.pt.x - XOffSet, mouseInfo.pt.y - YOffSet,
                        timestamp, wheelDelta));
                    break;
            }
        }

        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    // CSV-Speicher- und Ladefunktionen erweitern
    public void SaveToCsv(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Action,X,Y,Timestamp,WheelDelta");
            foreach (var mouseEvent in MouseEvents)
                writer.WriteLine(
                    $"{mouseEvent.Action},{mouseEvent.X},{mouseEvent.Y},{mouseEvent.Timestamp},{mouseEvent.WheelDelta}");
        }
    }

    public void LoadFromCsv(string filePath)
    {
        MouseEvents.Clear();
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath).Skip(1);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 5)
                {
                    var action = parts[0];
                    var x = int.Parse(parts[1]) + XOffSet;
                    var y = int.Parse(parts[2]) + YOffSet;
                    var timestamp = long.Parse(parts[3]);
                    var wheelDelta = int.Parse(parts[4]);
                    if (action == "LeftClick")
                    {
                        MouseEvents.Add(new MouseEvent("LeftMouseDown", x, y, timestamp, wheelDelta));
                        MouseEvents.Add(new MouseEvent("LeftMouseUp", x, y, timestamp, wheelDelta));
                    }
                    else
                    {
                        MouseEvents.Add(new MouseEvent(action, x, y, timestamp, wheelDelta));
                    }
                }
            }
        }
    }

    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    private enum MouseMessages
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEWHEEL = 0x020A
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}