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

Licensed under the **MIT License**. See [LICENSE](LICENSE) for details.

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
