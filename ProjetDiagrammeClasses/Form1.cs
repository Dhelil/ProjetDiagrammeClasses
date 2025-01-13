using ProjetDiagrammeClasses.Class;
using ProjetDiagrammeClasses.Controller;
using ProjetDiagrammeClasses.Controllers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetDiagrammeClasses
{
    public partial class Form1 : Form
    {
        private DiagrammeDeClasseController diagrammeDeClasseController = new DiagrammeDeClasseController();

        public Form1()
        {
            InitializeComponent();
            InitializeMenuPrincipal();
        }

        // M�thode configurant la barre de menu du fichier principal
        private void InitializeMenuPrincipal()
        {
            // Cr�e une nouvelle barre de menu principale pour l'application
            MenuStrip menuStrip = new MenuStrip();

            // Cr�ation du menu "Fichier"
            ToolStripMenuItem fichierMenu = new ToolStripMenuItem("Fichier");

            // Cr�e un sous-menu intitul� "Nouveau"
            ToolStripMenuItem nouveauMenuItem = new ToolStripMenuItem("Nouveau");

            // Associe un �v�nement au clic sur "Nouveau"
            // Correction : On passe une r�f�rence � la m�thode, pas un appel direct
            nouveauMenuItem.Click += (sender, e) => diagrammeDeClasseController.NouveauMenuItem_Click(sender, e);

            // Ajoute le sous-menu "Nouveau" au menu "Fichier"
            fichierMenu.DropDownItems.Add(nouveauMenuItem);

            // Ajoute le menu "Fichier" � la barre de menu principale
            menuStrip.Items.Add(fichierMenu);

            // Int�gration de la barre de menu dans la fen�tre principale
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }



        public static class Prompt
        {
            // M�thode statique pour afficher une bo�te de dialogue personnalis�e avec une �tiquette, une zone de texte et un bouton "OK".
            public static string ShowDialog(string text, string caption)
            {
                // Cr�ation d'une nouvelle fen�tre (Form) repr�sentant la bo�te de dialogue.
                Form prompt = new Form
                {
                    Width = 400,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog, // Style de bordure : fixe (pas redimensionnable)
                    Text = caption, // Titre de la fen�tre
                    StartPosition = FormStartPosition.CenterScreen // Positionnement de la fen�tre : centr�e � l'�cran
                };

                // Cr�ation d'une �tiquette (Label) pour afficher le texte explicatif.
                Label textLabel = new Label
                {
                    Left = 20,
                    Top = 20,
                    Text = text,
                    Width = 340
                };

                // Cr�ation d'une zone de texte pour permettre � l'utilisateur de saisir des donn�es.
                TextBox textBox = new TextBox
                {
                    Left = 20,
                    Top = 50,
                    Width = 340
                };

                // Cr�ation d'un bouton "OK" pour valider l'entr�e de l'utilisateur.
                Button confirmation = new Button
                {
                    Text = "OK",
                    Left = 270,
                    Width = 90,
                    Top = 100,
                    DialogResult = DialogResult.OK
                };

                // Ajout des contr�les (Label, TextBox et Button) � la fen�tre.
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);

                // D�finit le bouton "OK" comme le bouton activ� par d�faut lorsqu'on appuie sur la touche "Entr�e".
                prompt.AcceptButton = confirmation;

                // Affiche la bo�te de dialogue de mani�re modale (bloque l'acc�s � d'autres fen�tres jusqu'� sa fermeture).
                DialogResult result = prompt.ShowDialog();

                // V�rifie si l'utilisateur a cliqu� sur le bouton "OK".
                if (result == DialogResult.OK)
                {
                    // Retourne le texte saisi dans la zone de texte.
                    return textBox.Text;
                }
                else
                {
                    // Retourne null si l'utilisateur n'a pas valid� avec "OK".
                    return null;
                }
            }
        }
    }
}
