using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Fans.Entities;

namespace Fans.Context
{
    public class FansDbContext : DbContext
    {
        public virtual DbSet<FansUser> FansUsers { get; set; }
    }
}
