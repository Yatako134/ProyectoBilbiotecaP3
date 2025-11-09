using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestPrestamos
{
    public class Sancion
    {
        private int id_sancion;
        private Tipo_sancion tipo_sancion;
        private int duracion_dias;
        private DateTime fecha_inicio;
        private DateTime fecha_fin;
        private string justificacion;
        private EstadoSancion estado;
        private Prestamo prestamo;

        public Sancion()
        {
        }

        public Sancion(int id_sancion, Tipo_sancion tipo_sancion, int duracion_dias, DateTime fecha_inicio, DateTime fecha_fin, 
            string justificacion, EstadoSancion estado, Prestamo prestamo)
        {
            this.Id_sancion = id_sancion;
            this.Tipo_sancion = tipo_sancion;
            this.Duracion_dias = duracion_dias;
            this.Fecha_inicio = fecha_inicio;
            this.Fecha_fin = fecha_fin;
            this.Justificacion = justificacion;
            this.Estado = estado;
            this.Prestamo = prestamo;
        }

        public int Id_sancion { get => id_sancion; set => id_sancion = value; }
        public Tipo_sancion Tipo_sancion { get => tipo_sancion; set => tipo_sancion = value; }
        public int Duracion_dias { get => duracion_dias; set => duracion_dias = value; }
        public DateTime Fecha_inicio { get => fecha_inicio; set => fecha_inicio = value; }
        public DateTime Fecha_fin { get => fecha_fin; set => fecha_fin = value; }
        public string Justificacion { get => justificacion; set => justificacion = value; }
        public EstadoSancion Estado { get => estado; set => estado = value; }
        public Prestamo Prestamo { get => prestamo; set => prestamo = value; }
    }
}
