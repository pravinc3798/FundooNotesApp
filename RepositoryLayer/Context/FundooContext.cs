using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        //Added Microsoft.Entityframeworkcore 3.1.0
        //Added Microsoft.Entityframeworkcore.Relation 3.1.0
        //Added Microsoft.Entityframeworkcore.SqlServer 3.1.0
        //Added Microsoft.Entityframeworkcore.Tools 3.1.0
        //Added Microsoft.EntityFrameworkCore.Design 3.1.0
        //services.AddDbContext<FundooContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:FundooDB"])); - Startup.configureservices
        //Add-Migration 
        //Update-Database
        public FundooContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<NoteEntity> NoteTable { get; set; }
        public DbSet<CollabEntity> CollabTable { get; set; }
    }
}
