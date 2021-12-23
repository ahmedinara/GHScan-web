using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Core.Entities
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MobileScannedItem> MobileScannedItems { get; set; }
        public virtual DbSet<ScannedDetial> ScannedDetials { get; set; }
        public virtual DbSet<ScannedHeader> ScannedHeaders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SettingConfig> SettingConfigs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QrCode;Integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MobileScannedItem>(entity =>
            {
                entity.ToTable("MobileScannedItem");

                entity.HasIndex(e => e.CreatedBy, "IX_MobileScannedItem_CreatedBy");

                entity.Property(e => e.QrCode).IsRequired();

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MobileScannedItems)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MobileScannedItem_User");
            });

            modelBuilder.Entity<ScannedDetial>(entity =>
            {
                entity.ToTable("ScannedDetial");

                entity.HasIndex(e => e.ScannedMasterId, "IX_ScannedDetial_ScannedMasterId");

                entity.Property(e => e.QrCode).IsRequired();

                entity.Property(e => e.QrFormat).IsRequired();

                entity.HasOne(d => d.ScannedMaster)
                    .WithMany(p => p.ScannedDetials)
                    .HasForeignKey(d => d.ScannedMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScannedDetial_ScannedHeader");
            });

            modelBuilder.Entity<ScannedHeader>(entity =>
            {
                entity.ToTable("ScannedHeader");

                entity.HasIndex(e => e.CreatedBy, "IX_ScannedHeader_CreatedBy");

                entity.Property(e => e.MailRecive)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MailSent)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ScannedHeaders)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScannedHeader_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FristName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
