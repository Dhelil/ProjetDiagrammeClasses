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

            // Menu "Méthode"
            ToolStripMenuItem methodeMenu = new ToolStripMenuItem("Méthode");
            ToolStripMenuItem nouvelleMethodeItem = new ToolStripMenuItem("Nouvelle Méthode");
            nouvelleMethodeItem.Click += (s, e) => MessageBox.Show("Ajout de méthode non encore implémenté.");
            methodeMenu.DropDownItems.Add(nouvelleMethodeItem);

            // Menu "Relation"
            ToolStripMenuItem relationMenu = new ToolStripMenuItem("Relation");
            ToolStripMenuItem nouvelleRelationItem = new ToolStripMenuItem("Nouvelle Relation");
            nouvelleRelationItem.Click += (s, e) => MessageBox.Show("Ajout de relation non encore implémenté.");
            relationMenu.DropDownItems.Add(nouvelleRelationItem);

            menuStrip.Items.Add(classeMenu);
            menuStrip.Items.Add(methodeMenu);
            menuStrip.Items.Add(relationMenu);

            fenetreTravail.MainMenuStrip = menuStrip;
            fenetreTravail.Controls.Add(menuStrip);
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

            // Ajout d'un événement pour ajouter une méthode
            methodesListBox.DoubleClick += (s, e) =>
            {
                AjouterMethode(fenetreTravail, methodesListBox);
            };

            // Événements pour permettre la gestion des attributs/méthodes et le déplacement
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

            nouvelleClassePanel.Click += (s, e) => AjouterAttribut(fenetreTravail, attributsListBox);

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
                    attributsListBox.Items.Add($"{nouvelAttribut.getNom()} : {nouvelAttribut.getType()} ({nouvelAttribut.getVisibilite()})");

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
