using ProjetDiagrammeClasses.Class;
using System;
using System.Collections.Generic;

namespace ProjetDiagrammeClasses.Controller
{
    internal class ClasseController
    {

        // Constructeur
        public ClasseController()
        {
            //classes = new List<Classe>();
        }

        private List<Classe> classes = new List<Classe>();



        /// Ouvre une fenêtre permettant à l'utilisateur de saisir un nom pour une nouvelle classe.
        /// Si un nom valide est entré, une nouvelle classe est ajoutée visuellement à l'interface dans la fenêtre donnée.
        public void DemanderNomClasse(Form fenetreTravail)
        {
            // Création de la fenêtre
            Form dialogue = new Form
            {
                Text = "Nom de la Classe",
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };


            // Création du label pour afficher une instruction à l'utilisateur
            Label instructionLabel = new Label
            {
                Text = "Entrez le nom de la classe :",
                Location = new Point(10, 10),
                AutoSize = true
            };
            // Insertion du label dans la fenêtre
            dialogue.Controls.Add(instructionLabel);


            // Création de la zone de texte où l'utilisateur pourra entrer le nom de la classe
            TextBox nomTextBox = new TextBox
            {
                Location = new Point(10, 40),
                Width = 260
            };
            // Insertion de la textbox dans la fenêtre
            dialogue.Controls.Add(nomTextBox);


            // Création du bouton "Valider" pour confirmer l'entrée du nom de la classe
            Button validerButton = new Button
            {
                Text = "Valider",
                Location = new Point(100, 80),
                DialogResult = DialogResult.OK
            };
            // Insertion du bouton dans la fenêtre
            dialogue.Controls.Add(validerButton);


            // Associer le gestionnaire d'événements 'ValiderButton_Click' au bouton valider 
            validerButton.Click += ValiderButton_Click;



            // Création du bouton "Annuler" pour fermer la fenêtre sans effectuer d'action
            Button annulerButton = new Button
            {
                Text = "Annuler",
                Location = new Point(180, 80),
                DialogResult = DialogResult.Cancel
            };
            // Insertion du bouton dans la fenêtre
            dialogue.Controls.Add(annulerButton);


            // Affiche la fenêtre en mode modale, bloquant toute interaction avec la fenêtre parente jusqu'à sa fermeture
            dialogue.ShowDialog();


            // Gestionnaire d'événements pour le bouton "Valider", qui vérifie la saisie et ajoute la classe
            void ValiderButton_Click(object sender, EventArgs e)
            {
                // Vérifie si le champ de texte n'est pas vide
                if (!string.IsNullOrWhiteSpace(nomTextBox.Text))
                {
                    AjouterNouvelleClasse(fenetreTravail, nomTextBox.Text);
                    dialogue.Close();
                }
                else
                {
                    MessageBox.Show("Le nom ne peut pas être vide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }




        /// Ajoute une représentation visuelle d'une classe dans l'interface utilisateur.
        /// Cette méthode crée un panneau (Panel) qui représente une classe, avec un nom, 
        /// une liste d'attributs, et une liste de méthodes. Elle permet également 
        /// des interactions utilisateur comme le déplacement et la modification par double-clic.
        private void AjouterNouvelleClasse(Form fenetreTravail, string nomClasse)
        {
            // Calcul de la position centrale de la fenêtre
            int x = (fenetreTravail.ClientSize.Width - 150) / 2; // Position horizontale centrale
            int y = (fenetreTravail.ClientSize.Height - 100) / 2; // Position verticale centrale


            // Création d'un panneau représentant la classe
            Panel nouvelleClassePanel = new Panel
            {
                Location = new Point(x, y), // Position centrale dans la fenêtre
                Size = new Size(150, 150),
                BackColor = Color.LightGray, 
                BorderStyle = BorderStyle.FixedSingle // Bordure pour délimiter visuellement le panneau
            };


            // Création d'une étiquette pour afficher le nom de la classe
            Label nomClasseLabel = new Label
            {
                Text = nomClasse, // Nom de la classe fourni en paramètre
                Dock = DockStyle.Top, // Positionné en haut du panneau
                TextAlign = ContentAlignment.MiddleCenter, // Texte centré horizontalement et verticalement
                BackColor = Color.Gray,
                ForeColor = Color.White 
            };



            // Création d'une liste pour afficher les attributs de la classe
            ListBox attributsListBox = new ListBox
            {
                Dock = DockStyle.Bottom, // Positionné sous l'étiquette
                Height = 50 // Hauteur fixe pour afficher les attributs
            };



            // Création d'une liste pour afficher les méthodes de la classe
            ListBox methodesListBox = new ListBox
            {
                Dock = DockStyle.Fill, // Occupe l'espace restant dans le panneau
                Height = 50 // Hauteur initiale pour afficher les méthodes
            };



            // Ajout d'un événement pour permettre le déplacement du panneau
            nouvelleClassePanel.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left) // Vérifie que le clic est fait avec le bouton gauche
                {
                    // Stocke la position du clic dans le panneau (utilisé pour calculer le déplacement)
                    nouvelleClassePanel.Tag = new Point(e.X, e.Y);
                }
            };


            nouvelleClassePanel.MouseMove += (s, e) =>
            {
                if (e.Button == MouseButtons.Left && nouvelleClassePanel.Tag is Point startPoint) // Si un déplacement est en cours
                {
                    // Met à jour la position du panneau en fonction du mouvement de la souris
                    nouvelleClassePanel.Left += e.X - startPoint.X;
                    nouvelleClassePanel.Top += e.Y - startPoint.Y;
                }
            };


            // Ajout d'un événement sur l'étiquette pour permettre la modification de la classe
            nomClasseLabel.DoubleClick += (s, e) =>
            {
                // Ouvre une boîte de dialogue ou une logique pour modifier la classe
                ModifierClasse(fenetreTravail, nomClasseLabel);
            };

            // Ajout des éléments (méthodes, attributs, nom de la classe) au panneau
            nouvelleClassePanel.Controls.Add(methodesListBox); 
            nouvelleClassePanel.Controls.Add(attributsListBox); 
            nouvelleClassePanel.Controls.Add(nomClasseLabel);

            // Ajout de l'option de suppression par clic droit
            SupprimerClasse(fenetreTravail, nouvelleClassePanel);


            // Ajout du panneau représentant la classe à la fenêtre de travail
            fenetreTravail.Controls.Add(nouvelleClassePanel);
        }






        /// Permet à l'utilisateur de modifier le nom d'une classe existante via une boîte de dialogue, en double cliquant dessus.
        /// Le nom mis à jour est reflété dans l'interface utilisateur en modifiant l'étiquette associée à la classe.
        private void ModifierClasse(Form fenetreTravail, Label nomClasseLabel)
        {
            // Création de la fenêtre de dialogue pour modifier le nom de la classe
            Form dialogue = new Form
            {
                Text = "Modifier le Nom de la Classe", 
                Size = new Size(300, 150), 
                StartPosition = FormStartPosition.CenterParent, 
                FormBorderStyle = FormBorderStyle.FixedDialog, 
                MinimizeBox = false, // Désactive le bouton de réduction
                MaximizeBox = false  // Désactive le bouton d'agrandissement
            };


            // Ajout d'une étiquette avec une instruction pour l'utilisateur
            Label instructionLabel = new Label
            {
                Text = "Entrez le nouveau nom :",
                Location = new Point(10, 10),
                AutoSize = true // Ajuste la taille automatiquement au texte
            };
            // Ajout de l'étiquette à la fenêtre
            dialogue.Controls.Add(instructionLabel); 


            // Zone de texte pour entrer le nouveau nom, avec le nom actuel pré-rempli
            TextBox nomTextBox = new TextBox
            {
                Location = new Point(10, 40),
                Width = 260,
                Text = nomClasseLabel.Text // Pré-remplir avec le nom actuel
            };
            // Ajout de la zone de texte à la fenêtre
            dialogue.Controls.Add(nomTextBox); 



            // Bouton "Valider" pour enregistrer les modifications
            Button validerButton = new Button
            {
                Text = "Valider",
                Location = new Point(100, 80), 
                DialogResult = DialogResult.OK // Définit le résultat de la boîte de dialogue comme "OK"
            };

            // Gestionnaire d'événement pour le bouton "Valider"
            validerButton.Click += (s, e) =>
            {
                // Vérifie si le champ de texte n'est pas vide
                if (!string.IsNullOrWhiteSpace(nomTextBox.Text))
                {
                    // Met à jour le texte du label avec le nouveau nom
                    nomClasseLabel.Text = nomTextBox.Text;
                    dialogue.Close(); // Ferme la boîte de dialogue
                }
                else
                {
                    // Affiche une alerte si le champ est vide
                    MessageBox.Show("Le nom ne peut pas être vide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            // Ajout du bouton "Valider" à la fenêtre
            dialogue.Controls.Add(validerButton); 



            // Bouton "Annuler" pour fermer la fenêtre sans enregistrer les modifications
            Button annulerButton = new Button
            {
                Text = "Annuler",
                Location = new Point(180, 80),
                DialogResult = DialogResult.Cancel // Définit le résultat de la boîte de dialogue comme "Annuler"
            };
            // Ajout du bouton "Annuler" à la fenêtre
            dialogue.Controls.Add(annulerButton); 


            // Affiche la boîte de dialogue en mode modal (bloque les interactions avec la fenêtre parente)
            dialogue.ShowDialog();
        }






        
        /// Permet de supprimer une classe (représentée par un panel) via un clic droit.
        /// Un menu contextuel s'affiche avec une option pour supprimer la classe sélectionnée.
        private void SupprimerClasse(Form fenetreTravail, Panel classePanel)
        {
            // Crée un menu contextuel (popup) pour la suppression
            ContextMenuStrip menuContextuel = new ContextMenuStrip();

            // Crée un élément de menu "Supprimer" dans le menu contextuel
            ToolStripMenuItem supprimerClasseItem = new ToolStripMenuItem("Supprimer la Classe");

            // Associe un événement au clic sur "Supprimer"
            supprimerClasseItem.Click += (s, e) =>
            {
                // Demande de confirmation à l'utilisateur avant de supprimer
                var confirmation = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cette classe ?",
                    "Confirmation de suppression",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                // Si l'utilisateur confirme, supprime le panel de la fenêtre
                if (confirmation == DialogResult.Yes)
                {
                    fenetreTravail.Controls.Remove(classePanel);
                }
            };

            // Ajoute l'option "Supprimer" au menu contextuel
            menuContextuel.Items.Add(supprimerClasseItem);

            // Associe le menu contextuel au panneau représentant la classe
            classePanel.ContextMenuStrip = menuContextuel;
        }

    }
}
