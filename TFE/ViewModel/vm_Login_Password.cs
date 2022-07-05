using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using TFE.Model;
using TFE.Tools;

namespace TFE.ViewModel
{
    //x <summary>
    //! Cette classe contient les propriétés auxquelles la vue v_Login_Password peut se lier avec le Binding.
    //x </summary>
    public class vm_Login_Password : ViewModelBase
    {
        #region -- PROPERTIES --
        private string password_New;
        private string error_Pwd_New;
        private string error_Pwd_Check;
        private string beforeAnyChange;
        private string validate_Content;
        private string validate_Btn;
        #endregion

        #region -- RELAY COMMAND DECLARATION --
        public RelayCommand<PasswordBox> BtnChangePassword { get; private set; }
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_Login_Password
        //x </summary>
        public vm_Login_Password()
        {
            Password_New = "";
            BeforeAnyChange = "Veuillez modifier votre mot de passe";
            Validate_Btn = "Valider";
            BtnChangePassword = new RelayCommand<PasswordBox>((obj) => CheckAndChangePassword(obj));
        }

        #region -- RELAY COMMAND METHODS --
        private void CheckAndChangePassword(PasswordBox obj)
        {
            if (string.IsNullOrEmpty(password_New.Trim()) && string.IsNullOrEmpty(obj.Password))
            {
                Error_Pwd_New = Error.IsNullOrEmpty;
                Error_Pwd_Check = Error.IsNullOrEmpty;
            }
            else if (string.IsNullOrEmpty(password_New.Trim()))
            {
                Error_Pwd_New = Error.IsNullOrEmpty;
                Error_Pwd_Check = Error.Clear;
            }
            else if (string.IsNullOrEmpty(obj.Password.Trim()))
            {
                Error_Pwd_New = Error.Clear;
                Error_Pwd_Check = Error.IsNullOrEmpty;
            }
            else
            {
                Error_Pwd_New = Error.Clear;
                Error_Pwd_Check = Error.Clear;
                Employee e = (Employee)App.Current.Properties["UserConnected"];
                var SHA256_pwd_1 = Encryption.GetSHA256(password_New);
                if (e.Password != SHA256_pwd_1)
                {
                    var SHA256_pwd_2 = Encryption.GetSHA256(obj.Password);
                    if (SHA256_pwd_1 == SHA256_pwd_2)
                    {
                        e.Password = SHA256_pwd_1;
                        if (e.Save())
                        {
                            BeforeAnyChange = "";
                            Validate_Content = Error.PasswordChanged;
                            Validate_Btn = "Entrer dans l'application";
                        }
                    }
                    else
                    {
                        Error_Pwd_Check = Error.PasswordNeedToBeTheSame;
                        Error_Pwd_New = Error.Clear;
                    }
                }
                else
                {
                    Error_Pwd_New = Error.PasswordCantBeTheSame;
                    Error_Pwd_Check = Error.Clear;
                }
            }
        }
        #endregion

        #region -- ACCESSORS --
        public string Password_New
        {
            get { return password_New; }
            set { password_New = value; RaisePropertyChanged("Password_New"); }
        }

        public string Error_Pwd_New
        {
            get { return error_Pwd_New; }
            set { error_Pwd_New = value; RaisePropertyChanged("Error_Pwd_New"); }
        }

        public string Error_Pwd_Check
        {
            get { return error_Pwd_Check; }
            set { error_Pwd_Check = value; RaisePropertyChanged("Error_Pwd_Check"); }
        }

        public string BeforeAnyChange
        {
            get { return beforeAnyChange; }
            set { beforeAnyChange = value; RaisePropertyChanged("BeforeAnyChange"); }
        }

        public string Validate_Content
        {
            get { return validate_Content; }
            set { validate_Content = value; RaisePropertyChanged("Validate_Content"); }
        }

        public string Validate_Btn
        {
            get { return validate_Btn; }
            set { validate_Btn = value; RaisePropertyChanged("Validate_Btn"); }
        }
        #endregion
    }
}