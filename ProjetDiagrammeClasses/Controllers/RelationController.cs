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


        /// Dessine des lignes de connexion entre les panels de source et cible, avec un panneau de relation au milieu.
        /// Cette méthode est utilisée pour illustrer les relations visuelles entre les différentes classes dans l'interface.
        private void DrawConnectionLines(Form fenetreTravail, Panel sourcePanel, Panel ciblePanel, Panel relationPanel)
        {
            // Crée un objet Graphics pour dessiner sur la fenêtre
            Graphics graphics = fenetreTravail.CreateGraphics();

            // Calcule le point central du panneau source
            Point sourcePoint = new Point(
                sourcePanel.Location.X + sourcePanel.Width / 2, // X: centre horizontal du panneau source
                sourcePanel.Location.Y + sourcePanel.Height / 2 // Y: centre vertical du panneau source
            );

            // Calcule le point central du panneau cible
            Point ciblePoint = new Point(
                ciblePanel.Location.X + ciblePanel.Width / 2, // X: centre horizontal du panneau cible
                ciblePanel.Location.Y + ciblePanel.Height / 2 // Y: centre vertical du panneau cible
            );

            // Calcule le point central du panneau de relation
            Point relationPoint = new Point(
                relationPanel.Location.X + relationPanel.Width / 2, // X: centre horizontal du panneau de relation
                relationPanel.Location.Y + relationPanel.Height / 2 // Y: centre vertical du panneau de relation
            );

            // Crée une ligne entre la source et la relation (première ligne)
            Line line1 = new Line(sourcePoint, relationPoint);
            drawnLines.Add(line1); // Ajoute cette ligne à la liste des lignes dessinées
            DrawLine(graphics, line1); // Dessine cette ligne sur la fenêtre

            // Crée une ligne entre la relation et la cible (deuxième ligne)
            Line line2 = new Line(relationPoint, ciblePoint);
            drawnLines.Add(line2); // Ajoute cette ligne à la liste des lignes dessinées
            DrawLine(graphics, line2); // Dessine cette ligne sur la fenêtre
        }



        // Méthode pour dessiner une ligne
        private void DrawLine(Graphics graphics, Line line)
        {
            graphics.DrawLine(line.Pen, line.Start, line.End);
        }





        /// Met à jour la position du panneau de relation et redessine les lignes de connexion
        /// lorsque les panels des classes source et cible sont déplacés.
        /// Cette méthode permet de maintenir la position de la relation entre les classes
        /// synchronisée avec leurs nouvelles positions.
        private void UpdateRelationPosition(Form fenetreTravail, Panel sourcePanel, Panel ciblePanel, Panel relationPanel)
        {
            // Calcule la nouvelle position X du panneau de relation (centre horizontal entre source et cible)
            int xPosition = (sourcePanel.Location.X + sourcePanel.Width + ciblePanel.Location.X) / 2;

            // Calcule la nouvelle position Y du panneau de relation (centre vertical entre source et cible)
            int yPosition = (sourcePanel.Location.Y + ciblePanel.Location.Y) / 2;

            // Met à jour la position du panneau de relation, avec un léger décalage pour l'ajuster
            relationPanel.Location = new Point(xPosition - 50, yPosition - 25);

            // Invalide et met à jour la fenêtre afin de forcer un redessin
            fenetreTravail.Invalidate();
            fenetreTravail.Update();

            // Crée un objet Graphics pour dessiner sur la fenêtre
            Graphics graphics = fenetreTravail.CreateGraphics();

            // Calcule le point central du panneau source
            Point sourcePoint = new Point(
                sourcePanel.Location.X + sourcePanel.Width / 2, // X: centre horizontal du panneau source
                sourcePanel.Location.Y + sourcePanel.Height / 2 // Y: centre vertical du panneau source
            );

            // Calcule le point central du panneau cible
            Point ciblePoint = new Point(
                ciblePanel.Location.X + ciblePanel.Width / 2, // X: centre horizontal du panneau cible
                ciblePanel.Location.Y + ciblePanel.Height / 2 // Y: centre vertical du panneau cible
            );

            // Calcule le point central du panneau de relation
            Point relationPoint = new Point(
                relationPanel.Location.X + relationPanel.Width / 2, // X: centre horizontal du panneau de relation
                relationPanel.Location.Y + relationPanel.Height / 2 // Y: centre vertical du panneau de relation
            );

            // Crée une ligne entre la source et la relation (première ligne)
            Line line1 = new Line(sourcePoint, relationPoint);

            // Crée une ligne entre la relation et la cible (deuxième ligne)
            Line line2 = new Line(relationPoint, ciblePoint);

            // Efface les anciennes lignes dessinées et ajoute les nouvelles lignes à la collection
            drawnLines.Clear();
            drawnLines.Add(line1);
            drawnLines.Add(line2);

            // Redessine les lignes sur la fenêtre en utilisant les nouvelles positions
            DrawLine(graphics, line1);
            DrawLine(graphics, line2);
        }





        /// Affiche une fenêtre permettant à l'utilisateur de sélectionner deux classes pour établir une relation
        /// entre elles. L'utilisateur peut aussi spécifier le type de relation et son nom.
        public void DemanderClassesPourRelation(Form fenetreTravail)
        {
            // Récupère tous les panels des classes disponibles dans la fenêtre
            var classesPanels = fenetreTravail.Controls.OfType<Panel>()
                .Where(panel => panel.Controls.OfType<Label>().Any() && !string.IsNullOrEmpty(panel.Controls.OfType<Label>().FirstOrDefault()?.Text))
                .ToList();

            // Vérifie qu'il y a au moins deux classes disponibles pour créer une relation
            if (classesPanels.Count < 2)
            {
                MessageBox.Show("Vous devez avoir au moins deux classes pour créer une relation.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crée une nouvelle fenêtre de dialogue pour la sélection des classes et des relations
            Form dialogue = new Form
            {
                Text = "Sélectionner les Classes",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent
            };

            // Crée deux ListBox, une pour la classe source et une pour la classe cible
            ListBox sourceListBox = new ListBox { Dock = DockStyle.Left, Width = 150 };
            ListBox cibleListBox = new ListBox { Dock = DockStyle.Right, Width = 150 };

            // Ajoute le nom de chaque classe aux deux ListBox (source et cible)
            foreach (var panel in classesPanels)
            {
                string nomClasse = panel.Controls.OfType<Label>().FirstOrDefault()?.Text;
                sourceListBox.Items.Add(nomClasse); // Ajoute à la ListBox source
                cibleListBox.Items.Add(nomClasse); // Ajoute à la ListBox cible
            }

            // Crée un champ TextBox pour que l'utilisateur entre le nom de la relation
            TextBox relationNomTextBox = new TextBox
            {
                PlaceholderText = "Nom de la relation",
                Dock = DockStyle.Top
            };

            // Crée un ComboBox pour sélectionner le type de relation (Association, Héritage, Composition, Aggrégation)
            ComboBox typeRelationComboBox = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Association", "Héritage", "Composition", "Aggrégation" }
            };
            typeRelationComboBox.SelectedIndex = 0; // Sélectionne par défaut "Association"

            // Crée un bouton de validation qui permettra de valider le choix de l'utilisateur
            Button validerButton = new Button
            {
                Text = "Valider",
                Dock = DockStyle.Bottom
            };

            // Lors du clic sur le bouton "Valider", on vérifie les sélections et crée la relation
            validerButton.Click += (sender, e) =>
            {
                // Récupère le nom de la relation saisi
                string relationNom = relationNomTextBox.Text;
                if (string.IsNullOrEmpty(relationNom))
                {
                    // Si le nom est vide, affiche un message d'erreur
                    MessageBox.Show("Le nom de la relation est obligatoire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Récupère la classe source et la classe cible sélectionnées dans les ListBox
                string classeSource = sourceListBox.SelectedItem?.ToString();
                string classeCible = cibleListBox.SelectedItem?.ToString();

                // Si les deux classes sont sélectionnées
                if (!string.IsNullOrEmpty(classeSource) && !string.IsNullOrEmpty(classeCible))
                {
                    // Recherche les panels correspondants aux classes sélectionnées
                    var sourcePanel = classesPanels.FirstOrDefault(p => p.Controls.OfType<Label>().FirstOrDefault()?.Text == classeSource);
                    var ciblePanel = classesPanels.FirstOrDefault(p => p.Controls.OfType<Label>().FirstOrDefault()?.Text == classeCible);

                    // Si les panels source et cible sont trouvés
                    if (sourcePanel != null && ciblePanel != null)
                    {
                        // Récupère le type de relation sélectionné, par défaut "Association"
                        string typeRelation = typeRelationComboBox.SelectedItem?.ToString() ?? "Association";

                        // Appelle la méthode pour ajouter la relation entre les classes
                        AjouterRelation(fenetreTravail, relationNom, typeRelation, sourcePanel, ciblePanel);

                        // Ferme le dialogue après l'ajout de la relation
                        dialogue.Close();
                    }
                }
            };

            // Ajoute tous les contrôles (TextBox, ComboBox, ListBox et Button) à la fenêtre de dialogue
            dialogue.Controls.Add(relationNomTextBox);
            dialogue.Controls.Add(typeRelationComboBox);
            dialogue.Controls.Add(sourceListBox);
            dialogue.Controls.Add(cibleListBox);
            dialogue.Controls.Add(validerButton);

            // Affiche la fenêtre de dialogue
            dialogue.ShowDialog();
        }

    }
}
