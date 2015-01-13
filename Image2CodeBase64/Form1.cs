using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Image2CodeBase64
{
    public partial class Form1 : Form
    {
        Bitmap image = null;

        public Form1()
        {
            InitializeComponent();

            image = new Bitmap(350, 400);
            Graphics gx = Graphics.FromImage(image);
            using (Pen pen = new Pen(Color.Aqua, 20))
            using (Pen pen2 = new Pen(Color.OrangeRed, 10))
            {
                gx.DrawRectangle(pen, 100, 100, 100, 100);
                gx.DrawEllipse(pen2, 200, 150, 100, 100);
            }

            pictureBox1.Image = image;

        }

        private void button2CodeBase64_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                byte[] result = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);
                    result = stream.ToArray();
                    textBox1.Text = System.Convert.ToBase64String(result);
                }
            }
        }

        private void button2Image_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] png_bin = System.Convert.FromBase64String(textBox1.Text);
                MemoryStream stream = new MemoryStream(png_bin);
                Image im = Image.FromStream(stream, false, false);
                image = (Bitmap)im;
                pictureBox1.Image = image;
                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid binary data.");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            var keyCode = (Keys)(msg.WParam.ToInt32() &
                                  Convert.ToInt32(Keys.KeyCode));
            if ((msg.Msg == WM_KEYDOWN && keyCode == Keys.A)
                && (ModifierKeys == Keys.Control)
                && textBox1.Focused)
            {
                textBox1.SelectAll();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
