using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лр21
{
    public partial class Form1 : Form
    {
        // Объявление таймера для обновления времени на аналоговых часах
        private Timer clockTimer = new Timer();
        // Флаг, определяющий, работают ли часы в данный момент
        private bool isClockRunning = false;
        // Радиус циферблата
        private const int clockRadius = 80;
        // Длина часовой стрелки
        private const int hourHandLength = 50;
        // Длина минутной стрелки
        private const int minuteHandLength = 70;

        public Form1()
        {
            InitializeComponent();
            // Установка интервала таймера для обновления времени на аналоговых часах
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
        }

        // Обработчик события перерисовки формы
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Рисование графика функции
            DrawGraph(e.Graphics);
            // Рисование аналоговых часов
            DrawAnalogClock(e.Graphics);
        }

        // Рисование графика функции
        private void DrawGraph(Graphics g)
        {
            Pen graphPen = new Pen(Color.Blue, 2);
            for (int x = -ClientSize.Width / 2; x <= ClientSize.Width / 2; x++)
            {
                float y = -2 * x * x + 3 * x * 3;
                g.FillRectangle(Brushes.Red, ClientSize.Width / 2 + x, ClientSize.Height / 2 - y, 2, 2);
            }
        }

        // Рисование аналоговых часов
        private void DrawAnalogClock(Graphics g)
        {
            // Определение текущего времени
            DateTime currentTime = DateTime.Now;
            int centerX = ClientSize.Width / 2;
            int centerY = ClientSize.Height / 2;

            // Рисование круга для циферблата
            g.DrawEllipse(Pens.Black, centerX - clockRadius, centerY - clockRadius, 2 * clockRadius, 2 * clockRadius);

            // Рисование цифр на циферблате
            for (int i = 1; i <= 12; i++)
            {
                double angle = 2 * Math.PI * i / 12 - Math.PI / 2;
                int x = (int)(centerX + (clockRadius - 15) * Math.Cos(angle));
                int y = (int)(centerY + (clockRadius - 15) * Math.Sin(angle));
                g.DrawString(i.ToString(), Font, Brushes.Black, x, y);
            }

            // Рисование часовой стрелки
            double hourAngle = 2 * Math.PI * (currentTime.Hour % 12 + currentTime.Minute / 60.0) / 12 - Math.PI / 2;
            int hourX = (int)(centerX + hourHandLength * Math.Cos(hourAngle));
            int hourY = (int)(centerY + hourHandLength * Math.Sin(hourAngle));
            g.DrawLine(Pens.Black, centerX, centerY, hourX, hourY);

            // Рисование минутной стрелки
            double minuteAngle = 2 * Math.PI * (currentTime.Minute + currentTime.Second / 60.0) / 60 - Math.PI / 2;
            int minuteX = (int)(centerX + minuteHandLength * Math.Cos(minuteAngle));
            int minuteY = (int)(centerY + minuteHandLength * Math.Sin(minuteAngle));
            g.DrawLine(Pens.Black, centerX, centerY, minuteX, minuteY);

            // Рисование секундной стрелки
            double secondAngle = 2 * Math.PI * currentTime.Second / 60 - Math.PI / 2;
            int secondX = (int)(centerX + minuteHandLength * Math.Cos(secondAngle));
            int secondY = (int)(centerY + minuteHandLength * Math.Sin(secondAngle));
            g.DrawLine(Pens.Red, centerX, centerY, secondX, secondY);
        }

        // Обработчик события срабатывания таймера
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Обновление времени на аналоговых часах
            Invalidate();
        }

        // Обработчик нажатия кнопки
        private void button1_Click(object sender, EventArgs e)
        {
            // Запуск или остановка таймера для обновления времени на аналоговых часах
            if (isClockRunning)
            {
                clockTimer.Stop();
                isClockRunning = false;
                button1.Text = "Start";
            }
            else
            {
                clockTimer.Start();
                isClockRunning = true;
                button1.Text = "Stop";
            }
        }
    }
}
