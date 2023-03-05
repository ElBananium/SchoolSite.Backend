using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SchoolSite.Models;
using SchoolSite.Services;
using System.IO;

namespace SchoolSite.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private ICategoriesService _cateogoriesService;
		private INotesService _notesService;
		private IFilesService _imageService;

		public UserController(ICategoriesService categoriesService, INotesService notesService, IFilesService imageService)
		{
			_cateogoriesService = categoriesService;

			_notesService = notesService;
			_imageService = imageService;
		}


		[HttpGet("routes")]
		public ActionResult<IEnumerable<RoutesModel>> GetNavigation()
		{
			var categories = _cateogoriesService.GetAllCategories();

			var notes = _notesService.GetAllNotes();

			var routes = new List<RoutesModel>();

			foreach(var category in categories)
			{
				var resultNotes = new List<NotesModel>();

				

				

				foreach (var note in notes.Where(x => x.CategoryId == category.Id))
				{
					resultNotes.Add(new NotesModel() { Id = note.Id,Name = note.Name, Url = note.Url});
				}

				routes.Add(new RoutesModel() { Id = category.Id, Name = category.Name, Url = category.Url, NotesUrls = resultNotes });
			}

			return Ok(routes);


		}

		[HttpGet("note/{NoteId}")]
		public ActionResult<string> GetNoteBody(int NoteId)
		{
			return Ok(_notesService.GetNote(NoteId).Body);
		}


		[HttpGet("file/{fileId}")]
		public ActionResult<IFormFile> GetImage(int fileId)
		{


			var image = _imageService.GetFileById(fileId);
			Console.WriteLine(image.ContentType);
			return File(image.Bytes, image.ContentType, fileId.ToString() + "." + image.ContentType.Split("/")[1]);
		}
	}


	
}
