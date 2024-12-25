using System;
using System.Collections.Generic;

namespace ProjetDiagrammeClasses.Class
{
    internal class Classe
    {
        // Attributs privés
        private string nom;
        private List<Relation> relationsSortantes;
        private List<Relation> relationsEntrantes;

        // Constructeur
        public Classe(string nom)
        {
            this.nom = nom;
            relationsSortantes = new List<Relation>(); // Initialisation des relations sortantes
            relationsEntrantes = new List<Relation>(); // Initialisation des relations entrantes
        }

        // Propriétés
        public string Nom
        {
            get => nom;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le nom ne peut pas être vide.");
                nom = value;
            }
        }

        public List<Relation> RelationsSortantes => relationsSortantes;

        public List<Relation> RelationsEntrantes => relationsEntrantes;


        // Méthodes de gestion des relations entrantes
        public void AjouterRelationEntrante(Relation relation)
        {
            if (relation == null)
                throw new ArgumentNullException(nameof(relation), "La relation ne peut pas être null.");
            if (!relationsEntrantes.Contains(relation))
                relationsEntrantes.Add(relation);
        }

        public void SupprimerRelationEntrante(Relation relation)
        {
            if (relation == null)
                throw new ArgumentNullException(nameof(relation), "La relation ne peut pas être null.");
            relationsEntrantes.Remove(relation);
        }


        // Méthodes de gestion des relations sortantes
        public void AjouterRelationSortante(Relation relation)
        {
            if (relation == null)
                throw new ArgumentNullException(nameof(relation), "La relation ne peut pas être null.");
            if (!relationsSortantes.Contains(relation))
                relationsSortantes.Add(relation);
        }

        public void SupprimerRelationSortante(Relation relation)
        {
            if (relation == null)
                throw new ArgumentNullException(nameof(relation), "La relation ne peut pas être null.");
            relationsSortantes.Remove(relation);
        }
    }
}
