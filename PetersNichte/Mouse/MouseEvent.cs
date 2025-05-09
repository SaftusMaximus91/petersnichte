public class MouseEvent
{
    public MouseEvent(string action, int x, int y, long timestamp, int wheelDelta = 0)
    {
        Action = action;
        X = x;
        Y = y;
        Timestamp = timestamp;
        WheelDelta = wheelDelta;
    }

    public string Action { get; set; } // Typ der Aktion: "Move", "LeftClick", "RightClick", "Wheel"
    public int X { get; set; } // X-Koordinate
    public int Y { get; set; } // Y-Koordinate
    public long Timestamp { get; set; } // Zeitstempel in Millisekunden
    public int WheelDelta { get; set; } // Mausrad-Delta (nur für "Wheel")
}