using UnityEngine;

namespace Mvc.Entities
{
    public class SongoNiveau
    {
        [SerializeField] private int id;
        [SerializeField] private string nomNiveau;

        public SongoNiveau(int id, string nomNiveau)
        {
            this.id = id;
            this.nomNiveau = nomNiveau;
        }

        public int Id { get => id; set => id = value; }
        public string NomNiveau { get => nomNiveau; set => nomNiveau = value; }
    }
}
