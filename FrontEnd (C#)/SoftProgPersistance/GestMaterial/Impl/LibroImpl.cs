using SoftProgDBManager;
using SoftProgModel.GestMaterial;
using SoftProgPersistance.GestMaterial.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestMaterial.Impl
{
    public class LibroImpl : LibroDAO
    {
        private DbDataReader lector;
        public int eliminar(int idObjeto)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_libro", DbType.Int32, idObjeto, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("ELIMINAR_LIBRO", parametros);
        }

        public int insertar(Libro libro)
        {
            DbParameter[] parametros = new DbParameter[8];
            parametros[0] = DBManager.Instance.CreateParam("_id_libro", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_titulo", DbType.String, libro.Titulo, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_anho_publicacion", DbType.Int32, libro.Anho_publicacion, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_numero_paginas", DbType.Int32, libro.Numero_paginas, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_clasificacion_tematica", DbType.String, libro.Clasificacion_tematica, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_idioma", DbType.String, libro.Idioma, ParameterDirection.Input);
            parametros[6] = DBManager.Instance.CreateParam("_ISBN", DbType.String, libro.ISBNP, ParameterDirection.Input);
            parametros[7] = DBManager.Instance.CreateParam("_edicion", DbType.String    , libro.Edicion, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_LIBRO", parametros);
            libro.IdMaterial = Convert.ToInt32(parametros[0].Value);
            return libro.IdMaterial;
        }

        public BindingList<Libro> listarTodos()
        {
            BindingList<Libro> libros = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_ARTICULOS_TODOS", null);
            while (lector.Read())
            {
                if (libros == null) libros = new BindingList<Libro>();
                Libro libro = new Libro();
                if (!lector.IsDBNull(lector.GetOrdinal("id_libro"))) libro.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_articulo"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("titulo"))) libro.Titulo = lector.GetString(lector.GetOrdinal("titulo"));
                if (!lector.IsDBNull(lector.GetOrdinal("anho_publicacion"))) libro.Anho_publicacion = lector.GetInt32(lector.GetOrdinal("anho_publicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_paginas"))) libro.Numero_paginas = lector.GetInt32(lector.GetOrdinal("numero_paginas"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) libro.Estado = (EstadoMaterial)Enum.Parse(typeof(EstadoMaterial), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("clasificacion_tematica"))) libro.Clasificacion_tematica = lector.GetString(lector.GetOrdinal("clasificacion_tematica"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) libro.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("idioma"))) libro.Idioma = lector.GetString(lector.GetOrdinal("idioma"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) libro.Tipo = (TipoMaterial)Enum.Parse(typeof(TipoMaterial), lector.GetString(lector.GetOrdinal("tipo")));
                if (!lector.IsDBNull(lector.GetOrdinal("ISBN"))) libro.ISBNP = lector.GetString(lector.GetOrdinal("ISBN"));
                if (!lector.IsDBNull(lector.GetOrdinal("edicion"))) libro.Edicion = lector.GetString(lector.GetOrdinal("edicion"));
                libros.Add(libro);
            }
            DBManager.Instance.CerrarConexion();
            return libros;
        }

        public int modificar(Libro libro)
        {
            DbParameter[] parametros = new DbParameter[8];
            parametros[0] = DBManager.Instance.CreateParam("_id_libro", DbType.Int32, libro.IdMaterial, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_titulo", DbType.String, libro.Titulo, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_anho_publicacion", DbType.Int32, libro.Anho_publicacion, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_numero_paginas", DbType.Int32, libro.Numero_paginas, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_clasificacion_tematica", DbType.String, libro.Clasificacion_tematica, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_idioma", DbType.String, libro.Idioma, ParameterDirection.Input);
            parametros[6] = DBManager.Instance.CreateParam("_ISBN", DbType.String, libro.ISBNP, ParameterDirection.Input);
            parametros[7] = DBManager.Instance.CreateParam("_edicion", DbType.String, libro.Edicion, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_LIBRO", parametros);
        }

        public Libro obtenerPorId(int idObjeto)
        {
            Libro libro = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_libro", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_LIBRO_X_ID", parametros);
            if (lector.Read())
            {
                if (libro == null) libro = new Libro();
                if (!lector.IsDBNull(lector.GetOrdinal("id_libro"))) libro.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_articulo"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("titulo"))) libro.Titulo = lector.GetString(lector.GetOrdinal("titulo"));
                if (!lector.IsDBNull(lector.GetOrdinal("anho_publicacion"))) libro.Anho_publicacion = lector.GetInt32(lector.GetOrdinal("anho_publicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_paginas"))) libro.Numero_paginas = lector.GetInt32(lector.GetOrdinal("numero_paginas"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) libro.Estado = (EstadoMaterial)Enum.Parse(typeof(EstadoMaterial), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("clasificacion_tematica"))) libro.Clasificacion_tematica = lector.GetString(lector.GetOrdinal("clasificacion_tematica"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) libro.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("idioma"))) libro.Idioma = lector.GetString(lector.GetOrdinal("idioma"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) libro.Tipo = (TipoMaterial)Enum.Parse(typeof(TipoMaterial), lector.GetString(lector.GetOrdinal("tipo")));
                if (!lector.IsDBNull(lector.GetOrdinal("ISBN"))) libro.ISBNP = lector.GetString(lector.GetOrdinal("ISBN"));
                if (!lector.IsDBNull(lector.GetOrdinal("edicion"))) libro.Edicion = lector.GetString(lector.GetOrdinal("edicion"));
            }
            DBManager.Instance.CerrarConexion();
            return libro;
        }
    }
}
