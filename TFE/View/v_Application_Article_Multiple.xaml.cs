using System.Windows;

namespace TFE.View
{
    //x <summary>
    //! Code contenant des méthodes pour la gestion de la vue v_Application_Article_Multiple
    //x </summary>
    public partial class v_Application_Article_Multiple : Window
    {
        public v_Application_Article_Multiple()
        {
            InitializeComponent();
        }

        #region -- HEADER --
        private void Btn_Minimized_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Btn_Maximized_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 6;
                this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth - 6;
                this.WindowState = WindowState.Maximized;
                this.Btn_Maximized.Content = "2";
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.Btn_Maximized.Content = "1";
            }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        #endregion

    }
}
