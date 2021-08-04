using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using convert.xml;

namespace convert
{
    public static  class Helper
    {


        public static void AddReferences(this IList<Source> origin, IList<Reference> references)
        {

            foreach (var item in references)
            {
                var newSource = new Source();

                //agregamos los authores
                var author = new xml.Author();
                if (item.author is not null)
                {
                   
                    List<Person> authors = new List<Person>();
                    foreach (var aut in item.author)
                    {
                        Person person = new Person() { First = aut.given, Last = aut.family };
                        authors.Add(person);
                    }
                    author.NameList = new NameList() { Person = authors };
                }


                //Agregamos los editores
                var editor = new xml.Editor();
                if (item.editor is not null)
                {
                 
                    List<Person> editores = new List<Person>();
                    foreach (var aut in item.editor)
                    {
                        Person person = new Person() { First = aut.given, Last = aut.family };
                        editores.Add(person);
                    }
                    editor.NameList = new NameList() { Person = editores };
                }



                //agregamos los traductores
                var translator = new xml.Translator();
                if (item.translator is not null)
                {
                   

                    List<Person> traductores = new List<Person>();
                    foreach (var aut in item.translator)
                    {
                        Person person = new Person() { First = aut.given, Last = aut.family };
                        traductores.Add(person);
                    }
                    translator.NameList = new NameList() { Person = traductores };
                }
               

                var authorBase = new AuthorBase() { Author = author, Editor = editor, Translator = translator };

                newSource.Author = authorBase;

                newSource.City = item.publisherplace;
                newSource.Comments = item.note;
                newSource.CountryRegion = item.publisherplace;
                if (item.accessed is not null)
                {
                    if (item.accessed.dateparts is not null)
                    {
                        if (item.accessed.dateparts[0][2] is not null)
                        {
                            newSource.DayAccessed = item.accessed.dateparts[0][2].ToString();
                        }
                        if (item.accessed.dateparts[0][1] is not null)
                        {
                            newSource.MonthAccessed = item.accessed.dateparts[0][1].ToString();
                        }
                        if (item.accessed.dateparts[0][0] is not null)
                        {
                            newSource.YearAccessed = item.accessed.dateparts[0][0].ToString();
                        }
                    }
                }
             
                newSource.DOI = item.ISBN;
                newSource.Edition = item.edition;
                newSource.Guid = Guid.NewGuid().ToString();
                newSource.Medium = "Zotero";
                newSource.NumberVolumes = item.numberofvolumes;
                newSource.Pages = item.numberofpages;
                newSource.Publisher = item.publisher;
                newSource.ShortTitle = item.titleshort;
                newSource.SourceType = item.type;
                newSource.StandardNumber = item.collectionnumber;
                newSource.StateProvince = item.publisherplace;
                newSource.URL = item.URL;
                newSource.Title = item.title;
                newSource.Volume = item.volume;
                DateTime date;
                if (item.issued is not null)
                {
                    if (DateTime.TryParse(item.issued.literal ?? "", out date))
                    {
                        newSource.Year = date.Year.ToString();
                        newSource.Tag = item.title.Substring(0, 3) + newSource.Year;
                    }
                    else
                    {
                        newSource.Tag = item.title.Substring(0, 5);
                    }
                }           
                else
                {
                    newSource.Tag = item.title.Substring(0, 5);
                }
          
               



                origin.Add(newSource);




            }




        }


    }
}
