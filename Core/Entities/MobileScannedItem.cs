using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace Core.Entities
{
    public partial class MobileScannedItem
    {
        public int Id { get; set; }
        [DisplayName("QrCode")]
        public string QrCode { get; set; }
        [DisplayName("QrCode Format (GTIN;SN;BN;XD)")]
        public string FormatedQrCode { get; set; }
        public bool IsFinshed { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ScannedGuid { get; set; }

        public virtual User CreatedByNavigation { get; set; }
    }
}
