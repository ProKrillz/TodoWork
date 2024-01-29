using Moq;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;
using TodoWork.Domain.Entities;
using TodoWork.Domain.SQLConnection;

namespace TestTodo
{
    public class UnitTest1
    {        
        [Fact]
        public async void TestCreateUser()
        {
            var mock = new Mock<IConnection>();

            DTOUser dtoUser = new DTOUser { Id = Guid.NewGuid(), Name = "Thomas", Email = "mail@mail.dk", Password = "P@ssw0rd" };
            mock.Setup(l => l.CreateUserAsync(dtoUser)).Returns(Task.CompletedTask);

            TodoServices service = new TodoServices(mock.Object);

            await service.CreateUserAsync(dtoUser);

            mock.Verify();
        }
        [Fact]
        public async void TestUpdateUser()
        {
            var mock = new Mock<IConnection>();
            DTOUser dtoUser = new DTOUser { Id = Guid.NewGuid(), Name = "Thomas", Email = "mail@mail.dk", Password = "P@ssw0rd" };
            mock.Setup(l => l.UpdateUserAsync(dtoUser)).Returns(Task.CompletedTask);

            TodoServices service = new TodoServices(mock.Object);
            await service.CreateUserAsync(dtoUser);

            mock.Verify();
        }
    }
}