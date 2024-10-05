using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace glWinForm1
{
    public partial class RenderControl : OpenGL
    {
        private float[] polygonVertices = new float[]
        {
            2, 1,
            1, 2,
            1, 3,
            2, 5,
            3, 5,
            4, 3
        };

        public RenderControl()
        {
            InitializeComponent();
            Render += RenderControl_Render; // Підписка на подію рендерингу
            this.Resize += RenderControl_Resize; // Додаємо обробник зміни розміру
        }

        private void RenderControl_Render(object sender, EventArgs e)
        {
            // Основний метод відрисовки
            glClearColor(0.5f, 0.5f, 0.5f, 1.0f); // Установка кольору фону
            glClear(GL_COLOR_BUFFER_BIT); // Очистка буфера кольору

            // Настройка ортографічної проекції
            glLoadIdentity();
            glViewport(0, 0, Width, Height);
            glOrtho(-1, 8, -2, 6, -1, 1); // Установка анізотропної системи координат

            DrawGrid(); // Рисуем сетку
            DrawPolygon(); // Рисуем многоугольник
            DrawPoints(); // Рисуем точки
        }

        private void RenderControl_Resize(object sender, EventArgs e)
        {
            // Перемалювати сцену при зміні розміру вікна
            Invalidate();
        }

        private void DrawGrid()
        {
            glColor(Color.White);
            glLineWidth(1.0f); // Товщина лінії для сітки
            DrawLineGrid(); // Рисуем сетку

            // Рисуем ось X
            glColor(Color.Red); // Колір для осі X
            DrawLine(-1, 0, 8, 0); // Осі X

            // Рисуем стрелку для осі X
            DrawArrow(8, 0, 7.8f, 0.2f);
            DrawArrow(8, 0, 7.8f, -0.2f);

            // Рисуем ось Y
            glColor(Color.Blue); // Колір для осі Y
            DrawLine(0, -2, 0, 6); // Осі Y

            // Рисуем стрелку для осі Y
            DrawArrow(0, 6, 0.2f, 5.8f);
            DrawArrow(0, 6, -0.2f, 5.8f);

            // Нумерація осей
            DrawAxisLabels();
        }

        private void DrawAxisLabels()
        {
            // Установка кольору тексту
            Color labelColor = Color.White;

            // Підписи для осі X
            for (float x = -1; x <= 8; x++)
            {
                if (x % 1 == 0) // Рисуем только для целых чисел
                {
                    DrawText(x, -0.2f, x.ToString(), labelColor);
                }
            }

            // Підписи для осі Y
            for (float y = -2; y <= 6; y++)
            {
                if (y % 1 == 0) // Рисуем только для целых чисел
                {
                    DrawText(-0.3f, y, y.ToString(), labelColor);
                }
            }
        }

        private void DrawText(float x, float y, string text, Color color)
        {
            // Настройка шрифта и отрисовка текста
            using (Graphics g = this.CreateGraphics())
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                using (Brush brush = new SolidBrush(color))
                {
                    g.DrawString(text, new Font("Arial", 8), brush, x, y);
                }
            }
        }

        private void DrawPolygon()
        {
            glColor(Color.Black);
            glEnableClientState(GL_VERTEX_ARRAY); // Активируем массив вершин

            // Получаем указатель на массив вершин
            GCHandle handle = GCHandle.Alloc(polygonVertices, GCHandleType.Pinned);
            try
            {
                IntPtr vertexPointer = handle.AddrOfPinnedObject();
                glVertexPointer(2, GL_FLOAT, 0, vertexPointer); // Указываем OpenGL на массив вершин
                glDrawArrays(GL_LINE_LOOP, 0, polygonVertices.Length / 2); // Рисуем многоугольник
            }
            finally
            {
                handle.Free(); // Освобождаем указатель
            }

            glDisableClientState(GL_VERTEX_ARRAY); // Деактивируем массив вершин
        }

        private void DrawPoints()
        {
            glColor(Color.Black);
            glPointSize(10.0f); // Установка размера точки
            glBegin(GL_POINTS); // Начало рисования точек

            // Координати точок
            glVertex2f(2, 1);  // Точка 1 (2, 1)
            glVertex2f(1, 2);  // Точка 2 (1, 2)
            glVertex2f(1, 3);  // Точка 3 (1, 3)
            glVertex2f(2, 5);  // Точка 4 (2, 5)
            glVertex2f(3, 5);  // Точка 5 (3, 5)
            glVertex2f(4, 3);  // Точка 6 (4, 3)

            glEnd(); // Завершення малювання точок
        }

        private void DrawLine(float x1, float y1, float x2, float y2)
        {
            glBegin(GL_LINES);
            glVertex2f(x1, y1);
            glVertex2f(x2, y2);
            glEnd();
        }

        private void DrawArrow(float x1, float y1, float x2, float y2)
        {
            glBegin(GL_LINES);
            glVertex2f(x1, y1); // Кінець
            glVertex2f(x2, y2); // Стрілка
            glEnd();
        }

        private void DrawLineGrid()
        {
            glBegin(GL_LINES); // Початок малювання сітки

            // Вертикальні лінії
            for (float x = -1; x <= 8; x++)
            {
                glVertex2f(x, -2);
                glVertex2f(x, 6);
            }

            // Горизонтальні лінії
            for (float y = -2; y <= 6; y++)
            {
                glVertex2f(-1, y);
                glVertex2f(8, y);
            }

            glEnd(); // Завершення малювання сітки
        }
    }
}
