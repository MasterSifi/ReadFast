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
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,0,500);
            timer.Tick += OnTimedEvent;
            InitializeComponent();
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
                    start.Content = "Start";
                    timer.Stop();
                }
                else
                {

                    timer.Start();
                    start.Content = "Stop" ;
                }
                if(_output.Text=="The End. ")
                {
                    i = 0;
                   
                }
            }
            else
            {
                MessageBox.Show("Open file first!");
            }
        }

        private void numberOfWords_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            numberOfWords = Convert.ToInt32(_numberOfWords.Value);
        }

        private void tickSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tickSpeed = Convert.ToInt32(_tickSpeed.Value);
            timer.Interval = new TimeSpan(0,0,0,0,tickSpeed * 50);
        }

        private void _open_Click(object sender, RoutedEventArgs e)
        {
            start.Content = "Start";
            timer.Stop();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                path = dlg.FileName;
                PdfToString();
            }
        }


        private void ChangeUIColor()
        {
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
                mainGrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#1b1b1b"));
                titleGrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#424242"));
                titleText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
                _output.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
                _open.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
                start.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
                speedLabel.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
                wordsLabel.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
                nightMode.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
            }
            else
            {
                mainGrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f1f8e9"));
                titleGrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7da453"));
                titleText.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
                _output.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
                _open.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
                start.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
                speedLabel.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
                wordsLabel.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
                nightMode.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
            }
        }

        private void nightMode_Click(object sender, RoutedEventArgs e)
        {
            night = !night;
            ChangeUIColor();
        }
    }
}
