using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class ToggleSwitch : CheckBox
{
    [Category("Appearance")]
    public Color OnBackColor { get; set; } = Color.DodgerBlue;

    [Category("Appearance")]
    public Color OffBackColor { get; set; } = Color.Gainsboro;

    [Category("Appearance")]
    public Color ToggleColor { get; set; } = Color.White;

    [Category("Appearance")]
    public int BorderRadius { get; set; } = 16;

    [Category("Appearance")]
    public int BorderSize { get; set; } = 2;

    [Category("Appearance")]
    public Color BorderColor { get; set; } = Color.DarkGray;

    [Category("Appearance")]
    public bool SolidStyle { get; set; } = true;

    private int _togglePosition;
    private Timer _animationTimer;

    public ToggleSwitch()
    {
        this.MinimumSize = new Size(45, 22);
        this.AutoSize = false;

        _togglePosition = 2;

        _animationTimer = new Timer();
        _animationTimer.Interval = 1;
        _animationTimer.Tick += AnimationTick;
    }

    private void AnimationTick(object sender, EventArgs e)
    {
        int target = this.Checked ? this.Width - this.Height + 2 : 2;
        if (_togglePosition < target)
        {
            _togglePosition += 2; // سرعة الانزلاق عند التشغيل
            if (_togglePosition > target) _togglePosition = target;
            this.Invalidate();
        }
        else if (_togglePosition > target)
        {
            _togglePosition -= 2;
            if (_togglePosition < target) _togglePosition = target;
            this.Invalidate();
        }
        else
        {
            _animationTimer.Stop();
        }
    }

    protected override void OnCheckedChanged(EventArgs e)
    {
        _animationTimer.Start();
        base.OnCheckedChanged(e);
    }

    /*    protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;

            // رسم الخلفية بالـ border radius
            using (GraphicsPath path = GetRoundedRectangle(rect, BorderRadius))
            using (Brush backBrush = new SolidBrush(this.Checked ? OnBackColor : OffBackColor))
            {
                g.FillPath(backBrush, path);

                if (BorderSize > 0)
                {
                    using (Pen pen = new Pen(BorderColor, BorderSize))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            // رسم الـ toggle (الدائرة)
            int toggleSize = rect.Height - 4;
            using (Brush toggleBrush = new SolidBrush(ToggleColor))
            {
                g.FillEllipse(toggleBrush, new Rectangle(_togglePosition, 2, toggleSize, toggleSize));
            }
        }*/

    protected override void OnPaint(PaintEventArgs pevent)
    {
        Graphics g = pevent.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = this.ClientRectangle;

        // تحديث Region لجعل الحواف الخارجية دائرية
        using (GraphicsPath path = GetRoundedRectangle(rect, BorderRadius))
        {
            this.Region = new Region(path);

            // رسم الخلفية
            using (Brush backBrush = new SolidBrush(this.Checked ? OnBackColor : OffBackColor))
            {
                g.FillPath(backBrush, path);
            }

            // رسم الحدود
            if (BorderSize > 0)
            {
                using (Pen pen = new Pen(BorderColor, BorderSize))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        // رسم الـ toggle (الدائرة)
        int toggleSize = rect.Height - 4;
        using (Brush toggleBrush = new SolidBrush(ToggleColor))
        {
            g.FillEllipse(toggleBrush, new Rectangle(_togglePosition, 2, toggleSize, toggleSize));
        }
    }


    private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        float curveSize = radius * 2F;

        path.StartFigure();
        path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
        path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
        path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
        path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
        path.CloseFigure();

        return path;
    }
}
