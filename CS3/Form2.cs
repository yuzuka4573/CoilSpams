using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace CS3
{
    public partial class Form2 : Form
    {
        public string[] files = Directory.GetFiles("./file", "*.png");

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
                int file_num = files.Length;
                int r = rand.Next(0, file_num-1);
                string path = files[r];
                Console.WriteLine("file url : {0}", path);
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
            size_change("./file/del/del.png");
            Bitmap img = new Bitmap("./file/del/del.png");
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
