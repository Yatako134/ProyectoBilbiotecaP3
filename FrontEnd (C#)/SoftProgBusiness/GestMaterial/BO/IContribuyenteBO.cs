using SoftProgBusiness.BO;
using SoftProgModel.GestMaterial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.GestMaterial.BO
{
    public interface IContribuyenteBO : IBaseBO<Contribuyente>
    {
        int asignar_contribuyente(int id_material, int id_contribuyente);
        BindingList<Contribuyente> listar_autores_por_material(int id_material);
    }
}
