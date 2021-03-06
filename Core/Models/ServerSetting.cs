using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
   public class ServerSetting
    {
        [Required]
        public string Host { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Port { get; set; }
        [EmailAddress]
        [Required]
        public string mailTo { get; set; }
        [EmailAddress]
        [Required]
        public string mailForm{ get; set; }


    }
}
