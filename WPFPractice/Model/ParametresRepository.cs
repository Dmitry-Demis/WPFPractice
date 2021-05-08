using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice.Model
{
    public static class ParametresRepository
    {
        private static ObservableCollection<Parametres> _clients;
        public static ObservableCollection<Parametres> AllClients
        {
            get
            {
                if (_clients==null)
                {
                    _clients = GenerateClientRepository();
                }
                return _clients;
            }
        }

        private static ObservableCollection<Parametres> GenerateClientRepository()
        {
            ObservableCollection <Parametres> clients = new ObservableCollection<Parametres>();            
            clients.Add(new Parametres { NameOfParametre = "Параметр " + (clients.Count+1), Id = (clients.Count + 1) });
            clients.Add(new Parametres { NameOfParametre = "Параметр " + (clients.Count + 1), Id = (clients.Count + 1) });
            clients.Add(new Parametres { NameOfParametre = "Параметр " + (clients.Count + 1), Id = (clients.Count + 1) });
            clients.Add(new Parametres { NameOfParametre = "Параметр " + (clients.Count + 1), Id = (clients.Count + 1) });
            return clients;

        }
    }
}
