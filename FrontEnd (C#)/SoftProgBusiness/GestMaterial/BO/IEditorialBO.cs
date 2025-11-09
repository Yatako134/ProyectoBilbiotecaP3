using SoftProgBusiness.BO;
using SoftProgModel.GestMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.GestMaterial.BO
{
    public interface IEditorialBO : IBaseBO<Editorial>
    {
        int asignar_editorial(int id_material, int id_editorial);
    }
}
