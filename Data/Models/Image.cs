using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class SerFile
	{
		public int Id { get; set; }

		public byte[] Bytes { get; set; }

		public string ContentType { get; set; }
	}
}
