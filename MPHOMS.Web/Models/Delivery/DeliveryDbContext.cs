using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MPHOMS.Web.Models.Delivery
{
    public partial class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OrderNotificationStore1> OrderNotificationStore1 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "oleon134_mipeqhdev");

            modelBuilder.Entity<OrderNotificationStore1>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.ToTable("OrderNotificationStore1", "dbo");

                entity.HasIndex(e => e.OrderId)
                    .HasName("AK_OrderNotificationStore1_OrderID")
                    .IsUnique();

                entity.HasIndex(e => e.Status);

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderNumber).IsRequired();

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PaymentStatusId).HasColumnName("PaymentStatusID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Store)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
