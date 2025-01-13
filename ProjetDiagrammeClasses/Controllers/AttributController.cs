using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetDiagrammeClasses.Class;
using static ProjetDiagrammeClasses.Form1;

namespace ProjetDiagrammeClasses.Controllers
{
    internal class AttributController
    {
       private List<Classe> classes = new List<Classe>();


        private Classe ObtenirClasseParNom(string nomClasse)
        {
            return classes.FirstOrDefault(c => c.Nom == nomClasse);
        }

        public void AjouterAttribut(string nomClasse, string nomAttribut, string typeAttribut)
        {
            Classe classe = ObtenirClasseParNom(nomClasse);
            if (classe != null)
            {
                // Si nécessaire, adapte le constructeur d'Attribut pour inclure 'type' comme string
                classe.Attributs.Add(new Attribut(nomAttribut, Attribut.TypeE.String, Attribut.VisibiliteE.Private));
            }
            else
            {
                //MessageBox.Show($"La classe '{nomClasse}' n'existe pas.");
            }
        }

        private void AjouterAttribut(Form fenetreTravail, ListBox attributsListBox)
        {
            Form dialogue = new Form
            {
                Text = "Ajouter un Attribut",
                Size = new Size(300, 250),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label instructionLabel = new Label
            {
                Text = "Entrez le nom et le type de l'attribut :",
                Location = new Point(10, 10),
                AutoSize = true
            };
            dialogue.Controls.Add(instructionLabel);

            TextBox nomTextBox = new TextBox
            {
                Location = new Point(10, 40),
                Width = 260,
                PlaceholderText = "Nom de l'attribut"
            };
            dialogue.Controls.Add(nomTextBox);

            // Création du ComboBox pour les types
            Label typeLabel = new Label
            {
                Text = "Sélectionnez le type :",
                Location = new Point(10, 80),
                AutoSize = true
            };
            dialogue.Controls.Add(typeLabel);

            ComboBox typeComboBox = new ComboBox
            {
                Location = new Point(10, 100),
                Width = 260,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Remplir le ComboBox avec les types d'attributs
            typeComboBox.Items.AddRange(Enum.GetNames(typeof(Attribut.TypeE)));
            typeComboBox.SelectedIndex = 0; // Sélectionner le premier type par défaut
            dialogue.Controls.Add(typeComboBox);

            // Création du ComboBox pour les visibilités
            Label visibiliteLabel = new Label
            {
                Text = "Sélectionnez la visibilité :",
                Location = new Point(10, 140),
                AutoSize = true
            };
            dialogue.Controls.Add(visibiliteLabel);

            ComboBox visibiliteComboBox = new ComboBox
            {
                Location = new Point(10, 160),
                Width = 260,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Remplir le ComboBox avec les options de visibilité
            visibiliteComboBox.Items.AddRange(Enum.GetNames(typeof(Attribut.VisibiliteE)));
            visibiliteComboBox.SelectedIndex = 0; // Sélectionner la visibilité par défaut
            dialogue.Controls.Add(visibiliteComboBox);

            Button validerButton = new Button
            {
                Text = "Valider",
                Location = new Point(100, 180),
                DialogResult = DialogResult.OK
            };
            validerButton.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(nomTextBox.Text))
                {
                    // Création de l'attribut avec les données saisies
                    string nomAttribut = nomTextBox.Text;
                    Attribut.TypeE typeAttribut = (Attribut.TypeE)Enum.Parse(typeof(Attribut.TypeE), typeComboBox.SelectedItem.ToString());
                    Attribut.VisibiliteE visibiliteAttribut = (Attribut.VisibiliteE)Enum.Parse(typeof(Attribut.VisibiliteE), visibiliteComboBox.SelectedItem.ToString());

                    Attribut nouvelAttribut = new Attribut(nomAttribut, typeAttribut, visibiliteAttribut);

                    // Ajouter l'attribut à la liste des attributs de la classe
                    attributsListBox.Items.Add($"{nouvelAttribut.nom} : {nouvelAttribut.Type} ({nouvelAttribut.Visibilite})");

                    dialogue.Close();
                }
                else
                {
                    MessageBox.Show("Le nom de l'attribut est obligatoire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            dialogue.Controls.Add(validerButton);

            Button annulerButton = new Button
            {
                Text = "Annuler",
                Location = new Point(180, 180),
                DialogResult = DialogResult.Cancel
            };
            dialogue.Controls.Add(annulerButton);

            dialogue.ShowDialog();
        }

        public void DemanderClassePourAjouterAttribut(Form fenetreTravail)
        {
            // Obtenir les panels de classes dans la fenêtre de travail
            var classesPanels = fenetreTravail.Controls.OfType<Panel>()
                .Where(panel => panel.Controls.OfType<Label>().Any())
                .ToList();

            if (!classesPanels.Any())
            {
                MessageBox.Show("Aucune classe n'est disponible. Veuillez créer une classe avant d'ajouter un attribut.",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Créer une fenêtre pour sélectionner une classe
            Form dialogue = new Form
            {
                Text = "Sélectionner une Classe",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            ListBox classesListBox = new ListBox
            {
                Dock = DockStyle.Fill
            };

            foreach (var panel in classesPanels)
            {
                var nomClasseLabel = panel.Controls.OfType<Label>().FirstOrDefault();
                if (nomClasseLabel != null)
                {
                    classesListBox.Items.Add(nomClasseLabel.Text); // Ajouter le nom de la classe à la liste
                }
            }

            Button validerButton = new Button
            {
                Text = "Valider",
                Dock = DockStyle.Bottom
            };

            validerButton.Click += (s, e) =>
            {
                if (classesListBox.SelectedItem != null)
                {
                    string nomClasse = classesListBox.SelectedItem.ToString();
                    var classePanel = classesPanels.FirstOrDefault(panel =>
                        panel.Controls.OfType<Label>().Any(label => label.Text == nomClasse));
                    var attributsListBox = classePanel?.Controls.OfType<ListBox>().FirstOrDefault();

                    // Si la classe a des attributs
                    if (attributsListBox != null)
                    {
                        // Demander l'attribut à ajouter
                        string nomAttribut = Prompt.ShowDialog("Entrez le nom de l'attribut :", "Ajouter Attribut");
                        string typeAttribut = Prompt.ShowDialog("Entrez le type de l'attribut (Int, Float, Char, String) :", "Ajouter Attribut");

                        if (!string.IsNullOrEmpty(nomAttribut) && !string.IsNullOrEmpty(typeAttribut))
                        {
                            AjouterAttribut(nomClasse, nomAttribut, typeAttribut); // Ajouter l'attribut à la classe
                            attributsListBox.Items.Add($"{nomAttribut} : {typeAttribut}"); // Afficher l'attribut dans la liste de la classe
                            MessageBox.Show($"Attribut '{nomAttribut} : {typeAttribut}' ajouté à la classe '{nomClasse}'.");
                            dialogue.Close(); // Fermer le dialogue
                        }
                        else
                        {
                            MessageBox.Show("Le nom ou le type de l'attribut est vide.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une classe.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            dialogue.Controls.Add(classesListBox);
            dialogue.Controls.Add(validerButton);
            dialogue.ShowDialog();
        }
    }
}

