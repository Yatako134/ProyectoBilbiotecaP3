using SoftProgDBManager;
using SoftProgModel.GestMaterial;
using SoftProgPersistance.DAO;
using SoftProgPersistance.GestMaterial.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestMaterial.Impl
{
    public class MaterialImpl : MaterialDAO
    {
        private DbDataReader lector;
        int IDAO<MaterialBibliografico>.eliminar(int idObjeto)
        {
            throw new NotImplementedException();
        }

        int IDAO<MaterialBibliografico>.insertar(MaterialBibliografico objeto)
        {
            throw new NotImplementedException();
        }

        BindingList<MaterialBibliografico> IDAO<MaterialBibliografico>.listarTodos()
        {
            BindingList<MaterialBibliografico> materiales = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_MATERIALES_TODOS", null);
            while (lector.Read())
            {
                if (materiales == null) materiales = new BindingList<MaterialBibliografico>();
                MaterialBibliografico material = new MaterialBibliografico();
                if (!lector.IsDBNull(lector.GetOrdinal("id_material"))) material.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_material"));
                if (!lector.IsDBNull(lector.GetOrdinal("titulo"))) material.Titulo = lector.GetString(lector.GetOrdinal("titulo"));
                if (!lector.IsDBNull(lector.GetOrdinal("anho_publicacion"))) material.Anho_publicacion = lector.GetInt32(lector.GetOrdinal("anho_publicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_paginas"))) material.Numero_paginas = lector.GetInt32(lector.GetOrdinal("numero_paginas"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) material.Estado = (EstadoMaterial)Enum.Parse(typeof(EstadoMaterial), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("clasificacion_tematica"))) material.Clasificacion_tematica = lector.GetString(lector.GetOrdinal("clasificacion_tematica"));
                if (!lector.IsDBNull(lector.GetOrdinal("idioma"))) material.Idioma = lector.GetString(lector.GetOrdinal("idioma"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) material.Tipo = (TipoMaterial)Enum.Parse(typeof(TipoMaterial), lector.GetString(lector.GetOrdinal("tipo")));
                ContribuyenteDAO contribuyenteDAO = new ContribuyenteImpl();
                BibliotecaDAO bibliotecaDAO = new BibliotecaImpl();
                EjemplarDAO ejemplarDAO = new EjemplarImpl();
                material.Ejemplares = ejemplarDAO.listar_disponibles_por_material(material.IdMaterial);
                if (material.Ejemplares != null) material.CantidadDisponibles = material.Ejemplares.Count();
                else material.CantidadDisponibles = 0;
                    material.Bibliotecas = bibliotecaDAO.listar_bibliotecas_por_material(material.IdMaterial);
                material.Contribuyentes = contribuyenteDAO.listar_autores_por_material(material.IdMaterial);
                materiales.Add(material);
            }
            DBManager.Instance.CerrarConexion();
            return materiales;
        }

        int IDAO<MaterialBibliografico>.modificar(MaterialBibliografico objeto)
        {
            throw new NotImplementedException();
        }

        MaterialBibliografico IDAO<MaterialBibliografico>.obtenerPorId(int idObjeto)
        {
            throw new NotImplementedException();
        }
    }
}
