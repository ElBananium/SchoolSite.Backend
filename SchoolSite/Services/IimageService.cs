using Data;
using Data.Models;
using System;
using System.Collections;
using System.IO;

namespace SchoolSite.Services
{
	public interface IFilesService
	{
		public int AddFile(IFormFile image);

		public SerFile GetFileById(int id);

		public IEnumerable<int> GetFilesId();
	}


	public class FilesService : IFilesService
	{
		private ApplicationDbContext _context;

		public FilesService(ApplicationDbContext context)
		{
			_context = context;
		}


		public int AddFile(IFormFile image)
		{
			byte[] imageData = null;

			

			using (var binaryReader = new BinaryReader(image.OpenReadStream()))
			{
				imageData = binaryReader.ReadBytes((int)image.Length);
			}
			var img = new SerFile() { Bytes = imageData, ContentType = image.ContentType };
			_context.Files.Add(img);
			_context.SaveChanges();
			return img.Id;
		}



		public IEnumerable<int> GetFilesId()
		{
			return _context.Files.Select(x => x.Id);
		}

		public SerFile GetFileById(int id)
		{
			return _context.Files.FirstOrDefault(x => x.Id== id);
			
			
		}
	}
}
