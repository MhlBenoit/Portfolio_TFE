using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TFE.ViewModel;

namespace TFE.View
{
    //x <summary>
    //! Code contenant des méthodes pour la gestion de la vue v_Application_Direction
    //x </summary>
    public partial class v_Application_Direction : UserControl
    {
        public v_Application_Direction()
        {
            InitializeComponent();
        }

        public v_Application_Direction(int numero)
        {
            InitializeComponent();
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            TC.ItemContainerStyle = s;
            if (numero == 1)
                TC.SelectedIndex = 0;
            else if (numero == 2)
            {
                this.DataContext = new vm_Application_Direction();
                TC.SelectedIndex = 1;
                Design(Btn_Listing, Lightning_2);
            }
            else if (numero == 3)
                TC.SelectedIndex = 2;
            else if (numero == 4)
                TC.SelectedIndex = 3;

            Dp_Start.AddHandler(DatePicker.GotFocusEvent, new RoutedEventHandler(Got_Focus_1), true);
            Dp_End.AddHandler(DatePicker.GotFocusEvent, new RoutedEventHandler(Got_Focus_2), true);
        }

        #region -- Design --
        private void Design(Button b, Image i)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            // Foreground
            Btn_Listing.Foreground = Brushes.White;
            Btn_Add.Foreground = Brushes.White;
            Btn_Management.Foreground = Brushes.White;
            b.Foreground = c;
            //Image
            Lightning_1.Visibility = Visibility.Hidden;
            Lightning_2.Visibility = Visibility.Hidden;
            Lightning_3.Visibility = Visibility.Hidden;
            i.Visibility = Visibility.Visible;
        }
        #endregion

        #region -- Method for all tab --
        private void Btn_Listing_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 1;
            Design(Btn_Listing, Lightning_2);
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 0;
            txt_Add_Lastname.Text = "";
            txt_Add_Firstname.Text = "";
            txt_Add_Login.Text = "";
            txt_Add_Mail.Text = "";
            txt_Add_Phone.Text = "";
            cb_Add_Rank.SelectedItem = null;
            Design(Btn_Add, Lightning_1);
        }

        private void Btn_Management_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 3;
            Design(Btn_Management, Lightning_3);
        }

        private void Got_Focus(Separator separator)
        {
            var scb = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            separator.Background = scb;
        }

        private void Lost_Focus(Separator separator)
        {
            var scb = new SolidColorBrush(Color.FromRgb(71, 77, 115));
            separator.Background = scb;
        }

        private void Int_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (e.Text)
            {
                case "1": e.Handled = false; break;
                case "2": e.Handled = false; break;
                case "3": e.Handled = false; break;
                case "4": e.Handled = false; break;
                case "5": e.Handled = false; break;
                case "6": e.Handled = false; break;
                case "7": e.Handled = false; break;
                case "8": e.Handled = false; break;
                case "9": e.Handled = false; break;
                case "0": e.Handled = false; break;
                default: e.Handled = true; break;
            }
        }

        private void Decimal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (e.Text)
            {
                case "1": e.Handled = false; break;
                case "2": e.Handled = false; break;
                case "3": e.Handled = false; break;
                case "4": e.Handled = false; break;
                case "5": e.Handled = false; break;
                case "6": e.Handled = false; break;
                case "7": e.Handled = false; break;
                case "8": e.Handled = false; break;
                case "9": e.Handled = false; break;
                case "0": e.Handled = false; break;
                case ",": e.Handled = false; break;
                case ".": e.Handled = false; break;
                default: e.Handled = true; break;
            }
        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (e.Text)
            {
                case "1": e.Handled = false; break;
                case "2": e.Handled = false; break;
                case "3": e.Handled = false; break;
                case "4": e.Handled = false; break;
                case "5": e.Handled = false; break;
                case "6": e.Handled = false; break;
                case "7": e.Handled = false; break;
                case "8": e.Handled = false; break;
                case "9": e.Handled = false; break;
                case "0": e.Handled = false; break;
                case "/": e.Handled = false; break;
                case ".": e.Handled = false; break;
                default: e.Handled = true; break;
            }
        }

        private void Date_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (e.Text)
            {
                case "1": e.Handled = false; break;
                case "2": e.Handled = false; break;
                case "3": e.Handled = false; break;
                case "4": e.Handled = false; break;
                case "5": e.Handled = false; break;
                case "6": e.Handled = false; break;
                case "7": e.Handled = false; break;
                case "8": e.Handled = false; break;
                case "9": e.Handled = false; break;
                case "0": e.Handled = false; break;
                case "/": e.Handled = false; break;
                default: e.Handled = true; break;
            }
        }

        #endregion

        #region -- ADD --
        private void txt_Add_Lastname_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Lastname);
        }

        private void txt_Add_Lastname_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Lastname);
        }

        private void txt_Add_Firstname_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Firstname);
        }

        private void txt_Add_Firstname_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Firstname);
        }

        private void txt_Add_Login_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Login);
        }

        private void txt_Add_Login_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Login);
        }

        private void txt_Add_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Phone);
        }

        private void txt_Add_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Phone);
        }

        private void txt_Add_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Mail);
        }

        private void txt_Add_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Mail);
        }

        private void cb_Add_Rank_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Rank);
        }

        private void cb_Add_Rank_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Rank);
        }

        private void AddTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Btn_Add_Action != null)
            {
                Btn_Add_Action.Height = 30;
                Btn_Add_Action_Content.Content = "";
            }
        }

        private void AddSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Btn_Add_Action != null)
            {
                Btn_Add_Action.Height = 30;
                Btn_Add_Action_Content.Content = "";
            }
        }

        #endregion

        #region -- DETAILS --
        private void txt_Detail_Lastname_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Detail_Lastname);
        }

        private void txt_Detail_Lastname_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Detail_Lastname);
        }

        private void txt_Detail_Firstname_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Detail_Firstname);
        }

        private void txt_Detail_Firstname_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Detail_Firstname);
        }

        private void txt_Detail_Login_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Detail_Login);
        }

        private void txt_Detail_Login_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Detail_Login);
        }

        private void cb_Detail_Rank_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Detail_Rank);
        }

        private void cb_Detail_Rank_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Detail_Rank);
        }

        private void txt_Detail_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Detail_Phone);
        }

        private void txt_Detail_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Detail_Phone);
        }

        private void txt_Detail_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Detail_Mail);
        }

        private void txt_Detail_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Detail_Mail);
        }

        private void Btn_Details_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 1;
        }

        private void UpdateTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Btn_Details_Modify != null)
            {
                Btn_Details_Modify.Height = 30;
                Btn_Details_Modify_Label.Content = "";
            }
        }

        private void UpdateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Btn_Details_Modify != null)
            {
                Btn_Details_Modify.Height = 30;
                Btn_Details_Modify_Label.Content = "";
            }
        }
        #endregion

        #region -- LISTING --
        private void Btn_Listing_Consult_Detail_Click(object sender, RoutedEventArgs e)
        {
            if (Listing_Employee.SelectedItem != null)
            {
                TC.SelectedIndex = 2;
            }
        }

        private void Listing_Employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Employee.SelectedItem != null)
            {
                Btn_Listing_Consult_Detail.IsEnabled = true;
                Btn_Listing_Consult_Detail.Opacity = 1;
                Btn_Listing_Disable.IsEnabled = true;
                Btn_Listing_Disable.Opacity = 1;
            }
            else
            {
                Btn_Listing_Consult_Detail.IsEnabled = false;
                Btn_Listing_Consult_Detail.Opacity = 0.5;
                Btn_Listing_Disable.IsEnabled = false;
                Btn_Listing_Disable.Opacity = 0.5;
            }
        }

        #endregion

        #region -- ACCOUNT MANAGEMENT --
        private void Dp_Start_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Acc_1);
        }

        private void Dp_Start_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Acc_1);
        }

        private void Dp_End_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Acc_2);
        }

        private void Dp_End_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Acc_2);
        }

        void Got_Focus_1(object sender, RoutedEventArgs e)
        {
            e.Handled = false;
            Got_Focus(Separator_Acc_1);
        }

        void Got_Focus_2(object sender, RoutedEventArgs e)
        {
            e.Handled = false;
            Got_Focus(Separator_Acc_2);
        }

        private void Dp_Start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Got_Focus(Separator_Acc_1);
        }

        private void Dp_End_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Got_Focus(Separator_Acc_2);
        }
        #endregion

    }
}
