using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Xml.Serialization;
using ZoteroConverter.Entities.Microsoft;
using ZoteroConverter.Entities.Zotero;

namespace ZoteroConverter
{
    public static partial class Extensions
    {

        public static FileInfo GetSourcesFile()
        {
           return new FileInfo(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Bibliography\Sources.xml")));
        }


        public static List<Reference> GetReferences(this string path)
        {
            FileInfo zoteroFile = new FileInfo(path);
            return zoteroFile.GetReferences();
        }

        public static List<Reference> GetReferences(this FileInfo zoteroFile)
        {
            List<Reference> _referencesByZotero = new();
            try
            {
               
                string jsonRaw = File.ReadAllText(zoteroFile.FullName);
                string jsonClean = jsonRaw.Replace("-", "");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                _referencesByZotero = JsonSerializer.Deserialize<List<Reference>>(jsonClean, options)!;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al leer el archivo de Zotero: " + ex.Message);

            }

           return _referencesByZotero;


        }

        public static Sources? GetSources()
        {

            FileInfo worlFile = GetSourcesFile();

            Sources? sources = null;

            try
            {
                var serializer = new XmlSerializer(typeof(Sources), new XmlRootAttribute("Sources") 
                { 
                    Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography" 
                });

                if (!worlFile.Exists)
                {
                    // Si el archivo no existe, creamos uno nuevo con una estructura básica
                    sources = new Sources
                    {
                        Source = new List<Source>(),
                        SelectedStyle = "IEEE",
                        B = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography"
                        
                    };

                    if (sources.SaveSources())
                    {
                        return sources;
                    } 
                }


                using (var filexmlstram = File.OpenRead(worlFile.FullName))
                {
                    sources = (Sources?)serializer.Deserialize(filexmlstram);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo de Fuentes de Microsoft: " + ex.Message);
            }

        

            return sources; 


        }

        public static bool SaveSources(this Sources sources)
        {
            FileInfo worlFile = GetSourcesFile();
            try
            {
                var serializer = new XmlSerializer(typeof(Sources), new XmlRootAttribute("Sources")
                {
                    Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography"
                });
                using (var filexmlstram = File.Create(worlFile.FullName))
                {
                    serializer.Serialize(filexmlstram, sources);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el archivo de Fuentes de Microsoft: " + ex.Message);
                return false;
            }
            return true;
        }





        public static Source ToSource(this Reference item)
        {

            var newSource = new Source();

            //agregamos los authores
            var author = new Author();
            if (item.Author is not null)
            {

                List<Entities.Microsoft.Person> authors = new List<Entities.Microsoft.Person>();
                foreach (var aut in item.Author)
                {
                    Entities.Microsoft.Person person = new Entities.Microsoft.Person() { First = aut.Given, Last = aut.Family };
                    authors.Add(person);
                }
                author.NameList = new NameList() { Person = authors };
            }


            //Agregamos los editores
            var editor = new Entities.Microsoft.Editor();
            if (item.Editor is not null)
            {

                List<Entities.Microsoft.Person> editores = new List<Entities.Microsoft.Person>();
                foreach (var aut in item.Editor)
                {
                    Entities.Microsoft.Person person = new Entities.Microsoft.Person() { First = aut.Given, Last = aut.Family };
                    editores.Add(person);
                }
                editor.NameList = new NameList() { Person = editores };
            }



            //agregamos los traductores
            var translator = new Entities.Microsoft.Translator();
            if (item.Translator is not null)
            {


                List<Entities.Microsoft.Person> traductores = new List<Entities.Microsoft.Person>();
                foreach (var aut in item.Translator)
                {
                    Entities.Microsoft.Person person = new Entities.Microsoft.Person() { First = aut.Given, Last = aut.Family };
                    traductores.Add(person);
                }
                translator.NameList = new NameList() { Person = traductores };
            }


            var authorBase = new AuthorBase() { Author = author, Editor = editor, Translator = translator };

            newSource.Author = authorBase;

            newSource.City = item.PublisherPlace;
            newSource.Comments = item.Abstract;
            newSource.CountryRegion = item.PublisherPlace;
            newSource.DayAccessed = GetDatePart(item.Accessed, 2);
            newSource.MonthAccessed = GetDatePart(item.Accessed, 1);
            newSource.YearAccessed = GetDatePart(item.Accessed, 0);

            newSource.DOI = item.ISBN;
            newSource.Edition = item.Edition;
            newSource.Guid = Guid.NewGuid().ToString();
            newSource.Medium = "Zotero";
            newSource.NumberVolumes = item.NumberOfVolumes;
            newSource.Pages = item.NumberOfPages;
            newSource.Publisher = item.Publisher;
            newSource.ShortTitle = item.TitleShort;
            newSource.SourceType = item.Iype;
            newSource.StandardNumber = item.CollectionNumber;
            newSource.StateProvince = item.PublisherPlace;
            newSource.URL = item.URL;
            newSource.Title = item.Title;
            newSource.Volume = item.Volume;
            newSource.Year = GetDatePart(item.Issued, 0);
            newSource.Tag = CreateTag(item.Title, newSource.Year);


            return newSource;


        }

        public static List<Source> ToSources(this IList<Reference> references)
        {
            List<Source> sources = new List<Source>();
            foreach (var item in references)
            {
                sources.Add(item.ToSource());
            }
            return sources;
        }


        public static void AddReferences(this IList<Source> origin, IList<Reference> references)
        {

            foreach (var item in references)
            {
                origin.Add(item.ToSource());
            }


        }

        private static string GetDatePart(Date? date, int partIndex)
        {
            if (date?.dateparts is null || date.dateparts.Length == 0 || date.dateparts[0].Length <= partIndex)
            {
                return string.Empty;
            }

            return date.dateparts[0][partIndex]?.ToString() ?? string.Empty;
        }

        private static string CreateTag(string? title, string? year)
        {
            var cleanTitle = string.IsNullOrWhiteSpace(title) ? "Zotero" : title.Trim();
            var tagPrefixLength = Math.Min(5, cleanTitle.Length);
            var tag = cleanTitle.Substring(0, tagPrefixLength);

            return string.IsNullOrWhiteSpace(year) ? tag : $"{tag}_{year}";
        }


    }
}
