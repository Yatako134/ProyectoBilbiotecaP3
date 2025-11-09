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
    public class PrestamoBOImpl : IPrestamoBO
    {
        private PrestamoDAO daoPrestamo;
        public PrestamoBOImpl()
        {
            daoPrestamo = new PrestamoImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoPrestamo.eliminar(idObjeto);
        }

        public int finalizar_prestamo(int idPrestamo)
        {
            return daoPrestamo.finalizar_prestamo(idPrestamo);
        }

        public int insertar(Prestamo objeto)
        {
            return daoPrestamo.insertar(objeto);
        }

        public BindingList<Prestamo> listarTodos()
        {
            return daoPrestamo.listarTodos();
        }

        public int modificar(Prestamo objeto)
        {
            return daoPrestamo.modificar(objeto);   
        }

        public Prestamo obtenerPorId(int idObjeto)
        {
            return daoPrestamo.obtenerPorId (idObjeto); 
        }

        public void validar(Prestamo objeto)
        {
            throw new NotImplementedException();
        }
    }
}
