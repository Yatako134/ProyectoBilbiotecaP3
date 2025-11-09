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
    public class PrestamoImpl : PrestamoDAO
    {
        private DbDataReader lector;
        public int eliminar(int idObjeto)
        {
            throw new NotImplementedException();
        }

        public int finalizar_prestamo(int idPrestamo)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_prestamo", DbType.Int32, null, ParameterDirection.Output);
            return DBManager.Instance.EjecutarProcedimiento("FINALIZAR_PRESTAMO", parametros);
        }

        public int insertar(Prestamo prestamo)
        {
            DbParameter[] parametros = new DbParameter[3];
            parametros[0] = DBManager.Instance.CreateParam("_id_prestamo", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_id_ejemplar", DbType.String, prestamo.Ejemplar.IdEjemplar, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_id_usuario", DbType.Int32, prestamo.Usuario.Id_usuario, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_PRESTAMO", parametros);
            prestamo.IdPrestamo = Convert.ToInt32(parametros[0].Value);
            return prestamo.IdPrestamo;
        }

        public BindingList<Prestamo> listarTodos()
        {
            BindingList<Prestamo> prestamos = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_PRESTAMOS_TODOS", null);
            while (lector.Read())
            {
                if (prestamos == null) prestamos = new BindingList<Prestamo>();
                Prestamo prestamo = new Prestamo();
                if (!lector.IsDBNull(lector.GetOrdinal("id_prestamo"))) prestamo.IdPrestamo = lector.GetInt32(lector.GetOrdinal("id_prestamo"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_de_prestamo"))) prestamo.Fecha_devolucion = lector.GetDateTime(lector.GetOrdinal("fecha_de_prestamo"));
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_vencimiento"))) prestamo.Fecha_vencimiento = lector.GetDateTime(lector.GetOrdinal("fecha_vencimiento"));
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_devolucion"))) prestamo.Fecha_devolucion = lector.GetDateTime(lector.GetOrdinal("fecha_devolucion"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) prestamo.Estado = (EstadoPrestamo)Enum.Parse(typeof(EstadoPrestamo), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("id_ejemplar"))) prestamo.Ejemplar.IdEjemplar = lector.GetInt32(lector.GetOrdinal("id_ejemplar"));
                if (!lector.IsDBNull(lector.GetOrdinal("id_usuario"))) prestamo.Usuario.Id_usuario = lector.GetInt32(lector.GetOrdinal("id_usuario"));
                prestamos.Add(prestamo);
            }
            DBManager.Instance.CerrarConexion();
            return prestamos;
        }

        public int modificar(Prestamo objeto)
        {
            throw new NotImplementedException();
        }

        public Prestamo obtenerPorId(int idObjeto)
        {
            Prestamo prestamo = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("id_prestamo", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_TESIS_X_ID", parametros);
            if (lector.Read())
            {
                if (prestamo == null) prestamo = new Prestamo();
                if (!lector.IsDBNull(lector.GetOrdinal("id_prestamo"))) prestamo.IdPrestamo = lector.GetInt32(lector.GetOrdinal("id_prestamo"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_de_prestamo"))) prestamo.Fecha_devolucion = lector.GetDateTime(lector.GetOrdinal("fecha_de_prestamo"));
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_vencimiento"))) prestamo.Fecha_vencimiento = lector.GetDateTime(lector.GetOrdinal("fecha_vencimiento"));
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_devolucion"))) prestamo.Fecha_devolucion = lector.GetDateTime(lector.GetOrdinal("fecha_devolucion"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) prestamo.Estado = (EstadoPrestamo)Enum.Parse(typeof(EstadoPrestamo), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("id_ejemplar"))) prestamo.Ejemplar.IdEjemplar = lector.GetInt32(lector.GetOrdinal("id_ejemplar"));
                if (!lector.IsDBNull(lector.GetOrdinal("id_usuario"))) prestamo.Usuario.Id_usuario = lector.GetInt32(lector.GetOrdinal("id_usuario"));
            }
            DBManager.Instance.CerrarConexion();
            return prestamo;
        }
    }
}
