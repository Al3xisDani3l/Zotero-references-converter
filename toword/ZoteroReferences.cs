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
        public PersonZ[] author { get; set; }
        public PersonZ[] editor { get; set; }
        public PersonZ[] translator { get; set; }
        public Date accessed { get; set; }
        public Date issued { get; set; }
    }

    public class Date
    {
        public object[][] dateparts { get; set; }
    }

    public class PersonZ
    {
        public string family { get; set; }
        public string given { get; set; }
    }



}
