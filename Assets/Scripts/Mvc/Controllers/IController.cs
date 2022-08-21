namespace Mvc.Controllers
{
    public interface IController
    {
        public void lister(bool single = false);
        public void ajouter();
        public void supprimer();
        public void modifier();

    }
}