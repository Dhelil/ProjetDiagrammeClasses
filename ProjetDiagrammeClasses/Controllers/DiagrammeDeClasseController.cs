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

        private void InitializeFenetreTravail(Form fenetreTravail)
        {

            // Crée une barre de menu principale pour la fenêtre
            MenuStrip menuStrip = new MenuStrip();

            // Création du menu "Classe"
            // Crée un élément de menu principal intitulé "Classe"
            ToolStripMenuItem classeMenu = new ToolStripMenuItem("Classe");
            // Crée un sous-menu intitulé "Nouvelle Classe"
            ToolStripMenuItem nouvelleClasseItem = new ToolStripMenuItem("Nouvelle Classe");
            // Associe un événement au clic sur "Nouvelle Classe"
            nouvelleClasseItem.Click += (s, e) => classController.DemanderNomClasse(fenetreTravail);
            // Ajoute le sous-menu "Nouvelle Classe" au menu "Classe"
            classeMenu.DropDownItems.Add(nouvelleClasseItem);



            // Création du menu "Attribut"
            // Crée un élément de menu principal intitulé "Attribut"
            ToolStripMenuItem attributMenu = new ToolStripMenuItem("Attribut");
            // Crée un sous-menu intitulé "Nouvel Attribut"
            ToolStripMenuItem nouvelAttributItem = new ToolStripMenuItem("Nouvel Attribut");
            // Associe un événement au clic sur "Nouvel Attribut"
            nouvelAttributItem.Click += (s, e) => attributController.DemanderClassePourAjouterAttribut(fenetreTravail);
            // Ajoute le sous-menu "Nouvel Attribut" au menu "Attribut"
            attributMenu.DropDownItems.Add(nouvelAttributItem);



            // Création du menu "Méthode"
            // Crée un élément de menu principal intitulé "Méthode"
            ToolStripMenuItem methodeMenu = new ToolStripMenuItem("Méthode");
            // Crée un sous-menu intitulé "Nouvelle Méthode"
            ToolStripMenuItem nouvelleMethodeItem = new ToolStripMenuItem("Nouvelle Méthode");
            // Associe un événement au clic sur "Nouvelle Méthode"
            nouvelleMethodeItem.Click += (s, e) => methodeController.DemanderClassePourAjouterMethode(fenetreTravail);
            // Ajoute le sous-menu "Nouvelle Méthode" au menu "Méthode"
            methodeMenu.DropDownItems.Add(nouvelleMethodeItem);



            // Création du menu "Relation"
            // Crée un élément de menu principal intitulé "Relation"
            ToolStripMenuItem relationMenu = new ToolStripMenuItem("Relation");
            // Crée un sous-menu intitulé "Nouvelle Relation"
            ToolStripMenuItem nouvelleRelationItem = new ToolStripMenuItem("Nouvelle Relation");
            // Associe un événement au clic sur "Nouvelle Relation"
            // Affiche un message indiquant que la fonctionnalité n'est pas encore implémentée
            nouvelleRelationItem.Click += (s, e) => MessageBox.Show("Ajout de relation non encore implémenté.");
            // Ajoute le sous-menu "Nouvelle Relation" au menu "Relation"
            relationMenu.DropDownItems.Add(nouvelleRelationItem);



            // Ajout des menus principaux à la barre de menu principale
            menuStrip.Items.Add(classeMenu);
            menuStrip.Items.Add(attributMenu);
            menuStrip.Items.Add(methodeMenu);
            menuStrip.Items.Add(relationMenu);



            // Intégration de la barre de menu dans la fenêtre
            // Définit la barre de menu principale pour la fenêtre donnée
            fenetreTravail.MainMenuStrip = menuStrip;
            // Ajoute la barre de menu aux contrôles de la fenêtre
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
