using System;
using System.Collections.Generic;

namespace SoftMedicControlData.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal MontoTotal { get; set; }

    public string EstadoPago { get; set; } = null!;

    public int IdServicio { get; set; }

    public int CedulaDeudor { get; set; }

    public virtual ServicioMedico IdServicioNavigation { get; set; } = null!;
}
