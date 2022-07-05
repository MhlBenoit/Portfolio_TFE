using System;
using System.Windows;
using System.Windows.Media;
using TFE.ViewModel;

namespace TFE.View
{
    //x <summary>
    //! Code contenant des méthodes pour la gestion de la vue v_Login_Connexion
    //x </summary>
    public partial class v_Login_Connexion : Window
    {
        public v_Login_Connexion()
        {
            InitializeComponent();

            vm_Login_Connexion vm_lc = new vm_Login_Connexion();
            this.DataContext = vm_lc;
            if (vm_lc.CloseAction == null)
                vm_lc.CloseAction = new Action(this.Close);
        }

        private void Btn_Minimized_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            Separator1.Background = c;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var c = new SolidColorBrush(Color.FromRgb(71, 77, 115));
            Separator1.Background = c;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            Separator2.Background = c;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var c = new SolidColorBrush(Color.FromRgb(71, 77, 115));
            Separator2.Background = c;
        }
    }
}
