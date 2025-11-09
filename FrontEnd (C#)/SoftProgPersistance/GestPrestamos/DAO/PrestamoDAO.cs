using SoftProgModel.GestPrestamos;
using SoftProgPersistance.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestPrestamos.DAO
{
    public interface PrestamoDAO : IDAO<Prestamo>
    {
        int finalizar_prestamo(int idPrestamo);
    }
}
