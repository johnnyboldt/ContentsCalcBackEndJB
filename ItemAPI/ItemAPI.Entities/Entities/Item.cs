using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ItemAPI.Entities
{
    [Table("Item")]
    public class Item
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string value { get; set; }
        [Required]
        public string category { get; set; }
    }
}