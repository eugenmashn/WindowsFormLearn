using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace WindowsFormsApplication2
{
    public class WorkerContext : DbContext
    {
        public WorkerContext() : base("Model1")
        {
        }
        public DbSet<HolyDay> HolyDays { get; set; }
        public DbSet<People> Peoples { get; set; }

    }
}
