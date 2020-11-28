using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Dominio.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistencia
{
     public class ContextoCurso:IdentityDbContext<Usuario>
    {
        public ContextoCurso(DbContextOptions options): base (options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstructorCurso>().HasKey(prop => new { prop.Cursoid, prop.Instructorid });
            modelBuilder.Entity<Precio>().HasOne(prop => prop.Tcurso).WithOne(prop => prop.Tprecio)
                .HasForeignKey<Precio>(a => a.Cursoid);
            modelBuilder.Entity<Comentario>().HasOne(prop => prop.Tcurso).WithMany(a => a.Comentarios).
                HasForeignKey(a => a.Cursoid).IsRequired(true);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Comentario> TComentario { get; set; }
        public DbSet<Curso> TCurso { get; set; }
        public DbSet<Precio> TPrecio { get; set; }
        public DbSet<InstructorCurso> InstructorCurso { get; set; }
    }
}
