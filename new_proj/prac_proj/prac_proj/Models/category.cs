using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prac_proj.Models
{
	public class category
	{
		[Key]
		public int id { get; set; }

		[Required]
		public String Name { get; set; }

		[DisplayName("Display Order")]
		[Range(1,100,ErrorMessage ="range error 1,100")]
		public int DisplayOrder { get; set; }

		public DateTime CreateddateTime { get; set; } = DateTime.Now;

	
		public category()
		{
		}
	}
}

