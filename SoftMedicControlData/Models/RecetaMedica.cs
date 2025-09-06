using System;
using System.Collections.Generic;

namespace SoftMedicControlData.Models;

public partial class RecetaMedica
{
    public int IdReceta { get; set; }

    public DateOnly Fecha { get; set; }

    public string Medicamento { get; set; } = null!;

    public string Dosis { get; set; } = null!;

    public int DuracionDias { get; set; }

    public string? Instrucciones { get; set; }

    public int IdHistorial { get; set; }

    public virtual HistorialMedico IdHistorialNavigation { get; set; } = null!;
}
