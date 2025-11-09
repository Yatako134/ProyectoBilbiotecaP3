using SoftProgDBManager;
using SoftProgModel.GestMaterial;
using SoftProgModel.GestPrestamos;
using SoftProgPersistance.GestPrestamos.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestPrestamos.Impl
{
    public class SancionImpl : SancionDAO
    {
        private DbDataReader lector;

        public int eliminar(int idObjeto)
        {
            throw new NotImplementedException();
        }

        public int finalizar_sancion(int id_sancion)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_sancion", DbType.Int32, id_sancion, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("FINALIZAR_SANCION", parametros);
        }

        public int insertar(Sancion sancion)
        {
            DbParameter[] parametros = new DbParameter[5];
            parametros[0] = DBManager.Instance.CreateParam("_id_sancion", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_tipo_sancion", DbType.String, sancion.Tipo_sancion, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_duracion_dias", DbType.Int32, sancion.Duracion_dias, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_justificacion", DbType.Int32, sancion.Justificacion, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_id_prestamo", DbType.Int32, sancion.Prestamo.IdPrestamo, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_SANCION", parametros);
            sancion.Id_sancion = Convert.ToInt32(parametros[0].Value);
            return sancion.Id_sancion;
        }

        public BindingList<Sancion> listarTodos()
        {
            BindingList<Sancion> sanciones = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_SANCIONES_TODAS", null);
            while (lector.Read())
            {
                if (sanciones == null) sanciones = new BindingList<Sancion>();
                Sancion sancion = new Sancion();
                if (!lector.IsDBNull(lector.GetOrdinal("id_sancion"))) sancion.Id_sancion = lector.GetInt32(lector.GetOrdinal("id_sancion"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("tipo_sancion"))) sancion.Tipo_sancion = (Tipo_sancion)Enum.Parse(typeof(Tipo_sancion), lector.GetString(lector.GetOrdinal("tipo_sancion")));
                if (!lector.IsDBNull(lector.GetOrdinal("duracion_dias"))) sancion.Duracion_dias = lector.GetInt32(lector.GetOrdinal("duracion_dias"));
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_inicio"))) sancion.Fecha_inicio = lector.GetDateTime(lector.GetOrdinal("fecha_inicio"));
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_fin"))) sancion.Fecha_fin = lector.GetDateTime(lector.GetOrdinal("fecha_fin"));
                if (!lector.IsDBNull(lector.GetOrdinal("justificacion"))) sancion.Justificacion = lector.GetString(lector.GetOrdinal("justificacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) sancion.Estado = (EstadoSancion)Enum.Parse(typeof(EstadoSancion), lector.GetString(lector.GetOrdinal("estado")));
       //         if (!lector.IsDBNull(lector.GetOrdinal("id_prestamo"))) sancion.Prestamo.IdPrestamo = lector.GetInt32(lector.GetOrdinal("id_prestamo"));
                sanciones.Add(sancion);
            }
            DBManager.Instance.CerrarConexion();
            return sanciones;
        }

        public int modificar(Sancion sancion)
        {
            DbParameter[] parametros = new DbParameter[5];
            parametros[0] = DBManager.Instance.CreateParam("_id_sancion", DbType.Int32, sancion.Id_sancion, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_tipo_sancion", DbType.String, sancion.Tipo_sancion, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_duracion_dias", DbType.Int32, sancion.Duracion_dias, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_justificacion", DbType.Int32, sancion.Justificacion, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_id_prestamo", DbType.Int32, sancion.Prestamo.IdPrestamo, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_SANCION", parametros);
        }

        public Sancion obtenerPorId(int idObjeto)
        {
            Sancion sancion = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_sancion", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_SANCION_X_ID", parametros);
            if (lector.Read())
            {
                if (sancion == null) sancion = new Sancion();
                if (!lector.IsDBNull(lector.GetOrdinal("id_sancion"))) sancion.Id_sancion = lector.GetInt32(lector.GetOrdinal("id_sancion"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("tipo_sancion"))) sancion.Tipo_sancion = (Tipo_sancion)Enum.Parse(typeof(Tipo_sancion), lector.GetString(lector.GetOrdinal("tipo_sancion")));
                if (!lector.IsDBNull(lector.GetOrdinal("duracion_dias"))) sancion.Duracion_dias = lector.GetInt32(lector.GetOrdinal("duracion_dias"));
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_inicio"))) sancion.Fecha_inicio = lector.GetDateTime(lector.GetOrdinal("fecha_inicio"));
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_fin"))) sancion.Fecha_fin = lector.GetDateTime(lector.GetOrdinal("fecha_fin"));
                if (!lector.IsDBNull(lector.GetOrdinal("justificacion"))) sancion.Justificacion = lector.GetString(lector.GetOrdinal("justificacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) sancion.Estado = (EstadoSancion)Enum.Parse(typeof(EstadoSancion), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("id_prestamo"))) sancion.Prestamo.IdPrestamo = lector.GetInt32(lector.GetOrdinal("id_prestamo"));
            }
            DBManager.Instance.CerrarConexion();
            return sancion;
        }
    }
}
