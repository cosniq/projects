using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RepositoryLayer
{
    public partial class TIAProjContext : DbContext
    {
        public TIAProjContext()
        {
        }

        public TIAProjContext(DbContextOptions<TIAProjContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CounterTypesView> CounterTypesViews { get; set; } = null!;
        public virtual DbSet<CountersView> CountersViews { get; set; } = null!;
        public virtual DbSet<IndexReadingsView> IndexReadingsViews { get; set; } = null!;
        public virtual DbSet<InvoiceTypesView> InvoiceTypesViews { get; set; } = null!;
        public virtual DbSet<InvoicesView> InvoicesViews { get; set; } = null!;
        public virtual DbSet<LocInvTypeView> LocInvTypeViews { get; set; } = null!;
        public virtual DbSet<LocationsView> LocationsViews { get; set; } = null!;
        public virtual DbSet<MonthsView> MonthsViews { get; set; } = null!;
        public virtual DbSet<UnitsOfMeasurementView> UnitsOfMeasurementViews { get; set; } = null!;
        public virtual DbSet<UsersView> UsersViews { get; set; } = null!;
        public virtual DbSet<UsersWithDetailsView> UsersWithDetailsViews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2NBDDB1;Database=TIAProj;Trusted_Connection=No;User Id=TiaProjBackend;Password=TiaProjBackend");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CounterTypesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Counter_Types_View");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UnitOfMeasurement).HasColumnName("Unit_of_measurement");
            });

            modelBuilder.Entity<CountersView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Counters_View");

                entity.Property(e => e.CounterType).HasColumnName("Counter_Type");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.SerialNr)
                    .HasMaxLength(50)
                    .HasColumnName("Serial_Nr");
            });

            modelBuilder.Entity<IndexReadingsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Index_Readings_View");

                entity.Property(e => e.DateOfReading)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_Reading");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.NumberOnCounter).HasColumnName("Number_on_Counter");

                entity.Property(e => e.Invoice).HasColumnName("Invoice");
            });

            modelBuilder.Entity<InvoiceTypesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Invoice_Types_View");

                entity.Property(e => e.CostOnlyDependentOnUsage).HasColumnName("Cost_only_dependent_on_usage");

                entity.Property(e => e.CounterType).HasColumnName("Counter_Type");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<InvoicesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Invoices_View");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceType).HasColumnName("Invoice_Type");
            });

            modelBuilder.Entity<LocInvTypeView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LocInvType_View");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceType).HasColumnName("Invoice_Type");

                entity.Property(e => e.Active).HasColumnName("Active");
            });

            modelBuilder.Entity<LocationsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Locations_View");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<MonthsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Months_View");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UnitsOfMeasurementView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Units_of_measurement_View");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Symbol).HasMaxLength(50);
            });

            modelBuilder.Entity<UsersView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Users_View");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UsersWithDetailsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Users_With_Details_View");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_Birth");

                entity.Property(e => e.Email).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
