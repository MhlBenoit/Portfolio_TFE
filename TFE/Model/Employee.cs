using System.Collections.Generic;
using TFE.Tools;

namespace TFE.Model
{
    //x <summary>
    //! Décrit un employé
    //x </summary>
    public class Employee : Entity<Employee>
    {
        //x <summary>
        //! Informations d'un employé
        //x </summary>
        public string Login { get; set; }
        public string Password { get; set; }
        public Rank Rank_id { get; set; }
        public bool Active { get; set; }
        public Person Person_id { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_employee">Identifiant d'un employé</param>
        //! <param name="login">Nom d'utilisateur d'un employé</param>
        //! <param name="password">Mot de passe d'un employé</param>
        //! <param name="rank_id">Identifiant d'un rang</param>
        //! <param name="active">Détermine si un employé est actif ou non</param>
        //! <param name="person_id">Identifiant d'une personne</param>
        public Employee(uint id_employee, string login, string password, Rank rank_id, bool active, Person person_id)
            : base(id_employee)
        {
            DefinirReferenceEntity(this);
            Login = login;
            Password = password;
            Rank_id = rank_id;
            Active = active;
            Person_id = person_id;
        }

        //x <summary>
        //! Représente sous forme de texte cette entité
        //x </summary>
        //! <returns>Texte représentatif de cet objet</returns>
        public override string ToString()
        {
            return $"{Id}";
        }

        #region -- Validité champ pour la DB --
        //x <summary>
        //! Permet de tester la validité du champ login pour un employé
        //x </summary>
        //! <param name="login">Nom d'utilisateur à tester</param>
        //! <param name="loginFinal">Nom d'utilisateur finalement retenu si il est valide</param>
        //! <para>Ce nom d'utilisateur a subi une transformation de type Trim</para></param>
        //! <returns>Vrai si ce nom d'utilisateur est valide, sinon faux</returns>
        public static bool TesterValidite_Login(string login, out string loginFinal)
        {
            loginFinal = null;
            if (login == null) return false;
            login = login.Trim();
            if ((login.Length < 2) || (login.Length > 50)) return false;
            loginFinal = login;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ mot de passe pour un employé
        //x </summary>
        //! <param name="password">Mot de passe à tester</param>
        //! <param name="passwordFinal">Mot de passe finalement retenu si il est valide</param>
        //! <para>Ce mot de passe a subi une transformation de type Trim</para></param>
        //! <returns>Vrai si ce mot de passeest valide, sinon faux</returns>
        public static bool TesterValidite_Password(string password, out string passwordFinal)
        {
            passwordFinal = null;
            if (password == null) return false;
            password = password.Trim();
            if ((password.Length < 2) || (password.Length > 64)) return false;
            passwordFinal = password;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ rang pour un employé
        //x </summary>
        //! <param name="rank_id">Rang à tester</param>
        //! <param name="rank_idFinal">Rang finalement retenu si il est valide</param>
        //! <returns>Vrai si ce rang est valide, sinon faux</returns>
        public static bool TesterValidite_Rank_id(Rank rank_id, out Rank rank_idFinal)
        {
            rank_idFinal = null;
            if (rank_id != null)
            {
                rank_idFinal = rank_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ personne pour un employé
        //x </summary>
        //! <param name="person_id">Personne à tester</param>
        //! <param name="person_idFinal">Personne finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette personne est valide, sinon faux</returns>
        public static bool TesterValidite_Person_id(Person person_id, out Person person_idFinal)
        {
            person_idFinal = null;
            if (person_id != null)
            {
                person_idFinal = person_id;
                return true;
            }
            else
                return false;
        }
        #endregion

        //x <summary>
        //! Nom de table utile à la création d'une entité de type employé
        //x </summary>
        public override string NameTable => "employee";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type employé
        //x </summary>
        public override string ListFields => "id_employee, login, password, rank_id, active, person_id";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type employé au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("login", Login);
                yield return FieldValue.Associate("password", Password);
                yield return FieldValue.Associate("rank_id", Rank_id);
                yield return FieldValue.Associate("active", Active);
                yield return FieldValue.Associate("person_id", Person_id);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Employee s_EmployeeDeReference = new Employee(0, string.Empty, string.Empty, Rank.ReferenceEntity, true, Person.ReferenceEntity);

        //x <summary>
        //! Permet de mettre à jour une entité de type employé
        //x </summary>
        //! <param name="employee">Référence d'objet à mettre à jour</param>
        //! <param name="login">Nom d'utilisateur d'un employé</param>
        //! <param name="Password">Mot de passe d'un employé</param>
        //! <param name="rank_id">Rang d'un employé</param>
        //! <param name="active">détermine s'il est actif ou non</param>
        //! <param name="person_id">Informations d'un employé</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref Employee employee, string login, string password, Rank rank_id, bool active, Person person_id)
        {
            if ((employee == null) || (employee.Id == 0)) return false;
            if (!TesterValidite_Login(login, out login)) return false;
            if (!TesterValidite_Password(password, out password)) return false;
            if (!TesterValidite_Rank_id(rank_id, out rank_id)) return false;
            if (!TesterValidite_Person_id(person_id, out person_id)) return false;

            employee.Login = login;
            employee.Password = password;
            employee.Rank_id = rank_id;
            employee.Active = active;
            employee.Person_id = person_id;

            if (employee.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type employé
        //x </summary>
        //! <param name="login">Nom d'utilisateur d'un employé</param>
        //! <param name="Password">Mot de passe d'un employé</param>
        //! <param name="rank_id">Rang d'un employé</param>
        //! <param name="active">détermine s'il est actif ou non</param>
        //! <param name="person_id">Informations d'un employé</param>
        //! <returns>Nouvelle personne si possible, sinon null</returns>
        public static Employee Create(string login, string password, Rank rank_id, bool active, Person person_id)
        {
            if (!TesterValidite_Login(login, out login)) return null;
            if (!TesterValidite_Password(password, out password)) return null;
            if (!TesterValidite_Rank_id(rank_id, out rank_id)) return null;
            if (!TesterValidite_Person_id(person_id, out person_id)) return null;
            return new Employee(0, login, password, rank_id, active, person_id);
        }

        //x <summary>
        //! Permet de charger si possible l'employé correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_employee">Identifiant de l'employé à charger</param>
        //! <returns>Employé correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Employee Load(uint id_employee)
        {
            var record = Entity<Employee>.Load(id_employee);
            if (record == null) return null;
            return new Employee(id_employee, record.GetFieldValue<string>("login")
                                           , record.GetFieldValue<string>("password")
                                           , Rank.Load(record.GetFieldValue<uint>("rank_id"))
                                           , record.GetFieldValue<bool>("active")
                                           , Person.Load(record.GetFieldValue<uint>("person_id")));
        }

        //x <summary>
        //! Permet de charger si possible l'employé correspondant au login spécifié
        //x </summary>
        //! <param name="login">login de l'employé à charger</param>
        //! <returns>Employé correspondant au login spécifié si possible, sinon null</returns>
        public static new Employee Load(string login)
        {
            var record = Entity<Employee>.Load(login);
            if (record == null) return null;
            return new Employee(record.GetFieldValue<uint>("id_employee")
                                           , login
                                           , record.GetFieldValue<string>("password")
                                           , Rank.Load(record.GetFieldValue<uint>("rank_id"))
                                           , record.GetFieldValue<bool>("active")
                                           , Person.Load(record.GetFieldValue<uint>("person_id")));
        }

        //x <summary>
        //! Permet de réinitialiser le mot de passe de l'employé
        //x </summary>
        //! <param name="employee">Employé ciblé</param>
        //! <returns>True si le mot de passe à été réinitialisé, sinon false</returns>
        public static bool Reset_Password(Employee employee)
        {
            Person person = Person.Load(employee.Person_id.Id);
            if (person == null) return false;
            var DefaultPassword_BeforeCrypto = "Hibou";
            var DefaultPassword_AfterCrypto = Encryption.GetSHA256(DefaultPassword_BeforeCrypto);
            if (Update(ref employee,employee.Login, DefaultPassword_AfterCrypto, employee.Rank_id, employee.Active, employee.Person_id))
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'activer un employé
        //x </summary>
        //! <param name="employee">Employé ciblé</param>
        //! <returns>True si l'employé à été activé, sinon false</returns>
        public static bool Activate(Employee employee)
        {
            if (employee == null) return false;
            var record = Entity<Employee>.Load(employee.Id);
            if (record == null) return false;
            employee.Active = true;
            if (employee.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet de désactiver un employé
        //x </summary>
        //! <param name="employee">Employé ciblé</param>
        //! <returns>True si l'employé à été désactivé, sinon false</returns>
        public static bool Desactivate(Employee employee)
        {
            if (employee == null) return false;
            var record = Entity<Employee>.Load(employee.Id);
            if (record == null) return false;
            employee.Active = false;
            if (employee.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Enumère tous les employés 
        //x </summary>
        public static IEnumerable<Employee> Enumeration
        {
            get
            {
                return Enumerer((record) => new Employee(
                                            record.GetFieldValue<uint>("id_employee"),
                                            record.GetFieldValue<string>("login"),
                                            record.GetFieldValue<string>("password"),
                                            Rank.Load(record.GetFieldValue<uint>("rank_id")),
                                            record.GetFieldValue<bool>("active"),
                                            Person.Load(record.GetFieldValue<uint>("person_id"))));
            }
        }
    }
}
