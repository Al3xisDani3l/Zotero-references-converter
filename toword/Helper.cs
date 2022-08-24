using convert.xml;
using System;
using System.Collections.Generic;

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
                if (item.Author is not null)
                {
                   
                    List<Person> authors = new List<Person>();
                    foreach (var aut in item.Author)
                    {
                        Person person = new Person() { First = aut.Given, Last = aut.Family };
                        authors.Add(person);
                    }
                    author.NameList = new NameList() { Person = authors };
                }


                //Agregamos los editores
                var editor = new xml.Editor();
                if (item.Editor is not null)
                {
                 
                    List<Person> editores = new List<Person>();
                    foreach (var aut in item.Editor)
                    {
                        Person person = new Person() { First = aut.Given, Last = aut.Family };
                        editores.Add(person);
                    }
                    editor.NameList = new NameList() { Person = editores };
                }



                //agregamos los traductores
                var translator = new xml.Translator();
                if (item.Translator is not null)
                {
                   

                    List<Person> traductores = new List<Person>();
                    foreach (var aut in item.Translator)
                    {
                        Person person = new Person() { First = aut.Given, Last = aut.Family };
                        traductores.Add(person);
                    }
                    translator.NameList = new NameList() { Person = traductores };
                }
               

                var authorBase = new AuthorBase() { Author = author, Editor = editor, Translator = translator };

                newSource.Author = authorBase;

                newSource.City = item.PublisherPlace;
                newSource.Comments = item.Abstract;
                newSource.CountryRegion = item.PublisherPlace;
                if (item.Accessed is not null)
                {
                    if (item.Accessed.dateparts is not null)
                    {
                        if (item.Accessed.dateparts[0][2] is not null)
                        {
                            newSource.DayAccessed = item.Accessed.dateparts[0][2].ToString();
                        }
                        if (item.Accessed.dateparts[0][1] is not null)
                        {
                            newSource.MonthAccessed = item.Accessed.dateparts[0][1].ToString();
                        }
                        if (item.Accessed.dateparts[0][0] is not null)
                        {
                            newSource.YearAccessed = item.Accessed.dateparts[0][0].ToString();
                        }
                    }
                }
             
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
                DateTime date;
                if (item.Issued is not null)
                {
                    if (item.Issued.dateparts is not null)
                    {
                        newSource.Year = item.Issued.dateparts[0][0].ToString();
                        newSource.Tag = item.Title.Substring(0, 4)+ "_"+ newSource.Year;
                    }
                    else
                    {
                        newSource.Tag = item.Title.Substring(0, 5);
                    }
                }           
                else
                {
                    newSource.Tag = item.Title.Substring(0, 5);
                }
          
               



                origin.Add(newSource);




            }




        }


    }
}
