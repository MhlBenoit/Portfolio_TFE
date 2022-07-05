using System;
using System.Windows.Data;
using System.Windows.Markup;
using TFE.Model;

namespace TFE.Tools
{
    //x <summary>
    //! Classe utile pour convertir les informations d'un client sous la forme "NOM Prénom" ou "mail"
    //x </summary>
    public class Converter_Binding_Customer : Binding
    {
        public Converter_Binding_Customer()
        {
            Initialize();
        }

        public Converter_Binding_Customer(string path)
            : base(path)
        {
            Initialize();
        }

        public Converter_Binding_Customer(string path, object value)
            : base(path)
        {
            Initialize();
            this.Value = value;
        }

        private void Initialize()
        {
            this.Value = Binding.DoNothing;
            this.Converter = new SwitchConverter(this);
        }

        [ConstructorArgument("value")]
        public object Value { get; set; }

        private class SwitchConverter : IValueConverter
        {
            public SwitchConverter(Converter_Binding_Customer C_B_C)
            {
                _switch = C_B_C;
            }

            private Converter_Binding_Customer _switch;

            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                var customer = Customer.Load((uint)value);
                if (customer.Person_id.Lastname.ToLower() != "client" && customer.Person_id.Firstname.ToLower() != "anonyme")
                {
                    if (customer.Person_id.Lastname != "" && customer.Person_id.Firstname != "")
                    {
                        return $"{customer.Person_id.Lastname.ToUpper()} {customer.Person_id.Firstname}";
                    }
                    else
                        return $"{customer.Person_id.Mail}";
                }
                else
                    return $"{customer.Person_id.Mail}";
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return Binding.DoNothing;
            }
            #endregion
        }

    }
}
