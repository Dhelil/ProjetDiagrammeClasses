using ProjetDiagrammeClasses.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjetDiagrammeClasses.Controller
{
    public class CardinaliteController
    {
        private static List<Relation> relations; // Liste des relations partagée entre toutes les instances
        private Relation uneRelation;

        public CardinaliteController()
        {
            // Initialiser la liste des relations si elle est null
            if (relations == null)
            {
                relations = new List<Relation>();
            }
        }

        // Méthode pour ajouter une cardinalité à une relation existante
        public void AjouterCardinalite(Form fenetre, string relationNom, string cardinalite)
        {
            // Vérifie si une relation avec ce nom existe déjà dans la fenêtre
            var relationPanel = fenetre.Controls.OfType<Panel>()
            .FirstOrDefault(panel => panel.Controls.OfType<Label>().Any() &&
                                     panel.Controls.OfType<Label>().FirstOrDefault() != null &&
                                     panel.Controls.OfType<Label>().FirstOrDefault()?.Text.Contains(relationNom) == true);

            if (relationPanel == null)
            {
                MessageBox.Show("Relation introuvable. Impossible d'ajouter une cardinalité.");
                return;
            }

            // Calculer la position pour afficher la cardinalité à côté de la relation (similaire à la relation)
            Point cardinalitePosition = new Point(relationPanel.Location.X + relationPanel.Width / 2 - 25, relationPanel.Location.Y + relationPanel.Height / 2);

            // Créer un panneau pour afficher la cardinalité
            Panel cardinalitePanel = new Panel
            {
                Size = new Size(80, 30), // Taille pour la cardinalité
                Location = cardinalitePosition,
                BackColor = Color.LightYellow, // Couleur pour différencier
                BorderStyle = BorderStyle.FixedSingle
            };

            // Créer un label pour afficher le texte de la cardinalité
            Label cardinaliteLabel = new Label
            {
                Text = cardinalite, // Afficher la cardinalité choisie
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 8, FontStyle.Regular)
            };

            // Ajouter le label au panneau de cardinalité
            cardinalitePanel.Controls.Add(cardinaliteLabel);
            fenetre.Controls.Add(cardinalitePanel);

            // Mettre à jour l'affichage lorsque la relation est déplacée
            relationPanel.MouseMove += (s, e) => UpdateCardinalitePosition(fenetre, relationPanel, cardinalitePanel);
        }


        // Méthode pour mettre à jour la position de la cardinalité
        private void UpdateCardinalitePosition(Form fenetre, Panel relationPanel, Panel cardinalitePanel)
        {
            // Recalculer la position du panneau de cardinalité en fonction de la relation
            Point newPosition = new Point(relationPanel.Location.X + relationPanel.Width / 2 - cardinalitePanel.Width / 2, relationPanel.Location.Y + relationPanel.Height / 2);
            cardinalitePanel.Location = newPosition;
        }



        // Méthode pour demander à l'utilisateur de choisir une cardinalité et l'ajouter à la relation
        public void DemanderCardinalite(Form fenetre)
        {
            // Trouve les relations existantes ou les objets pour lesquelles ajouter une cardinalité
            var relationsPanels = fenetre.Controls.OfType<Panel>()
                .Where(panel => panel.Controls.OfType<Label>().Any() && !string.IsNullOrEmpty(panel.Controls.OfType<Label>().FirstOrDefault()?.Text))
                .ToList();

            if (relationsPanels.Count == 0)
            {
                MessageBox.Show("Aucune relation n'a été trouvée. Vous devez avoir au moins une relation pour ajouter une cardinalité.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Création de la fenêtre de dialogue
            Form dialogue = new Form
            {
                Text = "Choisir la Cardinalité",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent
            };

            // Liste pour les relations existantes
            ListBox relationsListBox = new ListBox { Dock = DockStyle.Top, Height = 150 };

            foreach (var panel in relationsPanels)
            {
                string relationNom = panel.Controls.OfType<Label>().FirstOrDefault()?.Text;
                relationsListBox.Items.Add(relationNom);
            }

            // ComboBox pour choisir la cardinalité
            ComboBox cardinaliteComboBox = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "1..1", "1..n", "n..n" }
            };
            cardinaliteComboBox.SelectedIndex = 0;

            // Bouton de validation
            Button validerButton = new Button
            {
                Text = "Valider",
                Dock = DockStyle.Bottom
            };

            validerButton.Click += (sender, e) =>
            {
                // Récupère la relation sélectionnée et la cardinalité choisie
                string relationNom = relationsListBox.SelectedItem?.ToString();
                string cardinaliteChoisie = cardinaliteComboBox.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(relationNom) || string.IsNullOrEmpty(cardinaliteChoisie))
                {
                    MessageBox.Show("Veuillez sélectionner une relation et une cardinalité.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Trouve la relation à laquelle ajouter la cardinalité
                var relationPanel = relationsPanels.FirstOrDefault(p => p.Controls.OfType<Label>().FirstOrDefault()?.Text == relationNom);

                if (relationPanel != null)
                {
                    // Ajoute la cardinalité à la relation
                    AjouterCardinalite(fenetre, relationNom, cardinaliteChoisie);
                    dialogue.Close();
                }
            };

            // Ajoute les contrôles à la fenêtre de dialogue
            dialogue.Controls.Add(relationsListBox);
            dialogue.Controls.Add(cardinaliteComboBox);
            dialogue.Controls.Add(validerButton);

            // Affiche la fenêtre de dialogue
            dialogue.ShowDialog();
        }
    }
}
