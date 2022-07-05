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
    //! Code contenant des méthodes pour la gestion de la vue v_Application_Article
    //x </summary>
    public partial class v_Application_Article : UserControl
    {
        public v_Application_Article()
        {
            InitializeComponent();
        }

        public v_Application_Article(int numero)
        {
            InitializeComponent();
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            TC.ItemContainerStyle = s;
            if (numero == 1)
                TC.SelectedIndex = 0;
            else if (numero == 2)
            {
                this.DataContext = new vm_Application_Article();
                TC.SelectedIndex = 1;
                Design(Btn_Listing, Lightning_2);
            }
            else if (numero == 3)
                TC.SelectedIndex = 2;
            else if (numero == 4)
                TC.SelectedIndex = 3;

            txt_Listing_Search_Name.Text = "Recherche par nom";
            txt_Listing_Search_EAN.Text = "Recherche par code EAN";
            txt_Listing_Search_Brand.Text = "Recherche par marque";
            txt_Bcs_Search_Brand.Text = "Recherche par marque";
            txt_Bcs_Search_Category.Text = "Recherche par catégorie";
            txt_Bcs_Search_SubCategory.Text = "Recherche par sous-catégorie";
        }

        #region -- Design --
        private void Design(Button b, Image i)
        {
            var c = new SolidColorBrush(Color.FromRgb(237, 198, 100));
            // Foreground
            Btn_Listing.Foreground = Brushes.White;
            Btn_Add.Foreground = Brushes.White;
            Btn_Brand_Category.Foreground = Brushes.White;
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
            Design(Btn_Add, Lightning_1);
        }

        private void Btn_Brand_Category_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 3;
            Design(Btn_Brand_Category, Lightning_3);
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
        private void txt_Add_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Name);
        }

        private void txt_Add_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Name);
        }

        private void txt_Add_EAN_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_EAN);
        }

        private void txt_Add_EAN_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_EAN);
        }

        private void txt_Add_Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Quantity);
        }

        private void txt_Add_Quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Quantity);
        }

        private void txt_Add_PurchasePrice_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_PurchasePrice);
        }

        private void txt_Add_PurchasePrice_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_PurchasePrice);
        }

        private void cb_Add_TVA_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_TVA);
        }

        private void cb_Add_TVA_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_TVA);
        }

        private void cb_Add_Category_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Category);
        }

        private void cb_Add_Category_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Category);
        }

        private void cb_Add_SubCategory_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_SubCategory);
        }

        private void cb_Add_SubCategory_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_SubCategory);
        }

        private void txt_Add_SellingPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_SellingPrice);
        }

        private void txt_Add_SellingPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_SellingPrice);
        }

        private void txt_Add_Promo_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Promo);
        }

        private void txt_Add_Promo_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Promo);
        }

        private void cb_Add_Brand_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Add_Brand);
        }

        private void cb_Add_Brand_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Add_Brand);
        }

        private void Btn_Listing_Search_FileOrder_Click(object sender, RoutedEventArgs e)
        {
            v_Application_Article_Multiple v = new v_Application_Article_Multiple();
            v.Show();
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
        #endregion

        #region -- DETAILS --
        private void txt_Details_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_Name);
        }

        private void txt_Details_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_Name);
        }

        private void cb_Details_Brand_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_Brand);
        }

        private void cb_Details_Brand_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_Brand);
        }

        private void cb_Details_Category_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_Category);
        }

        private void cb_Details_Category_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_Category);
        }

        private void cb_Details_SubCategory_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Details_SubCategory);
        }

        private void cb_Details_SubCategory_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Details_SubCategory);
        }

        private void Btn_Details_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TC.SelectedIndex = 1;
        }

        private void txt_Details_New_Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_New_Quantity);
        }

        private void txt_Details_New_Quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_New_Quantity);
        }

        private void txt_Details_New_PurchasePrice_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_New_PurchasePrice);
        }

        private void txt_Details_New_PurchasePrice_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_New_PurchasePrice);
        }

        private void cb_Details_New_TVA_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_New_TVA);
        }

        private void cb_Details_New_TVA_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_New_TVA);
        }

        private void txt_Details_New_SellingPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_New_SellingPrice);
        }

        private void txt_Details_New_SellingPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_New_SellingPrice);
        }

        private void txt_Details_New_Promo_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_New_Promo);
        }

        private void txt_Details_New_Promo_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_New_Promo);
        }

        private void Listing_Article_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Article.SelectedItem != null)
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

        private void UpdatePriceChanged(object sender, TextChangedEventArgs e)
        {
            if (Btn_Details_ModifyPrice != null)
            {
                Btn_Details_ModifyPrice.Height = 30;
                Btn_Details_ModifyPrice_Label.Content = "";
            }
        }

        private void UpdateSelectionPriceChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Btn_Details_ModifyPrice != null)
            {
                Btn_Details_ModifyPrice.Height = 30;
                Btn_Details_ModifyPrice_Label.Content = "";
            }
        }
        #endregion

        #region -- BRAND, CATEGORY & SUB-CATEGORY --
        private void txt_Bcs_Search_Brand_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Bcs_Search_Brand, "Recherche par marque");
            YellowColorBorderBrush(txt_Bcs_Search_Brand, Btn_Bcs_Search_Brand, 1);
            Search_Bcs_Brand_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Bcs_Search_Brand_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Bcs_Search_Brand, "Recherche par marque");
            BlueColorBorderBrush(txt_Bcs_Search_Brand, Btn_Bcs_Search_Brand, 1);
            Search_Bcs_Brand_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Bcs_Brand_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Bcs_Search_Brand, Btn_Bcs_Search_Brand, 2);
        }

        private void Search_Bcs_Brand_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Bcs_Search_Brand.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Bcs_Search_Brand, Btn_Bcs_Search_Brand, 2);
            }
        }

        private void txt_Bcs_Search_Category_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Bcs_Search_Category, "Recherche par catégorie");
            YellowColorBorderBrush(txt_Bcs_Search_Category, Btn_Bcs_Search_Category, 1);
            Search_Bcs_Category_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Bcs_Search_Category_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Bcs_Search_Category, "Recherche par catégorie");
            BlueColorBorderBrush(txt_Bcs_Search_Category, Btn_Bcs_Search_Category, 1);
            Search_Bcs_Category_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Bcs_Category_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Bcs_Search_Category, Btn_Bcs_Search_Category, 2);
        }

        private void Search_Bcs_Category_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Bcs_Search_Category.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Bcs_Search_Category, Btn_Bcs_Search_Category, 2);
            }
        }

        private void txt_Bcs_Search_SubCategory_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(txt_Bcs_Search_SubCategory, "Recherche par sous-catégorie");
            YellowColorBorderBrush(txt_Bcs_Search_SubCategory, Btn_Bcs_Search_SubCategory, 1);
            Search_Bcs_SubCategory_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Yellow.png", UriKind.Relative));
        }

        private void txt_Bcs_Search_SubCategory_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(txt_Bcs_Search_SubCategory, "Recherche par sous-catégorie");
            BlueColorBorderBrush(txt_Bcs_Search_SubCategory, Btn_Bcs_Search_SubCategory, 1);
            Search_Bcs_SubCategory_Img.Source = new BitmapImage(new Uri("/TFE;component/Resources/loupe_Blue.png", UriKind.Relative));
        }

        private void Search_Bcs_SubCategory_MouseEnter(object sender, MouseEventArgs e)
        {
            YellowColorBorderBrush(txt_Bcs_Search_SubCategory, Btn_Bcs_Search_SubCategory, 2);
        }

        private void Search_Bcs_SubCategory_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txt_Bcs_Search_SubCategory.IsFocused != true)
            {
                BlueColorBorderBrush(txt_Bcs_Search_SubCategory, Btn_Bcs_Search_SubCategory, 2);
            }
        }

        private void txt_Bcsc_Brand_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Bcsc_Brand);
        }

        private void txt_Bcsc_Brand_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Bcsc_Brand);
        }

        private void txt_Bcsc_Category_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Bcsc_Category);
        }

        private void txt_Bcsc_Category_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Bcsc_Category);
        }

        private void txt_Bcsc_SubCategory_GotFocus(object sender, RoutedEventArgs e)
        {
            Got_Focus(Separator_Bcsc_SubCategory);
        }

        private void txt_Bcsc_SubCategory_LostFocus(object sender, RoutedEventArgs e)
        {
            Lost_Focus(Separator_Bcsc_SubCategory);
        }

        private void Btn_Switch_Brand_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Bcsc_Brand.Visibility == Visibility.Visible)
            {
                Listing_Brand.SelectedItem = null;

                txt_error_brand.Text = string.Empty;
                txt_valid_brand.Content = string.Empty;

                txt_Bcsc_Brand.Visibility = Visibility.Collapsed;
                txt_Bcsc_Brand_New.Visibility = Visibility.Visible;
                Switch_Img_Brand.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));

                Btn_Bcsc_Add_Brand.IsEnabled = true;
                Btn_Bcsc_Add_Brand.Opacity = 1;
                Btn_Bcsc_Modify_Brand.IsEnabled = false;
                Btn_Bcsc_Modify_Brand.Opacity = 0.5;
                Btn_Bcsc_Delete_Brand.IsEnabled = false;
                Btn_Bcsc_Delete_Brand.Opacity = 0.5;
            }
        }

        private void Btn_Switch_Category_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Bcsc_Category.Visibility == Visibility.Visible)
            {
                Listing_Category.SelectedItem = null;

                txt_error_category.Text = string.Empty;
                txt_valid_category.Content = string.Empty;

                txt_Bcsc_Category.Visibility = Visibility.Collapsed;
                txt_Bcsc_Category_New.Visibility = Visibility.Visible;
                Switch_Img_Category.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));

                Btn_Bcsc_Add_Category.IsEnabled = true;
                Btn_Bcsc_Add_Category.Opacity = 1;
                Btn_Bcsc_Modify_Category.IsEnabled = false;
                Btn_Bcsc_Modify_Category.Opacity = 0.5;
                Btn_Bcsc_Delete_Category.IsEnabled = false;
                Btn_Bcsc_Delete_Category.Opacity = 0.5;
            }
        }

        private void Btn_Switch_SubCategory_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Bcsc_SubCategory.Visibility == Visibility.Visible)
            {
                Listing_SubCategory.SelectedItem = null;

                txt_error_subcategory.Text = string.Empty;
                txt_valid_subcategory.Content = string.Empty;

                txt_Bcsc_SubCategory.Visibility = Visibility.Collapsed;
                txt_Bcsc_SubCategory_New.Visibility = Visibility.Visible;
                Switch_Img_SubCategory.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));

                Btn_Bcsc_Add_SubCategory.IsEnabled = true;
                Btn_Bcsc_Add_SubCategory.Opacity = 1;
                Btn_Bcsc_Modify_SubCategory.IsEnabled = false;
                Btn_Bcsc_Modify_SubCategory.Opacity = 0.5;
                Btn_Bcsc_Delete_SubCategory.IsEnabled = false;
                Btn_Bcsc_Delete_SubCategory.Opacity = 0.5;
            }
        }

        private void Listing_Brand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Brand.SelectedItem != null)
            {
                txt_Bcsc_Brand.Visibility = Visibility.Visible;
                txt_Bcsc_Brand_New.Visibility = Visibility.Collapsed;
                Switch_Img_Brand.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));

                Btn_Bcsc_Add_Brand.IsEnabled = false;
                Btn_Bcsc_Add_Brand.Opacity = 0.5;
                Btn_Bcsc_Modify_Brand.IsEnabled = true;
                Btn_Bcsc_Modify_Brand.Opacity = 1;
                Btn_Bcsc_Delete_Brand.IsEnabled = true;
                Btn_Bcsc_Delete_Brand.Opacity = 1;
            }
            else
            {
                txt_Bcsc_Brand.Visibility = Visibility.Collapsed;
                txt_Bcsc_Brand_New.Visibility = Visibility.Visible;
                Switch_Img_Brand.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));

                Btn_Bcsc_Add_Brand.IsEnabled = true;
                Btn_Bcsc_Add_Brand.Opacity = 1;
                Btn_Bcsc_Modify_Brand.IsEnabled = false;
                Btn_Bcsc_Modify_Brand.Opacity = 0.5;
                Btn_Bcsc_Delete_Brand.IsEnabled = false;
                Btn_Bcsc_Delete_Brand.Opacity = 0.5;
            }
        }

        private void Listing_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_Category.SelectedItem != null)
            {
                txt_Bcsc_Category.Visibility = Visibility.Visible;
                txt_Bcsc_Category_New.Visibility = Visibility.Collapsed;
                Switch_Img_Category.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));

                Btn_Bcsc_Add_Category.IsEnabled = false;
                Btn_Bcsc_Add_Category.Opacity = 0.5;
                Btn_Bcsc_Modify_Category.IsEnabled = true;
                Btn_Bcsc_Modify_Category.Opacity = 1;
                Btn_Bcsc_Delete_Category.IsEnabled = true;
                Btn_Bcsc_Delete_Category.Opacity = 1;
                Btn_Bcsc_Add_SubCategory.IsEnabled = true;
                Btn_Bcsc_Add_SubCategory.Opacity = 1;
            }
            else
            {
                txt_Bcsc_Category.Visibility = Visibility.Collapsed;
                txt_Bcsc_Category_New.Visibility = Visibility.Visible;
                Switch_Img_Category.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));

                Btn_Bcsc_Add_Category.IsEnabled = true;
                Btn_Bcsc_Add_Category.Opacity = 1;
                Btn_Bcsc_Modify_Category.IsEnabled = false;
                Btn_Bcsc_Modify_Category.Opacity = 0.5;
                Btn_Bcsc_Delete_Category.IsEnabled = false;
                Btn_Bcsc_Delete_Category.Opacity = 0.5;
                Btn_Bcsc_Add_SubCategory.IsEnabled = false;
                Btn_Bcsc_Add_SubCategory.Opacity = 0.5;
            }
        }

        private void Listing_SubCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listing_SubCategory.SelectedItem != null)
            {
                txt_Bcsc_SubCategory.Visibility = Visibility.Visible;
                txt_Bcsc_SubCategory_New.Visibility = Visibility.Collapsed;
                Switch_Img_SubCategory.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_1.png", UriKind.Relative));

                Btn_Bcsc_Add_SubCategory.IsEnabled = false;
                Btn_Bcsc_Add_SubCategory.Opacity = 0.5;
                Btn_Bcsc_Modify_SubCategory.IsEnabled = true;
                Btn_Bcsc_Modify_SubCategory.Opacity = 1;
                Btn_Bcsc_Delete_SubCategory.IsEnabled = true;
                Btn_Bcsc_Delete_SubCategory.Opacity = 1;
            }
            else
            {
                txt_Bcsc_SubCategory.Visibility = Visibility.Collapsed;
                txt_Bcsc_SubCategory_New.Visibility = Visibility.Visible;
                Switch_Img_SubCategory.Source = new BitmapImage(new Uri("/TFE;component/Resources/switch_2.png", UriKind.Relative));

                Btn_Bcsc_Add_SubCategory.IsEnabled = true;
                Btn_Bcsc_Add_SubCategory.Opacity = 1;
                Btn_Bcsc_Modify_SubCategory.IsEnabled = false;
                Btn_Bcsc_Modify_SubCategory.Opacity = 0.5;
                Btn_Bcsc_Delete_SubCategory.IsEnabled = false;
                Btn_Bcsc_Delete_SubCategory.Opacity = 0.5;
            }
        }
        #endregion

    }
}
