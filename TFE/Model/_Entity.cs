using System;
using System.Collections.Generic;
using System.Linq;
using _DB;

namespace TFE.Model
{
    public abstract class _Entity
    {
        private static DB s_Bd;

        public static bool DefineDB(DB bd)
        {
            if ((s_Bd != null) || (bd == null)) return false;
            s_Bd = bd;
            return true;
        }

        protected static DB Bd => s_Bd;

        protected static bool VerifyPresence(string request, params object[] arguments)
        {
            return (Bd != null) && (Bd.GetValue<long>(request, arguments) > 0);
        }

        public class FieldValue
        {
            public string Name { get; private set; }

            public object Value { get; private set; }

            private FieldValue(string name, object value)
            {
                Name = name;
                Value = value;
            }

            public static FieldValue Associate(string name, object value)
            {
                return string.IsNullOrWhiteSpace(name) ? null : new FieldValue(name, value);
            }
        }
    }

    //x <summary>
    //! Décrit toute entité de la base de données
    //x </summary>
    public abstract class Entity<T> : _Entity, IEquatable<Entity<T>>
        where T : Entity<T>
    {

        //x <summary>
        //! Identifiant unique de l'entité, servant de clé primaire
        //x </summary>
        public uint Id { get; private set; }

        //x <summary>
        //! Constructeur
        //x </summary>
        //! <param name="id">Identifiant de cette nouvelle entité</param>
        protected Entity(uint id)
        {
            Id = id;
        }

        //x <summary>
        //! Teste l'égalité entre cette entité et celle spécifiée en paramètre
        //! <para>Basé sur un test d'égalité de nature et d'identifiant</para>
        //x </summary>
        //! <param name="other">Autre entité comparée à celle-ci</param>
        //! <returns>Vrai si cela représente la même entité, sinon faux</returns>
        public bool Equals(Entity<T> other)
        {
            return (other != null) && other.GetType().Equals(this.GetType()) && other.Id.Equals(this.Id);
        }

        //x <summary>
        //! Teste l'égalité entre cet objet et celui spécifié en paramètre
        //x </summary>
        //! <param name="obj">Objet comparé à celui-ci</param>
        //! <returns>Vrai en cas d'égalité, sinon faux</returns>
        public override bool Equals(object obj)
        {
            return (obj is T) ? Equals(obj as Entity<T>) : false;
        }

        //x <summary>
        //! Retourne le code de hachage de cet objet
        //! <para>Ce code est basé sur la nature et l'identifiant de cette entité</para>
        //x </summary>
        //! <returns>Code de hachage de cet objet</returns>
        public override int GetHashCode()
        {
            return GetType().GetHashCode() ^ Id.GetHashCode();
        }

        //x <summary>
        //! Représente sous forme de texte cette entité
        //x </summary>
        //! <returns>Texte représentatif de cet objet</returns>
        public override string ToString()
        {
            return $"{{ Id : {Id} }}";
        }

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type T
        //x </summary>
        public abstract string NameTable { get; }

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type T
        //x </summary>
        public abstract string ListFields { get; }

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type T au sein de la base de données
        //x </summary>
        public abstract IEnumerable<FieldValue> AssociationFieldsValues { get; }

        //x <summary>
        //! Référence de l'objet de "référence" pour le type T (dérivé de Entity)
        //x </summary>
        private static T s_ReferenceEntity;

        //x <summary>
        //! Tableau vide d'entités pour le type T
        //x </summary>
        private static readonly T[] s_NoEntity = new T[0];

        //x <summary>
        //! Enumération d'aucune entité
        //x </summary>
        protected static IEnumerable<T> NoEntity => s_NoEntity;

        //x <summary>
        //! Permet de définir l'entité de référence pour le type T (dérivé de Entity&lt;T&gt;)
        //x </summary>
        //! <param name="entity">Entité de référence</param>
        protected static void DefinirReferenceEntity(T entity)
        {
            if ((s_ReferenceEntity == null) && (entity != null)) s_ReferenceEntity = entity;
        }

        //x <summary>
        //! Entité de référence
        //! <para>A utiliser uniquement au sein d'une entité de référence d'un autre modèle !</para>
        //x </summary>
        public static T ReferenceEntity => s_ReferenceEntity;

        //x <summary>
        //! Permet de charger si possible l'record correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id">Identifiant de l'record spécifié</param>
        //! <returns>Enregistrement correspondant à l'identifiant spécifié si possible, sinon null</returns>
        protected static DB.IRecord Load(uint id)
        {
            var result = (Bd != null) ? Bd.GetRecord("SELECT {0} FROM `{1}` WHERE id_{1} = {2}",
                DB.SqlCode.Create(s_ReferenceEntity.ListFields),
                DB.SqlCode.Create(s_ReferenceEntity.NameTable),
                id) : null;
            return (result != null) && result.Exists ? result as DB.IRecord : null;
        }

        //x <summary>
        //! Permet de charger si possible l'record correspondant au login spécifié
        //x </summary>
        //! <param name="login">login de l'record spécifié</param>
        //! <returns>Enregistrement correspondant au string spécifié si possible, sinon null</returns>
        protected static DB.IRecord Load(string login)
        {
            var result = (Bd != null) ? Bd.GetRecord("SELECT {0} FROM {1} WHERE login = {2}",
                DB.SqlCode.Create(s_ReferenceEntity.ListFields),
                DB.SqlCode.Create(s_ReferenceEntity.NameTable),
                login) : null;
            return (result != null) && result.Exists ? result as DB.IRecord : null;
        }

        //x <summary>
        //! Permet de mettre à jour la base de données avec les informations de cette entité
        //x </summary>
        //! <returns>Vrai si la mise à jour a pu être réalisée, sinon faux</returns>
        public virtual bool Save()
        {
            if (Bd == null) return false;
            for (var attempt = (Id == 0) ? 1 : 0; attempt < 2; attempt++)
            {
                var result = Bd.Execute(
                    (attempt == 0) ? "UPDATE {0} SET {1} WHERE id_{0} = {2}" : "INSERT INTO {0} SET {1}",
                    DB.SqlCode.Create(NameTable),
                    DB.SqlCode.Create(string.Join(", ", AssociationFieldsValues.Where(association => association != null).Select(association => DB.Format(association.Name + " = {0}", association.Value)))),
                    Id);
                if (((attempt == 1) && (result.NewId != 0)) || ((attempt == 0) && result.IsSuccess))
                {
                    if (attempt == 1)
                    {
                        Id = (uint)result.NewId;
                    }
                    return true;
                }
            }
            return false;
        }

        //x <summary>
        //! Permet de supprimer de la base de données cette entité
        //x </summary>
        //! <returns>Vrai si la suppression a pu être réalisée, sinon faux</returns>
        public virtual bool Delete()
        {
            if ((Bd == null) || (Id == 0)) return false;
            return (Bd.Execute("DELETE FROM {0} WHERE id_{0} = {1}", DB.SqlCode.Create(NameTable), Id).AffectedRecordCount != 0);
        }

        //x <summary>
        //! Définit ce qu'est une méthode d'instanciation d'une entité à partir d'un record
        //x </summary>
        //! <param name="record">Enregistrement pour lequel réaliser une instanciation
        //! <para>Cet objet décrivant un record existe d'office (donc différent de null).</para></param>
        //! <returns></returns>
        public delegate T InstantiationMethod(DB.IRecord record);

        //x <summary>
        //! Permet d'énumérer les entités de type T, en tenant compte de l'instanciateur, et éventuellement d'un ordonnancement et/ou d'un filtre
        //x </summary>
        //! <param name="instanciateur">Instanciateur d'entité à partir de l'record spécifié</param>
        //! <param name="ordonnancement">Code d'ordonnancement (code de la clause WHERE - sans le mot clé WHERE)</param>
        //! <param name="filter">Code de filtrage (code de la clause ORDER BY - sans le mot clé ORDER BY)</param>
        //! <returns>Entités résultant de cette énumération</returns>
        protected static IEnumerable<T> Enumerer(InstantiationMethod instantiator, string scheduling = null, string filter = null)
        {
            if ((Bd == null) || (instantiator == null)) return s_NoEntity;
            var request = "SELECT {0} FROM `{1}`";
            if (!string.IsNullOrWhiteSpace(filter)) request += " WHERE {2}";
            if (!string.IsNullOrWhiteSpace(scheduling)) request += " ORDER BY {3}";
            return Bd.EnumerateRecords(request,
                        DB.SqlCode.Create(s_ReferenceEntity.ListFields),
                        DB.SqlCode.Create(s_ReferenceEntity.NameTable),
                        DB.SqlCode.Create(filter),
                        DB.SqlCode.Create(scheduling))
                   .Select(record => instantiator(record))
                   .Where(entite => entite != null);
        }
    }

}
