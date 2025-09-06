using System;
using System.Collections.Generic;

namespace SoftMedicControlData.Models;

public partial class Cita
{
    public  int IdCita { get; set; }

    public  DateOnly Fecha { get; set; } = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    public  TimeSpan Hora { get; set; }

    public  string Motivo { get; set; } = null!;

    public  string? Estado { get; set; } = null!;

    public  int CedulaPaciente { get; set; }

    public  int IdMedico { get; set; }

    public  int IdServicio { get; set; }

    public  bool Activo { get; set; } = true;

    public virtual Paciente CedulaPacienteNavigation { get; set; } = null!;

    public virtual Medico IdMedicoNavigation { get; set; } = null!;

    public virtual ServicioMedico IdServicioNavigation { get; set; } = null!;
}
