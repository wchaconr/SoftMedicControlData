using System;
using System.Collections.Generic;

namespace SoftMedicControlData.Models;

public partial class HistorialMedico
{
    public int IdHistorial { get; set; }

    public string Diagnostico { get; set; } = null!;

    public string Tratamientos { get; set; } = null!;

    public DateOnly Fecha { get; set; } = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    public string? Notas { get; set; }

    public int CedulaPaciente { get; set; }

    public virtual Paciente CedulaPacienteNavigation { get; set; } = null!;

    public virtual ICollection<RecetaMedica> RecetaMedica { get; set; } = new List<RecetaMedica>();
}
