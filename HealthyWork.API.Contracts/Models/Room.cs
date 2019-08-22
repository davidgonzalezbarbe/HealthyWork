using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthyWork.API.Contracts.Models
{
    public class Room
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Column("HeadQuarters")]
        public Guid HeadQuartersId { get; set; }
        public HeadQuarters HeadQuarters { get; set; }

        public List<Value> Values { get; set; }
        public List<User> Users { get; set; }
        public List<TelegramPush> TelegramPushes { get; set; }
    }
}
