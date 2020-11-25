namespace GestionVentas.Services.Services
{
    public interface IReporteService
    {
        byte[] GenerarReporteCantidadVentasDeArticulosVendidos(string fechaDesde, string fechaHasta);
        byte[] GenerarReporteRecaudacionDeVentaArticuloVendidos(string fechaDesde, string fechaHasta);
    }
}