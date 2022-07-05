using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TFE.Model;

namespace TFE.ViewModel.PDF
{
    //x <summary>
    //! Classe regroupant le traitement des informations pour extraire un remboursement
    //x </summary>
    public class vm_PDF_Refund : ViewModelBase
    {
        #region -- PROPERTIES --
        private Transaction t_Refund;
        private decimal totalRefund;
        private string fullname;
        private int loyalty_Bonus;
        private int loyalty_Total;
        private Visibility ano_1;
        private Visibility ano_2;

        private IList<Refund> listing_Bucket;
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_PDF_Refund
        //x </summary>
        public vm_PDF_Refund(Transaction transaction, bool export)
        {
            if (transaction.Customer_id.Person_id.Lastname != "" && transaction.Customer_id.Person_id.Firstname != "")
                Fullname = $"{transaction.Customer_id.Person_id.Lastname.Trim().ToUpper()} {transaction.Customer_id.Person_id.Firstname.Trim()} ";
            else
                Fullname = transaction.Customer_id.Person_id.Mail;

            T_Refund = transaction;
            Listing_Bucket = Refund.Enumeration.Where(x => x.Transaction_id.Id == transaction.Id).ToList();

            decimal reduc = 0;
            foreach (var i in Listing_Bucket)
                reduc += (decimal)i.Total;

            if (transaction.Reduction != 0 && reduc > transaction.Reduction)
            {
                var s = Refund.Create(transaction
                    , Article.Create("Réduction accordées", "000000000"
                                                                       , Brand.ReferenceEntity
                                                                       , Category.ReferenceEntity, null
                                                                       , Price_Info.ReferenceEntity, 0, false, DateTime.Now)
                    , null, transaction.Reduction);

                Listing_Bucket.Add(s);
            }
            else if (transaction.Reduction != 0 && reduc < transaction.Reduction)
            {
                var s = Refund.Create(transaction
                    , Article.Create("Réduction accordées", "000000000"
                                                                       , Brand.ReferenceEntity
                                                                       , Category.ReferenceEntity, null
                                                                       , Price_Info.ReferenceEntity, 0, false, DateTime.Now)
                    , null, reduc);

                Listing_Bucket.Add(s);
            }

            var reductionExist = 0;

            foreach (var i in Listing_Bucket)
            {
                if (i.Article_id.Name == "Réduction accordées")
                {
                    TotalRefund -= (decimal)i.Total;
                    reductionExist = (int)((i.Total / 10) * 50);
                }
                else
                {
                    TotalRefund += (decimal)i.Total;
                }
            }

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

            for (int i = 0; i < TotalRefund; i += 2)
            {
                Loyalty_Bonus++;
                if (Loyalty_Bonus > ((int)TotalRefund / 2))
                    Loyalty_Bonus--;
            }

            Loyalty_Total = (int)c_load.Loyalty_points;

            if (export == false)
            {
                c_load.Loyalty_points = Loyalty_Total;
                for (int i = 50; i < reductionExist + 1; i += 50)
                    c_load.Loyalty_points += 50;
                c_load.Save();
                Loyalty_Total = (int)c_load.Loyalty_points;
            }
        }

        #region -- ACCESSORS --
        public Transaction T_Refund
        {
            get { return t_Refund; }
            set { t_Refund = value; RaisePropertyChanged("T_Refund"); }
        }

        public decimal TotalRefund
        {
            get { return totalRefund; }
            set { totalRefund = value; RaisePropertyChanged("TotalRefund"); }
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

        public IList<Refund> Listing_Bucket
        {
            get { return listing_Bucket; }
            set { listing_Bucket = value; RaisePropertyChanged("Listing_Bucket"); }
        }
        #endregion
    }
}
