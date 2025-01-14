using ProjetDiagrammeClasses.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjetDiagrammeClasses.Controllers
{
    internal class RelationController
    {
        private Panel relationPanel;
        private List<Panel> lines = new List<Panel>();  // Pour stocker les lignes représentant les relations
        private List<Line> drawnLines = new List<Line>(); // Liste des lignes dessinées pour les relations

        // Structure pour stocker les lignes dessinées entre les classes
        private class Line
        {
            public Point Start { get; set; }
            public Point End { get; set; }
            public Pen Pen { get; set; }

            public Line(Point start, Point end)
            {
                Start = start;
                End = end;
                Pen = new Pen(Color.Black, 2); // Couleur noire et épaisseur de ligne 2
            }
        }

        // Méthode pour ajouter une relation dans un panneau dédié entre les deux classes
        private void AjouterRelation(Form fenetreTravail, string relationNom, string typeRelation, Panel sourcePanel, Panel ciblePanel)
        {
            // Créer et configurer le panneau de relation (petit carré)
            int xPosition = (sourcePanel.Location.X + sourcePanel.Width + ciblePanel.Location.X) / 2;
            int yPosition = (sourcePanel.Location.Y + ciblePanel.Location.Y) / 2;

            relationPanel = new Panel
            {
                Size = new Size(100, 50), // Taille réduite pour la relation
                Location = new Point(xPosition - 50, yPosition - 25), // Centrer le panneau entre les deux classes
                BackColor = Color.LightBlue,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Ajouter une étiquette pour afficher la relation avec le type entre parenthèses
            Label relationLabel = new Label
            {
                Text = $"{relationNom} ({typeRelation})", // Afficher le nom de la relation suivi du type entre parenthèses
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 8, FontStyle.Regular)
            };

            relationPanel.Controls.Add(relationLabel);
            fenetreTravail.Controls.Add(relationPanel); // Ajouter le panneau de relation à la fenêtre principale

            // Dessiner les traits reliant les deux classes à la relation
            DrawConnectionLines(fenetreTravail, sourcePanel, ciblePanel, relationPanel);

            // Ajouter des événements de déplacement pour mettre à jour les relations et les lignes
            sourcePanel.MouseMove += (s, e) => UpdateRelationPosition(fenetreTravail, sourcePanel, ciblePanel);
            ciblePanel.MouseMove += (s, e) => UpdateRelationPosition(fenetreTravail, sourcePanel, ciblePanel);
        }

        // Méthode pour dessiner des lignes entre les classes et la relation
        private void DrawConnectionLines(Form fenetreTravail, Panel sourcePanel, Panel ciblePanel, Panel relationPanel)
        {
            // Créer un objet Graphics pour dessiner sur le panneau de la fenêtre
            Graphics graphics = fenetreTravail.CreateGraphics();

            // Calcul des points de départ et d'arrivée des lignes
            Point sourcePoint = new Point(sourcePanel.Location.X + sourcePanel.Width / 2, sourcePanel.Location.Y + sourcePanel.Height / 2);
            Point ciblePoint = new Point(ciblePanel.Location.X + ciblePanel.Width / 2, ciblePanel.Location.Y + ciblePanel.Height / 2);
            Point relationPoint = new Point(relationPanel.Location.X + relationPanel.Width / 2, relationPanel.Location.Y + relationPanel.Height / 2);

            // Ajouter la ligne entre la source et la relation
            Line line1 = new Line(sourcePoint, relationPoint);
            drawnLines.Add(line1);
            DrawLine(graphics, line1);

            // Ajouter la ligne entre la relation et la cible
            Line line2 = new Line(relationPoint, ciblePoint);
            drawnLines.Add(line2);
            DrawLine(graphics, line2);
        }

        // Méthode pour dessiner une ligne donnée
        private void DrawLine(Graphics graphics, Line line)
        {
            graphics.DrawLine(line.Pen, line.Start, line.End);
        }

        // Méthode pour mettre à jour la position de la relation et des lignes lors du déplacement des classes
        private void UpdateRelationPosition(Form fenetreTravail, Panel sourcePanel, Panel ciblePanel)
        {
            // Recalculer la position du panneau de relation entre les deux classes
            int xPosition = (sourcePanel.Location.X + sourcePanel.Width + ciblePanel.Location.X) / 2;
            int yPosition = (sourcePanel.Location.Y + ciblePanel.Location.Y) / 2;

            relationPanel.Location = new Point(xPosition - 50, yPosition - 25);

            // Effacer les anciennes lignes avant de redessiner
            foreach (var line in drawnLines)
            {
                line.Pen.Dispose();
            }
            drawnLines.Clear();

            // Effacer les anciennes relations sur le formulaire
            fenetreTravail.Invalidate(); // Réinitialiser le graphique
            fenetreTravail.Update(); // Forcer la mise à jour de l'affichage graphique

            // Redessiner les lignes après le déplacement
            Graphics graphics = fenetreTravail.CreateGraphics();
            Point sourcePoint = new Point(sourcePanel.Location.X + sourcePanel.Width / 2, sourcePanel.Location.Y + sourcePanel.Height / 2);
            Point ciblePoint = new Point(ciblePanel.Location.X + ciblePanel.Width / 2, ciblePanel.Location.Y + ciblePanel.Height / 2);
            Point relationPoint = new Point(relationPanel.Location.X + relationPanel.Width / 2, relationPanel.Location.Y + relationPanel.Height / 2);

            // Ajouter la ligne entre la source et la relation
            Line line1 = new Line(sourcePoint, relationPoint);
            drawnLines.Add(line1);
            DrawLine(graphics, line1);

            // Ajouter la ligne entre la relation et la cible
            Line line2 = new Line(relationPoint, ciblePoint);
            drawnLines.Add(line2);
            DrawLine(graphics, line2);
        }

        public void DemanderClassesPourRelation(Form fenetreTravail)
        {
            // Récupérer toutes les classes (panels) dans la fenêtre de travail
            var classesPanels = fenetreTravail.Controls.OfType<Panel>()
                .Where(panel => panel.Controls.OfType<Label>().Any())
                .ToList();

            if (classesPanels.Count < 2)
            {
                MessageBox.Show("Vous devez avoir au moins deux classes pour créer une relation.",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fenêtre pour sélectionner les classes
            Form dialogue = new Form
            {
                Text = "Sélectionner les Classes",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            // ListBox pour sélectionner les classes source et cible
            ListBox sourceListBox = new ListBox { Dock = DockStyle.Left, Width = 150 };
            ListBox cibleListBox = new ListBox { Dock = DockStyle.Right, Width = 150 };

            foreach (var panel in classesPanels)
            {
                var nomClasseLabel = panel.Controls.OfType<Label>().FirstOrDefault();
                if (nomClasseLabel != null)
                {
                    sourceListBox.Items.Add(nomClasseLabel.Text);
                    cibleListBox.Items.Add(nomClasseLabel.Text);
                }
            }

            // Champs pour le nom de la relation
            TextBox relationNomTextBox = new TextBox
            {
                PlaceholderText = "Nom de la relation (verbe)",
                Dock = DockStyle.Top
            };

            // Combobox pour sélectionner le type de relation
            ComboBox typeRelationComboBox = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Association", "Héritage", "Composition", "Aggrégation" }
            };
            typeRelationComboBox.SelectedIndex = 0;

            // Bouton pour valider la relation
            Button validerButton = new Button
            {
                Text = "Valider",
                Dock = DockStyle.Bottom
            };
            validerButton.Click += (sender, e) =>
            {
                string relationNom = relationNomTextBox.Text;
                if (string.IsNullOrEmpty(relationNom))
                {
                    MessageBox.Show("Le nom de la relation est obligatoire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (sourceListBox.SelectedItem != null && cibleListBox.SelectedItem != null)
                {
                    string classeSource = sourceListBox.SelectedItem.ToString();
                    string classeCible = cibleListBox.SelectedItem.ToString();

                    Panel sourcePanel = classesPanels.FirstOrDefault(panel => panel.Controls.OfType<Label>().FirstOrDefault()?.Text == classeSource);
                    Panel ciblePanel = classesPanels.FirstOrDefault(panel => panel.Controls.OfType<Label>().FirstOrDefault()?.Text == classeCible);

                    if (sourcePanel != null && ciblePanel != null)
                    {
                        AjouterRelation(fenetreTravail, relationNom, typeRelationComboBox.Text, sourcePanel, ciblePanel);
                        dialogue.Close();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la sélection des classes.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            };

            // Ajouter les contrôles au dialogue
            dialogue.Controls.Add(sourceListBox);
            dialogue.Controls.Add(cibleListBox);
            dialogue.Controls.Add(relationNomTextBox);
            dialogue.Controls.Add(typeRelationComboBox);
            dialogue.Controls.Add(validerButton);
            dialogue.ShowDialog();
        }
    }
}
