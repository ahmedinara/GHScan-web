using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class User
    {
        public User()
        {
            MobileScannedItems = new HashSet<MobileScannedItem>();
            ScannedHeaders = new HashSet<ScannedHeader>();
        }

        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<MobileScannedItem> MobileScannedItems { get; set; }
        public virtual ICollection<ScannedHeader> ScannedHeaders { get; set; }
    }
}
