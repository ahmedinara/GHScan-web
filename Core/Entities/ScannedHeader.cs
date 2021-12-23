using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class ScannedHeader
    {
        public ScannedHeader()
        {
            ScannedDetials = new HashSet<ScannedDetial>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CountOfItems { get; set; }
        public string MailSent { get; set; }
        public string MailRecive { get; set; }
        public Guid ScannedGuid { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<ScannedDetial> ScannedDetials { get; set; }
    }
}
