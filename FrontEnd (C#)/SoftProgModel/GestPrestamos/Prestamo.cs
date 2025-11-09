using SoftProgModel.GestMaterial;
using SoftProgModel.GestUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestPrestamos
{
    public class Prestamo
    {
        private int idPrestamo;
        private DateTime fecha_de_prestamo;
        private DateTime fecha_vencimiento;
        private DateTime fecha_devolucion;
        private EstadoPrestamo estado;
        private Ejemplar ejemplar;
        private Sancion sancion;
        private Usuario usuario;

        public Prestamo()
        {
            this.ejemplar = new Ejemplar();
            this.sancion = new Sancion();
            this.usuario = new Usuario();
        }

        public Prestamo(int idPrestamo, DateTime fecha_de_prestamo, DateTime fecha_vencimiento, DateTime fecha_devolucion,
            EstadoPrestamo estado, Ejemplar ejemplar, Sancion sancion, Usuario usuario)
        {
            this.IdPrestamo = idPrestamo;
            this.Fecha_de_prestamo = fecha_de_prestamo;
            this.Fecha_vencimiento = fecha_vencimiento;
            this.Fecha_devolucion = fecha_devolucion;
            this.Estado = estado;
            this.Ejemplar = ejemplar;
            this.Sancion = sancion;
            this.Usuario = usuario;
        }

        public int IdPrestamo { get => idPrestamo; set => idPrestamo = value; }
        public DateTime Fecha_de_prestamo { get => fecha_de_prestamo; set => fecha_de_prestamo = value; }
        public DateTime Fecha_vencimiento { get => fecha_vencimiento; set => fecha_vencimiento = value; }
        public DateTime Fecha_devolucion { get => fecha_devolucion; set => fecha_devolucion = value; }
        public EstadoPrestamo Estado { get => estado; set => estado = value; }
        public Ejemplar Ejemplar { get => ejemplar; set => ejemplar = value; }
        public Sancion Sancion { get => sancion; set => sancion = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
    }
}
