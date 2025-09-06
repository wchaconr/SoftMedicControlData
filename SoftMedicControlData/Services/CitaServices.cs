using Microsoft.EntityFrameworkCore;
using SoftMedicControlData.Data;
using SoftMedicControlData.Models;

namespace SoftMedicControlData.Services
{
    public class CitaServices
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CitaServices> _logger;

        public CitaServices(AppDbContext context, ILogger<CitaServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<VwCita>> Get_TodosCitas()
        {
            try
            {
                return await _context.VwCita.OrderByDescending(x => x.Fecha).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los registros");
                throw;
            }
        }

        public async Task<List<ServicioMedico>> Get_TodosServiciosMedicos()
        {
            try
            {
                return await _context.ServicioMedico.OrderBy(x => x.NombreServicio).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los registros");
                throw;
            }
        }

        public async Task<List<Cita>> Get_CitaPorCedulaPaciente(int cedulaPaciente)
        {
            try
            {
                return await _context.Cita.Where(x => x.CedulaPaciente == cedulaPaciente).ToListAsync()
                       ?? throw new KeyNotFoundException($"No se entontraron citas para el paciente con ID {cedulaPaciente}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener registros");
                throw;
            }
        }

        public async Task<Cita> Get_CitaPorId(int IdCita)
        {
            try
            {
                return await _context.Cita.FindAsync(IdCita)
                       ?? throw new KeyNotFoundException($"No se entontraró la cita conel ID {IdCita}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el registro");
                throw;
            }
        }

        public async Task Add_RegistroCita(Cita Cita)
        {
            try
            {
                _context.Cita.Add(Cita);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el registro");
                throw;
            }
        }

        public async Task Update_RegistroCita(Cita Cita)
        {
            try
            {
                var RegistroExistente = await Get_CitaPorId(Cita.IdCita);
                if (RegistroExistente != null)
                {
                    _context.Entry(RegistroExistente).CurrentValues.SetValues(Cita);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Cita no encontrada para actualización");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la cita ID: {Cita.IdCita}");
                throw;
            }
        }

        public async Task Delete_RegistroCita(int IdCita)
        {
            try
            {
                var RegistroExistente = await Get_CitaPorId(IdCita);
                if (RegistroExistente != null)
                {
                    RegistroExistente.Activo = false;
                    _context.Entry(RegistroExistente).CurrentValues.SetValues(RegistroExistente);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Cita no encontrada para eliminación");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al borar la cita con ID: {IdCita}");
                throw;
            }
        }
    }
}
