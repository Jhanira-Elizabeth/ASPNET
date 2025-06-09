using HOTEL_MEDITERRANEO.Models; // ¡Añade esta línea!
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOTEL_MEDITERRANEO.Models
{
    [Table("Gasto")]
    public class GastoModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Descripción del Gasto")]
        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Display(Name = "Monto del Gasto")]
        [Required(ErrorMessage = "El monto del gasto es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0")]
        public decimal Monto { get; set; }

        [Display(Name = "Fecha del Gasto")]
        [Required(ErrorMessage = "La fecha del gasto es requerida")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Tipo de Gasto")]
        [Required(ErrorMessage = "El tipo de gasto es requerido")]
        public string Tipo { get; set; } = "Compras"; // Forzado a "Compras" por lógica del negocio
        //relacion con ventas
        public string NumeroRecibo { get; set; } // Clave foránea

        [ForeignKey("NumeroRecibo")]
        public VentasModel Venta { get; set; }
    }
}