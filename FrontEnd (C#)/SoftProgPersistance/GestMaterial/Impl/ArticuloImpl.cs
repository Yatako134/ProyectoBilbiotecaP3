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
    public class ArticuloImpl : ArticuloDAO
    {
        private DbDataReader lector;
        public int eliminar(int idObjeto)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_articulo", DbType.Int32, idObjeto, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("ELIMINAR_ARTICULO", parametros);
        }

        public int insertar(Articulo articulo)
        {
            DbParameter[] parametros = new DbParameter[10];
            parametros[0] = DBManager.Instance.CreateParam("_id_articulo", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_titulo", DbType.String, articulo.Titulo, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_anho_publicacion", DbType.Int32, articulo.Anho_publicacion, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_numero_paginas", DbType.Int32, articulo.Numero_paginas, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_clasificacion_tematica", DbType.String, articulo.Clasificacion_tematica, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_idioma", DbType.String, articulo.Idioma, ParameterDirection.Input);
            parametros[6] = DBManager.Instance.CreateParam("_ISSN", DbType.String, articulo.ISSNP, ParameterDirection.Input);
            parametros[7] = DBManager.Instance.CreateParam("_revista", DbType.String, articulo.Revista, ParameterDirection.Input);
            parametros[8] = DBManager.Instance.CreateParam("_volumen", DbType.Int32, articulo.Volumen, ParameterDirection.Input);
            parametros[9] = DBManager.Instance.CreateParam("_numero", DbType.Int32, articulo.Numero, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_ARTICULO", parametros);
            articulo.IdMaterial = Convert.ToInt32(parametros[0].Value);
            return articulo.IdMaterial;
        }

        public BindingList<Articulo> listarTodos()
        {
            BindingList<Articulo> articulos = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_ARTICULOS_TODOS", null);
            while (lector.Read())
            {
                if (articulos == null) articulos = new BindingList<Articulo>();
                Articulo articulo = new Articulo();
                if (!lector.IsDBNull(lector.GetOrdinal("id_articulo"))) articulo.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_articulo"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("titulo"))) articulo.Titulo = lector.GetString(lector.GetOrdinal("titulo"));
                if (!lector.IsDBNull(lector.GetOrdinal("anho_publicacion"))) articulo.Anho_publicacion = lector.GetInt32(lector.GetOrdinal("anho_publicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_paginas"))) articulo.Numero_paginas = lector.GetInt32(lector.GetOrdinal("numero_paginas"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) articulo.Estado = (EstadoMaterial)Enum.Parse(typeof(EstadoMaterial),lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("clasificacion_tematica"))) articulo.Clasificacion_tematica = lector.GetString(lector.GetOrdinal("clasificacion_tematica"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) articulo.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("idioma"))) articulo.Idioma = lector.GetString(lector.GetOrdinal("idioma"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) articulo.Tipo = (TipoMaterial)Enum.Parse(typeof(TipoMaterial),lector.GetString(lector.GetOrdinal("tipo"))); 
                if (!lector.IsDBNull(lector.GetOrdinal("ISSN"))) articulo.ISSNP = lector.GetString(lector.GetOrdinal("ISSN"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero"))) articulo.Numero = lector.GetInt32(lector.GetOrdinal("numero"));
                if (!lector.IsDBNull(lector.GetOrdinal("revista"))) articulo.Revista = lector.GetString(lector.GetOrdinal("revista"));
                if (!lector.IsDBNull(lector.GetOrdinal("volumen"))) articulo.Volumen = lector.GetInt32(lector.GetOrdinal("volumen"));
                articulos.Add(articulo);
            }
            DBManager.Instance.CerrarConexion();
            return articulos;
        }

        public int modificar(Articulo articulo)
        {
            DbParameter[] parametros = new DbParameter[10];
            parametros[0] = DBManager.Instance.CreateParam("_id_articulo", DbType.Int32, articulo.IdMaterial, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_titulo", DbType.String, articulo.Titulo, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_anho_publicacion", DbType.Int32, articulo.Anho_publicacion, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_numero_paginas", DbType.Int32, articulo.Numero_paginas, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_clasificacion_tematica", DbType.String, articulo.Clasificacion_tematica, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_idioma", DbType.String, articulo.Idioma, ParameterDirection.Input);
            parametros[6] = DBManager.Instance.CreateParam("_ISSN", DbType.String, articulo.ISSNP, ParameterDirection.Input);
            parametros[7] = DBManager.Instance.CreateParam("_revista", DbType.String, articulo.Revista, ParameterDirection.Input);
            parametros[8] = DBManager.Instance.CreateParam("_volumen", DbType.Int32, articulo.Volumen, ParameterDirection.Input);
            parametros[9] = DBManager.Instance.CreateParam("_numero", DbType.Int32, articulo.Numero, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_ARTICULO", parametros);
        }

        public Articulo obtenerPorId(int idObjeto)
        {
            Articulo articulo = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("id_articulo", DbType.Int32,idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_ARTICULO_X_ID", parametros);
            if (lector.Read())
            {
                if (articulo == null) articulo = new Articulo();
                if (!lector.IsDBNull(lector.GetOrdinal("id_articulo"))) articulo.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_articulo"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("titulo"))) articulo.Titulo = lector.GetString(lector.GetOrdinal("titulo"));
                if (!lector.IsDBNull(lector.GetOrdinal("anho_publicacion"))) articulo.Anho_publicacion = lector.GetInt32(lector.GetOrdinal("anho_publicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_paginas"))) articulo.Numero_paginas = lector.GetInt32(lector.GetOrdinal("numero_paginas"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) articulo.Estado = (EstadoMaterial)Enum.Parse(typeof(EstadoMaterial), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("clasificacion_tematica"))) articulo.Clasificacion_tematica = lector.GetString(lector.GetOrdinal("clasificacion_tematica"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) articulo.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("idioma"))) articulo.Idioma = lector.GetString(lector.GetOrdinal("idioma"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) articulo.Tipo = (TipoMaterial)Enum.Parse(typeof(TipoMaterial), lector.GetString(lector.GetOrdinal("tipo")));
                if (!lector.IsDBNull(lector.GetOrdinal("ISSN"))) articulo.ISSNP = lector.GetString(lector.GetOrdinal("ISSN"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero"))) articulo.Numero = lector.GetInt32(lector.GetOrdinal("numero"));
                if (!lector.IsDBNull(lector.GetOrdinal("revista"))) articulo.Revista = lector.GetString(lector.GetOrdinal("revista"));
                if (!lector.IsDBNull(lector.GetOrdinal("volumen"))) articulo.Volumen = lector.GetInt32(lector.GetOrdinal("volumen"));
            }
            DBManager.Instance.CerrarConexion();
            return articulo;
        }
    }
}
