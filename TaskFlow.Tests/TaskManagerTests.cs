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

    [Fact]
    public void GetById_Should_Return_Null_When_Task_Does_Not_Exist()
    {
        // Arrange
        var manager = new TaskManager();

        // Act
        var task = manager.GetById(Guid.NewGuid());

        // Assert
        Assert.Null(task);
    }

    [Fact]
    public void CompleteTask_Should_Return_False_When_Task_Does_Not_Exist()
    {
        // Arrange
        var manager = new TaskManager();

        // Act
        var completed = manager.CompleteTask(Guid.NewGuid());

        // Assert
        Assert.False(completed);
    }
}
