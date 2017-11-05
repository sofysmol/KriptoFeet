using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Categories.Models
{
    using System.ComponentModel.DataAnnotations;
    public class CategoryDB
    {
        [Key]
        public long Id {get; set;}
        public string Name {get; set;}
    }
}