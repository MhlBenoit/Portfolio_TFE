using System.Windows;
using System.Windows.Controls;

namespace TFE.View.PDF
{
    //x <summary>
    //! Code contenant des méthodes pour la gestion de la vue v_PDF_Accounting
    //x </summary>
    public partial class v_PDF_Accounting : Window
    {
        public v_PDF_Accounting()
        {
            InitializeComponent();
        }

        public void Print()
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(FileToPrint, "Royaume du Hibou");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
