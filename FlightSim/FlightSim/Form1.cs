using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightSim
{
    public partial class Form1 : Form
    {
        const double g = 9.81;
        const double dt = 0.01;
        double t = 0, a;
        double y = 0, x = 0;
        double y0, v0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Stop();
            else timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            y0 = (double)edHeight.Value;
            v0 = (double)edVelocity.Value;
            a = (double)edAngle.Value;

            var sizes = GetSize();
            var size1 = Convert.ToInt32(sizes.Item1)+0.5;
            var size2 = Convert.ToInt32(sizes.Item2)+0.5;

            graph.ChartAreas[0].AxisX.Maximum = size1;
            graph.ChartAreas[0].AxisY.Maximum = size2;
            
            x = 0;
            y = y0;
            t = 0;

            graph.Series[0].Points.Clear();
            graph.Series[0].Points.AddXY(0, y0);

            timer1.Start();
        }

        private Tuple<double, double, double> GetSize()
        {
            double sina = Math.Sin(a * Math.PI / 180);
            double ymax = y0 + (v0 * v0 * sina * sina / 2 / g);
            double tpol = (v0 * sina + Math.Sqrt(v0 * v0 * sina * sina + 2 * g * y0))/g;
            double xmax = v0 * tpol * Math.Cos(a *Math.PI/180);
            return Tuple.Create(xmax, ymax, tpol);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            x = v0 * Math.Cos(a * Math.PI / 180) * t;
            y = y0 + v0 * Math.Sin(a * Math.PI / 180) * t - g * t * t / 2;
            graph.Series[0].Points.AddXY(x, y);
            label4.Text = Convert.ToString(t);
            if (y <= 0) timer1.Stop();
        }
    }
}
