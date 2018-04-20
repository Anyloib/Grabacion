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
using NAudio;
using NAudio.Wave;

namespace WpfApp2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WaveIn waveIn;
        WaveFormat formato;
        WaveFileWriter writer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            formato = new WaveFormat(44100, 1);
            waveIn = new WaveIn();

            waveIn.DataAvailable += OnDataAvaiable;
            waveIn.RecordingStopped += OnRecordingStopped;

            writer = new WaveFileWriter("sonido.wav", formato);

            waveIn.StartRecording();
        }

        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {

        }

        void OnDataAvaiable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;
            int bytesGrabados = e.BytesRecorded;

            for(int i=0; i < bytesGrabados; i++)
            {
                lblMuestras.Text = buffer[i].ToString();
            }

            writer.Writer(buffer, 0, bytesGrabados);
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            waveIn.StopRecording();
        }
    }
}
