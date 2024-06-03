using EntitiesLayer;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer {
    public partial class DatabaseModel : DbContext {
        public DatabaseModel()
            : base("name=DatabaseModelConfig") {

        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Archive> Archives { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Administrator>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithMany(e => e.Authors)
                .Map(m => m.ToTable("Book_Author"));

            modelBuilder.Entity<Book>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.url_photo)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.url_digital)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.barcode_id)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Archives)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.Book_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.Book_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.Book_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Borrows)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.Book_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.OIB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Archives)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.Employee_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Borrows)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.Employee_borrow_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Borrows1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.Employee_return_id);

            modelBuilder.Entity<Genre>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Genre)
                .HasForeignKey(e => e.Genre_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.OIB)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.price_day_late)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Library>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Library)
                .HasForeignKey(e => e.Library_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Library)
                .HasForeignKey(e => e.Library_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Library)
                .HasForeignKey(e => e.Library_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.Library)
                .HasForeignKey(e => e.Library_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.OIB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.barcode_id)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Borrows)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.Member_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.Member_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.Member_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Notifications)
                .WithMany(e => e.Members)
                .Map(m => m.ToTable("NotificationRead"));

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Books)
                .WithMany(e => e.Members)
                .Map(m => m.ToTable("WishList"));

            modelBuilder.Entity<Notification>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.comment)
                .IsUnicode(false);
        }
    }
}
