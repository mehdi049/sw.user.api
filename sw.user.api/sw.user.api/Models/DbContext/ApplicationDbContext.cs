using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sw.user.api.Models.DbContext.Tables;

namespace sw.user.api.Models.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Preference> Preference { get; set; }
        public DbSet<User> User { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
