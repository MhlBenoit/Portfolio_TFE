using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit un dépôt
    //x </summary>
    public class Purchase : Entity<Purchase>
    {
        //x <summary>
        //! Informations d'un dépôt
        //x </summary>
        public Transaction Transaction_id { get; set; }
        public Article Article_id { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_purchase">Identifiant d'un dépôt</param>
        //! <param name="transaction_id">Transaction associée au dépôt</param>
        //! <param name="article_id">Article du dépôt</param>
        //! <param name="quantity">Quantité de l'article</param>
        //! <param name="total">Somme totale quantité x prix d'achat de l'article</param>
        public Purchase(uint id_purchase, Transaction transaction_id, Article article_id, int quantity, decimal total)
            : base(id_purchase)
        {
            DefinirReferenceEntity(this);
            Transaction_id = transaction_id;
            Article_id = article_id;
            Quantity = quantity;
            Total = total;
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
        //! Permet de tester la validité du champ transaction pour un dépôt
        //x </summary>
        //! <param name="transaction_id">Transaction à tester</param>
        //! <param name="transaction_idFinal">Transaction finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette transaction est valide, sinon faux</returns>
        public static bool TesterValidite_Transaction_id(Transaction transaction_id, out Transaction transaction_idFinal)
        {
            transaction_idFinal = null;
            if (transaction_id != null)
            {
                transaction_idFinal = transaction_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ article pour un dépôt
        //x </summary>
        //! <param name="article_id">Article à tester</param>
        //! <param name="article_idFinal">Article finalement retenue si il est valide</param>
        //! <returns>Vrai si cet article est valide, sinon faux</returns>
        public static bool TesterValidite_Article_id(Article article_id, out Article article_idFinal)
        {
            article_idFinal = null;
            if (article_id != null)
            {
                article_idFinal = article_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ quantité pour un dépôt
        //x </summary>
        //! <param name="quantity">Quantité à tester</param>
        //! <param name="quantityFinal">Quantitée finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette quantitée est valide, sinon faux</returns>
        public static bool TesterValidite_Quantity(int quantity, out int quantityFinal)
        {
            quantityFinal = 0;
            if (quantity != 0)
            {
                quantityFinal = quantity;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ total pour un dépôt
        //x </summary>
        //! <param name="total">Total à tester</param>
        //! <param name="totalFinal">Total finalement retenu si il est valide</param>
        //! <returns>Vrai si ce total est valide, sinon faux</returns>
        public static bool TesterValidite_Total(decimal total, out decimal totalFinal)
        {
            totalFinal = 0;
            if (total < 0) return false;
            totalFinal = total;
            return true;
        }
        #endregion

        //x <summary>
        //! Nom de table utile à la création d'une entité de type dépôt
        //x </summary>
        public override string NameTable => "purchase";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type dépôt
        //x </summary>
        public override string ListFields => "id_purchase, transaction_id, article_id, quantity, total";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type dépôt au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("transaction_id", Transaction_id);
                yield return FieldValue.Associate("article_id", Article_id);
                yield return FieldValue.Associate("quantity", Quantity);
                yield return FieldValue.Associate("total", Total);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Purchase s_PurchaseDeReference = new Purchase(0, Transaction.ReferenceEntity, Article.ReferenceEntity, 0, 0);

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type dépôt
        //x </summary>
        //! <param name="transaction_id">Transaction associée au dépôt</param>
        //! <param name="article_id">Article du dépôt</param>
        //! <param name="quantity">Quantité de l'article</param>
        //! <param name="total">Somme totale quantité x prix de vente de l'article</param>
        //! <returns>Nouvell dépôt si possible, sinon null</returns>
        public static Purchase Create(Transaction transaction_id, Article article_id, int quantity, decimal total)
        {
            if (!TesterValidite_Transaction_id(transaction_id, out transaction_id)) return null;
            if (!TesterValidite_Article_id(article_id, out article_id)) return null;
            if (!TesterValidite_Quantity(quantity, out quantity)) return null;
            if (!TesterValidite_Total(total, out total)) return null;
            return new Purchase(0, transaction_id, article_id, quantity, total);
        }

        //x <summary>
        //! Permet de charger si possible le dépôt correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_purchase">Identifiant d'un dépôt à charger</param>
        //! <returns>Dépôt correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Purchase Load(uint id_purchase)
        {
            var record = Entity<Purchase>.Load(id_purchase);
            if (record == null) return null;
            return new Purchase(id_purchase, Transaction.Load(record.GetFieldValue<uint>("transaction_id"))
                                           , Article.Load(record.GetFieldValue<uint>("article_id"))
                                           , record.GetFieldValue<int>("quantity")
                                           , record.GetFieldValue<decimal>("total"));
        }

        //x <summary>
        //! Enumère tous les dépôts
        //x </summary>
        public static IEnumerable<Purchase> Enumeration
        {
            get
            {
                return Enumerer((record) => new Purchase(
                        record.GetFieldValue<uint>("id_purchase"),
                        Transaction.Load(record.GetFieldValue<uint>("transaction_id")),
                        Article.Load(record.GetFieldValue<uint>("article_id")),
                        record.GetFieldValue<int>("quantity"),
                        record.GetFieldValue<decimal>("total")));
            }
        }
    }
}
