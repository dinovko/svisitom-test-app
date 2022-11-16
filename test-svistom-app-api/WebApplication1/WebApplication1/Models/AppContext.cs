using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> context) : base(context)
        {
        }
        public DbSet<TableData> TableDatas { get; set; }
    }
}
