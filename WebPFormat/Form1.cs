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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Multiselect = true;
                    openFileDialog.Filter =
                        "Image(*.webp;*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff)|*.webp;*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string[] pathFileNames = openFileDialog.FileNames;
                        foreach (var pathFileName in pathFileNames)
                        {
                            var pathFileExtension = Path.GetExtension(pathFileName);
                            ImageTo(pathFileName, pathFileExtension);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array aryFiles = (Array)e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < aryFiles.Length; i++)
                {
                    var pathFileName = aryFiles.GetValue(i).ToString();
                    var pathFileExtension = Path.GetExtension(pathFileName);

                    if (pathFileExtension == ".webp" || pathFileExtension == ".png" || pathFileExtension == ".jpg" || pathFileExtension == ".jpeg"
                        || pathFileExtension == ".gif" || pathFileExtension == ".bmp" || pathFileExtension == ".tif" || pathFileExtension == ".tiff")
                    {
                        ImageTo(pathFileName, pathFileExtension);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImageTo(string pathFileName, string pathFileExtension)
        {
            Image img;
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

            if (pathFileExtension != Extension)
            {
                string lossyFileName = Path.Combine(Path.GetDirectoryName(pathFileName),
                    Path.GetFileNameWithoutExtension(pathFileName) + Extension);
                img.Save(lossyFileName);
            }
            img.Dispose();
        }

        private void button1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

    }
}
