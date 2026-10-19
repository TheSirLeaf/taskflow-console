using TaskFlow.Console.Domain;

namespace TaskFlow.Console.Services;

public class TaskManager
{
    private readonly List<TaskItem> _tasks = new();

    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    public TaskItem AddTask(string title)
    {
        var task = new TaskItem(title);
        _tasks.Add(task);
        return task;
    }

    public TaskItem GetById(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            throw new KeyNotFoundException($"Tarefa com o Id '{id}' não encontrada.");
        }

        return task;
    }

    public bool CompleteTask(Guid id)
    {
        var task = GetById(id);
        task.Complete();
        return true;
    }
}
