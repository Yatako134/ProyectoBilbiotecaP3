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
    public class BibliotecaBOImpl : IBibliotecaBO
    {
        private BibliotecaDAO daoBiblioteca;
        public BibliotecaBOImpl()
        {
            daoBiblioteca = new BibliotecaImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoBiblioteca.eliminar(idObjeto);
        }

        public int insertar(Biblioteca objeto)
        {
            return daoBiblioteca.insertar(objeto);
        }

        public BindingList<Biblioteca> listarTodos()
        {
            return daoBiblioteca.listarTodos();
        }

        public int modificar(Biblioteca objeto)
        {
            return daoBiblioteca.modificar(objeto);
        }

        public Biblioteca obtenerPorId(int idObjeto)
        {
            return daoBiblioteca.obtenerPorId(idObjeto);
        }

        public void validar(Biblioteca objeto)
        {
            throw new NotImplementedException();
        }
    }
}
