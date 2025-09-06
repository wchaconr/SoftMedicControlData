using Microsoft.EntityFrameworkCore;
using SoftMedicControlData.Data;
using SoftMedicControlData.Models;

namespace SoftMedicControlData.Services
{
    public class MedicoServices 
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MedicoServices> _logger;

        public MedicoServices(AppDbContext context, ILogger<MedicoServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Medico>> Get_TodosMedicos()
        {
            try
            {
                return await _context.Medico.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los registros");
                throw;
            }
        }

        public async Task<Medico> Get_MedicoPorCedula(int cedula)
        {
            try
            {
                return await _context.Medico.FindAsync(cedula)
                       ?? throw new KeyNotFoundException($"Médico con ID {cedula} no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener registro con ID: {cedula}");
                throw;
            }
        }

        public async Task Add_RegistroMedico(Medico medico)
        {
            try
            {
                _context.Medico.Add(medico);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el registro");
                throw;
            }
        }

        public async Task Update_RegistroMedico(Medico medico)
        {
            try
            {
                var RegistroExistente = await Get_MedicoPorCedula(medico.IdMedico);
                if (RegistroExistente != null)
                {
                    _context.Entry(RegistroExistente).CurrentValues.SetValues(medico);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Médico con ID {medico.IdMedico} no encontrado para actualización");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar médico ID: {medico.IdMedico}");
                throw;
            }
        }

        public async Task Delete_RegistroMedico(int cedula)
        {
            try
            {
                var RegistroExistente = await Get_MedicoPorCedula(cedula);
                if (RegistroExistente != null)
                {
                    _context.Remove(RegistroExistente);
                    //_context.Entry(RegistroExistente).CurrentValues.SetValues(RegistroExistente);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Médico con ID {cedula} no encontrado para eliminación");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al borar Médico ID: {cedula}");
                throw;
            }
        }

        public async Task<List<Especialidades>> Get_TodosEspecialidades()
        {
            try
            {
                return await _context.Especialidades.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los registros");
                throw;
            }
        }
    }
}
