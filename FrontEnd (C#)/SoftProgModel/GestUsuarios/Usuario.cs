using SoftProgModel.GestPrestamos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestUsuarios
{
    public class Usuario
    {
        private int id_usuario;
        private int codigo_universitario;
        private string nombre;
        private string primer_apellido;
        private string segundo_apellido;
        private int DOI;
        private string contrasena;
        private string correo;
        private string numero_de_telefono;
        private bool activa;
        private Rol rol_usuario;
        private BindingList<Prestamo> prestamos;

        public Usuario()
        {
        }

        public Usuario(int id_usuario, int codigo_universitario, string nombre, string primer_apellido, string segundo_apellido,
            int DOI, string correo, string contrasena, string numero_de_telefono, Rol rol_usuario)
        {
            this.Id_usuario = id_usuario;
            this.Codigo_universitario = codigo_universitario;
            this.Nombre = nombre;
            this.Primer_apellido = primer_apellido;
            this.Segundo_apellido = segundo_apellido;
            this.DOI1 = DOI;
            this.Correo = correo;
            this.Contrasena = contrasena;
            this.Numero_de_telefono = numero_de_telefono;
            this.Rol_usuario = rol_usuario;
        }

        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public int Codigo_universitario { get => codigo_universitario; set => codigo_universitario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Primer_apellido { get => primer_apellido; set => primer_apellido = value; }
        public string Segundo_apellido { get => segundo_apellido; set => segundo_apellido = value; }
        public int DOI1 { get => DOI; set => DOI = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Numero_de_telefono { get => numero_de_telefono; set => numero_de_telefono = value; }
        public bool Activa { get => activa; set => activa = value; }
        public Rol Rol_usuario { get => rol_usuario; set => rol_usuario = value; }
        public BindingList<Prestamo> Prestamos { get => prestamos; set => prestamos = value; }
    }
}
