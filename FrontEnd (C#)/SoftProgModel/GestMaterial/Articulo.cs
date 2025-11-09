using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestMaterial
{
    public class Articulo : MaterialBibliografico
    {
        private string ISSN;
        private string revista;
        private int volumen;
        private int numero;

        public Articulo() : base()
        {
        }
        public Articulo(
        string ISSN, string revista, int volumen, int numero,
        int idMaterial, int numero_copias, string titulo, int anho_publicacion,
        int numero_paginas, EstadoMaterial estado, string clasificacion_tematica,
        bool activo, string idioma, TipoMaterial tipo) : 
            base(idMaterial, titulo, anho_publicacion, numero_paginas,
                estado, clasificacion_tematica, activo, idioma, tipo)
        {
            this.ISSNP = ISSN;
            this.Revista = revista;
            this.Volumen = volumen;
            this.Numero = numero;
        }

        public string ISSNP { get => ISSN; set => ISSN = value; }
        public string Revista { get => revista; set => revista = value; }
        public int Volumen { get => volumen; set => volumen = value; }
        public int Numero { get => numero; set => numero = value; }
    }
}
