using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gravity;
using System.Drawing.Drawing2D;

namespace GravityUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        double scale;
        Vector3 translate;
        
        DateTime lastTick;
        double timeScale;

        IBody[] bodies = new[]
        {
            SolarSystem.Sun,
            SolarSystem.Mercury,
            SolarSystem.Venus,
            SolarSystem.Earth,
            SolarSystem.Mars,
            SolarSystem.Jupiter,
            SolarSystem.Saturn,
            SolarSystem.Uranus,
            SolarSystem.Neptune,
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            SetScale();

            lastTick = DateTime.UtcNow;
            timeScale = 15 * 60 * 60 * 24; // 1s -> 1d

            gameTimer.Interval = 5;
            gameTimer.Enabled = true;
        }

        private void SetScale()
        {
            //var max = bodies.Max(b => b.Position.Length);
            var max = SolarSystem.Jupiter.Position.Length;
            scale = (Math.Min(pictureBox1.Width, pictureBox1.Height) / 2) / max * 0.95;
            translate = new Vector3(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);
        }

        private void Draw(Graphics gfx, Pen pen, IBody body, float radius)
        {
            var pos = body.Position * scale + translate;

            var reference = SolarSystem.Earth;
            if (body != reference)
            {
                var refpos = reference.Position * scale + translate;
                gfx.DrawLine(Pens.Gray, (float)refpos.X, (float)refpos.Y, (float)pos.X, (float)pos.Y);
            }

            gfx.DrawEllipse(pen, (float)(pos.X - radius), (float)(pos.Y - radius), radius * 2, radius * 2);



        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.UtcNow;
            var elapsed = (now - lastTick);
            lastTick = now;
            if (elapsed == TimeSpan.Zero)
            {
                return; // skip this iteration
            }

            Text = string.Format("{0:P} duty cycle", elapsed.TotalMilliseconds / gameTimer.Interval);

            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var gfx = Graphics.FromImage(bitmap))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;

                var fs = Enumerable.Range(0, bodies.Length).Select(i => Vector3.Zero).ToArray();
                
                for (int i = 0; i < bodies.Length; i++)
                {
                    for (int j = i + 1; j < bodies.Length; j++)
                    {
                        var f = bodies[i].F(bodies[j]);
                        fs[i] += f;
                        fs[j] -= f;
                    }
                }

                for (int k = 0; k < bodies.Length; k++)
                {
                    bodies[k].Apply(fs[k], elapsed.TotalSeconds * timeScale);
                }

                Draw(gfx, Pens.Yellow, SolarSystem.Sun, 3);
                Draw(gfx, Pens.Orange, SolarSystem.Mercury, 1);
                Draw(gfx, Pens.Green, SolarSystem.Venus, 1);
                Draw(gfx, Pens.Blue, SolarSystem.Earth, 1);
                Draw(gfx, Pens.Red, SolarSystem.Mars, 1);
                Draw(gfx, Pens.Black, SolarSystem.Jupiter, 1);
                Draw(gfx, Pens.White, SolarSystem.Saturn, 1);
                Draw(gfx, Pens.Aqua, SolarSystem.Uranus, 1);
                Draw(gfx, Pens.Cyan, SolarSystem.Neptune, 1);

                var earth = SolarSystem.Earth.Position * scale + translate;
                var sun = SolarSystem.Sun.Position * scale + translate;
                var r = (sun - earth);
                //var a = Math.Atan2(r.Y, r.X);
                //var dx = r.X * 0 - r.Y * 1;
                //var dy = r.X * 1 + r.Y * 0;

                
                //gfx.DrawLine(Pens.Black, (float)earth.X, (float)earth.Y, (float)(earth.X - r.Y), (float)(earth.Y + r.X));
                //gfx.DrawLine(Pens.Black, (float)earth.X, (float)earth.Y, (float)(earth.X + r.Y), (float)(earth.Y - r.X));

                var m = 10;
                gfx.DrawLine(Pens.Black, (float)(earth.X + r.Y * m), (float)(earth.Y - r.X * m), (float)(earth.X - r.Y * m), (float)(earth.Y + r.X * m));

            }
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = bitmap;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetScale();
        }
    }
}
