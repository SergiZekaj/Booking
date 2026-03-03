using System;
using System.Collections.Generic;
using System.Text;
using Booking.Domain.Addresses;
using Booking.Domain.Bookings;
using Booking.Domain.Estate;
using Booking.Domain.OwnerProfiles;
using Booking.Domain.Reviews;
using Booking.Domain.Roles;
using Booking.Domain.UserRoles;
using Booking.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users {  get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<OwnerProfileEntity> OwnerProfiles { get; set; } 
        public DbSet<PropertyEntity> Properties { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }    
        public DbSet<ReviewEntity> Reviews { get; set; }  

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>()
                .HasOne(u => u.OwnerProfileEntity)
                .WithOne(op => op.User)
                .HasForeignKey<OwnerProfileEntity>(op => op.UserId);

            builder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<UserRoleEntity>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRolesEntity)
                .HasForeignKey(ur => ur.UserId);    

            builder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);  

            builder.Entity<PropertyEntity>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.OwnedProperties)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PropertyEntity>()
                .HasOne(p => p.Address)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AddressId);

            builder.Entity<BookingEntity>()
                .HasOne(b => b.Guest)
                .WithMany(u => u.GuestBookings)
                .HasForeignKey(b => b.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookingEntity>()
                .HasOne(b => b.Property)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookingEntity>()
                .HasOne(b => b.Review)
                .WithOne(r => r.Booking)
                .HasForeignKey<ReviewEntity>(r => r.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ReviewEntity>()
                .HasOne(r => r.Guest)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ReviewEntity>()
                .HasIndex(r => r.BookingId)
                .IsUnique();

            builder.Entity<RoleEntity>().HasData(
                new RoleEntity
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "User",
                    Description = "Default user role",
                    IsDefault = true
                });
        }
 
    }
}
