using EFCoreTestModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreTestRespository
{
    public class MyTestDbContext:DbContext
    {
       

        public MyTestDbContext(DbContextOptions<MyTestDbContext> options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>(entity=> {
            //    entity.ToTable("SYS_USER");
            //    entity.HasComment("用户表");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("ID")
            //        .HasColumnType("varchar(128)");
                    
                    
            //});
            base.OnModelCreating(modelBuilder);
        }

    }
}
