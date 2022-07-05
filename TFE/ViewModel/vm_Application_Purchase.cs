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
    //! Cette classe contient les propriétés auxquelles la vue v_Application_Purchase peut se lier avec le Binding.
    //x </summary>
    public class vm_Application_Purchase : ViewModelBase
    {
        #region -- PROPERTIES --
        private Article bucket_Item_Selected;
        private Transaction transaction_Purchase_Selected;
        private Article article_ToFinalize_Selected;
        private Article newDeposit;
        private Brand newBrand;
        private Category newCategory;
        private Sub_Category newSubCategory;
        private Customer customer_Searched;
        private Customer newCustomer;
        private string searchPurchaseNumber;
        private string searchPurchaseCustomer;
        private string searchPurchaseDate;
        private int tabBucket_Selected;
        private string updateArticle_Content;
        private int updateArticle_Height;
        private string bucket_Sum;
        private string searchCustomer;
        private string searchPhone;
        private string searchMail;
        private string createNewCustomer_Content;
        private string createNewCustomer_Content_Bis;
        private int createNewCustomer_Height;
        private string add_PurchasePrice;
        private string add_SellingPrice;

        private string error_Deposit_Name;
        private string error_Deposit_Quantity;
        private string error_Deposit_PurchasePrice;
        private string error_Deposit_Brand;
        private string error_Deposit_Category;
        private string error_Deposit_SubCategory;
        private string error_Bucket_Validate;
        private string error_ToFinalize_Name;
        private string error_ToFinalize_EAN;
        private string error_ToFinalize_Quantity;
        private string error_ToFinalize_PurchasePrice;
        private string error_ToFinalize_TVA;
        private string error_ToFinalize_Brand;
        private string error_ToFinalize_Category;
        private string error_ToFinalize_SubCategory;
        private string error_ToFinalize_SellingPrice;
        private string error_ToFinalize_Promo;
        private string error_Customer_Searched;
        private string error_NewCustomer_Lastname;
        private string error_NewCustomer_Firstname;
        private string error_NewCustomer_Borndate;
        private string error_NewCustomer_Phone;
        private string error_NewCustomer_Mail;
        private string error_NewCustomer_Address;
        private string error_NewCustomer_City;
        private string error_NewCustomer_Postalcode;

        private string transaction_Purchase_Number;
        private string transaction_Purchase_Employee;
        private string transaction_Purchase_Customer;

        private IList<Article> listing_Bucket;
        private IList<Transaction> listing_Transaction_Purchase;
        private IList<Article> listing_Article_ToFinalize;
        private IList<Purchase> listing_Transaction_Purchase_Details;
        private IList<Brand> cb_Brand;
        private IList<Category> cb_Category;
        private IList<Sub_Category> cb_SubCategory;
        private IList<Tva> cb_TVA;
        #endregion

        #region -- RELAY COMMAND DECLARATION --
        public RelayCommand RefreshBtn { get; private set; }
        public RelayCommand FilterPurchaseNumberBtn { get; private set; }
        public RelayCommand FilterPurchaseCustomerBtn { get; private set; }
        public RelayCommand FilterPurchaseDateBtn { get; private set; }
        public RelayCommand FilterCustomerBtn { get; private set; }
        public RelayCommand FilterPhoneBtn { get; private set; }
        public RelayCommand FilterMailBtn { get; private set; }
        public RelayCommand ValidBucketBtn { get; private set; }
        public RelayCommand SelectedTransactionPurchaseAction { get; private set; }
        public RelayCommand SelectedArticleToFinalizeAction { get; private set; }
        public RelayCommand ExportTransactionPurchaseBtn { get; private set; }
        public RelayCommand Cb_Category_Changed_Action { get; private set; }
        public RelayCommand FinalizeArticleBtn { get; private set; }
        public RelayCommand AddToBucketBtn { get; private set; }
        public RelayCommand RemoveFromBucketBtn { get; private set; }
        public RelayCommand SwitchBrandBtn { get; private set; }
        public RelayCommand SwitchCategoryBtn { get; private set; }
        public RelayCommand CreateCustomerBtn { get; private set; }
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_Application_Purchase
        //x </summary>
        public vm_Application_Purchase()
        {
            FilterPurchaseNumberBtn = new RelayCommand(FilterPurchaseNumber);
            FilterPurchaseCustomerBtn = new RelayCommand(FilterPurchaseCustomer);
            FilterPurchaseDateBtn = new RelayCommand(FilterPurchaseDate);
            FilterCustomerBtn = new RelayCommand(FilterCustomer);
            FilterPhoneBtn = new RelayCommand(FilterPhone);
            FilterMailBtn = new RelayCommand(FilterMail);
            ValidBucketBtn = new RelayCommand(ValidBucket);
            SelectedTransactionPurchaseAction = new RelayCommand(SelectedTransactionPurchase);
            SelectedArticleToFinalizeAction = new RelayCommand(SelectedArticleToFinalize);
            ExportTransactionPurchaseBtn = new RelayCommand(ExportTransactionPurchase);
            Cb_Category_Changed_Action = new RelayCommand(Cb_Category_Changed);
            FinalizeArticleBtn = new RelayCommand(FinalizeArticle);
            AddToBucketBtn = new RelayCommand(AddToBucket);
            RemoveFromBucketBtn = new RelayCommand(RemoveFromBucket);
            SwitchBrandBtn = new RelayCommand(SwitchBrand);
            SwitchCategoryBtn = new RelayCommand(SwitchCategory);
            CreateCustomerBtn = new RelayCommand(CreateCustomer);

            RefreshBtn = new RelayCommand(Refresh);
            Refresh();
        }

        #region -- RELAY COMMAND METHODS --
        private void Refresh()
        {
            Listing_Bucket = Article.Enumeration.Where(x => x.Date > DateTime.Now).ToList();
            Listing_Transaction_Purchase = Transaction.Enumeration.Where(x => x.Type == 3).OrderByDescending(x => x.Date).ToArray();
            Listing_Article_ToFinalize = Article.Enumeration.Where(x => x.Deposit == true)
                                                            .Where(x => x.Price_info_id.Selling_price == 0)
                                                            .OrderByDescending(x => x.Ean_code)
                                                            .ToArray();
            Transaction_Purchase_Selected = Transaction.ReferenceEntity;
            Transaction_Purchase_Selected.Customer_id = Customer.ReferenceEntity;
            Transaction_Purchase_Selected.Employee_id = Employee.ReferenceEntity;
            Customer_Searched = new Customer(0, null, new Address(0, "", new City(0, "", "")), null, new Person(0, "", "", "", ""));
            NewCustomer = new Customer(0, null, new Address(0, "", new City(0, "", "")), null, new Person(0, "", "", "", ""));
            NewDeposit = new Article(0, "", "", new Brand(0, ""), new Category(0, ""), new Sub_Category(0, new Category(0, ""), ""), new Price_Info(0, null, null, Tva.ReferenceEntity, null), null, true, DateTime.Now);
            NewBrand = new Brand(0, "");
            NewCategory = new Category(0, "");
            NewSubCategory = new Sub_Category(0, new Category(0, ""), "");
            Cb_SubCategory = Sub_Category.Enumeration.Where(x => x.Id == 0).ToArray();

            TabBucket_Selected = 0;
            CreateNewCustomer_Content = Error.Clear;
            CreateNewCustomer_Content_Bis = Error.Clear;
            CreateNewCustomer_Height = 30;
            FinalizeArticle_Content = Error.Clear;
            FinalizeArticle_Height = 30;
            Bucket_Sum = string.Empty;
            Add_PurchasePrice = string.Empty;
            Add_SellingPrice = string.Empty;

            Error_Deposit_Name = Error.Clear;
            Error_Deposit_Quantity = Error.Clear;
            Error_Deposit_PurchasePrice = Error.Clear;
            Error_Deposit_Brand = Error.Clear;
            Error_Deposit_Category = Error.Clear;
            Error_Deposit_SubCategory = Error.Clear;
            Error_Bucket_Validate = Error.Clear;
            Error_ToFinalize_Name = Error.Clear;
            Error_ToFinalize_EAN = Error.Clear;
            Error_ToFinalize_Quantity = Error.Clear;
            Error_ToFinalize_PurchasePrice = Error.Clear;
            Error_ToFinalize_TVA = Error.Clear;
            Error_ToFinalize_Brand = Error.Clear;
            Error_ToFinalize_Category = Error.Clear;
            Error_ToFinalize_SubCategory = Error.Clear;
            Error_ToFinalize_SellingPrice = Error.Clear;
            Error_ToFinalize_Promo = Error.Clear;
            Error_Customer_Searched = Error.Clear;

            SearchCustomer = "Recherche par \"Nom, Prénom\"";
            SearchPhone = "Recherche par téléphone";
            SearchMail = "Recherche par email";

            SearchPurchaseNumber = "Recherche par numéro";
            SearchPurchaseCustomer = "Recherche par client";
            SearchPurchaseDate = "Recherche par date";

        }

        private void RefreshBucket(IList<Article> _lb)
        {
            var lb = _lb;
            Refresh();
            Listing_Bucket = lb;

            foreach (var i in Listing_Bucket)
            {
                if (Bucket_Sum == "")
                    Bucket_Sum += i.Price_info_id.Buying_price * i.Quantity;
                else
                    Bucket_Sum = (decimal.Parse(Bucket_Sum) + (decimal)(i.Price_info_id.Buying_price * i.Quantity)).ToString();
            }
            if (Listing_Bucket.Count != 0)
            {
                Bucket_Sum += " €";
            }
        }

        private void FilterPurchaseNumber()
        {
            if (SearchPurchaseNumber != "Recherche par numéro")
            {
                SearchPurchaseCustomer = "Recherche par client";
                SearchPurchaseDate = "Recherche par date";

                IList<Transaction> ltp = Transaction.Enumeration.Where(x => x.Id.ToString().Contains(SearchPurchaseNumber))
                                                                .Where(x => x.Type == 3)
                                                                .OrderByDescending(x => x.Date)
                                                                .ToArray();
                if (ltp.Any())
                    Listing_Transaction_Purchase = ltp;
                else
                    Listing_Transaction_Purchase = null;
            }
            else
                Listing_Transaction_Purchase = Transaction.Enumeration.Where(x => x.Type == 3).OrderByDescending(x => x.Date).ToArray();
        }

        private void FilterPurchaseCustomer()
        {
            if (SearchPurchaseCustomer != "Recherche par client")
            {
                SearchPurchaseNumber = "Recherche par numéro";
                SearchPurchaseDate = "Recherche par date";

                IList<Transaction> ltp = null;

                if (!SearchPurchaseCustomer.Contains("@"))
                {
                    ltp = Transaction.Enumeration.Where(x => x.Customer_id.Person_id.Lastname.ToLower().Contains(SearchPurchaseCustomer.Trim().ToLower())
                                                        || x.Customer_id.Person_id.Firstname.ToLower().Contains(SearchPurchaseCustomer.Trim().ToLower()))
                                                 .Where(x => x.Type == 3)
                                                 .OrderByDescending(x => x.Date)
                                                 .ToArray();
                }
                else
                {
                    ltp = Transaction.Enumeration.Where(x => x.Customer_id.Person_id.Mail.ToLower().Contains(SearchPurchaseCustomer.Trim().ToLower()))
                                                 .Where(x => x.Type == 3)
                                                 .OrderByDescending(x => x.Date)
                                                 .ToArray();
                }

                if (ltp.Any())
                    Listing_Transaction_Purchase = ltp;
                else
                    Listing_Transaction_Purchase = null;
            }
            else
                Listing_Transaction_Purchase = Transaction.Enumeration.Where(x => x.Type == 3).OrderByDescending(x => x.Date).ToArray();
        }

        private void FilterPurchaseDate()
        {
            if (SearchPurchaseDate != "Recherche par date")
            {
                SearchPurchaseNumber = "Recherche par numéro";
                SearchPurchaseCustomer = "Recherche par client";

                IList<Transaction> ltp = Transaction.Enumeration.Where(x => x.Date.ToShortDateString().Contains(SearchPurchaseDate.Trim().Replace(' ', '/')))
                                                                .Where(x => x.Type == 3)
                                                                .OrderByDescending(x => x.Date)
                                                                .ToArray();
                if (ltp.Any())
                    Listing_Transaction_Purchase = ltp;
                else
                    Listing_Transaction_Purchase = null;
            }
            else
                Listing_Transaction_Purchase = Transaction.Enumeration.Where(x => x.Type == 3).OrderByDescending(x => x.Date).ToArray();
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
                                                                                  .Replace(",", "")
                                                                                  .Replace(" ", "")
                                                                                  .Contains(SearchPhone.Trim().Replace("/", "")
                                                                                                              .Replace(".", "")
                                                                                                              .Replace(",", "")
                                                                                                              .Replace(" ", "")))
                                                     .FirstOrDefault();
                    if (c != null)
                    {
                        Customer_Searched = c;
                        Error_Customer_Searched = Error.Clear;
                        SearchPhone = "";
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

        private void ValidBucket()
        {
            if (Listing_Bucket.Count != 0)
            {
                var bs = Bucket_Sum.Replace('.', ',').Replace('€', ' ').Trim();

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
                            c = Customer.Load((uint)App.Current.Properties["NewCustomerRefundId"]);
                            App.Current.Properties["NewCustomerRefundId"] = 0;
                        }
                        break;
                }

                if (c == null)
                    Error_Bucket_Validate = Error.CustomerEmptyDeposit;
                else
                {
                    Error_Bucket_Validate = Error.Clear;

                    Transaction t_Create = Transaction.Create((Employee)App.Current.Properties["UserConnected"]
                                                                , c, 0 - decimal.Parse(bs), 0, 0, 0, 3);
                    if (t_Create.Save())
                    {
                        List<Article> la = new List<Article>();

                        foreach (var ib in Listing_Bucket)
                        {
                            Article a_search_last_ean = null;
                            try
                            {
                                a_search_last_ean = Article.Enumeration.Where(x => x.Ean_code.Substring(0, 3) == "DEP").Last();
                            }
                            catch (Exception) { }

                            ib.Ean_code = "DEP_";
                            if (a_search_last_ean != null)
                            {
                                string last_ean = a_search_last_ean.Ean_code;
                                int next_ean = int.Parse(last_ean.Substring(4)) + 1;

                                switch (next_ean.ToString().Length)
                                {
                                    case 1:
                                        ib.Ean_code += $"0000000{next_ean}";
                                        break;
                                    case 2:
                                        ib.Ean_code += $"000000{next_ean}";
                                        break;
                                    case 3:
                                        ib.Ean_code += $"00000{next_ean}";
                                        break;
                                    case 4:
                                        ib.Ean_code += $"0000{next_ean}";
                                        break;
                                    case 5:
                                        ib.Ean_code += $"000{next_ean}";
                                        break;
                                    case 6:
                                        ib.Ean_code += $"00{next_ean}";
                                        break;
                                    case 7:
                                        ib.Ean_code += $"0{next_ean}";
                                        break;
                                    case 8:
                                        ib.Ean_code += $"{next_ean}";
                                        break;
                                }
                            }
                            else
                                ib.Ean_code += "00000001";


                            ib.Brand_id.Save();
                            ib.Category_id.Save();
                            if (ib.Sub_category_id != null)
                            {
                                ib.Sub_category_id.Category_id = ib.Category_id;
                                ib.Sub_category_id.Save();
                            }
                            ib.Price_info_id.Save();
                            ib.Date = DateTime.Now;
                            ib.Save();

                            la.Add(ib);

                            Purchase p_Create = Purchase.Create(t_Create, ib, (int)ib.Quantity, (decimal)(ib.Price_info_id.Buying_price * ib.Quantity));
                            if (!p_Create.Save())
                            {
                                IList<Purchase> lp = Purchase.Enumeration.Where(x => x.Transaction_id == t_Create).ToArray();
                                foreach (var i in lp)
                                    i.Delete();
                                t_Create.Delete();
                                Error_Bucket_Validate = Error.ValidError;

                                foreach (var iib in la)
                                {
                                    iib.Price_info_id.Delete();
                                    iib.Delete();
                                }
                                return;
                            }
                        }
                        v_PDF_Deposit Deposit = new v_PDF_Deposit
                        {
                            DataContext = new vm_PDF_Deposit(t_Create)
                        };
                        Deposit.ShowDialog();
                        Refresh();
                    }
                }
            }
            else
                Error_Bucket_Validate = Error.BucketEmpty;
        }

        private void SelectedTransactionPurchase()
        {
            if (Transaction_Purchase_Selected != Transaction.ReferenceEntity)
            {
                Transaction_Purchase_Number = Transaction_Purchase_Selected.Id.ToString();
                Transaction_Purchase_Employee = $"{Transaction_Purchase_Selected.Employee_id.Person_id.Lastname.ToUpper()} {Transaction_Purchase_Selected.Employee_id.Person_id.Firstname}";
                if (Transaction_Purchase_Selected.Customer_id.Person_id.Lastname != "" && Transaction_Purchase_Selected.Customer_id.Person_id.Firstname != null)
                    Transaction_Purchase_Customer = $"{Transaction_Purchase_Selected.Customer_id.Person_id.Lastname.ToUpper()} {Transaction_Purchase_Selected.Customer_id.Person_id.Firstname}";
                else
                    Transaction_Purchase_Customer = Transaction_Purchase_Selected.Customer_id.Person_id.Mail;

                Listing_Transaction_Purchase_Details = Purchase.Enumeration.Where(x => x.Transaction_id.Id == Transaction_Purchase_Selected.Id).ToArray();
            }
        }

        private void SelectedArticleToFinalize()
        {
            Cb_Category_Changed();
        }

        private void ExportTransactionPurchase()
        {
            v_PDF_Deposit Deposit = new v_PDF_Deposit
            {
                DataContext = new vm_PDF_Deposit(transaction_Purchase_Selected)
            };
            Deposit.ShowDialog();
        }

        private void Cb_Category_Changed()
        {
            if (Article_ToFinalize_Selected != null)
            {
                Cb_SubCategory = Sub_Category.Enumeration.Where(x => x.Category_id.Id == Article_ToFinalize_Selected.Category_id.Id)
                                                         .Cast<Sub_Category>().ToList();
            }
            else if (NewDeposit != null && Cb_SubCategory.Count == 0)
            {
                if (NewCategory != null)
                    Cb_SubCategory = Sub_Category.Enumeration.Where(x => x.Category_id.Id == NewCategory.Id)
                                                             .Cast<Sub_Category>().ToList();
            }
        }

        private void FinalizeArticle()
        {
            Error_ToFinalize_Name = Verify._SimpleText(Article_ToFinalize_Selected.Name, "Le nom", 0, 50, false, true);
            Error_ToFinalize_Brand = Verify._Brand(Article_ToFinalize_Selected.Brand_id);
            Error_ToFinalize_Category = Verify._Category(Article_ToFinalize_Selected.Category_id);
            Error_ToFinalize_TVA = Verify._Tva(Article_ToFinalize_Selected.Price_info_id.Tva_id);
            Error_ToFinalize_SellingPrice = Verify._Decimal(Add_SellingPrice, out decimal sp);
            Error_ToFinalize_Promo = Verify._Double(Article_ToFinalize_Selected.Price_info_id.Promotion.ToString(), out double p);
            if (Error_ToFinalize_SellingPrice == Error.Clear && sp <= Article_ToFinalize_Selected.Price_info_id.Buying_price)
                Error_ToFinalize_PurchasePrice = Error.PriceTooBig;
            else
                Error_ToFinalize_PurchasePrice = Error.Clear;
            if (Error_ToFinalize_Promo == Error.Clear && (p < 0 || p > 99))
                Error_ToFinalize_Promo = Error.PromoValid;

            if (Error_ToFinalize_Name == Error.Clear && Error_ToFinalize_Brand == Error.Clear && Error_ToFinalize_Category == Error.Clear && Error_ToFinalize_PurchasePrice == Error.Clear &&
                Error_ToFinalize_TVA == Error.Clear && Error_ToFinalize_SellingPrice == Error.Clear && Error_ToFinalize_Promo == Error.Clear)
            {
                var a = Article_ToFinalize_Selected;
                Price_Info pi = Price_Info.Load(a.Price_info_id.Id);
                pi.Selling_price = sp;
                pi.Promotion = p / 100;
                pi.Tva_id = Article_ToFinalize_Selected.Price_info_id.Tva_id;
                pi.Save();

                Article.Update(ref a, a.Name, a.Ean_code, a.Brand_id, a.Category_id, a.Sub_category_id, pi, (int)a.Quantity, a.Deposit, a.Date);

                FinalizeArticle_Content = Error.Update;
                FinalizeArticle_Height = 0;
            }
        }

        private void AddToBucket()
        {
            Error_Deposit_Name = Verify._SimpleText(NewDeposit.Name, "Le nom", 0, 100, false, true);
            Error_Deposit_Quantity = Verify._Int(NewDeposit.Quantity.ToString(), out int quantity_to_add);
            if (Error_Deposit_Quantity == "" && quantity_to_add <= 0)
                Error_Deposit_Quantity = Error.FieldNumberTooShort("La quantitée", "e");
            Error_Deposit_PurchasePrice = Verify._Decimal(Add_PurchasePrice, out decimal pp);
            if (Error_Deposit_PurchasePrice == "" && pp <= 0)
                Error_Deposit_PurchasePrice = Error.PriceTooLow;
            if (NewBrand == null)
                Error_Deposit_Brand = Verify._SimpleText(NewDeposit.Brand_id.Name, "La marque", 0, 50, false, true);
            else
                Error_Deposit_Brand = Verify._Brand(NewBrand);
            if (NewCategory == null)
                Error_Deposit_Category = Verify._SimpleText(NewDeposit.Category_id.Name, "La catégorie", 0, 50, false, true);
            else
                Error_Deposit_Category = Verify._Category(NewCategory);

            if (Error_Deposit_Name == Error.Clear && Error_Deposit_Quantity == Error.Clear && Error_Deposit_PurchasePrice == Error.Clear
                && Error_Deposit_Brand == Error.Clear && Error_Deposit_Category == Error.Clear)
            {
                #region -- BRAND --
                Brand b_deposit = Brand.ReferenceEntity;
                if (NewBrand == null)
                    b_deposit = Brand.Create(NewDeposit.Brand_id.Name);
                else
                    b_deposit = Brand.Load(NewBrand.Id);
                #endregion

                #region -- CATEGORY --
                Category c_deposit = Category.ReferenceEntity;
                if (NewCategory == null)
                    c_deposit = Category.Create(NewDeposit.Category_id.Name);
                else
                    c_deposit = Category.Load(NewCategory.Id);
                #endregion

                #region -- SUB_CATEGORY --
                Sub_Category sc_deposit = Sub_Category.ReferenceEntity;
                if (NewCategory == null)
                    sc_deposit = Sub_Category.Create(NewDeposit.Category_id, NewDeposit.Sub_category_id.Name);
                else
                {
                    if (NewSubCategory != null)
                        sc_deposit = Sub_Category.Load(NewSubCategory.Id);
                    else
                        sc_deposit = null;
                }
                #endregion

                Price_Info pi_deposit = Price_Info.Create(pp, 0, Tva.Load(4), 0);

                Article a_create = Article.Create(NewDeposit.Name, "000", b_deposit, c_deposit, sc_deposit
                                                , pi_deposit, quantity_to_add, true, default);

                Listing_Bucket.Add(a_create);

                var cs = Customer_Searched;
                var nc = NewCustomer;
                RefreshBucket(Listing_Bucket);
                Customer_Searched = cs;
                NewCustomer = nc;
                Bucket_Item_Selected = null;
                SwitchBrand();
                SwitchCategory();

                if (App.Current.Properties["NewCustomerRefundId"] != null)
                    if ((uint)App.Current.Properties["NewCustomerRefundId"] != 0)
                    {
                        CreateNewCustomer_Content_Bis = Error.CustomerCreate;
                        CreateNewCustomer_Height = 0;
                    }

            }
        }

        private void RemoveFromBucket()
        {
            Bucket_Item_Selected.Delete();
            Listing_Bucket.Remove(Bucket_Item_Selected);
            var cs = Customer_Searched;
            var nc = NewCustomer;
            RefreshBucket(Listing_Bucket);
            Customer_Searched = cs;
            NewCustomer = nc;
            Bucket_Item_Selected = null;
        }

        private void SwitchBrand()
        {
            NewBrand = null;
            NewDeposit.Brand_id.Name = "";
        }

        private void SwitchCategory()
        {
            NewCategory = null;
            NewSubCategory = null;
            NewDeposit.Category_id.Name = "";
            NewDeposit.Sub_category_id.Name = "";
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
                        App.Current.Properties["NewCustomerRefundId"] = c_create.Id;
                        CreateNewCustomer_Content_Bis = Error.CustomerCreate;
                        Error_Bucket_Validate = Error.Clear;
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
        #endregion

        #region -- ACCESSORS --
        public Article Bucket_Item_Selected
        {
            get { return bucket_Item_Selected; }
            set { bucket_Item_Selected = value; RaisePropertyChanged("Bucket_Item_Selected"); }
        }

        public Transaction Transaction_Purchase_Selected
        {
            get { return transaction_Purchase_Selected; }
            set { transaction_Purchase_Selected = value; RaisePropertyChanged("Transaction_Purchase_Selected"); }
        }

        public Article NewDeposit
        {
            get { return newDeposit; }
            set { newDeposit = value; RaisePropertyChanged("NewDeposit"); }
        }

        public Brand NewBrand
        {
            get { return newBrand; }
            set { newBrand = value; RaisePropertyChanged("NewBrand"); }
        }

        public Category NewCategory
        {
            get { return newCategory; }
            set { newCategory = value; RaisePropertyChanged("NewCategory"); }
        }

        public Sub_Category NewSubCategory
        {
            get { return newSubCategory; }
            set { newSubCategory = value; RaisePropertyChanged("NewSubCategory"); }
        }

        public Article Article_ToFinalize_Selected
        {
            get { return article_ToFinalize_Selected; }
            set { article_ToFinalize_Selected = value; RaisePropertyChanged("Article_ToFinalize_Selected"); }
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

        public string SearchPurchaseNumber
        {
            get { return searchPurchaseNumber; }
            set { searchPurchaseNumber = value; RaisePropertyChanged("SearchPurchaseNumber"); }
        }

        public string SearchPurchaseCustomer
        {
            get { return searchPurchaseCustomer; }
            set { searchPurchaseCustomer = value; RaisePropertyChanged("SearchPurchaseCustomer"); }
        }

        public string SearchPurchaseDate
        {
            get { return searchPurchaseDate; }
            set { searchPurchaseDate = value; RaisePropertyChanged("SearchPurchaseDate"); }
        }

        public int TabBucket_Selected
        {
            get { return tabBucket_Selected; }
            set { tabBucket_Selected = value; RaisePropertyChanged("TabBucket_Selected"); }
        }

        public string FinalizeArticle_Content
        {
            get { return updateArticle_Content; }
            set { updateArticle_Content = value; RaisePropertyChanged("FinalizeArticle_Content"); }
        }

        public int FinalizeArticle_Height
        {
            get { return updateArticle_Height; }
            set { updateArticle_Height = value; RaisePropertyChanged("FinalizeArticle_Height"); }
        }

        public string Bucket_Sum
        {
            get { return bucket_Sum; }
            set { bucket_Sum = value; RaisePropertyChanged("Bucket_Sum"); }
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

        public string Add_PurchasePrice
        {
            get { return add_PurchasePrice; }
            set { add_PurchasePrice = value; RaisePropertyChanged("Add_PurchasePrice"); }
        }

        public string Add_SellingPrice
        {
            get { return add_SellingPrice; }
            set { add_SellingPrice = value; RaisePropertyChanged("Add_SellingPrice"); }
        }

        public string Error_Deposit_Name
        {
            get { return error_Deposit_Name; }
            set { error_Deposit_Name = value; RaisePropertyChanged("Error_Deposit_Name"); }
        }

        public string Error_Deposit_Quantity
        {
            get { return error_Deposit_Quantity; }
            set { error_Deposit_Quantity = value; RaisePropertyChanged("Error_Deposit_Quantity"); }
        }

        public string Error_Deposit_PurchasePrice
        {
            get { return error_Deposit_PurchasePrice; }
            set { error_Deposit_PurchasePrice = value; RaisePropertyChanged("Error_Deposit_PurchasePrice"); }
        }

        public string Error_Deposit_Brand
        {
            get { return error_Deposit_Brand; }
            set { error_Deposit_Brand = value; RaisePropertyChanged("Error_Deposit_Brand"); }
        }

        public string Error_Deposit_Category
        {
            get { return error_Deposit_Category; }
            set { error_Deposit_Category = value; RaisePropertyChanged("Error_Deposit_Category"); }
        }

        public string Error_Deposit_SubCategory
        {
            get { return error_Deposit_SubCategory; }
            set { error_Deposit_SubCategory = value; RaisePropertyChanged("Error_Deposit_SubCategory"); }
        }

        public string Error_Bucket_Validate
        {
            get { return error_Bucket_Validate; }
            set { error_Bucket_Validate = value; RaisePropertyChanged("Error_Bucket_Validate"); }
        }

        public string Error_ToFinalize_Name
        {
            get { return error_ToFinalize_Name; }
            set { error_ToFinalize_Name = value; RaisePropertyChanged("Error_ToFinalize_Name"); }
        }

        public string Error_ToFinalize_EAN
        {
            get { return error_ToFinalize_EAN; }
            set { error_ToFinalize_EAN = value; RaisePropertyChanged("Error_ToFinalize_EAN"); }
        }

        public string Error_ToFinalize_Quantity
        {
            get { return error_ToFinalize_Quantity; }
            set { error_ToFinalize_Quantity = value; RaisePropertyChanged("Error_ToFinalize_Quantity"); }
        }

        public string Error_ToFinalize_PurchasePrice
        {
            get { return error_ToFinalize_PurchasePrice; }
            set { error_ToFinalize_PurchasePrice = value; RaisePropertyChanged("Error_ToFinalize_PurchasePrice"); }
        }

        public string Error_ToFinalize_TVA
        {
            get { return error_ToFinalize_TVA; }
            set { error_ToFinalize_TVA = value; RaisePropertyChanged("Error_ToFinalize_TVA"); }
        }

        public string Error_ToFinalize_Brand
        {
            get { return error_ToFinalize_Brand; }
            set { error_ToFinalize_Brand = value; RaisePropertyChanged("Error_ToFinalize_Brand"); }
        }

        public string Error_ToFinalize_Category
        {
            get { return error_ToFinalize_Category; }
            set { error_ToFinalize_Category = value; RaisePropertyChanged("Error_ToFinalize_Category"); }
        }

        public string Error_ToFinalize_SubCategory
        {
            get { return error_ToFinalize_SubCategory; }
            set { error_ToFinalize_SubCategory = value; RaisePropertyChanged("Error_ToFinalize_SubCategory"); }
        }

        public string Error_ToFinalize_SellingPrice
        {
            get { return error_ToFinalize_SellingPrice; }
            set { error_ToFinalize_SellingPrice = value; RaisePropertyChanged("Error_ToFinalize_SellingPrice"); }
        }

        public string Error_ToFinalize_Promo
        {
            get { return error_ToFinalize_Promo; }
            set { error_ToFinalize_Promo = value; RaisePropertyChanged("Error_ToFinalize_Promo"); }
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

        public string Transaction_Purchase_Number
        {
            get { return transaction_Purchase_Number; }
            set { transaction_Purchase_Number = value; RaisePropertyChanged("Transaction_Purchase_Number"); }
        }

        public string Transaction_Purchase_Employee
        {
            get { return transaction_Purchase_Employee; }
            set { transaction_Purchase_Employee = value; RaisePropertyChanged("Transaction_Purchase_Employee"); }
        }

        public string Transaction_Purchase_Customer
        {
            get { return transaction_Purchase_Customer; }
            set { transaction_Purchase_Customer = value; RaisePropertyChanged("Transaction_Purchase_Customer"); }
        }

        public IList<Article> Listing_Bucket
        {
            get { return listing_Bucket; }
            set { listing_Bucket = value; RaisePropertyChanged("Listing_Bucket"); }
        }

        public IList<Transaction> Listing_Transaction_Purchase
        {
            get { return listing_Transaction_Purchase; }
            set { listing_Transaction_Purchase = value; RaisePropertyChanged("Listing_Transaction_Purchase"); }
        }

        public IList<Article> Listing_Article_ToFinalize
        {
            get { return listing_Article_ToFinalize; }
            set { listing_Article_ToFinalize = value; RaisePropertyChanged("Listing_Article_ToFinalize"); }
        }

        public IList<Purchase> Listing_Transaction_Purchase_Details
        {
            get { return listing_Transaction_Purchase_Details; }
            set { listing_Transaction_Purchase_Details = value; RaisePropertyChanged("Listing_Transaction_Purchase_Details"); }
        }

        public IList<Brand> Cb_Brand
        {
            get
            {
                return Brand.Enumeration.Cast<Brand>().ToList();
            }
            set { cb_Brand = value; RaisePropertyChanged("Cb_Brand"); }
        }

        public IList<Category> Cb_Category
        {
            get
            {
                return Category.Enumeration.Cast<Category>().ToList();
            }
            set { cb_Category = value; RaisePropertyChanged("Cb_Category"); }
        }

        public IList<Sub_Category> Cb_SubCategory
        {
            get
            {
                return cb_SubCategory;
            }
            set { cb_SubCategory = value; RaisePropertyChanged("Cb_SubCategory"); }
        }

        public IList<Tva> Cb_TVA
        {
            get
            {
                return Tva.Enumeration.Cast<Tva>().ToList(); ;
            }
            set { cb_TVA = value; RaisePropertyChanged("Cb_TVA"); }
        }

        #endregion
    }
}