using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HOTEL_MEDITERRANEO.Models
{
    [Table("Venta")]
    public class VentasModel
    {
        [Key]
        [Display(Name = "Número de Recibo")]
        [Required(ErrorMessage = "El número de recibo es requerido")]
        public string NumeroRecibo { get; set; }

        [Display(Name = "Nombre del Cliente")]
        [Required(ErrorMessage = "El nombre del cliente es requerido")]
        [MinLength(3, ErrorMessage = "El nombre del cliente debe tener al menos 3 caracteres")]
        public string NombreCliente { get; set; }

        [Display(Name = "Monto de la Venta")]
        [Required(ErrorMessage = "El monto es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0")]
        public decimal Monto { get; set; }

        [Display(Name = "Método de Pago")]
        [Required(ErrorMessage = "El método de pago es requerido")]
        public string MetodoPago { get; set; }

        [Display(Name = "Fecha de la Venta")]
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Número de Habitación")]
        [Required(ErrorMessage = "El número de habitación es requerido")]
        [Range(1, 1000, ErrorMessage = "El número de habitación debe ser un valor entre 1 y 1000")]
        public int NumeroHabitacion { get; set; }

        [Display(Name = "Número de Personas")]
        [Required(ErrorMessage = "El número de personas es requerido")]
        [Range(1, 10, ErrorMessage = "El número de personas debe ser entre 1 y 10")]
        public int NumeroPersonas { get; set; }

        // ----------------------------
        // Propiedades auxiliares para reportes
        // ----------------------------

        [NotMapped]
        public int AñoVenta => Fecha.Year;

        [NotMapped]
        public int MesVenta => Fecha.Month;

        [NotMapped]
        public int DiaVenta => Fecha.Day;

        [NotMapped]
        public string FechaFormateada => Fecha.ToString("dd/MM/yyyy");

        [NotMapped]
        public string MesNombre => Fecha.ToString("MMMM"); // Ej: junio, julio
    }
}