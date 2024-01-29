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
            await service.UpdateUserAsync(dtoUser);

            mock.VerifyAll();
            
        }
        [Fact]
        public async void TestUpdateUser2()
        {
            var mock = new Mock<IConnection>();
            DTOUser dtoUser = new DTOUser { Id = Guid.NewGuid(), Name = "Thomas", Email = "mail@mail.dk", Password = "P@ssw0rd" };
            TodoServices service = new TodoServices(mock.Object);

            await service.UpdateUserAsync(dtoUser);

            mock.Verify(repo => repo.UpdateUserAsync(It.Is<DTOUser>(u => u.Id == dtoUser.Id && u.Name == dtoUser.Name)), Times.Once);

        }
        [Fact]
        public async void TestGetUser()
        {
            var mock = new Mock<IConnection>();
            DTOUser dtoUser = new DTOUser { Id = Guid.NewGuid(), Name = "Thomas", Email = "mail@mail.dk", Password = "P@ssw0rd" };
            mock.Setup(l => l.GetUserByEmailAsync("mail@mail.dk")).ReturnsAsync(dtoUser);
            TodoServices service = new TodoServices(mock.Object);

            DTOUser foundUser = await service.GetUserByEmailAsync(dtoUser.Email);

            Assert.NotNull(foundUser);
            Assert.True(foundUser.Id == dtoUser.Id);

            mock.Verify(r => r.GetUserByEmailAsync(dtoUser.Email), Times.Once);
        }
        [Fact]
        public async void TestDeleteUser()
        {
            var mock = new Mock<IConnection>();
            DTOUser dtoUser = new DTOUser { Id = Guid.NewGuid(), Name = "Thomas", Email = "mail@mail.dk", Password = "P@ssw0rd" };
            mock.Setup(l => l.DeleteUserAsync(dtoUser.Id)).Returns(Task.CompletedTask);
            TodoServices service = new TodoServices(mock.Object);

            await service.DeleteUserAsync(dtoUser.Id);

            mock.Verify();
        }
        [Fact]
        public async void TestLogin()
        {
            var mock = new Mock<IConnection>();
            DTOUser dtoUser = new DTOUser { Id = Guid.NewGuid(), Name = "Thomas", Email = "mail@mail.dk", Password = "P@ssw0rd" };
            mock.Setup(l => l.UserLoginAsync(dtoUser.Email, dtoUser.Password)).ReturnsAsync(dtoUser);
            TodoServices service = new TodoServices(mock.Object);

            DTOUser foundUser = await service.UserLoginAsync(dtoUser.Email, dtoUser.Password);

            Assert.NotNull(foundUser);
            Assert.True(dtoUser.Id == foundUser.Id);

            mock.Verify();

        }
    }
}