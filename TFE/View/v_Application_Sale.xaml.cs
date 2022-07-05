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
    //! Code contenant des méthodes pour la gestion de la vue v_Application_Sale
    //x </summary>
    public partial class v_Application_Sale : UserControl
    {
        public v_Application_Sale()
        {
            InitializeComponent();
        }

        public v_Application_Sale(int numero)
        {
            InitializeComponent();
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            TC.ItemContainerStyle = s;
            if (numero == 1)
            {
                this.DataContext = new vm_Application_Sale();
                TC.SelectedIndex = 0;
                Design(Btn_Checkout, Lightning_2);
            }
            else if (numero == 2)
                TC.SelectedIndex = 1;
            else if (numero == 3)
                TC.SelectedIndex = 2;
            else if (numero == 4)
                TC.SelectedIndex = 3;

            dp_Sale_New_BornDate.AddHandler(DatePicker.GotFocusEvent, new RoutedEventHandler(Got_Focus_BornDate), true);

            txt_Listing_Bill_Number.Text = "Recherche par numéro";
            txt_Listing_Bill_Customer.Text = "Recherche par client";
            txt_Listing_Bill_Date.Text = "Recherche par date";

            txt_Listing_Refund_Number.Text = "Recherche par numéro";
            txt_Listing_Refund_Customer.Text = "Recherche par client";
            txt_Listing_Refund_Date.Text = "Recherche par date";

            txt_Sale_Search_Name.Text = "Recherche par \"Nom, Prénom\"";
            txt_Sale_Search_Phone.Text = "Recherche par téléphone";
            txt_Sale_Search_Mail.Text = "Recherche par email";

            txt_Listing_Search_Name.Text = "Recherche par nom";
            txt_Listing_Search_EAN.Text = "Recherche par code EAN";
            txt_Listing_Search_Brand.Text = "Recherche par marque";

        }

        #region -- Design --
        private void Design(Button b, Image i)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            // Foreground
            Btn_Checkout.Foreground = Brushes.White;
            Btn_Listing.Foreground = Brushes.White;
            Btn_Refund.Foreground = Brushes.White;
            b.Foreground = c;
            //Image
            Lightning_1.Visibility = Visibility.Hidden;
            Lightning_2.Visibility = Visibility.Hidden;
            Lightning_3.Visibility = Visibility.Hidden;
            i.Visibility = Visibility.Visible;
        }
        #endregion

        #region -- Method for all tab --
        private void Btn_Checkout_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 0;
            Design(Btn_Checkout, Lightning_2);
        }

        private void Btn_Listing_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 2;
            Design(Btn_Listing, Lightning_1);
        }

        private void Btn_Refund_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 5;
            Design(Btn_Refund, Lightning_3);
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

        #region -- SALE --
        private void txt_Sale_Search_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Sale_Search_Name, "Recherche par \"Nom, Prénom\"");
            YellowColorBorderBrush(txt_Sale_Search_Name, Btn_Sale_Search_Name, 1);
            Sale_Search_Name_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Sale_Search_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Sale_Search_Name, "Recherche par \"Nom, Prénom\"");
            BlueColorBorderBrush(txt_Sale_Search_Name, Btn_Sale_Search_Name, 1);
            Sale_Search_Name_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Sale_Search_Name_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Sale_Search_Name, Btn_Sale_Search_Name, 2);
        }

        private void Sale_Search_Name_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Sale_Search_Name.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Sale_Search_Name, Btn_Sale_Search_Name, 2);
            }
        }

        private void txt_Sale_Search_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Sale_Search_Phone, "Recherche par téléphone");
            YellowColorBorderBrush(txt_Sale_Search_Phone, Btn_Sale_Search_Phone, 1);
            Sale_Search_Phone_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Sale_Search_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Sale_Search_Phone, "Recherche par téléphone");
            BlueColorBorderBrush(txt_Sale_Search_Phone, Btn_Sale_Search_Phone, 1);
            Sale_Search_Phone_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Sale_Search_Phone_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Sale_Search_Phone, Btn_Sale_Search_Phone, 2);
        }

        private void Sale_Search_Phone_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Sale_Search_Phone.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Sale_Search_Phone, Btn_Sale_Search_Phone, 2);
            }
        }

        private void txt_Sale_Search_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Sale_Search_Mail, "Recherche par email");
            YellowColorBorderBrush(txt_Sale_Search_Mail, Btn_Sale_Search_Mail, 1);
            Sale_Search_Mail_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Sale_Search_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Sale_Search_Mail, "Recherche par email");
            BlueColorBorderBrush(txt_Sale_Search_Mail, Btn_Sale_Search_Mail, 1);
            Sale_Search_Mail_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Sale_Search_Mail_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Sale_Search_Mail, Btn_Sale_Search_Mail, 2);
        }

        private void Sale_Search_Mail_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Sale_Search_Mail.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Sale_Search_Mail, Btn_Sale_Search_Mail, 2);
            }
        }

        private void txt_Sale_New_LastName_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Sale_New_LastName);
        }

        private void txt_Sale_New_LastName_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Sale_New_LastName);
        }

        private void txt_Sale_New_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Sale_New_Phone);
        }

        private void txt_Sale_New_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Sale_New_Phone);
        }

        private void txt_Sale_New_Postal_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Sale_New_Postalcode);
        }

        private void txt_Sale_New_Postal_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Sale_New_Postalcode);
        }

        void Got_Focus_BornDate(object sender, RoutedEventArgs e)
        {
            e.Handled = false;
            Got_Focus(Separator_Sale_New_BornDate);
        }

        private void dp_Sale_New_BornDate_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Sale_New_BornDate);
        }

        private void dp_Sale_New_BornDate_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Sale_New_BornDate);
        }

        private void txt_Sale_New_City_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Sale_New_City);
        }

        private void txt_Sale_New_City_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Sale_New_City);
        }

        private void txt_Sale_New_FirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Sale_New_FirstName);
        }

        private void txt_Sale_New_FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Sale_New_FirstName);
        }

        private void txt_Sale_New_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Sale_New_Mail);
        }

        private void txt_Sale_New_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Sale_New_Mail);
        }

        private void txt_Sale_New_Address_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Sale_New_Address);
        }

        private void txt_Sale_New_Address_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Sale_New_Address);
        }

        private void txt_Sale_Code_EAN_GotFocus(object sender, RoutedEventArgs e)
        {
            YellowColorBorderBrush(txt_Sale_Code_EAN, Btn_Sale_Validate_Code_EAN, 1);
            YellowColorBorderBrush(txt_Sale_Code_EAN, Btn_Sale_Search_Code_EAN, 1);
            Sale_Validate_EAN_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/valid_Yellow.png", UriKind.Relative));
            Sale_Search_EAN_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Sale_Code_EAN_LostFocus(object sender, RoutedEventArgs e)
        {
            BlueColorBorderBrush(txt_Sale_Code_EAN, Btn_Sale_Validate_Code_EAN, 1);
            BlueColorBorderBrush(txt_Sale_Code_EAN, Btn_Sale_Search_Code_EAN, 1);
            Sale_Validate_EAN_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/valid_Blue.png", UriKind.Relative));
            Sale_Search_EAN_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Sale_EAN_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Sale_Code_EAN, Btn_Sale_Validate_Code_EAN, 2);
            YellowColorBorderBrush(txt_Sale_Code_EAN, Btn_Sale_Search_Code_EAN, 2);
        }

        private void Sale_EAN_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Sale_Code_EAN.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Sale_Code_EAN, Btn_Sale_Validate_Code_EAN, 2);
                BlueColorBorderBrush(txt_Sale_Code_EAN, Btn_Sale_Search_Code_EAN, 2);
            }
        }

        private void txt_Amount_given_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Amount_given);
        }

        private void txt_Amount_given_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Amount_given);
        }

        private void txt_Promo_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Promo);
        }

        private void txt_Promo_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Promo);
        }

        private void Btn_Sale_Search_Code_EAN_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 1;
        }

        private void Btn_Listing_Select_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 0;
            txt_Sale_Code_EAN.Focus();
        }

        private void Sale_Add_Quantity_Img_MouseEnter(object sender, MouseEventArgs e)
        {
            Sale_Add_Quantity_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/add_Yellow.png", UriKind.Relative));
        }

        private void Sale_Add_Quantity_Img_MouseLeave(object sender, MouseEventArgs e)
        {
            Sale_Add_Quantity_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/add_Blue.png", UriKind.Relative));
        }

        private void Sale_Remove_Quantity_Img_MouseEnter(object sender, MouseEventArgs e)
        {
            Sale_Remove_Quantity_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/remove_Yellow.png", UriKind.Relative));
        }

        private void Sale_Remove_Quantity_Img_MouseLeave(object sender, MouseEventArgs e)
        {
            Sale_Remove_Quantity_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/remove_Blue.png", UriKind.Relative));
        }

        private void Listing_Bucket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Bucket.SelectedItem != null)
            {
                Btn_Sale_Add_Quantity_EAN.Visibility = Visibility.Visible;
                Btn_Sale_Remove_Quantity_EAN.Visibility = Visibility.Visible;
                Label_Change_Quantity.Visibility = Visibility.Visible;
            }
            else
            {
                Btn_Sale_Add_Quantity_EAN.Visibility = Visibility.Collapsed;
                Btn_Sale_Remove_Quantity_EAN.Visibility = Visibility.Collapsed;
                Label_Change_Quantity.Visibility = Visibility.Collapsed;
            }
        }

        private void RadioBtn_Money_Click(object sender, RoutedEventArgs e)
        {
            if (RadioBtn_Money.IsChecked == true)
            {
                txt_Amount_given.Visibility = Visibility.Visible;
                lbl_Amount_given.Visibility = Visibility.Visible;
            }
            else
            {
                txt_Amount_given.Visibility = Visibility.Collapsed;
                lbl_Amount_given.Visibility = Visibility.Collapsed;
            }
        }

        private void Btn_Switch_Final_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_Sale_Final.IsEnabled == true)
            {
                Btn_Sale_Cancel.IsEnabled = true;
                Btn_Sale_Cancel.Opacity = 1;
                Btn_Sale_Final.IsEnabled = false;
                Btn_Sale_Final.Opacity = 0.5;
                Switch_Img_Final.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));
            }
            else
            {
                Btn_Sale_Cancel.IsEnabled = false;
                Btn_Sale_Cancel.Opacity = 0.5;
                Btn_Sale_Final.IsEnabled = true;
                Btn_Sale_Final.Opacity = 1;
                Switch_Img_Final.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));
            }
        }

        private void Btn_Sale_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Btn_Sale_Cancel.IsEnabled = false;
            Btn_Sale_Cancel.Opacity = 0.5;
            Btn_Sale_Final.IsEnabled = true;
            Btn_Sale_Final.Opacity = 1;
            lbl_Amount_given.Visibility = Visibility.Collapsed;
            txt_Amount_given.Visibility = Visibility.Collapsed;
            Switch_Img_Final.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));
        }

        private void CreateCustomerTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Btn_CreateCustomer != null)
            {
                Btn_CreateCustomer.Height = 30;
                Btn_CreateCustomer_Label_bis.Content = "";
                Btn_CreateCustomer_Label.Content = "";
            }
        }

        private void CreateCustomerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Btn_CreateCustomer != null)
            {
                Btn_CreateCustomer.Height = 30;
                Btn_CreateCustomer_Label_bis.Content = "";
                Btn_CreateCustomer_Label.Content = "";
            }
        }

        private void Btn_Listing_Select_Article_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 0;
            txt_Sale_Code_EAN.Focus();
        }
        #endregion

        #region -- LISTING ARTICLES --

        private void txt_Listing_Search_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Search_Name, "Recherche par nom");
            YellowColorBorderBrush(txt_Listing_Search_Name, Btn_Listing_Search_Name, 1);
            Search_Name_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Search_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Search_Name, "Recherche par nom");
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

        private void txt_Listing_Search_EAN_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Search_EAN, "Recherche par code EAN");
            YellowColorBorderBrush(txt_Listing_Search_EAN, Btn_Listing_Search_EAN, 1);
            Search_EAN_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Search_EAN_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Search_EAN, "Recherche par code EAN");
            BlueColorBorderBrush(txt_Listing_Search_EAN, Btn_Listing_Search_EAN, 1);
            Search_EAN_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_EAN_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Search_EAN, Btn_Listing_Search_EAN, 2);
        }

        private void Search_EAN_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Search_EAN.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Search_EAN, Btn_Listing_Search_EAN, 2);
            }
        }

        private void txt_Listing_Search_Brand_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Search_Brand, "Recherche par marque");
            YellowColorBorderBrush(txt_Listing_Search_Brand, Btn_Listing_Search_Brand, 1);
            Search_Brand_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Search_Brand_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Search_Brand, "Recherche par marque");
            BlueColorBorderBrush(txt_Listing_Search_Brand, Btn_Listing_Search_Brand, 1);
            Search_Brand_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Brand_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Search_Brand, Btn_Listing_Search_Brand, 2);
        }

        private void Search_Brand_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Search_Brand.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Search_Brand, Btn_Listing_Search_Brand, 2);
            }
        }

        private void Listing_Article_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Article.SelectedItem != null)
            {
                Btn_Listing_Select_Article.IsEnabled = true;
                Btn_Listing_Select_Article.Opacity = 1;
            }
            else
            {
                Btn_Listing_Select_Article.IsEnabled = false;
                Btn_Listing_Select_Article.Opacity = 0.5;
            }
        }

        #endregion

        #region -- LISTING BILL --
        private void txt_Listing_Bill_Number_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Bill_Number, "Recherche par numéro");
            YellowColorBorderBrush(txt_Listing_Bill_Number, Btn_Listing_Bill_Number, 1);
            Search_Bill_Number_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Bill_Number_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Bill_Number, "Recherche par numéro");
            BlueColorBorderBrush(txt_Listing_Bill_Number, Btn_Listing_Bill_Number, 1);
            Search_Bill_Number_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Bill_Number_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Bill_Number, Btn_Listing_Bill_Number, 2);
        }

        private void Search_Bill_Number_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Bill_Number.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Bill_Number, Btn_Listing_Bill_Number, 2);
            }
        }

        private void txt_Listing_Bill_Customer_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Bill_Customer, "Recherche par client");
            YellowColorBorderBrush(txt_Listing_Bill_Customer, Btn_Listing_Bill_Customer, 1);
            Search_Bill_Customer_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Bill_Customer_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Bill_Customer, "Recherche par client");
            BlueColorBorderBrush(txt_Listing_Bill_Customer, Btn_Listing_Bill_Customer, 1);
            Search_Bill_Customer_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Bill_Customer_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Bill_Customer, Btn_Listing_Bill_Customer, 2);
        }

        private void Search_Bill_Customer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Bill_Customer.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Bill_Customer, Btn_Listing_Bill_Customer, 2);
            }
        }

        private void txt_Listing_Bill_Date_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Bill_Date, "Recherche par date");
            YellowColorBorderBrush(txt_Listing_Bill_Date, Btn_Listing_Bill_Date, 1);
            Search_Bill_Date_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Bill_Date_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Bill_Date, "Recherche par date");
            BlueColorBorderBrush(txt_Listing_Bill_Date, Btn_Listing_Bill_Date, 1);
            Search_Bill_Date_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Bill_Date_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Bill_Date, Btn_Listing_Bill_Date, 2);
        }

        private void Search_Bill_Date_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Bill_Date.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Bill_Date, Btn_Listing_Bill_Date, 2);
            }
        }

        private void Btn_Listing_Consult_Details_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 3;
        }

        private void Btn_Listing_Refund_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 4;
        }

        private void Listing_Bill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Bill.SelectedItem != null)
            {
                Btn_Listing_Consult_Details.IsEnabled = true;
                Btn_Listing_Consult_Details.Opacity = 1;
                Btn_Listing_Refund.IsEnabled = true;
                Btn_Listing_Refund.Opacity = 1;
            }
            else
            {
                Btn_Listing_Consult_Details.IsEnabled = false;
                Btn_Listing_Consult_Details.Opacity = 0.5;
                Btn_Listing_Refund.IsEnabled = false;
                Btn_Listing_Refund.Opacity = 0.5;
            }
        }
        #endregion

        #region -- DETAILS --
        private void Btn_Export_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 2;
        }
        #endregion

        #region -- REFUND --
        private void Btn_Refund_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 2;
        }

        private void Listing_Refund_Bill_Before_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Btn_Refund_Modify.IsEnabled = true;
            Btn_Refund_Modify.Opacity = 1;
            if (Listing_Refund_Bill_Before.SelectedValue != null)
                Listing_Refund_Bill_After.SelectedValue = null;
        }

        private void Listing_Refund_Bill_After_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Btn_Refund_Modify.IsEnabled = true;
            Btn_Refund_Modify.Opacity = 1;
            if (Listing_Refund_Bill_After.SelectedValue != null)
                Listing_Refund_Bill_Before.SelectedValue = null;
        }

        #endregion

        #region -- LISTING REFUND --
        private void txt_Listing_Refund_Number_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Refund_Number, "Recherche par numéro");
            YellowColorBorderBrush(txt_Listing_Refund_Number, Btn_Listing_Refund_Number, 1);
            Search_Refund_Number_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Refund_Number_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Refund_Number, "Recherche par numéro");
            BlueColorBorderBrush(txt_Listing_Refund_Number, Btn_Listing_Refund_Number, 1);
            Search_Refund_Number_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Refund_Number_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Refund_Number, Btn_Listing_Refund_Number, 2);
        }

        private void Search_Refund_Number_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Refund_Number.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Refund_Number, Btn_Listing_Refund_Number, 2);
            }
        }

        private void txt_Listing_Refund_Customer_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Refund_Customer, "Recherche par client");
            YellowColorBorderBrush(txt_Listing_Refund_Customer, Btn_Listing_Refund_Customer, 1);
            Search_Refund_Customer_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Refund_Customer_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Refund_Customer, "Recherche par client");
            BlueColorBorderBrush(txt_Listing_Refund_Customer, Btn_Listing_Refund_Customer, 1);
            Search_Refund_Customer_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Refund_Customer_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Refund_Customer, Btn_Listing_Refund_Customer, 2);
        }

        private void Search_Refund_Customer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Refund_Customer.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Refund_Customer, Btn_Listing_Refund_Customer, 2);
            }
        }

        private void txt_Listing_Refund_Date_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Refund_Date, "Recherche par date");
            YellowColorBorderBrush(txt_Listing_Refund_Date, Btn_Listing_Refund_Date, 1);
            Search_Refund_Date_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Refund_Date_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Refund_Date, "Recherche par date");
            BlueColorBorderBrush(txt_Listing_Refund_Date, Btn_Listing_Refund_Date, 1);
            Search_Refund_Date_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Refund_Date_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Refund_Date, Btn_Listing_Refund_Date, 2);
        }

        private void Search_Refund_Date_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Refund_Date.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Refund_Date, Btn_Listing_Refund_Date, 2);
            }
        }

        private void Listing_Refund_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Refund.SelectedItem != null)
            {
                Btn_Listing_Refund_Export.IsEnabled = true;
                Btn_Listing_Refund_Export.Opacity = 1;
            }
            else
            {
                Btn_Listing_Refund_Export.IsEnabled = false;
                Btn_Listing_Refund_Export.Opacity = 0.5;
            }
        }

        #endregion

    }
}
