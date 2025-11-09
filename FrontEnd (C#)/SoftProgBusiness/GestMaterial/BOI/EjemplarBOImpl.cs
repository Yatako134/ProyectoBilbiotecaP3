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
    public class EjemplarBOImpl : IEjemplarBO
    {
        private EjemplarDAO daoEjemplar;
        public EjemplarBOImpl()
        {
            daoEjemplar = new EjemplarImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoEjemplar.eliminar(idObjeto);
        }

        public int insertar(Ejemplar objeto)
        {
            return daoEjemplar.insertar(objeto);
        }

        public BindingList<Ejemplar> listarTodos()
        {
            return daoEjemplar.listarTodos();
        }

        public int modificar(Ejemplar objeto)
        {
            return daoEjemplar.modificar(objeto);   
        }

        public Ejemplar obtenerPorId(int idObjeto)
        {
            return daoEjemplar.obtenerPorId(idObjeto);
        }

        public void validar(Ejemplar objeto)
        {
            throw new NotImplementedException();
        }
    }
}
