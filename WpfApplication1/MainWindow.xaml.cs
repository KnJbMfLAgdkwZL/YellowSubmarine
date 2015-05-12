using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Collections;
namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
        bool Up = false, Down = false, Left = false, Right = false, Space = false;
        BitmapImage[] aniline = new BitmapImage[4];
        Image[] Wawe_0 = new Image[10];
        Image[] Wawe_1 = new Image[10];
        Image[] Wawe_2 = new Image[10];
        Image[] Wawe_3 = new Image[10];
        Image[] Bottom = new Image[5];
        int time = 0;
        double X, Y, WIDTH, HEIGHT;
        double SpeedSky = 0.1;
        double SpeedWaterline = 1.0;
        int SpeedSubmarine = 3;
        bool GO = true;
        Random rand = new Random();
        int SpeedTorpedoes = 6;
        bool Died = false;
        int DiedTimer = 0;
        BitmapImage[] explosion = new BitmapImage[12];
        double score = 0;
        public MainWindow()
        {
            InitializeComponent();
            Up = Down = Left = Right = false;
            Timer1.Interval = 10;
            Timer1.Tick += new System.EventHandler(Timer1_Tick);
            Timer1.Start();
            Canvas.SetLeft(Sky2, Canvas.GetLeft(Sky1) + 2559);
            for (int i = 0; i < aniline.Length; i++)
            {
                aniline[i] = new BitmapImage();
                aniline[i].BeginInit();
                aniline[i].UriSource = new Uri("pack://application:,,,/WpfApplication1;component/Sorce/wawes_ani/" + i + ".png");
                aniline[i].EndInit();
            }
            for (int i = 0; i < Wawe_0.Length; i++)
            {
                Wawe_0[i] = new Image();
                Wawe_0[i].Name = "Wawe_0" + i;
                Wawe_0[i].Height = Double.NaN;
                Wawe_0[i].Width = 100;
                Wawe_0[i].Stretch = Stretch.Uniform;
                Wawe_0[i].Source = aniline[0];
                Canvas.SetTop(Wawe_0[i], 130);
                Canvas.SetLeft(Wawe_0[i], i * (Wawe_0[i].Width - 1));
                Wawes.Children.Add(Wawe_0[i]);
            }
            for (int i = 0; i < Wawe_1.Length; i++)
            {
                Wawe_1[i] = new Image();
                Wawe_1[i].Name = "Wawe_1" + i;
                Wawe_1[i].Height = Double.NaN;
                Wawe_1[i].Width = 110;
                Wawe_1[i].Stretch = Stretch.Uniform;
                Wawe_1[i].Source = aniline[1];
                Canvas.SetTop(Wawe_1[i], 135);
                Canvas.SetLeft(Wawe_1[i], i * (Wawe_1[i].Width - 1));
                Wawes.Children.Add(Wawe_1[i]);
            }
            for (int i = 0; i < Wawe_2.Length; i++)
            {
                Wawe_2[i] = new Image();
                Wawe_2[i].Name = "Wawe_2" + i;
                Wawe_2[i].Height = Double.NaN;
                Wawe_2[i].Width = 120;
                Wawe_2[i].Stretch = Stretch.Uniform;
                Wawe_2[i].Source = aniline[1];
                Canvas.SetTop(Wawe_2[i], 140);
                Canvas.SetLeft(Wawe_2[i], i * (Wawe_2[i].Width - 1));
                Wawes.Children.Add(Wawe_2[i]);
            }
            for (int i = 0; i < Wawe_3.Length; i++)
            {
                Wawe_3[i] = new Image();
                Wawe_3[i].Name = "Wawe_3" + i;
                Wawe_3[i].Height = Double.NaN;
                Wawe_3[i].Width = 130;
                Wawe_3[i].Stretch = Stretch.Uniform;
                Wawe_3[i].Source = aniline[1];
                Canvas.SetTop(Wawe_3[i], 145);
                Canvas.SetLeft(Wawe_3[i], i * (Wawe_3[i].Width - 1));
                Wawes.Children.Add(Wawe_3[i]);
            }
            for (int i = 0; i < Bottom.Length; i++)
            {
                Bottom[i] = new Image();
                Bottom[i].Name = "Bottom" + i;
                Bottom[i].Height = Double.NaN;
                Bottom[i].Width = 200;
                Bottom[i].Stretch = Stretch.Uniform;
                Wawes.Children.Add(Bottom[i]);
                Bottom[i].Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Sorce/bottom.png"));
                Canvas.SetTop(Bottom[i], 536);
                Canvas.SetLeft(Bottom[i], i * (Bottom[i].Width-1));
            }
            HEIGHT = Submarine.Height;
            WIDTH = Submarine.Width;
            for (int i = 0; i < explosion.Length; i++)
            {
                explosion[i] = new BitmapImage();
                explosion[i].BeginInit();
                explosion[i].UriSource = new Uri("pack://application:,,,/WpfApplication1;component/Sorce/explosion_ani/" + i + ".png");
                explosion[i].EndInit();
            }
            this.Title = "Score: 0";
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (time > 1000000)
                time = 0;
            if (!Died)
            {
                X = Canvas.GetLeft(Submarine);
                Y = Canvas.GetTop(Submarine);
                GO = true;
                int Gor = 0, Ver = 0;
                if (Up)
                    Ver -= SpeedSubmarine;
                if (Down)
                    Ver += SpeedSubmarine;
                if (Left)
                    Gor -= SpeedSubmarine;
                if (Right)
                    Gor += SpeedSubmarine;
                if (Y + Ver <= 175 || Y + Ver + HEIGHT >= 555 || X + Gor <= -5 || X + Gor + WIDTH >= 790)
                    GO = false;
                if (GO)
                {
                    Canvas.SetLeft(Submarine, X + Gor);
                    Canvas.SetTop(Submarine, Y + Ver);
                }
            }
            else
            {
                DiedTimer++;
                if (DiedTimer >= 300)
                    this.Close();
            }
            Canvas.SetLeft(Sky1, Canvas.GetLeft(Sky1) - SpeedSky);
            Canvas.SetLeft(Sky2, Canvas.GetLeft(Sky2) - SpeedSky);
            if (Canvas.GetLeft(Sky1) <= -2560)
                Canvas.SetLeft(Sky1, Canvas.GetLeft(Sky2) + 2559);
            if (Canvas.GetLeft(Sky2) <= -2560)
                Canvas.SetLeft(Sky2, Canvas.GetLeft(Sky1) + 2559);
            time++;
            for (int i = 0; i < Wawe_0.Length; i++)
            {
                Canvas.SetLeft(Wawe_0[i], Canvas.GetLeft(Wawe_0[i]) - SpeedWaterline);
                if (Canvas.GetLeft(Wawe_0[i]) <= -Wawe_0[i].Width)
                    Canvas.SetLeft(Wawe_0[i], Canvas.GetLeft(Wawe_0[i]) + Wawe_0.Length * (Wawe_0[i].Width - 1));
                Canvas.SetLeft(Wawe_1[i], Canvas.GetLeft(Wawe_1[i]) - SpeedWaterline);
                if (Canvas.GetLeft(Wawe_1[i]) <= -Wawe_1[i].Width)
                    Canvas.SetLeft(Wawe_1[i], Canvas.GetLeft(Wawe_1[i]) + Wawe_1.Length * (Wawe_1[i].Width - 1));
                Canvas.SetLeft(Wawe_2[i], Canvas.GetLeft(Wawe_2[i]) - SpeedWaterline);
                if (Canvas.GetLeft(Wawe_2[i]) <= -Wawe_2[i].Width)
                    Canvas.SetLeft(Wawe_2[i], Canvas.GetLeft(Wawe_2[i]) + Wawe_2.Length * (Wawe_2[i].Width - 1));
                Canvas.SetLeft(Wawe_3[i], Canvas.GetLeft(Wawe_3[i]) - SpeedWaterline);
                if (Canvas.GetLeft(Wawe_3[i]) <= -Wawe_3[i].Width)
                    Canvas.SetLeft(Wawe_3[i], Canvas.GetLeft(Wawe_3[i]) + Wawe_3.Length * (Wawe_3[i].Width - 1));
                if (time % 80 == 0)
                {
                    Wawe_0[i].Source = aniline[0];
                    Wawe_1[i].Source = aniline[3];
                    Wawe_2[i].Source = aniline[2];
                    Wawe_3[i].Source = aniline[1];
                }
                if (time % 80 == 20)
                {
                    Wawe_0[i].Source = aniline[1];
                    Wawe_1[i].Source = aniline[0];
                    Wawe_2[i].Source = aniline[3];
                    Wawe_3[i].Source = aniline[2];
                }
                if (time % 80 == 40)
                {
                    Wawe_0[i].Source = aniline[2];
                    Wawe_1[i].Source = aniline[1];
                    Wawe_2[i].Source = aniline[0];
                    Wawe_3[i].Source = aniline[3];
                }
                if (time % 80 == 60)
                {
                    Wawe_0[i].Source = aniline[3];
                    Wawe_1[i].Source = aniline[2];
                    Wawe_2[i].Source = aniline[1];
                    Wawe_3[i].Source = aniline[0];
                }
            }
            for (int i = 0; i < Bottom.Length; i++)
            {
                Canvas.SetLeft(Bottom[i], Canvas.GetLeft(Bottom[i]) - SpeedWaterline);
                if (Canvas.GetLeft(Bottom[i]) <= -Bottom[i].Width)
                    Canvas.SetLeft(Bottom[i], Canvas.GetLeft(Bottom[i]) + Bottom.Length * (Bottom[i].Width - 1));
            }
            if (time % ((100 - score < 10) ? 10 : 100 - score) == 0)//2 Секунды
            {
                Image tmp = new Image();
                tmp.Height = 404;
                tmp.Width = 60;
                tmp.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Sorce/Naval_Mine.png"));
                tmp.Stretch = Stretch.UniformToFill;
                Canvas.SetTop(tmp, rand.Next(175, 500));
                Canvas.SetLeft(tmp, 790);
                Mines.Children.Add(tmp);
            }
            for (int i = 0; i < Mines.Children.Count; i++)
            {
                Canvas.SetLeft(Mines.Children[i], Canvas.GetLeft(Mines.Children[i]) - SpeedWaterline);
                if (Canvas.GetLeft(Mines.Children[i]) <= -((Image)Mines.Children[i]).Width)
                {
                    Mines.Children.Remove(Mines.Children[i]);
                    break;
                }
                else
                    if (!Died)
                        if (Canvas.GetLeft(Mines.Children[i]) < Canvas.GetLeft(Submarine) + Submarine.Width &&
                                    Canvas.GetLeft(Mines.Children[i]) + ((Image)Mines.Children[i]).Width > Canvas.GetLeft(Submarine) &&
                                    Canvas.GetTop(Mines.Children[i]) < Canvas.GetTop(Submarine) + Submarine.Height &&
                                    Canvas.GetTop(Mines.Children[i]) + 60 > Canvas.GetTop(Submarine))
                        {
                            Image tmp = new Image();
                            tmp.Height = 236;
                            tmp.Width = 456;
                            tmp.Stretch = Stretch.UniformToFill;
                            tmp.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Sorce/gameover.png"));
                            Canvas.SetTop(tmp, 145);
                            Canvas.SetLeft(tmp, 400 - tmp.Width / 2);
                            Main.Children.Add(tmp);

                            tmp = new Image();
                            tmp.Width = 134;
                            tmp.Height = 134;
                            tmp.Stretch = Stretch.Fill;
                            tmp.Source = explosion[0];
                            Canvas.SetTop(tmp, Canvas.GetTop(Submarine) + Submarine.Height / 2 - 134 / 2);
                            Canvas.SetLeft(tmp, Canvas.GetLeft(Submarine) + Submarine.Width / 2 - 134 / 2);
                            tmp.MaxWidth = time;
                            ExpLarge.Children.Add(tmp);

                            Died = true;//Game Over
                            Mines.Children.Remove(Mines.Children[i]);
                            Main.Children.Remove(Submarine);
                            break;
                        }
            }
            if (!Died)
                if (Space)
                {
                    Image tmp = new Image();
                    tmp.Height = 5;
                    tmp.Width = 30;
                    tmp.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Sorce/Torpedo.png"));
                    tmp.Stretch = Stretch.Fill;
                    Canvas.SetTop(tmp, Y + 10 + HEIGHT / 2);
                    Canvas.SetLeft(tmp, X + WIDTH);
                    Torpedoes.Children.Add(tmp);
                    Space = false;
                }
            for (int i = 0; i < Torpedoes.Children.Count; i++)
            {
                Canvas.SetLeft(Torpedoes.Children[i], Canvas.GetLeft(Torpedoes.Children[i]) + SpeedTorpedoes);
                if (Canvas.GetLeft(Torpedoes.Children[i]) >= 800)
                {
                    Torpedoes.Children.Remove(Torpedoes.Children[i]);
                    break;
                }
                else
                {
                    Image Torpedo = (Image)Torpedoes.Children[i];
                    for (int j = 0; j < Mines.Children.Count; j++)
                    {
                        Image Mine = (Image)Mines.Children[j];
                        if (Canvas.GetLeft(Mine) < Canvas.GetLeft(Torpedo) + Torpedo.Width &&
                            Canvas.GetLeft(Mine) + Mine.Width > Canvas.GetLeft(Torpedo) &&
                            Canvas.GetTop(Mine) < Canvas.GetTop(Torpedo) + Torpedo.Height &&
                            Canvas.GetTop(Mine) + 60 > Canvas.GetTop(Torpedo))
                        {
                            Image tmp = new Image();
                            tmp.Width = 134;
                            tmp.Height = 134;
                            tmp.Stretch = Stretch.Fill;
                            tmp.Source = explosion[0];
                            Canvas.SetTop(tmp, Canvas.GetTop(Torpedo) + Torpedo.Height / 2 - 134 / 2);
                            Canvas.SetLeft(tmp, Canvas.GetLeft(Torpedo) + Torpedo.Width / 2 - 134 / 2);
                            tmp.MaxWidth = time;
                            ExpSmal.Children.Add(tmp);
                            Image tp = new Image();
                            tp.Width = 134;
                            tp.Height = 134;
                            tp.Stretch = Stretch.Fill;
                            tp.Source = explosion[0];
                            Canvas.SetTop(tp, Canvas.GetTop(Mine) + Mine.Width / 2 - 134 / 2);
                            Canvas.SetLeft(tp, Canvas.GetLeft(Mine) + Mine.Width / 2 - 134 / 2);
                            tp.MaxWidth = time;
                            ExpMed.Children.Add(tp);
                            tmp = new Image();
                            Image t = (Image)Mines.Children[j];
                            tmp.Height = 404;
                            tmp.Width = 60;
                            tmp.Stretch = Stretch.UniformToFill;
                            Canvas.SetTop(tmp, Canvas.GetTop(t));
                            Canvas.SetLeft(tmp, Canvas.GetLeft(t));
                            tmp.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Sorce/Naval_Mine_Broken.png"));
                            Chain.Children.Add(tmp);

                            score += 1;
                            this.Title = "Score: " + (score * 100);
                            SpeedWaterline += 0.1;
                            SpeedSky = SpeedWaterline / 10;

                            Torpedoes.Children.Remove(Torpedoes.Children[i]);
                            Mines.Children.Remove(Mines.Children[j]);
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < Chain.Children.Count; i++)
            {
                Canvas.SetLeft(Chain.Children[i], Canvas.GetLeft(Chain.Children[i]) - SpeedWaterline);
                Canvas.SetTop(Chain.Children[i], Canvas.GetTop(Chain.Children[i]) + 1);
                if (Canvas.GetLeft(Chain.Children[i]) <= -((Image)Chain.Children[i]).Width)
                {
                    Chain.Children.Remove(Chain.Children[i]);
                    break;
                }
            }
            for (int i = 0; i < ExpSmal.Children.Count; i++)
            {
                Canvas.SetLeft(ExpSmal.Children[i], Canvas.GetLeft(ExpSmal.Children[i]) + SpeedTorpedoes / 4);
                double conter = time - ((Image)ExpSmal.Children[i]).MaxWidth;
                if (conter <= 5)
                    ((Image)ExpSmal.Children[i]).Source = explosion[0];
                if (conter > 5 && conter <= 10)
                    ((Image)ExpSmal.Children[i]).Source = explosion[1];
                if (conter > 10 && conter <= 15)
                    ((Image)ExpSmal.Children[i]).Source = explosion[10];
                if (conter > 15 && conter <= 20)
                    ((Image)ExpSmal.Children[i]).Source = explosion[11];
                if (conter >= 25)
                {
                    ExpSmal.Children.Remove(ExpSmal.Children[i]);
                    break;
                }
            }
            for (int i = 0; i < ExpMed.Children.Count; i++)
            {
                Canvas.SetLeft(ExpMed.Children[i], Canvas.GetLeft(ExpMed.Children[i]) - SpeedWaterline / 3);
                double conter = time - ((Image)ExpMed.Children[i]).MaxWidth;
                if (conter <= 5)
                    ((Image)ExpMed.Children[i]).Source = explosion[0];
                if (conter > 5 && conter <= 10)
                    ((Image)ExpMed.Children[i]).Source = explosion[1];
                if (conter > 10 && conter <= 15)
                    ((Image)ExpMed.Children[i]).Source = explosion[2];
                if (conter > 15 && conter <= 20)
                    ((Image)ExpMed.Children[i]).Source = explosion[3];
                if (conter > 20 && conter <= 25)
                    ((Image)ExpMed.Children[i]).Source = explosion[8];
                if (conter > 25 && conter <= 30)
                    ((Image)ExpMed.Children[i]).Source = explosion[9];
                if (conter > 30 && conter <= 35)
                    ((Image)ExpMed.Children[i]).Source = explosion[10];
                if (conter > 35 && conter <= 40)
                    ((Image)ExpMed.Children[i]).Source = explosion[11];
                if (conter >= 40)
                {
                    ExpMed.Children.Remove(ExpMed.Children[i]);
                    break;
                }
            }
            for (int i = 0; i < ExpLarge.Children.Count; i++)
            {
                Canvas.SetLeft(ExpLarge.Children[i], Canvas.GetLeft(ExpLarge.Children[i]) + SpeedWaterline/2);
                double conter = time - ((Image)ExpLarge.Children[i]).MaxWidth;
                if (conter <= 5)
                    ((Image)ExpLarge.Children[i]).Source = explosion[0];
                if (conter > 5 && conter <= 10)
                    ((Image)ExpLarge.Children[i]).Source = explosion[1];
                if (conter > 10 && conter <= 15)
                    ((Image)ExpLarge.Children[i]).Source = explosion[2];
                if (conter > 15 && conter <= 20)
                    ((Image)ExpLarge.Children[i]).Source = explosion[3];
                if (conter > 20 && conter <= 25)
                    ((Image)ExpLarge.Children[i]).Source = explosion[4];
                if (conter > 25 && conter <= 30)
                    ((Image)ExpLarge.Children[i]).Source = explosion[5];
                if (conter > 30 && conter <= 35)
                    ((Image)ExpLarge.Children[i]).Source = explosion[6];
                if (conter > 40 && conter <= 45)
                    ((Image)ExpLarge.Children[i]).Source = explosion[7];
                if (conter > 50 && conter <= 55)
                    ((Image)ExpLarge.Children[i]).Source = explosion[8];
                if (conter > 55 && conter <= 60)
                    ((Image)ExpLarge.Children[i]).Source = explosion[9];
                if (conter > 65 && conter <= 70)
                    ((Image)ExpLarge.Children[i]).Source = explosion[10];
                if (conter > 70 && conter <= 75)
                    ((Image)ExpLarge.Children[i]).Source = explosion[11];
                if (conter >= 75)
                {
                    ExpLarge.Children.Remove(ExpLarge.Children[i]);
                    break;
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                Up = true;
            if (e.Key == Key.Down)
                Down = true;
            if (e.Key == Key.Left)
                Left = true;
            if (e.Key == Key.Right)
                Right = true;
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                Up = false;
            if (e.Key == Key.Down)
                Down = false;
            if (e.Key == Key.Left)
                Left = false;
            if (e.Key == Key.Right)
                Right = false;
            if (e.Key == Key.Space)
                Space = true;
        }
    }
}