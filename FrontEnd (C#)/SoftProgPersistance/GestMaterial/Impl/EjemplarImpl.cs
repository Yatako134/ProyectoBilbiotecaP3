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
    public class EjemplarImpl : EjemplarDAO
    {
        private DbDataReader lector;
        public int eliminar(int idObjeto)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_ejemplar", DbType.Int32, idObjeto, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("ELIMINAR_EJEMPLAR", parametros);
        }

        public int insertar(Ejemplar ejemplar)
        {
            DbParameter[] parametros = new DbParameter[4];
            parametros[0] = DBManager.Instance.CreateParam("_id_ejemplar", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_id_material", DbType.Int32, ejemplar.Material.IdMaterial, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_ubicacion", DbType.String, ejemplar.Ubicacion, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_id_biblioteca", DbType.Int32, ejemplar.Biblioteca.IdBiblioteca, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_EJEMPLAR", parametros);
            ejemplar.IdEjemplar = Convert.ToInt32(parametros[0].Value);
            return ejemplar.IdEjemplar;
        }

        public BindingList<Ejemplar> listarTodos()
        {
            BindingList<Ejemplar> ejemplares = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_EJEMPLARES_TODOS", null);
            while (lector.Read())
            {
                if (ejemplares == null) ejemplares = new BindingList<Ejemplar>();
                Ejemplar ejemplar = new Ejemplar();
                if (!lector.IsDBNull(lector.GetOrdinal("id_ejemplar"))) ejemplar.IdEjemplar = lector.GetInt32(lector.GetOrdinal("id_ejemplar"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("id_material"))) ejemplar.Material.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_material"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) ejemplar.Estado = (EstadoEjemplar)Enum.Parse(typeof(EstadoEjemplar), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("ubicacion"))) ejemplar.Ubicacion = lector.GetString(lector.GetOrdinal("ubicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) ejemplar.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("id_biblioteca"))) ejemplar.Biblioteca.IdBiblioteca = lector.GetInt32(lector.GetOrdinal("id_biblioteca"));
                ejemplares.Add(ejemplar);
            }
            DBManager.Instance.CerrarConexion();
            return ejemplares;
        }

        public int modificar(Ejemplar ejemplar)
        {
            DbParameter[] parametros = new DbParameter[4];
            parametros[0] = DBManager.Instance.CreateParam("_id_ejemplar", DbType.Int32, ejemplar.IdEjemplar, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_id_material", DbType.Int32, ejemplar.Material.IdMaterial, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_ubicacion", DbType.String, ejemplar.Ubicacion, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_id_biblioteca", DbType.Int32, ejemplar.Biblioteca.IdBiblioteca, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_EJEMPLAR", parametros);
        }

        public Ejemplar obtenerPorId(int idObjeto)
        {
            Ejemplar ejemplar = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_ejemplar", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_EJEMPLAR_X_ID", parametros);
            if (lector.Read())
            {
                if (ejemplar == null) ejemplar = new Ejemplar();
                if (!lector.IsDBNull(lector.GetOrdinal("id_ejemplar"))) ejemplar.IdEjemplar = lector.GetInt32(lector.GetOrdinal("id_ejemplar"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("id_material"))) ejemplar.Material.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_material"));
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) ejemplar.Estado = (EstadoEjemplar)Enum.Parse(typeof(EstadoEjemplar), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("ubicacion"))) ejemplar.Ubicacion = lector.GetString(lector.GetOrdinal("ubicacion"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) ejemplar.Activo = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("id_biblioteca"))) ejemplar.Biblioteca.IdBiblioteca = lector.GetInt32(lector.GetOrdinal("id_biblioteca"));
            }
            DBManager.Instance.CerrarConexion();
            return ejemplar;
        }

        public BindingList<Ejemplar> listar_disponibles_por_material(int _id_material)
        {
            BindingList<Ejemplar> ejemplares = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_material", DbType.Int32, _id_material, ParameterDirection.Input);

            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_EJEMPLARES_DISPONIBLES_POR_MATERIAL", parametros);
            while (lector.Read())
            {
                if (ejemplares == null) ejemplares = new BindingList<Ejemplar>();
                Ejemplar ejemplar = new Ejemplar();
                if (!lector.IsDBNull(lector.GetOrdinal("id_ejemplar"))) ejemplar.IdEjemplar = lector.GetInt32(lector.GetOrdinal("id_ejemplar"));// Se coloca el identificador del select
               MaterialBibliografico material = new MaterialBibliografico();
                if (!lector.IsDBNull(lector.GetOrdinal("id_material"))) material.IdMaterial = lector.GetInt32(lector.GetOrdinal("id_material"));
                ejemplar.Material = material;
                if (!lector.IsDBNull(lector.GetOrdinal("estado"))) ejemplar.Estado = (EstadoEjemplar)Enum.Parse(typeof(EstadoEjemplar), lector.GetString(lector.GetOrdinal("estado")));
                if (!lector.IsDBNull(lector.GetOrdinal("ubicacion"))) ejemplar.Ubicacion = lector.GetString(lector.GetOrdinal("ubicacion"));
                Biblioteca biblioteca = new Biblioteca();
                if (!lector.IsDBNull(lector.GetOrdinal("id_biblioteca"))) biblioteca.IdBiblioteca = lector.GetInt32(lector.GetOrdinal("id_biblioteca"));
                ejemplar.Biblioteca = biblioteca;
                ejemplares.Add(ejemplar);
            }
            DBManager.Instance.CerrarConexion();



            return ejemplares;
        }
    }
}
