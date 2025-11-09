using SoftProgBusiness.GestUsuarios.BO;
using SoftProgModel.GestUsuarios;
using SoftProgPersistance.GestUsuarios.DAO;
using SoftProgPersistance.GestUsuarios.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.GestUsuarios.BOI
{
    public class UsuarioBOImpl : IUsuarioBO
    {
        private UsuarioDAO daoUsuario;
        public UsuarioBOImpl()
        {
            daoUsuario = new UsuarioImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoUsuario.eliminar(idObjeto);
        }

        public int insertar(Usuario objeto)
        {
            return daoUsuario.insertar(objeto);
        }

        public BindingList<Usuario> listarTodos()
        {
            return daoUsuario.listarTodos();    
        }

        public int modificar(Usuario objeto)
        {
            return daoUsuario.modificar(objeto);
        }

        public Usuario obtenerPorId(int idObjeto)
        {
            return daoUsuario.obtenerPorId(idObjeto);   
        }

        public void validar(Usuario objeto)
        {
            throw new NotImplementedException();
        }
    }
}
