# Codex Snippet Manager

> Une application de bureau légère pour organiser, éditer et partager des extraits de code avec coloration syntaxique avancée.

---

## 🚀 Présentation

Codex est une application WPF en C# (.NET 6) qui permet aux développeurs de stocker, catégoriser et retrouver leurs extraits de code en un seul endroit. Objectifs principaux :

* **Accès rapide**: recherchez instantanément dans votre bibliothèque de snippets.
* **Édition enrichie**: éditez vos extraits avec coloration syntaxique complète grâce à AvalonEdit.
* **Import & Export**: sauvegardez ou partagez votre collection au format JSON/CSV.
* **Comptes Utilisateur**: inscription/connexion simple pour isoler les bibliothèques personnelles.

---

## 🔑 Fonctionnalités Clés

* **Tableau de bord**
  • Affiche vos catégories de snippets et les ajouts récents.
* **Créer & Éditer des Snippets**
  • Titre, étiquette de langue, description et éditeur de code avec coloration syntaxique.
* **Inscription / Connexion**
  • Authentification basique (nom d’utilisateur/mot de passe) pour isoler les bibliothèques.
* **Recherche & Filtrage**
  • Recherche en temps réel par titre, langage ou tags.
* **Import & Export**
  • Exportez votre bibliothèque au format JSON ou CSV.• Importez des fichiers existants pour remplir rapidement la base de données.
* **Coloration Syntaxique**
  • Gérée par AvalonEdit pour C#, JavaScript, Python, et plus.

## 🔧 Stack Technique

* **Framework**: .NET 6 (WPF)
* **Éditeur UI**: AvalonEdit (coloration syntaxique)
* **Stockage de Données**: SQLite (base locale)
* **ORM**: Entity Framework Core
* **Authentification**: nom d’utilisateur/mot de passe personnalisé stocké dans SQLite (hashé)
* **Compilation & Packaging**: MSBuild / WiX Toolset (installateur optionnel)

## ⚙️ Installation & Utilisation

1. **Cloner ou Télécharger**
```
git clone https://github.com/Alexandre-Scebba/snippet-codex.git
cd snippet-codex
```
2. **Ouvrir dans Visual Studio 2022**
   • Assurez-vous que le Runtime .NET 6 Desktop est installé.
3. **Restaurer & Compiler**
```
dotnet restore
dotnet build
```
**Exécuter l’Application**
  • Appuyez sur F5 dans Visual Studio ou lancez `SnippetCodex.exe` depuis `bin\Debug\net6.0-windows`.
**Configuration Initiale**
  • Créez un nouveau compte via l’écran de connexion.
  • La base SQLite (`snippets.db`) se créera automatiquement dans le dossier de l’application.

---

## 🚧 Défis Connus & Solutions

* **Mise à jour dynamique**
  • La liste des snippets se rafraîchit en direct lors de l’ajout ou de la modification — implémenté via le suivi des changements d’EF Core.
* **Erreurs de champs vides**
  • Validation pour éviter les titres ou corps de code vides.
* **Coloration syntaxique**
  • AvalonEdit peut ralentir sur les très gros fichiers — optimisé en limitant le rendu des polices sur les blocs volumineux.

---

## 🧪 Leçons Apprises

* **Tests Unitaires**
  • Couvre les opérations CRUD de snippets et la validation de connexion.
* **Intégration AvalonEdit**
  • Personnalisation des règles de coloration pour plusieurs langages.
* **Injection de Dépendances**
  • Configuration du DbContext EF Core et des services dans une application WPF via Microsoft.Extensions.Hosting.

  ---

## 🔭 Travaux Futurs

* **Synchronisation Cloud**
  • Synchronisez vos snippets avec Azure Cosmos DB ou un autre stockage cloud.
* **Organisation par Tags**
  • Glisser-déposer des tags et catégories hiérarchiques.
* **Partage de Snippets**
  • Générer des URLs publiques ou des exports Gist pour chaque snippet.
* **Multi-Plateforme**
  • Créer une application web ou mobile compagnon pour accéder à la bibliothèque sur le pouce.
* **Stockage Chiffré**
  • Chiffrer la base SQLite locale pour plus de sécurité.

---

## 👥 Équipe

* **Michael Rourke** – Responsable Front-End & Correcteur de Bugs
* **Dimitri Teolis** – Chef de Projet & Testeur Principal
* **Alexandre Scebba** – Architecte Backend & Spécialiste Intégration

---

## 📜 Licence

Ce projet est sous **licence MIT**.

## ⭐ Contribution

1. **Forkez** le dépôt sur GitHub

2. **Créez** une branche de fonctionnalité :
```
git checkout -b feature/<VotreFeature>
```
3. **Commit & Push**:
```
git commit -m "Ajout <feature> / correction <bug>"
git push origin feature/<VotreFeature>
```
4. **Ouvrez** une Pull Request vers `main`

5. **Intégrez* les retours et fusionnez une fois approuvé!

Merci pour vos contributions !

-------------------------------------------------------------------------------------------------------------------------
#ENG:

# Codex Snippet Manager

> A lightweight desktop app for organizing, editing, and sharing code snippets with rich syntax highlighting.

---

## 🚀 Overview

Codex is a C# WPF application (using .NET 6) that lets developers store, categorize, and retrieve code snippets in one place. Key goals:

* **Quick Access**: Instantly search your snippet library.
* **Rich Editing**: Edit snippets with full syntax highlighting via AvalonEdit.
* **Import & Export**: Backup or share your collection as JSON/CSV.
* **User Accounts**: Simple login/register to keep personal libraries separate.

---

## 🔑 Key Features

* **Dashboard**
  • Displays your snippet categories and recent additions.
* **Create & Edit Snippets**
  • Title, language tag, description, and code editor with syntax coloring.
* **Login / Register**
  • Basic user authentication (username/password) to isolate personal libraries.
* **Search & Filter**
  • Real-time searching by title, language, or tags.
* **Import & Export**
  • Export your snippet library to JSON or CSV.
  • Import existing snippet files to quickly populate your database.
* **Syntax Highlighting**
  • Powered by AvalonEdit for C#, JavaScript, Python, and more.

---

## 🔧 Tech Stack

* **Framework**: .NET 6 (WPF)
* **UI Editor**: AvalonEdit (syntax highlighting)
* **Data Storage**: SQLite (local file-based DB)
* **ORM**: Entity Framework Core
* **Authentication**: Custom username/password stored in SQLite (hashed)
* **Build & Packaging**: MSBuild / WiX Toolset (optional installer)

---

## ⚙️ Installation & Usage

1. **Clone or Download**

   ```bash
   git clone https://github.com/Alexandre-Scebba/snippet-codex.git
   cd snippet-codex
   ```
2. **Open in Visual Studio 2022**
   • Ensure .NET 6 Desktop Runtime is installed.
3. **Restore & Build**

   ```bash
   dotnet restore
   dotnet build
   ```
4. **Run the App**
   • Press F5 in Visual Studio or launch `SnippetCodex.exe` from `bin\Debug\net6.0-windows`.
5. **First-Time Setup**
   • Register a new account via the login screen.
   • Your SQLite DB (`snippets.db`) will be created automatically in the app folder.

---

## 🚧 Known Challenges & Solutions

* **Dynamic Updating**
  • Snippet list refreshes live when adding or editing—implemented via EF Core change tracking.
* **Null Field Errors**
  • Validations prevent empty titles or code bodies.
* **Syntax Highlighting**
  • AvalonEdit may lag with very large files—optimized by limiting font rendering on huge text blocks.

---

## 🧪 What We Learned

* **Unit Tests**
  • Covers create/edit/delete snippet operations and login validations.
* **AvalonEdit Integration**
  • Customizing highlighting rules for multiple languages.
* **Dependency Injection**
  • Configured EF Core DbContext and services in a WPF app using Microsoft.Extensions.Hosting.

---

## 🔭 Future Work

* **Cloud Sync**
  • Sync your snippets to Azure Cosmos DB or another cloud store.
* **Tag-Based Organization**
  • Drag-and-drop tags and hierarchical categories.
* **Snippet Sharing**
  • Generate public URLs or Gist exports for individual snippets.
* **Multi-Platform**
  • Build a companion web or mobile app to access your library on the go.
* **Encrypted Storage**
  • Encrypt the local SQLite DB for extra security.

---

## 👥 Meet the Team

* **Michael Rourke** – Front-End Lead & Bug Wrangler
* **Dimitri Teolis** – Team Lead & Test Commander
* **Alexandre Scebba** – Backend Architect & Integration Specialist

---

## 📜 License

Licensed under the **MIT License**.

---

## ⭐ Contributing

1. **Fork** the repo
2. **Create** a branch:

   ```bash
   git checkout -b feature/<YourFeature>
   ```
3. **Commit & Push**:

   ```bash
   git commit -m "Add <feature> / fix <bug>"
   git push origin feature/<YourFeature>
   ```
4. **Open** a Pull Request against `main`.
5. Address feedback and merge once approved.

Thank you for helping make Codex even better!
