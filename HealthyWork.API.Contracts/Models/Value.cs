using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthyWork.API.Contracts.Models
{
    public class Value
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public double SensorValue { get; set; }
        [Required]
        public SensorType Type { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public PushLevel Level { get; set; }
        [Required]
        [Column("Room")]
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }

    public enum SensorType
    {
        None,
        Sonido,
        Luz,
        Temperatura,
        Humedad
    }

    public enum PushLevel
    {
        None,
        Adecuado,
        Sobrepasado,
        Infrapasado
    }
}
