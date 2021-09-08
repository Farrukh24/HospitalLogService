using HospitalLogService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //TODO leanr what is dbset
        public DbSet<Log> Logs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Visitor> Visitors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //build department model
            modelBuilder.Entity<Department>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Name).IsRequired().HasMaxLength(32);
               entity.HasIndex(e => e.Name).IsUnique();
           });


            //build visitor model
            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(32);

                // entity.HasOne(a => a.Type).WithMany(b => b.Visitor).HasForeignKey(a => a.LogFK).IsRequired();


            });

            //build log model
            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedOn).IsRequired();
                entity.Property(e => e.Purpose).IsRequired().HasMaxLength(128);

                entity.HasOne(a => a.Visitor).WithMany(b => b.Logs).HasForeignKey(a => a.VisitorId);
                entity.HasOne(a => a.Department).WithMany(b => b.Logs).HasForeignKey(a => a.DepartmentId);
            });
        }

    }
}
