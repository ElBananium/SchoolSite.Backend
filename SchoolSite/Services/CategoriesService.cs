using Data;
using Data.Models;

namespace SchoolSite.Services
{
	public interface ICategoriesService
	{
		public IEnumerable<Category> GetAllCategories();
		

		public Category GetCategoryById(int id);

		public void AddCategory(string identificator, string name, int position);



		public void RemoveCategory(int id);
	}


	public class CategoriesService : ICategoriesService
	{
		private ApplicationDbContext _context;
		private INotesService _notesService;

		public CategoriesService(ApplicationDbContext context, INotesService notesService)
		{
			_context = context;
			_notesService = notesService;
		}

		public void AddCategory(string identificator, string name, int position) 
		{
			var category = new Category() { Name = name, Url = identificator, Position = position };
			_context.Categories.Add(category);


			_context.SaveChanges();

			_notesService.AddNote("Пустая запись", "blank"+category.Id, "", category);
			_context.SaveChanges();
		
		}

		public IEnumerable<Category> GetAllCategories()
		{
			

			return _context.Categories.ToList();
		}

		public Category GetCategoryById(int id)
		{

			var category = _context.Categories.FirstOrDefault(x => x.Id == id);

			if(category is null) throw new KeyNotFoundException();

			return category;
			
		}

		public void RemoveCategory(int id)
		{
			_context.Notes.RemoveRange(_context.Notes.Where(x => x.CategoryId == id).ToList());
			_context.Categories.Remove(new Category() { Id= id });
			_context.SaveChanges();
		}
	}
}
