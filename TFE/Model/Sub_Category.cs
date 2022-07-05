using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une sous-catégorie
    //x </summary>
    public class Sub_Category : Entity<Sub_Category>
    {
        //x <summary>
        //! Informations de la sous-catégorie
        //x </summary>
        public Category Category_id { get; set; }
        public string Name { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_sub_category">Identifiant de la sous-catégorie</param>
        //! <param name="category_id">Identifiant d'une catégorie</param>
        //! <param name="name">Nom de la sous-catégorie</param>
        public Sub_Category(uint id_sub_category, Category category_id, string name)
            : base(id_sub_category)
        {
            DefinirReferenceEntity(this);
            Category_id = category_id;
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
        //! Permet de tester la validité du champ catégorie pour un forum
        //x </summary>
        //! <param name="category_id">Catégorie à tester</param>
        //! <param name="category_idFinal">Catégorie finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette catégorie est valide, sinon faux</returns>
        public static bool TesterValidite_Category_id(Category category_id, out Category category_idFinal)
        {
            category_idFinal = null;
            if (category_id != null)
            {
                category_idFinal = category_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ nom pour une sous-catégorie
        //x </summary>
        //! <param name="name">Nom à tester</param>
        //! <param name="nameFinal">Nom finalement retenu si il est valide</param>
        //! <para>Ce nom a subi une transformation de type Trim</para>
        //! <param name="idAExclure">Eventuel identifiant de sous-catégorie à exclure du test</param>
        //! <returns>Vrai si ce nom est valide, sinon faux</returns>
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
        //! Nom de table utile à la création d'une entité de type sous-catégorie
        //x </summary>
        public override string NameTable => "sub_category";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type sous-catégorie
        //x </summary>
        public override string ListFields => "id_sub_category, category_id, name";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type sous-catégorie au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("category_id", Category_id);
                yield return FieldValue.Associate("name", Name);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Sub_Category s_Sub_CategoryDeReference = new Sub_Category(0, Category.ReferenceEntity, string.Empty);

        //x <summary>
        //! Permet de mettre à jour une entité de type sous-catégorie
        //x </summary>
        //! <param name="sub_category">Référence d'objet à mettre à jour</param>
        //! <param name="name">Nouveau nom à modifier en base de donnée</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref Sub_Category sub_category, string name)
        {
            if ((sub_category == null) || (sub_category.Id == 0)) return false;
            if (!TesterValidite_Name(name, out name)) return false;

            sub_category.Name = name;

            if (sub_category.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type sous-catégorie
        //x </summary>
        //! <param name="category_id">Catégorie liée à la catégorie</param>
        //! <param name="name">Nom de la sous-catégorie</param>
        //! <returns>Nouvelle sous-catégorie si possible, sinon null</returns>
        public static Sub_Category Create(Category category_id, string name)
        {
            if (!TesterValidite_Category_id(category_id, out category_id)) return null;
            if (!TesterValidite_Name(name, out name)) return null;
            return new Sub_Category(0, category_id, name);
        }

        //x <summary>
        //! Permet de charger si possible la sous-catégorie correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_sub_category">Identifiant de la sous-catégorie à charger</param>
        //! <returns>Sous-catégorie correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Sub_Category Load(uint id_sub_category)
        {
            var record = Entity<Sub_Category>.Load(id_sub_category);
            if (record == null) return null;
            return new Sub_Category(id_sub_category, Category.Load(record.GetFieldValue<uint>("category_id"))
                                                   , record.GetFieldValue<string>("name"));
        }

        //x <summary>
        //! Enumère toutes les sous-catégories
        //x </summary>
        public static IEnumerable<Sub_Category> Enumeration
        {
            get
            {
                return Enumerer((record) => new Sub_Category(
                        record.GetFieldValue<uint>("id_sub_category"),
                        Category.Load(record.GetFieldValue<uint>("category_id")),
                        record.GetFieldValue<string>("name")));
            }
        }
    }
}
