using ExcelDataReader;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using TFE.Model;
using TFE.Tools;

namespace TFE.ViewModel
{
    //x <summary>
    //! Cette classe contient les propriétés auxquelles la vue v_Application_Article peut se lier avec le Binding.
    //x </summary>
    public class vm_Application_Article : ViewModelBase
    {
        #region -- PROPERTIES --
        private Article article_Selected;
        private Brand brand_Selected;
        private Category category_Selected;
        private Sub_Category subCategory_Selected;
        private Article newArticle;
        private Brand newBrand;
        private Category newCategory;
        private Sub_Category newSubCategory;
        private DataSet ds;
        private string searchName;
        private string searchEAN;
        private string searchBrandBis;
        private string searchBrand;
        private string searchCategory;
        private string searchSubCategory;
        private string updateArticle_Content;
        private int updateArticle_Height;
        private string updatePrice_Content;
        private int updatePrice_Height;
        private string addArticleSimple_Content;
        private int addArticleSimple_Height;
        private string details_Quantity;
        private string details_Purchase_Price;
        private bool details_Purchase_Price_IsEnabled;
        private string details_Selling_Price;
        private string details_TVA;
        private string details_Promo;
        private string excelFileName;
        private string add_PurchasePrice;
        private string add_SellingPrice;

        private string error_Details_Name;
        private string error_Details_Brand;
        private string error_Details_Category;
        private string error_Details_SubCategory;
        private string error_Details_Stock;
        private string error_Details_Stock_2;
        private string error_Details_Price;
        private string error_Brand;
        private string error_Category;
        private string error_SubCategory;
        private string error_Add_Name;
        private string error_Add_EAN;
        private string error_Add_Quantity;
        private string error_Add_PurchasePrice;
        private string error_Add_TVA;
        private string error_Add_Brand;
        private string error_Add_Category;
        private string error_Add_SubCategory;
        private string error_Add_SellingPrice;
        private string error_Add_SellingPriceTooBig;
        private string error_Add_Promo;
        private string error_Excel;
        private string import_Content;
        private string valid_Brand;
        private string valid_Category;
        private string valid_SubCategory;

        private IList<Article> listing_Article;
        private IList<Brand> listing_Brand;
        private IList<Category> listing_Category;
        private IList<Sub_Category> listing_SubCategory;
        private IList<Brand> cb_Brand;
        private IList<Category> cb_Category;
        private IList<Sub_Category> cb_SubCategory;
        private IList<Tva> cb_TVA;
        #endregion

        #region -- RELAY COMMAND DECLARATION --
        public RelayCommand RefreshBtn { get; private set; }
        public RelayCommand Listing_Category_Changed_Action { get; private set; }
        public RelayCommand Listing_Article_Changed_Action { get; private set; }
        public RelayCommand Cb_Category_Changed_Action { get; private set; }
        public RelayCommand FilterNameBtn { get; private set; }
        public RelayCommand FilterEANBtn { get; private set; }
        public RelayCommand FilterBrandBisBtn { get; private set; }
        public RelayCommand FilterBrandBtn { get; private set; }
        public RelayCommand FilterCategoryBtn { get; private set; }
        public RelayCommand FilterSubCategoryBtn { get; private set; }
        public RelayCommand AddBrandBtn { get; private set; }
        public RelayCommand UpdateBrandBtn { get; private set; }
        public RelayCommand<Brand> DeleteBrandBtn { get; private set; }
        public RelayCommand AddCategoryBtn { get; private set; }
        public RelayCommand UpdateCategoryBtn { get; private set; }
        public RelayCommand<Category> DeleteCategoryBtn { get; private set; }
        public RelayCommand AddSubCategoryBtn { get; private set; }
        public RelayCommand UpdateSubCategoryBtn { get; private set; }
        public RelayCommand<Sub_Category> DeleteSubCategoryBtn { get; private set; }
        public RelayCommand UpdateArticleBtn { get; private set; }
        public RelayCommand<Article> DeleteArticleBtn { get; private set; }
        public RelayCommand AddQuantityBtn { get; private set; }
        public RelayCommand RemoveQuantityBtn { get; private set; }
        public RelayCommand ModifyPriceBtn { get; private set; }
        public RelayCommand CreateArticleSimpleBtn { get; private set; }
        public RelayCommand SearchExcelFileBtn { get; private set; }
        public RelayCommand ImportExcelFileBtn { get; private set; }
        public RelayCommand Selected_BCSC_ChangedAction { get; private set; }
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_Application_Article
        //x </summary>
        public vm_Application_Article()
        {
            Listing_Category_Changed_Action = new RelayCommand(Listing_Category_Changed);
            Listing_Article_Changed_Action = new RelayCommand(Listing_Article_Changed);
            Cb_Category_Changed_Action = new RelayCommand(Cb_Category_Changed);
            FilterNameBtn = new RelayCommand(FilterName);
            FilterEANBtn = new RelayCommand(FilterEAN);
            FilterBrandBisBtn = new RelayCommand(FilterBrandBis);
            FilterBrandBtn = new RelayCommand(FilterBrand);
            FilterCategoryBtn = new RelayCommand(FilterCategory);
            FilterSubCategoryBtn = new RelayCommand(FilterSubCategory);
            AddBrandBtn = new RelayCommand(AddBrand);
            UpdateBrandBtn = new RelayCommand(UpdateBrand);
            DeleteBrandBtn = new RelayCommand<Brand>((brand) => DeleteBrand(brand));
            AddCategoryBtn = new RelayCommand(AddCategory);
            UpdateCategoryBtn = new RelayCommand(UpdateCategory);
            DeleteCategoryBtn = new RelayCommand<Category>((category) => DeleteCategory(category));
            AddSubCategoryBtn = new RelayCommand(AddSubCategory);
            UpdateSubCategoryBtn = new RelayCommand(UpdateSubCategory);
            DeleteSubCategoryBtn = new RelayCommand<Sub_Category>((sub_Category) => DeleteSubCategory(sub_Category));
            UpdateArticleBtn = new RelayCommand(UpdateArticle);
            DeleteArticleBtn = new RelayCommand<Article>((article) => DeleteArticle(article));
            AddQuantityBtn = new RelayCommand(AddQuantity);
            RemoveQuantityBtn = new RelayCommand(RemoveQuantity);
            ModifyPriceBtn = new RelayCommand(ModifyPrice);
            CreateArticleSimpleBtn = new RelayCommand(CreateArticleSimple);
            SearchExcelFileBtn = new RelayCommand(SearchExcelFile);
            ImportExcelFileBtn = new RelayCommand(ImportExcelFile);
            Selected_BCSC_ChangedAction = new RelayCommand(Selected_BCSC_Changed);

            RefreshBtn = new RelayCommand(Refresh);
            Refresh();
        }

        #region -- RELAY COMMAND METHODS --
        private void Refresh()
        {
            Listing_Article = Article.Enumeration.Where(x => x.Price_info_id.Selling_price > 0).ToArray();
            Listing_Brand = Brand.Enumeration.ToArray();
            Listing_Category = Category.Enumeration.ToArray();
            Listing_SubCategory = null;
            NewArticle = new Article(0, "", "", new Brand(0, ""), new Category(0, ""), null, new Price_Info(0, null, null, Tva.ReferenceEntity, null), null, false, DateTime.Now);
            NewBrand = new Brand(0, "");
            NewCategory = new Category(0, "");
            NewSubCategory = null;
            ds = null;
            ExcelFileName = string.Empty;
            Add_PurchasePrice = string.Empty;
            Add_SellingPrice = string.Empty;
            Details_Quantity = string.Empty;
            Details_Selling_Price = string.Empty;
            Details_Promo = string.Empty;
            Details_TVA = null;

            UpdateArticle_Content = Error.Clear;
            UpdateArticle_Height = 30;
            updatePrice_Content = Error.Clear;
            updatePrice_Height = 30;
            AddArticleSimple_Content = Error.Clear;
            AddArticleSimple_Height = 30;
            Import_Content = Error.Clear;

            Error_Details_Name = Error.Clear;
            Error_Details_Brand = Error.Clear;
            Error_Details_Category = Error.Clear;
            Error_Details_SubCategory = Error.Clear;
            Error_Details_Stock = Error.Clear;
            Error_Details_Price = Error.Clear;
            Error_Brand = Error.Clear;
            Error_Category = Error.Clear;
            Error_SubCategory = Error.Clear;
            Error_Add_Name = Error.Clear;
            Error_Add_EAN = Error.Clear;
            Error_Add_Quantity = Error.Clear;
            Error_Add_PurchasePrice = Error.Clear;
            Error_Add_TVA = Error.Clear;
            Error_Add_Brand = Error.Clear;
            Error_Add_Category = Error.Clear;
            Error_Add_SubCategory = Error.Clear;
            Error_Add_SellingPrice = Error.Clear;
            Error_Add_SellingPriceTooBig = Error.Clear;
            Error_Add_Promo = Error.Clear;
            Error_Excel = Error.Clear;
            Valid_Brand = Error.Clear;
            Valid_Category = Error.Clear;
            Valid_SubCategory = Error.Clear;

            Article_Selected = Article.ReferenceEntity;
            Article_Selected.Brand_id = Brand.ReferenceEntity;
            Article_Selected.Category_id = Category.ReferenceEntity;
            Article_Selected.Sub_category_id = Sub_Category.ReferenceEntity;
            Article_Selected.Price_info_id = Price_Info.ReferenceEntity;
            Brand_Selected = Brand.ReferenceEntity;
            Category_Selected = Category.ReferenceEntity;
            SubCategory_Selected = Sub_Category.ReferenceEntity;
        }

        private void Listing_Category_Changed()
        {
            if (Category_Selected != null)
            {
                Listing_SubCategory = Sub_Category.Enumeration.Where(x => x.Category_id.Id == Category_Selected.Id)
                                                              .ToArray();
                NewSubCategory = Sub_Category.ReferenceEntity;
            }
        }

        private void Listing_Article_Changed()
        {
            Cb_Category_Changed();
            if (Article_Selected.Deposit == true)
            {
                Details_Purchase_Price_IsEnabled = false;
                Details_Purchase_Price = Article_Selected.Price_info_id.Buying_price.ToString() + " €";
            }
            else
                Details_Purchase_Price_IsEnabled = true;
        }

        private void Cb_Category_Changed()
        {
            if (Article_Selected != null)
            {
                Cb_SubCategory = Sub_Category.Enumeration.Where(x => x.Category_id.Id == Article_Selected.Category_id.Id)
                                                         .Cast<Sub_Category>().ToList();
            }
            if (NewArticle != null && cb_SubCategory.Count == 0)
            {
                Cb_SubCategory = Sub_Category.Enumeration.Where(x => x.Category_id.Id == NewArticle.Category_id.Id)
                                                         .Cast<Sub_Category>().ToList();
            }
        }

        private void FilterName()
        {
            if (SearchName != "Recherche par nom")
            {
                SearchEAN = "Recherche par code EAN";
                SearchBrandBis = "Recherche par marque";

                IList<Article> la = Article.Enumeration.Where(x => x.Price_info_id.Selling_price > 0)
                                                       .Where(x => x.Name.ToLower().Contains(SearchName.Trim().ToLower()))
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
                SearchBrandBis = "Recherche par marque";

                IList<Article> la = Article.Enumeration.Where(x => x.Price_info_id.Selling_price > 0)
                                                       .Where(x => x.Ean_code.ToLower().Contains(SearchEAN.Trim().ToLower()))
                                                       .ToArray();
                if (la.Any())
                    Listing_Article = la;
                else
                    Listing_Article = null;
            }
            else
                Listing_Article = Article.Enumeration.ToArray();
        }

        private void FilterBrandBis()
        {
            if (SearchBrandBis != "Recherche par marque")
            {
                SearchName = "Recherche par nom";
                SearchEAN = "Recherche par code EAN";

                IList<Article> la = Article.Enumeration.Where(x => x.Price_info_id.Selling_price > 0)
                                                       .Where(x => x.Brand_id.Name.ToLower().Contains(SearchBrandBis.Trim().ToLower()))
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
            IList<Brand> lb = Brand.Enumeration.Where(x => x.Name.ToLower().Contains(SearchBrand.Trim().ToLower()))
                                               .ToArray();
            if (lb.Any())
                Listing_Brand = lb;
            else
                Listing_Brand = null;
        }

        private void FilterCategory()
        {
            IList<Category> lc = Category.Enumeration.Where(x => x.Name.ToLower().Contains(SearchCategory.Trim().ToLower()))
                                                     .ToArray();
            Listing_SubCategory = null;
            if (lc.Any())
                Listing_Category = lc;
            else
                Listing_Category = null;
        }

        private void FilterSubCategory()
        {
            IList<Sub_Category> lsc = Sub_Category.Enumeration.Where(x => x.Name.ToLower().Contains(SearchSubCategory.Trim().ToLower()))
                                                              .Where(x => x.Category_id.Id == Category_Selected.Id)
                                                              .ToArray();
            if (lsc.Any())
                Listing_SubCategory = lsc;
            else
                Listing_SubCategory = null;
        }

        private void AddBrand()
        {
            Error_Brand = Verify._SimpleText(NewBrand.Name, "Le nom", 3, 50, false, true);

            if (Error_Brand == Error.Clear)
            {
                Brand b_create = Brand.Create(NewBrand.Name);

                if (b_create.Save())
                {
                    NewBrand.Name = string.Empty;
                    var c = Category_Selected;
                    var sc = SubCategory_Selected;
                    Refresh();
                    SubCategory_Selected = sc;
                    Category_Selected = c;
                    Valid_Brand = Error.Add;
                    Error_Brand = Error.Clear;
                }
                else
                {
                    b_create.Delete();
                    Error_Brand = Error.BrandExist;
                    Valid_Brand = Error.Clear;
                }
            }
            else
                Valid_Brand = Error.Clear;
        }

        private void UpdateBrand()
        {
            Error_Brand = Verify._SimpleText(Brand_Selected.Name, "Le nom", 3, 50, false, true);

            if (Error_Brand == Error.Clear)
            {
                var old_c = Brand.Load(Brand_Selected.Id);
                var new_c = Brand_Selected;

                if (old_c.Name != new_c.Name)
                {
                    if (Brand.Update(ref old_c, new_c.Name))
                    {
                        Brand_Selected.Name = string.Empty;
                        var c = Category_Selected;
                        var sc = SubCategory_Selected;
                        Refresh();
                        SubCategory_Selected = sc;
                        Category_Selected = c;
                        Valid_Brand = Error.Update;
                        Error_Brand = Error.Clear;
                    }
                    else
                    {
                        Error_Brand = Error.BrandExist;
                        Valid_Brand = Error.Clear;
                    }
                }
            }
            else
                Valid_Brand = Error.Clear;
        }

        private void DeleteBrand(Brand brand)
        {
            if (brand.Id != 0)
            {
                IList<Article> la = Article.Enumeration.Where(x => x.Brand_id.Id == brand.Id)
                                                       .ToArray();

                if (la.Count() == 0)
                {
                    brand.Delete();
                    var c = Category_Selected;
                    var sc = SubCategory_Selected;
                    Refresh();
                    SubCategory_Selected = sc;
                    Category_Selected = c;
                    Valid_Brand = Error.Delete;
                    Error_Brand = Error.Clear;
                }
                else
                {
                    Error_Brand = Error.CantDeleteWithReason("\ndes articles sont associés à cette marque");
                    Valid_Brand = Error.Clear;
                }
            }
        }

        private void AddCategory()
        {
            Error_Category = Verify._SimpleText(NewCategory.Name, "Le nom", 3, 50, false, true);

            if (Error_Category == Error.Clear)
            {
                Category c_create = Category.Create(NewCategory.Name);

                if (c_create.Save())
                {
                    NewCategory.Name = string.Empty;
                    var b = Brand_Selected;
                    var sc = SubCategory_Selected;
                    Refresh();
                    SubCategory_Selected = sc;
                    Brand_Selected = b;
                    Valid_Category = Error.Add;
                    Error_Category = Error.Clear;
                }
                else
                {
                    c_create.Delete();
                    Error_Category = Error.CategoryExist;
                    Valid_Category = Error.Clear;
                }
            }
            else
                Valid_Category = Error.Clear;
        }

        private void UpdateCategory()
        {
            Error_Category = Verify._SimpleText(Category_Selected.Name, "Le nom", 3, 50, false, true);

            if (Error_Category == Error.Clear)
            {
                var old_c = Category.Load(Category_Selected.Id);
                var new_c = Category_Selected;

                if (old_c.Name != new_c.Name)
                {
                    if (Category.Update(ref old_c, new_c.Name))
                    {
                        Category_Selected.Name = string.Empty;
                        var b = Brand_Selected;
                        var sc = SubCategory_Selected;
                        Refresh();
                        SubCategory_Selected = sc;
                        Brand_Selected = b;
                        Valid_Category = Error.Update;
                        Error_Category = Error.Clear;
                    }
                    else
                    {
                        Error_Category = Error.CategoryExist;
                        Valid_Category = Error.Clear;
                    }
                }
            }
            else
                Valid_Category = Error.Clear;
        }

        private void DeleteCategory(Category category)
        {
            if (category.Id != 0)
            {
                IList<Article> la = Article.Enumeration.Where(x => x.Category_id.Id == category.Id)
                                                       .ToArray();
                IList<Sub_Category> lsc = Sub_Category.Enumeration.Where(x => x.Category_id.Id == category.Id)
                                                                  .ToArray();
                if (la.Count() == 0 && lsc.Count() == 0)
                {
                    category.Delete();
                    var b = Brand_Selected;
                    var sc = SubCategory_Selected;
                    Refresh();
                    SubCategory_Selected = sc;
                    Brand_Selected = b;
                    Valid_Category = Error.Delete;
                    Error_Category = Error.Clear;
                }
                else
                {
                    Error_Category = Error.CantDeleteWithReason("\ndes articles sont associés à cette catégorie");
                    Valid_Category = Error.Clear;
                }
            }
        }

        private void AddSubCategory()
        {
            Error_SubCategory = Verify._SimpleText(NewSubCategory.Name, "Le nom", 3, 50, false, true);

            if (Error_SubCategory == Error.Clear)
            {
                Sub_Category sc_create = Sub_Category.Create(Category_Selected, NewSubCategory.Name);

                if (sc_create.Save())
                {
                    NewSubCategory.Name = string.Empty;
                    var b = Brand_Selected;
                    var c = Category_Selected;
                    Refresh();
                    Category_Selected = c;
                    Brand_Selected = b;
                    Valid_SubCategory = Error.Add;
                    Error_SubCategory = Error.Clear;
                }
                else
                {
                    sc_create.Delete();
                    Error_SubCategory = Error.SubCategoryExist;
                    Valid_SubCategory = Error.Clear;
                }
            }
            else
                Valid_SubCategory = Error.Clear;
        }

        private void UpdateSubCategory()
        {
            Error_SubCategory = Verify._SimpleText(SubCategory_Selected.Name, "Le nom", 3, 50, false, true);

            if (Error_SubCategory == Error.Clear)
            {
                var old_c = Sub_Category.Load(SubCategory_Selected.Id);
                var new_c = SubCategory_Selected;

                if (old_c.Name != new_c.Name)
                {
                    if (Sub_Category.Update(ref old_c, new_c.Name))
                    {
                        SubCategory_Selected.Name = string.Empty;
                        var b = Brand_Selected;
                        var c = Category_Selected;
                        Refresh();
                        Category_Selected = c;
                        Brand_Selected = b;
                        Valid_SubCategory = Error.Update;
                        Error_SubCategory = Error.Clear;
                    }
                    else
                    {
                        Error_SubCategory = Error.SubCategoryExist;
                        Valid_SubCategory = Error.Clear;
                    }
                }
            }
            else
                Valid_SubCategory = Error.Clear;
        }

        private void DeleteSubCategory(Sub_Category sub_Category)
        {
            if (sub_Category.Id != 0)
            {

                IList<Article> la = Article.Enumeration.Where(x => x.Sub_category_id != null)
                                                       .Where(x => x.Sub_category_id.Id == sub_Category.Id)
                                                       .ToArray();

                if (la.Any())
                {
                    foreach (Article a in la)
                    {
                        a.Sub_category_id = null;
                        a.Save();
                    }
                }
                sub_Category.Delete();
                var b = Brand_Selected;
                var c = Category_Selected;
                Refresh();
                Category_Selected = c;
                Brand_Selected = b;
                Valid_SubCategory = Error.Delete;
                Error_SubCategory = Error.Clear;
            }
        }

        private void UpdateArticle()
        {
            Error_Details_Name = Verify._SimpleText(Article_Selected.Name, "Le nom", 0, 50, false, true);
            Error_Details_Brand = Verify._Brand(Article_Selected.Brand_id);
            Error_Details_Category = Verify._Category(Article_Selected.Category_id);

            if (Error_Details_Name == Error.Clear && Error_Details_Brand == Error.Clear && Error_Details_Category == Error.Clear)
            {
                var a = Article_Selected;
                Article.Update(ref a, a.Name, a.Ean_code, a.Brand_id, a.Category_id, a.Sub_category_id, a.Price_info_id, (int)a.Quantity, a.Deposit, a.Date);

                UpdateArticle_Content = Error.Update;
                UpdateArticle_Height = 0;
            }
        }

        private void DeleteArticle(Article article)
        {
            if (article.Id != 0)
            {
                IList<Sale> ls = Sale.Enumeration.Where(x => x.Article_id.Id == article.Id)
                                 .ToArray();
                IList<Purchase> lp = Purchase.Enumeration.Where(x => x.Article_id.Id == article.Id)
                                                         .ToArray();

                if (ls.Count() == 0 && lp.Count() == 0)
                {
                    article.Delete();
                    article.Price_info_id.Delete();
                }

                Refresh();
            }
        }

        private void AddQuantity()
        {
            if (Details_Quantity != null)
            {
                if (Details_Quantity.Trim() != "")
                {
                    if (int.TryParse(Details_Quantity, out int result))
                    {
                        if (result >= 0)
                        {
                            Article.Update_Add_Stock(Article_Selected, result);
                            var a = Article_Selected;
                            Refresh();
                            Error_Details_Stock = Error.Update;
                            Error_Details_Stock_2 = Error.Clear;
                            Article_Selected = a;
                            Details_Quantity = null;
                        }
                    }
                    else
                    {
                        Error_Details_Stock_2 = Error.NoWhiteSpaceAuthorized;
                        Error_Details_Stock = Error.Clear;
                    }
                }
                else
                {
                    Error_Details_Stock = Error.Clear;
                    Error_Details_Stock_2 = Error.IsNullOrEmpty;
                }
            }
            else
            {
                Error_Details_Stock = Error.Clear;
                Error_Details_Stock_2 = Error.IsNullOrEmpty;
            }
        }

        private void RemoveQuantity()
        {
            if (Details_Quantity != null)
            {
                if (Details_Quantity.Trim() != "")
                {
                    if (int.TryParse(Details_Quantity, out int result))
                    {
                        if (result <= Article_Selected.Quantity)
                        {
                            Article.Update_Remove_Stock(Article_Selected, result);
                            var a = Article_Selected;
                            Refresh();
                            Error_Details_Stock = Error.Update;
                            Error_Details_Stock_2 = Error.Clear;
                            Article_Selected = a;
                            Details_Quantity = null;
                        }
                        else
                        {
                            Error_Details_Stock_2 = Error.StockTooBig;
                            Error_Details_Stock = Error.Clear;
                        }
                    }
                    else
                    {
                        Error_Details_Stock_2 = Error.NoWhiteSpaceAuthorized;
                        Error_Details_Stock = Error.Clear;
                    }
                }
                else
                {
                    Error_Details_Stock_2 = Error.IsNullOrEmpty;
                    Error_Details_Stock = Error.Clear;
                }
            }
            else
            {
                Error_Details_Stock_2 = Error.IsNullOrEmpty;
                Error_Details_Stock = Error.Clear;
            }
        }

        private void ModifyPrice()
        {
            if (Details_Purchase_Price != null && Details_Selling_Price != null && Details_TVA != null && Details_Promo != null)
            {
                if (decimal.TryParse(Details_Purchase_Price.Replace(".", ",").Replace("€",""), out decimal pp) &&
                    decimal.TryParse(Details_Selling_Price.Replace(".", ",").Replace("€", ""), out decimal sp) &&
                    double.TryParse(Details_Promo.Replace(".", ",").Replace("€", ""), out double p))
                {
                    if (pp > 0)
                    {
                        if (sp > pp)
                        {
                            if (p >= 0 && p < 100)
                            {
                                Price_Info pi = Price_Info.Load(article_Selected.Price_info_id.Id);
                                Tva t = Tva.Load(uint.Parse(Details_TVA));
                                Price_Info.Update(ref pi, pp, sp, t, p / 100);
                                Article_Selected.Price_info_id = pi;
                                var a = Article_Selected;
                                Refresh();
                                Article_Selected = a;
                                Details_Promo = null;
                                Details_Purchase_Price = null;
                                Details_Selling_Price = null;
                                Details_TVA = null;

                                UpdatePrice_Content = Error.Update;
                                UpdatePrice_Height = 0;
                            }
                            else
                                Error_Details_Price = Error.PromotionInvalid;
                        }
                        else
                            Error_Details_Price = Error.PriceTooBig;
                    }
                    else
                        Error_Details_Price = Error.PriceTooLow;
                }
                else
                    Error_Details_Price = Error.PriceValid;
            }
            else
                Error_Details_Price = Error.IsNullOrEmptyMultiple;
        }

        private void CreateArticleSimple()
        {
            Error_Add_Name = Verify._SimpleText(NewArticle.Name, "Le nom", 0, 100, false, true);
            Error_Add_EAN = Verify._SimpleText(NewArticle.Ean_code, "Le code", 0, 50, false, false);
            Error_Add_Quantity = Verify._Int(NewArticle.Quantity.ToString(), out int quantity_to_add);
            Error_Add_PurchasePrice = Verify._Decimal(Add_PurchasePrice, out decimal pp);
            Error_Add_TVA = Verify._Tva(NewArticle.Price_info_id.Tva_id);
            Error_Add_Brand = Verify._Brand(NewArticle.Brand_id);
            Error_Add_Category = Verify._Category(NewArticle.Category_id);
            Error_Add_SellingPrice = Verify._Decimal(Add_SellingPrice, out decimal sp);
            Error_Add_Promo = Verify._Double(NewArticle.Price_info_id.Promotion.ToString(), out double p);
            if (Error_Add_PurchasePrice == Error.Clear && Error_Add_SellingPrice == Error.Clear && sp <= pp)
                Error_Add_SellingPriceTooBig = Error.PriceTooBig;
            else
                Error_Add_SellingPriceTooBig = Error.Clear;
            if (Error_Add_Promo == Error.Clear && (p < 0 || p > 99))
                Error_Add_Promo = Error.PromoValid;
            if (Error_Add_Quantity == Error.Clear && quantity_to_add < 0)
                Error_Add_Quantity = Error.QuantityInvalid;
            if (Error_Add_EAN == Error.Clear)
            {
                var EANExist = Article.Enumeration.Where(x => x.Ean_code == NewArticle.Ean_code)
                                                  .Count();
                if (EANExist != 0)
                    Error_Add_EAN = Error.ArticleExist;
            }

            if (Error_Add_Name == Error.Clear && Error_Add_EAN == Error.Clear &&
               Error_Add_Quantity == Error.Clear && Error_Add_PurchasePrice == Error.Clear &&
               Error_Add_TVA == Error.Clear && Error_Add_Brand == Error.Clear &&
               Error_Add_Category == Error.Clear && Error_Add_SellingPrice == Error.Clear &&
               Error_Add_Promo == Error.Clear && Error_Add_SellingPriceTooBig == Error.Clear)
            {
                NewArticle.Price_info_id.Buying_price = pp;
                NewArticle.Price_info_id.Selling_price = sp;

                Brand b_load = Brand.Load(NewArticle.Brand_id.Id);
                Tva t_load = Tva.Load(NewArticle.Price_info_id.Tva_id.Id);
                Category c_load = Category.Load(NewArticle.Category_id.Id);
                Sub_Category sc_load = null;
                if (NewArticle.Sub_category_id != null)
                    sc_load = Sub_Category.Load(NewArticle.Sub_category_id.Id);

                Price_Info pi_create = Price_Info.Create(pp, sp, t_load, p / 100);
                if (pi_create.Save())
                {
                    Article a_create = Article.Create(NewArticle.Name, NewArticle.Ean_code, b_load, c_load, sc_load
                                                        , pi_create, quantity_to_add, false, default);
                    if (a_create.Save())
                    {
                        AddArticleSimple_Content = Error.Add;
                        AddArticleSimple_Height = 0;
                    }
                    else
                    {
                        pi_create.Delete();
                        a_create.Delete();
                        AddArticleSimple_Content = Error.ArticleExist;
                        AddArticleSimple_Height = 0;
                    }
                }
                else
                {
                    pi_create.Delete();
                    AddArticleSimple_Content = Error.ArticleExist;
                    AddArticleSimple_Height = 0;
                }
            }
        }

        private void SearchExcelFile()
        {
            import_Content = Error.Clear;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Fichier Excel| *.xls; *.xlsx; *.xlsm";
            if (ofd.ShowDialog() == true)
            {
                ExcelFileName = ofd.SafeFileName.ToString();
                ds = new DataSet();
                try
                {
                    FileStream fs = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read);
                    IExcelDataReader edr = ExcelReaderFactory.CreateOpenXmlReader(fs);
                    ds = edr.AsDataSet();
                    edr.Close();
                    Error_Excel = Error.Clear;
                }
                catch (Exception)
                {
                    Error_Excel = Error.ExcelOpen;
                }
            }
        }

        private void ImportExcelFile()
        {
            if (ds == null)
            {
                Error_Excel = Error.ExcelNotSelected;
                return;
            }
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable dt = ds.Tables[i];

                bool canBeImport = false;

                foreach (DataRow dr in dt.Rows)
                {
                    if (dt.Rows.IndexOf(dr) != 0)
                    {
                        string e_0 = Verify._SimpleText(dr.ItemArray[0].ToString(), "Le code", 0, 50, false, false);
                        string e_1 = Verify._SimpleText(dr.ItemArray[1].ToString(), "Le nom", 0, 100, false, true);
                        string e_2 = Verify._SimpleText(dr.ItemArray[2].ToString(), "La marque", 0, 50, false, true);
                        string e_3 = Verify._SimpleText(dr.ItemArray[3].ToString(), "La catégorie", 0, 50, false, true);
                        string e_4 = Verify._SimpleText(dr.ItemArray[4].ToString(), "La sous-catégorie", 0, 50, true, true);
                        string e_5 = Verify._Decimal(dr.ItemArray[5].ToString(), out decimal purchasePrice);
                        string e_6 = Verify._Decimal(dr.ItemArray[6].ToString(), out decimal sellingPrice);
                        string e_7 = Verify._Double(dr.ItemArray[7].ToString(), out double tva);
                        string e_8 = Verify._Int(dr.ItemArray[8].ToString(), out int quantity);
                        string e_9 = Verify._Double(dr.ItemArray[9].ToString(), out double promo);

                        if (e_0 != Error.Clear || e_1 != Error.Clear || e_2 != Error.Clear || e_3 != Error.Clear || e_4 != Error.Clear ||
                            e_5 != Error.Clear || e_6 != Error.Clear || e_7 != Error.Clear || e_8 != Error.Clear || e_9 != Error.Clear)
                        {
                            Import_Content = Error.Clear;
                            Error_Excel = Error.ExcelFailed(dt.Rows.IndexOf(dr) + 1);
                            return;
                        }
                        else
                        {
                            Tva t_search = Tva.Enumeration.FirstOrDefault(x => x.Value == tva);
                            if (quantity > 0 && t_search != null
                                && purchasePrice < sellingPrice && promo < 100 && promo >= 0)
                            {
                                canBeImport = true;
                            }
                            else
                            {
                                Import_Content = Error.Clear;
                                Error_Excel = Error.ExcelFailed(dt.Rows.IndexOf(dr) + 1);
                                return;
                            }
                        }
                    }
                }

                if (canBeImport)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Rows.IndexOf(dr) != 0)
                        {
                            decimal purchasePrice = decimal.Parse(dr.ItemArray[5].ToString());
                            decimal sellingPrice = decimal.Parse(dr.ItemArray[6].ToString());
                            double tva = double.Parse(dr.ItemArray[7].ToString());
                            int quantity = int.Parse(dr.ItemArray[8].ToString());
                            double promo = double.Parse(dr.ItemArray[9].ToString());

                            string ean = dr.ItemArray[0].ToString();
                            Article ean_search = Article.Enumeration.FirstOrDefault(x => x.Ean_code == ean);
                            string name = dr.ItemArray[1].ToString();
                            Brand brand = null;
                            Category category = null;
                            Sub_Category subCategory = null;
                            Tva t_search = Tva.Enumeration.FirstOrDefault(x => x.Value == tva);

                            #region -- BRAND -- 
                            var b_search = Brand.Enumeration.Where(x => x.Name == dr.ItemArray[2].ToString())
                                                            .FirstOrDefault();
                            if (b_search != null)
                                brand = Brand.Load(b_search.Id);
                            else
                            {
                                brand = Brand.Create(dr.ItemArray[2].ToString());
                                brand.Save();
                            }
                            #endregion

                            #region -- CATEGORY -- 
                            var c_search = Category.Enumeration.Where(x => x.Name == dr.ItemArray[3].ToString())
                                                               .FirstOrDefault();
                            if (c_search != null)
                                category = Category.Load(c_search.Id);
                            else
                            {
                                category = Category.Create(dr.ItemArray[3].ToString());
                                category.Save();
                            }
                            #endregion

                            #region -- SUB_CATEGORY -- 
                            var sc_search = Sub_Category.Enumeration.Where(x => x.Name == dr.ItemArray[4].ToString())
                                                                    .Where(x => x.Category_id.Id == category.Id)
                                                                    .FirstOrDefault();
                            if (sc_search != null)
                                subCategory = Sub_Category.Load(sc_search.Id);
                            else
                            {
                                subCategory = Sub_Category.Create(category, dr.ItemArray[4].ToString());
                                if (subCategory != null)
                                    subCategory.Save();
                            }
                            #endregion

                            if (ean_search == null)
                            {
                                Price_Info pi = Price_Info.Create(purchasePrice, sellingPrice, t_search, promo);
                                pi.Save();
                                Article a_create = Article.Create(name, ean, brand, category, subCategory, pi, quantity, false, DateTime.Now);
                                a_create.Save();
                            }
                            else
                            {
                                ean_search.Price_info_id.Buying_price = purchasePrice;
                                ean_search.Price_info_id.Selling_price = sellingPrice;
                                ean_search.Price_info_id.Tva_id = t_search;
                                ean_search.Price_info_id.Promotion = promo;

                                ean_search.Name = name;
                                ean_search.Brand_id = brand;
                                ean_search.Category_id = category;
                                ean_search.Sub_category_id = subCategory;
                                ean_search.Quantity += quantity;
                                ean_search.Date = DateTime.Now;

                                ean_search.Price_info_id.Save();
                                ean_search.Brand_id.Save();
                                ean_search.Category_id.Save();
                                if (ean_search.Sub_category_id != null)
                                    ean_search.Sub_category_id.Save();
                                ean_search.Save();
                            }
                        }
                    }
                    Import_Content = Error.ExcelValid;
                }
            }
        }

        private void Selected_BCSC_Changed()
        {
            Brand b_temp = null;
            Category c_temp = null;
            Sub_Category sc_temp = null;

            if (Brand_Selected != null)
                if (Brand_Selected.Id != 0)
                    b_temp = Brand_Selected;
            if (Category_Selected != null)
                if (Category_Selected.Id != 0)
                    c_temp = Category_Selected;
            if (SubCategory_Selected != null)
                if (SubCategory_Selected.Id != 0)
                    sc_temp = SubCategory_Selected;

            if (b_temp != null)
            {
                Valid_Brand = Error.Clear;
                Listing_Brand = Brand.Enumeration.ToArray();
                Brand_Selected = b_temp;
            }
            if (c_temp != null)
            {
                Valid_Category = Error.Clear;
                Listing_Category = Category.Enumeration.ToArray();
                Category_Selected = c_temp;
            }
            if (sc_temp != null)
            {
                Listing_Category_Changed();
                Valid_SubCategory = Error.Clear;
                SubCategory_Selected = sc_temp;
            }

            SearchBrand = "Recherche par marque";
            SearchCategory = "Recherche par catégorie";
            SearchSubCategory = "Recherche par sous-catégorie";
        }
        #endregion

        #region -- ACCESSORS --
        public Article Article_Selected
        {
            get { return article_Selected; }
            set { article_Selected = value; RaisePropertyChanged("Article_Selected"); }
        }

        public Brand Brand_Selected
        {
            get { return brand_Selected; }
            set { brand_Selected = value; RaisePropertyChanged("Brand_Selected"); }
        }

        public Category Category_Selected
        {
            get { return category_Selected; }
            set { category_Selected = value; RaisePropertyChanged("Category_Selected"); }
        }

        public Sub_Category SubCategory_Selected
        {
            get { return subCategory_Selected; }
            set { subCategory_Selected = value; RaisePropertyChanged("SubCategory_Selected"); }
        }

        public Article NewArticle
        {
            get { return newArticle; }
            set { newArticle = value; RaisePropertyChanged("NewArticle"); }
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

        public string SearchBrandBis
        {
            get { return searchBrandBis; }
            set { searchBrandBis = value; RaisePropertyChanged("SearchBrandBis"); }
        }

        public string SearchBrand
        {
            get { return searchBrand; }
            set { searchBrand = value; RaisePropertyChanged("SearchBrand"); }
        }

        public string SearchCategory
        {
            get { return searchCategory; }
            set { searchCategory = value; RaisePropertyChanged("SearchCategory"); }
        }

        public string SearchSubCategory
        {
            get { return searchSubCategory; }
            set { searchSubCategory = value; RaisePropertyChanged("SearchSubCategory"); }
        }

        public string UpdateArticle_Content
        {
            get { return updateArticle_Content; }
            set { updateArticle_Content = value; RaisePropertyChanged("UpdateArticle_Content"); }
        }

        public int UpdateArticle_Height
        {
            get { return updateArticle_Height; }
            set { updateArticle_Height = value; RaisePropertyChanged("UpdateArticle_Height"); }
        }

        public string UpdatePrice_Content
        {
            get { return updatePrice_Content; }
            set { updatePrice_Content = value; RaisePropertyChanged("UpdatePrice_Content"); }
        }

        public int UpdatePrice_Height
        {
            get { return updatePrice_Height; }
            set { updatePrice_Height = value; RaisePropertyChanged("UpdatePrice_Height"); }
        }

        public string AddArticleSimple_Content
        {
            get { return addArticleSimple_Content; }
            set { addArticleSimple_Content = value; RaisePropertyChanged("AddArticleSimple_Content"); }
        }

        public int AddArticleSimple_Height
        {
            get { return addArticleSimple_Height; }
            set { addArticleSimple_Height = value; RaisePropertyChanged("AddArticleSimple_Height"); }
        }

        public string Details_Quantity
        {
            get { return details_Quantity; }
            set { details_Quantity = value; RaisePropertyChanged("Details_Quantity"); }
        }

        public string Details_Purchase_Price
        {
            get { return details_Purchase_Price; }
            set { details_Purchase_Price = value; RaisePropertyChanged("Details_Purchase_Price"); }
        }

        public bool Details_Purchase_Price_IsEnabled
        {
            get { return details_Purchase_Price_IsEnabled; }
            set { details_Purchase_Price_IsEnabled = value; RaisePropertyChanged("Details_Purchase_Price_IsEnabled"); }
        }

        public string Details_Selling_Price
        {
            get { return details_Selling_Price; }
            set { details_Selling_Price = value; RaisePropertyChanged("Details_Selling_Price"); }
        }

        public string Details_TVA
        {
            get { return details_TVA; }
            set { details_TVA = value; RaisePropertyChanged("Details_TVA"); }
        }

        public string Details_Promo
        {
            get { return details_Promo; }
            set { details_Promo = value; RaisePropertyChanged("Details_Promo"); }
        }

        public string ExcelFileName
        {
            get { return excelFileName; }
            set { excelFileName = value; RaisePropertyChanged("ExcelFileName"); }
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

        public string Error_Details_Name
        {
            get { return error_Details_Name; }
            set { error_Details_Name = value; RaisePropertyChanged("Error_Details_Name"); }
        }

        public string Error_Details_Brand
        {
            get { return error_Details_Brand; }
            set { error_Details_Brand = value; RaisePropertyChanged("Error_Details_Brand"); }
        }

        public string Error_Details_Category
        {
            get { return error_Details_Category; }
            set { error_Details_Category = value; RaisePropertyChanged("Error_Details_Category"); }
        }

        public string Error_Details_SubCategory
        {
            get { return error_Details_SubCategory; }
            set { error_Details_SubCategory = value; RaisePropertyChanged("Error_Details_SubCategory"); }
        }

        public string Error_Details_Stock
        {
            get { return error_Details_Stock; }
            set { error_Details_Stock = value; RaisePropertyChanged("Error_Details_Stock"); }
        }

        public string Error_Details_Stock_2
        {
            get { return error_Details_Stock_2; }
            set { error_Details_Stock_2 = value; RaisePropertyChanged("Error_Details_Stock_2"); }
        }

        public string Error_Details_Price
        {
            get { return error_Details_Price; }
            set { error_Details_Price = value; RaisePropertyChanged("Error_Details_Price"); }
        }

        public string Error_Brand
        {
            get { return error_Brand; }
            set { error_Brand = value; RaisePropertyChanged("Error_Brand"); }
        }

        public string Error_Category
        {
            get { return error_Category; }
            set { error_Category = value; RaisePropertyChanged("Error_Category"); }
        }

        public string Error_SubCategory
        {
            get { return error_SubCategory; }
            set { error_SubCategory = value; RaisePropertyChanged("Error_SubCategory"); }
        }

        public string Error_Add_Name
        {
            get { return error_Add_Name; }
            set { error_Add_Name = value; RaisePropertyChanged("Error_Add_Name"); }
        }

        public string Error_Add_EAN
        {
            get { return error_Add_EAN; }
            set { error_Add_EAN = value; RaisePropertyChanged("Error_Add_EAN"); }
        }

        public string Error_Add_Quantity
        {
            get { return error_Add_Quantity; }
            set { error_Add_Quantity = value; RaisePropertyChanged("Error_Add_Quantity"); }
        }

        public string Error_Add_PurchasePrice
        {
            get { return error_Add_PurchasePrice; }
            set { error_Add_PurchasePrice = value; RaisePropertyChanged("Error_Add_PurchasePrice"); }
        }

        public string Error_Add_TVA
        {
            get { return error_Add_TVA; }
            set { error_Add_TVA = value; RaisePropertyChanged("Error_Add_TVA"); }
        }

        public string Error_Add_Brand
        {
            get { return error_Add_Brand; }
            set { error_Add_Brand = value; RaisePropertyChanged("Error_Add_Brand"); }
        }

        public string Error_Add_Category
        {
            get { return error_Add_Category; }
            set { error_Add_Category = value; RaisePropertyChanged("Error_Add_Category"); }
        }

        public string Error_Add_SubCategory
        {
            get { return error_Add_SubCategory; }
            set { error_Add_SubCategory = value; RaisePropertyChanged("Error_Add_SubCategory"); }
        }

        public string Error_Add_SellingPrice
        {
            get { return error_Add_SellingPrice; }
            set { error_Add_SellingPrice = value; RaisePropertyChanged("Error_Add_SellingPrice"); }
        }

        public string Error_Add_SellingPriceTooBig
        {
            get { return error_Add_SellingPriceTooBig; }
            set { error_Add_SellingPriceTooBig = value; RaisePropertyChanged("Error_Add_SellingPriceTooBig"); }
        }

        public string Error_Add_Promo
        {
            get { return error_Add_Promo; }
            set { error_Add_Promo = value; RaisePropertyChanged("Error_Add_Promo"); }
        }

        public string Error_Excel
        {
            get { return error_Excel; }
            set { error_Excel = value; RaisePropertyChanged("Error_Excel"); }
        }

        public string Import_Content
        {
            get { return import_Content; }
            set { import_Content = value; RaisePropertyChanged("Import_Content"); }
        }

        public string Valid_Brand
        {
            get { return valid_Brand; }
            set { valid_Brand = value; RaisePropertyChanged("Valid_Brand"); }
        }

        public string Valid_Category
        {
            get { return valid_Category; }
            set { valid_Category = value; RaisePropertyChanged("Valid_Category"); }
        }

        public string Valid_SubCategory
        {
            get { return valid_SubCategory; }
            set { valid_SubCategory = value; RaisePropertyChanged("Valid_SubCategory"); }
        }

        public IList<Article> Listing_Article
        {
            get
            {
                return listing_Article;
            }
            set { listing_Article = value; RaisePropertyChanged("Listing_Article"); }
        }

        public IList<Brand> Listing_Brand
        {
            get
            {
                return listing_Brand;
            }
            set { listing_Brand = value; RaisePropertyChanged("Listing_Brand"); }
        }

        public IList<Category> Listing_Category
        {
            get
            {
                return listing_Category;
            }
            set { listing_Category = value; RaisePropertyChanged("Listing_Category"); }
        }

        public IList<Sub_Category> Listing_SubCategory
        {
            get
            {
                return listing_SubCategory;
            }
            set { listing_SubCategory = value; RaisePropertyChanged("Listing_SubCategory"); }
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