using System;

namespace ProjetDiagrammeClasses.Class
{
    internal class Relation
    {
        // Attributs privés
        private TypeE type;
        private Classe classeSource;
        private Classe classeDestination;

        // Enumération pour les types d'attribut
        public enum TypeE
        {
            Association,
            Héritage,
            Agrégation,
            Composition
        }

        // Constructeur
        public Relation(TypeE type, Classe source, Classe destination)
        {
            this.type = type;
            classeSource = source;
            classeDestination = destination;
        }

        // Getters
        public TypeE GetTypeRelation() { return type; }
        public Classe GetClasseSource() { return classeSource; }
        public Classe GetClasseDestination() { return classeDestination; }

        // Setters
        public void SetTypeRelation(TypeE newType) { type = newType; }
        public void SetClasseSource(Classe source) { classeSource = source; }
        public void SetClasseDestination(Classe destination) { classeDestination = destination; }

        // Méthodes supplémentaires
       // public string Description()
        //{
           // return $"Relation de type {type} entre {classeSource?.GetNom()} et {classeDestination?.GetNom()}.";
        //}
    }
}
