using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestMaterial
{
    public class Biblioteca
    {
        private int idBiblioteca;
        private string nombre;
        private string ubicacion;
        private bool activo;
        public Biblioteca() { }
        public Biblioteca(int idBiblioteca, string nombre, string ubicacion, bool activo)
        {
            this.IdBiblioteca = idBiblioteca;
            this.Nombre = nombre;
            this.Ubicacion = ubicacion;
            this.Activo = activo;
        }

        public int IdBiblioteca { get => idBiblioteca; set => idBiblioteca = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }
        public bool Activo { get => activo; set => activo = value; }
    }
}
