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
    public interface BibliotecaDAO : IDAO<Biblioteca>
    {
        BindingList<Biblioteca> listar_bibliotecas_por_material(int _id_material);
    }
}
