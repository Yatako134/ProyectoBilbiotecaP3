using SoftProgModel.GestPrestamos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestMaterial
{
    public class Ejemplar
    {
        private int idEjemplar;
        private EstadoEjemplar estado;
        private string ubicacion;
        private Biblioteca biblioteca;
        private BindingList<Prestamo> prestamos;
        private MaterialBibliografico material;
        private bool activo;

        public Ejemplar()
        {
            Prestamos = new BindingList<Prestamo>();
        }

        public Ejemplar(int idEjemplar, EstadoEjemplar estado, string ubicacion,
               Biblioteca biblioteca, MaterialBibliografico material, bool activo)
        {
            this.IdEjemplar = idEjemplar;
            this.Estado = estado;
            this.Ubicacion = ubicacion;
            this.Biblioteca = biblioteca;
            this.Material = material;
            this.Activo = activo;
        }

        public int IdEjemplar { get => idEjemplar; set => idEjemplar = value; }
        public EstadoEjemplar Estado { get => estado; set => estado = value; }
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }
        public Biblioteca Biblioteca { get => biblioteca; set => biblioteca = value; }
        public BindingList<Prestamo> Prestamos { get => prestamos; set => prestamos = value; }
        public MaterialBibliografico Material { get => material; set => material = value; }
        public bool Activo { get => activo; set => activo = value; }
    }
}
