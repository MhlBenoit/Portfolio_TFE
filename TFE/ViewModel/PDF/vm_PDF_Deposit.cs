using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;
using TFE.Model;

namespace TFE.ViewModel.PDF
{
    //x <summary>
    //! Classe regroupant le traitement des informations pour extraire un bon de dépôt
    //x </summary>
    public class vm_PDF_Deposit : ViewModelBase
    {
        #region -- PROPERTIES --
        private Transaction t_Deposit;
        private decimal totalDeposit;
        private string fullname;
        private int loyalty_Total;

        private IList<Purchase> listing_Bucket;
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_PDF_Deposit
        //x </summary>
        public vm_PDF_Deposit(Transaction transaction)
        {
            if (transaction.Customer_id.Person_id.Lastname != "" && transaction.Customer_id.Person_id.Firstname != "")
                Fullname = $"{transaction.Customer_id.Person_id.Lastname.Trim().ToUpper()} {transaction.Customer_id.Person_id.Firstname.Trim()} ";
            else
                Fullname = transaction.Customer_id.Person_id.Mail;

            T_Deposit = transaction;
            Listing_Bucket = Purchase.Enumeration.Where(x => x.Transaction_id.Id == transaction.Id).ToList();


            foreach (var i in Listing_Bucket)
                TotalDeposit += (decimal)i.Total;

        }

        #region -- ACCESSORS --
        public Transaction T_Deposit
        {
            get { return t_Deposit; }
            set { t_Deposit = value; RaisePropertyChanged("T_Deposit"); }
        }

        public decimal TotalDeposit
        {
            get { return totalDeposit; }
            set { totalDeposit = value; RaisePropertyChanged("TotalDeposit"); }
        }

        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; RaisePropertyChanged("Fullname"); }
        }

        public int Loyalty_Total
        {
            get { return loyalty_Total; }
            set { loyalty_Total = value; RaisePropertyChanged("Loyalty_Total"); }
        }

        public IList<Purchase> Listing_Bucket
        {
            get { return listing_Bucket; }
            set { listing_Bucket = value; RaisePropertyChanged("Listing_Bucket"); }
        }
        #endregion
    }
}
