using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace StriveOverlay
{
    public partial class Form1 : Form
    {

        private static readonly Tuple<int, int[]> wallOnP1 = Tuple.Create(0x4C6E4F8, new int[] { 0x110, 0x0, 0xCFA0 });
        private static readonly Tuple<int, int[]> wallOnP2 = Tuple.Create(0x4C6E4F8, new int[] { 0x110, 0x8, 0xCFA0 });

        public int vBorder = 0;
        public int hBorder = 0;

        public double wallP1Perc = 0;
        public double wallP2Perc = 0;

        public int barP1x = 0;
        public int barP1y = 0;
        public int barP1width = 0;
        public int barP1height = 0;
        public SolidBrush barP1color;

        public int barP2x = 0;
        public int barP2y = 0;
        public int barP2width = 0;
        public int barP2height = 0;
        public SolidBrush barP2color;

        public struct RECT
        {
            public int left, top, right, bottom, width, height;
        }

        public static RECT rect;
        public static IntPtr handle;
        public static string WINDOW_NAME;
        public static string PROC_NAME = "GGST-Win64-Shipping";
        public static int screenWidth;
        public static int screenHeight;
        public static int calcGameHeight;
        public static int calcGameWidth;

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetClientRect(IntPtr hwnd, out RECT lpRect);

        public static OverlayWindow window;
        public static Graphics graphics;

        public Form1()
        {
            InitializeComponent();

            try
            {
                WINDOW_NAME = System.Diagnostics.Process.GetProcessesByName(PROC_NAME).First().MainWindowTitle;
                handle = FindWindow(null, WINDOW_NAME);
            }
            catch (Exception)
            {
                MessageBox.Show("GG STRIVE process was not found!\n(GGST-Win64-Shipping.exe)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("failed to open process");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;
            
            window = new OverlayWindow(0,0, screenWidth, screenHeight)
            {
                IsTopmost = true,
                IsVisible = true
            };

            graphics = new Graphics(IntPtr.Zero, screenWidth, screenHeight)
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = false,
                VSync = true
            };

            window.Create();
            
            graphics.WindowHandle = window.Handle;
            graphics.Setup();

            var hb = new BackgroundWorker();
            hb.DoWork += new DoWorkEventHandler(HeartBeat);
            hb.RunWorkerAsync();

        }

        private void HeartBeat(object sender, DoWorkEventArgs e)
        {
            var strive = MemoryLib.MemoryHandler.OpenProcessByName(PROC_NAME, true);

            while ((sender as BackgroundWorker).CancellationPending == false)
            {

                try
                {

                    IntPtr wallP1Ptr = strive.GetAddressWithOffsets(wallOnP1.Item1, wallOnP1.Item2);
                    IntPtr wallP2Ptr = strive.GetAddressWithOffsets(wallOnP2.Item1, wallOnP2.Item2);

                    wallP1Perc = MathHelper.Clamp((Convert.ToDouble(strive.ReadMemory<int>(wallP1Ptr)) / 3200) * 100, 0, 100);
                    wallP2Perc = MathHelper.Clamp((Convert.ToDouble(strive.ReadMemory<int>(wallP2Ptr)) / 3200) * 100, 0, 100);

                    System.Threading.Thread.Sleep(10);

                    SetProcessDPIAware();

                    GetClientRect(handle, out rect);
                    calcGameHeight = rect.bottom - rect.top;
                    calcGameWidth = rect.right - rect.left;

                    GetWindowRect(handle, out rect);
                    vBorder = (rect.bottom - rect.top) - calcGameHeight;
                    hBorder = (rect.right - rect.left) - calcGameWidth;

                    window.X = rect.left + (hBorder / 2);
                    window.Y = rect.top + vBorder - (vBorder / 5);

                    window.Width = calcGameWidth;
                    window.Height = calcGameHeight;

                    graphics.Width = calcGameWidth;
                    graphics.Height = calcGameHeight;

                    barP1width = Convert.ToInt32(calcGameWidth / 6);
                    barP1height = Convert.ToInt32(calcGameHeight / 40);
                    barP1x = Convert.ToInt32(calcGameWidth / 2) - Convert.ToInt32(calcGameWidth / 20.5) - barP1width;
                    barP1y = Convert.ToInt32(calcGameHeight / 15);

                    barP2width = barP1width;
                    barP2height = barP1height;
                    barP2x = Convert.ToInt32(calcGameWidth / 2) + Convert.ToInt32(calcGameWidth / 20.5);
                    barP2y = barP1y;

                    graphics.Recreate();
                    graphics.BeginScene();
                    graphics.ClearScene();

                    switch (wallP1Perc)
                    {
                        case var expression when wallP1Perc <= 75:
                            barP1color = graphics.CreateSolidBrush(6, 178, 37, 255);
                            break;
                        case var expression when wallP1Perc <= 90:
                            barP1color = graphics.CreateSolidBrush(255, 174, 0, 255);
                            break;
                        case var expression when wallP1Perc > 90:
                            barP1color = graphics.CreateSolidBrush(201, 37, 0, 255);
                            break;
                        default:
                            barP1color = graphics.CreateSolidBrush(0, 0, 0, 0);
                            break;
                    }

                    switch (wallP2Perc)
                    {
                        case var expression when wallP2Perc <= 70:
                            barP2color = graphics.CreateSolidBrush(6, 178, 37, 255);
                            break;
                        case var expression when wallP2Perc <= 90:
                            barP2color = graphics.CreateSolidBrush(255, 174, 0, 255);
                            break;
                        case var expression when wallP2Perc > 90:
                            barP2color = graphics.CreateSolidBrush(201, 37, 0, 255);
                            break;
                        default:
                            barP2color = graphics.CreateSolidBrush(0, 0, 0, 0);
                            break;
                    }


                    if (GetActiveWindowTitle() == WINDOW_NAME)
                    {

                        if (chkDebug.Checked)
                        {
                            graphics.DrawRectangle(graphics.CreateSolidBrush(Color.Green), new Rectangle(0, 0, window.Width, window.Height), 1);
                            graphics.DrawLine(graphics.CreateSolidBrush(Color.Green), new Line(0, window.Height / 2, window.Width, window.Height / 2), 1);
                            graphics.DrawLine(graphics.CreateSolidBrush(Color.Green), new Line(window.Width / 2, 0, window.Width / 2, window.Height), 1);
                        }

                        if (chkWallP1.Checked)
                        {
                            graphics.DrawBox2D(graphics.CreateSolidBrush(Color.Transparent),graphics.CreateSolidBrush(0, 0, 0, 100), new Rectangle(barP1x, barP1y, barP1x + barP1width, barP1y + barP1height), 1);
                            graphics.DrawVerticalProgressBar(graphics.CreateSolidBrush(0, 0, 0, 100), barP1color, new Rectangle(barP1x, barP1y, barP1x + barP1width, barP1y + barP1height), 1, (float)wallP1Perc);
                        }

                        if (chkWallP2.Checked)
                        {
                            graphics.DrawBox2D(graphics.CreateSolidBrush(Color.Transparent), graphics.CreateSolidBrush(0, 0, 0, 100), new Rectangle(barP2x, barP2y, barP2x + barP2width, barP2y + barP2height), 1);
                            graphics.DrawVerticalProgressBar(graphics.CreateSolidBrush(0, 0, 0, 125), barP2color, new Rectangle(barP2x, barP2y, barP2x + barP2width, barP2y + barP2height), 1, (float)wallP2Perc);
                        }
                    }

                    graphics.EndScene();

                }
                catch (Exception) 
                {
                    wallP1Perc = 0;
                    wallP2Perc = 0;
                }


            }
        }

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(linkLabel1.Text);
            Process.Start(sInfo);
        }
    }

    public static class MathHelper
    {
        public static T Clamp<T>(T aValue, T aMin, T aMax) where T : IComparable<T>
        {
            var _Result = aValue;
            if (aValue.CompareTo(aMax) > 0)
                _Result = aMax;
            else if (aValue.CompareTo(aMin) < 0)
                _Result = aMin;
            return _Result;
        }

    }

}
