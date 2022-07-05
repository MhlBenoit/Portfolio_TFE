namespace TFE.Tools
{
    //x <summary>
    //! Classe qui regroupe les erreurs possible au sein de l'application
    //x </summary>
    public class Error
    {
        //x <summary>
        //! A L L
        //x </summary>
        public static string Clear { get => ""; }
        public static string IsNullOrEmpty { get => "Ce champ ne peut être vide"; }
        public static string IsNullOrEmptyMultiple { get => "Ces champs ne peuvent être vide"; }
        public static string NoWhiteSpaceAuthorized { get => "Ce champ ne peut contenir d'espaces"; }
        public static string NoCharacterAuthorized { get => "Ce champ ne peut contenir de caractères interdits"; }
        public static string NoLetterAuthorized { get => "Ce champ ne peut contenir de lettres"; }
        public static string NoNumberAuthorized { get => "Ce champ ne peut contenir de chiffres"; }

        public static string Add { get => "L'ajout a bien été effectué"; }
        public static string Update { get => "La modification a bien été effectuée"; }
        public static string Delete { get => "La suppression a bien été effectuée"; }
        public static string CantDelete { get => "La suppression ne peut être effectuée"; }
        public static string CantDeleteWithReason(string reason) { return $"La suppression ne peut être effectuée, {reason}"; }

        public static string FieldFormatInvalid(string field, string end) { return $"{field} n'est pas valide. {end}"; }
        public static string FieldStringTooLong(string field, string grammar) { return $"{field} est trop long{grammar}"; }
        public static string FieldStringTooShort(string field, string grammar) { return $"{field} est trop court{grammar}"; }
        public static string FieldComboboxWithoutValue(string field, string grammar) { return $"{field} doit être sélectionn{grammar}"; }
        public static string FieldNumberTooLong(string field, string grammar) { return $"{field} ne peut être supérieur{grammar} à 0"; }
        public static string FieldNumberTooShort(string field, string grammar) { return $"{field} ne peut être inférieur{grammar} à 0"; }

        //x <summary>
        //! C O N N E X I O N 
        //x </summary>
        public static string DbDisconnected { get => "Le serveur n'est pas en ligne"; }
        public static string UserInvalid { get => "L'utilisateur n'exite pas"; }
        public static string UserNotActive { get => "L'utilisateur n'est pas actif"; }
        public static string PasswordInvalid { get => "Le mot de passe est incorrect"; }
        public static string PasswordCantBeTheSame{ get => "Le mot de passe ne peut être le même"; }
        public static string PasswordNeedToBeTheSame { get => "Le mot de passe n'est pas identique"; }
        public static string PasswordChanged { get => "Le mot de passe a été modifié"; }

        //x <summary>
        //! D I R E C T I O N
        //x </summary>
        public static string ResetPassword { get => "Le mot de passe de cet utilisateur a été réinitilisé"; }
        public static string ResetPasswordError { get => "Une erreur s'est produite, le mot de passe n'a pas été changé"; }
        public static string EmployeeExist { get => "L'employé existe déjà"; }
        public static string WrongWayAccounting { get => "La date de début doit être avant la date de fin"; }
        public static string LoginExist { get => "Ce nom d'utilisateur existe déjà"; }
        public static string MailExist { get => "Cette adresse email existe déjà"; }

        //x <summary>
        //! C U S T O M E R
        //x </summary>
        public static string CompleteAddress { get => "Une adresse doit être complète"; }
        public static string CustomerExist { get => "Le client existe déjà"; }
        public static string CityOrPostalcodeChanged { get => "Le code postal et la ville ne peuvent être modifiée sans que l'addresse soit changée également"; }

        //x <summary>
        //! A R T I C L E
        //x </summary>
        public static string BrandExist { get => "Cette marque existe déjà"; }
        public static string CategoryExist { get => "Cette catégorie existe déjà"; }
        public static string SubCategoryExist { get => "Cette sous-catégorie existe déjà"; }
        public static string StockTooBig { get => "Le stock n'en contient pas autant"; }
        public static string PriceTooLow { get => "Le prix d'achat ne peut être inférieur à 0"; }
        public static string QuantityInvalid { get => "La quantité ne peut être inférieure à 0"; }
        public static string PriceTooBig { get => "Le prix de vente ne peut être inférieur ou égal au prix d'achat"; }
        public static string PromotionInvalid { get => "La promotion doit être comprise entre 0 et 99"; }
        public static string PriceValid { get => "Les champs ne peuvent contenir que des chiffres"; }
        public static string SoloPriceValid { get => "Ce champ doit être un nombre"; }
        public static string PromoValid { get => "Nombre entre 0 et 99 requis"; }
        public static string ArticleExist { get => "Cet article existe déjà"; }
        public static string ExcelOpen { get => "Le fichier doit être fermé pour être importé"; }
        public static string ExcelFailed(int row) { return $"Une erreur est survenue à la ligne n° {row}"; }
        public static string ExcelValid { get => "L'importation est réussie"; }
        public static string ExcelNotSelected { get => "Un fichier Excel doit être sélectionné"; }

        //x <summary>
        //! S A L E   &   D E P O S I T
        //x </summary>
        public static string EanInvalid { get => "Le code barre est associé à aucun article"; }
        public static string CustomerNotFound { get => "Le client est introuvable"; }
        public static string CustomerNotValid { get => "La recherche n'est pas valide"; }
        public static string CustomerEmptySale { get => "Un client doit être sélectionné, même anonyme"; }
        public static string CustomerEmptyDeposit { get => "Un client doit être sélectionné"; }
        public static string BucketEmpty { get => "Le panier est vide"; }
        public static string CustomerCreate { get => "Le client a été créé"; }
        public static string PaymentSelect { get => "Un choix de paiement est requis"; }
        public static string PaymentToolow { get => "La somme donnée ne peut être inférieure au total"; }
        public static string ValidError { get => "Une erreur s'est produite"; }
        public static string QuantityTooLow { get => "Cet article n'est pas disponible en autant de quantitée"; }
        public static string ArticleNotDisponible { get => "Cet article n'est plus disponible"; }
        public static string RefundQuantityInvalid { get => "Quantité invalide"; }
        public static string RefundValid { get => "Remboursement effectué"; }
    }
}
