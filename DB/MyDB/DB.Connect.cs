using System;
using MySql.Data.MySqlClient;
using Debug = System.Diagnostics.Debug;

namespace _DB
{
    //x <summary>
    //! Fournit une connexion à un serveur MySQL afin d'y réaliser différentes opérations par le biais de requête SQL
    //x </summary>
    public partial class DB
    {
        //x <summary>
        //! Retient le nombre de connexions actuellement ouvertes
        //x </summary>
        public static int ConnectCount { get; private set; } = 0;

        //x <summary>
        //! Contient l'adresse d'hébergement du serveur MySQL visé
        //x </summary>
        private string m_ServerAddress;

        //x <summary>
        //! Contient le nom d'utilisateur se connectant au serveur MySQL visé
        //x </summary>
        private string m_Username;

        //x <summary>
        //! Contient le mot de passe de l'utilisateur se connectant au serveur MySQL visé
        //x </summary>
        private string m_Password;

        //x <summary>
        //! Contient le nom de la base de données MySQL à manipuler par défaut sur le serveur visé
        //x </summary>
        private string m_DatabaseName;

        //x <summary>
        //! Objet de connexion au serveur MySQL visé
        //x </summary>
        private MySqlConnection m_Connection;

        //x <summary>
        //! Indique si les paramètres de connexion semblent définis
        //x </summary>
        public bool IsDefined
        {
            get
            {
                return !string.IsNullOrEmpty(m_ServerAddress)
                    && !string.IsNullOrEmpty(m_Username)
                    && (m_Password != null)
                    && !string.IsNullOrEmpty(m_DatabaseName);
            }
        }

        //x <summary>
        //! Adresse de la machine hébergeant le serveur MySQL
        //! <para>Paramètre obligatoire ne pouvant être ni nul, ni vide, ni constitué que d'espacements</para>
        //x </summary>
        public string ServerAddress
        {
            get
            {
                return string.IsNullOrEmpty(m_ServerAddress) ? string.Empty : m_ServerAddress;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    value = value.Trim();
                    if (!string.Equals(value, m_ServerAddress))
                    {
                        Disconnect();
                        m_ServerAddress = value;
                    }
                }
            }
        }

        //x <summary>
        //! Nom de l'utilisateur MySQL utilisé pour profiter des services du serveur MySQL contacté
        //! <para>Paramètre obligatoire ne pouvant être ni nul, ni vide, ni constitué que d'espacements</para>
        //x </summary>
        public string Username
        {
            get
            {
                return string.IsNullOrEmpty(m_Username) ? string.Empty : m_Username;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    value = value.Trim();
                    if (!string.Equals(value, m_Username))
                    {
                        Disconnect();
                        m_Username = value;
                    }
                }
            }
        }

        //x <summary>
        //! Mot de passe de l'utilisateur MySQL utilisé pour profiter des services du serveur MySQL contacté
        //! <para>Paramètre optionnel ; néanmoins, pour des raisons évidentes de sécurité, il est très fortement conseillé d'en utiliser un !</para>
        //x </summary>
        public string Password
        {
            get
            {
                return string.IsNullOrEmpty(m_Password) ? string.Empty : m_Password;
            }
            set
            {
                if ((value != null) && string.IsNullOrEmpty(value)) value = null;
                if (!string.Equals(value, m_Password))
                {
                    Disconnect();
                    m_Password = value;
                }
            }
        }

        //x <summary>
        //! Nom de la base de données manipulée par défaut par cette connexion à un serveur MySQL
        //! <para>Paramètre obligatoire ne pouvant être ni nul, ni vide, ni constitué que d'espacements</para>
        //! <para>Néanmoins, il est possible une fois la connexion établie d'utiliser une requête USE pour changer de base de données manipulée par défaut</para>
        //x </summary>
        public string DatabaseName
        {
            get
            {
                return string.IsNullOrEmpty(m_DatabaseName) ? string.Empty : m_DatabaseName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    value = value.Trim();
                    if (!string.Equals(value, m_DatabaseName))
                    {
                        Disconnect();
                        m_DatabaseName = value;
                    }
                }
            }
        }

        //x <summary>
        //! Constructeur par défaut
        //x </summary>
        public DB()
        {
            m_ServerAddress = null;
            m_Username = null;
            m_Password = null;
            m_DatabaseName = null;
            m_Connection = null;
        }

        //x <summary>
        //! Constructeur par copie
        //! </summary>
        //! <param name="Source">Objet de connexion source de cette copie</param>
        //x </summary>
        public DB(DB Source)
            : this()
        {
            if (Source != null)
            {
                m_ServerAddress = Source.m_ServerAddress;
                m_Username = Source.m_Username;
                m_Password = Source.m_Password;
                m_DatabaseName = Source.m_DatabaseName;
            }
        }

        //x <summary>
        //! Constructeur spécifique à une connexion à un serveur "local"
        //! <para>Il s'agit d'un serveur MySQL hébergé sur la machine qui exécute le programme client utilisant cet objet de connexion ; son adresse est définie comme "localhost"</para>
        //x </summary>
        //! <param name="DatabaseName">Nom de la base de données</param>
        //! <param name="Username">Nom de l'utilisateur</param>
        //! <param name="Password">Mot de passe</param>
        public DB(string DatabaseName, string Username, string Password = null)
            : this(DatabaseName, Username, Password, "localhost")
        {
        }

        //x <summary>
        //! Constructeur spécifique à une connexion à un serveur "distant"
        //! <para>Il s'agit d'un serveur MySQL hébergé sur une machine dont l'adresse est spécifiée en paramètre</para>
        //x </summary>
        //! <param name="DatabaseName">Nom de la base de données</param>
        //! <param name="Username">Nom de l'utilisateur</param>
        //! <param name="Password">Mot de passe</param>
        //! <param name="ServerAddress">Adresse d'hébergement du serveur</param>
        public DB(string DatabaseName, string Username, string Password, string ServerAddress)
            : this()
        {
            this.DatabaseName = DatabaseName;
            this.Username = Username;
            this.Password = Password;
            this.ServerAddress = ServerAddress;
        }

        //x <summary>
        //! Permet de se déconnecter du serveur (si nécessaire)
        //x </summary>
        //! <returns>Vrai si une déconnexion a du être entreprise, sinon faux</returns>
        public bool Disconnect()
        {
            if (m_Connection == null) return false;
            m_Connection.Close();
            m_Connection = null;
            ConnectCount--;
            return true;
        }

        //x <summary>
        //! Permet de se connecter au serveur (si possible et si nécessaire)
        //x </summary>
        //! <returns>Vrai si une connexion a du être entreprise et a été réalisée avec succès, sinon faux</returns>
        public bool Connect()
        {
            if (m_Connection != null)
            {
                try
                {
                    if (m_Connection.Ping()) return true;
                }
                catch
                {
                    m_Connection = null;
                    ConnectCount--;
                }
            }
            if (!IsDefined)
            {
                Debug.WriteLine($"\nErreur de connexion au serveur MySQL par manque de paramètres correctement définis !\n* adresse du serveur : {ServerAddress}\n* nom d'utilisateur : {Username}\n* mot de passe : {Password}\n* nom de la base de données : {DatabaseName}\n");
                return false;
            }
            try
            {
                m_Connection = new MySqlConnection();
                m_Connection.ConnectionString = $"server={m_ServerAddress};uid={m_Username};pwd={m_Password};database={m_DatabaseName}";
                m_Connection.Open();
                MySqlCommand Command = new MySqlCommand("SET NAMES 'latin1';", m_Connection);
                Command.ExecuteNonQuery();
                Command = new MySqlCommand("SET CHARACTER_SET_RESULTS = 'latin1';", m_Connection);
                Command.ExecuteNonQuery();
                ConnectCount++;
                return true;
            }
            catch (Exception Error)
            {
                Debug.WriteLine($"\nErreur de connexion au serveur MySQL :\n\n* adresse du serveur : {ServerAddress}\n* nom d'utilisateur : {Username}\n* mot de passe : {Password}\n* nom de la base de données : {DatabaseName}\n{Error.Message}\n");
                m_Connection = null;
                return false;
            }
        }
    }
}
