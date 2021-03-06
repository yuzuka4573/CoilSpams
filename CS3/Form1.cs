﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace CS3
{
    public partial class Form1 : Form
    {
        public Random rnd = new Random();
        public Random rnd1 = new Random();
        public Random rnd2 = new Random();
        public Random rndx = new Random();
        public Random rndy = new Random();
        public int mouse_X;
        public int mouse_Y;
        public int form_move_start = 0;
        public int c = 0;
        public string [] files = Directory.GetFiles("./file","*.png");
        public Form1()
        {
            InitializeComponent();
            if (files.Length == 0) {
                MessageBox.Show("fileフォルダにpngファイルが入っていません","Warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            foreach (string i in files)
            {
                System.Console.WriteLine("{0} ", i);
            }
            this.MouseMove += new MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(this.Form1_MouseUp);
            this.MouseDown += new MouseEventHandler(this.Form1_MouseDown);
            timer1.Enabled = true;

            using (var rng = new RNGCryptoServiceProvider())
            {
                // 厳密にランダムなInt32を作る
                var buffer = new byte[sizeof(int)];
                rng.GetBytes(buffer);
                var seed = BitConverter.ToInt32(buffer, 0);
                // そのseedを基にRandomを作る
                var rndi1 = new Random(seed);
                timer1.Interval = rndi1.Next(30, 400);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("interval: " + timer1.Interval);
            c++; 
            Console.WriteLine(c);
            if (c < 150)
            {
                if (timer1.Interval > 10)
                {
                    Console.WriteLine("speed up!!");
                    timer1.Interval = timer1.Interval - 5;
                }

                Form2 f = new Form2();
                f.ShowInTaskbar = false;
                f.ShowDialog(this);
                
            }
            else {
                timer1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormBorderStyle = FormBorderStyle.None;
            using (var rng = new RNGCryptoServiceProvider())
            {
                // 厳密にランダムなInt32を作る
                var buffer = new byte[sizeof(int)];
                rng.GetBytes(buffer);
                var seed = BitConverter.ToInt32(buffer, 0);
                // そのseedを基にRandomを作る
                Random rand = new Random(seed);
                int file_num = files.Length;
                Console.WriteLine(file_num);
                int r = rand.Next(0, file_num-1);
                string path =files[r];
                Console.WriteLine("file url : {0}",path);
                show(path);
            }
        }

        private void show(string path)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[sizeof(int)];
                rng.GetBytes(buffer);
                var buffer2 = new byte[sizeof(int)];
                rng.GetBytes(buffer2);
                var seed = BitConverter.ToInt32(buffer, 0);
                var seed2 = BitConverter.ToInt32(buffer2, 0);
                Random rndx = new Random(seed);
                Random rndy = new Random(seed2);
                Location = new Point(rndx.Next(Screen.PrimaryScreen.Bounds.Width), rndy.Next(Screen.PrimaryScreen.Bounds.Height));
            }
            //フォームのサイズ変更
            size_change(@path);
            Bitmap img = new Bitmap(@path);
            img.MakeTransparent();
            this.BackgroundImage = img;
            this.TransparencyKey = BackColor;
        }

        //ウィンドウの大きさを画像の大きさに変更
        private void size_change(string path)
        {
            //元画像の縦横サイズを調べる
            System.Drawing.Bitmap bmpSrc = new System.Drawing.Bitmap(@path);
            int width = bmpSrc.Width;
            int height = bmpSrc.Height;
            //ウィンドウのサイズを変更
            this.Size = new Size(width, height);
        }

        //move pic
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  // マウスの左ボタンかチェックする
            {
                form_move_start = 1;
                mouse_X = e.X;
                mouse_Y = e.Y;
                Console.WriteLine("Left pressed");
            }
            else if (e.Button == MouseButtons.Right)  // マウスの右ボタンかチェックする
            {
                Console.WriteLine("Right pressed");
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (form_move_start == 1)
            {
                this.Left += e.X - mouse_X;
                this.Top += e.Y - mouse_Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Move to :" + this.Location);
            form_move_start = 0;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
