using SoftProgBusiness.GestMaterial.BO;
using SoftProgModel.GestMaterial;
using SoftProgPersistance.GestMaterial.DAO;
using SoftProgPersistance.GestMaterial.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.GestMaterial.BOI
{
    public class EditorialBOImpl : IEditorialBO
    {
        private EditorialDAO daoEditorial;
        public EditorialBOImpl()
        {
            daoEditorial = new EditorialImpl();
        }
        public int asignar_editorial(int id_material, int id_editorial)
        {
            return daoEditorial.asignar_editorial(id_material, id_editorial);
        }

        public int eliminar(int idObjeto)
        {
            return daoEditorial.eliminar(idObjeto);
        }

        public int insertar(Editorial objeto)
        {
            return daoEditorial.insertar(objeto);
        }

        public BindingList<Editorial> listarTodos()
        {
            return daoEditorial.listarTodos();
        }

        public int modificar(Editorial objeto)
        {
            return daoEditorial.modificar(objeto);
        }

        public Editorial obtenerPorId(int idObjeto)
        {
            return daoEditorial.obtenerPorId(idObjeto);
        }

        public void validar(Editorial objeto)
        {
            throw new NotImplementedException();
        }
    }
}
