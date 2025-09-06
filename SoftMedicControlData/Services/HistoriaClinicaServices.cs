using Microsoft.EntityFrameworkCore;
using SoftMedicControlData.Data;
using SoftMedicControlData.Models;
using SoftMedicControlData.Shared;

namespace SoftMedicControlData.Services
{
    public class HistoriaClinicaServices
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HistoriaClinicaServices> _logger;

        public HistoriaClinicaServices(AppDbContext context, ILogger<HistoriaClinicaServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<HistorialMedico>> Get_TodasHistorias()
        {
            try
            {
                return await _context.HistorialMedico.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la historia clínica");
                throw;
            }
        }

        public async Task<HistorialMedico> Get_HistorialMedicoPorCedulaPaciente(int cedula)
        {
            try
            {
                var historial = await _context.HistorialMedico
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CedulaPaciente == cedula);

                if (historial is null)
                {
                    //throw new KeyNotFoundException($"No se encontró historial médico para paciente con cédula {cedula}");
                    return null;
                }

                return historial;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial médico para paciente {Cedula}", cedula);
                throw new InvalidOperationException("Error inesperado al consultar la base de datos.", ex);
            }
        }

        public async Task<HistorialMedico> Get_HistorialMedicoPorId(int IdHistoria)
        {
            try
            {
                return await _context.HistorialMedico.FindAsync(IdHistoria)
                       ?? throw new KeyNotFoundException($"Historia con ID {IdHistoria} no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener historia con ID: {IdHistoria}");
                throw;
            }
        }

        public async Task Add_RegistroHistorialMedico(HistorialMedico historialMedico)
        {
            try
            {
                var existente = await Get_HistorialMedicoPorCedulaPaciente(historialMedico.CedulaPaciente);

                if (existente != null)
                {
                    throw new InvalidOperationException($"Ya existe una historia clínica para el paciente con ID {historialMedico.CedulaPaciente}");
                }
                else
                {
                    _context.HistorialMedico.Add(historialMedico);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la historia clinica");
                throw;
            }
        }

        public async Task Update_RegistroHistorialMedico(HistorialMedico HistorialMedico)
        {
            try
            {
                var RegistroExistente = await Get_HistorialMedicoPorId(HistorialMedico.IdHistorial);
                if (RegistroExistente != null)
                {
                    _context.Entry(RegistroExistente).CurrentValues.SetValues(HistorialMedico);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Historial médico del paciente con ID {HistorialMedico.CedulaPaciente} no encontrado para actualización");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar historial médico del paciente con ID: {HistorialMedico.CedulaPaciente}");
                throw;
            }
        }

        //public async Task Delete_RegistroHistorialMedico(int cedula)
        //{
        //    try
        //    {
        //        var RegistroExistente = await Get_HistorialMedicoPorCedulaPaciente(cedula);
        //        if (RegistroExistente != null)
        //        {
        //            RegistroExistente.Activo = false; // Marcar como inactivo en lugar de eliminar
        //            _context.Entry(RegistroExistente).CurrentValues.SetValues(RegistroExistente);
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            throw new KeyNotFoundException($"HistorialMedico con ID {cedula} no encontrado para eliminación");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error al borar HistorialMedico ID: {cedula}");
        //        throw;
        //    }
        //}
    }
}
