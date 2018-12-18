using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using KingRing.GameObjects;
using Timer = System.Windows.Forms.Timer;

namespace KingRing.Architecture
{
    public partial class KingRingWindow : Form
    {
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private readonly GameState gameState;
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private int tickCount;

        public KingRingWindow(DirectoryInfo imagesDirectory = null)
        {
            gameState = new GameState();

            ClientSize = new Size(
                GameState.ElementSize * Game.Width,
                GameState.ElementSize * Game.Height + GameState.ElementSize);
            FormBorderStyle = FormBorderStyle.FixedDialog;

            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("D:\\Michael\\Desktop\\Programming\\KingRing\\KingRing\\Pictures");

            foreach (var e in imagesDirectory.GetFiles("*.png"))
            {
                bitmaps[e.Name] = (Bitmap) Image.FromFile(e.FullName);
                bitmaps[e.Name].SetResolution(47, 47);
            }

            var timer = new Timer {Interval = 15};
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void KingRingWindow_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "KingRing";
            DoubleBuffered = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);
            Game.KeyPressed = e.KeyCode;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
            Game.KeyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, GameState.ElementSize);
            e.Graphics.FillRectangle(
                Brushes.Lime, 0, 0, GameState.ElementSize * Game.Width,
                GameState.ElementSize * Game.Height);
            foreach (var a in gameState.Animations)
                e.Graphics.DrawImage(bitmaps[a.Creature.GetImageFileName()], a.Location);
            e.Graphics.ResetTransform();
            e.Graphics.DrawString("Score: " + Game.Score, new Font("Arial", 16), Brushes.Green, 0, 0);
            e.Graphics.DrawString("Health: " + Player.Health, new Font("Arial", 16), Brushes.Green, 100, 0);

            if (Player.Health <= 0)
            {
                var form2 = new Form2();
                form2.Show();
                Thread.Sleep(3 * 1000);
                Close();
            }
        }

        private void TimerTick(object sender, EventArgs args)
        {
            if (tickCount == 0) gameState.BeginAct();
            foreach (var e in gameState.Animations)
                e.Location = new Point(e.Location.X + 4 * e.Command.DeltaX, e.Location.Y + 4 * e.Command.DeltaY);
            if (Game.Score >= 50 && Player.Health < 3)
            {
                Player.RestoreHealth();
                Game.Score -= 50;
            }
            if (tickCount == 7)
                gameState.EndAct();
            tickCount++;
            if (tickCount == 8) tickCount = 0;
            Invalidate();
        }
    }
}
