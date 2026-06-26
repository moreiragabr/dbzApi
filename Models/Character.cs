using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dbzApi.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required value.")]
        [MaxLength(50, ErrorMessage = "Too long, maximum of 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is a required value.")]
        [MaxLength(60, ErrorMessage = "Too long, maximum of 60 characters")]
        public string Type { get; set; } = string.Empty;
    }
}