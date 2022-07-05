using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit un rang
    //x </summary>
    public class Rank : Entity<Rank>
    {
        //x <summary>
        //! Informations d'un rang
        //x </summary>
        public string Name { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_rank">Identifiant du rang</param>
        //! <param name="name">Nom du rang</param>
        public Rank(uint id_rank, string name)
            : base(id_rank)
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
        //! Permet de tester la validité du champ nom pour un rang
        //x </summary>
        //! <param name="name">Nom à tester</param>
        //! <param name="nameFinal">Nom finalement retenu si il est valide</param>
        //! <para>Ce nom a subi une transformation de type Trim</para>
        //! <param name="idAExclure">Eventuel identifiant de catégorie à exclure du test</param>
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
        //! Nom de table utile à la création d'une entité de type rang
        //x </summary>
        public override string NameTable => "rank";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type rang
        //x </summary>
        public override string ListFields => "id_rank, name";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type rang au sein de la base de données
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
        private static Rank s_RankDeReference = new Rank(0, string.Empty);

        //x <summary>
        //! Permet de mettre à jour une entité de type rang
        //x </summary>
        //! <param name="rank">Référence d'objet à mettre à jour</param>
        //! <param name="name">Nouveau nom à modifier en base de donnée</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref Rank rank, string name)
        {
            if ((rank == null) || (rank.Id == 0)) return false;
            if (!TesterValidite_Name(name, out name)) return false;
            if (rank.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type rang
        //x </summary>
        //! <param name="name">Nom du rang</param>
        //! <returns>Nouveau rang si possible, sinon null</returns>
        public static Rank Create(string name)
        {
            if (!TesterValidite_Name(name, out name)) return null;
            return new Rank(0, name);
        }

        //x <summary>
        //! Permet de charger si possible le rang correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_rank">Identifiant du rang à charger</param>
        //! <returns>Rang correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Rank Load(uint id_rank)
        {
            var record = Entity<Rank>.Load(id_rank);
            if (record == null) return null;
            return new Rank(id_rank, record.GetFieldValue<string>("name"));
        }

        //x <summary>
        //! Enumère tous les rangs
        //x </summary>
        public static IEnumerable<Rank> Enumeration
        {
            get
            {
                return Enumerer((record) => new Rank(
                        record.GetFieldValue<uint>("id_rank"),
                        record.GetFieldValue<string>("name")));
            }
        }
    }

}
