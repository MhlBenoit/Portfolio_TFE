using _DB;

namespace TFE.Model
{
    //x <summary>
    //! Donne accès aux données de la base de données
    //x </summary>
    public class _Data
    {
        //x <summary>
        //! Objet de connexion et de manipulation par SQL à la base de données de l'application qui est gérée sur un serveur MySQL
        //x </summary>
        private DB m_DB;

        //x <summary>
        //! Constructeur
        //x </summary>
        public _Data()
        {
            m_DB = new DB("royaume_du_hibou", "u_royaume_du_hibou", "vlKuOZC0LKnvLHAD");
            _Entity.DefineDB(m_DB);
        }
    }
}
