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
    public class TesisImpl : TesisDAO
    {
        private DbDataReader lector;
        public int eliminar(int idObjeto)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_tesis", DbType.Int32, idObjeto, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("ELIMINAR_TESIS", parametros);
        }

        public int insertar(Tesis tesis)
        {
            DbParameter[] parametros = new DbParameter[10];
            parametros[0] = DBManager.Instance.CreateParam("_id_tesis", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_titulo", DbType.String, tesis.Titulo, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_anho_publicacion", DbType.Int32, tesis.Anho_publicacion, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_numero_paginas", DbType.Int32, tesis.Numero_paginas, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_clasificacion_tematica", DbType.String, tesis.Clasificacion_tematica, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_idioma", DbType.String, tesis.Idioma, ParameterDirection.Input);
            parametros[6] = DBManager.Instance.CreateParam("_especialidad", DbType.String, tesis.Especialidad, ParameterDirection.Input);
            parametros[7] = DBManager.Instance.CreateParam("_asesor", DbType.String, tesis.Asesor, ParameterDirection.Input);
            parametros[8] = DBManager.Instance.CreateParam("_grado", DbType.String, tesis.Grado, ParameterDirection.Input);
            parametros[9] = DBManager.Instance.CreateParam("_institucion_publicacion", DbType.String, tesis.InstitucionPublicacion, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_TESIS", parametros);
            tesis.IdMaterial = Convert.ToInt32(parametros[0].Value);
            return tesis.IdMaterial;
        }

        public BindingList<Tesis> listarTodos()
        {
            BindingList<Tesis> tesis_s = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_TESIS_TODOS", null);
            while (lector.Read())
            {
                if (tesis_s == null) tesis_s = new BindingList<Tesis>();
                Tesis tesis = new Tesis();
                if (!lector.IsDBNull(lector.GetOrdinal("id_tesis"))) tesis.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_articulo"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("titulo"))) tesis.Titulo = lector.GetString(lector.GetOrdinal("titulo"));
                if (!lector.IsDBNull(lector.GetOrdinal("anho_publicacion"))) tesis.Anho_publicacion = lector.GetInt32(lector.GetOrdinal("anho_publicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_paginas"))) tesis.Numero_paginas = lector.GetInt32(lector.GetOrdinal("numero_paginas"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) tesis.Estado = (EstadoMaterial)Enum.Parse(typeof(EstadoMaterial), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("clasificacion_tematica"))) tesis.Clasificacion_tematica = lector.GetString(lector.GetOrdinal("clasificacion_tematica"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) tesis.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("idioma"))) tesis.Idioma = lector.GetString(lector.GetOrdinal("idioma"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) tesis.Tipo = (TipoMaterial)Enum.Parse(typeof(TipoMaterial), lector.GetString(lector.GetOrdinal("tipo")));
                if (!lector.IsDBNull(lector.GetOrdinal("especialidad"))) tesis.Especialidad = lector.GetString(lector.GetOrdinal("especialidad"));
                if (!lector.IsDBNull(lector.GetOrdinal("asesor"))) tesis.Asesor = lector.GetString(lector.GetOrdinal("asesor"));
                if (!lector.IsDBNull(lector.GetOrdinal("grado"))) tesis.Grado = lector.GetString(lector.GetOrdinal("grado"));
                if (!lector.IsDBNull(lector.GetOrdinal("institucion_publicacion"))) tesis.InstitucionPublicacion = lector.GetString(lector.GetOrdinal("institucion_publicacion"));
                tesis_s.Add(tesis);
            }
            DBManager.Instance.CerrarConexion();
            return tesis_s;
        }

        public int modificar(Tesis tesis)
        {
            DbParameter[] parametros = new DbParameter[10];
            parametros[0] = DBManager.Instance.CreateParam("_id_tesis", DbType.Int32, tesis.IdMaterial, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_titulo", DbType.String, tesis.Titulo, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_anho_publicacion", DbType.Int32, tesis.Anho_publicacion, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_numero_paginas", DbType.Int32, tesis.Numero_paginas, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_clasificacion_tematica", DbType.String, tesis.Clasificacion_tematica, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_idioma", DbType.String, tesis.Idioma, ParameterDirection.Input);
            parametros[6] = DBManager.Instance.CreateParam("_especialidad", DbType.String, tesis.Especialidad, ParameterDirection.Input);
            parametros[7] = DBManager.Instance.CreateParam("_asesor", DbType.String, tesis.Asesor, ParameterDirection.Input);
            parametros[8] = DBManager.Instance.CreateParam("_grado", DbType.String, tesis.Grado, ParameterDirection.Input);
            parametros[9] = DBManager.Instance.CreateParam("_institucion_publicacion", DbType.String, tesis.InstitucionPublicacion, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_TESIS", parametros);
        }

        public Tesis obtenerPorId(int idObjeto)
        {
            Tesis tesis = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_tesis", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_TESIS_X_ID", parametros);
            if (lector.Read())
            {
                if (tesis == null) tesis = new Tesis();
                if (!lector.IsDBNull(lector.GetOrdinal("id_tesis"))) tesis.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_articulo"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("titulo"))) tesis.Titulo = lector.GetString(lector.GetOrdinal("titulo"));
                if (!lector.IsDBNull(lector.GetOrdinal("anho_publicacion"))) tesis.Anho_publicacion = lector.GetInt32(lector.GetOrdinal("anho_publicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_paginas"))) tesis.Numero_paginas = lector.GetInt32(lector.GetOrdinal("numero_paginas"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) tesis.Estado = (EstadoMaterial)Enum.Parse(typeof(EstadoMaterial), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("clasificacion_tematica"))) tesis.Clasificacion_tematica = lector.GetString(lector.GetOrdinal("clasificacion_tematica"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) tesis.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("idioma"))) tesis.Idioma = lector.GetString(lector.GetOrdinal("idioma"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) tesis.Tipo = (TipoMaterial)Enum.Parse(typeof(TipoMaterial), lector.GetString(lector.GetOrdinal("tipo")));
                if (!lector.IsDBNull(lector.GetOrdinal("especialidad"))) tesis.Especialidad = lector.GetString(lector.GetOrdinal("especialidad"));
                if (!lector.IsDBNull(lector.GetOrdinal("asesor"))) tesis.Asesor = lector.GetString(lector.GetOrdinal("asesor"));
                if (!lector.IsDBNull(lector.GetOrdinal("grado"))) tesis.Grado = lector.GetString(lector.GetOrdinal("grado"));
                if (!lector.IsDBNull(lector.GetOrdinal("institucion_publicacion"))) tesis.InstitucionPublicacion = lector.GetString(lector.GetOrdinal("institucion_publicacion"));
            }
            DBManager.Instance.CerrarConexion();
            return tesis;
        }
    }
}
