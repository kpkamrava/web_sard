using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace web_db
{
    public partial class sardweb_Context : DbContext
    {
        public sardweb_Context()
        {
        }

        public sardweb_Context(DbContextOptions<sardweb_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCar> TblCar { get; set; }
        public virtual DbSet<TblConfig> TblConfig { get; set; }
        public virtual DbSet<TblContract> TblContract { get; set; }
        public virtual DbSet<TblContractPacking> TblContractPacking { get; set; }
        public virtual DbSet<TblContractProduct> TblContractProduct { get; set; }
        public virtual DbSet<TblContractType> TblContractType { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<TblInjury> TblInjury { get; set; }
        public virtual DbSet<TblLocation> TblLocation { get; set; }
        public virtual DbSet<TblPacking> TblPacking { get; set; }
        public virtual DbSet<TblPortage> TblPortage { get; set; }
        public virtual DbSet<TblPortageDocument> TblPortageDocument { get; set; }
        public virtual DbSet<TblPortageRow> TblPortageRow { get; set; }
        public virtual DbSet<TblPortageRowInjury> TblPortageRowInjury { get; set; }
        public virtual DbSet<TblPortageRowLocation> TblPortageRowLocation { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblSalMali> TblSalMali { get; set; }
        public virtual DbSet<TblStoreLog> TblStoreLog { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserPermis> TblUserPermis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=sardsib_db;user=sa;password=8001721");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCar>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Img).HasColumnType("image");

                entity.Property(e => e.IsDel).HasColumnName("isDel");

                entity.Property(e => e.PriceTowBascol)
                    .HasColumnName("priceTowBascol")
                    .HasColumnType("decimal(21, 3)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblConfig>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ApiPass).HasMaxLength(200);

                entity.Property(e => e.ApiUrlPaivest).HasMaxLength(200);

                entity.Property(e => e.ApiUser).HasMaxLength(200);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TblContract>(entity =>
            {
                entity.HasIndex(x => new { x.FkCustomer, x.Code })
                    .HasName("IX_TblContract")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Azdate)
                    .HasColumnName("azdate")
                    .HasColumnType("date");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dateadd)
                    .HasColumnName("dateadd")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dateedit)
                    .HasColumnName("dateedit")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkContractType).HasColumnName("fkContractType");

                entity.Property(e => e.FkCustomer).HasColumnName("fkCustomer");

                entity.Property(e => e.FkSalmali).HasColumnName("fkSalmali");

                entity.Property(e => e.FkUsAdd).HasColumnName("fkUsAdd");

                entity.Property(e => e.FkUsEdit).HasColumnName("fkUsEdit");

                entity.Property(e => e.PriceOfBoxIn)
                    .HasColumnName("priceOfBoxIn")
                    .HasColumnType("decimal(22, 4)");

                entity.Property(e => e.PriceOfBoxOut)
                    .HasColumnName("priceOfBoxOut")
                    .HasColumnType("decimal(22, 4)");

                entity.Property(e => e.PriceOfKiloIn)
                    .HasColumnName("priceOfKiloIn")
                    .HasColumnType("decimal(22, 4)");

                entity.Property(e => e.PriceOfKiloOut)
                    .HasColumnName("priceOfKiloOut")
                    .HasColumnType("decimal(22, 4)");

                entity.Property(e => e.SumInCount).HasColumnName("_SumInCount");

                entity.Property(e => e.SumInWeight)
                    .HasColumnName("_SumInWeight")
                    .HasColumnType("decimal(22, 4)");

                entity.Property(e => e.SumOutCount).HasColumnName("_SumOutCount");

                entity.Property(e => e.SumOutWeight)
                    .HasColumnName("_SumOutWeight")
                    .HasColumnType("decimal(22, 4)");

                entity.Property(e => e.Tadate)
                    .HasColumnName("tadate")
                    .HasColumnType("date");

                entity.Property(e => e.Txt)
                    .HasColumnName("txt")
                    .HasMaxLength(500);

                entity.Property(e => e.WeightMaxIn).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeightMaxOut).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.FkContractTypeNavigation)
                    .WithMany(p => p.TblContract)
                    .HasForeignKey(x => x.FkContractType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContract_TblContractType");

                entity.HasOne(d => d.FkCustomerNavigation)
                    .WithMany(p => p.TblContract)
                    .HasForeignKey(x => x.FkCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContract_TblCustomer");
            });

            modelBuilder.Entity<TblContractPacking>(entity =>
            {
                entity.HasKey(x => new { x.FkContract, x.FkPacking });

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.FkPacking).HasColumnName("fkPacking");

                entity.HasOne(d => d.FkContractNavigation)
                    .WithMany(p => p.TblContractPacking)
                    .HasForeignKey(x => x.FkContract)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractPacking_TblContract");

                entity.HasOne(d => d.FkPackingNavigation)
                    .WithMany(p => p.TblContractPacking)
                    .HasForeignKey(x => x.FkPacking)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractPacking_TblPacking");
            });

            modelBuilder.Entity<TblContractProduct>(entity =>
            {
                entity.HasKey(x => new { x.FkContract, x.FkProduct });

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.FkProduct).HasColumnName("fkProduct");

                entity.HasOne(d => d.FkContractNavigation)
                    .WithMany(p => p.TblContractProduct)
                    .HasForeignKey(x => x.FkContract)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractProduct_TblContract");

                entity.HasOne(d => d.FkProductNavigation)
                    .WithMany(p => p.TblContractProduct)
                    .HasForeignKey(x => x.FkProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractProduct_TblProduct");
            });

            modelBuilder.Entity<TblContractType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.IsEntry).HasColumnName("isEntry");

                entity.Property(e => e.IsExit).HasColumnName("isExit");

                entity.Property(e => e.IsProduct1Packing0).HasColumnName("isProduct1Packing0");

                entity.Property(e => e.OutControlByContract).HasColumnName("outControlByContract");

                entity.Property(e => e.OutControlByLocation).HasColumnName("outControlByLocation");

                entity.Property(e => e.OutControlByPercent).HasColumnName("outControlByPercent");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Addras)
                    .IsRequired()
                    .HasColumnName("addras")
                    .HasMaxLength(250);

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.FkSalmali).HasColumnName("fkSalmali");

                entity.Property(e => e.IdOtherSystem)
                    .HasColumnName("idOtherSystem")
                    .HasMaxLength(50);

                entity.Property(e => e.IsEnable).HasColumnName("isEnable");

                entity.Property(e => e.Mob)
                    .IsRequired()
                    .HasColumnName("mob")
                    .HasMaxLength(50);

                entity.Property(e => e.NationalCode).HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblInjury>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Ord)
                    .HasColumnName("ord")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblLocation>(entity =>
            {
                entity.HasIndex(x => new { x.Code, x.FkP })
                    .HasName("IX_TblLocation")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.CodeFull)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FkP).HasColumnName("fkP");

                entity.Property(e => e.ForProduct).HasColumnName("forProduct");

                entity.Property(e => e.Isdell).HasColumnName("isdell");

                entity.Property(e => e.Kind).HasColumnName("kind");

                entity.Property(e => e.Ord)
                    .HasColumnName("ord")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Wight)
                    .HasColumnName("wight")
                    .HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.FkPNavigation)
                    .WithMany(p => p.InverseFkPNavigation)
                    .HasForeignKey(x => x.FkP)
                    .HasConstraintName("FK_TblLocation_TblLocation");
            });

            modelBuilder.Entity<TblPacking>(entity =>
            {
                entity.HasIndex(x => x.Code)
                    .HasName("IX_TblPacking")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.WightEmpty).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.WightFull).HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<TblPortage>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CarMashin)
                    .HasColumnName("carMashin")
                    .HasMaxLength(50);

                entity.Property(e => e.CarRanande)
                    .HasColumnName("carRanande")
                    .HasMaxLength(50);

                entity.Property(e => e.CarShMashin)
                    .HasColumnName("carShMashin")
                    .HasMaxLength(50);

                entity.Property(e => e.CarTell)
                    .HasColumnName("carTell")
                    .HasMaxLength(50);

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Date1)
                    .HasColumnName("date1")
                    .HasColumnType("datetime");

                entity.Property(e => e.Date2)
                    .HasColumnName("date2")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dateadd1)
                    .HasColumnName("dateadd1")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dateadd2)
                    .HasColumnName("dateadd2")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dateedit1)
                    .HasColumnName("dateedit1")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dateedit2)
                    .HasColumnName("dateedit2")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkCar).HasColumnName("fkCar");

                entity.Property(e => e.FkContracttype).HasColumnName("fkContracttype");

                entity.Property(e => e.FkCustomer).HasColumnName("fkCustomer");

                entity.Property(e => e.FkSalmali).HasColumnName("fkSalmali");

                entity.Property(e => e.FkUsAdd1).HasColumnName("fkUsAdd1");

                entity.Property(e => e.FkUsAdd2).HasColumnName("fkUsAdd2");

                entity.Property(e => e.FkUsEdit1).HasColumnName("fkUsEdit1");

                entity.Property(e => e.FkUsEdit2).HasColumnName("fkUsEdit2");

                entity.Property(e => e.FkUsPermit).HasColumnName("fkUsPermit");

                entity.Property(e => e.IsPermitOk).HasColumnName("IsPermitOK");

                entity.Property(e => e.KindTitle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PackingCount).HasColumnName("packingCount");

                entity.Property(e => e.PackingOfWeight)
                    .HasColumnName("packingOfWeight")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Txt)
                    .HasColumnName("txt")
                    .HasMaxLength(500);

                entity.Property(e => e.TxtPermit)
                    .HasColumnName("txtPermit")
                    .HasMaxLength(250);

                entity.Property(e => e.Weight1)
                    .HasColumnName("weight1")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Weight1IsBascul).HasColumnName("weight1IsBascul");

                entity.Property(e => e.Weight2)
                    .HasColumnName("weight2")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Weight2IsBascul).HasColumnName("weight2IsBascul");

                entity.Property(e => e.WeightNet)
                    .HasColumnName("weightNet")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.FkCarNavigation)
                    .WithMany(p => p.TblPortage)
                    .HasForeignKey(x => x.FkCar)
                    .HasConstraintName("FK_TblPortage_TblCar");

                entity.HasOne(d => d.FkContracttypeNavigation)
                    .WithMany(p => p.TblPortage)
                    .HasForeignKey(x => x.FkContracttype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortage_TblContractType");

                entity.HasOne(d => d.FkCustomerNavigation)
                    .WithMany(p => p.TblPortage)
                    .HasForeignKey(x => x.FkCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortage_TblCustomer");
            });

            modelBuilder.Entity<TblPortageDocument>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkPortage).HasColumnName("fkPortage");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasColumnType("image");

                entity.Property(e => e.Kind)
                    .IsRequired()
                    .HasColumnName("kind")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkPortageNavigation)
                    .WithMany(p => p.TblPortageDocument)
                    .HasForeignKey(x => x.FkPortage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageDocument_TblPortage");
            });

            modelBuilder.Entity<TblPortageRow>(entity =>
            {
                entity.HasIndex(x => x.FkPacking)
                    .HasName("IX_TblPortageRow");

                entity.HasIndex(x => x.FkProduct)
                    .HasName("IX_TblPortageRow_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.CodeLocation)
                    .HasColumnName("codeLocation")
                    .HasMaxLength(50);

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.FkLocation1).HasColumnName("fkLocation1");

                entity.Property(e => e.FkLocation2).HasColumnName("fkLocation2");

                entity.Property(e => e.FkLocation3).HasColumnName("fkLocation3");

                entity.Property(e => e.FkPacking).HasColumnName("fkPacking");

                entity.Property(e => e.FkPortage).HasColumnName("fkPortage");

                entity.Property(e => e.FkProduct).HasColumnName("fkProduct");

                entity.Property(e => e.FkUser).HasColumnName("fkUser");

                entity.Property(e => e.Txt)
                    .HasColumnName("txt")
                    .HasMaxLength(200);

                entity.HasOne(d => d.FkContractNavigation)
                    .WithMany(p => p.TblPortageRow)
                    .HasForeignKey(x => x.FkContract)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageRow_TblContract");

                entity.HasOne(d => d.FkPortageNavigation)
                    .WithMany(p => p.TblPortageRow)
                    .HasForeignKey(x => x.FkPortage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageRow_TblPortage");
            });

            modelBuilder.Entity<TblPortageRowInjury>(entity =>
            {
                entity.HasKey(x => new { x.FkInjury, x.FkPortageRow });

                entity.Property(e => e.FkInjury).HasColumnName("fkInjury");

                entity.Property(e => e.FkPortageRow).HasColumnName("fkPortageRow");

                entity.HasOne(d => d.FkInjuryNavigation)
                    .WithMany(p => p.TblPortageRowInjury)
                    .HasForeignKey(x => x.FkInjury)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageRowInjury_TblInjury");

                entity.HasOne(d => d.FkPortageRowNavigation)
                    .WithMany(p => p.TblPortageRowInjury)
                    .HasForeignKey(x => x.FkPortageRow)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageRowInjury_TblPortageRow");
            });

            modelBuilder.Entity<TblPortageRowLocation>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkLocation1).HasColumnName("fkLocation1");

                entity.Property(e => e.FkLocation2).HasColumnName("fkLocation2");

                entity.Property(e => e.FkLocation3).HasColumnName("fkLocation3");

                entity.Property(e => e.FkPortageRow).HasColumnName("fkPortageRow");

                entity.Property(e => e.Fkuser).HasColumnName("fkuser");

                entity.HasOne(d => d.FkPortageRowNavigation)
                    .WithMany(p => p.TblPortageRowLocation)
                    .HasForeignKey(x => x.FkPortageRow)
                    .HasConstraintName("FK_TblPortageRowLocation_TblPortageRow");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Ord)
                    .HasColumnName("ord")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblSalMali>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsOpen).HasColumnName("isOpen");

                entity.Property(e => e.Sal)
                    .IsRequired()
                    .HasColumnName("sal")
                    .HasMaxLength(50);

                entity.Property(e => e.SalAz)
                    .HasColumnName("salAz")
                    .HasColumnType("date");

                entity.Property(e => e.SalTa)
                    .HasColumnName("salTa")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<TblStoreLog>(entity =>
            {
                entity.HasKey(x => new { x.FkContractType, x.FkLocation, x.FkSalmali, x.FkCustomer, x.FkPacking, x.FkProduct });

                entity.Property(e => e.FkContractType).HasColumnName("fkContractType");

                entity.Property(e => e.FkLocation).HasColumnName("fkLocation");

                entity.Property(e => e.FkSalmali).HasColumnName("fkSalmali");

                entity.Property(e => e.FkCustomer).HasColumnName("fkCustomer");

                entity.Property(e => e.FkPacking).HasColumnName("fkPacking");

                entity.Property(e => e.FkProduct).HasColumnName("fkProduct");

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.WeightIn).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.WeightOut).HasColumnType("decimal(22, 2)");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDel).HasColumnName("isDel");

                entity.Property(e => e.Mob)
                    .HasColumnName("mob")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("roles")
                    .HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblUserPermis>(entity =>
            {
                entity.HasKey(x => new { x.FkPortageType, x.FkUser });

                entity.Property(e => e.FkPortageType).HasColumnName("fkPortageType");

                entity.Property(e => e.FkUser).HasColumnName("fkUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
