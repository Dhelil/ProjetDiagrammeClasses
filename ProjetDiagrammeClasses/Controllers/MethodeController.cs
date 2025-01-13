using ProjetDiagrammeClasses.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDiagrammeClasses.Controllers
{
    internal class MethodeController
    {

        private void AjouterMethode(Form fenetreTravail, ListBox methodesListBox)
        {
            using (Form methodeForm = new Form())
            {
                methodeForm.Text = "Ajouter une méthode";
                methodeForm.Size = new Size(300, 250);

                TextBox nomTextBox = new TextBox { PlaceholderText = "Nom de la méthode", Dock = DockStyle.Top };
                TextBox typeTextBox = new TextBox { PlaceholderText = "Type de retour", Dock = DockStyle.Top };
                TextBox parametresTextBox = new TextBox { PlaceholderText = "Paramètres (séparés par des virgules)", Dock = DockStyle.Top };

                Button ajouterButton = new Button { Text = "Ajouter", Dock = DockStyle.Bottom };
                ajouterButton.Click += (s, e) =>
                {
                    string nom = nomTextBox.Text;
                    string type = typeTextBox.Text;
                    string parametres = parametresTextBox.Text;

                    if (!string.IsNullOrWhiteSpace(nom) && !string.IsNullOrWhiteSpace(type))
                    {
                        // Créer une instance de la classe Methode
                        var nouvelleMethode = new Methode(nom, type, parametres.Split(',').Select(p => p.Trim()).ToList());

                        // Ajouter la méthode à la ListBox
                        methodesListBox.Items.Add($"{nouvelleMethode.nom}({string.Join(", ", nouvelleMethode.parametres)}) : {nouvelleMethode.type}");

                        methodeForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                methodeForm.Controls.Add(nomTextBox);
                methodeForm.Controls.Add(typeTextBox);
                methodeForm.Controls.Add(parametresTextBox);
                methodeForm.Controls.Add(ajouterButton);

                methodeForm.ShowDialog();
            }
        }


        public void DemanderClassePourAjouterMethode(Form fenetreTravail)
        {
            // Récupérer toutes les classes (panels) dans la fenêtre de travail
            var classesPanels = fenetreTravail.Controls.OfType<Panel>()
                .Where(panel => panel.Controls.OfType<Label>().Any())
                .ToList();

            if (!classesPanels.Any())
            {
                MessageBox.Show("Aucune classe n'est disponible. Veuillez créer une classe avant d'ajouter une méthode.",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Création d'une fenêtre
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
                    classesListBox.Items.Add(nomClasseLabel.Text);
                }
            }

            // Création du bouton Valider
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
                    var methodesListBox = classePanel?.Controls.OfType<ListBox>().LastOrDefault();

                    if (methodesListBox != null)
                    {
                        AjouterMethode(fenetreTravail, methodesListBox);
                        dialogue.Close();
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
