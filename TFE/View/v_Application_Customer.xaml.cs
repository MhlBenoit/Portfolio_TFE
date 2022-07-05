using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TFE.ViewModel;

namespace TFE.View
{
    //x <summary>
    //! Code contenant des méthodes pour la gestion de la vue v_Application_Customer
    //x </summary>
    public partial class v_Application_Customer : UserControl
    {
        public v_Application_Customer()
        {
            InitializeComponent();
        }

        public v_Application_Customer(int numero)
        {
            InitializeComponent();
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            TC.ItemContainerStyle = s;
            if (numero == 1)
                TC.SelectedIndex = 0;
            else if (numero == 2)
            {
                this.DataContext = new vm_Application_Customer();
                TC.SelectedIndex = 1;
                Design(Btn_Listing, Lightning_2);
            }
            else if (numero == 3)
                TC.SelectedIndex = 2;

            Dp_Add_BornDate.AddHandler(DatePicker.GotFocusEvent, new RoutedEventHandler(Got_Focus_BornDate), true);

            txt_Listing_Search_Name.Text = "Recherche par nom ou prénom";
            txt_Listing_Search_Phone.Text = "Recherche par n° de téléphone";
            txt_Listing_Search_Mail.Text = "Recherche par adresse email";
        }

        #region -- Design --
        private void Design(Button b, Image i)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            // Foreground
            Btn_Listing.Foreground = Brushes.White;
            Btn_Add.Foreground = Brushes.White;
            b.Foreground = c;
            //Image
            Lightning_1.Visibility = Visibility.Hidden;
            Lightning_2.Visibility = Visibility.Hidden;
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
            Design(Btn_Add, Lightning_1);
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

        private void txt_Add_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Phone);
        }

        private void txt_Add_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Phone);
        }

        void Got_Focus_BornDate(object sender, RoutedEventArgs e)
        {
            e.Handled = false;
            Got_Focus(Separator_Add_BornDate);
        }

        private void Dp_Add_BornDate_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_BornDate);
        }

        private void Dp_Add_BornDate_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_BornDate);
        }

        private void txt_Add_City_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_City);
        }

        private void txt_Add_City_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_City);
        }

        private void txt_Add_Firstname_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Firstname);
        }

        private void txt_Add_Firstname_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Firstname);
        }

        private void txt_Add_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Mail);
        }

        private void txt_Add_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Mail);
        }

        private void txt_Add_Address_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Address);
        }

        private void txt_Add_Address_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Address);
        }

        private void txt_Add_PostalCode_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_PostalCode);
        }

        private void txt_Add_PostalCode_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_PostalCode);
        }
        #endregion

        #region -- DETAILS --
        private void txt_Details_Lastname_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_Lastname);
        }

        private void txt_Details_Lastname_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_Lastname);
        }

        private void txt_Details_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_Phone);
        }

        private void txt_Details_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_Phone);
        }

        private void Dp_Details_BornDate_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_BornDate);
        }

        private void Dp_Details_BornDate_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_BornDate);
        }

        private void txt_Details_City_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_City);
        }

        private void txt_Details_City_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_City);
        }

        private void txt_Details_Firstname_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_Firstname);
        }

        private void txt_Details_Firstname_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_Firstname);
        }

        private void txt_Details_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_Mail);
        }

        private void txt_Details_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_Mail);
        }

        private void txt_Details_Address_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_Address);
        }

        private void txt_Details_Address_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_Address);
        }

        private void txt_Details_PostalCode_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_PostalCode);
        }

        private void txt_Details_PostalCode_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_PostalCode);
        }

        private void Btn_Details_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 1;
        }

        private void UpdateTextChanged_Details(object sender, TextChangedEventArgs e)
        {
            if (Btn_Details_Modify != null)
            {
                Btn_Details_Modify.Height = 30;
                Btn_Details_Modify_Label.Content = "";
            }
        }

        private void UpdateSelectionChanged_Details(object sender, SelectionChangedEventArgs e)
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
            TC.SelectedIndex = 2;
        }

        private void RemoveText(TextBox t, string text)
        {
            if (t.Text == text)
                t.Text = "";
        }

        private void AddText(TextBox t, string text)
        {
            if (string.IsNullOrWhiteSpace(t.Text))
                t.Text = text;
        }

        private void YellowColorBorderBrush(TextBox t, Button b, int n)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            if (n == 1)
            {
                t.BorderBrush = c;
                b.BorderBrush = c;
                t.Foreground = c;
                b.Foreground = c;
            }
            else if (n == 2)
            {
                t.BorderBrush = c;
                b.BorderBrush = c;
            }
        }

        private void BlueColorBorderBrush(TextBox t, Button b, int n)
        {
            var c = new SolidColorBrush(Color.FromRgb(85, 93, 143));
            if (n == 1)
            {
                t.BorderBrush = c;
                b.BorderBrush = c;
                t.Foreground = c;
                b.Foreground = c;
            }
            else if (n == 2)
            {
                t.BorderBrush = c;
                b.BorderBrush = c;
            }
        }

        private void txt_Listing_Search_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Search_Name, "Recherche par nom ou prénom");
            YellowColorBorderBrush(txt_Listing_Search_Name, Btn_Listing_Search_Name, 1);
            Search_Name_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Search_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Search_Name, "Recherche par nom ou prénom");
            BlueColorBorderBrush(txt_Listing_Search_Name, Btn_Listing_Search_Name, 1);
            Search_Name_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Name_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Search_Name, Btn_Listing_Search_Name, 2);
        }

        private void Search_Name_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Search_Name.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Search_Name, Btn_Listing_Search_Name, 2);
            }
        }

        private void txt_Listing_Search_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Search_Phone, "Recherche par n° de téléphone");
            YellowColorBorderBrush(txt_Listing_Search_Phone, Btn_Listing_Search_Phone, 1);
            Search_Phone_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Search_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Search_Phone, "Recherche par n° de téléphone");
            BlueColorBorderBrush(txt_Listing_Search_Phone, Btn_Listing_Search_Phone, 1);
            Search_Phone_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Phone_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Search_Phone, Btn_Listing_Search_Phone, 2);
        }

        private void Search_Phone_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Search_Phone.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Search_Phone, Btn_Listing_Search_Phone, 2);
            }
        }

        private void txt_Listing_Search_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Search_Mail, "Recherche par adresse email");
            YellowColorBorderBrush(txt_Listing_Search_Mail, Btn_Listing_Search_Mail, 1);
            Search_Mail_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Search_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Search_Mail, "Recherche par adresse email");
            BlueColorBorderBrush(txt_Listing_Search_Mail, Btn_Listing_Search_Mail, 1);
            Search_Mail_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Mail_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Search_Mail, Btn_Listing_Search_Mail, 2);
        }

        private void Search_Mail_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Search_Mail.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Search_Mail, Btn_Listing_Search_Mail, 2);
            }
        }

        private void Listing_Customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Customer.SelectedItem != null)
            {
                Btn_Listing_Consult_Detail.IsEnabled = true;
                Btn_Listing_Consult_Detail.Opacity = 1;
                Btn_Listing_Delete.IsEnabled = true;
                Btn_Listing_Delete.Opacity = 1;
            }
            else
            {
                Btn_Listing_Consult_Detail.IsEnabled = false;
                Btn_Listing_Consult_Detail.Opacity = 0.5;
                Btn_Listing_Delete.IsEnabled = false;
                Btn_Listing_Delete.Opacity = 0.5;
            }
        }

        #endregion

    }
}
