using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
using System;

namespace Mvc.Entities
{
    public class MatchEntrainement : MatchHorsLigne
    {

        private void Start()
        {
            typeDuMatch = TypeMatch.Entrainement;
        }
        public override void initialiseJoueurs()
        {
            Debug.Log("match rejoué : " + matchRejoue);
            if (!matchRejoue)
            {
                joueur1 = ((Joueur)Fonctions.instancierObjet(joueurOffPrefab).GetComponent<JoueurOff>());
                joueur1.Match = ((Match)this);
                joueur1.gameObject.name = "joueur" + (nbJoueur + 1).ToString();
                joueur1.Id = "1";
                joueur1.Surnom = joueur1.gameObject.name;
                joueur1.CouleurTouche = joueur1.CouleurToucheJoueur1;
                nbJoueur += 1;
                joueur2 = ((Joueur)Fonctions.instancierObjet(joueurOffPrefab).GetComponent<JoueurOff>());
                joueur2.Match = ((Match)this);
                joueur2.gameObject.name = "joueur" + (nbJoueur + 1).ToString();
                joueur2.Id = "2";
                joueur2.Surnom = joueur2.gameObject.name;
                joueur2.CouleurTouche = joueur2.CouleurToucheJoueur2;
                outilsJoueur.afficherNomsJoueurs(joueur1.Surnom, joueur2.Surnom);
                Fonctions.desactiverObjet(Enregistrement.gameObject);
            }
            else
            {
                Fonctions.desactiverObjet(Enregistrement.gameObject);
                Fonctions.finChargement();
                StartCoroutine(partagerLesPions());
            }
        }
        public new IEnumerator instancierLesPions()
        {
            etatDuMatch = EtatMatch.Debut;
            for (int i = 0; i < 70; i++)
            {
                listePions.Add(Fonctions.instancierObjet(typePionPrefab[PlayerPrefs.GetInt("typePion")], tableMatch.Caisse.transform.position + new Vector3(UnityEngine.Random.Range(-1.9f, 1.9f), 5f, UnityEngine.Random.Range(-1.9f, 1.9f))).GetComponent<Pion>());
                listePions[i].gameObject.name = "pion" + (i + 1).ToString();
                yield return new WaitForSeconds(Case.tempsDepotCaisse);
            }
            StartCoroutine(partagerLesPions());
        }
        public new IEnumerator partagerLesPions()
        {
            Fonctions.finChargement();
            tableMatch.reinitialiseCases();
            int[] listeCases = new int[16];
            Array.Copy(enregistrement.ListeCases, listeCases, enregistrement.ListeCases.Length);
            int i = 0;
            while (i < 70)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (listeCases[j] > 0)
                    {
                        listePions[i].transform.position = tableMatch.ListeCases[j].transform.position;
                        tableMatch.ListeCases[j].ajouterPion(listePions[i]);
                        listePions[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        i += 1;
                        listeCases[j] -= 1;
                    }
                }
                yield return new WaitForSeconds(Case.tempsAttente);
            }

            joueur1.premierAJouer();
            joueur2.deuxiemeAJouer();
            outilsJoueur.activerCompteurJoueur(1);
            tourJ.activerTourjoueur(1);

            scoreMatch.afficherScoreMatch();
            etatDuMatch = EtatMatch.EnCours;
            pauseMenu.EnPause = false;

        }
        public new void rejouerMatch()
        {
            matchRejoue = true;
            Fonctions.activerObjet(pauseMenu.BoutonPausePrefab);
            Fonctions.desactiverObjet(finMatchMenu.MenuFinMatch);
            Fonctions.activerObjet(enregistrement.gameObject);
        }
    }
}
