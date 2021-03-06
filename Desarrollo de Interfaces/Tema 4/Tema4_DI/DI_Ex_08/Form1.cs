﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DI_Ex_08
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog FD = new OpenFileDialog();
        Form form2 = new Form();
        FileInfo[] files;
        string strFilePath;
        int cont = 0;


        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (form2 != null)
                {
                    form2.Close();
                }
                form2 = new Form();
                if (FD.ShowDialog() == DialogResult.OK)
                {
                    string fileToOpen = FD.FileName;
                    FileInfo File = new FileInfo(fileToOpen);
                    form2.BackgroundImage = Image.FromFile(fileToOpen);

                }

                FileInfo fInfo = new FileInfo(FD.FileName);
                strFilePath = fInfo.DirectoryName;
                files = fInfo.Directory.GetFiles();
                _lblDirectorio.Text = strFilePath;
                _lblInfoImagen.Text = String.Format("Nombre: " + fInfo.Name + "\nExtesion: " + fInfo.Extension + "\nTemaño" + fInfo.Length);

                form2.Size = new Size(form2.BackgroundImage.Width, form2.BackgroundImage.Height);
                form2.FormBorderStyle = FormBorderStyle.FixedSingle;
                form2.Show();
            }
            catch (System.ObjectDisposedException)
            {

            }

        }




        private void _btnAnterior_Click(object sender, EventArgs e)
        {
            previaImagen();
        }

        private void _btnSiguiente_Click(object sender, EventArgs e)
        {
            siguienteImagen();
        }


        public void previaImagen()
        {
            cont--;
            if (cont < 0)
            {
                cont = files.Length - 1;
            }
            if (files[cont].ToString().EndsWith(".png") || files[cont].ToString().EndsWith(".jpg") || files[cont].ToString().EndsWith(".bmp"))
            {
                form2.BackgroundImage = Image.FromFile(strFilePath + "\\" + files[cont].ToString());
                form2.Size = new Size(form2.BackgroundImage.Width, form2.BackgroundImage.Height);
                _lblInfoImagen.Text = String.Format("Nombre: " + files[cont].Name + "\nExtesion: " + files[cont].Extension + "\nTemaño" + files[cont].Length);
                this.Text = "Image Viewer: " + files[cont].Name;
            }
            else
            {
                previaImagen();
            }
        }

        public void siguienteImagen()
        {

            cont++;
            if (cont > files.Length - 1)
            {
                cont = 0;
            }
            if (files[cont].ToString().EndsWith(".png") || files[cont].ToString().EndsWith(".jpg") || files[cont].ToString().EndsWith(".bmp"))
            {
                form2.BackgroundImage = Image.FromFile(strFilePath + "\\" + files[cont].ToString());
                form2.Size = new Size(form2.BackgroundImage.Width, form2.BackgroundImage.Height);
                _lblInfoImagen.Text = String.Format("Nombre: " + files[cont].Name + "\nExtesion: " + files[cont].Extension + "\nTemaño" + files[cont].Length);
                this.Text = "Image Viewer: " + files[cont].Name;
            }
            else
            {
                siguienteImagen();
            }

        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //capture up arrow key
            if (keyData == Keys.Up)
            {
                return true;
            }
            //capture down arrow key
            if (keyData == Keys.Down)
            {
                return true;
            }
            //capture left arrow key
            if (keyData == Keys.Left)
            {
                previaImagen();
                return true;
            }
            //capture right arrow key
            if (keyData == Keys.Right)
            {
                siguienteImagen();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}