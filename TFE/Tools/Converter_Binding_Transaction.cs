using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace TFE.Tools
{
    //x <summary>
    //! Classe utile pour convertir les valeurs 1-2-3 au type de transaction associé en base de données
    //x </summary>
    public class Converter_Binding_Transaction : Binding
    {
        public Converter_Binding_Transaction()
        {
            Initialize();
        }

        public Converter_Binding_Transaction(string path)
            : base(path)
        {
            Initialize();
        }

        public Converter_Binding_Transaction(string path, object valueIf_1, object valueIf_2, object valueIf_3)
            : base(path)
        {
            Initialize();
            this.ValueIf_1 = valueIf_1;
            this.ValueIf_2 = valueIf_2;
            this.ValueIf_3 = valueIf_3;
        }

        private void Initialize()
        {
            this.ValueIf_1 = Binding.DoNothing;
            this.ValueIf_2 = Binding.DoNothing;
            this.ValueIf_3 = Binding.DoNothing;
            this.Converter = new SwitchConverter(this);
        }

        [ConstructorArgument("valueIf_1")]
        public object ValueIf_1 { get; set; }

        [ConstructorArgument("valueIf_2")]
        public object ValueIf_2 { get; set; }

        [ConstructorArgument("valueIf_3")]
        public object ValueIf_3 { get; set; }

        private class SwitchConverter : IValueConverter
        {
            public SwitchConverter(Converter_Binding_Transaction C_B_T)
            {
                _switch = C_B_T;
            }

            private Converter_Binding_Transaction _switch;

            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                var type = System.Convert.ToInt64(value);

                if (type == 1)
                {
                    _switch.ValueIf_1 = "Vente";
                    return _switch.ValueIf_1;
                }
                else if (type == 2)
                {
                    _switch.ValueIf_2 = "Dépôt";
                    return _switch.ValueIf_2;
                }
                else if (type == 3)
                {
                    _switch.ValueIf_3 = "Remboursement";
                    return _switch.ValueIf_3;
                }
                else
                {
                    return Binding.DoNothing;
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
