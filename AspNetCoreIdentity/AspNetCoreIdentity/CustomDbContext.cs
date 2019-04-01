using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentity
{
    public class CustomDbContext : IdentityDbContext<CustomUser>
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CustomUser>(user => user.HasIndex(x => x.Locale).IsUnique(false));

            builder.Entity<Organization>(org =>
            {
                org.ToTable("Organizations");
                org.HasKey(x => x.Id);
                org.HasMany<CustomUser>().WithOne().HasForeignKey(x => x.OrgId).IsRequired(false);
            });
        }
    }
}
