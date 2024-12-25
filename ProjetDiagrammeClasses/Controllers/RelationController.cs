using ProjetDiagrammeClasses.Class;
using System;
using System.Collections.Generic;

namespace ProjetDiagrammeClasses.Controller
{
    internal class RelationController
    {
        // Liste pour stocker toutes les relations
        private List<Relation> relations;

        // Constructeur
        public RelationController()
        {
            relations = new List<Relation>();
        }

        // Ajouter une relation
        public void AjouterRelation(Relation.TypeE type, Classe source, Classe destination)
        {
            // Validation de base
            if (source == null || destination == null)
            {
                Console.WriteLine("Erreur : Source ou destination invalide.");
                return;
            }

            // Vérification si la relation existe déjà
            foreach (var relation in relations)
            {
                if (relation.GetClasseSource() == source && relation.GetClasseDestination() == destination && relation.GetTypeRelation() == type)
                {
                    Console.WriteLine("Erreur : La relation existe déjà.");
                    return;
                }
            }

            // Création de la nouvelle relation
            Relation nouvelleRelation = new Relation(type, source, destination);
            relations.Add(nouvelleRelation);

            // Mise à jour des relations des classes
            source.AjouterRelationSortante(nouvelleRelation);
            destination.AjouterRelationEntrante(nouvelleRelation);

            Console.WriteLine("Relation ajoutée avec succès.");
        }


        // Supprimer une relation
        public void SupprimerRelation(Relation relation)
        {
            if (relation == null || !relations.Contains(relation))
            {
                Console.WriteLine("Erreur : Relation introuvable.");
                return;
            }

            relations.Remove(relation);

            // Mise à jour des classes
            relation.GetClasseSource()?.SupprimerRelationSortante(relation);
            relation.GetClasseDestination()?.SupprimerRelationEntrante(relation);

            Console.WriteLine("Relation supprimée avec succès.");
        }


        // Récupérer toutes les relations
        public List<Relation> ObtenirRelations()
        {
            return relations;
        }

        // Afficher toutes les relations
        public void AfficherRelations()
        {
            if (relations.Count == 0)
            {
                Console.WriteLine("Aucune relation enregistrée.");
                return;
            }

            foreach (var relation in relations)
            {
               // Console.WriteLine(relation.Description());
            }
        }
    }
}
