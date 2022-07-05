using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using TFE.Model;

namespace TFE.ViewModel.PDF
{
    //x <summary>
    //! Classe regroupant le traitement des informations pour extraire un ensemble de transactions
    //x </summary>
    public class vm_PDF_Accounting : ViewModelBase
    {
        #region -- PROPERTIES --
        private string firstDate;
        private string secondDate;
        private int nb_Sales;
        private int nb_Deposit;
        private decimal tip;
        private decimal total;

        private List<Transaction> listing_Accounting;
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_PDF_Accounting
        //x </summary>
        public vm_PDF_Accounting(DateTime first, DateTime second)
        {
            Nb_Sales = 0;
            Nb_Deposit = 0;
            Total = 0;
            Listing_Accounting = new List<Transaction>();
            FirstDate = first.Date.ToLongDateString();
            SecondDate = second.Date.ToLongDateString();
            var AllTransaction = Transaction.Enumeration;

            foreach (var t in AllTransaction)
            {
                if (t.Date.Date >= first.Date && t.Date.Date <= second.Date)
                {
                    var transaction = Transaction.Load(t.Id);
                    Listing_Accounting.Add(transaction);
                    if (t.Type == 1)
                    {
                        Nb_Sales += 1;
                        Total += t.Total;
                        Tip += t.Tip;
                    }
                    else if (t.Type == 2)
                    {
                        Nb_Deposit += 1;
                        Total += t.Total;
                    }
                    else if (t.Type == 3)
                    {
                        Total -= t.Total;
                    }
                }
            }
        }

        #region -- ACCESSORS --
        public string FirstDate
        {
            get { return firstDate; }
            set { firstDate = value; RaisePropertyChanged("FirstDate"); }
        }

        public string SecondDate
        {
            get { return secondDate; }
            set { secondDate = value; RaisePropertyChanged("SecondDate"); }
        }

        public int Nb_Sales
        {
            get { return nb_Sales; }
            set { nb_Sales = value; RaisePropertyChanged("Nb_Sales"); }
        }

        public int Nb_Deposit
        {
            get { return nb_Deposit; }
            set { nb_Deposit = value; RaisePropertyChanged("Nb_Deposit"); }
        }

        public decimal Tip
        {
            get { return tip; }
            set { tip = value; RaisePropertyChanged("Tip"); }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; RaisePropertyChanged("Total"); }
        }

        public List<Transaction> Listing_Accounting
        {
            get
            {
                return listing_Accounting;
            }
            set { listing_Accounting = value; RaisePropertyChanged("Listing_Accounting"); }
        }
        #endregion
    }
}
