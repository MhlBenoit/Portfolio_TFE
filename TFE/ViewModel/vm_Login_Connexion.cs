using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TFE.View;
using System;
using System.Windows.Controls;
using TFE.Model;
using TFE.Tools;

namespace TFE.ViewModel
{
    //x <summary>
    //! Cette classe contient les propriétés auxquelles la vue v_Login_Connexion peut se lier avec le Binding.
    //x </summary>
    public class vm_Login_Connexion : ViewModelBase
    {
        #region -- PROPERTIES --
        //! Permet d'initialiser l'accès à la base de données
        public static _Data Donnees { get; private set; } = new _Data();

        private string user_Login;
        private string error_Login;
        private string error_Password;
        #endregion

        #region -- RELAY COMMAND DECLARATION --
        internal Action CloseAction { get; set; }
        public RelayCommand<PasswordBox> ConnectionBtn { get; private set; }
        #endregion

        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_Login_Connexion
        //x </summary>
        public vm_Login_Connexion()
        {
            User_Login = "";
            ConnectionBtn = new RelayCommand<PasswordBox>((obj) => Verification(obj));
        }

        #region -- RELAY COMMAND METHODS --
        private void Verification(PasswordBox obj)
        {
            if (string.IsNullOrEmpty(user_Login.Trim()) && string.IsNullOrEmpty(obj.Password))
            {
                Error_Login = Error.IsNullOrEmpty;
                Error_Password = Error.IsNullOrEmpty;
            }
            else if (string.IsNullOrEmpty(user_Login.Trim()))
            {
                Error_Login = Error.IsNullOrEmpty;
                Error_Password = Error.Clear;
            }
            else if (string.IsNullOrEmpty(obj.Password.Trim()))
            {
                Error_Login = Error.Clear;
                Error_Password = Error.IsNullOrEmpty;
            }
            else if (Customer.Load(1) == null)
            {
                Error_Login = Error.DbDisconnected;
                Error_Password = Error.Clear;
            }
            else
            {
                Error_Login = Error.Clear;
                Error_Password = Error.Clear;
                Employee e = Employee.Load(user_Login);
                if (e != null && e.Login == user_Login)
                {
                    var SHA256_pwd = Encryption.GetSHA256(obj.Password);
                    if (e.Password == SHA256_pwd)
                    {
                        if (e.Active == true)
                        {
                            App.Current.Properties["UserConnected"] = e;
                            App.Current.Properties["Rank"] = e.Rank_id.Id;
                            var SHA256_FirstConnexion = e.Password;
                            Person p = Person.Load(e.Person_id.Id);
                            var Before_SHA256_FirstPassword = "Hibou";
                            var After_SHA256_FirstPassword = Encryption.GetSHA256(Before_SHA256_FirstPassword);
                            if (e.Password == After_SHA256_FirstPassword)
                            {
                                App.Current.Properties["ResetPassword"] = true;
                                v_Application v_a = new v_Application();
                                CloseAction();
                                v_a.Show();
                            }
                            else
                            {
                                App.Current.Properties["ResetPassword"] = false;
                                v_Application v_a = new v_Application();
                                CloseAction();
                                v_a.Show();
                            }
                        }
                        else
                        {
                            Error_Login = Error.UserNotActive;
                        }
                    }
                    else
                    {
                        Error_Password = Error.PasswordInvalid;
                    }
                }
                else
                {
                    Error_Login = Error.UserInvalid;
                }
            }
        }
        #endregion

        #region -- ACCESSORS --
        public string User_Login
        {
            get { return user_Login; }
            set { user_Login = value; RaisePropertyChanged("User_Login"); }
        }

        public string Error_Login
        {
            get { return error_Login; }
            set { error_Login = value; RaisePropertyChanged("Error_Login"); }
        }

        public string Error_Password
        {
            get { return error_Password; }
            set { error_Password = value; RaisePropertyChanged("Error_Password"); }
        }
        #endregion
    }
}