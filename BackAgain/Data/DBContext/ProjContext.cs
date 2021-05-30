using BackAgain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public class ProjContext : IdentityDbContext<CustomIdentityUser>
    {
        public ProjContext(DbContextOptions<ProjContext> opt) : base(opt)
        {

        }

        public DbSet<Category> _Category { get; set; }
        public DbSet<ItemExtra> _ItemExtra { get; set; }
        public DbSet<ItemOption> _ItemOption { get; set; }
        public DbSet<Menu> _Menu { get; set; }
        public DbSet<MenuItem> _MenuItem { get; set; }
        public DbSet<Order> _Order { get; set; }
        public DbSet<OrderComment> _OrderComments { get; set; }
        public DbSet<OrderItem> _OrderItem { get; set; }
        public DbSet<OrderItemExtra> _OrderItemExtras { get; set; }
        public DbSet<OrderStateEnum> _OrderStateEnums { get; set; }
        public DbSet<OrderStatus> _Orderstatus { get; set; }
        public DbSet<OrderTransaction> _OrderTransactions { get; set; }
        public DbSet<POS> _POSs { get; set; }
        public DbSet<PosState> _POSState { get; set; }
        //public DbSet<PosTransaction> _posTransactions { get; set; }
        public DbSet<SocketConnection> _SocketConnection { get; set; }
        public DbSet<Subscription> _subscriptions { get; set; }
        public DbSet<Terminal> _Terminals { get; set; }
        public DbSet<TerminalMode> _TerminalModes { get; set; }
        public DbSet<TerminalState> _TerminalState { get; set; }
        //public DbSet<TerminalTransaction> _TerminalTransaction { get; set; }
        public DbSet<TransactionAffiliates> _TransactionAffiliate { get; set; }
        public DbSet<Theme> _Theme { get; set; }
        public DbSet<Transaction> _Transaction { get; set; }
        public DbSet<TransactionState> _TransactionState { get; set; }
        public DbSet<TransactionType> _TransactionType { get; set; }
        public DbSet<UpdateType> _UpdateType { get; set; }
        public DbSet<UserSettings> _UserSettings { get; set; }
        public DbSet<UserSubscription> _UserSubscriptions { get; set; }
        public DbSet<VersionUpdateLog> _VersionUpdateLog { get; set; }
        public DbSet<OrderComment> _orderComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserSettings>()
                   .HasIndex(e => e.UserId)
                   .IsUnique();

            builder.Entity<POS>()
                   .HasIndex(p => p.Serial).IsUnique();

            builder.Entity<Terminal>()
                   .HasIndex(p => p.Serial).IsUnique();

            builder.Entity<TransactionAffiliates>()
                   .HasOne(t => t.Pos)
                   .WithMany(P => P.transaction)
                   .HasForeignKey(t => t.PosSerial)
                   .HasPrincipalKey(p => p.Serial);

            builder.Entity<TransactionAffiliates>()
                   .HasOne(TA => TA.Terminal)
                   .WithMany(Ter => Ter.Transaction)
                   .HasForeignKey(TA => TA.TerminalSerial)
                   .HasPrincipalKey(Ter => Ter.Serial);

            builder.Entity<Order>()
                   .HasOne(O => O.pos)
                   .WithMany(P => P.Orders)
                   .HasForeignKey(O => O.POSSerial)
                   .HasPrincipalKey(P => P.Serial);

            builder.Entity<Terminal>()
                   .HasOne(Ter => Ter.pos)
                   .WithMany(P => P.Terminal)
                   .HasForeignKey(Ter => Ter.PosSerial)
                   .HasPrincipalKey(P => P.Serial);
        }
    }
}
