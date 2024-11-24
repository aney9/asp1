using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P50_4_22.Models;

public partial class PetStoreRpmContext : DbContext
{
    public PetStoreRpmContext()
    {
    }

    public PetStoreRpmContext(DbContextOptions<PetStoreRpmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CatalogProduct> CatalogProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PosOrder> PosOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-EJI2V8K\\SQLEXPRESS;Initial Catalog=PetStoreRPM;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.IdBrands).HasName("PK__Brands__147C88F55E83CB3D");

            entity.Property(e => e.IdBrands).HasColumnName("ID_brands");
            entity.Property(e => e.Brand1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Brand");
        });

        modelBuilder.Entity<CatalogProduct>(entity =>
        {
            entity.HasKey(e => e.IdCatalogproducts).HasName("PK__CatalogP__7D82B6FAA03B83B9");

            entity.Property(e => e.IdCatalogproducts).HasColumnName("ID_catalogproducts");
            entity.Property(e => e.BrandsId).HasColumnName("brands_ID");
            entity.Property(e => e.CategoriesId).HasColumnName("categories_ID");
            entity.Property(e => e.DescriptionProduct)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Img)
                .HasMaxLength(260)
                .IsUnicode(false);
            entity.Property(e => e.PriceOfProduct).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.Brands).WithMany(p => p.CatalogProducts)
                .HasForeignKey(d => d.BrandsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatalogPr__brand__3F466844");

            entity.HasOne(d => d.Categories).WithMany(p => p.CatalogProducts)
                .HasForeignKey(d => d.CategoriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatalogPr__categ__403A8C7D");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategories).HasName("PK__Categori__487EC275F5182293");

            entity.Property(e => e.IdCategories).HasColumnName("ID_categories");
            entity.Property(e => e.Categories)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrders).HasName("PK__Orders__D16B9390D610ED13");

            entity.HasIndex(e => e.OrderNumber, "UQ__Orders__CAC5E74353BF97FC").IsUnique();

            entity.Property(e => e.IdOrders).HasColumnName("ID_orders");
            entity.Property(e => e.DateOrder).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ordersum).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsersId).HasColumnName("users_ID");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__users_ID__44FF419A");
        });

        modelBuilder.Entity<PosOrder>(entity =>
        {
            entity.HasKey(e => e.IdPosorder).HasName("PK__PosOrder__06462F312C03B2AE");

            entity.ToTable("PosOrder");

            entity.Property(e => e.IdPosorder).HasColumnName("ID_posorder");
            entity.Property(e => e.OrdersId).HasColumnName("orders_ID");
            entity.Property(e => e.ProductsId).HasColumnName("products_ID");

            entity.HasOne(d => d.Orders).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.OrdersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PosOrder__orders__47DBAE45");

            entity.HasOne(d => d.Products).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.ProductsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PosOrder__produc__48CFD27E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PK__Users__18069104A4EF0F5A");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E386FB0E822").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053430BB8262").IsUnique();

            entity.Property(e => e.IdUsers).HasColumnName("ID_users");
            entity.Property(e => e.ClientName)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Client_Name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Loginpassword)
                .HasMaxLength(350)
                .IsUnicode(false);
            entity.Property(e => e.Loginvhod)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
