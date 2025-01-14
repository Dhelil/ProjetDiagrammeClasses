using System;

namespace ProjetDiagrammeClasses.Class
{
    internal class Relation
    {
        // Attributs privés
        private string nom;
        private TypeE type;
        private Classe classeSource;
        private Classe classeDestination;
        public string Cardinalite { get; set;}

        // Enumération pour les types d'attribut
        public enum TypeE
        {
            Association,
            Héritage,
            Agrégation,
            Composition
        }

        // Constructeur
        public Relation(TypeE type, Classe source, Classe destination, string nom)
        {
            this.type = type;
            classeSource = source;
            classeDestination = destination;
            Nom = nom;
        }

        // Getters
        public TypeE GetTypeRelation() { return type; }
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
        public Classe GetClasseSource() { return classeSource; }
        public Classe GetClasseDestination() { return classeDestination; }

        // Setters
        public void SetTypeRelation(TypeE newType) { type = newType; }
        public void SetClasseSource(Classe source) { classeSource = source; }
        public void SetClasseDestination(Classe destination) { classeDestination = destination; }
    }
}
