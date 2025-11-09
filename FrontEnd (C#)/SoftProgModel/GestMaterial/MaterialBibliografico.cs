using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestMaterial
{

    public class MaterialBibliografico
    {
        private int idMaterial;
        private string titulo;
        private int anho_publicacion;
        private int numero_paginas;
        private EstadoMaterial estado;
        private string clasificacion_tematica;
        private bool activo;
        private string idioma;
        private TipoMaterial tipo;
        private BindingList<Editorial> editoriales;
        private BindingList<Contribuyente> contribuyentes;
        private BindingList<Ejemplar> ejemplares;
        private BindingList<Biblioteca> bibliotecas;
        private int cantidadDisponibles;
        public string AutoresTexto
        {
            get
            {
                if (contribuyentes == null || contribuyentes.Count == 0)
                    return "No registrados";
                return string.Join(", ", contribuyentes.Select(a => a.Nombre + " " + a.Primer_apellido));
            }
        }
        public string BibliotecasTexto
        {
            get
            {
                if (bibliotecas == null || bibliotecas.Count == 0)
                    return "No disponible";
                return string.Join(", ", bibliotecas.Select(a => a.Nombre));
            }
        }

        public BindingList<Editorial> getEditoriales()
        {
            return Editoriales;
        }

        public void setEditoriales(BindingList<Editorial> editoriales)
        {
            this.Editoriales = editoriales;
        }

        public MaterialBibliografico()
        {
            this.Editoriales = new BindingList<Editorial>();
            this.Contribuyentes = new BindingList<Contribuyente>();
            this.Ejemplares = new BindingList<Ejemplar>();
            this.bibliotecas = new BindingList<Biblioteca>();
        }

        public MaterialBibliografico(int idMaterial, string titulo,
                int anho_publicacion, int numero_paginas, EstadoMaterial estado,
                string clasificacion_tematica, bool activo, string idioma, TipoMaterial tipo)
        {
            this.IdMaterial = idMaterial;
            this.Titulo = titulo;
            this.Anho_publicacion = anho_publicacion;
            this.Numero_paginas = numero_paginas;
            this.Estado = estado;
            this.Clasificacion_tematica = clasificacion_tematica;
            this.Activo = activo;
            this.Idioma = idioma;
            this.Tipo = tipo;
            this.Contribuyentes = new BindingList<Contribuyente>();
            this.Ejemplares = new BindingList<Ejemplar>();
        }



        public int IdMaterial { get => idMaterial; set => idMaterial = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public int Anho_publicacion { get => anho_publicacion; set => anho_publicacion = value; }
        public int Numero_paginas { get => numero_paginas; set => numero_paginas = value; }
        public EstadoMaterial Estado { get => estado; set => estado = value; }
        public string Clasificacion_tematica { get => clasificacion_tematica; set => clasificacion_tematica = value; }
        public bool Activo { get => activo; set => activo = value; }
        public string Idioma { get => idioma; set => idioma = value; }
        public TipoMaterial Tipo { get => tipo; set => tipo = value; }
        public BindingList<Editorial> Editoriales { get => editoriales; set => editoriales = value; }
        public BindingList<Contribuyente> Contribuyentes { get => contribuyentes; set => contribuyentes = value; }
        public BindingList<Ejemplar> Ejemplares { get => ejemplares; set => ejemplares = value; }
        public BindingList<Biblioteca> Bibliotecas { get => bibliotecas; set => bibliotecas = value; }
        public int CantidadDisponibles { get => cantidadDisponibles; set => cantidadDisponibles = value; }
    }
}
