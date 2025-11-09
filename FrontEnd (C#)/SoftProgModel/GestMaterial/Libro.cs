using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestMaterial
{
    public class Libro : MaterialBibliografico
    {
        private string ISBN;
        private string edicion;

        public Libro(): base()
        {
        }

        public Libro(string ISBN, string edicion, int idMaterial, int numero_copias, string titulo, int anho_publicacion, 
            int numero_paginas, EstadoMaterial estado, string clasificacion_tematica, bool activo, string idioma, TipoMaterial tipo):
            base(idMaterial, titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, tipo)
        {
            this.ISBNP = ISBN;
            this.Edicion = edicion;
        }

        public string ISBNP { get => ISBN; set => ISBN = value; }
        public string Edicion { get => edicion; set => edicion = value; }
    }
}
