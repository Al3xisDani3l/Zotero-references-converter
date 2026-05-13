using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ZoteroConverter.Entities.Zotero;

namespace ZoteroConverter
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<ReferenceRow> references = new();
        private List<Reference> zoteroReferences = new();

        public MainWindow()
        {
            InitializeComponent();
            dgReferencias.ItemsSource = references;
            ClearDetails();
        }

        private void btnCargarArchivo_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Seleccionar archivo exportado desde Zotero",
                Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            LoadZoteroFile(openFileDialog.FileName);
        }

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            if (zoteroReferences.Count == 0)
            {
                MessageBox.Show("Primero carga un archivo de referencias exportado desde Zotero.", "Sin referencias", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var sources = Extensions.GetSources();
            if (sources is null)
            {
                txtEstado.Text = "No se pudo leer el archivo Sources.xml de Microsoft Word.";
                return;
            }

            sources.Source ??= new();

            var result = MessageBox.Show(
                $"Se agregaran {zoteroReferences.Count} referencias al archivo de bibliografia de Word.\n\nArchivo destino:\n{Extensions.GetSourcesFile().FullName}\n\nDeseas continuar?",
                "Confirmar exportacion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                txtEstado.Text = "Exportacion cancelada.";
                return;
            }

            try
            {
                sources.Source.AddReferences(zoteroReferences);
                if (!sources.SaveSources())
                {
                    txtEstado.Text = "No se pudo guardar el archivo de bibliografia de Word.";
                    return;
                }

                txtEstado.Text = $"{zoteroReferences.Count} referencias exportadas correctamente a Word.";
                MessageBox.Show("Las referencias fueron agregadas a la bibliografia de Microsoft Word.", "Exportacion completada", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                txtEstado.Text = "Error al exportar las referencias.";
                MessageBox.Show("Error al exportar las referencias: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgReferencias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReferencias.SelectedItem is not ReferenceRow selected)
            {
                ClearDetails();
                return;
            }

            txtDetalleTitulo.Text = selected.Title;
            txtDetalleAutores.Text = selected.Authors;
            txtDetallePublicacion.Text = string.Join(Environment.NewLine, new[]
            {
                $"Tipo: {Display(selected.Type)}",
                $"Anio: {Display(selected.Year)}",
                $"Editorial: {Display(selected.Publisher)}",
                $"Lugar: {Display(selected.Reference.PublisherPlace)}",
                $"DOI / ISBN: {Display(selected.Identifier)}"
            });
            txtDetalleUrl.Text = Display(selected.Reference.URL);
            txtDetalleResumen.Text = Display(selected.Reference.Abstract);
        }

        private void LoadZoteroFile(string filePath)
        {
            txtEstado.Text = "Leyendo archivo de Zotero...";
            txtRutaArchivo.Text = filePath;
            zoteroReferences = filePath.GetReferences();

            references.Clear();
            for (var i = 0; i < zoteroReferences.Count; i++)
            {
                references.Add(ReferenceRow.FromReference(i + 1, zoteroReferences[i]));
            }

            btnExportar.IsEnabled = references.Count > 0;
            txtResumen.Text = references.Count == 1
                ? "1 referencia cargada desde Zotero."
                : $"{references.Count} referencias cargadas desde Zotero.";
            txtEstado.Text = references.Count > 0
                ? "Revisa las referencias y presiona Exportar a Word para agregarlas sin abrir Zotero."
                : "El archivo no contiene referencias o no pudo leerse.";

            if (references.Count > 0)
            {
                dgReferencias.SelectedIndex = 0;
            }
            else
            {
                ClearDetails();
            }
        }

        private void ClearDetails()
        {
            txtDetalleTitulo.Text = "-";
            txtDetalleAutores.Text = "-";
            txtDetallePublicacion.Text = "-";
            txtDetalleUrl.Text = "-";
            txtDetalleResumen.Text = "-";
        }

        private static string Display(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
        }

        private static string GetYear(Reference reference)
        {
            var dateParts = reference.Issued?.dateparts;
            if (dateParts is null || dateParts.Length == 0 || dateParts[0].Length == 0)
            {
                return string.Empty;
            }

            return dateParts[0][0]?.ToString() ?? string.Empty;
        }

        private static string GetAuthors(Reference reference)
        {
            if (reference.Author is null || reference.Author.Count == 0)
            {
                return string.Empty;
            }

            return string.Join("; ", reference.Author.Select(author =>
            {
                var fullName = $"{author.Given} {author.Family}".Trim();
                return string.IsNullOrWhiteSpace(fullName) ? author.Family ?? author.Given ?? string.Empty : fullName;
            }).Where(author => !string.IsNullOrWhiteSpace(author)));
        }

        private sealed class ReferenceRow
        {
            public int Index { get; init; }
            public string Title { get; init; } = string.Empty;
            public string Authors { get; init; } = string.Empty;
            public string Year { get; init; } = string.Empty;
            public string Type { get; init; } = string.Empty;
            public string Publisher { get; init; } = string.Empty;
            public string Identifier { get; init; } = string.Empty;
            public Reference Reference { get; init; } = new();

            public static ReferenceRow FromReference(int index, Reference reference)
            {
                return new ReferenceRow
                {
                    Index = index,
                    Title = Display(reference.Title),
                    Authors = Display(GetAuthors(reference)),
                    Year = Display(GetYear(reference)),
                    Type = Display(reference.Iype),
                    Publisher = Display(reference.Publisher),
                    Identifier = Display(reference.ISBN),
                    Reference = reference
                };
            }
        }
    }
}
