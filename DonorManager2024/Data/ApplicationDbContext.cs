using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorManager.Models;
using DonorManager2024.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DonorManager2024.Models.DonorRelated;
using DonorManager2024.ViewModels;
using DonorManager2024.Models.MailGroup;

namespace DonorManager2024.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Donor> Donor { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Campaign> Campaign { get; set; } = default!;
        public DbSet<DonorFlags> DonorFlags { get; set; }
        //public DbSet<DonorFlagCheckBoxes> DonorFlagCheckBoxes { get; set; } = default!;
        //public DbSet<DFCBTransaction> DFCBTransaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasMany(c => c.Donor).WithOne(d => d.Client).HasForeignKey(d => d.ClientId);

            modelBuilder.Entity<Campaign>().Property(c => c.ClientId).HasColumnName("ClientId");

            modelBuilder.Entity<Donor>().Property(d => d.ClientId).HasColumnName("ClientId");

            //modelBuilder.Entity<Donor>().HasMany(d => d.DonorFlags).WithOne(d => d.Donor).HasForeignKey(d => d.DonorId).IsRequired();

            modelBuilder.Entity<Campaign>().HasMany(t => t.Transactions).WithOne(t => t.Campaign).HasForeignKey(d => d.CampaignId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Donor>().HasMany(t => t.Transactions).WithOne(t => t.Donor).HasForeignKey(d => d.DonorId).IsRequired().OnDelete(DeleteBehavior.Restrict);            
                        
            //modelBuilder.Entity<DonorFlagsTransaction>().HasKey(dt => new { dt.FlagId, dt.TransId });

            //modelBuilder.Entity<DFCBTransaction>().HasOne(dt => dt.DonorFlagCheckBoxes).WithMany(d => d.DFCBTransaction).HasForeignKey(dt => dt.DonorFlagCheckId);

            //modelBuilder.Entity<DFCBTransaction>().HasOne(dt => dt.Transactions).WithMany(t => t.DFCBTransaction).HasForeignKey(dt => dt.TransId);

            //modelBuilder.Entity<DFCBTransaction>().ToTable("DFCBTransaction");
            
            //modelBuilder.Entity<DonorDonorFlags>().HasMany(d => d.Transactions).WithMany(t => t.DonorFlags).UsingEntity<DonorDonorFlags>(); //("DFCBTransaction"); 
            

            //modelBuilder.Entity<Transactions>().Property(t => t.TransId).HasColumnName("Id").HasColumnType("int").IsRequired();
            //modelBuilder.Entity<Transactions>().HasOne(t => t.Donor).WithOne().HasForeignKey<Donor>(t => t.DonorId).OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Transactions>().HasOne(t => t.Campaign).WithOne().HasForeignKey<Campaign>(t => t.CampaignId).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            
        }
               
        
        public DbSet<Promotions> Promotions { get; set; } = default!;
        public DbSet<Channels> Channels { get; set; } = default!;
        public DbSet<Transactions> Transactions { get; set; } = default!;
        public DbSet<AllUsersViewModel> AllUsers { get; set; } = default!;
        public DbSet<DonorManager2024.Models.Batches> Batches { get; set; } = default!;
        public DbSet<DonorManager2024.Models.MailGroup.CCRoster> CCRoster { get; set; } = default!;
        public DbSet<DonorManager2024.Models.MailGroup.NoMail> NoMail { get; set; } = default!;
        public DbSet<DonorManager2024.Models.MailGroup.Nixies> Nixies { get; set; } = default!;

    }
}