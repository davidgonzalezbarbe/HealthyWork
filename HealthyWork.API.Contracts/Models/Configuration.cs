using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthyWork.API.Contracts.Models
{
    public class Configuration
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Min { get; set; }
        [Required]
        public int Max { get; set; }
        [Required]
        public bool PushEnabled { get; set; }
        [Required]
        public bool EmailEnabled { get; set; }
        [Required]
        public bool TelegramEnabled { get; set; }

    }
}
