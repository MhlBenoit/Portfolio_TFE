using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Debug = System.Diagnostics.Debug;

namespace _DB
{
    //x <summary>
    //! Fournit une connexion à un serveur MySQL afin d'y réaliser différentes opérations par le biais de requête SQL
    //x </summary>
    public partial class DB
    {
        //x <summary>
        //! Indique à un indexeur de champ que l'on veut récupérer le nom du champ et pas sa valeur
        //x </summary>
        public static IFieldName FieldName { get; } = new _FieldName();

        //x <summary>
        //! Interface de l'indicateur que l'on peut fournir à un indexeur de champ quand on veut récupérer le nom du champ et pas sa valeur
        //x </summary>
        public interface IFieldName { }

        //x <summary>
        //! Implémentation privée de l'interface de l'indicateur que l'on peut fournir à un indexeur de champ quand on veut récupérer le nom du champ et pas sa valeur
        //x </summary>
        private class _FieldName : IFieldName { }

        //x <summary>
        //! Permet de retrouver les données d'un enregistrement
        //x </summary>
        public interface IRecord
        {
            //x <summary>
            //! Fournit le nombre de champs existants au sein de cet enregistrement
            //x </summary>
            int FieldCount { get; }

            //x <summary>
            //! Indexeur permettant de récupérer le nom du champ pour l'indice spécifié
            //x </summary>
            //! <param name="FieldName">Indicateur précisant que l'on veut récupérer le nom de champ et pas sa valeur</param>
            //! <param name="Index">Indice du champ</param>
            //! <returns>Nom du champ si l'indice spécifié est valide, sinon null</returns>
            string this[IFieldName FieldName, int Index] { get; }

            //x <summary>
            //! Indexeur permettant de récupérer la valeur non typée du champ pour l'indice spécifié
            //x </summary>
            //! <param name="Index">Indice du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur non typée du champ si l'indice spécifié est valide, sinon la valeur par défaut</returns>
            object this[int Index, object DefaultValue = null] { get; }

            //x <summary>
            //! Indexeur permettant de récupérer la valeur non typée du champ pour le nom spécifié
            //x </summary>
            //! <param name="FieldName">Nom du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur non typée du champ si le nom spécifié est valide, sinon la valeur par défaut</returns>
            object this[string FieldName, object DefaultValue = null] { get; }

            //x <summary>
            //! Retourne le nom du champ pour l'indice spécifié
            //x </summary>
            //! <param name="Index">Indice du champ</param>
            //! <returns>Nom du champ si l'indice spécifié est valide, sinon null</returns>
            string GetFieldName(int Index);

            //x <summary>
            //! Retourne la valeur non typée du champ pour l'indice spécifié
            //x </summary>
            //! <param name="Index">Indice du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur non typée du champ si l'indice spécifié est valide, sinon la valeur par défaut</returns>
            object GetFieldValue(int Index, object DefaultValue = null);

            //x <summary>
            //! Retourne la valeur typée du champ pour l'indice spécifié
            //x </summary>
            //! <typeparam name="T">Type de valeur attendue</typeparam>
            //! <param name="Index">Indice du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur typée du champ si l'indice spécifié est valide et si cette valeur est du type attendu, sinon la valeur par défaut</returns>
            T GetFieldValue<T>(int Index, T DefaultValue = default(T));

            //x <summary>
            //! Retourne la valeur non typée du champ pour le nom spécifié
            //x </summary>
            //! <param name="FieldName">Nom du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur non typée du champ si le nom spécifié est valide, sinon la valeur par défaut</returns>
            object GetFieldValue(string FieldName, object DefaultValue = null);

            //x <summary>
            //! Retourne la valeur typée du champ pour l'indice spécifié
            //x </summary>
            //! <typeparam name="T">Type de valeur attendue</typeparam>
            //! <param name="FieldName">Nom du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur typée du champ si le nom spécifié est valide et si cette valeur est du type attendu, sinon la valeur par défaut</returns>
            T GetFieldValue<T>(string FieldName, T DefaultValue = default(T));
        }

        //x <summary>
        //! Implémentation de l'interface permettant de récupérer les données d'un enregistrement
        //x </summary>
        private class Record : IRecord
        {
            //x <summary>
            //! Enregistrement sans aucun champ
            //! <para>Sert à représenter l'absence d'enregistrement résultant d'une requête SQL de consultation</para>
            //x </summary>
            public static IRecord Empty { get; } = new Record(null);

            //x <summary>
            //! Tableau des noms de champs
            //x </summary>
            private string[] m_FieldNames;

            //x <summary>
            //! Tableau des valeurs de champs
            //x </summary>
            private object[] m_FieldValues;

            //x <summary>
            //! Dictionnaire associant nom et valeur de chaque champ
            //x </summary>
            private Dictionary<string, object> m_Fields;

            //x <summary>
            //! Fournit le nombre de champs existants au sein de cet enregistrement
            //x </summary>
            public int FieldCount
            {
                get
                {
                    return m_FieldNames.Length;
                }
            }

            //x <summary>
            //! Indexeur permettant de récupérer le nom du champ pour l'indice spécifié
            //x </summary>
            //! <param name="FieldName">Indicateur précisant que l'on veut récupérer le nom de champ et pas sa valeur</param>
            //! <param name="Index">Indice du champ</param>
            //! <returns>Nom du champ si l'indice spécifié est valide, sinon null</returns>
            public object this[string FieldName, object DefaultValue = null]
            {
                get
                {
                    return GetFieldValue(FieldName, DefaultValue);
                }
            }

            //x <summary>
            //! Indexeur permettant de récupérer la valeur non typée du champ pour l'indice spécifié
            //x </summary>
            //! <param name="Index">Indice du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur non typée du champ si l'indice spécifié est valide, sinon la valeur par défaut</returns>
            public object this[int Index, object DefaultValue = null]
            {
                get
                {
                    return GetFieldValue(Index, DefaultValue);
                }
            }

            //x <summary>
            //! Indexeur permettant de récupérer la valeur non typée du champ pour le nom spécifié
            //x </summary>
            //! <param name="FieldName">Nom du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur non typée du champ si le nom spécifié est valide, sinon la valeur par défaut</returns>
            public string this[IFieldName FieldName, int Index]
            {
                get
                {
                    return GetFieldName(Index);
                }
            }

            //x <summary>
            //! Retourne le nom du champ pour l'indice spécifié
            //x </summary>
            //! <param name="Index">Indice du champ</param>
            //! <returns>Nom du champ si l'indice spécifié est valide, sinon null</returns>
            public string GetFieldName(int Index)
            {
                return ((Index >= 0) && (Index < m_FieldNames.Length)) ? m_FieldNames[Index] : null;
            }

            //x <summary>
            //! Retourne la valeur non typée du champ pour l'indice spécifié
            //x </summary>
            //! <param name="Index">Indice du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur non typée du champ si l'indice spécifié est valide, sinon la valeur par défaut</returns>
            public object GetFieldValue(int Index, object DefaultValue = null)
            {
                return ((Index >= 0) && (Index < m_FieldValues.Length)) ? m_FieldValues[Index] : DefaultValue;
            }

            //x <summary>
            //! Retourne la valeur typée du champ pour l'indice spécifié
            //x </summary>
            //! <typeparam name="T">Type de valeur attendue</typeparam>
            //! <param name="Index">Indice du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur typée du champ si l'indice spécifié est valide et si cette valeur est du type attendu, sinon la valeur par défaut</returns>
            public T GetFieldValue<T>(int Index, T DefaultValue = default(T))
            {
                if ((Index < 0) || (Index >= m_FieldValues.Length)) return DefaultValue;
                object Result = m_FieldValues[Index];
                if (typeof(T).Equals(typeof(TimeSpan)))
                {
                    TimeSpan FinalResult;
                    if (Result is DateTime) return (T)((object)new TimeSpan(((DateTime)Result).Hour, ((DateTime)Result).Minute, ((DateTime)Result).Second));
                    if (Result is TimeSpan) return (T)Result;
                    return (Result is MySql.Data.Types.MySqlDateTime)
                        && ToTimeSpan((MySql.Data.Types.MySqlDateTime)Result, out FinalResult, (TimeSpan)(object)DefaultValue)
                        ? (T)(object)FinalResult : DefaultValue;
                }
                else if (typeof(T).Equals(typeof(DateTime)))
                {
                    DateTime FinalResult;
                    if (Result is DateTime) return (T)Result;
                    if (Result is TimeSpan) return (T)((object)new DateTime(1, 1, 1, 0, 0, 0).AddSeconds(((TimeSpan)Result).TotalSeconds));
                    return (Result is MySql.Data.Types.MySqlDateTime)
                        && ToDateTime((MySql.Data.Types.MySqlDateTime)Result, out FinalResult, (DateTime)(object)DefaultValue)
                        ? (T)(object)FinalResult : DefaultValue;
                }
                else if (typeof(T).Equals(typeof(decimal)))
                {
                    decimal FinalResult;
                    if (Result is decimal) return (T)Result;
                    return (Result is MySql.Data.Types.MySqlDecimal)
                        && ToDecimal((MySql.Data.Types.MySqlDecimal)Result, out FinalResult, (decimal)(object)DefaultValue)
                        ? (T)(object)FinalResult : DefaultValue;
                }
                else
                {
                    return (Result is T) ? (T)Result : DefaultValue;
                }
            }

            //x <summary>
            //! Retourne la valeur non typée du champ pour le nom spécifié
            //x </summary>
            //! <param name="FieldName">Nom du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur non typée du champ si le nom spécifié est valide, sinon la valeur par défaut</returns>
            public object GetFieldValue(string FieldName, object DefaultValue = null)
            {
                object Result;
                return m_Fields.TryGetValue(FieldName, out Result) ? Result : DefaultValue;
            }

            //x <summary>
            //! Retourne la valeur typée du champ pour l'indice spécifié
            //x </summary>
            //! <typeparam name="T">Type de valeur attendue</typeparam>
            //! <param name="FieldName">Nom du champ</param>
            //! <param name="DefaultValue">Valeur par défaut à retourner</param>
            //! <returns>Valeur typée du champ si le nom spécifié est valide et si cette valeur est du type attendu, sinon la valeur par défaut</returns>
            public T GetFieldValue<T>(string FieldName, T DefaultValue = default(T))
            {
                object Result;
                if (!m_Fields.TryGetValue(FieldName, out Result)) return DefaultValue;
                if (typeof(T).Equals(typeof(TimeSpan)))
                {
                    TimeSpan FinalResult;
                    if (Result is DateTime) return (T)((object)new TimeSpan(((DateTime)Result).Hour, ((DateTime)Result).Minute, ((DateTime)Result).Second));
                    if (Result is TimeSpan) return (T)Result;
                    return (Result is MySql.Data.Types.MySqlDateTime)
                        && ToTimeSpan((MySql.Data.Types.MySqlDateTime)Result, out FinalResult, (TimeSpan)(object)DefaultValue)
                        ? (T)(object)FinalResult : DefaultValue;
                }
                else if (typeof(T).Equals(typeof(DateTime)))
                {
                    DateTime FinalResult;
                    if (Result is DateTime) return (T)Result;
                    if (Result is TimeSpan) return (T)((object)new DateTime(1, 1, 1, 0, 0, 0).AddSeconds(((TimeSpan)Result).TotalSeconds));
                    return (Result is MySql.Data.Types.MySqlDateTime)
                        && ToDateTime((MySql.Data.Types.MySqlDateTime)Result, out FinalResult, (DateTime)(object)DefaultValue)
                        ? (T)(object)FinalResult : DefaultValue;
                }
                else if (typeof(T).Equals(typeof(decimal)))
                {
                    decimal FinalResult;
                    if (Result is decimal) return (T)Result;
                    return (Result is MySql.Data.Types.MySqlDecimal)
                        && ToDecimal((MySql.Data.Types.MySqlDecimal)Result, out FinalResult, (decimal)(object)DefaultValue)
                        ? (T)(object)FinalResult : DefaultValue;
                }
                else
                {
                    return (Result is T) ? (T)Result : DefaultValue;
                }
            }

            //x <summary>
            //! Constructeur qui initialise les données d'enregistrement à partir d'un objet MySqlDataReader
            //x </summary>
            //! <param name="Reader">Objet de récupération des données d'un enregistrement résultant d'une requête SQL de consultation</param>
            public Record(MySqlDataReader Reader)
            {
                m_Fields = new Dictionary<string, object>();
                m_FieldNames = new string[(Reader != null) ? Reader.FieldCount : 0];
                for (int i = 0; i < m_FieldNames.Length; i++)
                {
                    m_FieldNames[i] = Reader.GetName(i);
                    m_Fields.Add(m_FieldNames[i], null);
                }
                m_FieldValues = new object[m_FieldNames.Length];
                Set(Reader);
            }

            //x <summary>
            //! Met à jour les données d'enregistrement à partir d'un objet MySqlDataReader
            //x </summary>
            //! <param name="Reader">Objet de récupération des données d'un enregistrement résultant d'une requête SQL de consultation</param>
            //! <returns>Vrai si l'opération a réussi, sinon faux</returns>
            public virtual bool Set(MySqlDataReader Reader)
            {
                if (Reader == null) return false;
                try
                {
                    Reader.GetValues(m_FieldValues);
                    for (int i = 0; i < m_FieldValues.Length; i++)
                    {
                        m_Fields[m_FieldNames[i]] = m_FieldValues[i];
                    }
                    return true;
                }
                catch (Exception Error)
                {
                    Debug.WriteLine($"\nDB.Record.Set() a échoué !\n{Error.Message}");
                    return false;
                }
            }

            //x <summary>
            //! Retourne une représentation de cet enregistrement sous forme d'une chaîne de caractères
            //x </summary>
            //! <returns>Chaîne présentant les données de cet enregistrement</returns>
            public override string ToString()
            {
                return string.Format("{{ {0}} }}", string.Join(" ; ", m_FieldNames.Select((Name, Index) => string.Format("[{0}] {1} : {2}", Index, Name, m_FieldValues[Index]))));
            }
        }

        //x <summary>
        //! Décrit un état possible pour un objet IRecordResult
        //x </summary>
        public enum RecordState
        {
            //x <summary>
            //! La tentative de consultation d'enregistrement a échoué
            //! <para>Suite à une erreur de requête SQL ou à un problème de connexion au serveur MySQL !</para>
            //x </summary>
            Failure,
            //x <summary>
            //! La tentative de consultation d'enregistrement a réussi, mais n'a produit aucun enregistrement
            //x </summary>
            None,
            //x <summary>
            //! La tentative de consultation d'enregistrement a réussi et a produit un ou plusieurs enregistrements
            //x </summary>
            Success
        }

        //x <summary>
        //! Permet de consulter un résultat d'exécution d'une requête de consultation, et également, si il correspond à un succès, de retrouver les données d'un enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //! <para>Ce type d'informations est retourné par la méthode GetRecord afin d'en décrire la réussite ou l'échec en lui adjoignant des informations complémentaires sur un enregistrement récupéré</para>
        //x </summary>
        public interface IRecordResult : IRecord
        {
            //x <summary>
            //! Indique si ce résultat fait suite à une réussite d'exécution d'une requête de consultation
            //x </summary>
            bool IsSuccess { get; }

            //x <summary>
            //! Indique si ce résultat permet de consulter les données d'un enregistrement récupéré avec succès
            //x </summary>
            bool Exists { get; }

            //x <summary>
            //! Précise l'état de ce résultat de consultation d'enregistrement
            //x </summary>
            RecordState State { get; }

            //x <summary>
            //! Indice de l'enregistrement actuellement récupété par la requête de consultation exécutée
            //! <para>Toujours égal à -1 en cas d'échec ou d'absence d'enregistrement</para>
            //x </summary>
            int RecordIndex { get; }
        }

        //x <summary>
        //! Implémentation de l'interface qui permet de consulter un résultat d'exécution d'une requête de consultation, et également, si il correspond à un succès, de retrouver les données d'un enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //! <para>Ce type d'informations est retourné par la méthode GetRecord afin d'en décrire la réussite ou l'échec en lui adjoignant des informations complémentaires sur un enregistrement récupéré</para>
        //x </summary>
        private class RecordResult : Record, IRecordResult
        {
            //x <summary>
            //! Résultat de consultation d'enregistrement signalant un échec
            //x </summary>
            public static IRecordResult Failure { get; } = new RecordResult();

            //x <summary>
            //! Résultat de consultation d'enregistrement signalant une réussite de son exécution, mais sans avoir produit le moindre enregistrement
            //x </summary>
            public static IRecordResult None { get; } = new RecordResult() { State = RecordState.None };

            //x <summary>
            //! Indique si ce résultat fait suite à une réussite d'exécution d'une requête de consultation
            //x </summary>
            public bool IsSuccess { get { return (this.State != RecordState.Failure); } }

            //x <summary>
            //! Indique si ce résultat permet de consulter les données d'un enregistrement récupéré avec succès
            //x </summary>
            public bool Exists { get; private set; }

            //x <summary>
            //! Précise l'état de ce résultat de consultation d'enregistrement
            //x </summary>
            public RecordState State { get; private set; }

            //x <summary>
            //! Indice de l'enregistrement actuellement récupété par la requête de consultation exécutée
            //! <para>Toujours égal à -1 en cas d'échec</para>
            //x </summary>
            public int RecordIndex { get; private set; }

            //x <summary>
            //! Constructeur privé par défaut, utilisé pour signaler un échec de consultation
            //x </summary>
            private RecordResult()
                : base(null)
            {
                this.Exists = false;
                this.RecordIndex = -1;
                this.State = RecordState.Failure;
            }

            //x <summary>
            //! Constructeur qui initialise les données d'enregistrement à partir d'un objet MySqlDataReader
            //x </summary>
            //! <param name="Reader">Objet de récupération des données d'un enregistrement résultant d'une requête SQL de consultation</param>
            public RecordResult(MySqlDataReader Reader)
                : base(Reader)
            {
                this.Exists = (base.FieldCount > 0);
                this.RecordIndex = this.Exists ? 0 : -1;
                this.State = this.Exists ? RecordState.Success : RecordState.Failure;
            }

            //x <summary>
            //! Met à jour les données d'enregistrement à partir d'un objet MySqlDataReader
            //x </summary>
            //! <param name="Reader">Objet de récupération des données d'un enregistrement résultant d'une requête SQL de consultation</param>
            //! <returns>Vrai si l'opération a réussi, sinon faux</returns>
            public override bool Set(MySqlDataReader Reader)
            {
                this.Exists = base.Set(Reader);
                if (this.Exists) this.RecordIndex++;
                return this.Exists;
            }

            //x <summary>
            //! Retourne une représentation de ce résultat de consultation d'enregistrement sous forme d'une chaîne de caractères
            //x </summary>
            //! <returns>Chaîne présentant les données de ce résultat de consultation d'enregistrement</returns>
            public override string ToString()
            {
                return string.Format("{{ Exists : {0} ; RecordIndex : {1} ; Fields : {2} }}", this.Exists, this.RecordIndex, base.ToString());
            }
        }

        //x <summary>
        //! Permet d'énumérer tous les enregistrements résultant de l'exécution d'une requête SQL de consultation
        //x </summary>
        public interface IRecordEnumerator : IEnumerable<IRecordResult>, IDisposable
        {
        }

        //x <summary>
        //! Implémente un énumérateur d'enregistrements résultant de l'exécution d'une requête SQL de consultation avec ces propres ressources
        //x </summary>
        private class RecordEnumerator : IRecordEnumerator
        {
            //x <summary>
            //! Tableau vide de résultat résultant d'une exécution d'une requête SQL de consultation
            //x </summary>
            public static IRecordResult[] Empty { get; } = new IRecordResult[0];

            //x <summary>
            //! Tableau comprenant un et un seul résultat décrivant une erreur survenue lors de l'exécution d'une requête SQL de consultation
            //x </summary>
            public static IRecordResult[] Failure { get; } = new IRecordResult[] { RecordResult.Failure };

            //x <summary>
            //! Indique si un résultat doit être énuméré en cas d'échec d'exécution ou d'absence d'enregistrements résultant de l'exécution de la requête de consultation
            //x </summary>
            public bool AlwaysGenerateResult { get; private set; }

            //x <summary>
            //! Requête SQL de consultation
            //! <para>Entièrement constituée</para>
            //x </summary>
            public string FullQuery { get; private set; }

            //x <summary>
            //! Connexion au serveur MySQL propre à cet énumérateur
            //x </summary>
            private DB m_DB;

            //x <summary>
            //! Objet MySqlDataReader spécifique à la connexion au serveur MySQL propre à cet énumérateur
            //x </summary>
            private MySqlDataReader m_Reader;

            //x <summary>
            //! Objet RecordResult spécifique et unique à cet énumérateur
            //x </summary>
            private IRecordResult m_Result;

            //x <summary>
            //! Constructeur
            //x </summary>
            //! <param name="Owner">Object de connexion au serveur MySQL pour lequel la méthode d'énumération d'enregistrements a été appelée</param>
            //! <param name="AlwaysGenerateResult">Indique si un résultat doit être énuméré en cas d'échec d'exécution ou d'absence d'enregistrements résultant de l'exécution de la requête de consultation</param>
            //! <param name="FullQuery">Requête SQL complète de consultation</param>
            public RecordEnumerator(DB Owner, bool AlwaysGenerateResult, string FullQuery)
            {
                this.AlwaysGenerateResult = AlwaysGenerateResult;
                this.FullQuery = FullQuery;
                m_DB = null;
                m_Reader = null;
                m_Result = null;
                if (Owner.m_Connection == null)
                {
                    if (!Owner.Connect())
                    {
                        throw new Exception("Connexion au serveur mySQL inexistante et impossible à (r)établir !");
                    }
                }
                m_DB = new DB(Owner);
                if (!m_DB.Connect())
                {
                    throw new Exception("Impossible de réaliser une connexion temporaire pour l'énumération d'enregistrements !");
                }
                MySqlCommand Command = new MySqlCommand();
                Command.CommandText = this.FullQuery;
                Command.Connection = m_DB.m_Connection;
                m_Reader = Command.ExecuteReader();
            }

            //x <summary>
            //! Réalise l'énumération d'enregistrements résultant de l'exécution d'une requête SQL de consultation par cet énumérateur
            //x </summary>
            //! <returns>Résultat(s) d'énumération d'enregistrements résultant de l'exécution d'une requête SQL de consultation</returns>
            private IEnumerable<IRecordResult> Execute()
            {
                try
                {
                    bool Failure = false;
                    while (true)
                    {
                        try
                        {
                            if (!m_Reader.Read())
                            {
                                if (m_Result == null)
                                {
                                    Failure = true;
                                    m_Result = RecordResult.None;
                                }
                                break;
                            }
                            if (m_Result == null)
                            {
                                m_Result = new RecordResult(m_Reader);
                            }
                            else if (!(m_Result as RecordResult).Set(m_Reader))
                            {
                                throw new Exception($"Impossibilité de copier les valeurs des champs de l'enregistrement courant [{m_Result.RecordIndex}] dans le résultat d'exécution !\n");
                            }
                        }
                        catch (Exception Error)
                        {
                            Debug.WriteLine($"\nErreur d'exécution d'une requête de consultation MySQL :\n{this.FullQuery}\n{Error.Message}\n");
                            Failure = true;
                            m_Result = RecordResult.Failure;
                            break;
                        }
                        yield return m_Result;
                    }
                    if (Failure && this.AlwaysGenerateResult) yield return m_Result;
                }
                finally
                {
                    Dispose();
                }
            }

            //x <summary>
            //! Destructeur
            //x </summary>
            ~RecordEnumerator()
            {
                Dispose();
            }

            #region Implémentation de l'interface IDisposable
            public void Dispose()
            {
                if (m_Result != null)
                {
                    m_Result = null;
                }
                if (m_Reader != null)
                {
                    m_Reader.Dispose();
                    m_Reader = null;
                }
                if (m_DB != null)
                {
                    m_DB.Disconnect();
                    m_DB = null;
                }
            }
            #endregion

            #region Implémentation de l'interface IEnumerable<IRecordResult>
            public IEnumerator<IRecordResult> GetEnumerator()
            {
                return Execute().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Execute().GetEnumerator();
            }
            #endregion
        }

        //x <summary>
        //! Permet l'exécution d'une requête de type consultation, et d'en récupérer les données du premier enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //x </summary>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Résultat de l'exécution de cette requête de consultation, décrivant la réussite ou l'échec de cette dernière, ainsi que, si possible, les données du premier enregistrement en résultant</returns>
        public IEnumerable<IRecordResult> EnumerateRecords(string Query, params object[] Arguments)
        {
            return EnumerateRecords(false, Query, Arguments);
        }

        //x <summary>
        //! Permet l'exécution d'une requête de type consultation, et d'en récupérer les données du premier enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //x </summary>
        //! <param name="AlwaysGenerateResult">Indique si un résultat doit être énuméré en cas d'échec d'exécution ou d'absence d'enregistrements résultant de l'exécution de la requête de consultation</param>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Résultat de l'exécution de cette requête de consultation, décrivant la réussite ou l'échec de cette dernière, ainsi que, si possible, les données du premier enregistrement en résultant</returns>
        public IEnumerable<IRecordResult> EnumerateRecords(bool AlwaysGenerateResult, string Query, params object[] Arguments)
        {
            IRecordEnumerator Enumerator = null;
            string FullQuery = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(Query)) throw new Exception("La requête ne peut être null ou vide !");
                FullQuery = Format(Query, Arguments);
                Enumerator = new RecordEnumerator(this, AlwaysGenerateResult, FullQuery);
            }
            catch (Exception Error)
            {
                Debug.WriteLine($"\nErreur d'exécution d'une requête de consultation MySQL :\n{FullQuery}\n{Error.Message}\n");
            }
            if (Enumerator == null)
            {
                return (AlwaysGenerateResult) ? RecordEnumerator.Failure : RecordEnumerator.Empty;
            }
            else
            {
                return Enumerator;
            }
        }

        //x <summary>
        //! Permet l'exécution d'une requête de type consultation, et d'en récupérer les données du premier enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //x </summary>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Résultat de l'exécution de cette requête de consultation, décrivant la réussite ou l'échec de cette dernière, ainsi que, si possible, les données du premier enregistrement en résultant</returns>
        public IRecordResult GetRecord(string Query, params object[] Arguments)
        {
            if (string.IsNullOrWhiteSpace(Query)) return RecordResult.Failure;
            if (m_Connection == null)
            {
                if (!Connect()) return RecordResult.Failure;
            }
            string FullQuery = string.Empty;
            try
            {
                FullQuery = Format(Query, Arguments);
                MySqlCommand Command = new MySqlCommand();
                Command.CommandText = FullQuery;
                Command.Connection = m_Connection;
                using (MySqlDataReader Reader = Command.ExecuteReader())
                {
                    if (!Reader.Read()) return RecordResult.None;
                    return new RecordResult(Reader);
                }
            }
            catch (Exception Error)
            {
                Debug.WriteLine($"\nErreur d'exécution d'une requête de consultation MySQL :\n{FullQuery}\n{Error.Message}\n");
                return RecordResult.Failure;
            }
        }

        //x <summary>
        //! Permet l'exécution d'une requête de type consultation, et d'en récupérer la valeur du premier champ du premier enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //x </summary>
        //! <typeparam name="T">Type de valeur attendue</typeparam>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Valeur typée du premier champ du premier enregistrement résultant de l'exécution de cette requête de consultation, sinon la valeur par défaut pour le type attendu</returns>
        public T GetValue<T>(string Query, params object[] Arguments)
        {
            if (typeof(T).Equals(typeof(DateTime)))
            {
                object Value = GetValueWithDefault((object)default(MySql.Data.Types.MySqlDateTime), Query, Arguments);
                DateTime Result;
                return (T)(object)((Value is MySql.Data.Types.MySqlDateTime) && ToDateTime((MySql.Data.Types.MySqlDateTime)Value, out Result) ? Result : default(DateTime));
            }
            else if (typeof(T).Equals(typeof(decimal)))
            {
                object Value = GetValueWithDefault((object)default(MySql.Data.Types.MySqlDecimal), Query, Arguments);
                decimal Result;
                return (T)(object)((Value is MySql.Data.Types.MySqlDecimal) && ToDecimal((MySql.Data.Types.MySqlDecimal)Value, out Result) ? Result : default(decimal));
            }
            else
            {
                object Value = GetValueWithDefault((object)default(T), Query, Arguments);
                return (Value is T) ? (T)Value : default(T);
            }
        }

        //x <summary>
        //! Permet l'exécution d'une requête de type consultation, et d'en récupérer la valeur du premier champ du premier enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //x </summary>
        //! <typeparam name="T">Type de valeur attendue</typeparam>
        //! <param name="DefaultValue">Valeur par défaut à retourner en cas d'échec de la récupération d'un premier enregistrement résultant de l'exécution de cette requête de consultation</param>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Valeur typée du premier champ du premier enregistrement résultant de l'exécution de cette requête de consultation, sinon la valeur par défaut spécifiée</returns>
        public T GetValueWithDefault<T>(T DefaultValue, string Query, params object[] Arguments)
        {
            if (typeof(T).Equals(typeof(DateTime)))
            {
                object Value = GetValueWithDefault((object)default(MySql.Data.Types.MySqlDateTime), Query, Arguments);
                DateTime Result;
                return (Value is MySql.Data.Types.MySqlDateTime) && ToDateTime((MySql.Data.Types.MySqlDateTime)Value, out Result) ? (T)(object)Result : DefaultValue;
            }
            else if (typeof(T).Equals(typeof(decimal)))
            {
                object Value = GetValueWithDefault((object)default(MySql.Data.Types.MySqlDecimal), Query, Arguments);
                decimal Result;
                return (Value is MySql.Data.Types.MySqlDecimal) && ToDecimal((MySql.Data.Types.MySqlDecimal)Value, out Result) ? (T)(object)Result : DefaultValue;
            }
            else
            {
                object Value = GetValueWithDefault((object)DefaultValue, Query, Arguments);
                return (Value is T) ? (T)Value : DefaultValue;
            }
        }

        //x <summary>
        //! Permet l'exécution d'une requête de type consultation, et d'en récupérer la valeur du premier champ du premier enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //x </summary>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Valeur non typée du premier champ du premier enregistrement résultant de l'exécution de cette requête de consultation, sinon la valeur par défaut pour le type attendu</returns>
        public object GetValue(string Query, params object[] Arguments)
        {
            return GetValueWithDefault(null, Query, Arguments);
        }

        //x <summary>
        //! Permet l'exécution d'une requête de type consultation, et d'en récupérer la valeur du premier champ du premier enregistrement
        //! <para>Une requête de consultation est de type SELECT ou SHOW</para>
        //x </summary>
        //! <param name="DefaultValue">Valeur par défaut à retourner en cas d'échec de la récupération d'un premier enregistrement résultant de l'exécution de cette requête de consultation</param>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Valeur non typée du premier champ du premier enregistrement résultant de l'exécution de cette requête de consultation, sinon la valeur par défaut spécifiée</returns>
        public object GetValueWithDefault(object DefaultValue, string Query, params object[] Arguments)
        {
            if (string.IsNullOrWhiteSpace(Query)) return DefaultValue;
            if (m_Connection == null)
            {
                if (!Connect()) return DefaultValue;
            }
            string FullQuery = string.Empty;
            try
            {
                FullQuery = Format(Query, Arguments);
                MySqlCommand Command = new MySqlCommand();
                Command.CommandText = FullQuery;
                Command.Connection = m_Connection;
                using (MySqlDataReader Reader = Command.ExecuteReader())
                {
                    if (!Reader.Read()) return DefaultValue;
                    return Reader.GetValue(0);
                }
            }
            catch (Exception Error)
            {
                Debug.WriteLine($"\nErreur d'exécution d'une requête de consultation MySQL :\n{FullQuery}\n{Error.Message}\n");
                return DefaultValue;
            }
        }
    }
}
