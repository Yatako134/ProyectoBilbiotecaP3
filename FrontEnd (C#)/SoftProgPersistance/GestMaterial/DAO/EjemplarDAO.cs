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
    public interface EjemplarDAO : IDAO<Ejemplar>
    {
        BindingList<Ejemplar> listar_disponibles_por_material(int _id_material);
    }
}
