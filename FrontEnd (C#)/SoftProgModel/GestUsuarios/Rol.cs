using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestUsuarios
{
    public class Rol
    {
        private int id_rol;
        private string tipo;
        private bool activo;
        private int cantidad_de_dias_por_prestamo;
        public Rol()
        {
        }

        public Rol(int id_rol, string tipo, bool activo,
                int cantidad_de_dias_por_prestamo)
        {
            this.Id_rol = id_rol;
            this.Tipo = tipo;
            this.Activo = activo;
            this.Cantidad_de_dias_por_prestamo = cantidad_de_dias_por_prestamo;
        }

        public int Id_rol { get => id_rol; set => id_rol = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public bool Activo { get => activo; set => activo = value; }
        public int Cantidad_de_dias_por_prestamo { get => cantidad_de_dias_por_prestamo; set => cantidad_de_dias_por_prestamo = value; }
    }
}
