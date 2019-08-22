using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthyWork.API.Contracts.Models
{
    public class TelegramPush
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public long ChatId { get; set; }
        [Required]
        [Column("Room")]
        public Guid RoomId { get; set; }
        public Room Room { get; set; }

    }
}
