using ProjetDiagrammeClasses.Class;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetDiagrammeClasses
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeMenuPrincipal();
        }

        private void InitializeMenuPrincipal()
        {
            MenuStrip menuStrip = new MenuStrip();

            ToolStripMenuItem fichierMenu = new ToolStripMenuItem("Fichier");
            ToolStripMenuItem nouveauMenuItem = new ToolStripMenuItem("Nouveau");
            nouveauMenuItem.Click += NouveauMenuItem_Click;

            fichierMenu.DropDownItems.Add(nouveauMenuItem);
            menuStrip.Items.Add(fichierMenu);

            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void NouveauMenuItem_Click(object sender, EventArgs e)
        {
            Form fenetreTravail = new Form
            {
                Text = "Diagramme de classe",
                Size = new Size(800, 600)
            };

            InitializeFenetreTravail(fenetreTravail);
            fenetreTravail.Show();
        }

        private void InitializeFenetreTravail(Form fenetreTravail)
        {
            MenuStrip menuStrip = new MenuStrip();

            // Menu "Classe"
            ToolStripMenuItem classeMenu = new ToolStripMenuItem("Classe");
            ToolStripMenuItem nouvelleClasseItem = new ToolStripMenuItem("Nouvelle Classe");
            nouvelleClasseItem.Click += (s, e) => DemanderNomClasse(fenetreTravail);
            classeMenu.DropDownItems.Add(nouvelleClasseItem);

            // Menu "Attribut"
            ToolStripMenuItem attributMenu = new ToolStripMenuItem("Attribut");
            ToolStripMenuItem nouvelAttributItem = new ToolStripMenuItem("Nouvel Attribut");
            nouvelAttributItem.Click += (s, e) => DemanderClassePourAjouterAttribut(fenetreTravail);
            attributMenu.DropDownItems.Add(nouvelAttributItem);

            // Menu "Méthode"
            ToolStripMenuItem methodeMenu = new ToolStripMenuItem("Méthode");
            ToolStripMenuItem nouvelleMethodeItem = new ToolStripMenuItem("Nouvelle Méthode");
            nouvelleMethodeItem.Click += (s, e) => DemanderClassePourAjouterMethode(fenetreTravail);
            methodeMenu.DropDownItems.Add(nouvelleMethodeItem);

            // Menu "Relation"
            ToolStripMenuItem relationMenu = new ToolStripMenuItem("Relation");
            ToolStripMenuItem nouvelleRelationItem = new ToolStripMenuItem("Nouvelle Relation");
            nouvelleRelationItem.Click += (s, e) => MessageBox.Show("Ajout de relation non encore implémenté.");
            relationMenu.DropDownItems.Add(nouvelleRelationItem);

            menuStrip.Items.Add(classeMenu);
            menuStrip.Items.Add(attributMenu);
            menuStrip.Items.Add(methodeMenu);
            menuStrip.Items.Add(relationMenu);

            fenetreTravail.MainMenuStrip = menuStrip;
            fenetreTravail.Controls.Add(menuStrip);
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form
                {
                    Width = 400,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

                Label textLabel = new Label { Left = 20, Top = 20, Text = text, Width = 340 };
                TextBox textBox = new TextBox { Left = 20, Top = 50, Width = 340 };
                Button confirmation = new Button { Text = "OK", Left = 270, Width = 90, Top = 100, DialogResult = DialogResult.OK };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);

                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
            }
        }


        private void DemanderClassePourAjouterAttribut(Form fenetreTravail)
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




        private void DemanderClassePourAjouterMethode(Form fenetreTravail)
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


        private void DemanderNomClasse(Form fenetreTravail)
        {
            Form dialogue = new Form
            {
                Text = "Nom de la Classe",
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label instructionLabel = new Label
            {
                Text = "Entrez le nom de la classe :",
                Location = new Point(10, 10),
                AutoSize = true
            };

            dialogue.Controls.Add(instructionLabel);

            TextBox nomTextBox = new TextBox
            {
                Location = new Point(10, 40),
                Width = 260
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
                    AjouterNouvelleClasse(fenetreTravail, nomTextBox.Text);
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


        // Méthode permettant de modifier le nom d'une classe
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

    }
}
