using System;
using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit un client
    //x </summary>
    public class Customer : Entity<Customer>
    {
        //x <summary>
        //! Informations d'un client
        //x </summary>
        public DateTime? Borndate { get; set; }
        public Address Address_id { get; set; }
        public int? Loyalty_points { get; set; }
        public Person Person_id { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_customer">Identifiant d'un client</param>
        //! <param name="borndate">Date de naissance d'un client</param>
        //! <param name="address_id">Adresse d'un client</param>
        //! <param name="loyalty_points">Points de fidelité d'un client</param>
        //! <param name="person_id">Informations du client</param>
        public Customer(uint id_customer, DateTime? borndate, Address address_id, int? loyalty_points, Person person_id)
            : base(id_customer)
        {
            DefinirReferenceEntity(this);
            Borndate = borndate;
            Address_id = address_id;
            Loyalty_points = loyalty_points;
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
        //! Permet de tester la validité du champ date de naissance pour un client
        //x </summary>
        //! <param name="borndate">Date de naissance à tester</param>
        //! <param name="borndateFinal">Date de naissance finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette date de naissance est valide, sinon faux</returns>
        public static bool TesterValidite_Borndate(DateTime? borndate, out DateTime? borndateFinal)
        {
            borndateFinal = borndate;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ adresse pour un client
        //x </summary>
        //! <param name="address_id">Adresse à tester</param>
        //! <param name="address_idFinal">Adresse finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette adresse est valide, sinon faux</returns>
        public static bool TesterValidite_Address_id(Address address_id, out Address address_idFinal)
        {
            address_idFinal = null;
            if (address_id != null)
            {
                address_idFinal = address_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ points de fidelité pour un client
        //x </summary>
        //! <param name="loyalty_points">Points de fidelité à tester</param>
        //! <param name="loyalty_pointsFinal">Points de fidelité finalement retenu si le nombre est valide</param>
        //! <returns>Vrai si le nombre de points de fidelité est valide, sinon faux</returns>
        public static bool TesterValidite_Loyalty_Points(int? loyalty_points, out int? loyalty_pointsFinal)
        {
            loyalty_pointsFinal = 0;
            if (loyalty_points >= 0)
            {
                loyalty_pointsFinal = loyalty_points;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ personne pour un client
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
        //! Nom de table utile à la création d'une entité de type client
        //x </summary>
        public override string NameTable => "customer";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type client
        //x </summary>
        public override string ListFields => "id_customer, borndate, address_id, loyalty_points, person_id";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type client au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("borndate", Borndate);
                yield return FieldValue.Associate("address_id", Address_id);
                yield return FieldValue.Associate("loyalty_points", Loyalty_points);
                yield return FieldValue.Associate("person_id", Person_id);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Customer s_CustomerDeReference = new Customer(0, null, Address.ReferenceEntity, null, Person.ReferenceEntity);

        //x <summary>
        //! Permet de mettre à jour une entité de type client
        //x </summary>
        //! <param name="customer">Référence d'objet à mettre à jour</param>
        //! <param name="borndate">Date de naissance d'un client</param>
        //! <param name="address_id">Adresse d'un client</param>
        //! <param name="loyalty_points">Points de fidelité d'un client</param>
        //! <param name="person_id">Informations du client</param>
        public static bool Update(ref Customer customer, DateTime? borndate, Address address_id, int? loyalty_points, Person person_id)
        {
            if ((customer == null) || (customer.Id == 0)) return false;
            if (!TesterValidite_Borndate(borndate, out borndate)) return false;
            if (!TesterValidite_Address_id(address_id, out address_id)) return false;
            if (!TesterValidite_Loyalty_Points(loyalty_points, out loyalty_points)) return false;
            if (!TesterValidite_Person_id(person_id, out person_id)) return false;
            if (customer.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type client
        //x </summary>
        //! <param name="borndate">Date de naissance d'un client</param>
        //! <param name="address_id">Adresse d'un client</param>
        //! <param name="loyalty_points">Points de fidelité d'un client</param>
        //! <param name="person_id">Informations du client</param>
        public static Customer Create(DateTime? borndate, Address address_id, int? loyalty_points, Person person_id)
        {
            if (!TesterValidite_Borndate(borndate, out borndate)) return null;
            if (!TesterValidite_Address_id(address_id, out address_id)) return null;
            if (!TesterValidite_Loyalty_Points(loyalty_points, out loyalty_points)) return null;
            if (!TesterValidite_Person_id(person_id, out person_id)) return null;
            return new Customer(0, borndate, address_id, loyalty_points, person_id);
        }

        //x <summary>
        //! Permet de charger si possible le client correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_customer">Identifiant du client à charger</param>
        //! <returns>Client correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Customer Load(uint id_customer)
        {
            var record = Entity<Customer>.Load(id_customer);
            if (record == null) return null;
            return new Customer(id_customer, record.GetFieldValue<DateTime?>("borndate")
                                           , Address.Load(record.GetFieldValue<uint>("address_id"))
                                           , record.GetFieldValue<int?>("loyalty_points")
                                           , Person.Load(record.GetFieldValue<uint>("person_id")));
        }

        //x <summary>
        //! Permet d'ajouter des points de fidelités au client
        //x </summary>
        //! <param name="customer">Référence d'objet à mettre à jour</param>
        //! <param name="points">Points à rajouter</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Add_Loyalty_Points(Customer customer, int points)
        {
            var record = Entity<Customer>.Load(customer.Id);
            if (record == null) return false;
            customer.Loyalty_points += points;
            if (customer.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet de retirer des points de fidelités au client
        //x </summary>
        //! <param name="customer">Référence d'objet à mettre à jour</param>
        //! <param name="points">Points à retirer</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Remove_Loyalty_Points(Customer customer, int? points)
        {
            var record = Entity<Customer>.Load(customer.Id);
            if (record == null) return false;
            if (customer.Loyalty_points < points) return false;
            customer.Loyalty_points -= points;
            if (customer.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Enumère tous les clients 
        //x </summary>
        public static IEnumerable<Customer> Enumeration
        {
            get
            {
                return Enumerer((record) => new Customer(
                                            record.GetFieldValue<uint>("id_customer"),
                                            record.GetFieldValue<DateTime?>("borndate"),
                                            Address.Load(record.GetFieldValue<uint>("address_id")),
                                            record.GetFieldValue<int?>("loyalty_points"),
                                            Person.Load(record.GetFieldValue<uint>("person_id"))));
            }
        }
    }
}
