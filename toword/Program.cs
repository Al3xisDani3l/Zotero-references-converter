using System;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using convert.xml;
using System.Xml.Serialization;

namespace convert
{
    class Program
    {
        static void Main(string[] args)
        {
            //Direccion del archivo json de convertiremos de zotero
            string pathExecute = Environment.CurrentDirectory;
            string NameJsonFile = "Elementos exportados.json";
            string FullPathJsonFile = "";

            //obtenemos la direccion de los recursos bibliograficos de Microsoft Word.
            string FullPathWorldXML = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Bibliography\Sources.xml");


            //comprobamos y establecemos los argumentos
            if (args != null)
            {

                if (args.Length > 0)
                {
                    if (args.Length == 1)
                    {
                        NameJsonFile = args[0];
                    }
                    else if (args.Length % 2 == 0)
                    {
                        for (int i = 0; i < args.Length; i++)
                        {
                            if (args[i].ToLower() == "-n")
                            {
                                NameJsonFile = args[i + 1];
                                i++;
                            }
                            if (args[i].ToLower() == "-f")
                            {
                                FullPathJsonFile = args[i + 1];
                                i++;
                            }
                            if (args[i].ToLower() == "-w")
                            {
                                FullPathWorldXML = args[i + 1];
                                i++;
                            }
                        }
                    } 
                }

            }

            if (string.IsNullOrEmpty(FullPathJsonFile))
            {
                FullPathJsonFile = Path.Combine(pathExecute, NameJsonFile);
            }




            //Leemos el archivo json y luego lo serializamos
            List<Reference> referencesByJson = new List<Reference>();
            try
            {
                string JsonString = File.ReadAllText(FullPathJsonFile);
                var result = JsonString.Replace("abstract", "_abstract").Replace("-", ""); //Corrigimos unos errores de formato del archivo json
                referencesByJson = JsonConvert.DeserializeObject<List<Reference>>(result);
                Console.WriteLine($"{referencesByJson.Count} referencias cargadas desde Zotero");
            }
            catch (Exception err)
            {
                Console.WriteLine("Ha ocurrido un error con el archivo json de zotero, verificar formato o ruta.");
                Console.WriteLine(err.Message);
                Environment.Exit(0);
            }

            //leemos el archivo .xml de las referencias de word
            try
            {
                Sources sources;
                var serializer = new XmlSerializer(typeof(Sources));
                using (var filexmlstram = File.OpenRead(FullPathWorldXML))
                {
                    sources = (Sources)serializer.Deserialize(filexmlstram);
                }

                if (sources is not null)
                {
                    Console.WriteLine($"{sources.Source.Count} referencias precargargadas en word");

                    Console.WriteLine($"\n{referencesByJson.Count} nuevas referencias seran agregadas al archivo source.xml\n" +
                        "\nEsto sobrescribira la informacion actual y podria haber perdida de datos" +
                        "\nSe recomienda ampliamente crear una copia de seguridad de su archivo de referencias" +
                        "\n¿Desea continuar con esta operacion? S/N");
                    string confirm = Console.ReadLine();
                    if (confirm.ToLower() == "s")
                    {
                        sources.Source.AddReferences(referencesByJson);
                        using (var overwrite = File.Create(FullPathWorldXML))
                        {
                            serializer.Serialize(overwrite, sources);
                        }

                        Console.WriteLine($"{referencesByJson.Count} nuevas referencias agregadas con exito!");
                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada, Ninguna referencia fue agregada!");
                    }



                }
            }
            catch (Exception err)
            {

                Console.WriteLine("Ha ocurrido un error con el archivo de referencias de word verificar formato o direccion");
                Console.WriteLine(err.Message);
            }
           

           

            


           
            
          

            


           






        }
    }
}
