using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TFE.Model;

namespace TFE.ViewModel.PDF
{
    //x <summary>
    //! Classe regroupant le traitement des informations pour extraire une facture de vente
    //x </summary>
    public class vm_PDF_Bill : ViewModelBase
    {
        #region -- PROPERTIES --
        private Customer c_Sale;
        private Transaction t_Sale;
        private decimal totalTVAC;
        private decimal totalHTVA;
        private decimal diffTVA;
        private string fullname;
        private int loyalty_Bonus;
        private int loyalty_Total;
        private Visibility ano_1;
        private Visibility ano_2;

        private IList<Sale> listing_Bucket;
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_PDF_Bill
        //x </summary>
        public vm_PDF_Bill(Transaction transaction, bool export)
        {
            if (transaction.Customer_id.Person_id.Lastname != "" && transaction.Customer_id.Person_id.Firstname != "")
                Fullname = $"{transaction.Customer_id.Person_id.Lastname.Trim().ToUpper()} {transaction.Customer_id.Person_id.Firstname.Trim()} ";
            else
                Fullname = transaction.Customer_id.Person_id.Mail;

            T_Sale = transaction;
            Listing_Bucket = Sale.Enumeration.Where(x => x.Transaction_id.Id == transaction.Id).ToList();

            if (transaction.Reduction != 0)
            {
                var s = Sale.Create(transaction
                    , Article.Create("Réduction des points de fidélité", "000000000"
                                                                       , Brand.ReferenceEntity
                                                                       , Category.ReferenceEntity, null
                                                                       , Price_Info.ReferenceEntity, 0, false, DateTime.Now)
                    , null, transaction.Reduction);

                Listing_Bucket.Add(s);

            }

            var reductionExist = 0;

            foreach (var i in Listing_Bucket)
            {
                if (i.Article_id.Name == "Réduction des points de fidélité")
                {
                    TotalTVAC -= (decimal)i.Total;
                    TotalHTVA -= (decimal)i.Total * (1 - (decimal)0.21);
                    reductionExist = (int)((i.Total / 10) * 50);
                }
                else
                {
                    TotalTVAC += (decimal)i.Total;
                    TotalHTVA += (decimal)i.Total * (1 - (decimal)i.Article_id.Price_info_id.Tva_id.Value);
                }
            }

            DiffTVA = TotalTVAC - TotalHTVA;

            Customer c_load = Customer.Load(transaction.Customer_id.Id);

            if (c_load.Id == 1)
            {
                Ano_1 = Visibility.Collapsed;
                Ano_2 = Visibility.Collapsed;
            }
            else
            {
                Ano_1 = Visibility.Visible;
                Ano_2 = Visibility.Visible;
            }

            for (int i = 0; i < TotalTVAC; i += 2)
            {
                Loyalty_Bonus++;
                if (Loyalty_Bonus > ((int)TotalTVAC/2))
                    Loyalty_Bonus--;
            }

            Loyalty_Total = (int)c_load.Loyalty_points + Loyalty_Bonus;

            if (export == false)
            {
                c_load.Loyalty_points = Loyalty_Total - reductionExist;
                Loyalty_Total = (int)c_load.Loyalty_points;
                c_load.Save();
            }
            else
                Loyalty_Total -= Loyalty_Bonus;
        }

        #region -- ACCESSORS --
        public Customer C_Sale
        {
            get { return c_Sale; }
            set { c_Sale = value; RaisePropertyChanged("C_Sale"); }
        }

        public Transaction T_Sale
        {
            get { return t_Sale; }
            set { t_Sale = value; RaisePropertyChanged("T_Sale"); }
        }

        public decimal TotalTVAC
        {
            get { return totalTVAC; }
            set { totalTVAC = value; RaisePropertyChanged("TotalTVAC"); }
        }

        public decimal TotalHTVA
        {
            get { return totalHTVA; }
            set { totalHTVA = value; RaisePropertyChanged("TotalHTVA"); }
        }

        public decimal DiffTVA
        {
            get { return diffTVA; }
            set { diffTVA = value; RaisePropertyChanged("DiffTVA"); }
        }

        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; RaisePropertyChanged("Fullname"); }
        }

        public int Loyalty_Bonus
        {
            get { return loyalty_Bonus; }
            set { loyalty_Bonus = value; RaisePropertyChanged("Loyalty_Bonus"); }
        }

        public int Loyalty_Total
        {
            get { return loyalty_Total; }
            set { loyalty_Total = value; RaisePropertyChanged("Loyalty_Total"); }
        }

        public Visibility Ano_1
        {
            get { return ano_1; }
            set { ano_1 = value; RaisePropertyChanged("Ano_1"); }
        }

        public Visibility Ano_2
        {
            get { return ano_2; }
            set { ano_2 = value; RaisePropertyChanged("Ano_2"); }
        }

        public IList<Sale> Listing_Bucket
        {
            get { return listing_Bucket; }
            set { listing_Bucket = value; RaisePropertyChanged("Listing_Bucket"); }
        }
        #endregion
    }
}
