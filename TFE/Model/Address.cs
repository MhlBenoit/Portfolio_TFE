using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une adresse
    //x </summary>
    public class Address : Entity<Address>
    {
        //x <summary>
        //! Informations de l'adresse
        //x </summary>
        public string Name { get; set; }
        public City City_id { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_address">Identifiant de l'adresse</param>
        //! <param name="name">Nom de l'adresse</param>
        //! <param name="city_id">Identifiant d'une ville</param>
        public Address(uint id_address, string name, City city_id)
            : base(id_address)
        {
            DefinirReferenceEntity(this);
            Name = name;
            City_id = city_id;
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
        //! Permet de tester la validité du champ nom pour une adresse
        //x </summary>
        //! <param name="name">Nom à tester</param>
        //! <param name="nameFinal">Nom finalement retenu si il est valide</param>
        //! <para>Ce nom a subi une transformation de type Trim</para></param>
        //! <param name="idAExclure">Eventuel identifiant de adresse à exclure du test</param>
        //! <returns>Vrai si ce nom de adresse est valide, sinon faux</returns>
        public static bool TesterValidite_Name(string name, out string nameFinal, int idAExclure = 0)
        {
            nameFinal = null;
            if (name == null) return false;
            name = name.Trim();
            if (name.Length > 100) return false;
            if (VerifyPresence(name, idAExclure)) return false;
            nameFinal = name;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ ville pour une adresse
        //x </summary>
        //! <param name="city_id">Ville à tester</param>
        //! <param name="city_idFinal">Ville finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette ville est valide, sinon faux</returns>
        public static bool TesterValidite_City_id(City city_id, out City city_idFinal)
        {
            city_idFinal = null;
            if (city_id != null)
            {
                city_idFinal = city_id;
                return true;
            }
            else
                return false;
        }
        #endregion

        //x <summary>
        //! Nom de table utile à la création d'une entité de type adresse
        //x </summary>
        public override string NameTable => "address";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type adresse
        //x </summary>
        public override string ListFields => "id_address, address, city_id";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type adresse au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("address", Name);
                yield return FieldValue.Associate("city_id", City_id);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Address s_AddressDeReference = new Address(0, string.Empty, City.ReferenceEntity);

        //x <summary>
        //! Permet de mettre à jour une entité de type adresse
        //x </summary>
        //! <param name="address">Référence d'objet à mettre à jour</param>
        //! <param name="name">Nom de l'adresse</param>
        //! <param name="city_id">Identifiant d'une ville</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref Address address, string name, City city_id)
        {
            if ((address == null) || (address.Id == 0)) return false;
            if (!TesterValidite_Name(name, out name)) return false;
            if (!TesterValidite_City_id(city_id, out city_id)) return false;

            address.Name = name;
            address.City_id = city_id;

            if (address.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type adresse
        //x </summary>
        //! <param name="name">Nom de l'adresse</param>
        //! <param name="city_id">Identifiant d'une ville</param>
        //! <returns>Nouvelle adresse si possible, sinon null</returns>
        public static Address Create(string name, City city_id)
        {
            if (!TesterValidite_City_id(city_id, out city_id)) return null;
            if (!TesterValidite_Name(name, out name)) return null;
            return new Address(0, name, city_id);
        }

        //x <summary>
        //! Permet de charger si possible l'adresse correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_address">Identifiant de l'adresse à charger</param>
        //! <returns>Address correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Address Load(uint id_address)
        {
            var record = Entity<Address>.Load(id_address);
            if (record == null) return null;
            return new Address(id_address, record.GetFieldValue<string>("address")
                                         , City.Load(record.GetFieldValue<uint>("city_id")));
        }

        //x <summary>
        //! Enumère toutes les adresses
        //x </summary>
        public static IEnumerable<Address> Enumeration
        {
            get
            {
                return Enumerer((record) => new Address(
                        record.GetFieldValue<uint>("id_address"),
                        record.GetFieldValue<string>("address"),
                        City.Load(record.GetFieldValue<uint>("city_id"))));
            }
        }
    }
}
