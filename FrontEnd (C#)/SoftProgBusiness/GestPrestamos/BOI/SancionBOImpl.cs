using SoftProgBusiness.GestPrestamos.BO;
using SoftProgModel.GestPrestamos;
using SoftProgPersistance.GestPrestamos.DAO;
using SoftProgPersistance.GestPrestamos.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.GestPrestamos.BOI
{
    public class SancionBOImpl : ISancionBO
    {
        private SancionDAO daoSancion;
        public SancionBOImpl()
        {
            daoSancion = new SancionImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoSancion.eliminar(idObjeto);   
        }

        public int finalizar_sancion(int id_sancion)
        {
            return daoSancion.finalizar_sancion(id_sancion);
        }

        public int insertar(Sancion objeto)
        {
            return daoSancion.insertar(objeto); 
        }

        public BindingList<Sancion> listarTodos()
        {
            return daoSancion.listarTodos();    
        }

        public int modificar(Sancion objeto)
        {
            return daoSancion.modificar(objeto);    
        }

        public Sancion obtenerPorId(int idObjeto)
        {
            return daoSancion.obtenerPorId(idObjeto);   
        }

        public void validar(Sancion objeto)
        {
            throw new NotImplementedException();
        }
    }
}
