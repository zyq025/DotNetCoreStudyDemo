using EFCoreTestModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreTestRespository
{
    public class MyTestDbContext:DbContext
    {
        // 构造函数，传入一个option，用于传递数据库连接字符串
        public MyTestDbContext(DbContextOptions<MyTestDbContext> options)
            :base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("SYS_USER");// 设置表名
                entity.HasComment("用户表");// 设置表注释

                entity.Property(e => e.Id) 
                    .HasColumnName("ID")// 设置列名
                    .HasColumnType("varchar(128)");//设置列类型和长度

                entity.Property(e => e.UserName)
                   .HasColumnName("USER_NAME")// 设置列名
                   .HasColumnType("varchar(128)");//设置列类型和长度

                entity.Property(e => e.UserPwd)
                  .HasColumnName("USER_PWD")// 设置列名
                  .HasColumnType("varchar(128)")//设置列类型和长度
                  .IsRequired();// 设置为必填，即不为空

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
