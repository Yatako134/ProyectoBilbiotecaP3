using SoftProgBusiness.BO;
using SoftProgModel.GestPrestamos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.GestPrestamos.BO
{
    public interface ISancionBO : IBaseBO<Sancion>
    {
        int finalizar_sancion(int id_sancion);
    }
}
