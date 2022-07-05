using System;
using System.Text.RegularExpressions;
using TFE.Model;

namespace TFE.Tools
{
    //x <summary>
    //! Classe regroupant les méthodes de vérification de données
    //x </summary>
    public class Verify
    {
        //x <summary>
        //! Vérifie si un texte est valide selon certaines conditions
        //x </summary>
        //! <param name="text">Texte à vérifier</param>
        //! <param name="field">Nom du champs à vérifier (pour afficher si erreur il y a)</param>
        //! <param name="min">Longueur minimum du texte</param>
        //! <param name="max">Longueur maximale du texte</param>
        //! <param name="canbeempty">Détermine si le texte peut être vide</param>
        //! <param name="withspace">Détermine si le texte peut contenir des espaces</param>
        //! <returns>Retourne un message vide s'il est valide, sinon un message d'erreur</returns>
        public static string _SimpleText(string text, string field, int min, int max, bool canbeempty, bool withspace)
        {
            if (canbeempty == false)
            {
                if (text.Trim() == "")
                    return Error.IsNullOrEmpty;
                else if (text.Trim().Length < min)
                    return Error.FieldStringTooShort(field, $" (min. {min} caractères)");
                else if (text.Trim().Length > max)
                    return Error.FieldStringTooLong(field, $" (max. {max} caractères)");
                else if (text.Trim().Contains(" ") && withspace == false)
                    return Error.NoWhiteSpaceAuthorized;
                else
                    return Error.Clear;
            }
            else
            {
                if (text.Trim().Length < min)
                    return Error.FieldStringTooShort(field, $" (min. {min} caractères)");
                else if (text.Trim().Length > max)
                    return Error.FieldStringTooLong(field, $" (max. {max} caractères)");
                else if (text.Trim().Contains(" ") && withspace == false)
                    return Error.NoWhiteSpaceAuthorized;
                else
                    return Error.Clear;
            }
        }

        //x <summary>
        //! Vérifie si une adresse mail est valide selon certaines conditions
        //x </summary>
        //! <param name="mail">Adresse mail à vérifier</param>
        //! <param name="field">Nom du champs à vérifier (pour afficher si erreur il y a)</param>
        //! <param name="min">Longueur minimum du texte</param>
        //! <param name="max">Longueur maximale du texte</param>
        //! <returns>Retourne un message vide s'il est valide, sinon un message d'erreur</returns>
        public static string _Mail(string mail, string field, int min, int max)
        {
            var test_mail = Regex.IsMatch(mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            if (mail.Trim() == "")
                return Error.IsNullOrEmpty;
            else if (mail.Trim().Length < min)
                return Error.FieldStringTooShort(field, $" (min. {min} caractères)");
            else if (mail.Trim().Length > max)
                return Error.FieldStringTooLong(field, $" (max. {max} caractères)");
            else if (mail.Trim().Contains(" "))
                return Error.NoWhiteSpaceAuthorized;
            else if (test_mail == false)
                return Error.FieldFormatInvalid(field, "");
            else
                return Error.Clear;
        }

        //x <summary>
        //! Vérifie si un rang est séléctionné ou pas
        //x </summary>
        public static string _Rank(Rank rank)
        {
            if (rank.Id == 0)
                return Error.IsNullOrEmpty;
            else
                return Error.Clear;
        }

        //x <summary>
        //! Vérifie si une marque est séléctionnée ou pas
        //x </summary>
        public static string _Brand(Brand brand)
        {
            if (brand.Id == 0)
                return Error.IsNullOrEmpty;
            else
                return Error.Clear;
        }

        //x <summary>
        //! Vérifie si une TVA est séléctionnée ou pas
        //x </summary>
        public static string _Tva(Tva tva)
        {
            if (tva.Id == 0)
                return Error.IsNullOrEmpty;
            else
                return Error.Clear;
        }

        //x <summary>
        //! Vérifie si une catégorie est séléctionnée ou pas
        //x </summary>
        public static string _Category(Category category)
        {
            if (category.Id == 0)
                return Error.IsNullOrEmpty;
            else
                return Error.Clear;
        }

        //x <summary>
        //! Vérifie si une date est valide ou pas
        //x </summary>
        public static string _Borndate(DateTime? datetime, string field)
        {
            if (datetime != null)
            {
                if (datetime > DateTime.Now)
                    return Error.FieldFormatInvalid(field, "");
                else
                    return Error.Clear;
            }
            else
                return Error.Clear;
        }

        //x <summary>
        //! Vérifie si un type de valeur est valide selon certaines conditions
        //x </summary>
        //! <param name="text">Texte à vérifier</param>
        //! <param name="field">Nom du champs à vérifier (pour afficher si erreur il y a)</param>
        //! <param name="min">Longueur minimum du texte</param>
        //! <param name="max">Longueur maximale du texte</param>
        //! <param name="type">Détermine le type de valeur</param>
        //! <returns>Retourne un message vide s'il est valide, sinon un message d'erreur</returns>
        public static string _Regex(string text, string field, int min, int max, string type)
        {
            if (type == "postalcode")
            {
                if (!Regex.IsMatch(text.ToString().Trim(), "^[0-9]*$"))
                    return Error.FieldFormatInvalid(field, "");
                else if (text.ToString().Trim().Length < min)
                    return Error.FieldStringTooShort(field, $" (min. {min} chiffres)");
                else if (text.ToString().Trim().Length > max)
                    return Error.FieldStringTooLong(field, $" (max. {max} chiffres)");
                else if (text.ToString().Trim().Contains(" "))
                    return Error.NoWhiteSpaceAuthorized;
                else
                    return Error.Clear;
            }
            else
                return Error.FieldFormatInvalid(field, "");
        }

        //x <summary>
        //! Vérifie si la conversion en décimal est possible
        //x </summary>
        public static string _Decimal(string s, out decimal d)
        {
            if (decimal.TryParse(s.Replace(".", ","), out decimal result))
            {
                d = result;
                return Error.Clear;
            }
            else
            {
                d = -1;
                return Error.SoloPriceValid;
            }
        }

        //x <summary>
        //! Vérifie si la conversion en double est possible
        //x </summary>
        public static string _Double(string s, out double d)
        {
            if (double.TryParse(s.Replace(".", ","), out double result))
            {
                d = result;
                return Error.Clear;
            }
            else
            {
                d = -1;
                return Error.SoloPriceValid;
            }
        }

        //x <summary>
        //! Vérifie si la conversion en int est possible
        //x </summary>
        public static string _Int(string s, out int i)
        {
            if (int.TryParse(s.Replace(".", ","), out int result))
            {
                i = result;
                return Error.Clear;
            }
            else
            {
                i = -1;
                return Error.SoloPriceValid;
            }
        }
    }
}
