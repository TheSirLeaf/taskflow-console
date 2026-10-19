using TaskFlow.Console.Services;

namespace TaskFlow.Tests;

public class TaskManagerTests
{
    [Fact]
    public void AddTask_Should_Add_Task_When_Title_Is_Valid()
    {
        // Arrange
        var manager = new TaskManager();
        string title = "Estudar C# .NET 10";

        // Act
        var task = manager.AddTask(title);

        // Assert
        Assert.NotNull(task);
        Assert.Equal(title, task.Title);
        Assert.False(task.IsCompleted);
        Assert.Single(manager.Tasks);
    }

    [Fact]
    public void AddTask_Should_Throw_Exception_When_Title_Is_Empty()
    {
        // Arrange
        var manager = new TaskManager();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => manager.AddTask(" "));
    }
}
