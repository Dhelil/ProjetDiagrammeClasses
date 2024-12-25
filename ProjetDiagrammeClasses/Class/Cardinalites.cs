using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDiagrammeClasses.Class
{
    internal class Cardinalites
    {
        public int min;     // Cardinalité min
        public int max;     // Cardinalité max

        // Type de cardinalité
        public TypeE type;

        // Déclaration de l'énumération pour les types de cardinalités
        public enum TypeE
        {
            Unitaire,  // 1
            Multiple,   // n
            ZeroOuUn,   // 0..1
            ZeroOuPlus, // 0..*
            UnOuPlus    // 1..*
        }


        // Constructeur
        public Cardinalites(int UnMin, int UnMax, TypeE UnType)
        {
            min = UnMin;
            max = UnMax;
            type = UnType;
        }
    }
}
