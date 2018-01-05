using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TikaOnDotNet.TextExtraction;
using System.Timers;
using System.Windows.Threading;
using System.IO;

namespace PdfFastReader
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        TextExtractor textExtractor;
        TextExtractionResult result;
        int i;
        int tickSpeed;
        int numberOfWords;
        bool startReader;
        bool night;
        string path;
        string source;
        string[] delimiter = new string[] { " ","  ","   ","\t","\n","\f", "\r", "\v" };
        string[] output;
        public MainWindow()
        {
            tickSpeed = 5;
            numberOfWords = 2;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,0,500);
            timer.Tick += OnTimedEvent;
            InitializeComponent();
            minimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
            maximizeButton.Click += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            closeButton.Click += (s, e) => Close();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        { 
            _output.Text = Read(i);
            i += numberOfWords;
        }

        public void PdfToString()
        {
            i = 0;
            textExtractor = new TextExtractor();
            result = textExtractor.Extract(@path);
            source = result.Text;
            output = result.Text.Split(delimiter, StringSplitOptions.None);
        }

      
        string Read(int i)
        {
            int n = 0;
            while(i<output.Length-numberOfWords)
            {
                string send = " ";
                while (n<numberOfWords)
                {
                    n++;
                    
                    send+= output[i + n]+" ";
                   
                   
                }
                return send;
            }
            
            return "The End. ";
        }
        


        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (path != null)
            {

                startReader = !startReader;
                if (!startReader)
                {
                    startButton.Content = "      Start";
                    timer.Stop();
                }
                else
                {

                    timer.Start();
                    startButton.Content = "      Stop";
                }
                if (_output.Text == "The End. ")
                {
                    i = 0;

                }
            }
            else
            {
                MessageBox.Show("Open file first!");
            }
        }




    


        private void ChangeUIColor()
        {
            night = !night;
            //#424242-gray
            //#1b1b1b-dark
            //#6d6d6d-light
            //#ffffff white
            //#000000 black
            //#7da453 dark
            //#aed581 light
            //#f1f8e9 white
            if (!night)
            {
                mainWindow.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                _output.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                speedText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                numberOfWordsText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                fontSizeText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                nightModeText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                startButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                loadButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                loadExpander.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                settingsExpander.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
                minimizeIcon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                maximizeIcon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                closeIcon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                nameText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
            }
            else
            {
                mainWindow.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF303030"));
                _output.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                speedText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                numberOfWordsText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                fontSizeText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                nightModeText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                startButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                loadButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                loadExpander.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                settingsExpander.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFAFAFA"));
                minimizeIcon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF303030"));
                maximizeIcon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF303030"));
                closeIcon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF303030"));
                nameText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF303030"));

            }
        }


       


        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.Content = "      Start";
            timer.Stop();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                path = dlg.FileName;
                PdfToString();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void urlButton_Click(object sender, RoutedEventArgs e)
        {

        }
       
        private void recentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _tickSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tickSpeed = Convert.ToInt32(_tickSpeed.Value);
            timer.Interval = new TimeSpan(0, 0, 0, 0, tickSpeed * 50);
        }

        private void _numberOfWords_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            numberOfWords = Convert.ToInt32(_numberOfWords.Value);
        }

        private void _fontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _output.FontSize = Convert.ToInt32(_fontSize.Value);
        }

        private void nightMode_Click(object sender, RoutedEventArgs e)
        {
            ChangeUIColor();
        }
    }
}
