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
    public class LibroBOImpl : ILibroBO
    {
        private LibroDAO daoLibro;
        public LibroBOImpl()
        {
            daoLibro = new LibroImpl();
        }
        public int eliminar(int idObjeto)
        {
            return daoLibro.eliminar(idObjeto); 
        }

        public int insertar(Libro objeto)
        {
            return daoLibro.insertar(objeto);   
        }

        public BindingList<Libro> listarTodos()
        {
            return daoLibro.listarTodos();  
        }

        public int modificar(Libro objeto)
        {
            return daoLibro.modificar(objeto);
        }

        public Libro obtenerPorId(int idObjeto)
        {
            return daoLibro.obtenerPorId(idObjeto);
        }

        public void validar(Libro objeto)
        {
            throw new NotImplementedException();
        }
    }
}
