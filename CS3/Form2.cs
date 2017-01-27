using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace CS3
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            timer1.Enabled = false;
            timer2.Enabled = false;
            using (var rng = new RNGCryptoServiceProvider())
            {
                // 厳密にランダムなInt32を作る
                var buffer = new byte[sizeof(int)];
                rng.GetBytes(buffer);
                var seed = BitConverter.ToInt32(buffer, 0);
                var buffer2 = new byte[sizeof(int)];
                rng.GetBytes(buffer2);
                var seed2 = BitConverter.ToInt32(buffer2, 0);
                // そのseedを基にRandomを作る
                var rndi1 = new Random(seed);
                var rndi2 = new Random(seed2);
                timer1.Interval = rndi1.Next(3499, 5500);
                timer2.Interval = rndi2.Next(1499, 3500);
            }
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            using (var rng = new RNGCryptoServiceProvider())
            {
                // 厳密にランダムなInt32を作る
                var buffer = new byte[sizeof(int)];
                rng.GetBytes(buffer);
                var seed = BitConverter.ToInt32(buffer, 0);
                // そのseedを基にRandomを作る
                Random rand = new Random(seed);
                int r = rand.Next(-1, 10);
                string path = "./file/1.png";
                switch (r)
                {
                    case 0:
                        path = "./file/1.png";
                        break;
                    case 1:
                        path = "./file/2.png";
                        break;
                    case 2:
                        path = "./file/3.png";
                        break;
                    case 3:
                        path = "./file/4.png";
                        break;
                    case 4:
                        path = "./file/5.png";
                        break;
                    case 5:
                        path = "./file/6.png";
                        break;
                    case 6:
                        path = "./file/7.png";
                        break;
                    case 7:
                        path = "./file/8.png";
                        break;
                    case 8:
                        path = "./file/9.png";
                        break;
                    case 9:
                        path = "./file/10.png";
                        break;
                }
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
            //Console.WriteLine(Location);
            //フォームのサイズ変更
            size_change(@path);
            Bitmap img = new Bitmap(@path);
            img.MakeTransparent();
            this.BackgroundImage = img;
            this.TransparencyKey = BackColor;
            timer1.Enabled = true;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            size_change("./file/del.png");
            Bitmap img = new Bitmap("./file/del.png");
            img.MakeTransparent();
            this.BackgroundImage = img;
            this.TransparencyKey = BackColor;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
