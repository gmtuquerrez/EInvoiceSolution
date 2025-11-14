using EInvoice.Infrastructure.Sri.Invoice.Generated;
using EInvoiceSolution.Core.Invoices.Models;
using System.Collections.ObjectModel;

namespace EInvoiceSolution.Core.Invoices.Mappers
{
    public static class InvoiceMapper
    {
        public static Factura MapToSriXsd(InvoiceModel invoiceModel)
        {

            var taxesDetails = new Collection<FacturaInfoFacturaTotalConImpuestosTotalImpuesto>(
                        (invoiceModel.TotalWithTaxes ?? new List<TotalWithTaxesModel>())
                            .Select(t => new FacturaInfoFacturaTotalConImpuestosTotalImpuesto
                            {
                                Codigo = t.TaxCode,
                                CodigoPorcentaje = t.PercentageCode,
                                BaseImponible = t.TaxableBase,
                                Valor = t.Value
                            })
                            .ToList());

            var factura = new Factura
            {
                InfoTributaria = new InfoTributaria
                {
                    Ambiente = invoiceModel.Enviroment,
                    TipoEmision = invoiceModel.EmissionType,
                    RazonSocial = invoiceModel.CompanyName,
                    NombreComercial = invoiceModel.TradeMark,
                    Ruc = invoiceModel.Ruc,
                    ClaveAcceso = invoiceModel.AccessKey,
                    CodDoc = invoiceModel.DocumentCode,
                    Estab = invoiceModel.Establishment,
                    PtoEmi = invoiceModel.EmissionPoint,
                    Secuencial = invoiceModel.Sequential,
                    DirMatriz = invoiceModel.EstablishmentAddress
                },

                InfoFactura = new FacturaInfoFactura
                {
                    FechaEmision = invoiceModel.IssueDate.ToString("dd/MM/yyyy"),
                    DirEstablecimiento = invoiceModel.EstablishmentAddress,
                    ContribuyenteEspecial = invoiceModel.SpecialTaxPayer,
                    ObligadoContabilidad = invoiceModel.RequiredKeepAccounting == "SI"
                        ? ObligadoContabilidad.Si
                        : ObligadoContabilidad.No,
                    TipoIdentificacionComprador = invoiceModel.BuyerIdentificationType,
                    IdentificacionComprador = invoiceModel.Customer.Identification,
                    RazonSocialComprador = invoiceModel.Customer.Name,
                    DireccionComprador = invoiceModel.Customer.Address,
                    TotalSinImpuestos = invoiceModel.TotalWiyhoutTaxes,
                    TotalDescuento = invoiceModel.TotalDiscount,
                    Propina = invoiceModel.Tip,
                    ImporteTotal = invoiceModel.TotalAmount,
                    Moneda = invoiceModel.Currency
                }
            };

            factura.InfoFactura.TotalConImpuestos.Add(taxesDetails);

            factura.Detalles.Clear();

            foreach (var item in invoiceModel.Items)
            {
                var detalle = new FacturaDetallesDetalle
                {
                    CodigoPrincipal = item.Code,
                    CodigoAuxiliar = item.AuxCode,
                    Descripcion = item.Description,
                    Cantidad = item.Quantity,
                    PrecioUnitario = item.UnitPrice,
                    Descuento = item.Discount,
                    PrecioTotalSinImpuesto = item.TotalWiyhoutTaxes,
                };

                // Impuestos por detalle
                detalle.Impuestos = new Collection<FacturaDetallesDetalleImpuestosImpuesto>(
                    item.Taxes.Select(t => new FacturaDetallesDetalleImpuestosImpuesto
                    {
                        Codigo = t.TaxCode,
                        CodigoPorcentaje = t.PercentageCode,
                        Tarifa = t.Rate,
                        BaseImponible = t.TaxableBase,
                        Valor = t.Value
                    }).ToList()
                );

                factura.Detalles.Add(detalle);
            }

            return factura;
        }
    }
}
