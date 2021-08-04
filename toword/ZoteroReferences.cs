using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace convert
{
   
       

        public class Reference
        {
            public string id { get; set; }
            public string type { get; set; }
            public string _abstract { get; set; }
            public string archive { get; set; }
            public string archive_location { get; set; }
            public string callnumber { get; set; }
            public string collectionnumber { get; set; }
            public string collectiontitle { get; set; }
            public string edition { get; set; }
            public string eventplace { get; set; }
            public string ISBN { get; set; }
            public string language { get; set; }
            public string note { get; set; }
            public string numberofpages { get; set; }
            public string numberofvolumes { get; set; }
            public string publisher { get; set; }
            public string publisherplace { get; set; }
            public string source { get; set; }
            public string title { get; set; }
            public string titleshort { get; set; }
            public string URL { get; set; }
            public string volume { get; set; }
            public Author[] author { get; set; }
            public Editor[] editor { get; set; }
            public Translator[] translator { get; set; }
            public Accessed accessed { get; set; }
            public Issued issued { get; set; }
        }

        public class Accessed
        {
            public object[][] dateparts { get; set; }
        }

        public class Issued
        {
            public string literal { get; set; }
        }

        public class Author
        {
            public string family { get; set; }
            public string given { get; set; }
        }

        public class Editor
        {
            public string family { get; set; }
            public string given { get; set; }
        }

        public class Translator
        {
            public string family { get; set; }
            public string given { get; set; }
        }

    
}
