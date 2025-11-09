using MySql.Data.MySqlClient;
using SoftProgBusiness.GestMaterial.BOI;
using SoftProgDBManager;
using SoftProgModel.GestMaterial;
using SoftProgPersistance.GestMaterial.DAO;
using SoftProgPersistance.GestMaterial.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProg
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ContribuyenteBOImpl contribuDAO = new ContribuyenteBOImpl();
            BindingList<Contribuyente> contribuyentes = contribuDAO.listar_autores_por_material(1);
            foreach (Contribuyente c in contribuyentes)
            {
                Console.WriteLine(c.Nombre + " " + c.Primer_apellido);
            }
        }
    }
}
