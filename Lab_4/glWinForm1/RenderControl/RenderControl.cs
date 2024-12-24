using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace glWinForm1
{
    public partial class RenderControl : OpenGL
    {
        private Point? segmentStart = null;
        private Point? segmentEnd = null;
        private bool isDrawingSegment = false;

        public RenderControl()
        {
            InitializeComponent();
            Render += RenderControl_Render;
            this.Resize += RenderControl_Resize;
            this.MouseDown += RenderControl_MouseDown;
            this.MouseUp += RenderControl_MouseUp;
            this.Paint += RenderControl_Paint;
        }

        private void RenderControl_Render(object sender, EventArgs e)
        {
            glClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            glClear(GL_COLOR_BUFFER_BIT);

            glLoadIdentity();
            glViewport(0, 0, Width, Height);
            glOrtho(-10, 10, -10, 10, -1, 1);

            DrawHyperbola();
            DrawParabola();

            if (segmentStart.HasValue && segmentEnd.HasValue)
            {
                DrawSegment(segmentStart.Value, segmentEnd.Value);
                FindAndDrawIntersections();
            }
        }

        private void RenderControl_Paint(object sender, PaintEventArgs e)
        {
            if (segmentStart.HasValue && segmentEnd.HasValue)
            {
                DrawSegmentData(e.Graphics, segmentStart.Value, segmentEnd.Value);
            }
        }

        private void RenderControl_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void RenderControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                segmentStart = e.Location;
                isDrawingSegment = true;
            }
        }

        private void RenderControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isDrawingSegment)
            {
                segmentEnd = e.Location;
                isDrawingSegment = false;
                Invalidate();
            }
        }

        private void DrawHyperbola()
        {
            glColor(Color.Red);
            glBegin(GL_LINE_STRIP);
            for (float x = -10; x < 10; x += 0.1f)
            {
                if (Math.Abs(x) < 0.1f) continue; // Уникаємо x = 0
                float y = 1.0f / x;
                glVertex2f(x, y);
            }
            glEnd();
        }

        private void DrawParabola()
        {
            glColor(Color.Green);
            glBegin(GL_LINE_STRIP);
            for (float t = -10; t <= 10; t += 0.1f)
            {
                float x = t;
                float y = t * t;
                glVertex2f(x, y);
            }
            glEnd();
        }

        private void DrawSegment(Point start, Point end)
        {
            glColor(Color.Blue);
            glLineWidth(2.0f);
            glBegin(GL_LINES);
            glVertex2f(NormalizeX(start.X), NormalizeY(start.Y));
            glVertex2f(NormalizeX(end.X), NormalizeY(end.Y));
            glEnd();
        }

        private void FindAndDrawIntersections()
        {
            if (segmentStart.HasValue && segmentEnd.HasValue)
            {
                // Конвертуємо координати пікселів у координати OpenGL
                PointF p1 = new PointF(NormalizeX(segmentStart.Value.X), NormalizeY(segmentStart.Value.Y));
                PointF p2 = new PointF(NormalizeX(segmentEnd.Value.X), NormalizeY(segmentEnd.Value.Y));

                // Знаходимо перетини з параболою
                var parabolaIntersections = FindIntersectionsWithParabola(p1, p2);
                // Знаходимо перетини з гіперболою
                var hyperbolaIntersections = FindIntersectionsWithHyperbola(p1, p2);

                // Малюємо точки перетину
                glColor(Color.Magenta);
                glPointSize(5.0f);
                glBegin(GL_POINTS);
                foreach (var point in parabolaIntersections)
                {
                    glVertex2f(point.X, point.Y);
                }
                foreach (var point in hyperbolaIntersections)
                {
                    glVertex2f(point.X, point.Y);
                }
                glEnd();
            }
        }

        private List<PointF> FindIntersectionsWithParabola(PointF p1, PointF p2)
        {
            List<PointF> intersections = new List<PointF>();

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            // Підставляємо параметричні рівняння відрізка в рівняння параболи y = x^2
            // y1 + t*dy = (x1 + t*dx)^2
            // Розкриваємо і приводимо до квадратного рівняння: (dx^2) t^2 + (2x1 dx - dy) t + (x1^2 - y1) = 0

            float a = dx * dx;
            float b = 2 * p1.X * dx - dy;
            float c = p1.X * p1.X - p1.Y;

            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                // Перетинів немає
                return intersections;
            }

            float sqrtDiscriminant = (float)Math.Sqrt(discriminant);
            float t1 = (-b + sqrtDiscriminant) / (2 * a);
            float t2 = (-b - sqrtDiscriminant) / (2 * a);

            // Перевіряємо, чи t належить [0, 1]
            if (t1 >= 0 && t1 <= 1)
            {
                float x = p1.X + t1 * dx;
                float y = p1.Y + t1 * dy;
                intersections.Add(new PointF(x, y));
            }

            if (t2 >= 0 && t2 <= 1 && discriminant > 0)
            {
                float x = p1.X + t2 * dx;
                float y = p1.Y + t2 * dy;
                intersections.Add(new PointF(x, y));
            }

            return intersections;
        }

        private List<PointF> FindIntersectionsWithHyperbola(PointF p1, PointF p2)
        {
            List<PointF> intersections = new List<PointF>();

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            // Рівняння гіперболи y = 1/x
            // Підставляємо параметричні рівняння відрізка:
            // y1 + t*dy = 1 / (x1 + t*dx)
            // Перетворюємо: (y1 + t*dy)(x1 + t*dx) = 1
            // y1*x1 + t*(y1*dx + x1*dy) + t^2*dx*dy - 1 = 0

            float A = dx * dy;
            float B = p1.Y * dx + p1.X * dy; // Виправлено: використання p1.X та p1.Y
            float C = p1.X * p1.Y - 1;       // Виправлено: використання p1.X та p1.Y

            float discriminant = B * B - 4 * A * C;

            if (Math.Abs(A) < 1e-6f && Math.Abs(B) > 1e-6f)
            {
                // Лінійне рівняння
                float t = -C / B;
                if (t >= 0 && t <= 1)
                {
                    float x = p1.X + t * dx;
                    if (Math.Abs(x) > 1e-6f)
                    {
                        float y = 1 / x;
                        intersections.Add(new PointF(x, y));
                    }
                }
            }
            else if (discriminant >= 0)
            {
                float sqrtDiscriminant = (float)Math.Sqrt(discriminant);
                float t1 = (-B + sqrtDiscriminant) / (2 * A);
                float t2 = (-B - sqrtDiscriminant) / (2 * A);

                if (t1 >= 0 && t1 <= 1)
                {
                    float x = p1.X + t1 * dx;
                    if (Math.Abs(x) > 1e-6f)
                    {
                        float y = 1 / x;
                        intersections.Add(new PointF(x, y));
                    }
                }

                if (t2 >= 0 && t2 <= 1 && discriminant > 0)
                {
                    float x = p1.X + t2 * dx;
                    if (Math.Abs(x) > 1e-6f)
                    {
                        float y = 1 / x;
                        intersections.Add(new PointF(x, y));
                    }
                }
            }

            return intersections;
        }

        private void DrawSegmentData(Graphics g, Point start, Point end)
        {
            float x1 = NormalizeX(start.X);
            float y1 = NormalizeY(start.Y);
            float x2 = NormalizeX(end.X);
            float y2 = NormalizeY(end.Y);

            string data = $"Start: ({x1:F2}, {y1:F2})\nEnd: ({x2:F2}, {y2:F2})";

            float midX = (start.X + end.X) / 2;
            float midY = (start.Y + end.Y) / 2;

            g.DrawString(data, new Font("Arial", 10), Brushes.Black, new PointF(midX, midY - 20));
        }

        private float NormalizeX(int x)
        {
            return (float)x / Width * 20 - 10;
        }

        private float NormalizeY(int y)
        {
            return 10 - (float)y / Height * 20;
        }
    }
}
