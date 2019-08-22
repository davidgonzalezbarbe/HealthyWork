using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthyWork.API.Contracts.Models
{
    public class HeadQuarters
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Room> Rooms { get; set; }

    }
}
