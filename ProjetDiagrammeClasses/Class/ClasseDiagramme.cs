using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetDiagrammeClasses.Class
{
    public class ClasseDiagram : UserControl
    {
        public string Nom { get; private set; }

        public ClasseDiagram(string nom)
        {
            this.Nom = nom;
            this.Size = new Size(150, 100); // Taille par défaut du rectangle
            this.BackColor = Color.LightGray; // Couleur de fond
            this.BorderStyle = BorderStyle.FixedSingle; // Bordure pour délimiter le contrôle
            this.Paint += ClasseDiagram_Paint; // Événement pour dessiner le texte
        }

        private void ClasseDiagram_Paint(object sender, PaintEventArgs e)
        {
            // Dessiner le texte de la classe (nom de la classe)
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            {
                e.Graphics.DrawString(this.Nom, font, Brushes.Black, new Point(10, 10));
            }
        }
    }
}
