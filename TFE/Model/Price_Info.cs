using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une info de prix
    //x </summary>
    public class Price_Info : Entity<Price_Info>
    {
        //x <summary>
        //! Informations d'une info de prix
        //x </summary>
        public decimal? Buying_price { get; set; }
        public decimal? Selling_price { get; set; }
        public Tva Tva_id { get; set; }
        public double? Promotion { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_price_info">Identifiant d'une info de prix</param>
        //! <param name="buying_price">Prix d'achat d'une info de prix</param>
        //! <param name="selling_price">Prix de vente d'une info de prix</param>
        //! <param name="tva_id">Identifiant d'une tva</param>
        //! <param name="promotion">Promotion d'une info de prix</param>
        public Price_Info(uint id_price_info, decimal? buying_price, decimal? selling_price, Tva tva_id, double? promotion)
            : base(id_price_info)
        {
            DefinirReferenceEntity(this);
            Buying_price = buying_price;
            Selling_price = selling_price;
            Tva_id = tva_id;
            Promotion = promotion;
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
        //! Permet de tester la validité du champ prix d'achat pour une info de prix
        //x </summary>
        //! <param name="buying_price">Prix d'achat à tester</param>
        //! <param name="buying_priceFinal">Prix d'achat finalement retenu si il est valide</param>
        //! <returns>Vrai si ce prix d'achat est valide, sinon faux</returns>
        public static bool TesterValidite_Buying_Price(decimal buying_price, out decimal buying_priceFinal)
        {
            buying_priceFinal = 0;
            if (buying_price < 0) return false;
            buying_priceFinal = buying_price;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ prix de vente pour une info de prix
        //x </summary>
        //! <param name="selling_price">Prix de vente à tester</param>
        //! <param name="selling_priceFinal">Prix de vente finalement retenu si il est valide</param>
        //! <returns>Vrai si ce prix de vente est valide, sinon faux</returns>
        public static bool TesterValidite_Selling_Price(decimal selling_price, out decimal selling_priceFinal)
        {
            selling_priceFinal = 0;
            if (selling_price < 0) return false;
            selling_priceFinal = selling_price;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ TVA pour une info de prix
        //x </summary>
        //! <param name="tva_id">TVA à tester</param>
        //! <param name="tva_idFinal">TVA finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette TVA est valide, sinon faux</returns>
        public static bool TesterValidite_Tva_id(Tva tva_id, out Tva tva_idFinal)
        {
            tva_idFinal = null;
            if (tva_id != null)
            {
                tva_idFinal = tva_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ promotion pour une info de prix
        //x </summary>
        //! <param name="promotion">Promotion à tester</param>
        //! <param name="promotionFinal">Promotion finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette promotion est valide, sinon faux</returns>
        public static bool TesterValidite_Promotion(double promotion, out double promotionFinal)
        {
            promotionFinal = 0;
            if ((promotion < 0) || (promotion > 100)) return false;
            promotionFinal = promotion;
            return true;
        }
        #endregion

        //x <summary>
        //! Nom de table utile à la création d'une entité de type info de prix
        //x </summary>
        public override string NameTable => "price_info";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type info de prix
        //x </summary>
        public override string ListFields => "id_price_info, buying_price, selling_price, tva_id, promotion";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type info de prix au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("buying_price", Buying_price);
                yield return FieldValue.Associate("selling_price", Selling_price);
                yield return FieldValue.Associate("tva_id", Tva_id);
                yield return FieldValue.Associate("promotion", Promotion);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Price_Info s_Price_InfoDeReference = new Price_Info(0, null, null, Tva.ReferenceEntity, null);

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type info de prix
        //x </summary>
        //! <param name="buying_price">Prix d'achat d'une info de prix</param>
        //! <param name="selling_price">Prix de vente d'une info de prix</param>
        //! <param name="tva_id">Identifiant d'une tva</param>
        //! <param name="promotion">Promotion d'une info de prix</param>
        //! <returns>Nouvelle info de prix si possible, sinon null</returns>
        public static Price_Info Create(decimal buying_price, decimal selling_price, Tva tva_id, double promotion)
        {
            if (!TesterValidite_Buying_Price(buying_price, out buying_price)) return null;
            if (!TesterValidite_Selling_Price(selling_price, out selling_price)) return null;
            if (!TesterValidite_Tva_id(tva_id, out tva_id)) return null;
            if (!TesterValidite_Promotion(promotion, out promotion)) return null;
            return new Price_Info(0, buying_price, selling_price, tva_id, promotion);
        }

        //x <summary>
        //! Permet de mettre à jour une entité de type info de prix
        //x </summary>
        //! <param name="price_Info">Référence d'objet à mettre à jour</param>
        //! <param name="buying_price">Prix d'achat d'une info de prix</param>
        //! <param name="selling_price">Prix de vente d'une info de prix</param>
        //! <param name="tva_id">Identifiant d'une tva</param>
        //! <param name="promotion">Promotion d'une info de prix</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref Price_Info price_Info, decimal buying_price, decimal selling_price, Tva tva_id, double promotion)
        {
            if ((price_Info == null) || (price_Info.Id == 0)) return false;
            if (!TesterValidite_Buying_Price(buying_price, out buying_price)) return false;
            if (!TesterValidite_Selling_Price(selling_price, out selling_price)) return false;
            if (!TesterValidite_Tva_id(tva_id, out tva_id)) return false;
            if (!TesterValidite_Promotion(promotion, out promotion)) return false;

            price_Info.Buying_price = buying_price;
            price_Info.Selling_price = selling_price;
            price_Info.Tva_id = tva_id;
            price_Info.Promotion = promotion;

            if (price_Info.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet de charger si possible l'info de prix correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_price_info">Identifiant d'une info de prix à charger</param>
        //! <returns>Info de prix correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Price_Info Load(uint id_price_info)
        {
            var record = Entity<Price_Info>.Load(id_price_info);
            if (record == null) return null;
            return new Price_Info(id_price_info, record.GetFieldValue<decimal>("buying_price")
                                               , record.GetFieldValue<decimal>("selling_price")
                                               , Tva.Load(record.GetFieldValue<uint>("tva_id"))
                                               , record.GetFieldValue<double>("promotion"));
        }

        //x <summary>
        //! Enumère toutes les infos de prix
        //x </summary>
        public static IEnumerable<Price_Info> Enumeration
        {
            get
            {
                return Enumerer((record) => new Price_Info(
                        record.GetFieldValue<uint>("id_price_info"),
                        record.GetFieldValue<decimal>("buying_price"),
                        record.GetFieldValue<decimal>("selling_price"),
                        Tva.Load(record.GetFieldValue<uint>("tva_id")),
                        record.GetFieldValue<double>("promotion")));
            }
        }
    }
}
