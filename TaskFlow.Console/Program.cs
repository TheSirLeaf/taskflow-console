using TaskFlow.Console.Services;

var manager = new TaskManager();

Console.WriteLine("=== TaskFlow - Gerenciador de Tarefas ===");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("1 - Adicionar tarefa");
    Console.WriteLine("2 - Listar tarefas");
    Console.WriteLine("3 - Concluir tarefa");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            AddTask(manager);
            break;
        case "2":
            ListTasks(manager);
            break;
        case "3":
            CompleteTask(manager);
            break;
        case "0":
            Console.WriteLine("Encerrando o TaskFlow. Até logo!");
            return;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}

static void AddTask(TaskManager manager)
{
    Console.Write("Título da tarefa: ");
    var title = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("O título da tarefa não pode ser vazio.");
        return;
    }

    var task = manager.AddTask(title);
    Console.WriteLine($"Tarefa adicionada com sucesso (Id: {task.Id}).");
}

static void ListTasks(TaskManager manager)
{
    if (manager.Tasks.Count == 0)
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }

    Console.WriteLine("Tarefas cadastradas:");
    foreach (var task in manager.Tasks)
    {
        var status = task.IsCompleted ? "Concluída" : "Pendente";
        Console.WriteLine($"- [{status}] {task.Title} (Id: {task.Id})");
    }
}

static void CompleteTask(TaskManager manager)
{
    Console.Write("Id da tarefa a concluir: ");
    var input = Console.ReadLine();

    if (!Guid.TryParse(input, out var id))
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    if (manager.CompleteTask(id))
    {
        Console.WriteLine("Tarefa concluída com sucesso.");
    }
    else
    {
        Console.WriteLine("Tarefa não encontrada.");
    }
}
