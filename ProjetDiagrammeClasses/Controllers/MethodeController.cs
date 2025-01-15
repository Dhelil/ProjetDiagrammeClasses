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

        /// Permet à l'utilisateur d'ajouter une méthode à une classe sélectionnée.
        /// Une fenêtre modale est affichée pour que l'utilisateur entre le nom, le type de retour et les paramètres de la méthode.
        /// La méthode est ensuite ajoutée à une liste affichée dans l'interface utilisateur.
        private void AjouterMethode(Form fenetreTravail, ListBox methodesListBox)
        {
            // Création d'une nouvelle fenêtre (Form) modale pour ajouter une méthode
            using (Form methodeForm = new Form())
            {
                // Propriétés de la fenêtre
                methodeForm.Text = "Ajouter une méthode"; 
                methodeForm.Size = new Size(300, 250);


                // Zone de texte pour entrer le nom de la méthode
                TextBox nomTextBox = new TextBox
                {
                    PlaceholderText = "Nom de la méthode",
                    Dock = DockStyle.Top                 // Alignement en haut
                };


                // Zone de texte pour entrer le type de retour de la méthode
                TextBox typeTextBox = new TextBox
                {
                    PlaceholderText = "Type de retour",
                    Dock = DockStyle.Top                 // Alignement en haut
                };


                // Zone de texte pour entrer les paramètres de la méthode
                TextBox parametresTextBox = new TextBox
                {
                    PlaceholderText = "Paramètres (séparés par des virgules)",
                    Dock = DockStyle.Top                                      // Alignement en haut
                };


                // Bouton pour valider et ajouter la méthode
                Button ajouterButton = new Button
                {
                    Text = "Ajouter",           
                    Dock = DockStyle.Bottom     // Alignement en bas
                };



                // Gestionnaire d'événements pour le clic sur le bouton "Ajouter"
                ajouterButton.Click += (s, e) =>
                {
                    // Récupère les valeurs saisies par l'utilisateur
                    string nom = nomTextBox.Text;                // Nom de la méthode
                    string type = typeTextBox.Text;              // Type de retour de la méthode
                    string parametres = parametresTextBox.Text;  // Paramètres (chaîne de caractères)

                    // Vérifie que les champs obligatoires (nom et type) ne sont pas vides
                    if (!string.IsNullOrWhiteSpace(nom) && !string.IsNullOrWhiteSpace(type))
                    {
                        // Crée une instance de la classe Methode (assumant qu'une telle classe existe)
                        var nouvelleMethode = new Methode(
                            nom,                                       // Nom de la méthode
                            type,                                      // Type de retour
                            parametres.Split(',').Select(p => p.Trim()).ToList() // Liste des paramètres (séparés par des virgules)
                        );

                        // Ajoute la méthode sous forme de texte formaté à la ListBox
                        methodesListBox.Items.Add($"{nouvelleMethode.nom}({string.Join(", ", nouvelleMethode.parametres)}) : {nouvelleMethode.type}");

                        // Ferme la fenêtre une fois que l'ajout est réussi
                        methodeForm.Close();
                    }
                    else
                    {
                        // Affiche un message d'erreur si des champs obligatoires sont vides
                        MessageBox.Show(
                            "Veuillez remplir tous les champs.",    // Message à afficher
                            "Erreur",                               // Titre de la fenêtre du message
                            MessageBoxButtons.OK,                   // Boutons d'action (OK uniquement)
                            MessageBoxIcon.Warning                  // Icône de type avertissement
                        );
                    }
                };

                // Ajout des contrôles (zones de texte et bouton) à la fenêtre
                methodeForm.Controls.Add(nomTextBox);
                methodeForm.Controls.Add(typeTextBox);
                methodeForm.Controls.Add(parametresTextBox);
                methodeForm.Controls.Add(ajouterButton);

                // Affiche la fenêtre de manière modale (l'utilisateur doit interagir avec elle avant de retourner à la fenêtre principale)
                methodeForm.ShowDialog();
            }
        }




        /// Permet à l'utilisateur de sélectionner une classe existante pour y ajouter une méthode.
        /// Une fenêtre modale affiche la liste des classes disponibles, et l'utilisateur choisit celle à modifier.
        public void DemanderClassePourAjouterMethode(Form fenetreTravail)
        {
            // Récupère tous les panneaux (représentant des classes) présents dans la fenêtre de travail
            var classesPanels = fenetreTravail.Controls.OfType<Panel>()
                .Where(panel => panel.Controls.OfType<Label>().Any()) // Filtre les panels ayant au moins un Label (nom de classe)
                .ToList();

            // Si aucune classe n'est trouvée, affiche un message d'erreur et arrête l'exécution
            if (!classesPanels.Any())
            {
                MessageBox.Show(
                    "Aucune classe n'est disponible. Veuillez créer une classe avant d'ajouter une méthode.", // Message d'erreur
                    "Erreur",                // Titre de la boîte de dialogue
                    MessageBoxButtons.OK,    // Boutons de la boîte de dialogue
                    MessageBoxIcon.Warning   // Icône d'avertissement
                );
                return; // Arrête l'exécution de la méthode
            }

            // Création d'une fenêtre modale pour afficher la liste des classes
            Form dialogue = new Form
            {
                Text = "Sélectionner une Classe",        // Titre de la fenêtre
                Size = new Size(300, 200),              // Taille de la fenêtre
                StartPosition = FormStartPosition.CenterParent, // Centre la fenêtre par rapport à la fenêtre parente
                FormBorderStyle = FormBorderStyle.FixedDialog,  // Style de bordure fixe
                MinimizeBox = false,                    // Désactive le bouton de minimisation
                MaximizeBox = false                     // Désactive le bouton de maximisation
            };

            // Création d'une ListBox pour afficher la liste des classes
            ListBox classesListBox = new ListBox
            {
                Dock = DockStyle.Fill // Prend tout l'espace disponible dans la fenêtre
            };

            // Ajout des noms de classes à la ListBox
            foreach (var panel in classesPanels)
            {
                var nomClasseLabel = panel.Controls.OfType<Label>().FirstOrDefault(); // Récupère le premier Label du panel
                if (nomClasseLabel != null)
                {
                    classesListBox.Items.Add(nomClasseLabel.Text); // Ajoute le texte du Label (nom de la classe) à la ListBox
                }
            }

            // Création du bouton "Valider"
            Button validerButton = new Button
            {
                Text = "Valider",        // Texte du bouton
                Dock = DockStyle.Bottom  // Positionné en bas de la fenêtre
            };

            // Gestionnaire d'événements pour le clic sur le bouton "Valider"
            validerButton.Click += (s, e) =>
            {
                // Vérifie qu'un élément est sélectionné dans la ListBox
                if (classesListBox.SelectedItem != null)
                {
                    // Récupère le nom de la classe sélectionnée
                    string nomClasse = classesListBox.SelectedItem.ToString();

                    // Trouve le panel correspondant à la classe sélectionnée
                    var classePanel = classesPanels.FirstOrDefault(panel =>
                        panel.Controls.OfType<Label>().Any(label => label.Text == nomClasse));

                    // Récupère la ListBox des méthodes de la classe sélectionnée
                    var methodesListBox = classePanel?.Controls.OfType<ListBox>().LastOrDefault();

                    // Si une ListBox pour les méthodes est trouvée, ouvre le formulaire pour ajouter une méthode
                    if (methodesListBox != null)
                    {
                        AjouterMethode(fenetreTravail, methodesListBox); // Appelle la méthode pour ajouter une méthode
                        dialogue.Close(); // Ferme la fenêtre de sélection
                    }
                }
                else
                {
                    // Si aucune classe n'est sélectionnée, affiche un message d'erreur
                    MessageBox.Show(
                        "Veuillez sélectionner une classe.", // Message d'erreur
                        "Erreur",                            // Titre de la boîte de dialogue
                        MessageBoxButtons.OK,                // Boutons de la boîte de dialogue
                        MessageBoxIcon.Warning               // Icône d'avertissement
                    );
                }
            };

            // Ajoute la ListBox et le bouton Valider à la fenêtre
            dialogue.Controls.Add(classesListBox);
            dialogue.Controls.Add(validerButton);

            // Affiche la fenêtre en mode modale
            dialogue.ShowDialog();
        }

    }
}
