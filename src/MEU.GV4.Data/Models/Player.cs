using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models
{
    public class Player
    {
        public string? Name { get; set; }
        public string? ID { get; set; }
        public string? EMail { get; set; }
        public string? Phone { get; set; }
        public string? Position { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public Experience? PlayerExperience { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? ModifyDate { get; set; }
    }
}
