using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class db_WebElerningContext : DbContext
    {
        public db_WebElerningContext()
        {
        }

        public db_WebElerningContext(DbContextOptions<db_WebElerningContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=TRANVUHAO;Database=db_WebElerning;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.RoleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RoleID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_Roles");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK_Caterogies");

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.CatName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(100);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.CourseName).HasMaxLength(250);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ShortDesc).HasMaxLength(250);

                entity.Property(e => e.Thumb).HasMaxLength(250);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_Courses_Caterogies");
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Forms_Courses");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.StudentBirthday).HasColumnType("date");

                entity.Property(e => e.StudentEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StudentName).HasMaxLength(50);

                entity.Property(e => e.StudentPhone).HasMaxLength(12);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_OrderDetails_Courses");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Orders");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.PageId).HasColumnName("PageID");

                entity.Property(e => e.PageName)
                    .HasMaxLength(250)
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsFixedLength(true);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.Salt)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
