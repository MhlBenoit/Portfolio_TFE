using System;
using System.Collections.Generic;
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
        // Permet de consulter un résultat d'exécution d'une requête d'action
        //! <para>Une requête d'action est généralement de type INSERT INTO, UPDATE, DELETE</para>
        //! <para>Ce type d'informations est retourné par la méthode Execute afin d'en décrire la réussite ou l'échec en lui adjoignant des informations complémentaires</para>
        //x </summary>
        public interface IExecuteResult
        {
            //x <summary>
            //! Indique si ce résultat correspond à un succès, ou pas
            //x </summary>
            bool IsSuccess { get; }

            //x <summary>
            //! Numéro du code d'erreur
            //! <para>Peut valoir 0 en cas d'erreur indéterminée ou en cas de réussite</para>
            //x </summary>
            int ErrorCode { get; }

            //x <summary>
            //! Nombre d'enregistrements affectés par la requête d'action exécutée
            //! <para>Toujours égal à 0 en cas d'échec</para>
            //x </summary>
            int AffectedRecordCount { get; }

            //x <summary>
            //! Identifiant de l'enregistrement créé
            //! <para>Cette information n'est disponible (c'est à dire, différente de 0) que si la requête d'action correspondait à une instruction INSERT INTO, que son exécution a réussi et qu'un et un seul enregistrement en a résulté !</para>
            //x </summary>
            long NewId { get; }
        }

        //x <summary>
        //! Implémentation de l'interface permettant de consulter un résultat d'exécution d'une requête d'action
        //! <para>Une requête d'action est généralement de type INSERT INTO, UPDATE, DELETE</para>
        //! <para>Ce type d'informations est retourné par la méthode Execute afin d'en décrire la réussite ou l'échec en lui adjoignant des informations complémentaires</para>
        //x </summary>
        private class ExecuteResult : IExecuteResult
        {
            //x <summary>
            //! Instance de résultat décrivant un échec indéterminé de l'exécution d'une requête d'action
            //! <para>Il est utilisé en cas d'erreur détectée avant la tentative d'exécution de la requête</para>
            //x </summary>
            public static ExecuteResult Failure { get; private set; } = GetFailure(0);

            //x <summary>
            //! Dictionnaire répertoriant par numéro de code d'erreur les instances des résultats décrivant des échecs d'exécution de requête d'action
            //x </summary>
            private static Dictionary<int, ExecuteResult> s_Failures = null;

            //x <summary>
            //! Permet de récupérer, ou bien de créer et d'enregistrer, l'instance de résultat décrivant un échec identifié par le numéro de code d'erreur spécifié
            //x </summary>
            //! <param name="ErrorCode">Numéro du code de l'erreur</param>
            //! <returns>Instance de résultat décrivant un échec identifié par le numéro de code d'erreur spécifié</returns>
            public static ExecuteResult GetFailure(int ErrorCode)
            {
                ExecuteResult Result;
                if (s_Failures == null)
                {
                    s_Failures = new Dictionary<int, ExecuteResult>();
                }
                if (!s_Failures.TryGetValue(ErrorCode, out Result))
                {
                    Result = new ExecuteResult(ErrorCode);
                    s_Failures.Add(ErrorCode, Result);
                }
                return Result;
            }

            //x <summary>
            //! Indique si ce résultat correspond à un succès, ou pas
            //x </summary>
            public bool IsSuccess { get; private set; }

            //x <summary>
            //! Numéro du code d'erreur
            //! <para>Peut valoir 0 en cas d'erreur indéterminée ou en cas de réussite</para>
            //x </summary>
            public int ErrorCode { get; private set; }

            //x <summary>
            //! Nombre d'enregistrements affectés par la requête d'action exécutée
            //! <para>Toujours égal à 0 en cas d'échec</para>
            //x </summary>
            public int AffectedRecordCount { get; private set; }

            //x <summary>
            //! Identifiant de l'enregistrement créé
            //! <para>Cette information n'est disponible (c'est à dire, différente de 0) que si la requête d'action correspondait à une instruction INSERT INTO, que son exécution a réussi et qu'un et un seul enregistrement en a résulté !</para>
            //x </summary>
            public long NewId { get; private set; }

            //x <summary>
            //! Constructeur permettant de définir un résultat de type "échec"
            //x </summary>
            //! <param name="ErrorCode">Numéro du code d'erreur</param>
            private ExecuteResult(int ErrorCode = 0)
            {
                this.IsSuccess = false;
                this.ErrorCode = ErrorCode;
                this.AffectedRecordCount = 0;
                this.NewId = 0;
            }

            //x <summary>
            //! Constructeur permettant de définir un résultat de type "réussite"
            //x </summary>
            //! <param name="AffectedRecordCount">Nombre d'enregistrements affectés par l'exécution de cette requête d'action</param>
            //! <param name="NewId">Identifiant de l'unique enregistrement créé, si tel est le cas, sinon 0</param>
            public ExecuteResult(int AffectedRecordCount, long NewId)
            {
                this.IsSuccess = true;
                this.ErrorCode = 0;
                this.AffectedRecordCount = AffectedRecordCount;
                this.NewId = NewId;
            }
        }

        //x <summary>
        //! Permet l'exécution d'une requête de type action
        //! <para>Une requête d'action est généralement de type INSERT INTO, UPDATE, DELETE</para>
        //x </summary>
        //! <param name="Query">Requête SQL, celle-ci pouvant être paramétrée par le biais d'une indexation {0}, {1}, {2}, ...</param>
        //! <param name="Arguments">Arguments fournissant les valeurs des données à injecter en lieu et place des paramètres indexés de la requête spécifiée</param>
        //! <returns>Résultat de l'exécution de cette requête d'action, décrivant la réussite ou l'échec de cette dernière</returns>
        public IExecuteResult Execute(string Query, params object[] Arguments)
        {
            if (string.IsNullOrWhiteSpace(Query)) return ExecuteResult.Failure;
            if (m_Connection == null)
            {
                if (!Connect()) return ExecuteResult.Failure;
            }
            string FullQuery = string.Empty;
            try
            {
                FullQuery = Format(Query, Arguments);
                MySqlCommand Command = new MySqlCommand();
                Command.CommandText = FullQuery;
                Command.Connection = m_Connection;
                int AffectedRecordCount = Command.ExecuteNonQuery();
                long NewId = (AffectedRecordCount == 1) && FullQuery.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) ? Command.LastInsertedId : 0;
                return new ExecuteResult(AffectedRecordCount, NewId);
            }
            catch (Exception Error)
            {
                Debug.WriteLine($"\nErreur d'exécution d'une requête d'action MySQL :\n{FullQuery}\n{Error.Message}\n");
                int ErrorCode = 0;
                if (Error is MySqlException)
                {
                    ErrorCode = (Error as MySqlException).Number;
                }
                return ExecuteResult.GetFailure(ErrorCode);
            }
        }
    }
}
