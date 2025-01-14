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

