using System;
using System.Collections.Generic;

namespace ProjetDiagrammeClasses.Class
{
    internal class Attribut
    {
        public string nom;              // Nom de l'attribut
        public TypeE type;              // Type de l'attribut
        public VisibiliteE visibilite;  // Visibilité de l'attribut

        // Déclaration de l'énumération pour les types d'attribut
        public enum TypeE
        {
            Int,
            Float,
            Char,
            String
        }

        // Déclaration de l'énumération pour la visibilité
        public enum VisibiliteE
        {
            Public,
            Private,
            Protected
        }

        // Constructeur
        public Attribut(string UnNom, TypeE UnType, VisibiliteE UneVisibilite)
        {
            nom = UnNom;
            type = UnType;
            visibilite = UneVisibilite;
        }

        // Getters
        public string getNom() { return nom; }
        public string getType() { return type.ToString(); }
        public string getVisibilite() { return visibilite.ToString(); }

        // Setters
        public void setNom(string UnNewnom) { nom = UnNewnom; }
        public void setType(TypeE UnNewType) { type = UnNewType; }
        public void setVisibilite(VisibiliteE UneNewVisibilite) { visibilite = UneNewVisibilite; }
    }
}
