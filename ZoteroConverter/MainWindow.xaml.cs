using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using ZoteroConverter.Entities.Zotero;


namespace ZoteroConverter
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

        private void btnConvertir_Click(object sender, RoutedEventArgs e)
        {

           //abrimos el dialogo para seleccionar el archivo de zotero
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                txtRutaArchivo.Text = filePath;

                List<Reference> zoteroReferences = filePath.GetReferences();

                var sourcesNew = zoteroReferences.ToSources();



            }


}
    }
}