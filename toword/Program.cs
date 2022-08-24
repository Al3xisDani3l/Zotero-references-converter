using convert;
using convert.xml;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace zwconverter
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {

            var rootCommand = new RootCommand("Exportador de referencias de Zotero a referencias compatibles con el modelo de Microsoft Word.");

            var zoteroFile = new Option<FileInfo>(
                name: "--filezotero",
                description: "El archivo .json generado por Zotero y que sera exportado.",
                getDefaultValue: () => new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Elementos exportados.json"))
                );
            zoteroFile.AddAlias("-fz");

            var worldFile = new Option<FileInfo>(
               name: "--fileWorld",
               description: "El archivo Source.xml al que se le agregaran las referencias.",
               getDefaultValue: () => new FileInfo(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Bibliography\Sources.xml")))
               );
            worldFile.AddAlias("-fw");

            var from = new Option<uint?>(
               name: "--from",
               description: "Indice de la primera referenca a agregar",
               getDefaultValue: () => 0


               );
            from.AddAlias("-fr");

            var to = new Option<uint?>(
              name: "--to",
              description: "Indice de la ultima referenca a agregar",
              getDefaultValue: () => 0
              );
            to.AddAlias("-t");

            var readOrigin = new Option<bool>(
             name: "--readOrigin",
             description: "Muestra todas las referencias de zotero indexadas.",
             getDefaultValue: () => true

             ); 
            readOrigin.AddAlias("-r");
           


            rootCommand.AddOption(zoteroFile);
            rootCommand.AddOption(worldFile);
            rootCommand.AddOption(from);
            rootCommand.AddOption(to);
            rootCommand.AddOption(readOrigin);

          
            rootCommand.SetHandler(async (a, b, c, d, e) => await Export(a, b, c, d, e), zoteroFile, worldFile, from, to, readOrigin);

            return await rootCommand.InvokeAsync(args);





        }

        internal static async Task Export(FileInfo zoteroFile, FileInfo worlFile, uint? from, uint? to, bool readOrigin)
        {

            List<Reference> _referencesByZotero = new();
            List<Reference> _referencesByZoteroChunk = new();
            try
            {
                string jsonRaw = File.ReadAllText(zoteroFile.FullName);
                string jsonClean = jsonRaw.Replace("-", "");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                _referencesByZotero = JsonSerializer.Deserialize<List<Reference>>(jsonClean,options);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Info: {_referencesByZotero?.Count ?? 0} referencias cargadas desde zotero");
                
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                int _from = (int)(from ?? 0);
                int _to = (int)(_referencesByZotero.Count - _from - to);


                _referencesByZoteroChunk = _referencesByZotero.GetRange(_from, _to);



                if (readOrigin)
                {
                    int i = 0;
                    foreach (var zoteroReference in _referencesByZotero)
                    {
                        Console.WriteLine($"Referencia[{i++}]: {{[Title]:{zoteroReference.Title},[Autor]: {zoteroReference.Author}}}");

                    }
                }
               
                    Sources sources;
                    var serializer = new XmlSerializer(typeof(Sources));
                    using (var filexmlstram = File.OpenRead(worlFile.FullName))
                    {
                        sources = (Sources)serializer.Deserialize(filexmlstram);
                    }

                    if (sources is not null)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Info: {sources.Source.Count} referencias precargargadas en word");

                        Console.WriteLine($"\n{_referencesByZoteroChunk.Count} nuevas referencias seran agregadas al archivo source.xml\n" +
                        "\nEsto sobrescribira la informacion actual y podria haber perdida de datos" +
                        "\nSe recomienda ampliamente crear una copia de seguridad de su archivo de referencias" +
                        "\n¿Desea continuar con esta operacion? S/N");
                        string confirm = Console.ReadLine();
                        if (confirm.ToLower() == "s")
                        {
                            sources.Source.AddReferences(_referencesByZoteroChunk);
                            using (var overwrite = File.Create(worlFile.FullName))
                            {
                                serializer.Serialize(overwrite, sources);
                            }

                            Console.WriteLine($"{_referencesByZoteroChunk.Count} nuevas referencias agregadas con exito!");
                        }
                        else
                        {
                            Console.WriteLine("Operacion cancelada, Ninguna referencia fue agregada!");
                        }


                    

                }





            }
            catch (Exception err)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(err.Message);
                Console.ForegroundColor = ConsoleColor.White;

            }
            
             await Task.CompletedTask;

        }




    }
}
