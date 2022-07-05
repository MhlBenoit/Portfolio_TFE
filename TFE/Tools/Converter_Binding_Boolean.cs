using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TFE.Tools
{
    //x <summary>
    //! Classe utile pour convertir les valeurs d'un boolean dans un datagrid
    //x </summary>
    public class Converter_Binding_Boolean : Binding
    {
        public Converter_Binding_Boolean()
        {
            Initialize();
        }

        public Converter_Binding_Boolean(string path)
            : base(path)
        {
            Initialize();
        }

        public Converter_Binding_Boolean(string path, object valueIfTrue, object valueIfFalse)
            : base(path)
        {
            Initialize();
            this.ValueIfTrue = valueIfTrue;
            this.ValueIfFalse = valueIfFalse;
        }

        private void Initialize()
        {
            this.ValueIfTrue = Binding.DoNothing;
            this.ValueIfFalse = Binding.DoNothing;
            this.Converter = new SwitchConverter(this);
        }

        [ConstructorArgument("valueIfTrue")]
        public object ValueIfTrue { get; set; }

        [ConstructorArgument("valueIfFalse")]
        public object ValueIfFalse { get; set; }

        private class SwitchConverter : IValueConverter
        {
            public SwitchConverter(Converter_Binding_Boolean C_B_B)
            {
                _switch = C_B_B;
            }

            private Converter_Binding_Boolean _switch;

            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    bool b = System.Convert.ToBoolean(value);
                    return b ? _switch.ValueIfTrue : _switch.ValueIfFalse;
                }
                catch
                {
                    return DependencyProperty.UnsetValue;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return Binding.DoNothing;
            }
            #endregion
        }

    }
}
