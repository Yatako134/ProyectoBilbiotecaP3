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
    public class ArticuloBOImpl : IArticuloBO
    {
        private ArticuloDAO daoArticulo;
        public ArticuloBOImpl()
        {
            daoArticulo = new ArticuloImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoArticulo.eliminar(idObjeto);
        }

        public int insertar(Articulo objeto)
        {
            return daoArticulo.insertar(objeto);
        }

        public BindingList<Articulo> listarTodos()
        {
            return daoArticulo.listarTodos();
        }

        public int modificar(Articulo objeto)
        {
            return daoArticulo.modificar(objeto);   
        }

        public Articulo obtenerPorId(int idObjeto)
        {
            return daoArticulo.obtenerPorId(idObjeto);  
        }

        public void validar(Articulo objeto)
        {
            throw new NotImplementedException();
        }
    }
}
