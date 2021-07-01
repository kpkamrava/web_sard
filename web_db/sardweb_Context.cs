using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

#nullable disable

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
        public virtual DbSet<TblCar> TblCars { get; set; }
        //public virtual DbSet<TblConfig> TblConfigs { get; set; }
        public virtual DbSet<TblConf> TblConf { get; set; }
        public virtual DbSet<TblContract> TblContracts { get; set; }
        public virtual DbSet<TblContractPacking> TblContractPackings { get; set; }
        public virtual DbSet<TblContractProduct> TblContractProducts { get; set; }
        public virtual DbSet<TblContractType> TblContractTypes { get; set; }
        public virtual DbSet<TblCustomer> TblCustomers { get; set; }
        public virtual DbSet<TblCustomerGroup> TblCustomerGroups { get; set; }
        public virtual DbSet<TblGroup> TblGroups { get; set; }
        public virtual DbSet<TblInjury> TblInjuries { get; set; }
        public virtual DbSet<TblLocation> TblLocations { get; set; }
        public virtual DbSet<TblPacking> TblPackings { get; set; }
        public virtual DbSet<TblPortage> TblPortages { get; set; }
        public virtual DbSet<TblPortageDocument> TblPortageDocuments { get; set; }
        public virtual DbSet<TblPortageInjury> TblPortageInjuries { get; set; }
        public virtual DbSet<TblPortageMoney> TblPortageMoneys { get; set; }
        public virtual DbSet<TblPortageRow> TblPortageRows { get; set; }
        public virtual DbSet<TblPortageRowInjury> TblPortageRowInjuries { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblSalMali> TblSalMalis { get; set; }
        public virtual DbSet<TblStoreLog> TblStoreLogs { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblUserPermi> TblUserPermis { get; set; }
        public virtual DbSet<TblUserSal> TblUserSals { get; set; }
        public virtual DbSet<ViewListAshkhasPortageEroor> ViewListAshkhasPortageEroors { get; set; }



        public virtual DbSet<_temp.TblTemp> TblTemps { get; set; }
        public virtual DbSet<_temp.TblTempRow> TblTempRows { get; set; }

        public virtual DbSet<_note.TblNote> TblNotes { get; set; }
        public virtual DbSet<_note.TblNoteDate> TblNoteDates { get; set; }
        public virtual DbSet<_note.TblNoteRoll> TblNoteRolls { get; set; }
        public virtual DbSet<_note.TblNoteRows> TblNoteRows { get; set; }


        public virtual DbSet<_queu.TblQueu> TblQueus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=web_sard;MultipleActiveResultSets=true;");
            }
           
        }

        public bool backupDb(string path,  bool compress = true)
        {
            var conString = Database.GetDbConnection().ConnectionString;


            SqlCommand oCommand = null;
            SqlConnection oConnection =   new SqlConnection(conString);


            string dbName = oConnection.Database;

            string command = $@"BACKUP DATABASE [{dbName}] TO
DISK = N'{path}'
WITH NOFORMAT, INIT, 
NAME = N'{dbName}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, {(compress ? "COMPRESSION," : "")}   STATS = 10";

            if (oConnection.State != ConnectionState.Open)
                oConnection.Open();
            oCommand = new SqlCommand(command, oConnection);
            oCommand.ExecuteNonQuery();



            return true;
        }
         
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<_note.TblNoteDate>()
            .HasOne (s => s.TblNote)
            .WithMany(g => g.TblNoteDates)
            .HasForeignKey(s => s.FkTblNote);

            modelBuilder.Entity<_note.TblNoteRoll>()
         .HasOne(s => s.TblNote)
         .WithMany(g => g.TblNoteRolls)
         .HasForeignKey(s => s.FkTblNote);
         
            modelBuilder.Entity<_note.TblNoteRows>()
               .HasOne(s => s.TblNote)
                .WithMany(g => g.TblNoteRows)
                 .HasForeignKey(s => s.FkTblNote);

            //   modelBuilder.Entity<TblTemp>()
            //.HasOne<TblUser>(s => s.UserTaiid)
            //.WithMany(g => g.TblTempTaiids)
            //.HasForeignKey(s => s.FkuserTaiid);

            //        modelBuilder.Entity<TblTempRow>()
            //.HasOne(s => s.FktempNavigation)
            //.WithMany(g => g.TblTempRows)
            //.HasForeignKey(s => s.Fktemp).IsRequired();



            modelBuilder.Entity<TblCar>(entity =>
            {
                entity.ToTable("TblCar");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Img)
                    .HasColumnType("image")
                    .HasAnnotation("Relational:ColumnType", "image");

                entity.Property(e => e.IsDel).HasColumnName("isDel");

                entity.Property(e => e.PriceTowBascol)
                    .HasColumnType("decimal(21, 3)")
                    .HasColumnName("priceTowBascol")
                    .HasAnnotation("Relational:ColumnType", "decimal(21, 3)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            //modelBuilder.Entity<TblConfig>(entity =>
            //{
            //    entity.HasNoKey();

            //    entity.ToTable("TblConfig");

            //    entity.Property(e => e.ApiPass).HasMaxLength(200);

            //    entity.Property(e => e.ApiUrlPaivest).HasMaxLength(200);

            //    entity.Property(e => e.ApiUser).HasMaxLength(200);

            //    entity.Property(e => e.DegatBascul).HasColumnName("degatBascul");

            //    entity.Property(e => e.KindOtherSystem)
            //        .HasMaxLength(50)
            //        .HasColumnName("kindOtherSystem");

            //    entity.Property(e => e.Title)
            //        .HasMaxLength(200)
            //        .HasColumnName("title");
            //});

            modelBuilder.Entity<TblContract>(entity =>
            {
                entity.ToTable("TblContract");

                entity.HasIndex(e => new { e.FkCustomer, e.Code }, "IX_TblContract")
                    .IsUnique();

                entity.HasIndex(e => new { e.FkSalmali, e.FkContractType }, "IX_TblContract_1");

                entity.HasIndex(e => e.FkCustomer, "IX_TblContract_2");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Azdate)
                    .HasColumnType("date")
                    .HasColumnName("azdate")
                    .HasAnnotation("Relational:ColumnType", "date");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Dateadd)
                    .HasColumnType("datetime")
                    .HasColumnName("dateadd")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Dateedit)
                    .HasColumnType("datetime")
                    .HasColumnName("dateedit")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FkContractType).HasColumnName("fkContractType");

                entity.Property(e => e.FkCustomer).HasColumnName("fkCustomer");

                entity.Property(e => e.FkSalmali).HasColumnName("fkSalmali");

                entity.Property(e => e.FkUsAdd).HasColumnName("fkUsAdd");

                entity.Property(e => e.FkUsEdit).HasColumnName("fkUsEdit");

                entity.Property(e => e.IsEndVrud).HasColumnName("isEndVrud");

                entity.Property(e => e.IsEndXroj).HasColumnName("isEndXroj");

                entity.Property(e => e.PriceOfBoxIn)
                    .HasColumnType("decimal(22, 4)")
                    .HasColumnName("priceOfBoxIn")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 4)");

                entity.Property(e => e.PriceOfBoxOut)
                    .HasColumnType("decimal(22, 4)")
                    .HasColumnName("priceOfBoxOut")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 4)");

                entity.Property(e => e.PriceOfKiloIn)
                    .HasColumnType("decimal(22, 4)")
                    .HasColumnName("priceOfKiloIn")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 4)");

                entity.Property(e => e.PriceOfKiloOut)
                    .HasColumnType("decimal(22, 4)")
                    .HasColumnName("priceOfKiloOut")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 4)");

                entity.Property(e => e.SendSms).HasColumnName("sendSms");

                entity.Property(e => e.SumInCount).HasColumnName("_SumInCount");

                entity.Property(e => e.SumInWeight)
                    .HasColumnType("decimal(22, 4)")
                    .HasColumnName("_SumInWeight")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 4)");

                entity.Property(e => e.SumOutCount).HasColumnName("_SumOutCount");

                entity.Property(e => e.SumOutWeight)
                    .HasColumnType("decimal(22, 4)")
                    .HasColumnName("_SumOutWeight")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 4)");

                entity.Property(e => e.Tadate)
                    .HasColumnType("date")
                    .HasColumnName("tadate")
                    .HasAnnotation("Relational:ColumnType", "date");

                entity.Property(e => e.Txt)
                    .HasMaxLength(500)
                    .HasColumnName("txt");

                entity.Property(e => e.WeightMaxIn)
                    .HasColumnType("decimal(18, 2)")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 2)");

                entity.Property(e => e.WeightMaxOut)
                    .HasColumnType("decimal(18, 2)")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 2)");

                entity.HasOne(d => d.FkContractTypeNavigation)
                    .WithMany(p => p.TblContracts)
                    .HasForeignKey(d => d.FkContractType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContract_TblContractType");

                entity.HasOne(d => d.FkCustomerNavigation)
                    .WithMany(p => p.TblContracts)
                    .HasForeignKey(d => d.FkCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContract_TblCustomer");
            });

            modelBuilder.Entity<TblContractPacking>(entity =>
            {
                entity.HasKey(e => new { e.FkContract, e.FkPacking });

                entity.ToTable("TblContractPacking");

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.FkPacking).HasColumnName("fkPacking");

                entity.HasOne(d => d.FkContractNavigation)
                    .WithMany(p => p.TblContractPackings)
                    .HasForeignKey(d => d.FkContract)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractPacking_TblContract");

                entity.HasOne(d => d.FkPackingNavigation)
                    .WithMany(p => p.TblContractPackings)
                    .HasForeignKey(d => d.FkPacking)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractPacking_TblPacking");
            });

            modelBuilder.Entity<TblContractProduct>(entity =>
            {
                entity.HasKey(e => new { e.FkContract, e.FkProduct });

                entity.ToTable("TblContractProduct");

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.FkProduct).HasColumnName("fkProduct");

                entity.HasOne(d => d.FkContractNavigation)
                    .WithMany(p => p.TblContractProducts)
                    .HasForeignKey(d => d.FkContract)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractProduct_TblContract");

                entity.HasOne(d => d.FkProductNavigation)
                    .WithMany(p => p.TblContractProducts)
                    .HasForeignKey(d => d.FkProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractProduct_TblProduct");
            });

            modelBuilder.Entity<TblContractType>(entity =>
            {
                entity.ToTable("TblContractType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.FkSalmali).HasColumnName("fkSalmali");

           

           

            

              

             

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("title");

                entity.HasOne(d => d.FkSalmaliNavigation)
                    .WithMany(p => p.TblContractTypes)
                    .HasForeignKey(d => d.FkSalmali)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractType_TblSalMali");
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.ToTable("TblCustomer");

                entity.HasIndex(e => e.FkSalmali, "IX_TblCustomer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Addras)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("addras");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Dateloginlast)
                    .HasColumnType("datetime")
                    .HasColumnName("dateloginlast")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Datesendpassword)
                    .HasColumnType("datetime")
                    .HasColumnName("datesendpassword")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FkSalmali).HasColumnName("fkSalmali");

                entity.Property(e => e.IdOtherSystem)
                    .HasMaxLength(50)
                    .HasColumnName("idOtherSystem");

                entity.Property(e => e.IsEnable).HasColumnName("isEnable");

                entity.Property(e => e.Mob)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("mob");

                entity.Property(e => e.NationalCode).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TblCustomerGroup>(entity =>
            {
                entity.HasKey(e => new { e.FkCustumer, e.FkGroup });

                entity.ToTable("TblCustomerGroup");

                entity.Property(e => e.FkCustumer).HasColumnName("fkCustumer");

                entity.Property(e => e.FkGroup).HasColumnName("fkGroup");

                entity.HasOne(d => d.FkCustumerNavigation)
                    .WithMany(p => p.TblCustomerGroups)
                    .HasForeignKey(d => d.FkCustumer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblCustomerGroup_TblCustomer");

                entity.HasOne(d => d.FkGroupNavigation)
                    .WithMany(p => p.TblCustomerGroups)
                    .HasForeignKey(d => d.FkGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblContractGroup_TblGroup");
            });

            modelBuilder.Entity<TblGroup>(entity =>
            {
                entity.ToTable("TblGroup");

                entity.HasIndex(e => e.Fklocation, "IX_TblGroup");

                entity.HasIndex(e => e.IsActive, "IX_TblGroup_1");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Class)
                    .HasMaxLength(150)
                    .HasColumnName("class");

                entity.Property(e => e.Fklocation).HasColumnName("fklocation");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TblInjury>(entity =>
            {
                entity.ToTable("TblInjury");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Ord)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ord");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TblLocation>(entity =>
            {
                entity.ToTable("TblLocation");

                entity.HasIndex(e => new { e.Code, e.FkP }, "IX_TblLocation")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.CodeFull)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FkP).HasColumnName("fkP");

                entity.Property(e => e.ForProduct).HasColumnName("forProduct");

                entity.Property(e => e.Isdell).HasColumnName("isdell");

                entity.Property(e => e.Kind).HasColumnName("kind");

                entity.Property(e => e.Ord)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ord");

                entity.Property(e => e.OtcodeAnbar).HasColumnName("OTcodeAnbar");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.Wight)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("wight")
                    .HasAnnotation("Relational:ColumnType", "numeric(18, 2)");

                entity.HasOne(d => d.FkPNavigation)
                    .WithMany(p => p.InverseFkPNavigation)
                    .HasForeignKey(d => d.FkP)
                    .HasConstraintName("FK_TblLocation_TblLocation");
            });

            modelBuilder.Entity<TblPacking>(entity =>
            {
                entity.ToTable("TblPacking");

                entity.HasIndex(e => e.Code, "IX_TblPacking")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");
 
                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsNotAc).HasColumnName("IsNotAC");

                entity.Property(e => e.OtcodeKala)
                    .HasMaxLength(50)
                    .HasColumnName("OTcodeKala");

                entity.Property(e => e.OtcodeKalaAcc)
                    .HasMaxLength(50)
                    .HasColumnName("OTcodeKalaAcc");

                entity.Property(e => e.OtcodeVahedShomaresh).HasColumnName("OTcodeVahedShomaresh");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.WightEmpty)
                    .HasColumnType("decimal(18, 3)")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 3)");

                entity.Property(e => e.WightFull)
                    .HasColumnType("decimal(18, 3)")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 3)");
            });

            modelBuilder.Entity<TblPortage>(entity =>
            {
                entity.ToTable("TblPortage");

                entity.HasIndex(e => new { e.FkSalmali, e.FkContracttype, e.KindCode, e.IsEnd, e.IsDel, e.Code }, "IX_TblPortage");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CarMashin)
                    .HasMaxLength(50)
                    .HasColumnName("carMashin");

                entity.Property(e => e.CarRanande)
                    .HasMaxLength(50)
                    .HasColumnName("carRanande");

                entity.Property(e => e.CarShMashin)
                    .HasMaxLength(50)
                    .HasColumnName("carShMashin");

                entity.Property(e => e.CarTell)
                    .HasMaxLength(50)
                    .HasColumnName("carTell");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Date1)
                    .HasColumnType("datetime")
                    .HasColumnName("date1")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Date2)
                    .HasColumnType("datetime")
                    .HasColumnName("date2")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Dateadd1)
                    .HasColumnType("datetime")
                    .HasColumnName("dateadd1")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Dateadd2)
                    .HasColumnType("datetime")
                    .HasColumnName("dateadd2")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Dateedit1)
                    .HasColumnType("datetime")
                    .HasColumnName("dateedit1")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Dateedit2)
                    .HasColumnType("datetime")
                    .HasColumnName("dateedit2")
                    .HasAnnotation("Relational:ColumnType", "datetime");

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

                entity.Property(e => e.OtcodeResid)
                    .HasMaxLength(50)
                    .HasColumnName("OTcodeResid");

                entity.Property(e => e.PackingCount).HasColumnName("packingCount");

                 entity.Property(e => e.PackingOfWeight).HasColumnName("packingOfWeight");

                entity.Property(e => e.SaveFaktor).HasColumnName("saveFaktor");

                entity.Property(e => e.SmsVaziat).HasMaxLength(150);

                entity.Property(e => e.SumMoney)
                    .HasColumnType("decimal(22, 4)")
                    .HasColumnName("sumMoney")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 4)");

                entity.Property(e => e.Txt)
                    .HasMaxLength(500)
                    .HasColumnName("txt");

                entity.Property(e => e.TxtPermit)
                    .HasMaxLength(250)
                    .HasColumnName("txtPermit");

                entity.Property(e => e.Weight1)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("weight1")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 2)");

                entity.Property(e => e.Weight1IsBascul).HasColumnName("weight1IsBascul");

                entity.Property(e => e.Weight2)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("weight2")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 2)");

                entity.Property(e => e.Weight2IsBascul).HasColumnName("weight2IsBascul");

                entity.Property(e => e.WeightNet)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("weightNet")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 2)");

                entity.HasOne(d => d.FkCarNavigation)
                    .WithMany(p => p.TblPortages)
                    .HasForeignKey(d => d.FkCar)
                    .HasConstraintName("FK_TblPortage_TblCar");

                entity.HasOne(d => d.FkContracttypeNavigation)
                    .WithMany(p => p.TblPortages)
                    .HasForeignKey(d => d.FkContracttype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortage_TblContractType");

                entity.HasOne(d => d.FkCustomerNavigation)
                    .WithMany(p => p.TblPortages)
                    .HasForeignKey(d => d.FkCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortage_TblCustomer");
            });

            modelBuilder.Entity<TblPortageDocument>(entity =>
            {
                entity.ToTable("TblPortageDocument");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FkPortage).HasColumnName("fkPortage");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnType("image")
                    .HasColumnName("image")
                    .HasAnnotation("Relational:ColumnType", "image");

                entity.Property(e => e.Kind)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("kind");

                entity.HasOne(d => d.FkPortageNavigation)
                    .WithMany(p => p.TblPortageDocuments)
                    .HasForeignKey(d => d.FkPortage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageDocument_TblPortage");
            });

            modelBuilder.Entity<TblPortageInjury>(entity =>
            {
                entity.HasKey(e => new { e.FkPortage, e.FkInjury });

                entity.ToTable("TblPortageInjury");

                entity.Property(e => e.FkPortage).HasColumnName("fkPortage");

                entity.Property(e => e.FkInjury).HasColumnName("fkInjury");

                entity.HasOne(d => d.FkInjuryNavigation)
                    .WithMany(p => p.TblPortageInjuries)
                    .HasForeignKey(d => d.FkInjury)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageInjury_TblInjury");

                entity.HasOne(d => d.FkPortageNavigation)
                    .WithMany(p => p.TblPortageInjuries)
                    .HasForeignKey(d => d.FkPortage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageInjury_TblPortage");
            });

            modelBuilder.Entity<TblPortageMoney>(entity =>
            {
                entity.ToTable("TblPortageMoney");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FkCar).HasColumnName("fkCar");

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.FkContractType).HasColumnName("fkContractType");

                entity.Property(e => e.FkCustomer).HasColumnName("fkCustomer");

                entity.Property(e => e.FkPacking).HasColumnName("fkPacking");

                entity.Property(e => e.FkPortage).HasColumnName("fkPortage");

                entity.Property(e => e.FkProduct).HasColumnName("fkProduct");

                entity.Property(e => e.Fklocation).HasColumnName("fklocation");

                entity.Property(e => e.Kind)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("kind");

                entity.Property(e => e.PriceOneWeight)
                    .HasColumnType("decimal(22, 2)")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 2)");

                entity.Property(e => e.PriceSum)
                    .HasColumnType("decimal(22, 2)")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 2)");

                entity.Property(e => e.Txt)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("txt");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(22, 2)")
                    .HasAnnotation("Relational:ColumnType", "decimal(22, 2)");

                entity.HasOne(d => d.FkContractNavigation)
                    .WithMany(p => p.TblPortageMoneys)
                    .HasForeignKey(d => d.FkContract)
                    .HasConstraintName("FK_TblPortageMoney_TblContract");

                entity.HasOne(d => d.FkCustomerNavigation)
                    .WithMany(p => p.TblPortageMoneys)
                    .HasForeignKey(d => d.FkCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageMoney_TblCustomer");

                entity.HasOne(d => d.FkPortageNavigation)
                    .WithMany(p => p.TblPortageMoneys)
                    .HasForeignKey(d => d.FkPortage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageMoney_TblPortage");
            });

            modelBuilder.Entity<TblPortageRow>(entity =>
            {
                entity.ToTable("TblPortageRow");

                entity.HasIndex(e => e.FkPacking, "IX_TblPortageRow");

                entity.HasIndex(e => e.FkProduct, "IX_TblPortageRow_1");

                entity.HasIndex(e => e.FkPortage, "IX_TblPortageRow_2");

                entity.HasIndex(e => e.FkContract, "IX_TblPortageRow_3");

                entity.HasIndex(e => new { e.FkPortage, e.FkContract }, "IX_TblPortageRow_4");

                entity.HasIndex(e => new { e.FkContractType, e.FkPortage, e.FkContract }, "IX_TblPortageRow_5");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.CodeLocation)
                    .HasMaxLength(50)
                    .HasColumnName("codeLocation");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.FkContractType).HasColumnName("fkContractType");

                entity.Property(e => e.FkLocation1).HasColumnName("fkLocation1");

                entity.Property(e => e.FkLocation2).HasColumnName("fkLocation2");

                entity.Property(e => e.FkLocation3).HasColumnName("fkLocation3");

                entity.Property(e => e.FkPacking).HasColumnName("fkPacking");

                entity.Property(e => e.FkPortage).HasColumnName("fkPortage");

                entity.Property(e => e.FkProduct).HasColumnName("fkProduct");

                entity.Property(e => e.FkUser).HasColumnName("fkUser");

                entity.Property(e => e.IsNimPalet).HasColumnName("isNimPalet");

                entity.Property(e => e.Txt)
                    .HasMaxLength(200)
                    .HasColumnName("txt");

                entity.Property(e => e.WeightOne).HasColumnName("weightOne");

                entity.HasOne(d => d.FkContractNavigation)
                    .WithMany(p => p.TblPortageRows)
                    .HasForeignKey(d => d.FkContract)
                    .HasConstraintName("FK_TblPortageRow_TblContract");

                entity.HasOne(d => d.FkPortageNavigation)
                    .WithMany(p => p.TblPortageRows)
                    .HasForeignKey(d => d.FkPortage)
                    .HasConstraintName("FK_TblPortageRow_TblPortage");
            });

            modelBuilder.Entity<TblPortageRowInjury>(entity =>
            {
                entity.HasKey(e => new { e.FkInjury, e.FkPortageRow });

                entity.ToTable("TblPortageRowInjury");

                entity.Property(e => e.FkInjury).HasColumnName("fkInjury");

                entity.Property(e => e.FkPortageRow).HasColumnName("fkPortageRow");

                entity.HasOne(d => d.FkInjuryNavigation)
                    .WithMany(p => p.TblPortageRowInjuries)
                    .HasForeignKey(d => d.FkInjury)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageRowInjury_TblInjury");

                entity.HasOne(d => d.FkPortageRowNavigation)
                    .WithMany(p => p.TblPortageRowInjuries)
                    .HasForeignKey(d => d.FkPortageRow)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPortageRowInjury_TblPortageRow");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.ToTable("TblProduct");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsNotAc).HasColumnName("IsNotAC");

                entity.Property(e => e.Ord)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ord");

                entity.Property(e => e.OtcodeKala)
                    .HasMaxLength(50)
                    .HasColumnName("OTcodeKala");

                entity.Property(e => e.OtcodeKalaAcc)
                    .HasMaxLength(50)
                    .HasColumnName("OTcodeKalaAcc");

                entity.Property(e => e.OtcodeVahedShomaresh).HasColumnName("OTcodeVahedShomaresh");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TblSalMali>(entity =>
            {
                entity.ToTable("TblSalMali");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IsOpen).HasColumnName("isOpen");

                entity.Property(e => e.Sal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sal");

                entity.Property(e => e.SalAz)
                    .HasColumnType("date")
                    .HasColumnName("salAz")
                    .HasAnnotation("Relational:ColumnType", "date");

                entity.Property(e => e.SalTa)
                    .HasColumnType("date")
                    .HasColumnName("salTa")
                    .HasAnnotation("Relational:ColumnType", "date");
            });

            modelBuilder.Entity<TblStoreLog>(entity =>
            {
                entity.ToTable("TblStoreLog");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FkContract).HasColumnName("fkContract");

                entity.Property(e => e.FkContractType).HasColumnName("fkContractType");

                entity.Property(e => e.FkCustomer).HasColumnName("fkCustomer");

                entity.Property(e => e.FkLocation1).HasColumnName("fkLocation1");

                entity.Property(e => e.FkLocation2).HasColumnName("fkLocation2");

                entity.Property(e => e.FkLocation3).HasColumnName("fkLocation3");

                entity.Property(e => e.FkPacking).HasColumnName("fkPacking");

                entity.Property(e => e.FkProduct).HasColumnName("fkProduct");

                entity.Property(e => e.FkSalmali).HasColumnName("fkSalmali");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(25, 5)")
                    .HasAnnotation("Relational:ColumnType", "decimal(25, 5)");

                entity.Property(e => e.WeightIn)
                    .HasColumnType("decimal(25, 5)")
                    .HasAnnotation("Relational:ColumnType", "decimal(25, 5)");

                entity.Property(e => e.WeightMovement)
                    .HasColumnType("decimal(25, 5)")
                    .HasAnnotation("Relational:ColumnType", "decimal(25, 5)");

                entity.Property(e => e.WeightOut)
                    .HasColumnType("decimal(25, 5)")
                    .HasAnnotation("Relational:ColumnType", "decimal(25, 5)");
            });



            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("TblUser");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDel).HasColumnName("isDel");

                entity.Property(e => e.Mob)
                    .HasMaxLength(50)
                    .HasColumnName("mob");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("roles");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<TblUserPermi>(entity =>
            {
                entity.HasKey(e => new { e.FkPortageType, e.FkUser });

                entity.Property(e => e.FkPortageType).HasColumnName("fkPortageType");

                entity.Property(e => e.FkUser).HasColumnName("fkUser");
            });

            modelBuilder.Entity<TblUserSal>(entity =>
            {
                entity.HasKey(e => new { e.FkUser, e.FkSal });

                entity.ToTable("TblUserSal");

                entity.Property(e => e.FkUser).HasColumnName("fkUser");

                entity.Property(e => e.FkSal).HasColumnName("fkSal");

                entity.HasOne(d => d.FkSalNavigation)
                    .WithMany(p => p.TblUserSals)
                    .HasForeignKey(d => d.FkSal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblUserSal_TblSalMali");

                entity.HasOne(d => d.FkUserNavigation)
                    .WithMany(p => p.TblUserSals)
                    .HasForeignKey(d => d.FkUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblUserSal_TblUser");
            });

            modelBuilder.Entity<ViewListAshkhasPortageEroor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_ListAshkhasPortageEroor");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.FkCustomer).HasColumnName("fkCustomer");

                entity.Property(e => e.Garardad).HasColumnName("garardad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
