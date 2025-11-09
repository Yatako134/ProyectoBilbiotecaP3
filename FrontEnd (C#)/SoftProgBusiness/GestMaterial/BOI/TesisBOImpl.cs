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
    public class TesisBOImpl : ITesisBO
    {
        private TesisDAO daoTesis;
        public TesisBOImpl()
        {
            daoTesis = new TesisImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoTesis.eliminar(idObjeto);
        }

        public int insertar(Tesis objeto)
        {
            return daoTesis.insertar(objeto);
        }

        public BindingList<Tesis> listarTodos()
        {
            return daoTesis.listarTodos();
        }

        public int modificar(Tesis objeto)
        {
            return daoTesis.modificar(objeto);
        }

        public Tesis obtenerPorId(int idObjeto)
        {
            return daoTesis.obtenerPorId(idObjeto);
        }

        public void validar(Tesis objeto)
        {
            throw new NotImplementedException();
        }
    }
}
