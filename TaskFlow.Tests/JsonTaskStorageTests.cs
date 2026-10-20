using TaskFlow.Console.Services;
using TaskFlow.Console.Storage;

namespace TaskFlow.Tests;

public class JsonTaskStorageTests : IDisposable
{
    private readonly string _filePath = Path.Combine(
        Path.GetTempPath(),
        $"taskflow-test-{Guid.NewGuid():N}.json");

    [Fact]
    public void Load_Should_Return_Empty_When_File_Does_Not_Exist()
    {
        // Arrange
        var storage = new JsonTaskStorage(_filePath);

        // Act
        var tasks = storage.Load();

        // Assert
        Assert.Empty(tasks);
    }

    [Fact]
    public void Save_And_Load_Should_RoundTrip_Tasks()
    {
        // Arrange
        var storage = new JsonTaskStorage(_filePath);
        var manager = new TaskManager(storage);
        var added = manager.AddTask("Tarefa persistida");
        manager.CompleteTask(added.Id);

        // Act
        var reloaded = new TaskManager(storage);

        // Assert
        var task = Assert.Single(reloaded.Tasks);
        Assert.Equal(added.Id, task.Id);
        Assert.Equal("Tarefa persistida", task.Title);
        Assert.True(task.IsCompleted);
    }

    public void Dispose()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }
    }
}
