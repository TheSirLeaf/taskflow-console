using System.Text.Json;
using TaskFlow.Console.Domain;

namespace TaskFlow.Console.Storage;

/// <summary>
/// Persiste as tarefas em um arquivo JSON. Um caminho vazio mantém tudo em memória.
/// </summary>
public class JsonTaskStorage : ITaskStorage
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public string FilePath { get; }

    public JsonTaskStorage(string filePath)
    {
        FilePath = filePath;
    }

    public IReadOnlyList<TaskItem> Load()
    {
        if (string.IsNullOrWhiteSpace(FilePath) || !File.Exists(FilePath))
        {
            return new List<TaskItem>();
        }

        var json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<TaskItem>();
        }

        return JsonSerializer.Deserialize<List<TaskItem>>(json, _options) ?? new List<TaskItem>();
    }

    public void Save(IEnumerable<TaskItem> tasks)
    {
        if (string.IsNullOrWhiteSpace(FilePath))
        {
            return;
        }

        var directory = Path.GetDirectoryName(FilePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonSerializer.Serialize(tasks, _options);
        File.WriteAllText(FilePath, json);
    }
}
