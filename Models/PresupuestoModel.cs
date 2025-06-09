using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOTEL_MEDITERRANEO.Models
{
    public class PresupuestoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El monto del presupuesto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto del presupuesto debe ser positivo.")]
        [Display(Name = "Monto Total del Presupuesto")]
        public decimal MontoTotal { get; set; }

        [Display(Name = "Fecha de Última Actualización")]
        public DateTime? FechaActualizacion { get; set; }
    }
}