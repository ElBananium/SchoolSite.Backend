using System.Text.Json.Serialization;

namespace SchoolSite.Models
{


	public class RoutesModel
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public string Url { get; set; }

		[JsonPropertyName("children")]
		public IEnumerable<NotesModel> NotesUrls { get; set; }



	}


	public class CategoryModel
	{

		public string Name { get; set; }

		public string Identificator { get; set; }

		public int Position { get; set; }
	}

	public class NoteAppendModel
	{
		public string Name { get; set; }

		public string Identificator { get; set; }

		public string Body { get; set; }

	}

	public class NotesModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Url { get; set; }
	}
}
