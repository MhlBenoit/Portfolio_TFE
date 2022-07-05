using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using TFE.Model;
using TFE.Tools;

namespace TFE.ViewModel
{
    //x <summary>
    //! Cette classe contient les propriétés auxquelles la vue v_Application_Customer peut se lier avec le Binding.
    //x </summary>
    public class vm_Application_Customer : ViewModelBase
    {
        #region -- PROPERTIES --
        private Customer customer_Selected;
        private Customer newCustomer;
        private string searchCustomer;
        private string searchPhone;
        private string searchMail;
        private string updateCustomer_Content;
        private int updateCustomer_Height;
        private string addCustomer_Content;
        private int addCustomer_Height;

        private string error_Details_Lastname;
        private string error_Details_Firstname;
        private string error_Details_Phone;
        private string error_Details_Mail;
        private string error_Details_BornDate;
        private string error_Details_Address;
        private string error_Details_City;
        private string error_Details_PostalCode;
        private string error_Details_AddressComplete;

        private string error_Add_Lastname;
        private string error_Add_Firstname;
        private string error_Add_Phone;
        private string error_Add_Mail;
        private string error_Add_BornDate;
        private string error_Add_Address;
        private string error_Add_City;
        private string error_Add_PostalCode;
        private string error_Add_AddressComplete;

        private IList<Customer> listing_Customer;
        #endregion

        #region -- RELAY COMMAND DECLARATION --
        public RelayCommand RefreshBtn { get; private set; }
        public RelayCommand FilterCustomerBtn { get; private set; }
        public RelayCommand FilterPhoneBtn { get; private set; }
        public RelayCommand FilterMailBtn { get; private set; }
        public RelayCommand UpdateCustomerBtn { get; private set; }
        public RelayCommand CreateCustomerBtn { get; private set; }
        public RelayCommand<Customer> DeleteCustomerBtn { get; private set; }
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_Application_Customer
        //x </summary>
        public vm_Application_Customer()
        {
            FilterCustomerBtn = new RelayCommand(FilterCustomer);
            FilterPhoneBtn = new RelayCommand(FilterPhone);
            FilterMailBtn = new RelayCommand(FilterMail);
            UpdateCustomerBtn = new RelayCommand(UpdateCustomer);
            CreateCustomerBtn = new RelayCommand(CreateCustomer);
            DeleteCustomerBtn = new RelayCommand<Customer>((customer) => DeleteCustomer(customer));

            RefreshBtn = new RelayCommand(Refresh);
            Refresh();
        }

        #region -- RELAY COMMAND METHODS --
        private void Refresh()
        {
            Listing_Customer = Customer.Enumeration.Skip(1).ToArray();
            Customer_Selected = null;
            NewCustomer = new Customer(0, null, new Address(0, "", new City(0, "", "")), 0, new Person(0, "", "", "", ""));
            UpdateCustomer_Content = Error.Clear;
            UpdateCustomer_Height = 30;
            AddCustomer_Content = Error.Clear;
            AddCustomer_Height = 30;

            Error_Details_Lastname = Error.Clear;
            Error_Details_Firstname = Error.Clear;
            Error_Details_Phone = Error.Clear;
            Error_Details_Mail = Error.Clear;
            Error_Details_BornDate = Error.Clear;
            Error_Details_Address = Error.Clear;
            Error_Details_PostalCode = Error.Clear;
            Error_Details_City = Error.Clear;
            Error_Details_AddressComplete = Error.Clear;

            Error_Add_Lastname = Error.Clear;
            Error_Add_Firstname = Error.Clear;
            Error_Add_Phone = Error.Clear;
            Error_Add_Mail = Error.Clear;
            Error_Add_BornDate = Error.Clear;
            Error_Add_Address = Error.Clear;
            Error_Add_PostalCode = Error.Clear;
            Error_Add_City = Error.Clear;
            Error_Add_AddressComplete = Error.Clear;

        }

        private void FilterCustomer()
        {
            if (SearchCustomer.Trim() != "")
            {
                if (SearchCustomer != "Recherche par nom ou prénom")
                {
                    SearchPhone = "Recherche par n° de téléphone";
                    SearchMail = "Recherche par adresse email";

                    IList<Customer> lc = Customer.Enumeration.Where(x => x.Person_id.Lastname.ToLower().Contains(SearchCustomer.Trim().ToLower())
                                                                      || x.Person_id.Firstname.ToLower().Contains(SearchCustomer.Trim().ToLower()))
                                                             .Where(x => x.Person_id.Id != 1)
                                                             .ToArray();
                    if (lc.Any())
                        Listing_Customer = lc;
                    else
                        Listing_Customer = null;
                }
                else
                    Listing_Customer = Customer.Enumeration.Skip(1).ToArray();
            }
            else
                Listing_Customer = Customer.Enumeration.Skip(1).ToArray();
        }

        private void FilterPhone()
        {
            if (SearchPhone.Trim() != "")
            {
                if (SearchPhone != "Recherche par n° de téléphone")
                {
                    SearchCustomer = "Recherche par nom ou prénom";
                    SearchMail = "Recherche par adresse email";

                    IList<Customer> lc = Customer.Enumeration.Where(x => x.Person_id.Phone.Replace("/", "").Replace(".", "").Replace(" ", "")
                                                             .Contains(SearchPhone.Trim().Replace("/", "").Replace(".", "").Replace(" ", "")))
                                                             .Where(x => x.Person_id.Id != 1)
                                                             .ToArray();
                    if (lc.Any())
                        Listing_Customer = lc;
                    else
                        Listing_Customer = null;
                }
                else
                    Listing_Customer = Customer.Enumeration.Skip(1).ToArray();
            }
            else
                Listing_Customer = Customer.Enumeration.Skip(1).ToArray();
        }

        private void FilterMail()
        {
            if (SearchMail.Trim() != "")
            {
                if (SearchMail != "Recherche par adresse email")
                {
                    SearchCustomer = "Recherche par nom ou prénom";
                    SearchPhone = "Recherche par n° de téléphone";

                    IList<Customer> lc = Customer.Enumeration.Where(x => x.Person_id.Mail.ToLower().Contains(SearchMail.Trim().ToLower()))
                                                             .Where(x => x.Person_id.Id != 1)
                                                             .ToArray();
                    if (lc.Any())
                        Listing_Customer = lc;
                    else
                        Listing_Customer = null;
                }
                else
                    Listing_Customer = Customer.Enumeration.Skip(1).ToArray();
            }
            else
                Listing_Customer = Customer.Enumeration.Skip(1).ToArray();
        }

        private void UpdateCustomer()
        {
            Error_Details_Lastname = Verify._SimpleText(Customer_Selected.Person_id.Lastname, "Le nom", 0, 50, true, false);
            Error_Details_Firstname = Verify._SimpleText(Customer_Selected.Person_id.Firstname, "Le prénom", 0, 50, true, false);
            Error_Details_Phone = Verify._SimpleText(Customer_Selected.Person_id.Phone, "Le numéro", 0, 50, true, false);
            Error_Details_Mail = Verify._Mail(Customer_Selected.Person_id.Mail, "L'email", 5, 50);
            Error_Details_BornDate = Verify._Borndate(Customer_Selected.Borndate, "La date");

            if (Error_Details_Mail == Error.Clear)
            {
                var MailExist = Person.Enumeration.Where(x => x.Mail.ToLower() == Customer_Selected.Person_id.Mail.ToLower())
                                                  .Where(x => x.Id != Customer_Selected.Person_id.Id)
                                                  .Count();
                if (MailExist != 0)
                    Error_Details_Mail = Error.MailExist;
            }

            if (Customer_Selected.Address_id.Name.Trim() != "" ||
                Customer_Selected.Address_id.City_id.PostalCode.Trim() != "" ||
                Customer_Selected.Address_id.City_id.Name.Trim() != "")
            {
                Error_Details_Address = Verify._SimpleText(Customer_Selected.Address_id.Name, "L'adresse", 0, 100, true, true);
                if (Customer_Selected.Address_id.Name.Trim() == "")
                    Error_Details_Address = Error.CompleteAddress;

                Error_Details_PostalCode = Verify._Regex(Customer_Selected.Address_id.City_id.PostalCode, "Le code postal", 4, 5, "postalcode");
                if (Customer_Selected.Address_id.City_id.PostalCode.Trim() == "")
                    Error_Details_PostalCode = Error.CompleteAddress;

                Error_Details_City = Verify._SimpleText(Customer_Selected.Address_id.City_id.Name, "La ville", 3, 50, true, true);
                if (Customer_Selected.Address_id.City_id.Name.Trim() == "")
                    Error_Details_City = Error.CompleteAddress;
                Error_Details_AddressComplete = Error.Clear;
            }
            else
            {
                Error_Details_Address = Error.Clear;
                Error_Details_PostalCode = Error.Clear;
                Error_Details_City = Error.Clear;
            }

            if (Error_Details_Lastname == Error.Clear && Error_Details_Firstname == Error.Clear &&
                Error_Details_BornDate == Error.Clear && Error_Details_Address == Error.Clear &&
                Error_Details_PostalCode == Error.Clear && Error_Details_City == Error.Clear &&
                Error_Details_Phone == Error.Clear && Error_Details_Mail == Error.Clear)
            {
                var old_c = Customer.Load(Customer_Selected.Id);
                var old_a = Address.Load(Customer_Selected.Address_id.Id);
                var new_c = Customer_Selected;

                if (old_a != null)
                {
                    if (old_c.Address_id.Name == new_c.Address_id.Name && old_c.Address_id.City_id.PostalCode != new_c.Address_id.City_id.PostalCode ||
                        old_c.Address_id.Name == new_c.Address_id.Name && old_c.Address_id.City_id.Name != new_c.Address_id.City_id.Name)
                    {
                        Error_Details_AddressComplete = Error.CityOrPostalcodeChanged;
                        return;
                    }
                    else if (old_c.Address_id.Name != new_c.Address_id.Name)
                    {
                        City city_exist = City.Enumeration.Where(x => x.Name.ToLower().Contains(new_c.Address_id.City_id.Name.Trim().ToLower()))
                                                    .FirstOrDefault();
                        if (city_exist != null)
                        {
                            Address.Update(ref old_a, new_c.Address_id.Name, city_exist);
                        }
                        else
                        {
                            City city_created = City.Create(new_c.Address_id.City_id.PostalCode, new_c.Address_id.City_id.Name);
                            city_created.Save();

                            Address.Update(ref old_a, new_c.Address_id.Name, city_created);
                        }
                    }

                    var new_p = Person.Load(new_c.Person_id.Id);
                    Person.UpdateCustomer(ref new_p, new_c.Person_id.Lastname, new_c.Person_id.Firstname, new_c.Person_id.Phone, new_c.Person_id.Mail);

                    Customer.Update(ref new_c, new_c.Borndate, old_a, new_c.Loyalty_points, new_p);
                    UpdateCustomer_Content = Error.Update;
                    UpdateCustomer_Height = 0;
                }
            }
        }

        private void CreateCustomer()
        {
            Error_Add_Lastname = Verify._SimpleText(NewCustomer.Person_id.Lastname, "Le nom", 0, 50, true, false);
            Error_Add_Firstname = Verify._SimpleText(NewCustomer.Person_id.Firstname, "Le prénom", 0, 50, true, false);
            Error_Add_Phone = Verify._SimpleText(NewCustomer.Person_id.Phone, "Le numéro", 0, 50, true, false);
            Error_Add_Mail = Verify._Mail(NewCustomer.Person_id.Mail, "L'email", 5, 50);
            Error_Add_BornDate = Verify._Borndate(NewCustomer.Borndate, "La date");

            if (Error_Add_Mail == Error.Clear)
            {
                var MailExist = Person.Enumeration.Where(x => x.Mail.ToLower() == NewCustomer.Person_id.Mail.ToLower())
                                                  .Where(x => x.Id != NewCustomer.Person_id.Id)
                                                  .Count();
                if (MailExist != 0)
                    Error_Add_Mail = Error.MailExist;
            }

            if (NewCustomer.Address_id.Name.Trim() != "" ||
                NewCustomer.Address_id.City_id.PostalCode.Trim() != "" ||
                NewCustomer.Address_id.City_id.Name.Trim() != "")
            {
                Error_Add_Address = Verify._SimpleText(NewCustomer.Address_id.Name, "L'adresse", 0, 100, true, true);
                if (NewCustomer.Address_id.Name.Trim() == "")
                    Error_Add_Address = Error.CompleteAddress;

                Error_Add_PostalCode = Verify._Regex(NewCustomer.Address_id.City_id.PostalCode, "Le code postal", 4, 5, "postalcode");
                if (NewCustomer.Address_id.City_id.PostalCode.Trim() == "")
                    Error_Add_PostalCode = Error.CompleteAddress;

                Error_Add_City = Verify._SimpleText(NewCustomer.Address_id.City_id.Name, "La ville", 3, 50, true, true);
                if (NewCustomer.Address_id.City_id.Name.Trim() == "")
                    Error_Add_City = Error.CompleteAddress;
            }
            else
            {
                Error_Add_Address = Error.Clear;
                Error_Add_PostalCode = Error.Clear;
                Error_Add_City = Error.Clear;
            }

            if (Error_Add_Lastname == Error.Clear && Error_Add_Firstname == Error.Clear &&
                Error_Add_BornDate == Error.Clear && Error_Add_Address == Error.Clear &&
                Error_Add_PostalCode == Error.Clear && Error_Add_City == Error.Clear &&
                Error_Add_Phone == Error.Clear && Error_Add_Mail == Error.Clear)
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
                    Customer c_create = Customer.Create(NewCustomer.Borndate, a_create, NewCustomer.Loyalty_points, p_create);
                    if (c_create.Save())
                    {
                        AddCustomer_Content = Error.Add;
                        AddCustomer_Height = 0;
                    }
                    else
                    {
                        p_create.Delete();
                        a_create.Delete();
                        AddCustomer_Content = Error.CustomerExist;
                        AddCustomer_Height = 0;
                    }
                }
                else
                {
                    a_create.Delete();
                    AddCustomer_Content = Error.CustomerExist;
                    AddCustomer_Height = 0;
                }
            }
        }

        private void DeleteCustomer(Customer customer)
        {
            if (customer.Id != 0)
            {
                IList<Transaction> lt = Transaction.Enumeration.Where(x => x.Customer_id.Id == customer.Id)
                                                               .ToArray();
                if (lt.Any())
                {
                    foreach (Transaction t in lt)
                    {
                        t.Customer_id = Customer.Load(1);
                        t.Save();
                    }
                }
                customer.Delete();
                customer.Person_id.Delete();
                customer.Address_id.Delete();
                Refresh();
            }
        }
        #endregion

        #region -- ACCESSORS --
        public Customer Customer_Selected
        {
            get { return customer_Selected; }
            set { customer_Selected = value; RaisePropertyChanged("Customer_Selected"); }
        }

        public Customer NewCustomer
        {
            get { return newCustomer; }
            set { newCustomer = value; RaisePropertyChanged("NewCustomer"); }
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

        public string UpdateCustomer_Content
        {
            get { return updateCustomer_Content; }
            set { updateCustomer_Content = value; RaisePropertyChanged("UpdateCustomer_Content"); }
        }

        public int UpdateCustomer_Height
        {
            get { return updateCustomer_Height; }
            set { updateCustomer_Height = value; RaisePropertyChanged("UpdateCustomer_Height"); }
        }

        public string AddCustomer_Content
        {
            get { return addCustomer_Content; }
            set { addCustomer_Content = value; RaisePropertyChanged("AddCustomer_Content"); }
        }

        public int AddCustomer_Height
        {
            get { return addCustomer_Height; }
            set { addCustomer_Height = value; RaisePropertyChanged("AddCustomer_Height"); }
        }

        public string Error_Details_Lastname
        {
            get { return error_Details_Lastname; }
            set { error_Details_Lastname = value; RaisePropertyChanged("Error_Details_Lastname"); }
        }

        public string Error_Details_Firstname
        {
            get { return error_Details_Firstname; }
            set { error_Details_Firstname = value; RaisePropertyChanged("Error_Details_Firstname"); }
        }

        public string Error_Details_Phone
        {
            get { return error_Details_Phone; }
            set { error_Details_Phone = value; RaisePropertyChanged("Error_Details_Phone"); }
        }

        public string Error_Details_Mail
        {
            get { return error_Details_Mail; }
            set { error_Details_Mail = value; RaisePropertyChanged("Error_Details_Mail"); }
        }

        public string Error_Details_BornDate
        {
            get { return error_Details_BornDate; }
            set { error_Details_BornDate = value; RaisePropertyChanged("Error_Details_BornDate"); }
        }

        public string Error_Details_Address
        {
            get { return error_Details_Address; }
            set { error_Details_Address = value; RaisePropertyChanged("Error_Details_Address"); }
        }

        public string Error_Details_City
        {
            get { return error_Details_City; }
            set { error_Details_City = value; RaisePropertyChanged("Error_Details_City"); }
        }

        public string Error_Details_PostalCode
        {
            get { return error_Details_PostalCode; }
            set { error_Details_PostalCode = value; RaisePropertyChanged("Error_Details_PostalCode"); }
        }

        public string Error_Details_AddressComplete
        {
            get { return error_Details_AddressComplete; }
            set { error_Details_AddressComplete = value; RaisePropertyChanged("Error_Details_AddressComplete"); }
        }

        public string Error_Add_Lastname
        {
            get { return error_Add_Lastname; }
            set { error_Add_Lastname = value; RaisePropertyChanged("Error_Add_Lastname"); }
        }

        public string Error_Add_Firstname
        {
            get { return error_Add_Firstname; }
            set { error_Add_Firstname = value; RaisePropertyChanged("Error_Add_Firstname"); }
        }

        public string Error_Add_Phone
        {
            get { return error_Add_Phone; }
            set { error_Add_Phone = value; RaisePropertyChanged("Error_Add_Phone"); }
        }

        public string Error_Add_Mail
        {
            get { return error_Add_Mail; }
            set { error_Add_Mail = value; RaisePropertyChanged("Error_Add_Mail"); }
        }

        public string Error_Add_BornDate
        {
            get { return error_Add_BornDate; }
            set { error_Add_BornDate = value; RaisePropertyChanged("Error_Add_BornDate"); }
        }

        public string Error_Add_Address
        {
            get { return error_Add_Address; }
            set { error_Add_Address = value; RaisePropertyChanged("Error_Add_Address"); }
        }

        public string Error_Add_City
        {
            get { return error_Add_City; }
            set { error_Add_City = value; RaisePropertyChanged("Error_Add_City"); }
        }

        public string Error_Add_PostalCode
        {
            get { return error_Add_PostalCode; }
            set { error_Add_PostalCode = value; RaisePropertyChanged("Error_Add_PostalCode"); }
        }

        public string Error_Add_AddressComplete
        {
            get { return error_Add_AddressComplete; }
            set { error_Add_AddressComplete = value; RaisePropertyChanged("Error_Add_AddressComplete"); }
        }

        public IList<Customer> Listing_Customer
        {
            get
            {
                return listing_Customer;
            }
            set { listing_Customer = value; RaisePropertyChanged("Listing_Customer"); }
        }
        #endregion
    }
}