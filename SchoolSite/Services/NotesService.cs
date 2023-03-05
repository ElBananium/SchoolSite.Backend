using Data;
using Data.Models;
using SchoolSite.Models;

namespace SchoolSite.Services
{
	public interface INotesService
	{

		public void AddNote(string name, string identificator, string body, Category category);

		public void RemoveNote(int NoteId);

		public Note GetNote(int Noteid);

		public IEnumerable<Note> GetNotesInCategory(Category category);

		public IEnumerable<Note> GetAllNotes();

		public void UpdateNote(int NoteId, NoteAppendModel model);
	}


	public class NotesService : INotesService
	{
		private ApplicationDbContext _context;
		private ICategoriesService _categoriesService;

		public void AddNote(string name, string identificator, string body, Category category)
		{
			if (!_context.Categories.Contains(category)) throw new KeyNotFoundException(); 
			
			_context.Notes.Add(new Note() { CategoryId = category.Id, Body = body, Name = name, Url = category.Url+"/"+identificator});
			_context.SaveChanges();
		}

		public void RemoveNote(int NoteId)
		{
			var note = _context.Notes.FirstOrDefault(x => x.Id == NoteId);
			_context.Notes.Remove(note) ;
			
			if(_context.Notes.Where(x => x.CategoryId == note.CategoryId).Count() == 1)
			{
				_context.Categories.Remove(new Category() { Id= note.CategoryId });
			}
			
			_context.SaveChanges();

		}


		public IEnumerable<Note> GetNotesInCategory(Category category)
		{
			return _context.Notes.Where(x => x.CategoryId== category.Id).ToList();
		}

		public Note GetNote(int NoteId)
		{
			var note = _context.Notes.FirstOrDefault(x => x.Id == NoteId);

			if (note is null) throw new KeyNotFoundException();

			return note;
		}

		public IEnumerable<Note> GetAllNotes()
		{
			return _context.Notes.ToList();
		}

		public void UpdateNote(int NoteId, NoteAppendModel model)
		{
			var note = _context.Notes.FirstOrDefault(x => x.Id == NoteId);

			if (note is null) throw new KeyNotFoundException();

			if(model.Body != "") note.Body = model.Body;
			if(model.Identificator!= "") note.Url = note.Url.Split('/')[0] +"/"+ model.Identificator;
			if(model.Name != "") note.Name= model.Name;

			_context.Notes.Update(note);
			_context.SaveChanges();

		}

		public NotesService(ApplicationDbContext context)
		{
			_context = context;
		}
	}
}
