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
    public class ContribuyenteBOImpl : IContribuyenteBO
    {
        private ContribuyenteDAO daoContribuyente;
        public ContribuyenteBOImpl()
        {
            daoContribuyente = new ContribuyenteImpl();
        }
        public int asignar_contribuyente(int id_material, int id_contribuyente)
        {
            return daoContribuyente.asignar_contribuyente(id_material, id_contribuyente);
        }

        public int eliminar(int idObjeto)
        {
            return daoContribuyente.eliminar(idObjeto);
        }

        public int insertar(Contribuyente objeto)
        {
            return daoContribuyente.insertar(objeto);
        }

        public BindingList<Contribuyente> listarTodos()
        {
            return daoContribuyente.listarTodos();
        }

        public int modificar(Contribuyente objeto)
        {
            return daoContribuyente.modificar(objeto);
        }

        public Contribuyente obtenerPorId(int idObjeto)
        {
            return daoContribuyente.obtenerPorId(idObjeto);
        }

        public void validar(Contribuyente objeto)
        {
            throw new NotImplementedException();
        }

       public BindingList<Contribuyente> listar_autores_por_material(int id_material)
        {
            return daoContribuyente.listar_autores_por_material(id_material);
        }
    }
}
