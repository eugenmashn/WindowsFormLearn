using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class HolyDay
    {
        public int Id { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime SecontDate { get; set; }
        public People People { get; set; }
        public bool IndexDate { get;set; }
        public int Peopleid { get; set; }

    }
}
