using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WebsiteLibrary.Models.Entites;

public partial class LibraryDatabaseContext : DbContext
{
    public LibraryDatabaseContext()
    {
    }

    public LibraryDatabaseContext(DbContextOptions<LibraryDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookimport> Bookimports { get; set; }

    public virtual DbSet<Bookimportdetail> Bookimportdetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Fine> Fines { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Loandetail> Loandetails { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=LibraryDatabase;user=root;password=admin", ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("books");

            entity.HasIndex(e => e.Isbn, "ISBN").IsUnique();

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.PublishedYear).HasColumnType("year");
            entity.Property(e => e.Publisher).HasMaxLength(255);
            entity.Property(e => e.Quantity).HasDefaultValueSql("'0'");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasMany(d => d.Categories).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "Bookcategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("bookcategories_ibfk_2"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("bookcategories_ibfk_1"),
                    j =>
                    {
                        j.HasKey("BookId", "CategoryId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("bookcategories");
                        j.HasIndex(new[] { "CategoryId" }, "CategoryID");
                        j.IndexerProperty<int>("BookId").HasColumnName("BookID");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("CategoryID");
                    });
        });

        modelBuilder.Entity<Bookimport>(entity =>
        {
            entity.HasKey(e => e.ImportId).HasName("PRIMARY");

            entity.ToTable("bookimports");

            entity.HasIndex(e => e.StaffId, "StaffID");

            entity.HasIndex(e => e.SupplierId, "SupplierID");

            entity.Property(e => e.ImportId).HasColumnName("ImportID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TotalAmount).HasPrecision(10, 2);

            entity.HasOne(d => d.Staff).WithMany(p => p.Bookimports)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("bookimports_ibfk_2");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Bookimports)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("bookimports_ibfk_1");
        });

        modelBuilder.Entity<Bookimportdetail>(entity =>
        {
            entity.HasKey(e => e.ImportDetailId).HasName("PRIMARY");

            entity.ToTable("bookimportdetails");

            entity.HasIndex(e => e.BookId, "BookID");

            entity.HasIndex(e => e.ImportId, "ImportID");

            entity.Property(e => e.ImportDetailId).HasColumnName("ImportDetailID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.ImportId).HasColumnName("ImportID");
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.Book).WithMany(p => p.Bookimportdetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("bookimportdetails_ibfk_2");

            entity.HasOne(d => d.Import).WithMany(p => p.Bookimportdetails)
                .HasForeignKey(d => d.ImportId)
                .HasConstraintName("bookimportdetails_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Fine>(entity =>
        {
            entity.HasKey(e => e.FineId).HasName("PRIMARY");

            entity.ToTable("fines");

            entity.HasIndex(e => e.LoanId, "LoanID");

            entity.Property(e => e.FineId).HasColumnName("FineID");
            entity.Property(e => e.FineAmount).HasPrecision(10, 2);
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Unpaid'")
                .HasColumnType("enum('Unpaid','Paid')");

            entity.HasOne(d => d.Loan).WithMany(p => p.Fines)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("fines_ibfk_1");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PRIMARY");

            entity.ToTable("loans");

            entity.HasIndex(e => e.MemberId, "MemberID");

            entity.HasIndex(e => e.StaffId, "StaffID");

            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Remarks).HasColumnType("text");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Borrowed'")
                .HasColumnType("enum('Borrowed','Returned','Overdue')");

            entity.HasOne(d => d.Member).WithMany(p => p.Loans)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("loans_ibfk_1");

            entity.HasOne(d => d.Staff).WithMany(p => p.Loans)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("loans_ibfk_2");
        });

        modelBuilder.Entity<Loandetail>(entity =>
        {
            entity.HasKey(e => e.LoanDetailId).HasName("PRIMARY");

            entity.ToTable("loandetails");

            entity.HasIndex(e => e.BookId, "BookID");

            entity.HasIndex(e => e.LoanId, "LoanID");

            entity.Property(e => e.LoanDetailId).HasColumnName("LoanDetailID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");

            entity.HasOne(d => d.Book).WithMany(p => p.Loandetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("loandetails_ibfk_2");

            entity.HasOne(d => d.Loan).WithMany(p => p.Loandetails)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("loandetails_ibfk_1");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PRIMARY");

            entity.ToTable("members");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Active'")
                .HasColumnType("enum('Active','Inactive')");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PRIMARY");

            entity.ToTable("staff");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'Assistant'")
                .HasColumnType("enum('Librarian','Assistant')");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Active'")
                .HasColumnType("enum('Active','Inactive')");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("suppliers");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.ContactName).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.ReferenceId, "ReferenceID");

            entity.HasIndex(e => e.Username, "Username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");
            entity.Property(e => e.Role).HasColumnType("enum('Member','Staff')");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Reference).WithMany(p => p.Users)
                .HasForeignKey(d => d.ReferenceId)
                .HasConstraintName("users_ibfk_1");

            entity.HasOne(d => d.ReferenceNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.ReferenceId)
                .HasConstraintName("users_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
