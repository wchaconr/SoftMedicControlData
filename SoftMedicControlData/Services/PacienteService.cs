using Microsoft.EntityFrameworkCore;
using SoftMedicControlData.Components.Pages;
using SoftMedicControlData.Data;
using SoftMedicControlData.Models;

namespace SoftMedicControlData.Services
{
    public class PacienteService 
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PacienteService> _logger;

        public PacienteService(AppDbContext context, ILogger<PacienteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Paciente>> Get_TodosPacientes()
        {
            try
            {
                return await _context.Paciente.Where(x => x.Activo == true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pacientes");
                throw;
            }
        }

        public async Task<Paciente> Get_PacientePorCedula(int cedula)
        {
            try
            {
                return await _context.Paciente.FindAsync(cedula)
                       ?? throw new KeyNotFoundException($"Paciente con ID {cedula} no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener paciente con ID: {cedula}");
                throw;
            }
        }

        public async Task Add_RegistroPaciente(Paciente paciente)
        {
            try
            {
                _context.Paciente.Add(paciente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar paciente");
                throw;
            }
        }

        public async Task Update_RegistroPaciente(Paciente paciente)
        {
            try
            {
                var RegistroExistente = await Get_PacientePorCedula(paciente.CedulaPaciente);
                if (RegistroExistente != null)
                {
                    _context.Entry(RegistroExistente).CurrentValues.SetValues(paciente);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Paciente con ID {paciente.CedulaPaciente} no encontrado para actualización");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar paciente ID: {paciente.CedulaPaciente}");
                throw;
            }
        }

        public async Task Delete_RegistroPaciente(int cedula)
        {
            try
            {
                var RegistroExistente = await Get_PacientePorCedula(cedula);
                if (RegistroExistente != null)
                {
                    RegistroExistente.Activo = false; // Marcar como inactivo en lugar de eliminar
                    _context.Entry(RegistroExistente).CurrentValues.SetValues(RegistroExistente);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Paciente con ID {cedula} no encontrado para eliminación");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al borar paciente ID: {cedula}");
                throw;
            }
        }
    }
}
