using TaskFlow.Console.Domain;

namespace TaskFlow.Console.Storage;

/// <summary>
/// Contrato de persistência das tarefas. Implementações podem guardar os dados
/// em memória, em arquivo JSON ou em qualquer outro meio.
/// </summary>
public interface ITaskStorage
{
    IReadOnlyList<TaskItem> Load();

    void Save(IEnumerable<TaskItem> tasks);
}
