using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une TVA
    //x </summary>
    public class Tva : Entity<Tva>
    {
        //x <summary>
        //! Informations de la TVA
        //x </summary>
        public double Value { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param value="id_tva">Identifiant de la TVA</param>
        //! <param value="value">Valeur de la TVA</param>
        public Tva(uint id_tva, double value)
            : base(id_tva)
        {
            DefinirReferenceEntity(this);
            Value = value;
        }

        //x <summary>
        //! Représente sous forme de texte cette entité
        //x </summary>
        //! <returns>Texte représentatif de cet objet</returns>
        public override string ToString()
        {
            return $"{Id}";
        }

        //x <summary>
        //! Nom de table utile à la création d'une entité de type TVA
        //x </summary>
        public override string NameTable => "tva";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type TVA
        //x </summary>
        public override string ListFields => "id_tva, value";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type TVA au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("value", Value);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Tva s_TvaDeReference = new Tva(0, 0);

        //x <summary>
        //! Permet de charger si possible la TVA correspondant à l'identifiant spécifié
        //x </summary>
        //! <param value="id_tva">Identifiant de la TVA à charger</param>
        //! <returns>TVA correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Tva Load(uint id_tva)
        {
            var record = Entity<Tva>.Load(id_tva);
            if (record == null) return null;
            return new Tva(id_tva, record.GetFieldValue<double>("value"));
        }

        //x <summary>
        //! Enumère toutes les TVA
        //x </summary>
        public static IEnumerable<Tva> Enumeration
        {
            get
            {
                return Enumerer((record) => new Tva(
                        record.GetFieldValue<uint>("id_tva"),
                        record.GetFieldValue<double>("value")));
            }
        }
    }

}
