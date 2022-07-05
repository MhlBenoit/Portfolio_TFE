using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TFE.View
{
    //x <summary>
    //! Code contenant des méthodes pour la gestion de la vue v_Application
    //x </summary>
    public partial class v_Application : Window
    {
        public v_Application()
        {
            InitializeComponent();
            if (App.Current.Properties["Rank"].ToString() != "1")
            {
                Btn_Direction.Visibility = Visibility.Collapsed;
                Column_Direction.Width = GridLength.Auto;
                if (App.Current.Properties["Rank"].ToString() != "2")
                {
                    Btn_Purchase.Visibility = Visibility.Collapsed;
                    Column_Purchase.Width = GridLength.Auto;
                }
            }

            Design(Btn_Sale, SaleRectangle_1, SaleRectangle_2);
            Show("Sale", 1);
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
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
                this.WindowState = WindowState.Maximized;
                this.Btn_Maximized.Content = "2";
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.Btn_Maximized.Content = "1";
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        #endregion

        #region -- TOP MENU --
        private void Design(Button b, Rectangle r1, Rectangle r2)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            // Foreground
            Btn_Direction.Foreground = Brushes.White;
            Btn_Customer.Foreground = Brushes.White;
            Btn_Article.Foreground = Brushes.White;
            Btn_Sale.Foreground = Brushes.White;
            Btn_Purchase.Foreground = Brushes.White;
            b.Foreground = c;
            // Rectangle
            DirectionRectangle_1.Visibility = Visibility.Hidden;
            DirectionRectangle_2.Visibility = Visibility.Hidden;
            CustomerRectangle_1.Visibility = Visibility.Hidden;
            CustomerRectangle_2.Visibility = Visibility.Hidden;
            ArticleRectangle_1.Visibility = Visibility.Hidden;
            ArticleRectangle_2.Visibility = Visibility.Hidden;
            SaleRectangle_1.Visibility = Visibility.Hidden;
            SaleRectangle_2.Visibility = Visibility.Hidden;
            PurchaseRectangle_1.Visibility = Visibility.Hidden;
            PurchaseRectangle_2.Visibility = Visibility.Hidden;
            r1.Visibility = Visibility.Visible;
            r2.Visibility = Visibility.Visible;
        }

        private void Show(string page, int numero)
        {
            if (page == "Direction")
            {
                this.DataContext = new v_Application_Direction(numero);
                Btn_Direction.IsEnabled = false;
                Btn_Customer.IsEnabled = true;
                Btn_Article.IsEnabled = true;
                Btn_Sale.IsEnabled = true;
                Btn_Purchase.IsEnabled = true;
            }
            else if (page == "Customer")
            {
                this.DataContext = new v_Application_Customer(numero);
                Btn_Direction.IsEnabled = true;
                Btn_Customer.IsEnabled = false;
                Btn_Article.IsEnabled = true;
                Btn_Sale.IsEnabled = true;
                Btn_Purchase.IsEnabled = true;
            }
            else if (page == "Article")
            {
                this.DataContext = new v_Application_Article(numero);
                Btn_Direction.IsEnabled = true;
                Btn_Customer.IsEnabled = true;
                Btn_Article.IsEnabled = false;
                Btn_Sale.IsEnabled = true;
                Btn_Purchase.IsEnabled = true;
            }
            else if (page == "Sale")
            {
                this.DataContext = new v_Application_Sale(numero);
                Btn_Direction.IsEnabled = true;
                Btn_Customer.IsEnabled = true;
                Btn_Article.IsEnabled = true;
                Btn_Sale.IsEnabled = false;
                Btn_Purchase.IsEnabled = true;
            }
            else if (page == "Purchase")
            {
                this.DataContext = new v_Application_Purchase(numero);
                Btn_Direction.IsEnabled = true;
                Btn_Customer.IsEnabled = true;
                Btn_Article.IsEnabled = true;
                Btn_Sale.IsEnabled = true;
                Btn_Purchase.IsEnabled = false;
            }
        }
        #endregion

        private void Btn_Direction_Click(object sender, RoutedEventArgs e)
        {
            Design(Btn_Direction, DirectionRectangle_1, DirectionRectangle_2);
            Show("Direction", 2);
        }

        private void Btn_Customer_Click(object sender, RoutedEventArgs e)
        {
            Design(Btn_Customer, CustomerRectangle_1, CustomerRectangle_2);
            Show("Customer", 2);
        }

        private void Btn_Article_Click(object sender, RoutedEventArgs e)
        {
            Design(Btn_Article, ArticleRectangle_1, ArticleRectangle_2);
            Show("Article", 2);
        }

        private void Btn_Sale_Click(object sender, RoutedEventArgs e)
        {
            Design(Btn_Sale, SaleRectangle_1, SaleRectangle_2);
            Show("Sale", 1);
        }

        private void Btn_Purchase_Click(object sender, RoutedEventArgs e)
        {
            Design(Btn_Purchase, PurchaseRectangle_1, PurchaseRectangle_2);
            Show("Purchase", 1);
        }
    }
}