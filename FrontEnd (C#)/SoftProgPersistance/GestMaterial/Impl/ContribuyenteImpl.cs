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
    public class ContribuyenteImpl : ContribuyenteDAO
    {
        private DbDataReader lector;

        public int asignar_contribuyente(int id_material, int id_contribuyente)
        {
            DbParameter[] parametros = new DbParameter[2];
            parametros[0] = DBManager.Instance.CreateParam("_id_material", DbType.Int32, id_material, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_id_contribuyente", DbType.Int32, id_contribuyente, ParameterDirection.Input);
            int resultado =DBManager.Instance.EjecutarProcedimiento("ASIGNAR_CONTRIBUYENTE", parametros);
            return resultado;
        }

        public int eliminar(int idObjeto)
        {
            throw new NotImplementedException();
        }

        public int insertar(Contribuyente contribuyente)
        {
            DbParameter[] parametros = new DbParameter[6];
            parametros[0] = DBManager.Instance.CreateParam("_id_contribuyente", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_nombre", DbType.String, contribuyente.Nombre, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_primer_apellido", DbType.String, contribuyente.Primer_apellido, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_segundo_apellido", DbType.String, contribuyente.Segundo_apellido, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_seudonimo", DbType.String, contribuyente.Seudonimo, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_tipo_contribuyente", DbType.String, contribuyente.Tipo_contribuyente, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_CONTRIBUYENTE", parametros);
            contribuyente.IdContribuyente = Convert.ToInt32(parametros[0].Value);
            return contribuyente.IdContribuyente;
        }

        public BindingList<Contribuyente> listarTodos()
        {
            BindingList<Contribuyente> contribuyentes = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_CONTRIBUYENTES_TODOS", null);
            while (lector.Read())
            {
                if (contribuyentes == null) contribuyentes = new BindingList<Contribuyente>();
                Contribuyente contribuyente = new Contribuyente();
                if (!lector.IsDBNull(lector.GetOrdinal("id_contribuyente"))) contribuyente.IdContribuyente = lector.GetInt32(lector.GetOrdinal("id_contribuyente"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) contribuyente.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                if (!lector.IsDBNull(lector.GetOrdinal("primer_apellido"))) contribuyente.Primer_apellido = lector.GetString(lector.GetOrdinal("primer_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("segundo_apellido"))) contribuyente.Segundo_apellido = lector.GetString(lector.GetOrdinal("segundo_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("seudonimo"))) contribuyente.Seudonimo = lector.GetString(lector.GetOrdinal("seudonimo"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo_contribuyente"))) contribuyente.Tipo_contribuyente = (TipoContribuyente)Enum.Parse(typeof(TipoContribuyente), lector.GetString(lector.GetOrdinal("tipo_contribuyente")));
                contribuyentes.Add(contribuyente);
            }
            DBManager.Instance.CerrarConexion();
            return contribuyentes;
        }

        public int modificar(Contribuyente contribuyente)
        {
            DbParameter[] parametros = new DbParameter[6];
            parametros[0] = DBManager.Instance.CreateParam("_id_contribuyente", DbType.Int32, contribuyente.IdContribuyente, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_nombre", DbType.String, contribuyente.Nombre, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_primer_apellido", DbType.String, contribuyente.Primer_apellido, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_segundo_apellido", DbType.String, contribuyente.Segundo_apellido, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_seudonimo", DbType.String, contribuyente.Seudonimo, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_tipo_contribuyente", DbType.String, contribuyente.Tipo_contribuyente, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_CONTRIBUYENTE", parametros);
        }

        public Contribuyente obtenerPorId(int idObjeto)
        {
            Contribuyente contribuyente = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("id_contribuyente", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_CONTRIBUYENTE_X_ID", parametros);
            if (lector.Read())
            {
                if (contribuyente == null) contribuyente = new Contribuyente();
                if (!lector.IsDBNull(lector.GetOrdinal("id_contribuyente"))) contribuyente.IdContribuyente = lector.GetInt32(lector.GetOrdinal("id_contribuyente"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) contribuyente.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                if (!lector.IsDBNull(lector.GetOrdinal("primer_apellido"))) contribuyente.Primer_apellido = lector.GetString(lector.GetOrdinal("primer_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("segundo_apellido"))) contribuyente.Segundo_apellido = lector.GetString(lector.GetOrdinal("segundo_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("seudonimo"))) contribuyente.Seudonimo = lector.GetString(lector.GetOrdinal("seudonimo"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo_contribuyente"))) contribuyente.Tipo_contribuyente = (TipoContribuyente)Enum.Parse(typeof(TipoContribuyente), lector.GetString(lector.GetOrdinal("tipo_contribuyente")));
            }
            DBManager.Instance.CerrarConexion();
            return contribuyente;
        }

        public BindingList<Contribuyente> listar_autores_por_material(int id_material)
        {
            BindingList<Contribuyente> contribuyentes = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_material", DbType.Int32, id_material, ParameterDirection.Input);

            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_AUTORES_POR_MATERIAL", parametros);
            while (lector.Read())
            {
                if (contribuyentes == null) contribuyentes = new BindingList<Contribuyente>();
                Contribuyente contribuyente = new Contribuyente();
                if (!lector.IsDBNull(lector.GetOrdinal("id_contribuyente"))) contribuyente.IdContribuyente = lector.GetInt32(lector.GetOrdinal("id_contribuyente"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) contribuyente.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                if (!lector.IsDBNull(lector.GetOrdinal("primer_apellido"))) contribuyente.Primer_apellido = lector.GetString(lector.GetOrdinal("primer_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("segundo_apellido"))) contribuyente.Segundo_apellido = lector.GetString(lector.GetOrdinal("segundo_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("seudonimo"))) contribuyente.Seudonimo = lector.GetString(lector.GetOrdinal("seudonimo"));
                if (!lector.IsDBNull(lector.GetOrdinal("tipo_contribuyente"))) contribuyente.Tipo_contribuyente = (TipoContribuyente)Enum.Parse(typeof(TipoContribuyente), lector.GetString(lector.GetOrdinal("tipo_contribuyente")));
                contribuyentes.Add(contribuyente);
            }
            DBManager.Instance.CerrarConexion();



            return contribuyentes;
        }
    }
}
