using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
