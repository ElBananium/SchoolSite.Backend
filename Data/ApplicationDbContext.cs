using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class ApplicationDbContext : DbContext
	{

		public DbSet<SerFile> Files { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Note> Notes { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			Database.EnsureCreated();
			
		}
	}
}
