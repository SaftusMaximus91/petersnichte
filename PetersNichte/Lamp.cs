using System.Drawing.Drawing2D;

namespace WinFormsApp1;

public class LampControl : Control
{
    private readonly Color offColor = Color.Gray;
    private readonly Color onColor = Color.Red;
    private bool isOn; // Status der Lampe (Ein/Aus)

    public LampControl()
    {
        Size = new Size(20, 20); // Standardgröße der Lampe
        DoubleBuffered = true; // Verhindert Flackern beim Zeichnen
    }

    // Umschalten der Farbe
    public void Toggle()
    {
        isOn = !isOn;
        Invalidate(); // Löst das Neuzeichnen aus
    }

    // Zeichne die Lampe
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using (Brush brush = new SolidBrush(isOn ? onColor : offColor))
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(brush, 0, 0, Width, Height);
        }
    }
}