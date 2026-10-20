using TaskFlow.Console.Domain;
using TaskFlow.Console.Storage;

namespace TaskFlow.Console.Services;

public class TaskManager
{
    private readonly List<TaskItem> _tasks;
    private readonly ITaskStorage? _storage;

    public TaskManager(ITaskStorage? storage = null)
    {
        _storage = storage;
        _tasks = storage?.Load().ToList() ?? new List<TaskItem>();
    }

    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    public TaskItem AddTask(string title)
    {
        var task = new TaskItem(title);
        _tasks.Add(task);
        Persist();
        return task;
    }

    public TaskItem? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public bool CompleteTask(Guid id)
    {
        var task = GetById(id);
        if (task == null)
        {
            return false;
        }

        task.Complete();
        Persist();
        return true;
    }

    private void Persist()
    {
        _storage?.Save(_tasks);
    }
}
