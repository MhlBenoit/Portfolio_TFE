using System;
using System.Collections.Generic;

namespace TFE.Model
{
    //x <summary>
    //! Décrit une transaction
    //x </summary>
    public class Transaction : Entity<Transaction>
    {
        //x <summary>
        //! Informations d'une transaction
        //x </summary>
        public Employee Employee_id { get; set; }
        public Customer Customer_id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal? Reduction { get; set; }
        public decimal Sum { get; set; }
        public decimal Tip { get; set; }
        public int Type { get; set; }

        //x <summary>
        //! Constructeur privé
        //x </summary>
        //! <param name="id_transaction">Identifiant d'une transaction</param>
        //! <param name="employee_id">Employé d'une transaction</param>
        //! <param name="customer_id">Client d'une transaction</param>
        //! <param name="date">Date d'une transaction</param>
        //! <param name="total">Total d'une transaction</param>
        //! <param name="reduction">Réduction accordée d'une transaction</param>
        //! <param name="sum">Somme donnée par le client d'une transaction</param>
        //! <param name="tip">Pourboire donné par le client d'une transaction</param>
        //! <param name="type">Type de transaction</param>
        public Transaction(uint id_transaction, Employee employee_id, Customer customer_id, DateTime date, decimal total, decimal? reduction, decimal sum, decimal tip, int type)
            : base(id_transaction)
        {
            DefinirReferenceEntity(this);
            Employee_id = employee_id;
            Customer_id = customer_id;
            Date = date;
            Total = total;
            Reduction = reduction;
            Sum = sum;
            Tip = tip;
            Type = type;
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
        //! Permet de tester la validité du champ employé pour une transaction
        //x </summary>
        //! <param name="employee_id">Employé à tester</param>
        //! <param name="employee_idFinal">Employé finalement retenu si il est valide
        //! <returns>Vrai si cet employé est valide, sinon faux</returns>
        public static bool TesterValidite_Employee_id(Employee employee_id, out Employee employee_idFinal)
        {
            employee_idFinal = null;
            if (employee_id != null)
            {
                employee_idFinal = employee_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ client pour une transaction
        //x </summary>
        //! <param name="customer_id">Client à tester</param>
        //! <param name="customer_idFinal">Client finalement retenu si il est valide
        //! <returns>Vrai si ce client est valide, sinon faux</returns>
        public static bool TesterValidite_Customer_id(Customer customer_id, out Customer customer_idFinal)
        {
            customer_idFinal = null;
            if (customer_id != null)
            {
                customer_idFinal = customer_id;
                return true;
            }
            else
                return false;
        }

        //x <summary>
        //! Permet de tester la validité du champ total pour une transaction
        //x </summary>
        //! <param name="total">Total à tester</param>
        //! <param name="totalFinal">Total finalement retenu si il est valide
        //! <returns>Vrai si ce total est valide, sinon faux</returns>
        public static bool TesterValidite_Total(decimal total, out decimal totalFinal)
        {
            totalFinal = total;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ somme donnée pour une transaction
        //x </summary>
        //! <param name="sum">Somme donnée à tester</param>
        //! <param name="sumFinal">Somme donnée finalement retenue si elle est valide
        //! <returns>Vrai si cette somme donnée est valide, sinon faux</returns>
        public static bool TesterValidite_Sum(decimal sum, out decimal sumFinal)
        {
            sumFinal = 0;
            if (sum < 0) return false;
            sumFinal = sum;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ pourboire pour une transaction
        //x </summary>
        //! <param name="tip">Pourboire à tester</param>
        //! <param name="tipFinal">Pourboire finalement retenu si il est valide
        //! <returns>Vrai si ce pourboire est valide, sinon faux</returns>
        public static bool TesterValidite_Tip(decimal tip, out decimal tipFinal)
        {
            tipFinal = 0;
            if (tip < 0) return false;
            tipFinal = tip;
            return true;
        }

        //x <summary>
        //! Permet de tester la validité du champ type pour une transaction
        //x </summary>
        //! <param name="type">Type à tester</param>
        //! <param name="typeFinal">Type finalement retenu si il est valide
        //! <returns>Vrai si ce type est valide, sinon faux</returns>
        public static bool TesterValidite_Type(int type, out int typeFinal)
        {
            typeFinal = 0;
            switch (type)
            {
                case 1:
                    typeFinal = type;
                    return true;
                case 2:
                    typeFinal = type;
                    return true;
                case 3:
                    typeFinal = type;
                    return true;

                default:
                    return false;
            }
        }
        #endregion

        //x <summary>
        //! Nom de table utile à la création d'une entité de type transaction
        //x </summary>
        public override string NameTable => "transaction";

        //x <summary>
        //! Liste des champs utiles à la création d'une entité de type transaction
        //x </summary>
        public override string ListFields => "id_transaction, employee_id, customer_id, date, total, reduction, sum, tip, type";

        //x <summary>
        //! Liste des associations entre nom et valeur des champs utiles à la mise à jour d'une entité de type transaction au sein de la base de données
        //x </summary>
        public override IEnumerable<FieldValue> AssociationFieldsValues
        {
            get
            {
                yield return FieldValue.Associate("employee_id", Employee_id);
                yield return FieldValue.Associate("customer_id", Customer_id);
                yield return FieldValue.Associate("date", Date);
                yield return FieldValue.Associate("total", Total);
                yield return FieldValue.Associate("reduction", Reduction);
                yield return FieldValue.Associate("sum", Sum);
                yield return FieldValue.Associate("tip", Tip);
                yield return FieldValue.Associate("type", Type);
            }
        }

        //x <summary>
        //! Objet de référence pour ce modèle
        //x </summary>
        private static Transaction s_TransactionDeReference = new Transaction(0, Employee.ReferenceEntity, Customer.ReferenceEntity, DateTime.Now, 0, null, 0, 0, 0);

        //x <summary>
        //! Permet d'instancier une nouvelle entité de type transaction
        //x </summary>
        //! <param name="employee_id">Employé d'une transaction</param>
        //! <param name="customer_id">Client d'une transaction</param>
        //! <param name="date">Date d'une transaction</param>
        //! <param name="total">Total d'une transaction</param>
        //! <param name="reduction">Réduction accordée d'une transaction</param>
        //! <param name="sum">Somme donnée par le client d'une transaction</param>
        //! <param name="tip">Pourboire donné par le client d'une transaction</param>
        //! <param name="type">Type de transaction</param>        
        //! <returns>Nouvelle transaction si possible, sinon null</returns>
        public static Transaction Create(Employee employee_id, Customer customer_id, decimal total, decimal reduction, decimal sum, decimal tip, int type)
        {
            if (!TesterValidite_Employee_id(employee_id, out employee_id)) return null;
            if (!TesterValidite_Customer_id(customer_id, out customer_id)) return null;
            if (!TesterValidite_Total(total, out total)) return null;
            if (!TesterValidite_Sum(sum, out sum)) return null;
            if (!TesterValidite_Tip(tip, out tip)) return null;
            if (!TesterValidite_Type(type, out type)) return null;
            return new Transaction(0, employee_id, customer_id, DateTime.Now, total, reduction, sum, tip, type);
        }

        //x <summary>
        //! Permet de charger si possible la transaction correspondant à l'identifiant spécifié
        //x </summary>
        //! <param name="id_transaction">Identifiant d'une transaction à charger</param>
        //! <returns>Transaction correspondant à l'identifiant spécifié si possible, sinon null</returns>
        public static new Transaction Load(uint id_transaction)
        {
            var record = Entity<Transaction>.Load(id_transaction);
            if (record == null) return null;
            return new Transaction(id_transaction, Employee.Load(record.GetFieldValue<uint>("employee_id"))
                                                 , Customer.Load(record.GetFieldValue<uint>("customer_id"))
                                                 , record.GetFieldValue<DateTime>("date")
                                                 , record.GetFieldValue<decimal>("total")
                                                 , record.GetFieldValue<decimal?>("reduction")
                                                 , record.GetFieldValue<decimal>("sum")
                                                 , record.GetFieldValue<decimal>("tip")
                                                 , record.GetFieldValue<int>("type"));
        }

        //x <summary>
        //! Enumère toutes les transactions
        //x </summary>
        public static IEnumerable<Transaction> Enumeration
        {
            get
            {
                return Enumerer((record) => new Transaction(
                        record.GetFieldValue<uint>("id_transaction"),
                        Employee.Load(record.GetFieldValue<uint>("employee_id")),
                        Customer.Load(record.GetFieldValue<uint>("customer_id")),
                        record.GetFieldValue<DateTime>("date"),
                        record.GetFieldValue<decimal>("total"),
                        record.GetFieldValue<decimal?>("reduction"),
                        record.GetFieldValue<decimal>("sum"),
                        record.GetFieldValue<decimal>("tip"),
                        record.GetFieldValue<int>("type")));
            }
        }
    }
}
