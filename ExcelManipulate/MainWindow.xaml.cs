using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExcelManipulate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnArquivo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                OpenFileDialog dialog = new OpenFileDialog()
                {
                    FileName = "Selecione um csv",
                    Filter = "CSV (*.csv)|*.csv",
                    Title = "Open csv file"
                };
                Nullable<bool> result = dialog.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    txtArquivo.Text = dialog.FileName;
                    DataContext = Auxiliar.utils.getCsvList(dialog.FileName);
                    btnSalvar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                FileName = txtArquivo.Text.Substring(txtArquivo.Text.LastIndexOf('\\')+1),
                Filter = "CSV (*.csv)|*.csv",
                Title = "Save csv file"
            };
            Nullable<bool> result = saveFile.ShowDialog();
            if (result == true && saveFile.FileName != string.Empty)
            {


                var dataList = new List<string>(Auxiliar.utils.GetList(txtArquivo.Text));
                if (dataList.Count == 0)
                {
                    MessageBox.Show("Não foi encontradado os dados!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    int cont = dataList[0].Split(';').Count();
                    switch (cont)
                    {
                        case 1:
                        case 2:                       
                        case 3:
                            Auxiliar.utils.WriteCsv(Auxiliar.utils.TirarDuplicidade(dataList).ToArray(), saveFile.FileName);
                            break;
                        case 4:
                            Auxiliar.utils.WriteCsv(Auxiliar.utils.OrganizeList(dataList).ToArray(), saveFile.FileName);
                            break;
                        default:
                            Auxiliar.utils.WriteCsv(Auxiliar.utils.ClearThePast(dataList).ToArray(), saveFile.FileName);
                            break;
                    }
                    MessageBox.Show("Salvo com Sucesso!", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }


            }

        }
    }
}
