using System;
using System.Collections.Generic;

namespace SoftMedicControlData.Models;

public partial class Paciente
{
    public int CedulaPaciente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; } = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    public string Genero { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public long Telefono { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public bool Activo { get; set; } = true;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<HistorialMedico> HistorialMedico { get; set; } = new List<HistorialMedico>();
}
