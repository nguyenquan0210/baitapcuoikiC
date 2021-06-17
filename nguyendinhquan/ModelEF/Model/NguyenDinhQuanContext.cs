using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ModelEF.Model
{
    public partial class NguyenDinhQuanContext : DbContext
    {
        public NguyenDinhQuanContext()
            : base("name=NguyenDinhQuanContext")
        {
        }

        public virtual DbSet<loai_sp> loai_sp { get; set; }
        public virtual DbSet<san_pham> san_pham { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<loai_sp>()
                .Property(e => e.ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<loai_sp>()
                .HasMany(e => e.san_pham)
                .WithRequired(e => e.loai_sp)
                .HasForeignKey(e => e.ID_l)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<san_pham>()
                .Property(e => e.ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<san_pham>()
                .Property(e => e.ID_l)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.UserName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.Password)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
