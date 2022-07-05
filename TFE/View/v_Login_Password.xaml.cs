using System.Windows;
using System.Windows.Media;

namespace TFE.View
{
    //x <summary>
    //! Code contenant des méthodes pour la gestion de la vue v_Login_Password
    //x </summary>
    public partial class v_Login_Password : Window
    {
        public v_Login_Password()
        {
            InitializeComponent();
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

        private void pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txt_Password_New.Text = pwd.Password;
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            if (Validate.Content.ToString() == "Entrer dans l'application")
            {
                Close();
            }
        }
    }
}
