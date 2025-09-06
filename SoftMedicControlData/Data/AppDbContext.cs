using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SoftMedicControlData.Models;

namespace SoftMedicControlData.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Cita { get; set; }

    public virtual DbSet<Especialidades> Especialidades { get; set; }

    public virtual DbSet<Factura> Factura { get; set; }

    public virtual DbSet<HistorialMedico> HistorialMedico { get; set; }

    public virtual DbSet<Medico> Medico { get; set; }

    public virtual DbSet<Paciente> Paciente { get; set; }

    public virtual DbSet<RecetaMedica> RecetaMedica { get; set; }

    public virtual DbSet<ServicioMedico> ServicioMedico { get; set; }

    public virtual DbSet<VwCita> VwCita { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Cita__7C17FD1683F70FEB");

            entity.ToTable("Cita", "gcm");

            entity.Property(e => e.IdCita).HasColumnName("ID_Cita");
            entity.Property(e => e.CedulaPaciente).HasColumnName("Cedula_Paciente");
            entity.Property(e => e.IdMedico).HasColumnName("ID_Medico");
            entity.Property(e => e.IdServicio).HasColumnName("ID_Servicio");
            entity.Property(e => e.Motivo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CedulaPacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.CedulaPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__Cedula_Pac__5EBF139D");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdMedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__ID_Medico__5FB337D6");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__ID_Servici__60A75C0F");
        });

        modelBuilder.Entity<Especialidades>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__Especial__5D7732D78A3623F6");

            entity.ToTable("Especialidades", "gcm");

            entity.Property(e => e.IdEspecialidad).HasColumnName("ID_Especialidad");
            entity.Property(e => e.NombreEspecialidad)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__E9D586A8FF77D30A");

            entity.ToTable("Factura", "gcm");

            entity.Property(e => e.IdFactura).HasColumnName("ID_Factura");
            entity.Property(e => e.CedulaDeudor).HasColumnName("Cedula_deudor");
            entity.Property(e => e.EstadoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Estado_Pago");
            entity.Property(e => e.IdServicio).HasColumnName("ID_Servicio");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Monto_Total");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Factura)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__ID_Serv__5812160E");
        });

        modelBuilder.Entity<HistorialMedico>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__ECA8945430255106");

            entity.ToTable("Historial_Medico", "gcm");

            entity.Property(e => e.IdHistorial).HasColumnName("ID_Historial");
            entity.Property(e => e.CedulaPaciente).HasColumnName("Cedula_Paciente");
            entity.Property(e => e.Diagnostico).IsUnicode(false);
            entity.Property(e => e.Notas).IsUnicode(false);
            entity.Property(e => e.Tratamientos).IsUnicode(false);

            entity.HasOne(d => d.CedulaPacienteNavigation).WithMany(p => p.HistorialMedico)
                .HasForeignKey(d => d.CedulaPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Cedul__6383C8BA");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.IdMedico).HasName("PK__Medico__EFBF88F76BD55A3E");

            entity.ToTable("Medico", "gcm");

            entity.Property(e => e.IdMedico)
                .ValueGeneratedNever()
                .HasColumnName("ID_Medico");
            entity.Property(e => e.Apellido)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Correo_Electronico");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdEspecialidad).HasColumnName("ID_Especialidad");
            entity.Property(e => e.JornadaAtencion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Jornada_Atencion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Medico)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK__Medico__ID_Espec__6FE99F9F");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.CedulaPaciente).HasName("PK__Paciente__5FD3BC9D93B7B1BA");

            entity.ToTable("Paciente", "gcm");

            entity.Property(e => e.CedulaPaciente)
                .ValueGeneratedNever()
                .HasColumnName("Cedula_Paciente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Correo_Electronico");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RecetaMedica>(entity =>
        {
            entity.HasKey(e => e.IdReceta).HasName("PK__Receta_M__19C0463101029AF3");

            entity.ToTable("Receta_Medica", "gcm");

            entity.Property(e => e.IdReceta).HasColumnName("ID_Receta");
            entity.Property(e => e.Dosis)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DuracionDias).HasColumnName("Duracion_Dias");
            entity.Property(e => e.IdHistorial).HasColumnName("ID_Historial");
            entity.Property(e => e.Instrucciones).IsUnicode(false);
            entity.Property(e => e.Medicamento)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdHistorialNavigation).WithMany(p => p.RecetaMedica)
                .HasForeignKey(d => d.IdHistorial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Receta_Me__ID_Hi__66603565");
        });

        modelBuilder.Entity<ServicioMedico>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__1932F584844CFF89");

            entity.ToTable("Servicio_Medico", "gcm");

            entity.Property(e => e.IdServicio).HasColumnName("ID_Servicio");
            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripción)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Servicio");
        });

        modelBuilder.Entity<VwCita>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Cita", "gcm");

            entity.Property(e => e.CedulaPaciente).HasColumnName("Cedula_Paciente");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdCita).HasColumnName("ID_Cita");
            entity.Property(e => e.IdMedico).HasColumnName("ID_Medico");
            entity.Property(e => e.IdServicio).HasColumnName("ID_Servicio");
            entity.Property(e => e.Motivo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NombreMedico)
                .HasMaxLength(401)
                .IsUnicode(false)
                .HasColumnName("Nombre_Medico");
            entity.Property(e => e.NombrePaciente)
                .HasMaxLength(401)
                .IsUnicode(false)
                .HasColumnName("Nombre_Paciente");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Servicio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
