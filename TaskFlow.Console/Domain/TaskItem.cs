using System.Text.Json.Serialization;

namespace TaskFlow.Console.Domain;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }

    public TaskItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("O titulo da tarefa não pode ser vazio ou nulo.", nameof(title));
        }

        Id = Guid.NewGuid();
        Title = title.Trim();
        IsCompleted = false;
    }

    [JsonConstructor]
    private TaskItem(Guid id, string title, bool isCompleted)
    {
        Id = id;
        Title = title;
        IsCompleted = isCompleted;
    }

    public void Complete()
    {
        IsCompleted = true;
    }
}
