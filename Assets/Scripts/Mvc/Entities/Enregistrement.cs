using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
using Mvc.Controllers;
using UnityEngine.SceneManagement;


namespace Mvc.Entities
{
    public class Enregistrement : MonoBehaviour
    {
        [SerializeField] private string nomJoueur1;
        [SerializeField] private string nomJoueur2;
        [SerializeField] private Match match;
        [SerializeField] private SceneController sceneController;
        [SerializeField] private string nomScene;
        [SerializeField] private int[] listeCases = new int[16];
        [SerializeField] private TMPro.TMP_Text textTotal;


        public Match Match { get => match; set => match = value; }
        public string NomJoueur1 { get => nomJoueur1; set => nomJoueur1 = value; }
        public string NomJoueur2 { get => nomJoueur2; set => nomJoueur2 = value; }
        public int[] ListeCases { get => listeCases; set => listeCases = value; }

        public void saisirNomJoueur1(string nom)
        {
            nomJoueur1 = nom;
        }
        public void saisirNomJoueur2(string nom)
        {
            nomJoueur2 = nom;
        }
        public void saisirNombrePionsCase1(string nombrePion = "0")
        {
            listeCases[0] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase2(string nombrePion = "0")
        {
            listeCases[1] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase3(string nombrePion = "0")
        {
            listeCases[2] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase4(string nombrePion = "0")
        {
            listeCases[3] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase5(string nombrePion = "0")
        {
            listeCases[4] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase6(string nombrePion = "0")
        {
            listeCases[5] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase7(string nombrePion = "0")
        {
            listeCases[6] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase8(string nombrePion = "0")
        {
            listeCases[7] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase9(string nombrePion = "0")
        {
            listeCases[8] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase10(string nombrePion = "0")
        {
            listeCases[9] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase11(string nombrePion = "0")
        {
            listeCases[10] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase12(string nombrePion = "0")
        {
            listeCases[11] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase13(string nombrePion = "0")
        {
            listeCases[12] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCase14(string nombrePion = "0")
        {
            listeCases[13] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCaseG1(string nombrePion = "0")
        {
            listeCases[14] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void saisirNombrePionsCaseG2(string nombrePion = "0")
        {
            listeCases[15] = nombrePion == "" ? 0 : int.Parse(nombrePion);
            afficheTotal();
        }
        public void boutonEntrer()
        {
            if (Fonctions.sceneActuelle("SceneMatch1vs1"))
            {
                if (nomJoueur1.Length > 1 && nomJoueur2.Length > 1)
                {
                    Fonctions.debutChargement();
                    sceneController.commencerMatchHorsLigne();
                }
                else
                {
                    Fonctions.afficherMsgScene("Deux lettres au minimum ", "erreur");
                }
            }
            else if (Fonctions.sceneActuelle("SceneMatchEntrainement"))
            {
                if (totalPion() == 70)
                {
                    if (totalPionJoueur1() == 0 || totalPionJoueur2() == 0)
                    {
                        Fonctions.afficherMsgScene("Au moins un pion de chaque coté", "erreur");
                    }
                    else
                    {
                        Fonctions.debutChargement();
                        sceneController.commencerMatchHorsLigne();
                    }

                }
                else if (totalPion() > 70)
                {
                    Fonctions.afficherMsgScene("Il y a plus de 70 pions", "erreur");
                }
                else
                {
                    Fonctions.afficherMsgScene("Il y a moins de 70 pions", "erreur");
                }
            }
        }
        public void boutonRetour()
        {
            if (Fonctions.sceneActuelle("SceneMatch1vs1"))
            {
                nomScene = "ScenePlay";
                Fonctions.changerDeScene(nomScene);
            }
            else if (Fonctions.sceneActuelle("SceneMatchEntrainement"))
            {
                nomScene = "SceneMenuPrincipal";
                Fonctions.changerDeScene(nomScene);
            }
        }

        public void afficheTotal()
        {
            textTotal.text = totalPion().ToString();
        }
        public int totalPion()
        {
            int som = 0;
            for (int i = 0; i < 16; i++)
            {
                som += listeCases[i];
            }
            return som;
        }
        public int totalPionJoueur1()
        {
            int som = 0;
            for (int i = 0; i < 7; i++)
            {
                som += listeCases[i];
            }
            return som;
        }
        public int totalPionJoueur2()
        {
            int som = 0;
            for (int i = 7; i < 14; i++)
            {
                som += listeCases[i];
            }
            return som;
        }

    }
}
