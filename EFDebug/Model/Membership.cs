using System;
using System.ComponentModel.DataAnnotations;

namespace EFDebug.Model
{
    public class Membership
    {
        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
