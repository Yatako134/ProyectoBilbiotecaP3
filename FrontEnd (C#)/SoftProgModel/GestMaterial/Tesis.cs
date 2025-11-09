using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestMaterial
{
    public class Tesis : MaterialBibliografico
    {
        private string institucionPublicacion;
        private string especialidad;
        private string asesor;
        private string grado;

        public Tesis(): base()
        {
        }

        public Tesis(string institucionPublicacion, string especialidad, string asesor, string grado, int idMaterial, int numero_copias, string titulo, 
            int anho_publicacion, int numero_paginas, EstadoMaterial estado, string clasificacion_tematica, bool activo, string idioma, TipoMaterial tipo):
            base (idMaterial, titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, tipo)
        {
            this.InstitucionPublicacion = institucionPublicacion;
            this.Especialidad = especialidad;
            this.Asesor = asesor;
            this.Grado = grado;
        }

        public string InstitucionPublicacion { get => institucionPublicacion; set => institucionPublicacion = value; }
        public string Especialidad { get => especialidad; set => especialidad = value; }
        public string Asesor { get => asesor; set => asesor = value; }
        public string Grado { get => grado; set => grado = value; }
    }
}
