using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using TFE.Model;
using TFE.Tools;
using TFE.View.PDF;
using TFE.ViewModel.PDF;

namespace TFE.ViewModel
{
    //x <summary>
    //! Cette classe contient les propriétés auxquelles la vue v_Application_Sale peut se lier avec le Binding.
    //x </summary>
    public class vm_Application_Sale : ViewModelBase
    {
        #region -- PROPERTIES --
        private Article bucket_Item_Selected;
        private Article article_Selected;
        private Transaction transaction_Sale_Selected;
        private Transaction transaction_Refund_Selected;
        private Sale refund_Before_Selected;
        private Refund refund_After_Selected;
        private Customer customer_Searched;
        private Customer newCustomer;
        private string bucket_Ean;
        private bool isChecked_Bank;
        private bool isChecked_Money;
        private string bucket_Money;
        private string bucket_Promo;
        private string bucket_Sum;
        private bool change_Quantity;
        private string searchName;
        private string searchEAN;
        private string searchBrand;
        private string searchCustomer;
        private string searchPhone;
        private string searchMail;
        private string searchSaleNumber;
        private string searchSaleCustomer;
        private string searchSaleDate;
        private string searchRefundNumber;
        private string searchRefundCustomer;
        private string searchRefundDate;
        private string createNewCustomer_Content;
        private string createNewCustomer_Content_Bis;
        private int createNewCustomer_Height;
        private string validRefund_Content;
        private int validRefund_Height;
        private bool anonymousCustomer;
        private int tabBucket_Selected;
        private bool useLoyalty_IsEnabled;
        private double useLoyalty_Opacity;
        private int? refund_Quantity;
        private decimal? refund_Back;
        private decimal? refund_Total;

        private string error_Bucket_Ean;
        private string error_Bucket_Validate;
        private string error_Customer_Searched;
        private string error_NewCustomer_Lastname;
        private string error_NewCustomer_Firstname;
        private string error_NewCustomer_Borndate;
        private string error_NewCustomer_Phone;
        private string error_NewCustomer_Mail;
        private string error_NewCustomer_Address;
        private string error_NewCustomer_City;
        private string error_NewCustomer_Postalcode;
        private string error_Refund;

        private string transaction_Sale_Number;
        private string transaction_Sale_Employee;
        private string transaction_Sale_Customer;

        private IList<Article> listing_Bucket;
        private IList<Article> listing_Article;
        private IList<Transaction> listing_Transaction_Sale;
        private IList<Sale> listing_Transaction_Sale_Details;
        private IList<Refund> listing_Transaction_Sale_Refund;
        private IList<Transaction> listing_Transaction_Refund;
        #endregion

        #region -- RELAY COMMAND DECLARATION --
        public RelayCommand RefreshBtn { get; private set; }
        public RelayCommand ValidateEanBtn { get; private set; }
        public RelayCommand FilterNameBtn { get; private set; }
        public RelayCommand FilterEANBtn { get; private set; }
        public RelayCommand FilterBrandBtn { get; private set; }
        public RelayCommand FilterCustomerBtn { get; private set; }
        public RelayCommand FilterPhoneBtn { get; private set; }
        public RelayCommand FilterMailBtn { get; private set; }
        public RelayCommand FilterSaleNumberBtn { get; private set; }
        public RelayCommand FilterSaleCustomerBtn { get; private set; }
        public RelayCommand FilterSaleDateBtn { get; private set; }
        public RelayCommand FilterRefundNumberBtn { get; private set; }
        public RelayCommand FilterRefundCustomerBtn { get; private set; }
        public RelayCommand FilterRefundDateBtn { get; private set; }
        public RelayCommand SelectArticleBtn { get; private set; }
        public RelayCommand AddQuantityBtn { get; private set; }
        public RelayCommand RemoveQuantityBtn { get; private set; }
        public RelayCommand CreateCustomerBtn { get; private set; }
        public RelayCommand ValidBucketBtn { get; private set; }
        public RelayCommand ChangeTabBtn { get; private set; }
        public RelayCommand UseLoyaltyBtn { get; private set; }
        public RelayCommand SelectedTransactionSaleAction { get; private set; }
        public RelayCommand ExportTransactionSaleBtn { get; private set; }
        public RelayCommand RefundQuantityBtn { get; private set; }
        public RelayCommand ValidRefundBtn { get; private set; }
        public RelayCommand ExportTransactionRefundBtn { get; private set; }
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_Application_Sale
        //x </summary>
        public vm_Application_Sale()
        {
            ValidateEanBtn = new RelayCommand(ValidateEan);
            FilterNameBtn = new RelayCommand(FilterName);
            FilterEANBtn = new RelayCommand(FilterEAN);
            FilterBrandBtn = new RelayCommand(FilterBrand);
            FilterCustomerBtn = new RelayCommand(FilterCustomer);
            FilterPhoneBtn = new RelayCommand(FilterPhone);
            FilterMailBtn = new RelayCommand(FilterMail);
            FilterSaleNumberBtn = new RelayCommand(FilterSaleNumber);
            FilterSaleCustomerBtn = new RelayCommand(FilterSaleCustomer);
            FilterSaleDateBtn = new RelayCommand(FilterSaleDate);
            FilterRefundNumberBtn = new RelayCommand(FilterRefundNumber);
            FilterRefundCustomerBtn = new RelayCommand(FilterRefundCustomer);
            FilterRefundDateBtn = new RelayCommand(FilterRefundDate);
            SelectArticleBtn = new RelayCommand(SelectArticle);
            AddQuantityBtn = new RelayCommand(AddQuantity);
            RemoveQuantityBtn = new RelayCommand(RemoveQuantity);
            CreateCustomerBtn = new RelayCommand(CreateCustomer);
            ValidBucketBtn = new RelayCommand(ValidBucket);
            ChangeTabBtn = new RelayCommand(ChangeTab);
            UseLoyaltyBtn = new RelayCommand(UseLoyalty);
            SelectedTransactionSaleAction = new RelayCommand(SelectedTransactionSale);
            ExportTransactionSaleBtn = new RelayCommand(ExportTransactionSale);
            RefundQuantityBtn = new RelayCommand(RefundQuantity);
            ValidRefundBtn = new RelayCommand(ValidRefund);
            ExportTransactionRefundBtn = new RelayCommand(ExportTransactionRefund);

            RefreshBtn = new RelayCommand(Refresh);
            Refresh();
        }

        #region -- RELAY COMMAND METHODS --
        private void Refresh()
        {
            Listing_Bucket = Article.Enumeration.Where(x => x.Date > DateTime.Now).ToList();
            Listing_Article = Article.Enumeration.Where(x => x.Price_info_id.Selling_price > 0).ToArray();
            Listing_Transaction_Sale = Transaction.Enumeration.Where(x => x.Type == 1).OrderByDescending(x => x.Date).ToArray();
            Listing_Transaction_Refund = Transaction.Enumeration.Where(x => x.Type == 2).OrderByDescending(x => x.Date).ToArray();
            Listing_Transaction_Sale_Refund = Refund.Enumeration.Where(x => x.Transaction_id.Id == 0).ToList();
            Customer_Searched = new Customer(0, null, new Address(0, "", new City(0, "", "")), null, new Person(0, "", "", "", ""));
            NewCustomer = new Customer(0, null, new Address(0, "", new City(0, "", "")), null, new Person(0, "", "", "", ""));
            Transaction_Sale_Selected = Transaction.ReferenceEntity;
            Transaction_Sale_Selected.Customer_id = Customer.ReferenceEntity;
            Transaction_Sale_Selected.Employee_id = Employee.ReferenceEntity;
            Transaction_Refund_Selected = Transaction.ReferenceEntity;
            Transaction_Refund_Selected.Customer_id = Customer.ReferenceEntity;
            Transaction_Refund_Selected.Employee_id = Employee.ReferenceEntity;
            Refund_Before_Selected = Sale.ReferenceEntity;
            AnonymousCustomer = false;
            Bucket_Ean = string.Empty;
            Bucket_Sum = string.Empty;
            Bucket_Money = string.Empty;
            Bucket_Promo = string.Empty;
            IsChecked_Bank = false;
            IsChecked_Money = false;
            Refund_Back = null;
            Refund_Total = null;
            Refund_Quantity = null;
            TabBucket_Selected = 0;

            CreateNewCustomer_Content = Error.Clear;
            CreateNewCustomer_Content_Bis = Error.Clear;
            CreateNewCustomer_Height = 30;
            ValidRefund_Content = Error.Clear;
            ValidRefund_Height = 30;
            UseLoyalty_IsEnabled = false;
            UseLoyalty_Opacity = 0.5;

            Error_Bucket_Ean = Error.Clear;
            Error_Customer_Searched = Error.Clear;
            Error_NewCustomer_Lastname = Error.Clear;
            Error_NewCustomer_Firstname = Error.Clear;
            Error_NewCustomer_Borndate = Error.Clear;
            Error_NewCustomer_Phone = Error.Clear;
            Error_NewCustomer_Mail = Error.Clear;
            Error_NewCustomer_Address = Error.Clear;
            Error_NewCustomer_City = Error.Clear;
            Error_NewCustomer_Postalcode = Error.Clear;
            Error_Bucket_Validate = Error.Clear;

            SearchSaleNumber = "Recherche par numéro";
            SearchSaleCustomer = "Recherche par client";
            SearchSaleDate = "Recherche par date";

            SearchRefundNumber = "Recherche par numéro";
            SearchRefundCustomer = "Recherche par client";
            SearchRefundDate = "Recherche par date";

            SearchCustomer = "Recherche par \"Nom, Prénom\"";
            SearchPhone = "Recherche par téléphone";
            SearchMail = "Recherche par email";

            SearchName = "Recherche par nom";
            SearchEAN = "Recherche par code EAN";
            SearchBrand = "Recherche par marque";
        }

        private void RefreshBucket(IList<Article> _lb)
        {
            var lb = _lb;
            Refresh();
            Listing_Bucket = lb;

            foreach (var i in Listing_Bucket)
            {
                if (Bucket_Sum == "")
                    Bucket_Sum += i.Price_info_id.Selling_price * i.Quantity;
                else
                    Bucket_Sum = (decimal.Parse(Bucket_Sum) + (decimal)(i.Price_info_id.Selling_price * i.Quantity)).ToString();
            }
            if (Listing_Bucket.Count != 0)
            {
                Bucket_Sum += " €";
                Bucket_Money = Bucket_Sum;
            }
        }

        private void ValidateEan()
        {
            if (Bucket_Ean.Length != 0)
            {
                Article a_load = Article.Enumeration.Where(x => x.Ean_code == Bucket_Ean)
                                                    .FirstOrDefault();
                if (a_load != null)
                {
                    if (Listing_Bucket.Contains(a_load))
                    {
                        var a_search = Listing_Bucket.Where(x => x.Ean_code == a_load.Ean_code).First();
                        a_search.Quantity += 1;
                        Listing_Bucket.Remove(a_load);
                        Listing_Bucket.Add(a_search);
                    }
                    else
                    {
                        var price = (double)a_load.Price_info_id.Selling_price;
                        var promo = 1 - a_load.Price_info_id.Promotion;
                        a_load.Price_info_id.Selling_price = (Decimal)Math.Round(price * (double)promo, 2);
                        a_load.Quantity = 1;
                        Listing_Bucket.Add(a_load);
                    }
                    var cs = Customer_Searched;
                    var nc = NewCustomer;
                    RefreshBucket(Listing_Bucket);
                    Customer_Searched = cs;
                    NewCustomer = nc;
                    if (Customer_Searched.Loyalty_points >= 50)
                    {
                        UseLoyalty_IsEnabled = true;
                        UseLoyalty_Opacity = 1;
                    }
                    Article_Selected = null;
                    Error_Bucket_Ean = string.Empty;
                }
                else
                    Error_Bucket_Ean = Error.EanInvalid;
            }
        }

        private void FilterName()
        {
            if (SearchName != "Recherche par nom")
            {
                SearchEAN = "Recherche par code EAN";
                SearchBrand = "Recherche par marque";

                IList<Article> la = Article.Enumeration.Where(x => x.Name.ToLower().Contains(SearchName.ToLower()))
                                                   .ToArray();
                if (la.Any())
                    Listing_Article = la;
                else
                    Listing_Article = null;
            }
            else
                Listing_Article = Article.Enumeration.ToArray();
        }

        private void FilterEAN()
        {
            if (SearchEAN != "Recherche par code EAN")
            {
                SearchName = "Recherche par nom";
                SearchBrand = "Recherche par marque";

                IList<Article> la = Article.Enumeration.Where(x => x.Ean_code.ToLower().Contains(SearchEAN.ToLower()))
                                                   .ToArray();
                if (la.Any())
                    Listing_Article = la;
                else
                    Listing_Article = null;
            }
            else
                Listing_Article = Article.Enumeration.ToArray();
        }

        private void FilterBrand()
        {
            if (SearchBrand != "Recherche par marque")
            {
                SearchName = "Recherche par nom";
                SearchEAN = "Recherche par code EAN";

                IList<Article> la = Article.Enumeration.Where(x => x.Brand_id.Name.ToLower().Contains(SearchBrand.ToLower()))
                                                   .ToArray();
                if (la.Any())
                    Listing_Article = la;
                else
                    Listing_Article = null;
            }
            else
                Listing_Article = Article.Enumeration.ToArray();
        }

        private void FilterCustomer()
        {
            if (SearchCustomer != "")
            {
                if (SearchCustomer != "Recherche par \"Nom, Prénom\"")
                {
                    SearchPhone = "Recherche par téléphone";
                    SearchMail = "Recherche par email";

                    if (SearchCustomer.Contains(','))
                    {
                        string[] fullname = SearchCustomer.Split(',');

                        Customer c = Customer.Enumeration.Where(x => x.Person_id.Lastname.ToLower().Contains(fullname[0].Trim().ToLower()))
                                                                 .Where(x => x.Person_id.Firstname.ToLower().Contains(fullname[1].Trim().ToLower()))
                                                                 .FirstOrDefault();
                        if (c != null)
                        {
                            Customer_Searched = c;
                            Error_Customer_Searched = Error.Clear;
                            SearchCustomer = "";
                            if (c.Loyalty_points >= 50)
                            {
                                UseLoyalty_IsEnabled = true;
                                UseLoyalty_Opacity = 1;
                            }
                            else
                            {
                                UseLoyalty_IsEnabled = false;
                                UseLoyalty_Opacity = 0.5;
                            }
                        }
                        else
                            Error_Customer_Searched = Error.CustomerNotFound;
                    }
                    else
                        Error_Customer_Searched = Error.CustomerNotValid;
                }
                else
                    Error_Customer_Searched = Error.CustomerNotFound;
            }
            else
                Error_Customer_Searched = Error.CustomerNotFound;
        }

        private void FilterPhone()
        {
            if (SearchPhone != "")
            {
                if (SearchPhone != "Recherche par téléphone")
                {
                    SearchCustomer = "Recherche par \"Nom, Prénom\"";
                    SearchMail = "Recherche par email";

                    Customer c = Customer.Enumeration.Where(x => x.Person_id.Phone.Replace("/", "")
                                                                                  .Replace(".", "")
                                                                                  .Replace(" ", "")
                                                                                  .Contains(SearchPhone.Trim().Replace("/", "")
                                                                                                              .Replace(".", "")
                                                                                                              .Replace(" ", "")))
                                                     .FirstOrDefault();
                    if (c != null)
                    {
                        Customer_Searched = c;
                        Error_Customer_Searched = Error.Clear;
                        SearchPhone = "";
                        if (c.Loyalty_points >= 50)
                        {
                            UseLoyalty_IsEnabled = true;
                            UseLoyalty_Opacity = 1;
                        }
                        else
                        {
                            UseLoyalty_IsEnabled = false;
                            UseLoyalty_Opacity = 0.5;
                        }
                    }
                    else
                        Error_Customer_Searched = Error.CustomerNotFound;
                }
                else
                    Error_Customer_Searched = Error.CustomerNotFound;
            }
            else
                Error_Customer_Searched = Error.CustomerNotFound;
        }

        private void FilterMail()
        {
            if (SearchMail != "")
            {
                if (SearchMail != "Recherche par email")
                {
                    SearchCustomer = "Recherche par \"Nom, Prénom\"";
                    SearchPhone = "Recherche par téléphone";

                    Customer c = Customer.Enumeration.Where(x => x.Person_id.Mail.ToLower().Contains(SearchMail.Trim().ToLower()))
                                                     .FirstOrDefault();
                    if (c != null)
                    {
                        Customer_Searched = c;
                        Error_Customer_Searched = Error.Clear;
                        SearchMail = "";
                        if (c.Loyalty_points >= 50)
                        {
                            UseLoyalty_IsEnabled = true;
                            UseLoyalty_Opacity = 1;
                        }
                        else
                        {
                            UseLoyalty_IsEnabled = false;
                            UseLoyalty_Opacity = 0.5;
                        }
                    }
                    else
                        Error_Customer_Searched = Error.CustomerNotFound;
                }
                else
                    Error_Customer_Searched = Error.CustomerNotFound;
            }
            else
                Error_Customer_Searched = Error.CustomerNotFound;
        }

        private void FilterSaleNumber()
        {
            if (SearchSaleNumber != "Recherche par numéro")
            {
                SearchSaleCustomer = "Recherche par client";
                SearchSaleDate = "Recherche par date";

                IList<Transaction> lts = Transaction.Enumeration.Where(x => x.Id.ToString().Contains(SearchSaleNumber))
                                                                .Where(x => x.Type == 1)
                                                                .OrderByDescending(x => x.Date)
                                                                .ToArray();
                if (lts.Any())
                    Listing_Transaction_Sale = lts;
                else
                    Listing_Transaction_Sale = null;
            }
            else
                Listing_Transaction_Sale = Transaction.Enumeration.Where(x => x.Type == 1).OrderByDescending(x => x.Date).ToArray();
        }

        private void FilterSaleCustomer()
        {
            if (SearchSaleCustomer != "Recherche par client")
            {
                SearchSaleNumber = "Recherche par numéro";
                SearchSaleDate = "Recherche par date";

                IList<Transaction> lts = null;

                if (!SearchSaleCustomer.Contains("@"))
                {
                    lts = Transaction.Enumeration.Where(x => x.Customer_id.Person_id.Lastname.ToLower().Contains(SearchSaleCustomer.Trim().ToLower())
                                                        || x.Customer_id.Person_id.Firstname.ToLower().Contains(SearchSaleCustomer.Trim().ToLower()))
                                                 .Where(x => x.Type == 1)
                                                 .OrderByDescending(x => x.Date)
                                                 .ToArray();
                }
                else
                {
                    lts = Transaction.Enumeration.Where(x => x.Customer_id.Person_id.Mail.ToLower().Contains(SearchSaleCustomer.Trim().ToLower()))
                                                 .Where(x => x.Type == 1)
                                                 .OrderByDescending(x => x.Date)
                                                 .ToArray();
                }


                if (lts.Any())
                    Listing_Transaction_Sale = lts;
                else
                    Listing_Transaction_Sale = null;
            }
            else
                Listing_Transaction_Sale = Transaction.Enumeration.Where(x => x.Type == 1).OrderByDescending(x => x.Date).ToArray();
        }

        private void FilterSaleDate()
        {
            if (SearchSaleNumber != "Recherche par date")
            {
                SearchSaleNumber = "Recherche par numéro";
                SearchSaleCustomer = "Recherche par client";

                IList<Transaction> lts = Transaction.Enumeration.Where(x => x.Date.ToShortDateString().Contains(SearchSaleDate.Trim().Replace(' ', '/')))
                                                                .Where(x => x.Type == 1)
                                                                .OrderByDescending(x => x.Date)
                                                                .ToArray();
                if (lts.Any())
                    Listing_Transaction_Sale = lts;
                else
                    Listing_Transaction_Sale = null;
            }
            else
                Listing_Transaction_Sale = Transaction.Enumeration.Where(x => x.Type == 1).OrderByDescending(x => x.Date).ToArray();
        }

        private void FilterRefundNumber()
        {
            if (SearchRefundNumber != "Recherche par numéro")
            {
                SearchRefundCustomer = "Recherche par client";
                SearchRefundDate = "Recherche par date";

                IList<Transaction> ltr = Transaction.Enumeration.Where(x => x.Id.ToString().Contains(SearchRefundNumber))
                                                                .Where(x => x.Type == 2)
                                                                .OrderByDescending(x => x.Date)
                                                                .ToArray();
                if (ltr.Any())
                    Listing_Transaction_Refund = ltr;
                else
                    Listing_Transaction_Refund = null;
            }
            else
                Listing_Transaction_Refund = Transaction.Enumeration.Where(x => x.Type == 2).OrderByDescending(x => x.Date).ToArray();
        }

        private void FilterRefundCustomer()
        {
            if (SearchRefundCustomer != "Recherche par client")
            {
                SearchRefundNumber = "Recherche par numéro";
                SearchRefundDate = "Recherche par date";

                IList<Transaction> ltr = null;

                if (!SearchRefundCustomer.Contains("@"))
                {
                    ltr = Transaction.Enumeration.Where(x => x.Customer_id.Person_id.Lastname.ToLower().Contains(SearchRefundCustomer.Trim().ToLower())
                                                        || x.Customer_id.Person_id.Firstname.ToLower().Contains(SearchRefundCustomer.Trim().ToLower()))
                                                 .Where(x => x.Type == 2)
                                                 .OrderByDescending(x => x.Date)
                                                 .ToArray();
                }
                else
                {
                    ltr = Transaction.Enumeration.Where(x => x.Customer_id.Person_id.Mail.ToLower().Contains(SearchRefundCustomer.Trim().ToLower()))
                                                 .Where(x => x.Type == 2)
                                                 .OrderByDescending(x => x.Date)
                                                 .ToArray();
                }

                if (ltr.Any())
                    Listing_Transaction_Refund = ltr;
                else
                    Listing_Transaction_Refund = null;
            }
            else
                Listing_Transaction_Refund = Transaction.Enumeration.Where(x => x.Type == 2).OrderByDescending(x => x.Date).ToArray();
        }

        private void FilterRefundDate()
        {
            if (SearchRefundDate != "Recherche par date")
            {
                SearchRefundNumber = "Recherche par numéro";
                SearchRefundCustomer = "Recherche par client";

                IList<Transaction> ltr = Transaction.Enumeration.Where(x => x.Date.ToShortDateString().Contains(SearchRefundDate.Trim().Replace(' ', '/')))
                                                                .Where(x => x.Type == 2)
                                                                .OrderByDescending(x => x.Date)
                                                                .ToArray();
                if (ltr.Any())
                    Listing_Transaction_Refund = ltr;
                else
                    Listing_Transaction_Refund = null;
            }
            else
                Listing_Transaction_Refund = Transaction.Enumeration.Where(x => x.Type == 2).OrderByDescending(x => x.Date).ToArray();
        }

        private void SelectArticle()
        {
            Article a_load = Article.Enumeration.Where(x => x.Ean_code == Article_Selected.Ean_code).First();
            if (a_load.Quantity == 0)
            {
                Article_Selected = null;
                Error_Bucket_Ean = Error.ArticleNotDisponible;
                return;
            }
            else if (Listing_Bucket.Contains(a_load))
            {
                var a_search = Listing_Bucket.Where(x => x.Ean_code == a_load.Ean_code).First();
                a_search.Quantity += 1;
                Listing_Bucket.Remove(a_load);
                Listing_Bucket.Add(a_search);
            }
            else
            {
                var price = (double)a_load.Price_info_id.Selling_price;
                var promo = 1 - a_load.Price_info_id.Promotion;
                a_load.Price_info_id.Selling_price = (Decimal)Math.Round(price * (double)promo, 2);
                a_load.Quantity = 1;
                Listing_Bucket.Add(a_load);
            }

            var cs = Customer_Searched;
            var nc = NewCustomer;
            RefreshBucket(Listing_Bucket);
            Customer_Searched = cs;
            NewCustomer = nc;

            if (Customer_Searched.Loyalty_points >= 50)
            {
                UseLoyalty_IsEnabled = true;
                UseLoyalty_Opacity = 1;
            }

            Article_Selected = null;
            Error_Bucket_Ean = string.Empty;

            if (App.Current.Properties["NewCustomerSaleId"] != null)
                if ((uint)App.Current.Properties["NewCustomerSaleId"] != 0)
                {
                    CreateNewCustomer_Content_Bis = Error.CustomerCreate;
                    CreateNewCustomer_Height = 0;
                }
        }

        private void AddQuantity()
        {
            Article a = Article.Load(Bucket_Item_Selected.Id);

            Bucket_Item_Selected.Quantity += 1;
            if (a.Quantity < Bucket_Item_Selected.Quantity)
            {
                Bucket_Item_Selected.Quantity -= 1;
                Article bis = Bucket_Item_Selected;
                var cs = Customer_Searched;
                var nc = NewCustomer;
                RefreshBucket(Listing_Bucket);
                Customer_Searched = cs;
                NewCustomer = nc;
                if (Customer_Searched.Loyalty_points >= 50)
                {
                    UseLoyalty_IsEnabled = true;
                    UseLoyalty_Opacity = 1;
                }
                Article_Selected = null;
                Error_Bucket_Ean = string.Empty;
                Bucket_Item_Selected = bis;
                Error_Bucket_Ean = Error.QuantityTooLow;
            }
            else
            {
                Article bis = Bucket_Item_Selected;
                var cs = Customer_Searched;
                var nc = NewCustomer;
                RefreshBucket(Listing_Bucket);
                Customer_Searched = cs;
                NewCustomer = nc;
                if (Customer_Searched.Loyalty_points >= 50)
                {
                    UseLoyalty_IsEnabled = true;
                    UseLoyalty_Opacity = 1;
                }
                Article_Selected = null;
                Error_Bucket_Ean = string.Empty;
                Bucket_Item_Selected = bis;
            }
        }

        private void RemoveQuantity()
        {
            if (Bucket_Item_Selected.Quantity > 1)
                Bucket_Item_Selected.Quantity -= 1;
            else
                Listing_Bucket.Remove(Bucket_Item_Selected);
            Error_Bucket_Ean = Error.Clear;
            Article bis = Bucket_Item_Selected;
            var cs = Customer_Searched;
            var nc = NewCustomer;
            RefreshBucket(Listing_Bucket);
            Customer_Searched = cs;
            NewCustomer = nc;
            if (Customer_Searched.Loyalty_points >= 50)
            {
                UseLoyalty_IsEnabled = true;
                UseLoyalty_Opacity = 1;
            }
            Article_Selected = null;
            Error_Bucket_Ean = string.Empty;
            Bucket_Item_Selected = bis;
        }

        private void CreateCustomer()
        {
            Error_NewCustomer_Lastname = Verify._SimpleText(NewCustomer.Person_id.Lastname, "Le nom", 0, 50, true, false);
            Error_NewCustomer_Firstname = Verify._SimpleText(NewCustomer.Person_id.Firstname, "Le prénom", 0, 50, true, false);
            Error_NewCustomer_Phone = Verify._SimpleText(NewCustomer.Person_id.Phone, "Le numéro", 0, 50, true, false);
            Error_NewCustomer_Mail = Verify._Mail(NewCustomer.Person_id.Mail, "L'email", 5, 50);
            Error_NewCustomer_Borndate = Verify._Borndate(NewCustomer.Borndate, "La date");

            if (Error_NewCustomer_Mail == Error.Clear)
            {
                var MailExist = Person.Enumeration.Where(x => x.Mail.ToLower() == NewCustomer.Person_id.Mail.ToLower())
                                                  .Where(x => x.Id != NewCustomer.Person_id.Id)
                                                  .Count();
                if (MailExist != 0)
                    Error_NewCustomer_Mail = Error.MailExist;
            }

            if (NewCustomer.Address_id.Name.Trim() != "" ||
                NewCustomer.Address_id.City_id.PostalCode.Trim() != "" ||
                NewCustomer.Address_id.City_id.Name.Trim() != "")
            {
                Error_NewCustomer_Address = Verify._SimpleText(NewCustomer.Address_id.Name, "L'adresse", 0, 100, true, true);
                if (NewCustomer.Address_id.Name.Trim() == "")
                    Error_NewCustomer_Address = Error.CompleteAddress;

                Error_NewCustomer_Postalcode = Verify._Regex(NewCustomer.Address_id.City_id.PostalCode, "Le code postal", 4, 5, "postalcode");
                if (NewCustomer.Address_id.City_id.PostalCode.Trim() == "")
                    Error_NewCustomer_Postalcode = Error.CompleteAddress;

                Error_NewCustomer_City = Verify._SimpleText(NewCustomer.Address_id.City_id.Name, "La ville", 3, 50, true, true);
                if (NewCustomer.Address_id.City_id.Name.Trim() == "")
                    Error_NewCustomer_City = Error.CompleteAddress;
            }
            else
            {
                Error_NewCustomer_Address = Error.Clear;
                Error_NewCustomer_Postalcode = Error.Clear;
                Error_NewCustomer_City = Error.Clear;
            }

            if (Error_NewCustomer_Lastname == Error.Clear && Error_NewCustomer_Firstname == Error.Clear &&
                Error_NewCustomer_Borndate == Error.Clear && Error_NewCustomer_Address == Error.Clear &&
                Error_NewCustomer_Postalcode == Error.Clear && Error_NewCustomer_City == Error.Clear &&
                Error_NewCustomer_Phone == Error.Clear && Error_NewCustomer_Mail == Error.Clear)
            {
                Address a_create = Address.ReferenceEntity;

                City city_exist = City.Enumeration.Where(x => x.Name.ToLower().Contains(NewCustomer.Address_id.City_id.Name.Trim().ToLower()))
                                                  .FirstOrDefault();
                if (city_exist != null)
                {
                    a_create = Address.Create(NewCustomer.Address_id.Name, city_exist);
                    a_create.Save();
                }
                else
                {
                    City city_created = City.Create(NewCustomer.Address_id.City_id.PostalCode, NewCustomer.Address_id.City_id.Name);
                    city_created.Save();

                    a_create = Address.Create(NewCustomer.Address_id.Name, city_created);
                    a_create.Save();
                }

                Person p_create = Person.CreateCustomer(NewCustomer.Person_id.Lastname, NewCustomer.Person_id.Firstname,
                                                        NewCustomer.Person_id.Phone, NewCustomer.Person_id.Mail);
                if (p_create.Save())
                {
                    if (NewCustomer.Borndate == DateTime.MinValue)
                        NewCustomer.Borndate = DateTime.Now;
                    Customer c_create = Customer.Create(NewCustomer.Borndate, a_create, 0, p_create);
                    if (c_create.Save())
                    {
                        App.Current.Properties["NewCustomerSaleId"] = c_create.Id;
                        CreateNewCustomer_Content_Bis = Error.CustomerCreate;
                        CreateNewCustomer_Height = 0;
                    }
                    else
                    {
                        p_create.Delete();
                        a_create.Delete();
                        CreateNewCustomer_Content = Error.CustomerExist;
                        CreateNewCustomer_Height = 0;
                    }
                }
                else
                {
                    a_create.Delete();
                    CreateNewCustomer_Content = Error.CustomerExist;
                    CreateNewCustomer_Height = 0;
                }
            }
        }

        private void ValidBucket()
        {
            if (Listing_Bucket.Count != 0)
            {
                if (IsChecked_Bank == true || IsChecked_Money == true)
                {
                    var bm = Bucket_Money.Replace('.', ',').Replace('€', ' ').Trim();
                    var bs = Bucket_Sum.Replace('.', ',').Replace('€', ' ').Trim();

                    if (decimal.Parse(bm) >= decimal.Parse(bs))
                    {
                        Customer c = null;
                        switch (TabBucket_Selected)
                        {
                            case 0:
                                if (Customer_Searched.Id != 0)
                                    c = Customer_Searched;
                                break;
                            case 1:
                                if (CreateNewCustomer_Content_Bis == Error.CustomerCreate)
                                {
                                    c = Customer.Load((uint)App.Current.Properties["NewCustomerSaleId"]);
                                    App.Current.Properties["NewCustomerSaleId"] = 0;
                                }
                                break;
                            case 2:
                                if (AnonymousCustomer == true)
                                    c = Customer.Load(1);
                                break;
                        }

                        if (c == null)
                            Error_Bucket_Validate = Error.CustomerEmptySale;
                        else
                        {
                            var tip = decimal.Parse(bm) - decimal.Parse(bs);
                            decimal reduction = 0;
                            if (Bucket_Promo.Length != 0)
                                decimal.TryParse(Bucket_Promo.Substring(0, Bucket_Promo.Length - 2), out reduction);

                            Transaction t_Create = Transaction.Create((Employee)App.Current.Properties["UserConnected"]
                                                                        , c, decimal.Parse(bs), reduction, decimal.Parse(bm), tip, 1);
                            if (t_Create.Save())
                            {
                                List<Article> la = new List<Article>();

                                foreach (var ib in Listing_Bucket)
                                {
                                    la.Add(ib);
                                    Article a_Load = Article.Load(ib.Id);
                                    Article.Update_Remove_Stock(a_Load, (int)ib.Quantity);

                                    Sale s_Create = Sale.Create(t_Create, ib, ib.Quantity, ib.Price_info_id.Selling_price * ib.Quantity);
                                    if (!s_Create.Save())
                                    {
                                        IList<Sale> ls = Sale.Enumeration.Where(x => x.Transaction_id == t_Create).ToArray();
                                        foreach (var i in ls)
                                            i.Delete();
                                        t_Create.Delete();
                                        Error_Bucket_Validate = Error.ValidError;

                                        foreach (var iib in la)
                                        {
                                            Article.Update_Add_Stock(a_Load, (int)iib.Quantity);
                                        }

                                        return;
                                    }
                                    else
                                        Refresh();
                                }

                                v_PDF_Bill Bill = new v_PDF_Bill
                                {
                                    DataContext = new vm_PDF_Bill(t_Create, false)
                                };
                                Bill.ShowDialog();
                            }
                        }
                    }
                    else
                        Error_Bucket_Validate = Error.PaymentToolow;
                }
                else
                    Error_Bucket_Validate = Error.PaymentSelect;
            }
            else
                Error_Bucket_Validate = Error.BucketEmpty;
        }

        private void ChangeTab()
        {
            Customer_Searched = new Customer(0, null, new Address(0, "", new City(0, "", "")), null, new Person(0, "", "", "", ""));
            NewCustomer = new Customer(0, null, new Address(0, "", new City(0, "", "")), null, new Person(0, "", "", "", ""));
            AnonymousCustomer = false;
        }

        private void UseLoyalty()
        {
            if (Customer_Searched != null)
            {
                Customer c_load = Customer.Load(Customer_Searched.Id);
                decimal lp = 10;
                for (int i = 0; i < c_load.Loyalty_points; i += 50)
                {
                    var t = decimal.Parse(Bucket_Sum.Substring(0, Bucket_Sum.Length - 2)) - lp;
                    if (t < 0)
                    {
                        break;
                    }
                    c_load.Loyalty_points -= 50;
                    lp += 10;
                }
                Bucket_Promo = $"{lp - 10},00 €";
                bool b_ischeck = IsChecked_Bank;
                bool m_ischeck = IsChecked_Money;
                var bp = Bucket_Promo;
                RefreshBucket(Listing_Bucket);
                Bucket_Promo = bp;
                var bs1 = decimal.Parse(Bucket_Sum.Substring(0, Bucket_Sum.Length - 2));
                var bs2 = decimal.Parse(Bucket_Promo.Substring(0, Bucket_Promo.Length - 2));
                Bucket_Sum = (bs1 - bs2).ToString();
                Bucket_Sum += " €";
                Bucket_Money = Bucket_Sum;
                Customer_Searched = c_load;
                IsChecked_Bank = b_ischeck;
                IsChecked_Money = m_ischeck;
                UseLoyalty_IsEnabled = false;
                UseLoyalty_Opacity = 0.5;
            }
        }

        private void SelectedTransactionSale()
        {
            if (Transaction_Sale_Selected != null)
            {
                if (Transaction_Sale_Selected != Transaction.ReferenceEntity)
                {
                    Transaction_Sale_Number = Transaction_Sale_Selected.Id.ToString();
                    Transaction_Sale_Employee = $"{Transaction_Sale_Selected.Employee_id.Person_id.Lastname.ToUpper()} {Transaction_Sale_Selected.Employee_id.Person_id.Firstname}";
                    if (Transaction_Sale_Selected.Customer_id.Person_id.Lastname != "" && Transaction_Sale_Selected.Customer_id.Person_id.Firstname != null)
                        Transaction_Sale_Customer = $"{Transaction_Sale_Selected.Customer_id.Person_id.Lastname.ToUpper()} {Transaction_Sale_Selected.Customer_id.Person_id.Firstname}";
                    else
                        Transaction_Sale_Customer = Transaction_Sale_Selected.Customer_id.Person_id.Mail;

                    Listing_Transaction_Sale_Details = Sale.Enumeration.Where(x => x.Transaction_id.Id == Transaction_Sale_Selected.Id).ToArray();
                }
            }
        }

        private void ExportTransactionSale()
        {
            v_PDF_Bill Bill = new v_PDF_Bill
            {
                DataContext = new vm_PDF_Bill(Transaction_Sale_Selected, true)
            };
            Bill.ShowDialog();
        }

        private void RefundQuantity()
        {
            if (Refund_Before_Selected != null)
            {
                if (Refund_Before_Selected.Quantity >= Refund_Quantity && Refund_Quantity > 0)
                {
                    decimal unitPrice = (decimal)Refund_Before_Selected.Total / ((decimal)Refund_Before_Selected.Quantity);

                    Refund_Before_Selected.Quantity -= Refund_Quantity;
                    Refund_Before_Selected.Total -= unitPrice * Refund_Quantity;

                    var refund = unitPrice * (decimal)Refund_Quantity;
                    if (Refund_Total == null)
                        Refund_Total = 0;
                    Refund_Total += refund;

                    if (Refund_Total - Transaction_Sale_Selected.Reduction > 0)
                    {
                        Refund_Back = Refund_Total - Transaction_Sale_Selected.Reduction;
                    }
                    else if (Refund_Total - Transaction_Sale_Selected.Reduction <= 0)
                    {
                        Refund_Back = 0;
                    }

                    Refund r = new Refund(0, Refund_Before_Selected.Transaction_id
                                           , Refund_Before_Selected.Article_id
                                           , (int)Refund_Quantity
                                           , unitPrice * (decimal)Refund_Quantity);

                    var r_search = Listing_Transaction_Sale_Refund.Where(x => x.Article_id == r.Article_id).FirstOrDefault();
                    if (r_search != null)
                    {
                        r_search.Quantity += (int)Refund_Quantity;
                        r_search.Total += unitPrice * (int)Refund_Quantity;
                    }
                    else
                    {
                        r.Save();
                        Listing_Transaction_Sale_Refund.Add(r);
                    }


                    var ts = Transaction_Sale_Selected;
                    var ls = Listing_Transaction_Sale_Details;
                    var lr = Listing_Transaction_Sale_Refund;
                    var rb = Refund_Back;
                    var rt = Refund_Total;

                    Refresh();

                    Transaction_Sale_Selected = ts;
                    Listing_Transaction_Sale_Details = ls;
                    Listing_Transaction_Sale_Refund = lr;
                    decimal sum = 0;
                    foreach (var i in Listing_Transaction_Sale_Details)
                    {
                        if (i.Quantity > 0)
                            sum += (decimal)i.Total / (decimal)i.Quantity;
                    }
                    Refund_Back = rb;
                    Refund_Total = rt;
                    Refund_Quantity = null;
                    Error_Refund = Error.Clear;
                }
                else
                {
                    Error_Refund = Error.RefundQuantityInvalid;
                    Refund_Quantity = null;
                }
            }
            else if (Refund_After_Selected != null)
            {
                if (Refund_After_Selected.Quantity >= Refund_Quantity && Refund_Quantity > 0)
                {
                    decimal unitPrice = (decimal)Refund_After_Selected.Total / ((decimal)Refund_After_Selected.Quantity);

                    Refund_After_Selected.Quantity -= Refund_Quantity;
                    Refund_After_Selected.Total -= unitPrice * Refund_Quantity;

                    var refund = unitPrice * (decimal)Refund_Quantity;

                    Refund_Back -= refund;
                    Refund_Total -= refund;
                    if (Refund_Back < 0)
                        Refund_Back = 0;

                    var s_search = Listing_Transaction_Sale_Details.Where(x => x.Article_id == Refund_After_Selected.Article_id).First();
                    if (s_search != null)
                    {
                        s_search.Quantity += (int)Refund_Quantity;
                        s_search.Total += unitPrice * (int)Refund_Quantity;
                    }

                    if (Refund_After_Selected.Quantity == 0)
                    {
                        var r_search = Listing_Transaction_Sale_Refund.Where(x => x.Article_id == Refund_After_Selected.Article_id).First();
                        Listing_Transaction_Sale_Refund.Remove(r_search);
                        r_search.Delete();
                    }

                    if (Listing_Transaction_Sale_Refund.Count == 0)
                        Refund_Back = null;

                    var ts = Transaction_Sale_Selected;
                    var ls = Listing_Transaction_Sale_Details;
                    var lr = Listing_Transaction_Sale_Refund;
                    var rb = Refund_Back;
                    var rt = Refund_Total;

                    Refresh();

                    Transaction_Sale_Selected = ts;
                    Listing_Transaction_Sale_Details = ls;
                    Listing_Transaction_Sale_Refund = lr;
                    Refund_Back = rb;
                    Refund_Total = rt;
                    Refund_Quantity = null;
                    Error_Refund = Error.Clear;
                }
                else
                {
                    Refund_Quantity = null;
                    Error_Refund = Error.RefundQuantityInvalid;
                }
            }
        }

        private void ValidRefund()
        {
            if (Refund_Back != null)
            {
                Transaction t_create = Transaction.Create((Employee)App.Current.Properties["UserConnected"]
                                                        , Transaction_Sale_Selected.Customer_id
                                                        , 0 - (decimal)Refund_Back, (decimal)Transaction_Sale_Selected.Reduction, 0, 0, 2);
                if (t_create.Save())
                {
                    foreach (var i in Listing_Transaction_Sale_Refund)
                    {
                        Refund s = new Refund(0, t_create, i.Article_id, i.Quantity, i.Total);
                        if (!s.Save())
                        {
                            foreach (var rd in Refund.Enumeration.Where(x => x.Transaction_id == t_create).ToArray())
                            {
                                rd.Delete();
                                t_create.Delete();
                                return;
                            }
                        }
                    }
                    v_PDF_Refund pdf = new v_PDF_Refund
                    {
                        DataContext = new vm_PDF_Refund(t_create, false)
                    };
                    pdf.ShowDialog();

                    ValidRefund_Content = Error.RefundValid;
                    ValidRefund_Height = 0;
                    Refund_Total = null;
                }
            }
        }

        private void ExportTransactionRefund()
        {
            v_PDF_Refund Refund = new v_PDF_Refund
            {
                DataContext = new vm_PDF_Refund(Transaction_Refund_Selected, true)
            };
            Refund.ShowDialog();
        }
        #endregion

        #region -- ACCESSEURS --
        public Article Bucket_Item_Selected
        {
            get { return bucket_Item_Selected; }
            set { bucket_Item_Selected = value; RaisePropertyChanged("Bucket_Item_Selected"); }
        }

        public Article Article_Selected
        {
            get { return article_Selected; }
            set { article_Selected = value; RaisePropertyChanged("Article_Selected"); }
        }

        public Transaction Transaction_Sale_Selected
        {
            get { return transaction_Sale_Selected; }
            set { transaction_Sale_Selected = value; RaisePropertyChanged("Transaction_Sale_Selected"); }
        }

        public Transaction Transaction_Refund_Selected
        {
            get { return transaction_Refund_Selected; }
            set { transaction_Refund_Selected = value; RaisePropertyChanged("Transaction_Refund_Selected"); }
        }

        public Sale Refund_Before_Selected
        {
            get { return refund_Before_Selected; }
            set { refund_Before_Selected = value; RaisePropertyChanged("Refund_Before_Selected"); }
        }

        public Refund Refund_After_Selected
        {
            get { return refund_After_Selected; }
            set { refund_After_Selected = value; RaisePropertyChanged("Refund_After_Selected"); }
        }

        public Customer Customer_Searched
        {
            get { return customer_Searched; }
            set { customer_Searched = value; RaisePropertyChanged("Customer_Searched"); }
        }

        public Customer NewCustomer
        {
            get { return newCustomer; }
            set { newCustomer = value; RaisePropertyChanged("NewCustomer"); }
        }

        public string Bucket_Ean
        {
            get { return bucket_Ean; }
            set { bucket_Ean = value; RaisePropertyChanged("Bucket_Ean"); }
        }

        public bool IsChecked_Bank
        {
            get { return isChecked_Bank; }
            set { isChecked_Bank = value; RaisePropertyChanged("IsChecked_Bank"); }
        }

        public bool IsChecked_Money
        {
            get { return isChecked_Money; }
            set { isChecked_Money = value; RaisePropertyChanged("IsChecked_Money"); }
        }

        public string Bucket_Money
        {
            get { return bucket_Money; }
            set { bucket_Money = value; RaisePropertyChanged("Bucket_Money"); }
        }

        public string Bucket_Promo
        {
            get { return bucket_Promo; }
            set { bucket_Promo = value; RaisePropertyChanged("Bucket_Promo"); }
        }

        public string Bucket_Sum
        {
            get { return bucket_Sum; }
            set { bucket_Sum = value; RaisePropertyChanged("Bucket_Sum"); }
        }

        public bool Change_Quantity
        {
            get { return change_Quantity; }
            set { change_Quantity = value; RaisePropertyChanged("Change_Quantity"); }
        }

        public string SearchName
        {
            get { return searchName; }
            set { searchName = value; RaisePropertyChanged("SearchName"); }
        }

        public string SearchEAN
        {
            get { return searchEAN; }
            set { searchEAN = value; RaisePropertyChanged("SearchEAN"); }
        }

        public string SearchBrand
        {
            get { return searchBrand; }
            set { searchBrand = value; RaisePropertyChanged("SearchBrand"); }
        }

        public string SearchCustomer
        {
            get { return searchCustomer; }
            set { searchCustomer = value; RaisePropertyChanged("SearchCustomer"); }
        }

        public string SearchPhone
        {
            get { return searchPhone; }
            set { searchPhone = value; RaisePropertyChanged("SearchPhone"); }
        }

        public string SearchMail
        {
            get { return searchMail; }
            set { searchMail = value; RaisePropertyChanged("SearchMail"); }
        }

        public string SearchSaleNumber
        {
            get { return searchSaleNumber; }
            set { searchSaleNumber = value; RaisePropertyChanged("SearchSaleNumber"); }
        }

        public string SearchSaleCustomer
        {
            get { return searchSaleCustomer; }
            set { searchSaleCustomer = value; RaisePropertyChanged("SearchSaleCustomer"); }
        }

        public string SearchSaleDate
        {
            get { return searchSaleDate; }
            set { searchSaleDate = value; RaisePropertyChanged("SearchSaleDate"); }
        }

        public string SearchRefundNumber
        {
            get { return searchRefundNumber; }
            set { searchRefundNumber = value; RaisePropertyChanged("SearchRefundNumber"); }
        }

        public string SearchRefundCustomer
        {
            get { return searchRefundCustomer; }
            set { searchRefundCustomer = value; RaisePropertyChanged("SearchRefundCustomer"); }
        }

        public string SearchRefundDate
        {
            get { return searchRefundDate; }
            set { searchRefundDate = value; RaisePropertyChanged("SearchRefundDate"); }
        }

        public string CreateNewCustomer_Content
        {
            get { return createNewCustomer_Content; }
            set { createNewCustomer_Content = value; RaisePropertyChanged("CreateNewCustomer_Content"); }
        }

        public string CreateNewCustomer_Content_Bis
        {
            get { return createNewCustomer_Content_Bis; }
            set { createNewCustomer_Content_Bis = value; RaisePropertyChanged("CreateNewCustomer_Content_Bis"); }
        }

        public int CreateNewCustomer_Height
        {
            get { return createNewCustomer_Height; }
            set { createNewCustomer_Height = value; RaisePropertyChanged("CreateNewCustomer_Height"); }
        }

        public string ValidRefund_Content
        {
            get { return validRefund_Content; }
            set { validRefund_Content = value; RaisePropertyChanged("ValidRefund_Content"); }
        }

        public int ValidRefund_Height
        {
            get { return validRefund_Height; }
            set { validRefund_Height = value; RaisePropertyChanged("ValidRefund_Height"); }
        }

        public bool AnonymousCustomer
        {
            get { return anonymousCustomer; }
            set { anonymousCustomer = value; RaisePropertyChanged("AnonymousCustomer"); }
        }

        public int TabBucket_Selected
        {
            get { return tabBucket_Selected; }
            set { tabBucket_Selected = value; RaisePropertyChanged("TabBucket_Selected"); }
        }

        public bool UseLoyalty_IsEnabled
        {
            get { return useLoyalty_IsEnabled; }
            set { useLoyalty_IsEnabled = value; RaisePropertyChanged("UseLoyalty_IsEnabled"); }
        }

        public double UseLoyalty_Opacity
        {
            get { return useLoyalty_Opacity; }
            set { useLoyalty_Opacity = value; RaisePropertyChanged("UseLoyalty_Opacity"); }
        }

        public int? Refund_Quantity
        {
            get { return refund_Quantity; }
            set { refund_Quantity = value; RaisePropertyChanged("Refund_Quantity"); }
        }

        public decimal? Refund_Back
        {
            get { return refund_Back; }
            set { refund_Back = value; RaisePropertyChanged("Refund_Back"); }
        }

        public decimal? Refund_Total
        {
            get { return refund_Total; }
            set { refund_Total = value; RaisePropertyChanged("Refund_Total"); }
        }

        public string Error_Bucket_Ean
        {
            get { return error_Bucket_Ean; }
            set { error_Bucket_Ean = value; RaisePropertyChanged("Error_Bucket_Ean"); }
        }

        public string Error_Bucket_Validate
        {
            get { return error_Bucket_Validate; }
            set { error_Bucket_Validate = value; RaisePropertyChanged("Error_Bucket_Validate"); }
        }

        public string Error_Customer_Searched
        {
            get { return error_Customer_Searched; }
            set { error_Customer_Searched = value; RaisePropertyChanged("Error_Customer_Searched"); }
        }

        public string Error_NewCustomer_Lastname
        {
            get { return error_NewCustomer_Lastname; }
            set { error_NewCustomer_Lastname = value; RaisePropertyChanged("Error_NewCustomer_Lastname"); }
        }

        public string Error_NewCustomer_Firstname
        {
            get { return error_NewCustomer_Firstname; }
            set { error_NewCustomer_Firstname = value; RaisePropertyChanged("Error_NewCustomer_Firstname"); }
        }

        public string Error_NewCustomer_Borndate
        {
            get { return error_NewCustomer_Borndate; }
            set { error_NewCustomer_Borndate = value; RaisePropertyChanged("Error_NewCustomer_Borndate"); }
        }

        public string Error_NewCustomer_Phone
        {
            get { return error_NewCustomer_Phone; }
            set { error_NewCustomer_Phone = value; RaisePropertyChanged("Error_NewCustomer_Phone"); }
        }

        public string Error_NewCustomer_Mail
        {
            get { return error_NewCustomer_Mail; }
            set { error_NewCustomer_Mail = value; RaisePropertyChanged("Error_NewCustomer_Mail"); }
        }

        public string Error_NewCustomer_Address
        {
            get { return error_NewCustomer_Address; }
            set { error_NewCustomer_Address = value; RaisePropertyChanged("Error_NewCustomer_Address"); }
        }

        public string Error_NewCustomer_City
        {
            get { return error_NewCustomer_City; }
            set { error_NewCustomer_City = value; RaisePropertyChanged("Error_NewCustomer_City"); }
        }

        public string Error_NewCustomer_Postalcode
        {
            get { return error_NewCustomer_Postalcode; }
            set { error_NewCustomer_Postalcode = value; RaisePropertyChanged("Error_NewCustomer_Postalcode"); }
        }

        public string Error_Refund
        {
            get { return error_Refund; }
            set { error_Refund = value; RaisePropertyChanged("Error_Refund"); }
        }

        public string Transaction_Sale_Number
        {
            get { return transaction_Sale_Number; }
            set { transaction_Sale_Number = value; RaisePropertyChanged("Transaction_Sale_Number"); }
        }

        public string Transaction_Sale_Employee
        {
            get { return transaction_Sale_Employee; }
            set { transaction_Sale_Employee = value; RaisePropertyChanged("Transaction_Sale_Employee"); }
        }

        public string Transaction_Sale_Customer
        {
            get { return transaction_Sale_Customer; }
            set { transaction_Sale_Customer = value; RaisePropertyChanged("Transaction_Sale_Customer"); }
        }

        public IList<Article> Listing_Bucket
        {
            get { return listing_Bucket; }
            set { listing_Bucket = value; RaisePropertyChanged("Listing_Bucket"); }
        }

        public IList<Article> Listing_Article
        {
            get { return listing_Article; }
            set { listing_Article = value; RaisePropertyChanged("Listing_Article"); }
        }

        public IList<Transaction> Listing_Transaction_Sale
        {
            get { return listing_Transaction_Sale; }
            set { listing_Transaction_Sale = value; RaisePropertyChanged("Listing_Transaction_Sale"); }
        }

        public IList<Sale> Listing_Transaction_Sale_Details
        {
            get { return listing_Transaction_Sale_Details; }
            set { listing_Transaction_Sale_Details = value; RaisePropertyChanged("Listing_Transaction_Sale_Details"); }
        }

        public IList<Refund> Listing_Transaction_Sale_Refund
        {
            get { return listing_Transaction_Sale_Refund; }
            set { listing_Transaction_Sale_Refund = value; RaisePropertyChanged("Listing_Transaction_Sale_Refund"); }
        }

        public IList<Transaction> Listing_Transaction_Refund
        {
            get { return listing_Transaction_Refund; }
            set { listing_Transaction_Refund = value; RaisePropertyChanged("Listing_Transaction_Refund"); }
        }
        #endregion
    }
}