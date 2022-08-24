using System.Collections.Generic;

namespace convert
{



    public class Reference
    {
        public string Id { get; set; }
        public string Iype { get; set; }
        public string Abstract { get; set; }
        public string Archive { get; set; }
        public string ArchiveLocation { get; set; }
        public string CallNumber { get; set; }
        public string CollectionNumber { get; set; }
        public string Collectiontitle { get; set; }
        public string Edition { get; set; }
        public string Eventplace { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string Note { get; set; }
        public string NumberOfPages { get; set; }
        public string NumberOfVolumes { get; set; }
        public string Publisher { get; set; }
        public string PublisherPlace { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string TitleShort { get; set; }
        public string URL { get; set; }
        public string Volume { get; set; }
        public List<PersonZ> Author { get; set; }
        public List<PersonZ> Editor { get; set; }
        public List<PersonZ> Translator { get; set; }
        public Date Accessed { get; set; }
        public Date Issued { get; set; }
    }

    public class Date
    {
        public object[][] dateparts { get; set; }
    }

    public class PersonZ
    {
        public string Family { get; set; }
        public string Given { get; set; }
    }



}
