using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une catégorie
    //x </summary>
    public class Category : Entity<Category>
    {
        //x <summary>
        //! Informations de la catégorie
        //x </summary>
        public string Name { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_category">Identifiant de la catégorie</param>
        //! <param name="name">Nom de la catégorie</param>
        public Category(uint id_category, string name)
            : base(id_category)
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
        //! Permet de tester la validité du champ nom pour une catégorie
        //x </summary>
        //! <param name="name">Nom à tester</param>
        //! <param name="nameFinal">Nom finalement retenu si il est valide</param>
        //! <para>Ce nom a subi une transformation de type Trim</para></param>
        //! <param name="idAExclure">Eventuel identifiant de catégorie à exclure du test</param>
        //! <returns>Vrai si ce nom de catégorie est valide, sinon faux</returns>
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
        //! Nom de table utile à la création d'une entité de type catégorie
        //x </summary>
        public override string NameTable => "category";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type catégorie
        //x </summary>
        public override string ListFields => "id_category, name";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type catégorie au sein de la base de données
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
        private static Category s_CategoryDeReference = new Category(0, string.Empty);

        //x <summary>
        //! Permet de mettre à jour une entité de type catégorie
        //x </summary>
        //! <param name="category">Référence d'objet à mettre à jour</param>
        //! <param name="name">Nom de la catégorie</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref Category category, string name)
        {
            if ((category == null) || (category.Id == 0)) return false;
            if (!TesterValidite_Name(name, out name)) return false;

            category.Name = name;

            if (category.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type catégorie
        //x </summary>
        //! <param name="name">Nom de la catégorie</param>
        //! <returns>Nouvelle catégorie si possible, sinon null</returns>
        public static Category Create(string name)
        {
            if (!TesterValidite_Name(name, out name)) return null;
            return new Category(0, name);
        }

        //x <summary>
        //! Permet de charger si possible la catégorie correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_category">Identifiant de la catégorie à charger</param>
        //! <returns>Category correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Category Load(uint id_category)
        {
            var record = Entity<Category>.Load(id_category);
            if (record == null) return null;
            return new Category(id_category, record.GetFieldValue<string>("name"));
        }

        //x <summary>
        //! Enumère toutes les catégories
        //x </summary>
        public static IEnumerable<Category> Enumeration
        {
            get
            {
                return Enumerer((record) => new Category(
                        record.GetFieldValue<uint>("id_category"),
                        record.GetFieldValue<string>("name")));
            }
        }
    }
}
