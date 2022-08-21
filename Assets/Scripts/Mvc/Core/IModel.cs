
using System.Collections.Generic;
using Firebase.Database;

namespace Mvc.Core
{
    public interface IModel
    {
        public void insert();
        public void update( string cheminAttribut,Dictionary<string, object> cleValeur);
        public void delete(string id);
        public void selectAll();
        public void selectById(string id);
        public void selectWhere(Query requete);

    }
}
