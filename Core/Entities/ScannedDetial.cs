using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class ScannedDetial
    {
        public int Id { get; set; }
        public int ScannedMasterId { get; set; }
        public string QrCode { get; set; }
        public string QrFormat { get; set; }

        public virtual ScannedHeader ScannedMaster { get; set; }
    }
}
