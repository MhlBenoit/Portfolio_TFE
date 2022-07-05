using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TFE.ViewModel;

namespace TFE.View
{
    //x <summary>
    //! Code contenant des méthodes pour la gestion de la vue v_Application_Purchase
    //x </summary>
    public partial class v_Application_Purchase : UserControl
    {
        public v_Application_Purchase()
        {
            InitializeComponent();
        }

        public v_Application_Purchase(int numero)
        {
            InitializeComponent();
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            TC.ItemContainerStyle = s;
            if (numero == 1)
            {
                this.DataContext = new vm_Application_Purchase();
                TC.SelectedIndex = 0;
                Design(Btn_Deposit, Lightning_2);
            }
            else if (numero == 2)
                TC.SelectedIndex = 1;
            else if (numero == 3)
                TC.SelectedIndex = 2;
            else if (numero == 4)
                TC.SelectedIndex = 3;
            else if (numero == 5)
                TC.SelectedIndex = 4;

            dp_Purchase_New_BornDate.AddHandler(DatePicker.GotFocusEvent, new RoutedEventHandler(Got_Focus_BornDate), true);

            txt_Purchase_Search_Name.Text = "Recherche par \"Nom, Prénom\"";
            txt_Purchase_Search_Phone.Text = "Recherche par téléphone";
            txt_Purchase_Search_Mail.Text = "Recherche par email";

            txt_Listing_Deposit_Number.Text = "Recherche par numéro";
            txt_Listing_Deposit_Customer.Text = "Recherche par client";
            txt_Listing_Deposit_Date.Text = "Recherche par date";
        }

        #region -- Design --
        private void Design(Button b, Image i)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            // Foreground
            Btn_Deposit.Foreground = Brushes.White;
            Btn_Listing_Deposit.Foreground = Brushes.White;
            Btn_Listing_To_Finalize.Foreground = Brushes.White;
            b.Foreground = c;
            //Image
            Lightning_1.Visibility = Visibility.Hidden;
            Lightning_2.Visibility = Visibility.Hidden;
            Lightning_3.Visibility = Visibility.Hidden;
            i.Visibility = Visibility.Visible;
        }
        #endregion

        #region -- Method for all tab --
        private void Btn_Deposit_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 0;
            Design(Btn_Deposit, Lightning_2);
            Btn_Purchase_Cancel_Click(sender, e);
        }

        private void Btn_Listing_Deposit_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 1;
            Design(Btn_Listing_Deposit, Lightning_1);
        }

        private void Btn_Listing_To_Finalize_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 3;
            Design(Btn_Listing_To_Finalize, Lightning_3);
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

        #region -- DEPOSIT --
        private void Btn_Switch_Brand_Click(object sender, RoutedEventArgs e)
        {
            if (cb_Purchase_Brand.Visibility == Visibility.Visible)
            {
                cb_Purchase_Brand.Visibility = Visibility.Collapsed;
                txt_Purchase_Brand.Visibility = Visibility.Visible;
                Switch_Img_Brand.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));

            }
            else
            {
                cb_Purchase_Brand.Visibility = Visibility.Visible;
                txt_Purchase_Brand.Visibility = Visibility.Collapsed;
                Switch_Img_Brand.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));
            }
        }

        private void Btn_Switch_Category_Click(object sender, RoutedEventArgs e)
        {
            if (cb_Purchase_Category.Visibility == Visibility.Visible)
            {
                cb_Purchase_Category.Visibility = Visibility.Collapsed;
                txt_Purchase_Category.Visibility = Visibility.Visible;
                cb_Purchase_SubCategory.Visibility = Visibility.Collapsed;
                txt_Purchase_SubCategory.Visibility = Visibility.Visible;
                Switch_Img_Category.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));
            }
            else
            {
                cb_Purchase_Category.Visibility = Visibility.Visible;
                txt_Purchase_Category.Visibility = Visibility.Collapsed;
                cb_Purchase_SubCategory.Visibility = Visibility.Visible;
                txt_Purchase_SubCategory.Visibility = Visibility.Collapsed;
                Switch_Img_Category.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));
            }
        }

        private void txt_Purchase_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_Name);
        }

        private void txt_Purchase_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_Name);
        }

        private void txt_Purchase_Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_Quantity);
        }

        private void txt_Purchase_Quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_Quantity);
        }

        private void txt_Purchase_Price_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_Price);
        }

        private void txt_Purchase_Price_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_Price);
        }

        private void cb_Purchase_Brand_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_Brand);
        }

        private void cb_Purchase_Brand_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_Brand);
        }

        private void txt_Purchase_Brand_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_Brand);
        }

        private void txt_Purchase_Brand_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_Brand);
        }

        private void cb_Purchase_Category_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_Category);
        }

        private void cb_Purchase_Category_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_Category);
        }

        private void txt_Purchase_Category_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_Category);
        }

        private void txt_Purchase_Category_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_Category);
        }

        private void cb_Purchase_SubCategory_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_SubCategory);
        }

        private void cb_Purchase_SubCategory_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_SubCategory);
        }

        private void txt_Purchase_SubCategory_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_SubCategory);
        }

        private void txt_Purchase_SubCategory_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_SubCategory);
        }

        private void txt_Purchase_Search_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Purchase_Search_Name, "Recherche par \"Nom, Prénom\"");
            YellowColorBorderBrush(txt_Purchase_Search_Name, Btn_Purchase_Search_Name, 1);
            Purchase_Search_Name_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Purchase_Search_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Purchase_Search_Name, "Recherche par \"Nom, Prénom\"");
            BlueColorBorderBrush(txt_Purchase_Search_Name, Btn_Purchase_Search_Name, 1);
            Purchase_Search_Name_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Purchase_Search_Name_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Purchase_Search_Name, Btn_Purchase_Search_Name, 2);
        }

        private void Purchase_Search_Name_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Purchase_Search_Name.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Purchase_Search_Name, Btn_Purchase_Search_Name, 2);
            }
        }

        private void txt_Purchase_Search_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Purchase_Search_Phone, "Recherche par téléphone");
            YellowColorBorderBrush(txt_Purchase_Search_Phone, Btn_Purchase_Search_Phone, 1);
            Purchase_Search_Phone_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Purchase_Search_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Purchase_Search_Phone, "Recherche par téléphone");
            BlueColorBorderBrush(txt_Purchase_Search_Phone, Btn_Purchase_Search_Phone, 1);
            Purchase_Search_Phone_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Purchase_Search_Phone_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Purchase_Search_Phone, Btn_Purchase_Search_Phone, 2);
        }

        private void Purchase_Search_Phone_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Purchase_Search_Phone.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Purchase_Search_Phone, Btn_Purchase_Search_Phone, 2);
            }
        }

        private void txt_Purchase_Search_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Purchase_Search_Mail, "Recherche par email");
            YellowColorBorderBrush(txt_Purchase_Search_Mail, Btn_Purchase_Search_Mail, 1);
            Purchase_Search_Mail_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Purchase_Search_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Purchase_Search_Mail, "Recherche par email");
            BlueColorBorderBrush(txt_Purchase_Search_Mail, Btn_Purchase_Search_Mail, 1);
            Purchase_Search_Mail_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Purchase_Search_Mail_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Purchase_Search_Mail, Btn_Purchase_Search_Mail, 2);
        }

        private void Purchase_Search_Mail_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Purchase_Search_Mail.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Purchase_Search_Mail, Btn_Purchase_Search_Mail, 2);
            }
        }

        private void txt_Purchase_New_LastName_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_New_LastName);
        }

        private void txt_Purchase_New_LastName_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_New_LastName);
        }

        private void txt_Purchase_New_Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_New_Phone);
        }

        private void txt_Purchase_New_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_New_Phone);
        }

        void Got_Focus_BornDate(object sender, RoutedEventArgs e)
        {
            e.Handled = false;
            Got_Focus(Separator_Purchase_New_BornDate);
        }

        private void dp_Purchase_New_BornDate_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_New_BornDate);
        }

        private void dp_Purchase_New_BornDate_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_New_BornDate);
        }

        private void txt_Purchase_New_City_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_New_City);
        }

        private void txt_Purchase_New_City_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_New_City);
        }

        private void txt_Purchase_New_FirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_New_FirstName);
        }

        private void txt_Purchase_New_FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_New_FirstName);
        }

        private void txt_Purchase_New_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_New_Mail);
        }

        private void txt_Purchase_New_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_New_Mail);
        }

        private void txt_Purchase_New_Address_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_New_Address);
        }

        private void txt_Purchase_New_Address_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_New_Address);
        }

        private void txt_Purchase_New_PostalCode_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Purchase_New_PostalCode);
        }

        private void txt_Purchase_New_PostalCode_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Purchase_New_PostalCode);
        }

        private void Btn_Purchase_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Btn_Purchase_Cancel.IsEnabled = false;
            Btn_Purchase_Cancel.Opacity = 0.5;
            Btn_Purchase_Final.IsEnabled = true;
            Btn_Purchase_Final.Opacity = 1;
            Switch_Img_Final.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));
            cb_Purchase_Brand.Visibility = Visibility.Visible;
            txt_Purchase_Brand.Visibility = Visibility.Collapsed;
            Switch_Img_Brand.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));
            cb_Purchase_Category.Visibility = Visibility.Visible;
            txt_Purchase_Category.Visibility = Visibility.Collapsed;
            cb_Purchase_SubCategory.Visibility = Visibility.Visible;
            txt_Purchase_SubCategory.Visibility = Visibility.Collapsed;
            Switch_Img_Category.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));
        }

        private void Btn_Switch_Final_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_Purchase_Final.IsEnabled == true)
            {
                Btn_Purchase_Cancel.IsEnabled = true;
                Btn_Purchase_Cancel.Opacity = 1;
                Btn_Purchase_Final.IsEnabled = false;
                Btn_Purchase_Final.Opacity = 0.5;
                Switch_Img_Final.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));
            }
            else
            {
                Btn_Purchase_Cancel.IsEnabled = false;
                Btn_Purchase_Cancel.Opacity = 0.5;
                Btn_Purchase_Final.IsEnabled = true;
                Btn_Purchase_Final.Opacity = 1;
                Switch_Img_Final.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));
            }
        }

        private void Listing_Bucket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Bucket.SelectedItem != null)
                Btn_Purchase_Remove_From_List.Visibility = Visibility.Visible;
            else
                Btn_Purchase_Remove_From_List.Visibility = Visibility.Collapsed;
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
        #endregion

        #region -- LISTING DEPOSIT --
        private void txt_Listing_Deposit_Number_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Deposit_Number, "Recherche par numéro");
            YellowColorBorderBrush(txt_Listing_Deposit_Number, Btn_Listing_Deposit_Number, 1);
            Listing_Deposit_1_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Deposit_Number_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Deposit_Number, "Recherche par numéro");
            BlueColorBorderBrush(txt_Listing_Deposit_Number, Btn_Listing_Deposit_Number, 1);
            Listing_Deposit_1_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Listing_Deposit_Number_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Deposit_Number, Btn_Listing_Deposit_Number, 2);
        }

        private void Listing_Deposit_Number_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Deposit_Number.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Deposit_Number, Btn_Listing_Deposit_Number, 2);
            }
        }

        private void txt_Listing_Deposit_Customer_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Deposit_Customer, "Recherche par client");
            YellowColorBorderBrush(txt_Listing_Deposit_Customer, Btn_Listing_Deposit_Customer, 1);
            Listing_Deposit_2_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Deposit_Customer_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Deposit_Customer, "Recherche par client");
            BlueColorBorderBrush(txt_Listing_Deposit_Customer, Btn_Listing_Deposit_Customer, 1);
            Listing_Deposit_2_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Listing_Deposit_Customer_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Deposit_Customer, Btn_Listing_Deposit_Customer, 2);
        }

        private void Listing_Deposit_Customer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Deposit_Customer.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Deposit_Customer, Btn_Listing_Deposit_Customer, 2);
            }
        }

        private void txt_Listing_Deposit_Date_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Listing_Deposit_Date, "Recherche par date");
            YellowColorBorderBrush(txt_Listing_Deposit_Date, Btn_Listing_Deposit_Date, 1);
            Listing_Deposit_3_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Listing_Deposit_Date_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Listing_Deposit_Date, "Recherche par date");
            BlueColorBorderBrush(txt_Listing_Deposit_Date, Btn_Listing_Deposit_Date, 1);
            Listing_Deposit_3_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Listing_Deposit_Date_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Listing_Deposit_Date, Btn_Listing_Deposit_Date, 2);
        }

        private void Listing_Deposit_Date_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Listing_Deposit_Date.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Listing_Deposit_Date, Btn_Listing_Deposit_Date, 2);
            }
        }

        private void Btn_Listing_Deposit_Consult_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 2;
        }


        private void Listing_Deposit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Deposit.SelectedItem != null)
            {
                Btn_Listing_Deposit_Consult.IsEnabled = true;
                Btn_Listing_Deposit_Consult.Opacity = 1;
            }
            else
            {
                Btn_Listing_Deposit_Consult.IsEnabled = false;
                Btn_Listing_Deposit_Consult.Opacity = 0.5;
            }
        }
        #endregion

        #region -- DETAILS DEPOSIT --
        private void Btn_Export_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 1;
        }
        #endregion

        #region -- LISTING TO FINALIZE --
        private void Btn_Listing_To_Finalize_Action_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 4;
        }

        private void Listing_ToFinalize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_ToFinalize.SelectedItem != null)
            {
                Btn_Listing_To_Finalize_Action.IsEnabled = true;
                Btn_Listing_To_Finalize_Action.Opacity = 1;
            }
            else
            {
                Btn_Listing_To_Finalize_Action.IsEnabled = false;
                Btn_Listing_To_Finalize_Action.Opacity = 0.5;
            }
        }

        #endregion

        #region -- DETAILS TO FINALIZE --
        private void txt_Finalize_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Finalize_Name);
        }

        private void txt_Finalize_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Finalize_Name);
        }

        private void cb_Finalize_TVA_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Finalize_TVA);
        }

        private void cb_Finalize_TVA_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Finalize_TVA);
        }

        private void cb_Finalize_Category_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Finalize_Category);
        }

        private void cb_Finalize_Category_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Finalize_Category);
        }

        private void cb_Finalize_SubCategory_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Finalize_SubCategory);
        }

        private void cb_Finalize_SubCategory_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Finalize_SubCategory);
        }

        private void txt_Finalize_SellingPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Finalize_SellingPrice);
        }

        private void txt_Finalize_SellingPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Finalize_SellingPrice);
        }

        private void txt_Finalize_Promo_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Finalize_Promo);
        }

        private void txt_Finalize_Promo_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Finalize_Promo);
        }

        private void cb_Finalize_Brand_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Finalize_Brand);
        }

        private void cb_Finalize_Brand_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Finalize_Brand);
        }

        private void UpdateTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Btn_Finalize_Deposit != null)
            {
                Btn_Finalize_Deposit.Height = 30;
                Btn_Finalize_Deposit_Label.Content = "";
            }
        }

        private void UpdateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Btn_Finalize_Deposit != null)
            {
                Btn_Finalize_Deposit.Height = 30;
                Btn_Finalize_Deposit_Label.Content = "";
            }
        }
        #endregion

    }
}
