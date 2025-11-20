using EInvoice.Infrastructure.Domain.Entities;
using EInvoice.Infrastructure.Sri.Invoice.Generated;
using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
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
                    TipoIdentificacionComprador = invoiceModel.CustomerIdentificationType,
                    IdentificacionComprador = invoiceModel.Customer.Identification,
                    RazonSocialComprador = invoiceModel.Customer.FullName,
                    DireccionComprador = invoiceModel.Customer.Address,
                    TotalSinImpuestos = invoiceModel.TotalWithoutTaxes,
                    TotalDescuento = invoiceModel.TotalDiscount,
                    Propina = invoiceModel.Tip,
                    ImporteTotal = invoiceModel.TotalAmount,
                    Moneda = invoiceModel.Currency
                }
            };

            foreach (var item in taxesDetails)
            {
                factura.InfoFactura.TotalConImpuestos.Add(item);
            }


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
                    PrecioTotalSinImpuesto = item.TotalWithoutTaxes,
                };

                // Impuestos por detalle
                foreach (var tax in item.Taxes ?? new List<TaxesModel>())
                {
                    var impuesto = new Impuesto
                    {
                        Codigo = tax.TaxCode,
                        CodigoPorcentaje = tax.PercentageCode,
                        Tarifa = tax.Rate,
                        BaseImponible = tax.TaxableBase,
                        Valor = tax.Value
                    };
                    detalle.Impuestos.Add(impuesto);
                }


                factura.Detalles.Add(detalle);
            }

            // Pagos (si existen)

            if (invoiceModel.Payments != null && invoiceModel.Payments.Any())
            {
                foreach (var payment in invoiceModel.Payments)
                {
                    factura.InfoFactura.Pagos.Add(new PagosPago
                    {
                        FormaPago = payment.Method,
                        Total = payment.Amount,
                        Plazo = payment.Term,
                        UnidadTiempo = payment.TimeUnit
                    });
                }
            }

            // Campos Adicionales (si existen)
            if (invoiceModel.AdditionalFields != null && invoiceModel.AdditionalFields.Any())
            {
                foreach (var field in invoiceModel.AdditionalFields)
                {
                    factura.InfoAdicional.Add(new FacturaInfoAdicionalCampoAdicional
                    {
                        Nombre = field.Name,
                        Value = field.Value
                    });
                }
            }

            return factura;
        }

        public static InvoiceHeaderDto ToHeaderDto(this Invoice invoice)
        {
            return new InvoiceHeaderDto
            {
                Id = invoice.Id,
                AccessKey = invoice.AccessKey,
                DocumentCode = invoice.DocumentCode,
                EstablishmentCode = invoice.EstablishmentCode,
                EmissionPointCode = invoice.EmissionPointCode,
                Sequential = invoice.Sequential,
                IssueDate = invoice.IssueDate,
                Ruc = invoice.Ruc,
                TotalAmount = invoice.TotalAmount,
                CustomerId = invoice.CustomerId,
                CompanyId = invoice.CompanyId,
                EmissionPointId = invoice.EmissionPointId,
                JsonData = invoice.JsonData,
                StatusId = invoice.StatusId,
                XmlGenerated = invoice.XmlGenerated,
                XmlSigned = invoice.XmlSigned,
                XmlAuthorized = invoice.XmlAuthorized,
                SriResponse = invoice.SriResponse
            };
        }
    }
}
