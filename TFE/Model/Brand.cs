using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une marque
    //x </summary>
    public class Brand : Entity<Brand>
    {
        //x <summary>
        //! Informations de la marque
        //x </summary>
        public string Name { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_brand">Identifiant de la marque</param>
        //! <param name="name">Nom de la marque</param>
        public Brand(uint id_brand, string name)
            : base(id_brand)
        {
            DefinirReferenceEntity(this);
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
        //! Permet de tester la validité du champ nom pour une marque
        //x </summary>
        //! <param name="name">Nom à tester</param>
        //! <param name="nameFinal">Nom finalement retenu si il est valide</param>
        //! <para>Ce nom a subi une transformation de type Trim</para></param>
        //! <param name="idAExclure">Eventuel identifiant de marque à exclure du test</param>
        //! <returns>Vrai si ce nom de marque est valide, sinon faux</returns>
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
        //! Nom de table utile à la création d'une entité de type marque
        //x </summary>
        public override string NameTable => "brand";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type marque
        //x </summary>
        public override string ListFields => "id_brand, name";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type marque au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("name", Name);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Brand s_BrandDeReference = new Brand(0, string.Empty);

        //x <summary>
        //! Permet de mettre à jour une entité de type marque
        //x </summary>
        //! <param name="brand">Référence d'objet à mettre à jour</param>
        //! <param name="name">Nom de la marque</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref Brand brand, string name)
        {
            if ((brand == null) || (brand.Id == 0)) return false;
            if (!TesterValidite_Name(name, out name)) return false;

            brand.Name = name;

            if (brand.Save())
                return true;
            else
                return false;
        }
        
        //x <summary>
        //! Permet d'instancier une nouvelle entité de type marque
        //x </summary>
        //! <param name="name">Nom de la marque</param>
        //! <returns>Nouvelle marque si possible, sinon null</returns>
        public static Brand Create(string name)
        {
            if (!TesterValidite_Name(name, out name)) return null;
            return new Brand(0, name);
        }

        //x <summary>
        //! Permet de charger si possible la marque correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_brand">Identifiant de la marque à charger</param>
        //! <returns>Marque correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Brand Load(uint id_brand)
        {
            var record = Entity<Brand>.Load(id_brand);
            if (record == null) return null;
            return new Brand(id_brand, record.GetFieldValue<string>("name"));
        }

        //x <summary>
        //! Enumère toutes les marques
        //x </summary>
        public static IEnumerable<Brand> Enumeration
        {
            get
            {
                return Enumerer((record) => new Brand(
                        record.GetFieldValue<uint>("id_brand"),
                        record.GetFieldValue<string>("name")));
            }
        }
    }

}
