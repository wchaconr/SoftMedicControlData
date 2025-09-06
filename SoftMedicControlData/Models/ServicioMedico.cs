using System;
using System.Collections.Generic;

namespace SoftMedicControlData.Models;

public partial class ServicioMedico
{
    public int IdServicio { get; set; }

    public string NombreServicio { get; set; } = null!;

    public string? Descripción { get; set; }

    public decimal Costo { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Factura> Factura { get; set; } = new List<Factura>();
}
