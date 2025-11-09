using SoftProgBusiness.BO;
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
    public class MaterialBOImpl : IMaterialBO
    {
        private MaterialDAO materialDAO;
        public MaterialBOImpl() {
            materialDAO = new MaterialImpl();
        }
        int IBaseBO<MaterialBibliografico>.eliminar(int idObjeto)
        {
            throw new NotImplementedException();
        }

        int IBaseBO<MaterialBibliografico>.insertar(MaterialBibliografico objeto)
        {
            throw new NotImplementedException();
        }

        public BindingList<MaterialBibliografico> listarTodos()
        {
            return materialDAO.listarTodos();
        }

        int IBaseBO<MaterialBibliografico>.modificar(MaterialBibliografico objeto)
        {
            throw new NotImplementedException();
        }

        MaterialBibliografico IBaseBO<MaterialBibliografico>.obtenerPorId(int idObjeto)
        {
            throw new NotImplementedException();
        }

        void IBaseBO<MaterialBibliografico>.validar(MaterialBibliografico objeto)
        {
            throw new NotImplementedException();
        }
    }
}
