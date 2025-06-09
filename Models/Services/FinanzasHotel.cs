using System.Collections.Generic;
using System.Linq;

using HOTEL_MEDITERRANEO.Models; // Asegúrate de incluir el namespace correcto

namespace HOTEL_MEDITERRANEO.Services
{
    public class FinanzasHotel
    {
        public static decimal CalcularEfectivoTotal(List<VentasModel> ventas, List<GastoModel> gastos)
        {
            decimal totalVentas = ventas.Sum(v => v.Monto);
            decimal totalGastos = gastos
                .Where(g => g.Tipo == "Compras")
                .Sum(g => g.Monto);
            return totalVentas - totalGastos;
        }
    }
}
