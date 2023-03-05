using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSite.Models;
using SchoolSite.Services;

namespace SchoolSite.Controllers
{
	[Route("api/admin")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private ICategoriesService _cateogoriesService;
		private INotesService _notesService;
		private IFilesService _imageService;

		public AdminController(ICategoriesService categoriesService, INotesService notesService, IFilesService imageService)
		{
			_cateogoriesService = categoriesService;

			_notesService = notesService;
			_imageService = imageService;
		}


		[HttpPost("category")]
		public ActionResult AddCategory([FromBody] CategoryModel category)
		{
			_cateogoriesService.AddCategory(category.Identificator, category.Name, category.Position);

			return Ok();
		}

		[HttpDelete("category/{id}")]
		public ActionResult DeleteCategory(int id)
		{


			_cateogoriesService.RemoveCategory(id);

			return Ok();
		}

		[HttpPost("category/{CategoryId}/note")]

		public ActionResult AddNote(int CategoryId, [FromBody] NoteAppendModel noteAppendModel)
		{
			var category = _cateogoriesService.GetCategoryById(CategoryId);

			_notesService.AddNote(noteAppendModel.Name, noteAppendModel.Identificator, noteAppendModel.Body, category);

			return Ok();
		}

		[HttpDelete("category/{CategoryId}/{NoteId}")]
		public ActionResult DeleteNote(int CategoryId, int NoteId) 
		{
			

			_notesService.RemoveNote(NoteId);


			return Ok();



		}
		[HttpPatch("category/{CategoryId}/{NoteId}")]
		public ActionResult UpdateNote(int CategoryId, int NoteId, [FromBody] NoteAppendModel model)
		{
			_notesService.UpdateNote(NoteId, model);

			return Ok();
		}


		[HttpPost("file")]
		public ActionResult<int> AddImage(IFormFile image)
		{
			var result = _imageService.AddFile(image);
			
			return Ok(result);
		}


	}
}
