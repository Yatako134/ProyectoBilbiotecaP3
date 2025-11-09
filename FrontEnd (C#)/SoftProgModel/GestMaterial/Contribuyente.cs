using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgModel.GestMaterial
{
    public class Contribuyente
    {
        private int idContribuyente;
        private string nombre;
        private string primer_apellido;
        private string segundo_apellido;
        private string seudonimo;
        private TipoContribuyente tipo_contribuyente;
        private BindingList<MaterialBibliografico> materiales;
        public Contribuyente() { }

        public Contribuyente(int idContribuyente, string nombre, string primer_apellido, 
            string segundo_apellido, string seudonimo, TipoContribuyente tipo_contribuyente)
        {
            this.IdContribuyente = idContribuyente;
            this.Nombre = nombre;
            this.Primer_apellido = primer_apellido;
            this.Segundo_apellido = segundo_apellido;
            this.Seudonimo = seudonimo;
            this.Tipo_contribuyente = tipo_contribuyente;
        }

        public int IdContribuyente { get => idContribuyente; set => idContribuyente = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Primer_apellido { get => primer_apellido; set => primer_apellido = value; }
        public string Segundo_apellido { get => segundo_apellido; set => segundo_apellido = value; }
        public string Seudonimo { get => seudonimo; set => seudonimo = value; }
        public TipoContribuyente Tipo_contribuyente { get => tipo_contribuyente; set => tipo_contribuyente = value; }
        internal BindingList<MaterialBibliografico> Materiales { get => materiales; set => materiales = value; }
    }
}
