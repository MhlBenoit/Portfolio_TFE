using GalaSoft.MvvmLight;
using TFE.View;

namespace TFE.ViewModel
{
    //x <summary>
    //! Classe servant d'interface principale pour l'application (hors connextion)
    //! Les diff�rents onglet suivant tel que :
    //!         - Direction
    //!         - Client�le
    //!         - Article
    //!         - Caisse
    //!         - D�p�t
    //! viennent s'injecter dans cette interface dans l'id�e de ne pas avoir une multitude de page ouverte � g�rer
    //x </summary>
    public class vm_Application : ViewModelBase
    {
        //x <summary>
        //! Initialise une nouvelle instance de la classe vm_Application
        //x </summary>
        public vm_Application()
        {
            if ((bool)App.Current.Properties["ResetPassword"] == true)
            {
                v_Login_Password v_lp = new v_Login_Password();
                v_lp.DataContext = new vm_Login_Password();
                v_lp.ShowDialog();
            }
        }
    }
}