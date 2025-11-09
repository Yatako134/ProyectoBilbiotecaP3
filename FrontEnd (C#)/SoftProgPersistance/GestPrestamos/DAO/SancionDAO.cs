using SoftProgModel.GestPrestamos;
using SoftProgPersistance.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestPrestamos.DAO
{
    public interface SancionDAO : IDAO<Sancion>
    {
        int finalizar_sancion(int id_sancion);
    }
}
