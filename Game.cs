using RudnevDragProject.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RudnevDragProject
{
    public partial class Game : Form
    {

        int points, moves, backMove = 0;
        double timeCS, timeSec, timeMin = 0;
        bool timerActive, started = false;
        string folder= Application.StartupPath;
        Random rnd = new Random();
        Pen penGreen = new Pen(Color.ForestGreen, 2);
        Pen penRed = new Pen(Color.Tomato, 2);
        List<Image> listright = new List<Image>();
        List<Image> fullist = new List<Image>();
        List<Image> dragreturn = new List<Image>();
        List<Image> imageget = new List<Image>();
        List<Image> imageswap = new List<Image>();
        List<Image> fulllistimage = new List<Image>();
        List<Image> moveBackImg1 = new List<Image>();
        List<Image> moveBackImg2 = new List<Image>();
        List<Image> moveForwardImg1 = new List<Image>();
        List<Image> moveForwardImg2 = new List<Image>();
        List<PictureBox> fullistpic = new List<PictureBox>();
        List<PictureBox> listBoxright = new List<PictureBox>();
        List<PictureBox> picdraglist = new List<PictureBox>();
        List<PictureBox> picdragleaveenter = new List<PictureBox>();
        List<PictureBox> picdragdrop = new List<PictureBox>();
        List<PictureBox> moveBackBox1 = new List<PictureBox>();
        List<PictureBox> moveBackBox2 = new List<PictureBox>();
        List<PictureBox> moveForwardBox1 = new List<PictureBox>();
        List<PictureBox> moveForwardBox2 = new List<PictureBox>();

        public Game()
        {
            InitializeComponent();
        /*    for (int i = 0; i <= 8; i++, fullist.Add(Image.FromFile($"C:/Users/Nikita/source/repos/RudnevDragProject/Resources/image_part_00{i}.jpg"))) { }
        /*    for (int i = 0; i <= 8; i++, fullist.Add(Image.FromFile($"C:/Users/Nikita/source/repos/RudnevDragProject/Resources/image_part_00{i}.jpg"))) { }
            for (int i = 9; i <= 35; i++, fullist.Add(Image.FromFile($"C:/Users/Nikita/source/repos/RudnevDragProject/Resources/image_part_0{i}.jpg"))) { }*/
            for (int i = 0; i <= 8; i++, fulllistimage.Add(Image.FromFile(folder + $"\\image_part_00{i}.jpg"))) { }
            for (int i = 9; i <= 35; i++, fulllistimage.Add(Image.FromFile(folder + $"\\image_part_0{i}.jpg"))) { }
            listright.AddRange(fulllistimage);
            fullist.AddRange(fulllistimage);
            foreach (PictureBox pic in panelBoxes.Controls.OfType<PictureBox>())
            {
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
            btnLeft.Enabled = false;
            btnRight.Enabled = false;
            shuffleImages();
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

        void movesPlus()
        {
            moves++;
            lblMoves.Text = moves.ToString();
        }

        void movesMinus()
        {
            moves--;
            lblMoves.Text = moves.ToString();
        }

        void MBox()
        {
            MaterialMessageBox.Res("Please start or unpause the game", "Start", MessageBoxButtons.OK);
        }

        void checkMoveBack()
        {
            if (moveBackImg2.Count == 0)
            {
                btnLeft.Enabled = false;
            }
            else
            {
                btnLeft.Enabled = true;
            }
        }

        void checkMoveForward()
        {
            if (backMove < 0)
            {
                backMove = 0;
            }
            if (backMove>0/* && moveForwardBox1.Count!=0 && moveForwardBox2.Count!=0 && moveForwardImg1.Count!=0 && moveForwardImg2.Count!=0*/)
            {
                btnRight.Enabled = true;
            }
            else
            {
                btnRight.Enabled = false;
            }
        }

        void Again()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnAgain.Enabled = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
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
            ProgBar.Value = 0;
            moveBackImg1.Clear();
            moveBackImg2.Clear();
            moveBackBox1.Clear();
            moveBackBox2.Clear();
            moveForwardImg1.Clear();
            moveForwardImg2.Clear();
            moveForwardBox1.Clear();
            moveForwardBox2.Clear();
            btnRight.Enabled = false;
            btnLeft.Enabled = false;
            backMove = 0;
            BackColor = Color.DarkSeaGreen;
            shuffleImages();
        }

        void Animations()
        {
            label4.Visible = label4.Visible == false ? true : false;
            label5.Visible = label5.Visible == false ? true : false;
            label6.Visible = label6.Visible == false ? true : false;
            BackColor = Color.FromArgb(rnd.Next(0, 256),
            rnd.Next(0, 256), rnd.Next(0, 256));
        }

        void animateWin()
        {
            new Thread(() => {
                Action action = async () =>
                {
                    for (int i = 0, y = 0; i <= 250; i++, await Task.Delay(1), y++)
                    {
                        label4.CreateGraphics().DrawImage(Resources.win, 0, 1, i, y);
                    }
                    /*  for (int i = 0; i <= 605; i++)
                      {
                          label5.CreateGraphics().DrawImage(Resources.tar, 58, 250, i, 266);
                      }
                      for (int i = 0; i <= 299; i++)
                      {
                          label6.CreateGraphics().DrawImage(Resources.trophy, 453, 1, i, 231);
                      }*/
                };
                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }).Start();
        }

        async void Win()
        {
            animateWin();
            foreach (PictureBox pic in panelBoxes.Controls.OfType<PictureBox>())
            {
                pic.Visible = false;
            }
            List<Panel> panels = new List<Panel>() { panelTop, panelPicture, PanelContent};
            foreach (Control control in panels)
            {
                control.Visible = false;
            }
            timerActive = false;
            for (Opacity = 0.1; Opacity < 1; Opacity += 0.1)
            {
                await Task.Delay(40);
                Animations();
                await Task.Delay(40);

            }
            for (Opacity = 0.2; Opacity < 1; Opacity += 0.1)
            {
                await Task.Delay(60);
                Animations();
                await Task.Delay(60);
            }
            for (Opacity = 0.3; Opacity < 1; Opacity += 0.1)
            {
                await Task.Delay(80);
                Animations();
                await Task.Delay(80);
            }

            for (int i = 0; i <=10; i++, await Task.Delay(120))
            {
                label4.Visible = label4.Visible == false ? true : false;
                await Task.Delay(120);
                label5.Visible = label5.Visible == false ? true : false;
                await Task.Delay(120);
                label6.Visible = label6.Visible == false ? true : false;
            }
            animateWin();
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            MaterialMessageBox.Res("Congratulations you won", "Congratulations!!!", MessageBoxButtons.OK);
            using (WebClient client = new WebClient())
            {
                client.DownloadFileAsync(
                new Uri("https://rudnev19.thkit.ee/diplom.png"), folder+"\\diplom.png");
                //File.Move(folder+"\\diplom.png", Environment.SpecialFolder.Desktop+"\\diplom.png");
            }
            DialogResult result = MaterialMessageBox.Res("Do you want to play again?", "Again?", MessageBoxButtons.YesNo);
            if(result==DialogResult.Yes)
            {
                Again();
                foreach (PictureBox pic in panelBoxes.Controls.OfType<PictureBox>())
                {
                    pic.Visible = true;
                }
                foreach (Control control in panels)
                {
                    control.Visible = true;
                }
            }
            else
            {
                Close();
            }

            /*    int cc = 0;
                string path = folder + "\\" + "records.txt";
                var list = new List<char>();
                var liststr = new List<string>();
                using (StreamWriter stream = new StreamWriter(path, true, Encoding.UTF8))
                {
                    stream.Write("\n" + "----------------------" + "\n"
                        + $"Игрок {cc}" + "\n" +
                        "Ходы:" + lblMoves.Text + "\n" +
                        "Минуты:" + lblMinute.Text + "\n" +
                        "Секунды:" + lblSecond.Text + "\n" +
                        "Миллисекунды:" + lblMili.Text + "\n"
                        + "----------------------" + "\n");
                }
                liststr.Add(File.ReadAllText(path));
                cc= liststr.Where(x => x.Contains("1")).Count();
                list.AddRange(File.ReadAllText(path));
                File.ReadAllLines(path);
                cc = list.Count;
                MessageBox.Show(cc.ToString() + path + "\n" + File.ReadAllText(path));*/
            //   file.AddRange(reader.ReadToEnd().Where(x =>x.ToString().StartsWith("Игрок")));
            //   int s = list.Contains("Игрок").ToString().Count();

            //  MessageBox.Show(string.Join(Environment.NewLine, reader.ReadToEnd().Where(x => x.ToString().StartsWith("Игрок"))));
        }

        public async void Form1_Load(object sender, EventArgs e)
        {
            this.LoadAnimation();
            lblMain.Visible = false;
            await Task.Delay(1100);
            lblMain.AnimateText();
            lblMain.Visible = true;
            foreach (PictureBox pictureBox in panelBoxes.Controls.OfType<PictureBox>())
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
                        MBox();
                    }
                };

                pictureBox.DragLeave += (s, a) =>
                {
                    if(started)
                    {
                        pictureBox.Image = dragreturn.Last();
                        dragreturn.Clear();
                    }

                };

                pictureBox.MouseDown += (s, a) =>
                {
                    if (started)
                    {
                        imageget.Add(pictureBox.Image);
                        picdraglist.Add(pictureBox);
                        picdragleaveenter.Add(pictureBox);
                        moveBackBox1.Add(pictureBox);
                        pictureBox.DoDragDrop(imageget.Last(), DragDropEffects.Move);
                    }
                    else
                    {
                      MBox();
                    }
                };

                pictureBox.DragDrop += async (s, a) =>
                {
                    if (started)
                    {
                        imageswap.Add(pictureBox.Image);
                        moveBackImg2.Add(pictureBox.Image);
                        picdragdrop.Add(pictureBox);
                        picdraglist.Last().Image = imageswap.Last();
                        foreach (PictureBox boxS in picdragdrop)
                        {
                            if (s == boxS)
                            {
                                boxS.Image = imageget.Last();
                                if (boxS.Image != imageswap.Last())
                                {
                                    movesPlus();
                                    checkMoveBack();
                                    checkMoveForward();
                                    moveBackBox2.Add(boxS);
                                    moveBackImg1.Add(boxS.Image);
                                    List<PictureBox> list = new List<PictureBox>();
                                    for (int i = 0; i <= 35; i++)
                                    {
                                        list.AddRange(listBoxright.Where(x => listBoxright[i].Image==listright[i] && x.Image==boxS.Image && x==boxS));
                                    }
                                    ProgBar.Value = list.Count;
                                    if (list.Count == 36)
                                    {
                                        await Task.Delay(100);
                                        Win();
                                    }
                                    list.Clear();
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
                };
            }
            checkMoveBack();

            btnLeft.Click += (s, a) =>
            {
                moveBackBox1.Last().Image = moveBackImg1.Last();
                moveBackBox2.Last().Image = moveBackImg2.Last();
                moveForwardBox1.Add(moveBackBox1.Last());
                moveForwardBox2.Add(moveBackBox2.Last());
                moveForwardImg1.Add(moveBackImg1.Last());
                moveForwardImg2.Add(moveBackImg2.Last());
                moveBackImg1.Remove(moveBackImg1.Last());
                moveBackImg2.Remove(moveBackImg2.Last());
                moveBackBox1.Remove(moveBackBox1.Last());
                moveBackBox2.Remove(moveBackBox2.Last());
                movesMinus();
                backMove++;
                checkMoveForward();
                checkMoveBack();
            };

            btnRight.Click += (s, a) =>
            {
                if(backMove>0)
                {
                    moveForwardBox1.Last().Image = moveForwardImg1.Last();
                    moveForwardBox2.Last().Image = moveForwardImg2.Last();
                    moveForwardImg1.Remove(moveForwardImg1.Last());
                    moveForwardImg2.Remove(moveForwardImg2.Last());
                    moveForwardBox1.Remove(moveForwardBox1.Last());
                    moveForwardBox2.Remove(moveForwardBox2.Last());
                    movesPlus();
                }
                backMove--;
                checkMoveForward();
            };

            btnStart.Click += (s, a) =>
            {
                timerActive = true;
                btnPause.Enabled = true;
                btnAgain.Enabled = true;
                started = true;
                btnStart.Enabled = false;
            };

            btnPause.Click += (s, a) =>
            {
                timerActive = timerActive == false ? true : false;
                if (btnPause.Text == "Pause")
                {
                    started = false;
                    btnPause.Size = new Size(140, 43);
                    btnPause.Location = new Point(885, 14);
                    btnPause.Text = "Unpause";
                }
                else
                {
                    started = true;
                    btnPause.Size = new Size(122, 43);
                    btnPause.Location = new Point(902, 14);
                    btnPause.Text = "Pause";
                }
            };

            btnAgain.Click += (s, a) =>
            {
                timerActive = false;
                Opacity = 0.85;
                DialogResult dialogResult = MaterialMessageBox.Res("Are you really want to try again?", "Again", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Again();
                }
                else if (dialogResult == DialogResult.No && !btnStart.Enabled && btnPause.Text == "Pause")
                {
                    timerActive = true;
                }
                Opacity = 1;
            };

            btnExit.Click += (s, a) =>
            {
                Opacity = 0.85;
                timerActive = false;
                DialogResult dialogResult = MaterialMessageBox.Res("Are you really want to exit?", "Exit", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
                else if (dialogResult == DialogResult.No && !btnStart.Enabled && btnPause.Text == "Pause")
                {
                    timerActive = true;
                    Opacity = 1;
                }
                else
                {
                    Opacity = 1;
                }
            };

            timer1.Tick += (s, a) =>
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
            };
        }
    }
}
