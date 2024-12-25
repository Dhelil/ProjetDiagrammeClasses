using ProjetDiagrammeClasses.Class;
using System;
using System.Collections.Generic;

namespace ProjetDiagrammeClasses.Controller
{
    internal class ClasseController
    {
        private List<Classe> classes;

        // Constructeur
        public ClasseController()
        {
            classes = new List<Classe>();
        }

        // Méthodes de gestion des classes
        public Classe CreerClasse(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom de la classe ne peut pas être vide.");

            Classe nouvelleClasse = new Classe(nom);
            classes.Add(nouvelleClasse);
            return nouvelleClasse;
        }

        public void SupprimerClasse(Classe classe)
        {
            if (classe == null)
                throw new ArgumentNullException(nameof(classe), "La classe ne peut pas être null.");
            classes.Remove(classe);
        }

        public Classe RechercherClasseParNom(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom recherché ne peut pas être vide.");

            return classes.Find(c => c.Nom == nom);
        }

        // Méthodes pour gérer les relations via la classe
        public void AjouterRelationEntrante(Classe classe, Relation relation)
        {
            if (classe == null || relation == null)
                throw new ArgumentNullException("La classe et la relation ne peuvent pas être nulles.");

            classe.AjouterRelationEntrante(relation);
        }

        public void SupprimerRelationEntrante(Classe classe, Relation relation)
        {
            if (classe == null || relation == null)
                throw new ArgumentNullException("La classe et la relation ne peuvent pas être nulles.");

            classe.SupprimerRelationEntrante(relation);
        }

        public void AjouterRelationSortante(Classe classe, Relation relation)
        {
            if (classe == null || relation == null)
                throw new ArgumentNullException("La classe et la relation ne peuvent pas être nulles.");

            classe.AjouterRelationSortante(relation);
        }

        public void SupprimerRelationSortante(Classe classe, Relation relation)
        {
            if (classe == null || relation == null)
                throw new ArgumentNullException("La classe et la relation ne peuvent pas être nulles.");

            classe.SupprimerRelationSortante(relation);
        }

        // Méthodes d'accès à toutes les classes
        public List<Classe> ObtenirToutesLesClasses()
        {
            return classes;
        }
    }
}
