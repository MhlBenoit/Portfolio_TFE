using System;
using System.Windows.Data;
using System.Windows.Markup;
using TFE.Model;

namespace TFE.Tools
{
    //x <summary>
    //! Classe utile pour convertir les informations d'un employé sous la forme "NOM Prénom"
    //x </summary>
    public class Converter_Binding_Employee : Binding
    {
        public Converter_Binding_Employee()
        {
            Initialize();
        }

        public Converter_Binding_Employee(string path)
            : base(path)
        {
            Initialize();
        }

        public Converter_Binding_Employee(string path, object value)
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
            public SwitchConverter(Converter_Binding_Employee C_B_E)
            {
                _switch = C_B_E;
            }

            private Converter_Binding_Employee _switch;

            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                var employee = Employee.Load((uint)value);

                return $"{employee.Person_id.Lastname.ToUpper()} {employee.Person_id.Firstname}";
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return Binding.DoNothing;
            }
            #endregion
        }

    }
}
