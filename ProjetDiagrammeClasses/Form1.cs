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

        // Méthode configurant la barre de menu du fichier principal
        private void InitializeMenuPrincipal()
        {
            // Crée une nouvelle barre de menu principale pour l'application
            MenuStrip menuStrip = new MenuStrip();

            // Création du menu "Fichier"
            ToolStripMenuItem fichierMenu = new ToolStripMenuItem("Fichier");

            // Crée un sous-menu intitulé "Nouveau"
            ToolStripMenuItem nouveauMenuItem = new ToolStripMenuItem("Nouveau");

            // Associe un événement au clic sur "Nouveau"
            // Correction : On passe une référence à la méthode, pas un appel direct
            nouveauMenuItem.Click += (sender, e) => diagrammeDeClasseController.NouveauMenuItem_Click(sender, e);

            // Ajoute le sous-menu "Nouveau" au menu "Fichier"
            fichierMenu.DropDownItems.Add(nouveauMenuItem);

            // Ajoute le menu "Fichier" à la barre de menu principale
            menuStrip.Items.Add(fichierMenu);

            // Intégration de la barre de menu dans la fenêtre principale
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }



        public static class Prompt
        {
            // Méthode statique pour afficher une boîte de dialogue personnalisée avec une étiquette, une zone de texte et un bouton "OK".
            public static string ShowDialog(string text, string caption)
            {
                // Création d'une nouvelle fenêtre (Form) représentant la boîte de dialogue.
                Form prompt = new Form
                {
                    Width = 400,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog, // Style de bordure : fixe (pas redimensionnable)
                    Text = caption, // Titre de la fenêtre
                    StartPosition = FormStartPosition.CenterScreen // Positionnement de la fenêtre : centrée à l'écran
                };

                // Création d'une étiquette (Label) pour afficher le texte explicatif.
                Label textLabel = new Label
                {
                    Left = 20,
                    Top = 20,
                    Text = text,
                    Width = 340
                };

                // Création d'une zone de texte pour permettre à l'utilisateur de saisir des données.
                TextBox textBox = new TextBox
                {
                    Left = 20,
                    Top = 50,
                    Width = 340
                };

                // Création d'un bouton "OK" pour valider l'entrée de l'utilisateur.
                Button confirmation = new Button
                {
                    Text = "OK",
                    Left = 270,
                    Width = 90,
                    Top = 100,
                    DialogResult = DialogResult.OK
                };

                // Ajout des contrôles (Label, TextBox et Button) à la fenêtre.
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);

                // Définit le bouton "OK" comme le bouton activé par défaut lorsqu'on appuie sur la touche "Entrée".
                prompt.AcceptButton = confirmation;

                // Affiche la boîte de dialogue de manière modale (bloque l'accès à d'autres fenêtres jusqu'à sa fermeture).
                DialogResult result = prompt.ShowDialog();

                // Vérifie si l'utilisateur a cliqué sur le bouton "OK".
                if (result == DialogResult.OK)
                {
                    // Retourne le texte saisi dans la zone de texte.
                    return textBox.Text;
                }
                else
                {
                    // Retourne null si l'utilisateur n'a pas validé avec "OK".
                    return null;
                }
            }
        }
    }
}
