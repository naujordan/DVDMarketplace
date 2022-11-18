using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JTN.DVDCentral.PL
{
    public partial class DVDCentralEntities : DbContext
    {
        public DVDCentralEntities()
        {
        }

        public DVDCentralEntities(DbContextOptions<DVDCentralEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<tblCustomer> tblCustomers { get; set; } = null!;
        public virtual DbSet<tblDirector> tblDirectors { get; set; } = null!;
        public virtual DbSet<tblFormat> tblFormats { get; set; } = null!;
        public virtual DbSet<tblGenre> tblGenres { get; set; } = null!;
        public virtual DbSet<tblMovie> tblMovies { get; set; } = null!;
        public virtual DbSet<tblMovieGenre> tblMovieGenres { get; set; } = null!;
        public virtual DbSet<tblOrder> tblOrders { get; set; } = null!;
        public virtual DbSet<tblOrderItem> tblOrderItems { get; set; } = null!;
        public virtual DbSet<tblRating> tblRatings { get; set; } = null!;
        public virtual DbSet<tblUser> tblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JTN.DVDCentral.DB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCustomer>(entity =>
            {
                entity.ToTable("tblCustomer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblDirector>(entity =>
            {
                entity.ToTable("tblDirector");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblFormat>(entity =>
            {
                entity.ToTable("tblFormat");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblGenre>(entity =>
            {
                entity.ToTable("tblGenre");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblMovie>(entity =>
            {
                entity.ToTable("tblMovie");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblMovieGenre>(entity =>
            {
                entity.ToTable("tblMovieGenre");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<tblOrder>(entity =>
            {
                entity.ToTable("tblOrder");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<tblOrderItem>(entity =>
            {
                entity.ToTable("tblOrderItem");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<tblRating>(entity =>
            {
                entity.ToTable("tblRating");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(28)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
