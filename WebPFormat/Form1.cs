using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebPFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Image img;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Multiselect = true;
                    openFileDialog.Filter = "Image(*.webp;*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff)|*.webp;*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string[] pathFileNames = openFileDialog.FileNames;
                        foreach (var pathFileName in pathFileNames)
                        {
                            if (Path.GetExtension(pathFileName) == ".webp")
                            {
                                using (WebP webp = new WebP())
                                {
                                    img = webp.Load(pathFileName);
                                }
                            }
                            else
                            {
                                img = Image.FromFile(pathFileName);
                            }

                            string Extension = ".jpg";
                            if (radioButton1.Checked)
                            {
                                Extension = ".jpg";
                            }
                            else if (radioButton2.Checked)
                            {
                                Extension = ".png";
                            }
                            else if (radioButton3.Checked)
                            {
                                Extension = ".bmp";
                            }
                            else if (radioButton4.Checked)
                            {
                                Extension = ".gif";
                            }

                            //string lossyFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "123.jpg");
                            string lossyFileName = Path.Combine(Path.GetDirectoryName(pathFileName), Path.GetFileNameWithoutExtension(pathFileName) + Extension);
                            img.Save(lossyFileName);
                            img.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
