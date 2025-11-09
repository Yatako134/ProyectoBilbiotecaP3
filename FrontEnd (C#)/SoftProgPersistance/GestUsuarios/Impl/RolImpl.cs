using SoftProgDBManager;
using SoftProgModel.GestMaterial;
using SoftProgModel.GestUsuarios;
using SoftProgPersistance.GestUsuarios.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestUsuarios.Impl
{
    public class RolImpl : RolDAO
    {
        private DbDataReader lector;
        public int eliminar(int idObjeto)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_rol", DbType.Int32, idObjeto, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("ELIMINAR_ROL", parametros);
        }

        public int insertar(Rol rol)
        {
            DbParameter[] parametros = new DbParameter[3];
            parametros[0] = DBManager.Instance.CreateParam("_id_rol", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_tipo", DbType.String, rol.Tipo, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_cantidad_de_dias_por_prestamo", DbType.Int32, rol.Cantidad_de_dias_por_prestamo, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_ROL", parametros);
            rol.Id_rol = Convert.ToInt32(parametros[0].Value);
            return rol.Id_rol;
        }

        public BindingList<Rol> listarTodos()
        {
            BindingList<Rol> roles = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_ROLES_TODOS", null);
            while (lector.Read())
            {
                if (roles == null) roles = new BindingList<Rol>();
                Rol rol = new Rol();
                if (!lector.IsDBNull(lector.GetOrdinal("id_rol"))) rol.Id_rol = lector.GetInt32(lector.GetOrdinal("id_rol"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) rol.Tipo = lector.GetString(lector.GetOrdinal("tipo"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) rol.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("cantidad_de_dias_por_prestamo"))) rol.Cantidad_de_dias_por_prestamo = lector.GetInt32(lector.GetOrdinal("cantidad_de_dias_por_prestamo"));
                roles.Add(rol);
            }
            DBManager.Instance.CerrarConexion();
            return roles;
        }

        public int modificar(Rol rol)
        {
            DbParameter[] parametros = new DbParameter[3];
            parametros[0] = DBManager.Instance.CreateParam("_id_rol", DbType.Int32, rol.Tipo, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_tipo", DbType.String, rol.Tipo, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_cantidad_de_dias_por_prestamo", DbType.Int32, rol.Cantidad_de_dias_por_prestamo, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_ROL", parametros);
        }

        public Rol obtenerPorId(int idObjeto)
        {
            Rol rol = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_rol", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_ROL_X_ID", parametros);
            if (lector.Read())
            {
                if (rol == null) rol = new Rol();
                if (!lector.IsDBNull(lector.GetOrdinal("id_rol"))) rol.Id_rol = lector.GetInt32(lector.GetOrdinal("id_rol"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("tipo"))) rol.Tipo = lector.GetString(lector.GetOrdinal("tipo"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) rol.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("cantidad_de_dias_por_prestamo"))) rol.Cantidad_de_dias_por_prestamo = lector.GetInt32(lector.GetOrdinal("cantidad_de_dias_por_prestamo"));
            }
            DBManager.Instance.CerrarConexion();
            return rol;
        }
    }
}
