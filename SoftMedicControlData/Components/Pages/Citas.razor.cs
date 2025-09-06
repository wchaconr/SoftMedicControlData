using Microsoft.AspNetCore.Components;
using SoftMedicControlData.Models;
using System;
using System.Threading.Tasks;

namespace SoftMedicControlData.Components.Pages
{
    public partial class Citas
    {
        private Cita cita = new();
        private List<VwCita> listaCitas = [];
        private List<Medico> ListaMedicosDisponinles = [];
        private List<ServicioMedico> ListaServiciosMedicos = [];
        private List<Especialidades> ListaEspecialidades = [];

        // Variables de estado
        private bool ventanaNuevaCitaVisible = false;
        private bool ventanaConsultaCitaVisible = false;
        private Cita? citaEditada = null;
        //private bool ventanaEdicionVisible = false;
        private bool ventanaEditVisible = false;
        private bool mostrarPopup = false;
        private bool modoEdicion = false;
        private bool citaRegistradaOK = false;
        private bool citaEditadaOK = false;

        // Filtros
        private int filtroDocumentoPaciente;
        private int filtroDocumentoMedico;
        private DateOnly? filtroFecha;

        private int? _idEspecialidadSeleccionada;
        public int? IdEspecialidadSeleccionada
        {
            get => _idEspecialidadSeleccionada;
            set
            {
                if (_idEspecialidadSeleccionada != value)
                {
                    _idEspecialidadSeleccionada = value;
                    _ = CargarMedicos(); 
                }
            }
        }

        private DateOnly _fechaSeleccionada = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public DateOnly FechaSeleccionada
        {
            get => _fechaSeleccionada;
            set
            {
                if (_fechaSeleccionada != value)
                {
                    _fechaSeleccionada = value;
                    cita.Fecha = value; // actualizar también el modelo
                    _ = CargarMedicos();
                }
            }
        }

        private TimeSpan _horaSeleccionada;
        public TimeSpan HoraSeleccionada
        {
            get => _horaSeleccionada;
            set
            {
                if (_horaSeleccionada != value)
                {
                    _horaSeleccionada = value;
                    cita.Hora = value; // actualizar también el modelo
                    _ = CargarMedicos();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await CargarEspecialidades();
            await CargarCitas();
            await CargarServiciosMedicos();
        }

        private async Task CargarCitas()
        {
            listaCitas = await citaService.Get_TodosCitas();
        }

        private async Task CargarEspecialidades()
        {
            ListaEspecialidades = await medicoService.Get_TodosEspecialidades();
        }

        private async Task CargarServiciosMedicos()
        {
            ListaServiciosMedicos = await citaService.Get_TodosServiciosMedicos();
        }

        private async Task CargarMedicos()
        {
            if (cita != null)
            {
                var TodosMedicos = await medicoService.Get_TodosMedicos();

                if (listaCitas.Count > 0)
                {
                    if (IdEspecialidadSeleccionada != null)
                    {
                        var listaIdsMedicosOcupados = listaCitas.Where(x => x.Fecha == cita.Fecha && x.Hora == cita.Hora).Select(x => x.IdMedico);
                        //Si estoy editando me debe traer el médico que ya está asignado a la cita
                        if (ventanaEditVisible)
                            listaIdsMedicosOcupados = listaIdsMedicosOcupados.Where(x => !x.Equals(citaEditada?.IdMedico));
                        ListaMedicosDisponinles = [.. TodosMedicos.Where(x => !listaIdsMedicosOcupados.Contains(x.IdMedico) && x.IdEspecialidad == IdEspecialidadSeleccionada)];
                        StateHasChanged();
                    }
                }
                else
                {                    
                    if (IdEspecialidadSeleccionada != null)
                    {
                        var jornada = cita.Hora.Hours < 12 ? "Mañana" : "Tarde";
                        ListaMedicosDisponinles = [.. TodosMedicos.Where(x => x.IdEspecialidad == IdEspecialidadSeleccionada && x.JornadaAtencion == jornada)];
                        StateHasChanged();
                    }
                }
            }
        }

        private async Task GuardarCita()
        {
            if (cita != null)
            {
                await citaService.Add_RegistroCita(cita);

                //await CargarCitas();
                mostrarPopup = true;
                citaEditadaOK = false;
                citaRegistradaOK = true;

                // Actualizar UI
                StateHasChanged();
                LimpiarFormulario();
            }
        }

        private void AbrirVentanaNuevaCita()
        {
            ventanaNuevaCitaVisible = true;
            ventanaConsultaCitaVisible = false;
            modoEdicion = false;
        }

        private void AbrirVentanaConsultarCita()
        {
            ventanaConsultaCitaVisible = true;
            ventanaNuevaCitaVisible = false;
            modoEdicion = true;
        }

        private async Task EliminarCita(VwCita cita)
        {
            await citaService.Delete_RegistroCita(cita.IdCita);
            await CargarMedicos();
        }

        //private void Cancelar()
        //{
        //    LimpiarFormulario();
        //}

        private void LimpiarFormulario()
        {
            cita = new Cita();
            StateHasChanged();
        }

        private async Task AbrirVentanaFlotante(VwCita cita)
        {
            FechaSeleccionada = cita.Fecha;
            HoraSeleccionada = cita.Hora;
            IdEspecialidadSeleccionada = (await medicoService.Get_MedicoPorCedula(cita.IdMedico)).IdEspecialidad;

            //_ = CargarMedicos();
            //// Actualizar UI
            //StateHasChanged();

            // Clonar medico para edición
            citaEditada = new Cita
            {
                Fecha = cita.Fecha, 
                Hora = cita.Hora,
                CedulaPaciente = cita.CedulaPaciente,
                IdServicio = cita.IdServicio,
                Motivo = cita.Motivo,
                Estado = cita.Estado,
                IdMedico = cita.IdMedico,
                Activo = true
            };

            ventanaEditVisible = true;
        }

        private void CerrarVentanaEdit()
        {
            ventanaEditVisible = false;
            citaEditada = null;
            FechaSeleccionada = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            HoraSeleccionada = new TimeSpan();
            IdEspecialidadSeleccionada = 0;

            _ = CargarMedicos();
            // Actualizar UI
            StateHasChanged();
        }

        private async Task GuardarCambios()
        {
            if (citaEditada == null) return;

            try
            {
                // Buscar medico original
                var original = listaCitas.FirstOrDefault(p =>
                    p.IdCita == citaEditada.IdCita);

                if (original != null)
                {
                    // Actualizar propiedades
                    var citaExistenteEditada = new Cita
                    {
                        IdCita = original.IdCita,
                        Fecha = citaEditada.Fecha,
                        Hora = citaEditada.Hora,
                        CedulaPaciente = citaEditada.CedulaPaciente,
                        IdServicio = citaEditada.IdServicio,
                        Motivo = citaEditada.Motivo,
                        Estado = citaEditada.Estado,
                        IdMedico = citaEditada.IdMedico,
                        Activo = true
                    };

                    // Guardar en BD
                    await citaService.Update_RegistroCita(citaExistenteEditada);

                    //await CargarCitas();
                    mostrarPopup = true;
                    citaEditadaOK = true;
                    citaRegistradaOK = false;

                    // Actualizar UI
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                // Manejar error (opcional)
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
            finally
            {
                CerrarVentanaEdit();
            }
        }

        //public class VentanaFlotante
        //{
        //    public Medico medicoAEditar { get; set; } = new();
        //    public string EstiloPosicion { get; set; } = "left:100px; top:100px; z-index:100;";
        //    public bool Minimizada { get; set; }
        //    public int ZIndex { get; set; } = 100;
        //}
    }
}