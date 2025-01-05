using System;

namespace ProjetDiagrammeClasses.Class
{
    internal class Attribut
    {
        // Propriétés automatiques
        public string nom { get; set; }              // Nom de l'attribut
        public TypeE Type { get; set; }             // Type de l'attribut
        public VisibiliteE Visibilite { get; set; } // Visibilité de l'attribut

        // Déclaration de l'énumération pour les types d'attribut
        public enum TypeE
        {
            Int,
            Float,
            Double,
            Char,
            String,
            Bool,
            Object
        }

        // Déclaration de l'énumération pour la visibilité
        public enum VisibiliteE
        {
            Public,
            Private,
            Protected
        }

        // Constructeur avec validation
        public Attribut(string unNom, TypeE unType, VisibiliteE uneVisibilite)
        {
            if (string.IsNullOrWhiteSpace(unNom))
            {
                throw new ArgumentException("Le nom de l'attribut ne peut pas être vide ou null.");
            }

            nom = unNom;
            Type = unType;
            Visibilite = uneVisibilite;
        }

        // Méthode ToString pour une représentation textuelle de l'attribut
        public override string ToString()
        {
            return $"{Visibilite.ToString().ToLower()} {Type.ToString().ToLower()} {nom}";
        }
    }
}
