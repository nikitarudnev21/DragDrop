using RudnevDragProject.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RudnevDragProject
{
    public partial class lblMiliSecond : Form
    {
        int points, moves, progCount, rightMove, right = 0;
        double timeCS, timeSec, timeMin = 0;
        bool timerActive = false;
        bool started = false;
        Image a1, a2, a3, a4, a5, a6, b1, b2, b3, b4, b5, b6, c1, c2, c3, c4, c5, c6, d1, d2, d3,
        d4, d5, d6, e1, e2, e3, e4, e5, e6, f1, f2, f3, f4, f5, f6;
        string[] res = new string[] {};
        List<string> strlist = new List<string>();
        List<int> imgnext = new List<int>();
        List<Image> listright = new List<Image>();
        List<Image> fullist = new List<Image>();
        List<Image> dragreturn = new List<Image>();
        List<Image> imageget = new List<Image>();
        List<Image> imageswap = new List<Image>();
        List<Image> fulllistimage = new List<Image>();
        List<PictureBox> fullistpic = new List<PictureBox>();
        List<PictureBox> listBoxright = new List<PictureBox>();
        List<PictureBox> picdraglist = new List<PictureBox>();
        List<PictureBox> picdragleaveenter = new List<PictureBox>();
        List<PictureBox> picdragdrop = new List<PictureBox>();
        Random rnd = new Random();
        Pen penGreen = new Pen(Color.ForestGreen, 2);
        Pen penRed = new Pen(Color.Tomato, 2);
        public lblMiliSecond()
        {
            InitializeComponent();
            imgnext.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, });
        /*    for (int i = 0; i <= 8; i++, fullist.Add(Image.FromFile($"C:/Users/Nikita/source/repos/RudnevDragProject/Resources/image_part_00{i}.jpg"))) { }
            for (int i = 9; i <= 35; i++, fullist.Add(Image.FromFile($"C:/Users/Nikita/source/repos/RudnevDragProject/Resources/image_part_0{i}.jpg"))) { }*/
            string folder = Application.StartupPath;
            Image[] im = new Image[] { a1, a2, a3, a4, a5, a6, b1, b2, b3, b4, b5, b6, c1, c2, c3, c4, c5, c6, d1, d2, d3,
            d4, d5, d6, e1, e2, e3, e4, e5, e6, f1, f2, f3, f4, f5, f6};
            //  listright.AddRange(fullist);
            //    fullist.CopyTo(im);
            //    foreach (Image img in im) { fullist.Add(img); }
            for (int i = 0; i <= 8; i++, fulllistimage.Add(Image.FromFile(folder + $"\\image_part_00{i}.jpg"))) { }
            for (int i = 9; i <= 35; i++, fulllistimage.Add(Image.FromFile(folder + $"\\image_part_0{i}.jpg"))) { }
            listright.AddRange(fulllistimage);
            fullist.AddRange(fulllistimage);
            foreach (PictureBox pic in panelBoxes.Controls.OfType<PictureBox>())
            {
                /* fullist.Reverse();
                 pic.Image = fullist.Last();
                 fullist.Remove(fullist.Last());*/
                fullistpic.Add(pic);
                listBoxright.Add(pic);
                pic.Image = fulllistimage.First();
                fulllistimage.Remove(fulllistimage.First());
                pic.AllowDrop = true;
                pic.BorderStyle = BorderStyle.None;
                pic.Cursor = Cursors.Default;
            }
            btnPause.Enabled = false;
            btnAgain.Enabled = false;
             shuffleImages();
            // checkWin();

        }

        void shuffleImages()
        {
            int[] n = new int[36];
            List<int> a = new List<int>();
            var randomNumbers = Enumerable.Range(0, 36).OrderBy(x => rnd.Next()).Take(36).ToList();
            foreach (int item in randomNumbers)
            {
                a.Add(item);
            }
            for (int i = 0; i < fullistpic.Count; i++)
            {
                n[i] = a[i];
                fullistpic[i].Image = fullist[n[i]];
            }
        }


        void movesUpdate()
        {
            moves++;
            lblMoves.Text = moves.ToString();
        }
        void checkWin()
        {
            /*   List<Image> imgg = new List<Image> { };
               for (int i = 0; i <35; i++)
               {
                   var listnew = listBoxright.Where(x => x.Image == listright[i]);
                   listBoxright[i].Image =  listnew.Last().Image;
               }*/
            listright.Reverse();
            foreach (PictureBox pic in listBoxright)
            {
                listright.Remove(listright.Last());
                foreach (Image img in listright)
                {
                    if (pic.Image == img)
                    {
                        label2.Text = "win";
                    }
                    else
                    {
                        label2.Text = "lost";
                    }
                }
            }

          /* for (int i = 0; i <=5; i++)
            {
                if (listBoxright[i].Image==listright[i])
                {
                    label2.Text = "win";
                }
                else
                {
                    label2.Text = "lost";
                }
            }*/
        }

        void Form1_Load(object sender, EventArgs e)
        {

            foreach (PictureBox pictureBox in panelBoxes.Controls)
            {
                pictureBox.DragEnter += (s, a) =>
                {
                    if (started)
                    {
                        for (points = 10; points > 2; points-- )
                        {
                            penGreen.DashPattern = new float[] { 2, points };
                            penRed.DashPattern = new float[] { 2, points };
                            var lastimage = picdragleaveenter.Last();
                            lastimage.CreateGraphics().DrawRectangle(penRed, 1, 1, lastimage.Width - 2, lastimage.Height - 2);
                            pictureBox.CreateGraphics().DrawRectangle(penGreen, 1, 1, pictureBox.Width - 2, pictureBox.Height - 2);

                        }
                        dragreturn.Add(pictureBox.Image);
                        a.Effect = a.AllowedEffect;
                    }
                    else
                    {
                        MessageBox.Show("Please start the game", "Start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                pictureBox.DragLeave += (s, a) =>
                {
                    if(started)
                    {
                        pictureBox.Image = dragreturn.Last();
                        dragreturn.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Please start or unpause the game", "Start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                pictureBox.MouseDown += (s, a) =>
                    {
                    if (started)
                    {
                        imageget.Add(pictureBox.Image);
                        picdraglist.Add(pictureBox);
                        picdragleaveenter.Add(pictureBox);
                        pictureBox.DoDragDrop(imageget.Last(), DragDropEffects.Move);
                    }
                    else
                    {
                        MessageBox.Show("Please start or unpause the game", "Start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    };
                pictureBox.DragDrop += (s, a) =>
                {
                    if(started)
                    {
                        imageswap.Add(pictureBox.Image);
                        picdragdrop.Add(pictureBox);
                        picdraglist.Last().Image = imageswap.Last();
                        foreach (PictureBox boxS in picdragdrop)
                        {
                            if (s == boxS)
                            {
                                boxS.Image = imageget.Last();
                                if (boxS.Image != imageswap.Last())
                                {
                                    checkWin();
                                    movesUpdate();
                                }
                            }
                            else
                            {
                                pictureBox.Image = dragreturn.Last();
                                dragreturn.Clear();
                            }
                        }
                        imageget.Clear();
                        imageswap.Clear();
                        picdragdrop.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Please start or unpause the game", "Start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
            }
        }
        private  void Timer1_Tick(object sender, EventArgs e)
        {
                if (timerActive)
                {
                    timeCS += 1.5;
                    if (timeCS >= 100)
                    {
                        timeSec++;
                        timeCS = 0;
                        if (timeSec >= 60)
                        {
                            timeMin++;
                            timeSec = 0;
                        }
                    }
                    lblMinute.Text = String.Format("{0:00}", timeMin);
                    lblSecond.Text = String.Format("{0:00}", timeSec);
                    lblMili.Text = String.Format("{0:00}", timeCS);
                }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            timerActive = false;
            DialogResult dialogResult = MessageBox.Show("Are you really want to exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialogResult== DialogResult.Yes)  this.Close();
            if (dialogResult == DialogResult.No) if(btnStart.Enabled==false) timerActive = true;
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
            timerActive = true;
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            btnAgain.Enabled = true;
            started = true;
        }
        private void BtnPause_Click(object sender, EventArgs e)
        {
            timerActive = timerActive == false ? true : false;
            // btnPause.Text = btnPause.Text == "Pause" ? "Unpause" : "Pause";
            if (btnPause.Text == "Pause")
            {
                started = false;
                btnPause.Size = new Size(140, 43);
                btnPause.Location = new Point(160, 23);
                btnPause.Text = "Unpause";
            }
            else
            {
                started = true;
                btnPause.Size = new Size(122, 43);
                btnPause.Location = new Point(176, 23);
                btnPause.Text = "Pause";
            }
        }
        private void BtnAgain_Click(object sender, EventArgs e)
        {
            timerActive = false;
            DialogResult dialogResult = MessageBox.Show("Are you really want to try again", "Again", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                shuffleImages();
                btnStart.Enabled = true;
                btnPause.Enabled = false;
                btnAgain.Enabled = false;
                timerActive = false;
                started = false;
                btnPause.Text = "Pause";
                lblMoves.Text = "";
                lblMinute.Text = "";
                lblSecond.Text = "";
                lblMili.Text = "";
                moves = 0;
                timeCS = 0;
                timeSec = 0;
                timeMin = 0;
            }
            if(dialogResult == DialogResult.No)
            {
                timerActive = true;
            }

        }
    }
}
