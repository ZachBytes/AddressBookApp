using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using AddressBookApp.Models;
using Microsoft.Extensions.Configuration;


namespace AddressBookApp.Contexts
{
    public class AddressDbContext : DbContext
    {
        public AddressDbContext()
        {
        }
        public AddressDbContext(DbContextOptions<AddressDbContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed the database with at least 1 record

            modelBuilder.Entity<Contact>()
                    .HasData(
                    new Contact()
                    {
                        ContactID = 1,
                        FirstName = "TestFirst",
                        LastName = "TestLast"
                    });
            modelBuilder.Entity<Contact>()
                    .HasData(
                    new Contact()
                    {
                        ContactID = 2,
                        FirstName = "2ndTestFirst",
                        LastName = "2ndTestLast"
                    });
            modelBuilder.Entity<Address>()
                    .HasData(
                    new Address()
                    {
                        AddressID = 1,
                        ContactID = 1,
                        StreetAddress = "1 Any Street",
                        City = "Springfield",
                        State = "New Jersey",
                        PostalCode = "11111"
                    });
            modelBuilder.Entity<Address>()
                    .HasData(
                    new Address()
                    {
                        AddressID = 2,
                        ContactID = 1,
                        StreetAddress = "2 Some Street",
                        City = "Paramus",
                        State = "New Jersey",
                        PostalCode = "99999"
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
