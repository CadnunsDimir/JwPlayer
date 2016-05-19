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
        public MainWindow()
        {
            InitializeComponent();
            _service = new JwMusicasService();
            dgMusicas.ItemsSource = _lista = _service.GetCanticos();
        }

        protected void ListItem_dbClick(object sender, MouseButtonEventArgs e)
        {
            var cantico = (Cantico)((ListViewItem)e.Source).Content;
            MessageBox.Show(cantico.Titulo);
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
            
            if (e.Key == Key.Return && tbox.Text.ContainsOnlyNumbers())
            {
                var cantico = _lista.FirstOrDefault(x => x.Numero == tbox.Text.ToInt());
                if (cantico != null)
                {
                    MessageBox.Show(cantico.Titulo);
                }
            }
            else if (tbox.Text.ContainsOnlyNumbers())
            {
                dgMusicas.ItemsSource = _lista.Where(x => x.Numero.ToString().StartsWith(tbox.Text));
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
