using SoftProgBusiness.BO;
using SoftProgModel.GestPrestamos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.GestPrestamos.BO
{
    public interface IPrestamoBO : IBaseBO<Prestamo>
    {
        int finalizar_prestamo(int idPrestamo);
    }
}
