using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une ville
    //x </summary>
    public class City : Entity<City>
    {
        //x <summary>
        //! Informations de la ville
        //x </summary>
        public string PostalCode { get; set; }
        public string Name { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_city">Identifiant de la ville</param>
        //! <param name="postalcode">Code postal de la ville</param>
        //! <param name="name">Nom de la ville</param>
        public City(uint id_city, string postalcode, string name)
            : base(id_city)
        {
            DefinirReferenceEntity(this);
            PostalCode = postalcode;
            Name = name;
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
        //! Permet de tester la validité du champ code postal pour une ville
        //x </summary>
        //! <param name="postalcode">Code postal à tester</param>
        //! <param name="postalcodeFinal">Code postal finalement retenu si il est valide</param>
        //! <returns>Vrai si ce code postal est valide, sinon faux</returns>
        public static bool TesterValidite_PostalCode(string postalcode, out string postalcodeFinal)
        {
            postalcodeFinal = null;
            if (postalcode == null) return false;
            postalcode = postalcode.Trim();
            if (postalcode.Length < 4 || postalcode.Length > 5) return false;
            if (int.Parse(postalcode) < 1000 || int.Parse(postalcode) > 99999) return false;
            postalcodeFinal = postalcode;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ nom pour une ville
        //x </summary>
        //! <param name="name">Nom à tester</param>
        //! <param name="nameFinal">Nom finalement retenu si il est valide</param>
        //! <para>Ce nom a subi une transformation de type Trim</para>
        //! <param name="idAExclure">Eventuel identifiant de ville à exclure du test</param>
        //! <returns>Vrai si ce nom de ville est valide, sinon faux</returns>
        public static bool TesterValidite_Name(string name, out string nameFinal, int idAExclure = 0)
        {
            nameFinal = null;
            if (name == null) return false;
            name = name.Trim();
            if ((name.Length < 2) || (name.Length > 50)) return false;
            if (VerifyPresence(name, idAExclure)) return false;
            nameFinal = name;
            return true;
        }
        #endregion

        //x <summary>
        //! Nom de table utile à la création d'une entité de type ville
        //x </summary>
        public override string NameTable => "city";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type ville
        //x </summary>
        public override string ListFields => "id_city, postal_code, name";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type ville au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("postal_code", PostalCode);
                yield return FieldValue.Associate("name", Name);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static City s_CityDeReference = new City(0, string.Empty, string.Empty);

        //x <summary>
        //! Permet de mettre à jour une entité de type ville
        //x </summary>
        //! <param name="city">Référence d'objet à mettre à jour</param>
        //! <param name="postalcode">Code postal de la ville</param>
        //! <param name="name">Nom de la ville</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref City city, string postalcode, string name)
        {
            if ((city == null) || (city.Id == 0)) return false;
            if (!TesterValidite_PostalCode(postalcode, out postalcode)) return false;
            if (!TesterValidite_Name(name, out name)) return false;

            city.PostalCode = postalcode;
            city.Name = name;

            if (city.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type ville
        //x </summary>
        //! <param name="postalcode">Code postal de la ville</param>
        //! <param name="name">Nom de la ville</param>
        //! <returns>Nouvelle ville si possible, sinon null</returns>
        public static City Create(string postalcode, string name)
        {
            if (!TesterValidite_PostalCode(postalcode, out postalcode)) return null;
            if (!TesterValidite_Name(name, out name)) return null;
            return new City(0, postalcode, name);
        }

        //x <summary>
        //! Permet de charger si possible la ville correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_city">Identifiant de la ville à charger</param>
        //! <returns>City correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new City Load(uint id_city)
        {
            var record = Entity<City>.Load(id_city);
            if (record == null) return null;
            return new City(id_city, record.GetFieldValue<string>("postal_code")
                                   , record.GetFieldValue<string>("name"));
        }

        //x <summary>
        //! Enumère toutes les villes
        //x </summary>
        public static IEnumerable<City> Enumeration
        {
            get
            {
                return Enumerer((record) => new City(
                        record.GetFieldValue<uint>("id_city"),
                        record.GetFieldValue<string>("postal_code"),
                        record.GetFieldValue<string>("name")));
            }
        }
    }
}
