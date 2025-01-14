using System;
using System.Collections.Generic;

namespace ProjetDiagrammeClasses.Class
{
    internal class DiagrammeDeClasse
    {
        // Attributs
        private List<Classe> classes;
        private List<Relation> relations;

        // Constructeur
        public DiagrammeDeClasse()
        {
            classes = new List<Classe>();
            relations = new List<Relation>();
        }

        // Getters
        public List<Classe> getClasses() { return classes; }
        public List<Relation> getRelations() { return relations; }

        // Setters
        public void setClasses(List<Classe> lesNewClasses) { classes = lesNewClasses; }
        public void setRelations(List<Relation> lesNewRelations) { relations = lesNewRelations; }

        // Méthode permettant d'ajouter une classe
        public event Action ClasseAjoutee;

        // Méthode permettant d'ajouter une classe
        public void AjouterClasse(Classe nouvelleClasse)
        {
            if (nouvelleClasse == null)
                throw new ArgumentNullException(nameof(nouvelleClasse), "La classe ne peut pas être null.");

            if (!classes.Contains(nouvelleClasse))
            {
                classes.Add(nouvelleClasse);
                ClasseAjoutee?.Invoke(); // Notifie qu'une classe a été ajoutée
            }
        }


        // Méthode permettant d'ajouter une relation
        public void AjouterRelation(Relation nouvelleRelation)
        {
            if (nouvelleRelation == null)
                throw new ArgumentNullException(nameof(nouvelleRelation), "La relation ne peut pas être null.");

            if (!relations.Contains(nouvelleRelation))
                relations.Add(nouvelleRelation);
        }
    }
}
