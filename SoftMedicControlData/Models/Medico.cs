using System;
using System.Collections.Generic;

namespace SoftMedicControlData.Models;

public partial class Medico
{
    public int IdMedico { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Especialidad { get; set; } = null!;

    public long Telefono { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public string JornadaAtencion { get; set; } = null!;

    public int? IdEspecialidad { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Especialidades? IdEspecialidadNavigation { get; set; }
}
