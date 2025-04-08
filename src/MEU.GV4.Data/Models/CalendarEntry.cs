using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models
{
    public class CalendarEntry
    {
        public DateOnly? GameDate { get; set; }
        public TimeOnly? GameTime { get; set; }
        public string? Place { get; set; }
        public string? Notes { get; set; }
    }
}
