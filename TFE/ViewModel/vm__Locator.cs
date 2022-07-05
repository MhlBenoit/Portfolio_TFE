using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace TFE.ViewModel
{
    //x <summary>
    //! Classe servant à faire la liaison entre les différentes vues et les viewmodels associés via le système de Binding
    //x </summary>
    public class vm__Locator
    {
        public vm__Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<vm_Login_Connexion>();
            SimpleIoc.Default.Register<vm_Login_Password>();
            SimpleIoc.Default.Register<vm_Application>();
            SimpleIoc.Default.Register<vm_Application_Direction>();
            SimpleIoc.Default.Register<vm_Application_Customer>();
            SimpleIoc.Default.Register<vm_Application_Article>();
            SimpleIoc.Default.Register<vm_Application_Sale>();
            SimpleIoc.Default.Register<vm_Application_Purchase>();
        }

        public vm_Login_Connexion Login_Connexion
        {
            get => ServiceLocator.Current.GetInstance<vm_Login_Connexion>();
        }

        public vm_Login_Password Login_Password
        {
            get => ServiceLocator.Current.GetInstance<vm_Login_Password>();
        }

        public vm_Application Application
        {
            get => ServiceLocator.Current.GetInstance<vm_Application>();
        }

        public vm_Application_Direction Direction
        {
            get => ServiceLocator.Current.GetInstance<vm_Application_Direction>();
        }

        public vm_Application_Customer Customer
        {
            get => ServiceLocator.Current.GetInstance<vm_Application_Customer>();
        }

        public vm_Application_Article Article
        {
            get => ServiceLocator.Current.GetInstance<vm_Application_Article>();
        }

        public vm_Application_Sale Sale
        {
            get => ServiceLocator.Current.GetInstance<vm_Application_Sale>();
        }

        public vm_Application_Purchase Purchase
        {
            get => ServiceLocator.Current.GetInstance<vm_Application_Purchase>();
        }

        public static void Cleanup() { } // TODO Clear the ViewModels
    }
}