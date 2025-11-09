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
    public class RolBOImpl : IRolBO
    {
        private RolDAO daoRol;
        public RolBOImpl()
        {
            daoRol = new RolImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoRol.eliminar(idObjeto);
        }

        public int insertar(Rol objeto)
        {
            return daoRol.insertar(objeto);
        }

        public BindingList<Rol> listarTodos()
        {
            return daoRol.listarTodos();
        }

        public int modificar(Rol objeto)
        {
            return daoRol.modificar(objeto);
        }

        public Rol obtenerPorId(int idObjeto)
        {
            return daoRol.obtenerPorId(idObjeto);
        }

        public void validar(Rol objeto)
        {
            throw new NotImplementedException();
        }
    }
}
