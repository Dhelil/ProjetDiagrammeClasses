using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDiagrammeClasses.Class
{
    internal class Methode
    {
        public string nom;
        public string type;
        public List<string> parametres;

        // Constructeur
        public Methode(string unNom, string unType, List<string> uneListeDeParametres)
        {
            nom = unNom;
            type = unType;
            parametres = uneListeDeParametres;
        }

        // Getters
        public string getNom() { return nom; }
        public string getType() { return type.ToString(); }

        // Setters
        public void setNom(string unNewNom) { nom = unNewNom; }
        //public void setType(TypeE unNewType) { type = unNewType.ToString(); }
    }
}
