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
        
        private Classe ObtenirClasseParNom(string nomClasse)
        {
            return classes.FirstOrDefault(c => c.Nom == nomClasse);
        }

        private void AjouterNouvelleClasse(Form fenetreTravail, string nomClasse)
        {
            // Calcul de la position centrale de la fenêtre
            int x = (fenetreTravail.ClientSize.Width - 150) / 2;
            int y = (fenetreTravail.ClientSize.Height - 100) / 2;

            Panel nouvelleClassePanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(150, 150),
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label nomClasseLabel = new Label
            {
                Text = nomClasse,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Gray,
                ForeColor = Color.White
            };

            ListBox attributsListBox = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 50 // Taille pour les attributs
            };

            ListBox methodesListBox = new ListBox
            {
                Dock = DockStyle.Fill, // Place restante pour les méthodes
                Height = 50
            };

            // Événements pour permettre la gestion des attributs et le déplacement
            nouvelleClassePanel.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    nouvelleClassePanel.Tag = new Point(e.X, e.Y);
                }
            };

            nouvelleClassePanel.MouseMove += (s, e) =>
            {
                if (e.Button == MouseButtons.Left && nouvelleClassePanel.Tag is Point startPoint)
                {
                    nouvelleClassePanel.Left += e.X - startPoint.X;
                    nouvelleClassePanel.Top += e.Y - startPoint.Y;
                }
            };


            nomClasseLabel.DoubleClick += (s, e) => ModifierClasse(fenetreTravail, nomClasseLabel);

            nouvelleClassePanel.Controls.Add(methodesListBox);
            nouvelleClassePanel.Controls.Add(attributsListBox);
            nouvelleClassePanel.Controls.Add(nomClasseLabel);

            fenetreTravail.Controls.Add(nouvelleClassePanel);
        }


        private void ModifierClasse(Form fenetreTravail, Label nomClasseLabel)
        {
            Form dialogue = new Form
            {
                Text = "Modifier le Nom de la Classe",
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label instructionLabel = new Label
            {
                Text = "Entrez le nouveau nom :",
                Location = new Point(10, 10),
                AutoSize = true
            };
            dialogue.Controls.Add(instructionLabel);

            TextBox nomTextBox = new TextBox
            {
                Location = new Point(10, 40),
                Width = 260,
                Text = nomClasseLabel.Text
            };
            dialogue.Controls.Add(nomTextBox);

            Button validerButton = new Button
            {
                Text = "Valider",
                Location = new Point(100, 80),
                DialogResult = DialogResult.OK
            };
            validerButton.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(nomTextBox.Text))
                {
                    nomClasseLabel.Text = nomTextBox.Text;
                    dialogue.Close();
                }
                else
                {
                    MessageBox.Show("Le nom ne peut pas être vide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            dialogue.Controls.Add(validerButton);

            Button annulerButton = new Button
            {
                Text = "Annuler",
                Location = new Point(180, 80),
                DialogResult = DialogResult.Cancel
            };
            dialogue.Controls.Add(annulerButton);

            dialogue.ShowDialog();
        }


    }
}
