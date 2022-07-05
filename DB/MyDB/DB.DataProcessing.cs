using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.Types;

namespace _DB
{
    //x <summary>
    //! Fournit une connexion à un serveur MySQL afin d'y réaliser différentes opérations par le biais de requête SQL
    //x </summary>
    public partial class DB
    {
        //x <summary>
        //! Tableau vide d'objets servant d'arguments à une requête
        //x </summary>
        private static readonly object[] c_EmptyArgumentArray = new object[0];

        //x <summary>
        //! Définit les paramètres régionaux de langue anglaise
        //x </summary>
        private static readonly System.Globalization.CultureInfo c_EnglishCulture = System.Globalization.CultureInfo.GetCultureInfo("EN-US");

        //x <summary>
        //! Permet de formater "proprement" une requête paramétrée en y injectant les données spécifiées
        //x </summary>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Requête SQL dans laquelle les arguments y ont été injectés "proprement"</returns>
        public static string Format(string Query, params object[] Arguments)
        {
            if (string.IsNullOrWhiteSpace(Query)) return string.Empty;
            if (Arguments == null) Arguments = c_EmptyArgumentArray;
            return string.Format(Query.Trim(), Arguments.Select(Argument => FormatValue(Argument)).ToArray());
        }

        //x <summary>
        //! Formate si possible une valeur "non typée" en chaîne de caractère de manière à pouvoir l'injecter sans soucis au sein d'une requête MySQL
        //x </summary>
        //! <param name="Value">Valeur à formater</param>
        //! <returns>Chaîne représentant cette valeur une fois correctement formatée, si possible</returns>
        //! <exception cref="Exception">Le type "interne" de la valeur à formater n'est pas pris en charge par MySQL !</exception>
        public static string FormatValue(object Value)
        {
            if (Value == null)
            {
                return "NULL";
            }
            else if ((Value is sbyte) || (Value is byte) || (Value is short) || (Value is ushort) || (Value is int) || (Value is uint) || (Value is long) || (Value is ulong))
            {
                return Value.ToString();
            }
            else if (Value is float)
            {
                return ((float)Value).ToString(c_EnglishCulture);
            }
            else if (Value is double)
            {
                return ((double)Value).ToString(c_EnglishCulture);
            }
            else if (Value is decimal)
            {
                return ((decimal)Value).ToString(c_EnglishCulture);
            }
            else if (Value is MySqlDecimal)
            {
                MySqlDecimal MyValue = (MySqlDecimal)Value;
                if (MyValue.IsNull)
                {
                    return "NULL";
                }
                else
                {
                    return MyValue.Value.ToString(c_EnglishCulture);
                }
            }
            else if ((Value is char) || (Value is string) || (Value is StringBuilder))
            {
                string Text = (Value is string) ? (string)Value : Value.ToString();
                return string.Format("\"{0}\"", Text.Replace("\\", "\\\\").Replace("\"", "\\\""));
            }
            else if (Value is bool)
            {
                return (bool)Value ? "TRUE" : "FALSE";
            }
            else if (Value is DateTime)
            {
                return string.Format("\"{0}\"", ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else if (Value is MySqlDateTime)
            {
                MySqlDateTime MyValue = (MySqlDateTime)Value;
                if (MyValue.IsNull)
                {
                    return "NULL";
                }
                else if (!MyValue.IsValidDateTime)
                {
                    var Result = MyValue.ToString();
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                    return string.Format("\"{0}\"", Result);
                }
                else
                {
                    return string.Format("\"{0}\"", MyValue.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            else if (Value is SqlCode)
            {
                return Value.ToString();
            }
            else if (uint.TryParse(Value.ToString(), out uint result))
            {
                return result.ToString();
            }
            throw new Exception($"Erreur d'injection SQL : le type {Value.GetType().FullName} n'est pas pris en charge !");
        }

        //x <summary>
        //! Encapsule du code SQL afin qu'il puisse être injecté comme partie variable d'une requête MySQL
        //x </summary>
        public class SqlCode
        {
            //x <summary>
            //! Code SQL à encapsuler
            //x </summary>
            private string m_Query;

            //x <summary>
            //! Valeurs des parties variables du code SQL à encapsuler
            //x </summary>
            private object[] m_Arguments;

            //x <summary>
            //! Code SQL
            //x </summary>
            public string Query
            {
                get
                {
                    return m_Query;
                }
            }

            //x <summary>
            //! Valeurs des parties variables de ce code SQL
            //x </summary>
            public IEnumerable<object> Arguments
            {
                get
                {
                    return m_Arguments;
                }
            }

            //x <summary>
            //! Crée un objet d'encapsulation d'un code SQL
            //x </summary>
            //! <param name="Query">Code SQL à encapsuler</param>
            //! <param name="Arguments">Valeurs des parties variables du code SQL à encapsuler</param>
            //! <returns>Objet encapsulant le code SQL si possible, sinon null</returns>
            public static SqlCode Create(string Query, params object[] Arguments)
            {
                if (Query == null) return null;
                return new SqlCode(Query, Arguments);
            }

            //x <summary>
            //! Constructeur privé
            //x </summary>
            //! <param name="Query">Code SQL à encapsuler</param>
            //! <param name="Arguments">Valeurs des parties variables du code SQL à encapsuler</param>
            private SqlCode(string Query, object[] Arguments)
            {
                m_Query = Query;
                m_Arguments = Arguments;
            }

            //x <summary>
            //! Code SQL encapsulé et pour lequel les valeurs de ses parties variables y ont été injectées
            //x </summary>
            //! <returns>Chaîne correspondant au code SQL prêt à être injecté dans une requête MySQL</returns>
            public override string ToString()
            {
                return Format(m_Query, m_Arguments);
            }
        }

        //x <summary>
        //! Tente d'extraire une valeur de type DateTime à partir d'une valeur de type MySqlDateTime
        //x </summary>
        //! <param name="Source">Valeur de type MySqlDateTime à partir de laquelle opérer l'extraction</param>
        //! <param name="Result">Valeur de type DateTime extraite</param>
        //! <param name="DefaultResult">Valeur de type DateTime à retourner si l'extraction n'est pas réalisable</param>
        //! <returns>Vrai si l'extraction a pu se faire, sinon faux</returns>
        public static bool ToDateTime(MySqlDateTime Source, out DateTime Result, DateTime DefaultResult = default(DateTime))
        {
            if (Source.IsValidDateTime)
            {
                Result = Source.Value;
                return true;
            }
            else
            {
                Result = DefaultResult;
                return false;
            }
        }

        //x <summary>
        //! Tente d'extraire une valeur de type TimeSpan à partir d'une valeur de type MySqlDateTime
        //x </summary>
        //! <param name="Source">Valeur de type MySqlDateTime à partir de laquelle opérer l'extraction</param>
        //! <param name="Result">Valeur de type TimeSpan extraite</param>
        //! <param name="DefaultResult">Valeur de type TimeSpan à retourner si l'extraction n'est pas réalisable</param>
        //! <returns>Vrai si l'extraction a pu se faire, sinon faux</returns>
        public static bool ToTimeSpan(MySqlDateTime Source, out TimeSpan Result, TimeSpan DefaultResult = default(TimeSpan))
        {
            if (Source.IsValidDateTime)
            {
                Result = new TimeSpan(Source.Value.Hour, Source.Value.Minute, Source.Value.Second);
                return true;
            }
            else
            {
                Result = DefaultResult;
                return false;
            }
        }

        //x <summary>
        //! Tente de générer une valeur de type MySqlDateTime à partir d'une valeur de type DateTime
        //x </summary>
        //! <param name="Source">Valeur de type DateTime à partir de laquelle opérer la génération</param>
        //! <param name="Result">Valeur de type MySqlDateTime générée</param>
        //! <param name="DefaultResult">Valeur de type MySqlDateTime à retourner si la génération n'est pas réalisable</param>
        //! <returns>Vrai si la génération a pu se faire, sinon faux</returns>
        public static bool ToMySqlDateTime(DateTime Source, out MySqlDateTime Result, MySqlDateTime DefaultResult = default(MySqlDateTime))
        {
            Result = new MySqlDateTime(Source);
            if (Result.IsValidDateTime)
            {
                return true;
            }
            else
            {
                Result = DefaultResult;
                return false;
            }
        }

        //x <summary>
        //! Tente d'extraire une valeur decimal à partir d'une valeur de type MySqlDecimal
        //x </summary>
        //! <param name="Source">Valeur de type MySqlDecimal à partir de laquelle opérer l'extraction</param>
        //! <param name="Result">Valeur de type decimal extraite</param>
        //! <param name="DefaultResult">Valeur de type decimal à retourner si l'extraction n'est pas réalisable</param>
        //! <returns>Vrai si l'extraction a pu se faire, sinon faux</returns>
        public static bool ToDecimal(MySqlDecimal Source, out decimal Result, decimal DefaultResult = default(decimal))
        {
            if (!Source.IsNull)
            {
                Result = Source.Value;
                return true;
            }
            else
            {
                Result = DefaultResult;
                return false;
            }
        }
    }
}
