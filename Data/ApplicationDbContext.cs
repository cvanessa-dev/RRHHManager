using RRHHManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RRHHManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Vacaciones> Vacaciones { get; set; }
        public DbSet<Constancia> Constancias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<AntiguedadLaboral> AntiguedadLaborals { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<ExpedientesDigitales> expedientesDigitals { get; set; }
        public DbSet<HistorialLaboral> HistorialLaborals { get; set; }
        public DbSet<HistorialSalarial> HistorialSalarials { get; set; }
        public DbSet<Permisos> Permiso { get; set; }
        public DbSet<Reportes> Reportes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { modelBuilder.Entity<Puesto>().HasOne(p => p.Departamento).WithMany().HasForeignKey(p => p.DepartamentoId).OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Empleado>().HasOne(e => e.Puesto).WithMany(p => p.Empleados).HasForeignKey(e => e.PuestoId).OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Empleado>().HasOne(e => e.Departamento).WithMany(d => d.Empleados).HasForeignKey(e => e.DepartamentoId).OnDelete(DeleteBehavior.Restrict); }
        public DbSet<RRHHManager.Models.Exportaciones> Exportaciones { get; set; } = default!;
    }
}
