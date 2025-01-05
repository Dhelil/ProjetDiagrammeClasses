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
        private List<Attribut> attributs; // Nouvelle liste pour les attributs


        // Constructeur
        public Classe(string nom)
        {
            this.nom = nom;
            relationsSortantes = new List<Relation>(); // Initialisation des relations sortantes
            relationsEntrantes = new List<Relation>(); // Initialisation des relations entrantes
            attributs = new List<Attribut>(); // Initialisation de la liste des attributs

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

        public List<Attribut> Attributs => attributs; // Propriété pour accéder aux attributs



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

        // Méthode pour ajouter un attribut
        public void AjouterAttribut(Attribut attribut)
        {
            if (attribut == null)
                throw new ArgumentNullException(nameof(attribut), "L'attribut ne peut pas être null.");
            attributs.Add(attribut);
        }

        // Méthode pour supprimer un attribut
        public void SupprimerAttribut(string nomAttribut)
        {
            Attribut attribut = attributs.Find(a => a.nom == nomAttribut);
            if (attribut != null)
                attributs.Remove(attribut);
            else
                throw new ArgumentException("L'attribut spécifié n'existe pas.");
        }
    }
}
