using SoftProgModel.GestMaterial;
using SoftProgPersistance.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestMaterial.DAO
{
    public interface ContribuyenteDAO : IDAO<Contribuyente>
    {
        int asignar_contribuyente(int id_material, int id_contribuyente);
        BindingList<Contribuyente> listar_autores_por_material(int id_material);
    }
}
