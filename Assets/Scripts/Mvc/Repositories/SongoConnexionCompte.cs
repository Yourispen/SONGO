using UnityEngine;

namespace Mvc.Repositories
{
    public class SongoConnexionCompte
    {
        [SerializeField] private int id;
        [SerializeField] private string typeConnexionCompte;

        public SongoConnexionCompte(int id, string typeConnexionCompte)
        {
            this.id = id;
            this.typeConnexionCompte = typeConnexionCompte;
        }

        public int Id { get => id; set => id = value; }
        public string TypeConnexionCompte { get => typeConnexionCompte; set => typeConnexionCompte = value; }
    }
}
