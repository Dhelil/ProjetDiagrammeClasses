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
        private List<Panel> lines = new List<Panel>(); // Pour stocker les lignes représentant les relations
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

        // Méthode pour ajouter une relation entre deux classes
        private void AjouterRelation(Form fenetreTravail, string relationNom, string typeRelation, Panel sourcePanel, Panel ciblePanel)
        {
            // Calcul de la position du panneau de relation
            int xPosition = (sourcePanel.Location.X + sourcePanel.Width + ciblePanel.Location.X) / 2;
            int yPosition = (sourcePanel.Location.Y + ciblePanel.Location.Y) / 2;

            Panel relationPanel = new Panel
            {
                Size = new Size(100, 50), // Taille réduite pour le panneau de relation
                Location = new Point(xPosition - 50, yPosition - 25), // Centrer le panneau entre les deux classes
                BackColor = Color.LightBlue,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label relationLabel = new Label
            {
                Text = $"{relationNom} ({typeRelation})", // Afficher le nom de la relation suivi du type
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 8, FontStyle.Regular)
            };

            relationPanel.Controls.Add(relationLabel);
            fenetreTravail.Controls.Add(relationPanel);

            // Dessiner les lignes reliant les classes et le panneau de relation
            DrawConnectionLines(fenetreTravail, sourcePanel, ciblePanel, relationPanel);

            // Mettre à jour les lignes lors du déplacement des classes
            sourcePanel.MouseMove += (s, e) => UpdateRelationPosition(fenetreTravail, sourcePanel, ciblePanel, relationPanel);
            ciblePanel.MouseMove += (s, e) => UpdateRelationPosition(fenetreTravail, sourcePanel, ciblePanel, relationPanel);
        }



        // Méthode pour dessiner les lignes entre les classes et le panneau de relation
        private void DrawConnectionLines(Form fenetreTravail, Panel sourcePanel, Panel ciblePanel, Panel relationPanel)
        {
            Graphics graphics = fenetreTravail.CreateGraphics();

            Point sourcePoint = new Point(sourcePanel.Location.X + sourcePanel.Width / 2, sourcePanel.Location.Y + sourcePanel.Height / 2);
            Point ciblePoint = new Point(ciblePanel.Location.X + ciblePanel.Width / 2, ciblePanel.Location.Y + ciblePanel.Height / 2);
            Point relationPoint = new Point(relationPanel.Location.X + relationPanel.Width / 2, relationPanel.Location.Y + relationPanel.Height / 2);

            Line line1 = new Line(sourcePoint, relationPoint);
            drawnLines.Add(line1);
            DrawLine(graphics, line1);

            Line line2 = new Line(relationPoint, ciblePoint);
            drawnLines.Add(line2);
            DrawLine(graphics, line2);
        }

        // Méthode pour dessiner une ligne
        private void DrawLine(Graphics graphics, Line line)
        {
            graphics.DrawLine(line.Pen, line.Start, line.End);
        }

        // Mettre à jour la position de la relation et des lignes lors du déplacement des classes
        private void UpdateRelationPosition(Form fenetreTravail, Panel sourcePanel, Panel ciblePanel, Panel relationPanel)
        {
            int xPosition = (sourcePanel.Location.X + sourcePanel.Width + ciblePanel.Location.X) / 2;
            int yPosition = (sourcePanel.Location.Y + ciblePanel.Location.Y) / 2;

            relationPanel.Location = new Point(xPosition - 50, yPosition - 25);

            fenetreTravail.Invalidate();
            fenetreTravail.Update();

            Graphics graphics = fenetreTravail.CreateGraphics();

            Point sourcePoint = new Point(sourcePanel.Location.X + sourcePanel.Width / 2, sourcePanel.Location.Y + sourcePanel.Height / 2);
            Point ciblePoint = new Point(ciblePanel.Location.X + ciblePanel.Width / 2, ciblePanel.Location.Y + ciblePanel.Height / 2);
            Point relationPoint = new Point(relationPanel.Location.X + relationPanel.Width / 2, relationPanel.Location.Y + relationPanel.Height / 2);

            Line line1 = new Line(sourcePoint, relationPoint);
            Line line2 = new Line(relationPoint, ciblePoint);

            drawnLines.Clear();
            drawnLines.Add(line1);
            drawnLines.Add(line2);

            DrawLine(graphics, line1);
            DrawLine(graphics, line2);
        }

        public void DemanderClassesPourRelation(Form fenetreTravail)
        {
            var classesPanels = fenetreTravail.Controls.OfType<Panel>()
                .Where(panel => panel.Controls.OfType<Label>().Any() && !string.IsNullOrEmpty(panel.Controls.OfType<Label>().FirstOrDefault()?.Text))
                .ToList();

            if (classesPanels.Count < 2)
            {
                MessageBox.Show("Vous devez avoir au moins deux classes pour créer une relation.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form dialogue = new Form
            {
                Text = "Sélectionner les Classes",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent
            };

            ListBox sourceListBox = new ListBox { Dock = DockStyle.Left, Width = 150 };
            ListBox cibleListBox = new ListBox { Dock = DockStyle.Right, Width = 150 };

            foreach (var panel in classesPanels)
            {
                string nomClasse = panel.Controls.OfType<Label>().FirstOrDefault()?.Text;
                sourceListBox.Items.Add(nomClasse);
                cibleListBox.Items.Add(nomClasse);
            }

            TextBox relationNomTextBox = new TextBox
            {
                PlaceholderText = "Nom de la relation",
                Dock = DockStyle.Top
            };

            ComboBox typeRelationComboBox = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Association", "Héritage", "Composition", "Aggrégation" }
            };
            typeRelationComboBox.SelectedIndex = 0;

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

                string classeSource = sourceListBox.SelectedItem?.ToString();
                string classeCible = cibleListBox.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(classeSource) && !string.IsNullOrEmpty(classeCible))
                {
                    var sourcePanel = classesPanels.FirstOrDefault(p => p.Controls.OfType<Label>().FirstOrDefault()?.Text == classeSource);
                    var ciblePanel = classesPanels.FirstOrDefault(p => p.Controls.OfType<Label>().FirstOrDefault()?.Text == classeCible);

                    if (sourcePanel != null && ciblePanel != null)
                    {
                        string typeRelation = typeRelationComboBox.SelectedItem?.ToString() ?? "Association";
                        AjouterRelation(fenetreTravail, relationNom, typeRelation, sourcePanel, ciblePanel);
                        dialogue.Close();
                    }
                }
            };

            dialogue.Controls.Add(relationNomTextBox);
            dialogue.Controls.Add(typeRelationComboBox);
            dialogue.Controls.Add(sourceListBox);
            dialogue.Controls.Add(cibleListBox);
            dialogue.Controls.Add(validerButton);

            dialogue.ShowDialog();
        }
    }
}
