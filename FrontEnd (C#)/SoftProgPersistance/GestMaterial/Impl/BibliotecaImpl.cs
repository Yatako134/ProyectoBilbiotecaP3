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
    public class BibliotecaImpl : BibliotecaDAO
    {
        private DbDataReader lector;
        public int eliminar(int idObjeto)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_biblioteca", DbType.Int32, idObjeto, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("ELIMINAR_BIBLIOTECA", parametros);
        }

        public int insertar(Biblioteca biblioteca)
        {
            DbParameter[] parametros = new DbParameter[3];
            parametros[0] = DBManager.Instance.CreateParam("_id_biblioteca", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_nombre", DbType.String, biblioteca.Nombre, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_ubicacion", DbType.String, biblioteca.Ubicacion, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_BIBLIOTECA", parametros);
            biblioteca.IdBiblioteca = Convert.ToInt32(parametros[0].Value);
            return biblioteca.IdBiblioteca;
        }

        public BindingList<Biblioteca> listarTodos()
        {
            BindingList<Biblioteca> bibliotecas = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_BIBLIOTECAS_TODAS", null);
            while (lector.Read())
            {
                if (bibliotecas == null) bibliotecas = new BindingList<Biblioteca>();
                Biblioteca biblioteca = new Biblioteca();
                if (!lector.IsDBNull(lector.GetOrdinal("id_biblioteca"))) biblioteca.IdBiblioteca = lector.GetInt32(lector.GetOrdinal("id_biblioteca"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) biblioteca.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                if (!lector.IsDBNull(lector.GetOrdinal("ubicacion"))) biblioteca.Ubicacion = lector.GetString(lector.GetOrdinal("ubicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) biblioteca.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                bibliotecas.Add(biblioteca);
            }
            DBManager.Instance.CerrarConexion();
            return bibliotecas;
        }

        public int modificar(Biblioteca biblioteca)
        {
            DbParameter[] parametros = new DbParameter[3];
            parametros[0] = DBManager.Instance.CreateParam("_id_biblioteca", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_nombre", DbType.String, biblioteca.Nombre, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_ubicacion", DbType.String, biblioteca.Ubicacion, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_BIBLIOTECA", parametros);
        }

        public Biblioteca obtenerPorId(int idObjeto)
        {
            Biblioteca biblioteca = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("id_biblioteca", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_BIBLIOTECA_X_ID", parametros);
            if (lector.Read())
            {
                if (biblioteca == null) biblioteca = new Biblioteca();
                if (!lector.IsDBNull(lector.GetOrdinal("id_biblioteca"))) biblioteca.IdBiblioteca = lector.GetInt32(lector.GetOrdinal("id_biblioteca"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) biblioteca.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                if (!lector.IsDBNull(lector.GetOrdinal("ubicacion"))) biblioteca.Ubicacion = lector.GetString(lector.GetOrdinal("ubicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) biblioteca.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
            }
            DBManager.Instance.CerrarConexion();
            return biblioteca;
        }

        public BindingList<Biblioteca> listar_bibliotecas_por_material(int _id_material)
        {
            BindingList<Biblioteca> bibliotecas = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_material", DbType.Int32, _id_material, ParameterDirection.Input);

            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_BIBLIOTECAS_DE_MATERIAL", parametros);
            while (lector.Read())
            {
                if (bibliotecas == null) bibliotecas = new BindingList<Biblioteca>();
                Biblioteca biblioteca = new Biblioteca();
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) biblioteca.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                bibliotecas.Add(biblioteca);
            }
            DBManager.Instance.CerrarConexion();



            return bibliotecas;
        }
    }
}
