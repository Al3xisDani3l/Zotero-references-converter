using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;



namespace convert.xml
{




	[XmlRoot(ElementName = "Person", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class Person
	{
		[XmlElement(ElementName = "Last", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Last { get; set; }
		[XmlElement(ElementName = "Middle", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Middle { get; set; }
		[XmlElement(ElementName = "First", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string First { get; set; }
	}

	[XmlRoot(ElementName = "NameList", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class NameList
	{
		[XmlElement(ElementName = "Person", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public List<Person> Person { get; set; }
	}

	

	[XmlRoot(ElementName = "Author", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class AuthorBase
    {
		[XmlElement(ElementName = "Author", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public Author Author { get; set; }
		[XmlElement(ElementName = "Editor", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public Editor Editor { get; set; }
		[XmlElement(ElementName = "Translator", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public Translator Translator { get; set; }
	}

	[XmlRoot(ElementName = "Author", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class Author
	{
		[XmlElement(ElementName = "NameList", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public NameList NameList { get; set; }


	}

	[XmlRoot(ElementName = "Editor", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class Editor
	{
		[XmlElement(ElementName = "NameList", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public NameList NameList { get; set; }
	}

	[XmlRoot(ElementName = "Source", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class Source
	{
		[XmlElement(ElementName = "Tag", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Tag { get; set; }
		[XmlElement(ElementName = "SourceType", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string SourceType { get; set; }
		[XmlElement(ElementName = "Guid", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Guid { get; set; }
		[XmlElement(ElementName = "Title", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Title { get; set; }
		[XmlElement(ElementName = "Year", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Year { get; set; }
		[XmlElement(ElementName = "Author", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public AuthorBase Author { get; set; }
		[XmlElement(ElementName = "City", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string City { get; set; }
		[XmlElement(ElementName = "Publisher", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Publisher { get; set; }
		[XmlElement(ElementName = "StateProvince", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string StateProvince { get; set; }
		[XmlElement(ElementName = "CountryRegion", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string CountryRegion { get; set; }
		[XmlElement(ElementName = "Volume", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Volume { get; set; }
		[XmlElement(ElementName = "NumberVolumes", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string NumberVolumes { get; set; }
		[XmlElement(ElementName = "ShortTitle", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string ShortTitle { get; set; }
		[XmlElement(ElementName = "StandardNumber", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string StandardNumber { get; set; }
		[XmlElement(ElementName = "Pages", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Pages { get; set; }
		[XmlElement(ElementName = "Edition", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Edition { get; set; }
		[XmlElement(ElementName = "Comments", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Comments { get; set; }
		[XmlElement(ElementName = "Medium", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string Medium { get; set; }
		[XmlElement(ElementName = "YearAccessed", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string YearAccessed { get; set; }
		[XmlElement(ElementName = "MonthAccessed", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string MonthAccessed { get; set; }
		[XmlElement(ElementName = "DayAccessed", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string DayAccessed { get; set; }
		[XmlElement(ElementName = "URL", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string URL { get; set; }
		[XmlElement(ElementName = "DOI", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public string DOI { get; set; }
	}

	[XmlRoot(ElementName = "Translator", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class Translator
	{
		[XmlElement(ElementName = "NameList", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public NameList NameList { get; set; }
	}

	[XmlRoot(ElementName = "Sources", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class Sources
	{
		[XmlElement(ElementName = "Source", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
		public List<Source> Source { get; set; }
		[XmlAttribute(AttributeName = "SelectedStyle")]
		public string SelectedStyle { get; set; }
		[XmlAttribute(AttributeName = "b", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string B { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}



}
