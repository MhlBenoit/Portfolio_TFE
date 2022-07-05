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
    //! Cette classe contient les propriétés auxquelles la vue v_Application_Direction peut se lier avec le Binding.
    //x </summary>
    public class vm_Application_Direction : ViewModelBase
    {
        #region -- PROPERTIES --
        private Employee employee_Selected;
        private Employee newEmployee;
        private Transaction transaction_Selected;
        private string resetPassword_Content_1;
        private string resetPassword_Content_2;
        private int resetPassword_Height;
        private string updateEmployee_Content;
        private int updateEmployee_Height;
        private string addEmployee_Content;
        private int addEmployee_Height;
        private string exportAccounting_Content;
        private bool activeReadOnly;
        private DateTime firstDate_Accounting;
        private DateTime secondDate_Accounting;

        private string error_Details_Lastname;
        private string error_Details_Firstname;
        private string error_Details_Login;
        private string error_Details_Rank;
        private string error_Details_Phone;
        private string error_Details_Mail;
        private string error_Add_Lastname;
        private string error_Add_Firstname;
        private string error_Add_Login;
        private string error_Add_Rank;
        private string error_Add_Phone;
        private string error_Add_Mail;

        private IList<Employee> listing_Employee;
        private IList<Transaction> listing_Transaction;
        private IList<Rank> cb_Rank;
        #endregion

        #region -- RELAY COMMAND DECLARATION --
        public RelayCommand RefreshBtn { get; private set; }
        public RelayCommand ShowDetailsBtn { get; private set; }
        public RelayCommand ResetPasswordBtn { get; private set; }
        public RelayCommand<Employee> ActiveEmployeeBtn { get; private set; }
        public RelayCommand UpdateEmployeeBtn { get; private set; }
        public RelayCommand CreateEmployeeBtn { get; private set; }
        public RelayCommand CreatePDF_AccountingBtn { get; private set; }
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_Application_Direction
        //x </summary>
        public vm_Application_Direction()
        {
            ShowDetailsBtn = new RelayCommand(ShowDetails);
            ResetPasswordBtn = new RelayCommand(ResetPassword);
            ActiveEmployeeBtn = new RelayCommand<Employee>((employee) => ActiveEmployee(employee));
            UpdateEmployeeBtn = new RelayCommand(UpdateEmployee);
            CreateEmployeeBtn = new RelayCommand(CreateEmployee);
            CreatePDF_AccountingBtn = new RelayCommand(CreatePDF_Accounting);

            RefreshBtn = new RelayCommand(Refresh);
            Refresh();
        }

        #region -- RELAY COMMAND METHODS --
        private void Refresh()
        {
            Listing_Employee = Employee.Enumeration.Skip(1).OrderBy(x => x.Rank_id.Name).ToArray();
            Listing_Transaction = Transaction.Enumeration.OrderByDescending(x => x.Date)
                                                         .Where(x => x.Date.Month == DateTime.Now.Month)
                                                         .Where(x => x.Date.Year == DateTime.Now.Year)
                                                         .ToArray();
            Employee_Selected = Employee.ReferenceEntity;
            Transaction_Selected = null;
            NewEmployee = new Employee(0, "", "", Rank.ReferenceEntity, true, new Person(0, "", "", "", ""));
            ResetPassword_Content_1 = Error.Clear;
            ResetPassword_Content_2 = Error.Clear;
            ResetPassword_Height = 30;
            AddEmployee_Content = Error.Clear;
            AddEmployee_Height = 30;
            UpdateEmployee_Content = Error.Clear;
            UpdateEmployee_Height = 30;
            ExportAccounting_Content = Error.Clear;

            Error_Details_Lastname = Error.Clear;
            Error_Details_Firstname = Error.Clear;
            Error_Details_Login = Error.Clear;
            Error_Details_Rank = Error.Clear;
            Error_Details_Phone = Error.Clear;
            Error_Details_Mail = Error.Clear;

            Error_Add_Lastname = Error.Clear;
            Error_Add_Firstname = Error.Clear;
            Error_Add_Login = Error.Clear;
            Error_Add_Rank = Error.Clear;
            Error_Add_Phone = Error.Clear;
            Error_Add_Mail = Error.Clear;
            Error_Add_Rank = Error.Clear;
        }

        private void ShowDetails()
        {
            if (Employee_Selected != null)
            {
                Employee e = (Employee)App.Current.Properties["UserConnected"];
                if (e.Id == Employee_Selected.Id)
                    ActiveReadOnly = false;
                else
                    ActiveReadOnly = true;
            }
        }

        private void ResetPassword()
        {
            if (employee_Selected != null)
            {
                if (Employee.Reset_Password(Employee_Selected))
                {
                    ResetPassword_Content_1 = Error.ResetPassword;
                    ResetPassword_Content_2 = Error.Clear;
                    ResetPassword_Height = 0;
                }
                else
                {
                    ResetPassword_Content_1 = Error.Clear;
                    ResetPassword_Content_2 = Error.ResetPasswordError;
                    ResetPassword_Height = 0;
                }
            }
        }

        private void ActiveEmployee(Employee employee)
        {
            Employee e = (Employee)App.Current.Properties["UserConnected"];
            if (e.Id != Employee_Selected.Id)
            {
                if (employee.Active == true)
                {
                    Employee.Desactivate(Employee.Load(employee.Id));
                    Refresh();
                }
                else if (employee.Active == false)
                {
                    Employee.Activate(Employee.Load(employee.Id));
                    Refresh();
                }
            }
        }

        private void UpdateEmployee()
        {
            Error_Details_Lastname = Verify._SimpleText(Employee_Selected.Person_id.Lastname, "Le nom", 3, 50, false, false);
            Error_Details_Firstname = Verify._SimpleText(Employee_Selected.Person_id.Firstname, "Le prénom", 3, 50, false, false);
            Error_Details_Phone = Verify._SimpleText(Employee_Selected.Person_id.Phone, "Le numéro", 9, 50, false, false);
            Error_Details_Login = Verify._SimpleText(Employee_Selected.Login, "Le login", 2, 50, false, false);
            Error_Details_Mail = Verify._Mail(Employee_Selected.Person_id.Mail, "L'email", 5, 50);

            if (Error_Details_Login == Error.Clear)
            {
                var LoginExist = Employee.Enumeration.Where(x => x.Login.ToLower() == Employee_Selected.Login.ToLower())
                                                     .Where(x=>x.Id != Employee_Selected.Id)
                                                     .Count();
                if (LoginExist != 0)
                    Error_Details_Login = Error.LoginExist;
            }
            if (Error_Details_Mail == Error.Clear)
            {
                var MailExist = Person.Enumeration.Where(x => x.Mail.ToLower() == Employee_Selected.Person_id.Mail.ToLower())
                                                  .Where(x=>x.Id != Employee_Selected.Person_id.Id)
                                                  .Count();
                if (MailExist != 0)
                    Error_Details_Mail = Error.MailExist;
            }

            if (Error_Details_Lastname == Error.Clear && Error_Details_Firstname == Error.Clear &&
                Error_Details_Login == Error.Clear && Error_Details_Rank == Error.Clear &&
                Error_Details_Phone == Error.Clear && Error_Details_Mail == Error.Clear)
            {
                var e = Employee_Selected;
                var p = Person.Load(e.Person_id.Id);
                Person.UpdateEmployee(ref p, e.Person_id.Lastname, e.Person_id.Firstname, e.Person_id.Phone, e.Person_id.Mail);
                Employee.Update(ref e, e.Login, e.Password, e.Rank_id, e.Active, e.Person_id);
                UpdateEmployee_Content = Error.Update;
                UpdateEmployee_Height = 0;
            }
        }

        private void CreateEmployee()
        {
            Error_Add_Lastname = Verify._SimpleText(NewEmployee.Person_id.Lastname, "Le nom", 3, 50, false, false);
            Error_Add_Firstname = Verify._SimpleText(NewEmployee.Person_id.Firstname, "Le prénom", 3, 50, false, false);
            Error_Add_Phone = Verify._SimpleText(NewEmployee.Person_id.Phone, "Le numéro", 9, 50, false, false);
            Error_Add_Login = Verify._SimpleText(NewEmployee.Login, "Le login", 2, 50, false, false);
            Error_Add_Mail = Verify._Mail(NewEmployee.Person_id.Mail, "L'email", 5, 50);
            Error_Add_Rank = Verify._Rank(newEmployee.Rank_id);

            if (Error_Add_Login == Error.Clear)
            {
                var LoginExist = Employee.Enumeration.Where(x => x.Login.ToLower() == NewEmployee.Login.ToLower())
                                                     .Where(x => x.Id != NewEmployee.Id)
                                                     .Count();
                if (LoginExist != 0)
                    Error_Add_Login = Error.LoginExist;
            }
            if (Error_Add_Mail == Error.Clear)
            {
                var MailExist = Person.Enumeration.Where(x => x.Mail.ToLower() == NewEmployee.Person_id.Mail.ToLower())
                                                  .Where(x => x.Id != NewEmployee.Person_id.Id)
                                                  .Count();
                if (MailExist != 0)
                    Error_Add_Mail = Error.MailExist;
            }

            if (Error_Add_Lastname == Error.Clear && Error_Add_Firstname == Error.Clear &&
                Error_Add_Login == Error.Clear && Error_Add_Rank == Error.Clear &&
                Error_Add_Phone == Error.Clear && Error_Add_Mail == Error.Clear)
            {
                var e = NewEmployee;
                var p_create = Person.CreateEmployee(e.Person_id.Lastname, e.Person_id.Firstname, e.Person_id.Phone, e.Person_id.Mail);
                if (p_create.Save())
                {
                    var DefaultPassword_BeforeCrypto = $"{p_create.Lastname.Substring(0, 1).ToUpper()}{p_create.Lastname.Substring(1, 2)}" +
                                                       $"{p_create.Firstname.Substring(0, 1).ToUpper()}{p_create.Firstname.Substring(1, 2)}";
                    var DefaultPassword_AfterCrypto = Encryption.GetSHA256(DefaultPassword_BeforeCrypto);

                    var e_create = Employee.Create(e.Login, DefaultPassword_AfterCrypto, e.Rank_id, true, p_create);
                    if (e_create.Save())
                    {
                        AddEmployee_Content = Error.Add;
                        AddEmployee_Height = 0;
                    }
                    else
                        p_create.Delete();
                }
                else
                {
                    AddEmployee_Content = Error.EmployeeExist;
                    AddEmployee_Height = 0;
                }
            }
        }

        private void CreatePDF_Accounting()
        {
            if (FirstDate_Accounting > SecondDate_Accounting)
            {
                ExportAccounting_Content = Error.WrongWayAccounting;
            }
            else
            {
                ExportAccounting_Content = Error.Clear;
                v_PDF_Accounting Accounting = new v_PDF_Accounting
                {
                    DataContext = new vm_PDF_Accounting(FirstDate_Accounting.Date, SecondDate_Accounting.Date)
                };
                Accounting.ShowDialog();
            }
        }
        #endregion

        #region -- ACCESSORS --
        public Employee Employee_Selected
        {
            get { return employee_Selected; }
            set { employee_Selected = value; RaisePropertyChanged("Employee_Selected"); }
        }

        public Transaction Transaction_Selected
        {
            get { return transaction_Selected; }
            set { transaction_Selected = value; RaisePropertyChanged("Transaction_Selected"); }
        }

        public Employee NewEmployee
        {
            get { return newEmployee; }
            set { newEmployee = value; RaisePropertyChanged("NewEmployee"); }
        }

        public string ResetPassword_Content_1
        {
            get { return resetPassword_Content_1; }
            set { resetPassword_Content_1 = value; RaisePropertyChanged("ResetPassword_Content_1"); }
        }

        public string ResetPassword_Content_2
        {
            get { return resetPassword_Content_2; }
            set { resetPassword_Content_2 = value; RaisePropertyChanged("ResetPassword_Content_2"); }
        }

        public int ResetPassword_Height
        {
            get { return resetPassword_Height; }
            set { resetPassword_Height = value; RaisePropertyChanged("ResetPassword_Height"); }
        }

        public string UpdateEmployee_Content
        {
            get { return updateEmployee_Content; }
            set { updateEmployee_Content = value; RaisePropertyChanged("UpdateEmployee_Content"); }
        }

        public int UpdateEmployee_Height
        {
            get { return updateEmployee_Height; }
            set { updateEmployee_Height = value; RaisePropertyChanged("UpdateEmployee_Height"); }
        }

        public string AddEmployee_Content
        {
            get { return addEmployee_Content; }
            set { addEmployee_Content = value; RaisePropertyChanged("AddEmployee_Content"); }
        }

        public int AddEmployee_Height
        {
            get { return addEmployee_Height; }
            set { addEmployee_Height = value; RaisePropertyChanged("AddEmployee_Height"); }
        }

        public string ExportAccounting_Content
        {
            get { return exportAccounting_Content; }
            set { exportAccounting_Content = value; RaisePropertyChanged("ExportAccounting_Content"); }
        }

        public bool ActiveReadOnly
        {
            get { return activeReadOnly; }
            set { activeReadOnly = value; RaisePropertyChanged("ActiveReadOnly"); }
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

        public string Error_Details_Login
        {
            get { return error_Details_Login; }
            set { error_Details_Login = value; RaisePropertyChanged("Error_Details_Login"); }
        }

        public string Error_Details_Rank
        {
            get { return error_Details_Rank; }
            set { error_Details_Rank = value; RaisePropertyChanged("Error_Details_Rank"); }
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

        public string Error_Add_Login
        {
            get { return error_Add_Login; }
            set { error_Add_Login = value; RaisePropertyChanged("Error_Add_Login"); }
        }

        public string Error_Add_Rank
        {
            get { return error_Add_Rank; }
            set { error_Add_Rank = value; RaisePropertyChanged("Error_Add_Rank"); }
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

        public DateTime FirstDate_Accounting
        {
            get
            {
                if (firstDate_Accounting == DateTime.MinValue)
                    return DateTime.Now;
                else
                    return firstDate_Accounting;
            }
            set { firstDate_Accounting = value; RaisePropertyChanged("FirstDate_Accounting"); }
        }

        public DateTime SecondDate_Accounting
        {
            get
            {
                if (secondDate_Accounting == DateTime.MinValue)
                    return DateTime.Now;
                else
                    return secondDate_Accounting;
            }
            set { secondDate_Accounting = value; RaisePropertyChanged("SecondDate_Accounting"); }
        }

        public IList<Employee> Listing_Employee
        {
            get
            {
                return listing_Employee;
            }
            set { listing_Employee = value; RaisePropertyChanged("Listing_Employee"); }
        }

        public IList<Transaction> Listing_Transaction
        {
            get
            {
                return listing_Transaction;
            }
            set { listing_Transaction = value; RaisePropertyChanged("Listing_Transaction"); }
        }

        public IList<Rank> Cb_Rank
        {
            get
            {
                return Rank.Enumeration.Cast<Rank>().ToList();
            }
            set { cb_Rank = value; RaisePropertyChanged("Cb_Rank"); }
        }
        #endregion
    }
}