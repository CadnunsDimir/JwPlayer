using JwPlayer.Models;
using JwPlayer.Service;
using JwPlayer.Utils;
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

namespace JwPlayer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private JwMusicasService _service;
        private List<Cantico> _lista;
        private Mp3Player _player;
        private static bool JaExibindoPergunta = false;
        public MainWindow()
        {
            InitializeComponent();
            _service = new JwMusicasService();
            dgMusicas.ItemsSource = _lista = _service.GetCanticos();
            _player = new Mp3Player();
        }

        protected void ListItem_dbClick(object sender, MouseButtonEventArgs e)
        {
            var cantico = (Cantico)((ListViewItem)e.Source).Content;
            tboxCantico.Text = cantico.Numero.ToString();
            ExibirPerguntaParaTocar(cantico);
        }

        private void ExibirPerguntaParaTocar(Cantico cantico)
        {
            if (!JaExibindoPergunta)
            {
                JaExibindoPergunta = true;
                if (MessageBox.Show(cantico.Numero + " -> " + cantico.Titulo, "Cântico selecionado", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    _player.SetStream(cantico.LinkToDownload);
                }
                JaExibindoPergunta = false;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var teclasValidas = new[]{
                Key.NumPad0,Key.NumPad1,Key.NumPad2,Key.NumPad3,Key.NumPad4,Key.NumPad5,Key.NumPad6,Key.NumPad7,Key.NumPad8,Key.NumPad9,
                Key.Return, Key.Enter
            };
            if (!teclasValidas.Contains(e.Key))
            {
                e.Handled = true;
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            
            var tbox = (TextBox)sender;
            tbox.Text = tbox.Text.Trim();
            tbox.IsReadOnly = true;
            if (e.Key == Key.Return && tbox.Text.ContainsOnlyNumbers())
            {
                TocarPrimeiraOcorrencia(tbox.Text.ToInt());
            }
            else if (tbox.Text.ContainsOnlyNumbers())
            {
                dgMusicas.ItemsSource = _lista.Where(x => x.Numero.ToString().StartsWith(tbox.Text));
            }
            else
            {
                e.Handled = false;
            }
            tbox.IsReadOnly = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _player.StopIfPlaying();
        }

        private void bntStart_Click(object sender, RoutedEventArgs e)
        {
            if (tboxCantico.Text.ContainsOnlyNumbers())
            {
                TocarPrimeiraOcorrencia(tboxCantico.Text.ToInt());
            }
        }

        private void TocarPrimeiraOcorrencia(int numero)
        {
            var cantico = _lista.FirstOrDefault(x => x.Numero == numero);

            if (cantico != null)
            {
                ExibirPerguntaParaTocar(cantico);
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (btnPause.Content.ToString() == "Pause")
            {
                _player.Pause();
                btnPause.Content = "Resume";
            }
            else if (btnPause.Content.ToString() == "Resume")
            {
                _player.Resume();
                btnPause.Content = "Pause";
            }
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _player.StopIfPlaying();
        }
    }
}
