using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une personne
    //x </summary>
    public class Person : Entity<Person>
    {
        //x <summary>
        //! Informations d'une personne
        //x </summary>
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_person">Identifiant d'une personne</param>
        //! <param name="lastname">Nom d'une personne</param>
        //! <param name="firstname">Prénom d'une personne</param>
        //! <param name="phone">Téléphone d'une personne</param>
        //! <param name="mail">Email d'une personne</param>
        public Person(uint id_person, string lastname, string firstname, string phone, string mail)
            : base(id_person)
        {
            DefinirReferenceEntity(this);
            Lastname = lastname;
            Firstname = firstname;
            Phone = phone;
            Mail = mail;
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
        //! Permet de tester la validité du champ nom pour une personne
        //x </summary>
        //! <param name="lastname">Nom à tester</param>
        //! <param name="lastnameFinal">Nom finalement retenu si il est valide</param>
        //! <para>Ce nom a subi une transformation de type Trim</para></param>
        //! <returns>Vrai si ce nom est valide, sinon faux</returns>
        public static bool TesterValidite_Lastname(string lastname, out string lastnameFinal)
        {
            lastnameFinal = null;
            if (lastname == null) return false;
            lastname = lastname.Trim();
            if ((lastname.Length < 3) || (lastname.Length > 50)) return false;
            lastnameFinal = lastname;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ nom pour une personne
        //x </summary>
        //! <param name="lastname">Nom à tester</param>
        //! <param name="lastnameFinal">Nom finalement retenu si il est valide</param>
        //! <para>Ce nom a subi une transformation de type Trim</para></param>
        //! <returns>Vrai si ce nom est valide, sinon faux</returns>
        public static bool TesterValidite_Lastname_Empty(string lastname, out string lastnameFinal)
        {
            lastnameFinal = null;
            if (lastname == null) return false;
            lastname = lastname.Trim();
            if (lastname.Length > 50) return false;
            lastnameFinal = lastname;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ prénom pour une personne
        //x </summary>
        //! <param name="firstname">Prénom à tester</param>
        //! <param name="firstnameFinal">Prénom finalement retenu si il est valide</param>
        //! <para>Ce prénom a subi une transformation de type Trim</para></param>
        //! <returns>Vrai si ce prénom est valide, sinon faux</returns>
        public static bool TesterValidite_Firstname(string firstname, out string firstnameFinal)
        {
            firstnameFinal = null;
            if (firstname == null) return false;
            firstname = firstname.Trim();
            if ((firstname.Length < 3) || (firstname.Length > 50)) return false;
            firstnameFinal = firstname;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ prénom pour une personne
        //x </summary>
        //! <param name="firstname">Prénom à tester</param>
        //! <param name="firstnameFinal">Prénom finalement retenu si il est valide</param>
        //! <para>Ce prénom a subi une transformation de type Trim</para></param>
        //! <returns>Vrai si ce prénom est valide, sinon faux</returns>
        public static bool TesterValidite_Firstname_Empty(string firstname, out string firstnameFinal)
        {
            firstnameFinal = null;
            if (firstname == null) return false;
            firstname = firstname.Trim();
            if (firstname.Length > 50) return false;
            firstnameFinal = firstname;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ téléphone pour une personne
        //x </summary>
        //! <param name="phone">Numéro de téléphone à tester</param>
        //! <param name="phoneFinal">Numéro de téléphone finalement retenu si il est valide</param>
        //! <para>Ce numéro de téléphone a subi une transformation de type Trim</para>
        //! <returns>Vrai si ce numéro de téléphone est valide, sinon faux</returns>
        public static bool TesterValidite_Phone(string phone, out string phoneFinal)
        {
            phoneFinal = null;
            if (phone == null) return false;
            phone = phone.Trim();
            if ((phone.Length < 2) || (phone.Length > 50)) return false;
            phoneFinal = phone;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ téléphone pour une personne
        //x </summary>
        //! <param name="phone">Numéro de téléphone à tester</param>
        //! <param name="phoneFinal">Numéro de téléphone finalement retenu si il est valide</param>
        //! <para>Ce numéro de téléphone a subi une transformation de type Trim</para></param>
        //! <returns>Vrai si ce numéro de téléphone est valide, sinon faux</returns>
        public static bool TesterValidite_Phone_Empty(string phone, out string phoneFinal)
        {
            phoneFinal = null;
            if (phone == null) return false;
            phone = phone.Trim();
            if (phone.Length > 50) return false;
            phoneFinal = phone;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ email pour une personne
        //x </summary>
        //! <param name="mail">Email à tester</param>
        //! <param name="mailFinal">Email finalement retenu si il est valide</param>
        //! <para>Cet email a subi une transformation de type Trim</para></param>
        //! <returns>Vrai si cet email est valide, sinon faux</returns>
        public static bool TesterValidite_Mail(string mail, out string mailFinal)
        {
            mailFinal = null;
            if (mail == null) return false;
            mail = mail.Trim();
            if ((mail.Length < 2) || (mail.Length > 50)) return false;
            mailFinal = mail;
            return true;
        }
        #endregion

        //x <summary>
        //! Nom de table utile à la création d'une entité de type personne
        //x </summary>
        public override string NameTable => "person";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type personne
        //x </summary>
        public override string ListFields => "id_person, lastname, firstname, phone, mail";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type personne au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("lastname", Lastname);
                yield return FieldValue.Associate("firstname", Firstname);
                yield return FieldValue.Associate("phone", Phone);
                yield return FieldValue.Associate("mail", Mail);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Person s_PersonDeReference = new Person(0, string.Empty, string.Empty, string.Empty, string.Empty);

        //x <summary>
        //! Permet de mettre à jour une entité de type personne pour un employé
        //x </summary>
        //! <param name="person">Référence d'objet à mettre à jour</param>
        //! <param name="lastname">Nom d'une personne</param>
        //! <param name="firstname">Prénom d'une personne</param>
        //! <param name="phone">Téléphone d'une personne</param>
        //! <param name="mail">Email d'une personne</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool UpdateEmployee(ref Person person, string lastname, string firstname, string phone, string mail)
        {
            if ((person == null) || (person.Id == 0)) return false;
            if (!TesterValidite_Lastname(lastname, out lastname)) return false;
            if (!TesterValidite_Firstname(firstname, out firstname)) return false;
            if (!TesterValidite_Phone(phone, out phone)) return false;
            if (!TesterValidite_Mail(mail, out mail)) return false;

            person.Lastname = lastname;
            person.Firstname = firstname;
            person.Phone = phone;
            person.Mail = mail;

            if (person.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet de mettre à jour une entité de type personne pour un client
        //x </summary>
        //! <param name="person">Référence d'objet à mettre à jour</param>
        //! <param name="lastname">Nom d'une personne</param>
        //! <param name="firstname">Prénom d'une personne</param>
        //! <param name="phone">Téléphone d'une personne</param>
        //! <param name="mail">Email d'une personne</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool UpdateCustomer(ref Person person, string lastname, string firstname, string phone, string mail)
        {
            if ((person == null) || (person.Id == 0)) return false;
            if (!TesterValidite_Lastname_Empty(lastname, out lastname)) return false;
            if (!TesterValidite_Firstname_Empty(firstname, out firstname)) return false;
            if (!TesterValidite_Phone_Empty(phone, out phone)) return false;
            if (!TesterValidite_Mail(mail, out mail)) return false;

            person.Lastname = lastname;
            person.Firstname = firstname;
            person.Phone = phone;
            person.Mail = mail;

            if (person.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type personne pour un employé
        //x </summary>
        //! <param name="lastname">Nom d'une personne</param>
        //! <param name="firstname">Prénom d'une personne</param>
        //! <param name="phone">Téléphone d'une personne</param>
        //! <param name="mail">Email d'une personne</param>
        //! <returns>Nouvelle personne si possible, sinon null</returns>
        public static Person CreateEmployee(string lastname, string firstname, string phone, string mail)
        {
            if (!TesterValidite_Lastname(lastname, out lastname)) return null;
            if (!TesterValidite_Firstname(firstname, out firstname)) return null;
            if (!TesterValidite_Phone(phone, out phone)) return null;
            if (!TesterValidite_Mail(mail, out mail)) return null;
            return new Person(0, lastname, firstname, phone, mail);
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type personne pour un client
        //x </summary>
        //! <param name="lastname">Nom d'une personne</param>
        //! <param name="firstname">Prénom d'une personne</param>
        //! <param name="phone">Téléphone d'une personne</param>
        //! <param name="mail">Email d'une personne</param>
        //! <returns>Nouvelle personne si possible, sinon null</returns>
        public static Person CreateCustomer(string lastname, string firstname, string phone, string mail)
        {
            if (!TesterValidite_Lastname_Empty(lastname, out lastname)) return null;
            if (!TesterValidite_Firstname_Empty(firstname, out firstname)) return null;
            if (!TesterValidite_Phone_Empty(phone, out phone)) return null;
            if (!TesterValidite_Mail(mail, out mail)) return null;
            return new Person(0, lastname, firstname, phone, mail);
        }

        //x <summary>
        //! Permet de charger si possible la personne correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_person">Identifiant de la personne à charger</param>
        //! <returns>Personne correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Person Load(uint id_person)
        {
            var record = Entity<Person>.Load(id_person);
            if (record == null) return null;
            return new Person(id_person, record.GetFieldValue<string>("lastname")
                                       , record.GetFieldValue<string>("firstname")
                                       , record.GetFieldValue<string>("phone")
                                       , record.GetFieldValue<string>("mail"));
        }

        //x <summary>
        //! Enumère toutes les personnes
        //x </summary>
        public static IEnumerable<Person> Enumeration
        {
            get
            {
                return Enumerer((record) => new Person(
                        record.GetFieldValue<uint>("id_person"),
                        record.GetFieldValue<string>("lastname"),
                        record.GetFieldValue<string>("firstname"),
                        record.GetFieldValue<string>("phone"),
                        record.GetFieldValue<string>("mail")));
            }
        }
    }
}
