using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class People
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Day { get; set; }
        public ICollection<HolyDay> HolyDays { get; set; } = new List<HolyDay>();

    }
}
