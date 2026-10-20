using System.Text.Json;
using System.Text.Json.Serialization;
using TaskFlow.API.Models;

namespace TaskFlow.API.Repositories;

public class TarefaRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public TarefaRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<Tarefa> ObterTodos()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Tarefa>();
        }

        var json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Tarefa>();
        }

        return JsonSerializer.Deserialize<List<Tarefa>>(json, _jsonOptions) ?? new List<Tarefa>();
    }

    public Tarefa? ObterPorId(int id)
    {
        return ObterTodos().FirstOrDefault(t => t.Id == id);
    }

    public Tarefa Adicionar(Tarefa tarefa)
    {
        var tarefas = ObterTodos();
        tarefa.Id = tarefas.Count == 0 ? 1 : tarefas.Max(t => t.Id) + 1;
        tarefas.Add(tarefa);
        Salvar(tarefas);
        return tarefa;
    }

    public bool Atualizar(Tarefa tarefa)
    {
        var tarefas = ObterTodos();
        var index = tarefas.FindIndex(t => t.Id == tarefa.Id);
        if (index < 0)
        {
            return false;
        }

        tarefas[index] = tarefa;
        Salvar(tarefas);
        return true;
    }

    public bool Remover(int id)
    {
        var tarefas = ObterTodos();
        var removidas = tarefas.RemoveAll(t => t.Id == id);
        if (removidas == 0)
        {
            return false;
        }

        Salvar(tarefas);
        return true;
    }

    private void Salvar(List<Tarefa> tarefas)
    {
        var json = JsonSerializer.Serialize(tarefas, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}
