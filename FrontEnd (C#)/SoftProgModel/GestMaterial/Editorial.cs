using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestMaterial
{
    public class Editorial
    {
        private int idEditorial;
        private string nombre;
        private BindingList<MaterialBibliografico> materiales;

        public Editorial() { }
        
        
        public int IdEditorial { get => idEditorial; set => idEditorial = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        internal BindingList<MaterialBibliografico> Materiales { get => materiales; set => materiales = value; }
    }
}
