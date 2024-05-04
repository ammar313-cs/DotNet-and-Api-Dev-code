using System;
using Microsoft.EntityFrameworkCore;
using prac_proj.Models;

namespace prac_proj.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base(options) 
		{

		}

		public DbSet<category> Categories { get; set; }
	}
}

