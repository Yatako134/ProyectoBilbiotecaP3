using SoftProgModel.GestMaterial;
using SoftProgPersistance.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestMaterial.DAO
{
    public interface EditorialDAO : IDAO<Editorial>
    {
        int asignar_editorial(int id_material, int id_editorial);
    }
}
