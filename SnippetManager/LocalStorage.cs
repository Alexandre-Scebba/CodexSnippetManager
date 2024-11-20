using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SnippetManager.Models;

namespace SnippetManager.Data
{
    public class LocalStorage
    {
        private const string FilePath = "snippets.json";

        public void SaveSnippet(Snippet snippet)
        {
            var snippets = LoadSnippets();
            snippets.Add(snippet);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(snippets));
        }

        public List<Snippet> LoadSnippets()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Snippet>>(json) ?? new List<Snippet>();
            }
            return new List<Snippet>();
        }
    }
}