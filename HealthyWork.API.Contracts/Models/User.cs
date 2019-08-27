using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthyWork.API.Contracts.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [Column("Room")]
        public Guid RoomId { get; set; }
        public Room Room { get; set; }

    }
}
