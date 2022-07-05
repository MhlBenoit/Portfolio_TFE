using System;
using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit un article
    //x </summary>
    public class Article : Entity<Article>
    {
        //x <summary>
        //! Informations d'un article
        //x </summary>
        public string Name { get; set; }
        public string Ean_code { get; set; }
        public Brand Brand_id { get; set; }
        public Category Category_id { get; set; }
        public Sub_Category Sub_category_id { get; set; }
        public Price_Info Price_info_id { get; set; }
        public int? Quantity { get; set; }
        public bool Deposit { get; set; }
        public DateTime Date { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_article">Identifiant d'un article</param>
        //! <param name="name">Nom de l'article</param>
        //! <param name="ean_code">Code barre de l'article</param>
        //! <param name="brand_id">Marque de l'article</param>
        //! <param name="category_id">Catégorie de l'article</param>
        //! <param name="sub_category_id">Sous-catégorie de l'article</param>
        //! <param name="price_info_id">Informations de prix de l'article</param>
        //! <param name="quantity">Quantité de l'article</param>
        //! <param name="deposit">Détermine si l'article est un dépôt ou non</param>
        //! <param name="date">Date liée à l'article</param>
        public Article(uint id_article, string name, string ean_code, Brand brand_id, Category category_id, Sub_Category sub_category_id, Price_Info price_info_id, int? quantity, bool deposit, DateTime date)
            : base(id_article)
        {
            DefinirReferenceEntity(this);
            Name = name;
            Ean_code = ean_code;
            Brand_id = brand_id;
            Category_id = category_id;
            Sub_category_id = sub_category_id;
            Price_info_id = price_info_id;
            Quantity = quantity;
            Deposit = deposit;
            Date = date;
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
        //! Permet de tester la validité du champ nom pour un article
        //x </summary>
        //! <param name="name">Nom d'article à tester</param>
        //! <param name="nameFinal">Nom d'article finalement retenu si il est valide</param>
        //! <para>Ce nom d'article a subi une transformation de type Trim</para>
        //! <returns>Vrai si ce nom d'article est valide, sinon faux</returns>
        public static bool TesterValidite_Name(string name, out string nameFinal)
        {
            nameFinal = null;
            if (name == null) return false;
            name = name.Trim();
            if ((name.Length < 2) || (name.Length > 100)) return false;
            nameFinal = name;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ code barre pour un article
        //x </summary>
        //! <param name="ean_code">Code barre à tester</param>
        //! <param name="ean_codeFinal">Code barre finalement retenu si il est valide</param>
        //! <para>Ce code barre a subi une transformation de type Trim</para>
        //! <returns>Vrai si ce code barre est valide, sinon faux</returns>
        public static bool TesterValidite_Ean_Code(string ean_code, out string ean_codeFinal)
        {
            ean_codeFinal = null;
            if (ean_code == null) return false;
            ean_code = ean_code.Trim();
            if ((ean_code.Length < 2) || (ean_code.Length > 50)) return false;
            ean_codeFinal = ean_code;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ marque pour un article
        //x </summary>
        //! <param name="brand_id">Marque à tester</param>
        //! <param name="brand_idFinal">Marque finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette marque est valide, sinon faux</returns>
        public static bool TesterValidite_Brand_id(Brand brand_id, out Brand brand_idFinal)
        {
            brand_idFinal = null;
            if (brand_id != null)
            {
                brand_idFinal = brand_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ catégorie pour un article
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
        //! Permet de tester la validité du champ sous-catégorie pour un article
        //x </summary>
        //! <param name="sub_category_id">Sous-catégorie à tester</param>
        //! <param name="sub_category_idFinal">Sous-catégorie finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette sous-catégorie est valide, sinon faux</returns>
        public static bool TesterValidite_Sub_Category_id(Sub_Category sub_category_id, out Sub_Category sub_category_idFinal)
        {
            sub_category_idFinal = null;
            if (sub_category_id != null)
                sub_category_idFinal = sub_category_id;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ informations de prix pour un article
        //x </summary>
        //! <param name="price_info_id">Informations de prix à tester</param>
        //! <param name="price_info_idFinal">Informations de prix finalement retenue si elle est valide</param>
        //! <returns>Vrai si cette informations de prix est valide, sinon faux</returns>
        public static bool TesterValidite_Price_Info_id(Price_Info price_info_id, out Price_Info price_info_idFinal)
        {
            price_info_idFinal = null;
            if (price_info_id != null)
            {
                price_info_idFinal = price_info_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ quantitée pour un article
        //x </summary>
        //! <param name="quantity">Quantitée à tester</param>
        //! <param name="quantityFinal">Quantitée finalement retenue si il est valide</param>
        //! <returns>Vrai si cet identifiant d'une personne est valide, sinon faux</returns>
        public static bool TesterValidite_Quantity(int quantity, out int quantityFinal)
        {
            quantityFinal = 0;
            if (quantity >= 0)
            {
                quantityFinal = quantity;
                return true;
            }
            else
                return false;
        }
        #endregion

        //x <summary>
        //! Nom de table utile à la création d'une entité de type article
        //x </summary>
        public override string NameTable => "article";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type article
        //x </summary>
        public override string ListFields => "id_article, name, ean_code, brand_id, category_id, sub_category_id, price_info_id, quantity, deposit, date";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type article au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("name", Name);
                yield return FieldValue.Associate("ean_code", Ean_code);
                yield return FieldValue.Associate("brand_id", Brand_id);
                yield return FieldValue.Associate("category_id", Category_id);
                yield return FieldValue.Associate("sub_category_id", Sub_category_id);
                yield return FieldValue.Associate("price_info_id", Price_info_id);
                yield return FieldValue.Associate("quantity", Quantity);
                yield return FieldValue.Associate("deposit", Deposit);
                yield return FieldValue.Associate("date", Date);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Article s_ArticleDeReference = new Article(0, string.Empty, string.Empty, Brand.ReferenceEntity,
                                                                                                 Category.ReferenceEntity,
                                                                                                 Sub_Category.ReferenceEntity,
                                                                                                 Price_Info.ReferenceEntity,
                                                                                                 null, false, DateTime.Now);

        //x <summary>
        //! Permet de mettre à jour une entité de type article
        //x </summary>
        //! <param name="article">Référence d'objet à mettre à jour</param>
        //! <param name="name">Nom de l'article</param>
        //! <param name="ean_code">Code barre de l'article</param>
        //! <param name="brand_id">Marque de l'article</param>
        //! <param name="category_id">Catégorie de l'article</param>
        //! <param name="sub_category_id">Sous-catégorie de l'article</param>
        //! <param name="price_info_id">Informations de prix de l'article</param>
        //! <param name="quantity">Quantité de l'article</param>
        //! <param name="deposit">Détermine si l'article est un dépôt ou non</param>
        //! <param name="date">Date liée à l'article</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update(ref Article article, string name, string ean_code, Brand brand_id, Category category_id, Sub_Category sub_category_id, Price_Info price_info_id, int quantity, bool deposit, DateTime date)
        {
            if ((article == null) || (article.Id == 0)) return false;
            if (!TesterValidite_Name(name, out name)) return false;
            if (!TesterValidite_Ean_Code(ean_code, out ean_code)) return false;
            if (!TesterValidite_Brand_id(brand_id, out brand_id)) return false;
            if (!TesterValidite_Category_id(category_id, out category_id)) return false;
            if (!TesterValidite_Sub_Category_id(sub_category_id, out sub_category_id)) return false;
            if (!TesterValidite_Price_Info_id(price_info_id, out price_info_id)) return false;
            if (!TesterValidite_Quantity(quantity, out quantity)) return false;

            article.Name = name;
            article.Ean_code = ean_code;
            article.Brand_id = brand_id;
            article.Category_id = category_id;
            article.Sub_category_id = sub_category_id;
            article.Price_info_id = price_info_id;
            article.Quantity = quantity;
            article.Deposit = deposit;
            article.Date = date;

            if (article.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type article
        //x </summary>
        //! <param name="name">Nom de l'article</param>
        //! <param name="ean_code">Code barre de l'article</param>
        //! <param name="brand_id">Marque de l'article</param>
        //! <param name="category_id">Catégorie de l'article</param>
        //! <param name="sub_category_id">Sous-catégorie de l'article</param>
        //! <param name="price_info_id">Informations de prix de l'article</param>
        //! <param name="quantity">Quantité de l'article</param>
        //! <param name="deposit">Détermine si l'article est un dépôt ou non</param>
        //! <param name="date">Date liée à l'article</param>
        //! <returns>Nouvelle personne si possible, sinon null</returns>
        public static Article Create(string name, string ean_code, Brand brand_id, Category category_id, Sub_Category sub_category_id, Price_Info price_info_id, int quantity, bool deposit, DateTime date)
        {
            if (!TesterValidite_Name(name, out name)) return null;
            if (!TesterValidite_Ean_Code(ean_code, out ean_code)) return null;
            if (!TesterValidite_Brand_id(brand_id, out brand_id)) return null;
            if (!TesterValidite_Category_id(category_id, out category_id)) return null;
            if (!TesterValidite_Sub_Category_id(sub_category_id, out sub_category_id)) return null;
            if (!TesterValidite_Price_Info_id(price_info_id, out price_info_id)) return null;
            if (!TesterValidite_Quantity(quantity, out quantity)) return null;
            return new Article(0, name, ean_code, brand_id, category_id, sub_category_id, price_info_id, quantity, deposit, date);
        }

        //x <summary>
        //! Permet de charger si possible l'article correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_article">Identifiant de l'article à charger</param>
        //! <returns>Article correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Article Load(uint id_article)
        {
            var record = Entity<Article>.Load(id_article);
            if (record == null) return null;
            return new Article(id_article, record.GetFieldValue<string>("name")
                                           , record.GetFieldValue<string>("ean_code")
                                           , Brand.Load(record.GetFieldValue<uint>("brand_id"))
                                           , Category.Load(record.GetFieldValue<uint>("category_id"))
                                           , Sub_Category.Load(record.GetFieldValue<uint>("sub_category_id"))
                                           , Price_Info.Load(record.GetFieldValue<uint>("price_info_id"))
                                           , record.GetFieldValue<int>("quantity")
                                           , record.GetFieldValue<bool>("deposit")
                                           , record.GetFieldValue<DateTime>("date"));
        }

        //x <summary>
        //! Permet de mettre à jour le stock d'un article 
        //x </summary>
        //! <param name="article">Référence d'objet à mettre à jour</param>
        //! <param name="quantity">Quantitée à rajouter au stock</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update_Add_Stock(Article article, int quantity)
        {
            var record = Entity<Article>.Load(article.Id);
            if (record == null) return false;

            article.Quantity += quantity;

            if (article.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet de mettre à jour le stock d'un article 
        //x </summary>
        //! <param name="article">Référence d'objet à mettre à jour</param>
        //! <param name="quantity">Quantitée à retirer du stock</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update_Remove_Stock(Article article, int quantity)
        {
            var record = Entity<Article>.Load(article.Id);
            if (record == null) return false;
            if (article.Quantity < quantity) return false;

            article.Quantity -= quantity;

            if (article.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Permet de modifier les informations de prix d'un article
        //x </summary>
        //! <param name="article">Référence d'objet à mettre à jour</param>
        //! <param name="buying_price">Prix d'achat de l'article</param>
        //! <param name="selling_price">Prix de vente de l'article</param>
        //! <param name="tva">TVA de l'article</param>
        //! <param name="promotion">Promotion sur l'article</param>
        //! <returns>True si la modification est passée, sinon false</returns>
        public static bool Update_Price(Article article, decimal buying_price, decimal selling_price, Tva tva, int promotion)
        {
            Price_Info record = Price_Info.Load(article.Price_info_id.Id);
            if (record == null) return false;
            if (buying_price < 0) return false;
            if (selling_price < 0 || selling_price < buying_price) return false;
            if (tva.Value < 1 || tva.Value > 4) return false;
            if (promotion < 0 || promotion > 100) return false;

            record.Buying_price = buying_price;
            record.Selling_price = selling_price;
            record.Tva_id = tva;
            record.Promotion = promotion;

            if (record.Save())
                return true;
            else
                return false;
        }

        //x <summary>
        //! Enumère tous les articles 
        //x </summary>
        public static IEnumerable<Article> Enumeration
        {
            get
            {
                return Enumerer((record) => new Article(
                                            record.GetFieldValue<uint>("id_article"),
                                            record.GetFieldValue<string>("name"),
                                            record.GetFieldValue<string>("ean_code"),
                                            Brand.Load(record.GetFieldValue<uint>("brand_id")),
                                            Category.Load(record.GetFieldValue<uint>("category_id")),
                                            Sub_Category.Load(record.GetFieldValue<uint>("sub_category_id")),
                                            Price_Info.Load(record.GetFieldValue<uint>("price_info_id")),
                                            record.GetFieldValue<int>("quantity"),
                                            record.GetFieldValue<bool>("deposit"),
                                            record.GetFieldValue<DateTime>("date")));
            }
        }
    }

}
