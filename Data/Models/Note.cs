using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class Note
	{
		public int Id { get; set; }

		public int CategoryId { get; set; }

		public string Name { get; set; }

		public string Url { get; set; }

		[Column(TypeName = "Text")]
		public string Body { get; set; } 
	}
}
