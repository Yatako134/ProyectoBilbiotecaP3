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
    public class UsuarioImpl : UsuarioDAO
    {
        private DbDataReader lector;
        public int eliminar(int idObjeto)
        {
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_usuario", DbType.Int32, idObjeto, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("ELIMINAR_USUARIO", parametros);
        }

        public int insertar(Usuario usuario)
        {
            DbParameter[] parametros = new DbParameter[10];
            parametros[0] = DBManager.Instance.CreateParam("_id_usuario", DbType.Int32, null, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_codigo_universitario", DbType.String, usuario.Codigo_universitario, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_nombre", DbType.Int32, usuario.Nombre, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_primer_apellido", DbType.Int32, usuario.Primer_apellido, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_segundo_apellido", DbType.String, usuario.Segundo_apellido, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_DOI", DbType.String, usuario.DOI1, ParameterDirection.Input);
            parametros[6] = DBManager.Instance.CreateParam("_correo", DbType.String, usuario.Correo, ParameterDirection.Input);
            parametros[7] = DBManager.Instance.CreateParam("_contrasena", DbType.String, usuario.Contrasena, ParameterDirection.Input);
            parametros[8] = DBManager.Instance.CreateParam("_numero_de_telefono", DbType.Int32, usuario.Numero_de_telefono, ParameterDirection.Input);
            parametros[9] = DBManager.Instance.CreateParam("_id_rol", DbType.Int32, usuario.Rol_usuario.Id_rol, ParameterDirection.Input);
            DBManager.Instance.EjecutarProcedimiento("INSERTAR_USUARIO", parametros);
            usuario.Id_usuario = Convert.ToInt32(parametros[0].Value);
            return usuario.Id_usuario;
        }

        public BindingList<Usuario> listarTodos()
        {
            BindingList<Usuario> usuarios = null;
            lector = DBManager.Instance.EjecutarProcedimientoLectura("LISTAR_USUARIOS_TODOS", null);
            while (lector.Read())
            {
                if (usuarios == null) usuarios = new BindingList<Usuario>();
                Usuario usuario = new Usuario();
                if (!lector.IsDBNull(lector.GetOrdinal("id_usuario"))) usuario.Id_usuario = lector.GetInt32(lector.GetOrdinal("id_usuario"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("codigo_universitario"))) usuario.Codigo_universitario = lector.GetInt32(lector.GetOrdinal("codigo_universitario"));
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) usuario.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                if (!lector.IsDBNull(lector.GetOrdinal("primer_apellido"))) usuario.Primer_apellido = lector.GetString(lector.GetOrdinal("primer_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("segundo_apellido"))) usuario.Segundo_apellido = lector.GetString(lector.GetOrdinal("segundo_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("DOI"))) usuario.DOI1 = lector.GetInt32(lector.GetOrdinal("DOI"));
                if (!lector.IsDBNull(lector.GetOrdinal("contrasena"))) usuario.Contrasena = lector.GetString(lector.GetOrdinal("contrasena"));
                if (!lector.IsDBNull(lector.GetOrdinal("correo"))) usuario.Correo = lector.GetString(lector.GetOrdinal("correo"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_de_telefono"))) usuario.Numero_de_telefono = lector.GetString(lector.GetOrdinal("numero_de_telefono"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) usuario.Activa = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("id_rol"))) usuario.Rol_usuario.Id_rol = lector.GetInt32(lector.GetOrdinal("id_rol"));
                usuarios.Add(usuario);
            }
            DBManager.Instance.CerrarConexion();
            return usuarios;
        }

        public int modificar(Usuario usuario)
        {
            DbParameter[] parametros = new DbParameter[10];
            parametros[0] = DBManager.Instance.CreateParam("_id_usuario", DbType.Int32, usuario.Id_usuario, ParameterDirection.Output);
            parametros[1] = DBManager.Instance.CreateParam("_codigo_universitario", DbType.String, usuario.Codigo_universitario, ParameterDirection.Input);
            parametros[2] = DBManager.Instance.CreateParam("_nombre", DbType.Int32, usuario.Nombre, ParameterDirection.Input);
            parametros[3] = DBManager.Instance.CreateParam("_primer_apellido", DbType.Int32, usuario.Primer_apellido, ParameterDirection.Input);
            parametros[4] = DBManager.Instance.CreateParam("_segundo_apellido", DbType.String, usuario.Segundo_apellido, ParameterDirection.Input);
            parametros[5] = DBManager.Instance.CreateParam("_DOI", DbType.String, usuario.DOI1, ParameterDirection.Input);
            parametros[6] = DBManager.Instance.CreateParam("_correo", DbType.String, usuario.Correo, ParameterDirection.Input);
            parametros[7] = DBManager.Instance.CreateParam("_contrasena", DbType.String, usuario.Contrasena, ParameterDirection.Input);
            parametros[8] = DBManager.Instance.CreateParam("_numero_de_telefono", DbType.Int32, usuario.Numero_de_telefono, ParameterDirection.Input);
            parametros[9] = DBManager.Instance.CreateParam("_id_rol", DbType.Int32, usuario.Rol_usuario.Id_rol, ParameterDirection.Input);
            return DBManager.Instance.EjecutarProcedimiento("MODIFICAR_USUARIO", parametros);
        }

        public Usuario obtenerPorId(int idObjeto)
        {
            Usuario usuario = null;
            DbParameter[] parametros = new DbParameter[1];
            parametros[0] = DBManager.Instance.CreateParam("_id_usuario", DbType.Int32, idObjeto, ParameterDirection.Input);
            lector = DBManager.Instance.EjecutarProcedimientoLectura("OBTENER_USUARIO_X_ID", parametros);
            if (lector.Read())
            {
                if (usuario == null) usuario = new Usuario();
                if (!lector.IsDBNull(lector.GetOrdinal("id_usuario"))) usuario.Id_usuario = lector.GetInt32(lector.GetOrdinal("id_usuario"));// Se coloca el identificador del select
                if (!lector.IsDBNull(lector.GetOrdinal("codigo_universitario"))) usuario.Codigo_universitario = lector.GetInt32(lector.GetOrdinal("codigo_universitario"));
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) usuario.Nombre = lector.GetString(lector.GetOrdinal("nombre"));
                if (!lector.IsDBNull(lector.GetOrdinal("primer_apellido"))) usuario.Primer_apellido= lector.GetString(lector.GetOrdinal("primer_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("segundo_apellido"))) usuario.Segundo_apellido =  lector.GetString(lector.GetOrdinal("segundo_apellido"));
                if (!lector.IsDBNull(lector.GetOrdinal("DOI"))) usuario.DOI1 = lector.GetInt32(lector.GetOrdinal("DOI"));
                if (!lector.IsDBNull(lector.GetOrdinal("contrasena"))) usuario.Contrasena = lector.GetString(lector.GetOrdinal("contrasena"));
                if (!lector.IsDBNull(lector.GetOrdinal("correo"))) usuario.Correo = lector.GetString(lector.GetOrdinal("correo"));
                if (!lector.IsDBNull(lector.GetOrdinal("numero_de_telefono"))) usuario.Numero_de_telefono = lector.GetString(lector.GetOrdinal("numero_de_telefono"));
                if (!lector.IsDBNull(lector.GetOrdinal("activo"))) usuario.Activa = lector.GetBoolean(lector.GetOrdinal("activo"));
                if (!lector.IsDBNull(lector.GetOrdinal("id_rol"))) usuario.Rol_usuario.Id_rol = lector.GetInt32(lector.GetOrdinal("id_rol"));
            }
            DBManager.Instance.CerrarConexion();
            return usuario;
        }
    }
}
