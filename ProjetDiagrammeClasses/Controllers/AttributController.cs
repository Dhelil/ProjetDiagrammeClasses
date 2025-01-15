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

    
        /// Ajoute un nouvel attribut à une classe spécifique, si celle-ci existe.
        public void AjouterAttribut(string nomClasse, string nomAttribut, string typeAttribut)
        {
            // Recherche une instance de la classe en fonction de son nom
            Classe classe = ObtenirClasseParNom(nomClasse);

            // Vérifie si la classe a été trouvée
            if (classe != null)
            {
                // Ajoute un nouvel attribut à la liste des attributs de la classe
                // Le constructeur d'Attribut est appelé avec :
                // - Le nom de l'attribut (nomAttribut)
                // - Un type d'attribut par défaut (Attribut.TypeE.String, ici considéré comme un exemple)
                // - Une visibilité par défaut (Attribut.VisibiliteE.Private, ici considéré comme privé)
                classe.Attributs.Add(new Attribut(nomAttribut, Attribut.TypeE.String, Attribut.VisibiliteE.Private));
            }
            else
            {
                // Si la classe n'existe pas, une action pourrait être effectuée ici, par exemple :
                // - Afficher un message d'erreur
                // - Lancer une exception
                //MessageBox.Show($"La classe '{nomClasse}' n'existe pas.");
            }
        }





        /// Ouvre une fenêtre modale permettant à l'utilisateur de sélectionner une classe et d'ajouter un attribut à cette classe.
        /// Vérifie que le type de l'attribut soit l'un des types valides : Int, Float, Char, String.
        public void DemanderClassePourAjouterAttribut(Form fenetreTravail)
        {
            // Récupérer tous les panels qui représentent des classes dans la fenêtre de travail
            var classesPanels = fenetreTravail.Controls.OfType<Panel>()
                .Where(panel => panel.Controls.OfType<Label>().Any()) // S'assurer que le panel contient un Label (nom de la classe)
                .ToList();

            // Si aucune classe n'est disponible, afficher un message d'erreur
            if (!classesPanels.Any())
            {
                MessageBox.Show("Aucune classe n'est disponible. Veuillez créer une classe avant d'ajouter un attribut.",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sortir de la méthode si aucune classe n'est trouvée
            }

            // Créer une nouvelle fenêtre pour sélectionner une classe
            Form dialogue = new Form
            {
                Text = "Sélectionner une Classe", // Titre de la fenêtre
                Size = new Size(300, 200), // Taille de la fenêtre
                StartPosition = FormStartPosition.CenterParent, // Centrer la fenêtre par rapport à la fenêtre parente
                FormBorderStyle = FormBorderStyle.FixedDialog, // Style de bordure fixe pour la fenêtre
                MinimizeBox = false, // Désactiver la minimisation de la fenêtre
                MaximizeBox = false // Désactiver la maximisation de la fenêtre
            };

            // Créer une ListBox pour afficher les classes disponibles
            ListBox classesListBox = new ListBox
            {
                Dock = DockStyle.Fill // Remplir toute la fenêtre
            };

            // Ajouter les noms des classes à la ListBox
            foreach (var panel in classesPanels)
            {
                var nomClasseLabel = panel.Controls.OfType<Label>().FirstOrDefault(); // Obtenir le label contenant le nom de la classe
                if (nomClasseLabel != null)
                {
                    classesListBox.Items.Add(nomClasseLabel.Text); // Ajouter le nom de la classe à la liste
                }
            }

            // Créer un bouton "Valider" pour confirmer la sélection de la classe
            Button validerButton = new Button
            {
                Text = "Valider", // Texte du bouton
                Dock = DockStyle.Bottom // Placer le bouton en bas de la fenêtre
            };

            // Gestion de l'événement Click du bouton "Valider"
            validerButton.Click += (s, e) =>
            {
                // Si une classe a été sélectionnée
                if (classesListBox.SelectedItem != null)
                {
                    string nomClasse = classesListBox.SelectedItem.ToString(); // Récupérer le nom de la classe sélectionnée
                    var classePanel = classesPanels.FirstOrDefault(panel =>
                        panel.Controls.OfType<Label>().Any(label => label.Text == nomClasse)); // Trouver le panel de la classe sélectionnée
                    var attributsListBox = classePanel?.Controls.OfType<ListBox>().FirstOrDefault(); // Trouver la ListBox des attributs de la classe

                    // Si la classe possède une ListBox pour les attributs
                    if (attributsListBox != null)
                    {
                        // Demander le nom de l'attribut à ajouter via une fenêtre de dialogue
                        string nomAttribut = Prompt.ShowDialog("Entrez le nom de l'attribut :", "Ajouter Attribut");
                        string typeAttribut = Prompt.ShowDialog("Entrez le type de l'attribut (Int, Float, Char, String) :", "Ajouter Attribut");

                        // Vérifier que le type d'attribut est valide
                        if (string.IsNullOrEmpty(nomAttribut) || string.IsNullOrEmpty(typeAttribut))
                        {
                            MessageBox.Show("Le nom ou le type de l'attribut est vide.");
                            return; // Sortir si le nom ou le type est vide
                        }

                        // Liste des types valides
                        var typesValidés = new HashSet<string> { "Int", "Float", "Char", "String" };

                        // Vérifier si le type de l'attribut est valide
                        if (!typesValidés.Contains(typeAttribut))
                        {
                            MessageBox.Show("Le type de l'attribut doit être l'un des suivants : Int, Float, Char, String.",
                                            "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Sortir si le type est invalide
                        }

                        // Ajouter l'attribut à la classe via la méthode AjouterAttribut
                        AjouterAttribut(nomClasse, nomAttribut, typeAttribut);

                        // Afficher l'attribut dans la ListBox de la classe
                        attributsListBox.Items.Add($"{nomAttribut} : {typeAttribut}");

                        // Afficher un message de confirmation
                        MessageBox.Show($"Attribut '{nomAttribut} : {typeAttribut}' ajouté à la classe '{nomClasse}'.");

                        // Fermer la fenêtre de sélection de classe
                        dialogue.Close();
                    }
                }
                else
                {
                    // Si aucune classe n'est sélectionnée, afficher un message d'erreur
                    MessageBox.Show("Veuillez sélectionner une classe.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Ajouter la ListBox et le bouton "Valider" à la fenêtre de dialogue
            dialogue.Controls.Add(classesListBox);
            dialogue.Controls.Add(validerButton);

            // Afficher la fenêtre de dialogue en mode modale (bloque l'interaction avec la fenêtre parente)
            dialogue.ShowDialog();
        }


    }
}

