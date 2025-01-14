using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetDiagrammeClasses.Class;
using ProjetDiagrammeClasses.Controller;
using ProjetDiagrammeClasses.Controllers;

namespace ProjetDiagrammeClasses.Controllers
{
    internal class DiagrammeDeClasseController
    {
        ClasseController classController = new ClasseController();

        AttributController attributController = new AttributController();

        MethodeController methodeController = new MethodeController();

        RelationController relationController = new RelationController();

        CardinaliteController cardinaliteController = new CardinaliteController();

        private void InitializeFenetreTravail(Form fenetreTravail)
        {

            Panel sourcePanel = new Panel(); // Crée un panel source
            Panel ciblePanel = new Panel();  // Crée un panel cible


            // Crée une barre de menu principale pour la fenêtre
            MenuStrip menuStrip = new MenuStrip();

            // Création du menu "Classe"
            ToolStripMenuItem classeMenu = new ToolStripMenuItem("Classe");
            ToolStripMenuItem nouvelleClasseItem = new ToolStripMenuItem("Nouvelle Classe");
            nouvelleClasseItem.Click += (s, e) => classController.DemanderNomClasse(fenetreTravail);
            classeMenu.DropDownItems.Add(nouvelleClasseItem);

            // Création du menu "Attribut"
            ToolStripMenuItem attributMenu = new ToolStripMenuItem("Attribut");
            ToolStripMenuItem nouvelAttributItem = new ToolStripMenuItem("Nouvel Attribut");
            nouvelAttributItem.Click += (s, e) => attributController.DemanderClassePourAjouterAttribut(fenetreTravail);
            attributMenu.DropDownItems.Add(nouvelAttributItem);

            // Création du menu "Méthode"
            ToolStripMenuItem methodeMenu = new ToolStripMenuItem("Méthode");
            ToolStripMenuItem nouvelleMethodeItem = new ToolStripMenuItem("Nouvelle Méthode");
            nouvelleMethodeItem.Click += (s, e) => methodeController.DemanderClassePourAjouterMethode(fenetreTravail);
            methodeMenu.DropDownItems.Add(nouvelleMethodeItem);

            // Création du menu "Relation"
            ToolStripMenuItem relationMenu = new ToolStripMenuItem("Relation");
            ToolStripMenuItem nouvelleRelationItem = new ToolStripMenuItem("Nouvelle Relation");
            nouvelleRelationItem.Click += (s, e) => relationController.DemanderClassesPourRelation(fenetreTravail);
            relationMenu.DropDownItems.Add(nouvelleRelationItem);

            // Création du menu "Cardinalité"
            ToolStripMenuItem cardinaliteMenu = new ToolStripMenuItem("Cardinalité");
            ToolStripMenuItem nouvelleCardinaliteItem = new ToolStripMenuItem("Nouvelle Cardinalité");
            nouvelleCardinaliteItem.Click += (s, e) => cardinaliteController.DemanderCardinalite(fenetreTravail); 
            cardinaliteMenu.DropDownItems.Add(nouvelleCardinaliteItem);

            // Ajout des menus principaux à la barre de menu principale
            menuStrip.Items.Add(classeMenu);
            menuStrip.Items.Add(attributMenu);
            menuStrip.Items.Add(methodeMenu);
            menuStrip.Items.Add(relationMenu);
            menuStrip.Items.Add(cardinaliteMenu);  // Ajout du menu Cardinalité

            // Intégration de la barre de menu dans la fenêtre
            fenetreTravail.MainMenuStrip = menuStrip;
            fenetreTravail.Controls.Add(menuStrip);
        }



        public void NouveauMenuItem_Click(object sender, EventArgs e)
        {
            // Crée une nouvelle fenêtre (Form) qui représentera l'espace de travail pour le diagramme de classe
            Form fenetreTravail = new Form
            {
                // Définit le titre de la fenêtre
                Text = "Diagramme de classe",
                // Définit la taille de la fenêtre
                Size = new Size(800, 600)
            };

            // Initialise les éléments de la fenêtre de travail en appelant la méthode InitializeFenetreTravail
            // Cette méthode ajoute une barre de menu avec des options comme Classe, Attribut, Méthode, et Relation
            InitializeFenetreTravail(fenetreTravail);

            // Affiche la fenêtre créée
            fenetreTravail.Show();
        }
    }
}
