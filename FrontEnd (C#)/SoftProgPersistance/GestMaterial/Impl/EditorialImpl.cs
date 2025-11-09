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
    public class EditorialImpl : EditorialDAO
    {
        private DbDataReader lector;
        public int asignar_editorial(int id_material, int id_editorial)
        {
            DbParameter[] parametros = new DbParameter[2];
            parametros[0] = DBManager.Instance.CreateParam("id_material", DbType.Int32, id_material, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("id_editorial", DbType.Int32, id_editorial, ParameterDirection.Input);
            int resultado = DBManager.Instance.EjecutarProcedimiento("ASIGNAR_EDITORIAL", parametros);
            return resultado;
        }

        public int eliminar(int idObjeto)
        {
            throw new NotImplementedException();
        }

        public int insertar(Editorial editorial)
        {
            DbParameter[] parametros = new DbParameter[2];
            parametros[0] = DBManager.Instance.CreateParam("_id_editorial", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_nombre", DbType.String, editorial.Nombre, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_EDITORIAL", parametros);
            editorial.IdEditorial = Convert.ToInt32(parametros[0].Value);
            return editorial.IdEditorial;
        }

        public BindingList<Editorial> listarTodos()
        {
            BindingList<Editorial> editoriales = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_EDITORIALES_TODAS", null);
            while (lector.Read())
            {
                if (editoriales == null) editoriales = new BindingList<Editorial>();
                Editorial editorial = new Editorial();
                if (!lector.IsDBNull(lector.GetOrdinal("id_editorial"))) editorial.IdEditorial = lector.GetInt32(lector.GetOrdinal("id_editorial"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) editorial.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                editoriales.Add(editorial);
            }
            DBManager.Instance.CerrarConexion();
            return editoriales;
        }

        public int modificar(Editorial editorial)
        {
            DbParameter[] parametros = new DbParameter[2];
            parametros[0] = DBManager.Instance.CreateParam("_id_editorial", DbType.Int32, editorial.IdEditorial, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_nombre", DbType.String, editorial.Nombre, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_EDITORIAL", parametros);
        }

        public Editorial obtenerPorId(int idObjeto)
        {
            Editorial editorial = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_editorial", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_EDITORIAL_X_ID", parametros);
            if (lector.Read())
            {
                if (editorial == null) editorial = new Editorial();
                if (!lector.IsDBNull(lector.GetOrdinal("id_editorial"))) editorial.IdEditorial= lector.GetInt32(lector.GetOrdinal("id_editorial"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) editorial.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
            }
            DBManager.Instance.CerrarConexion();
            return editorial;
        }
    }
}
