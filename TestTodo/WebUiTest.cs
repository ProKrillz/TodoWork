using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace TestTodo
{
    public class WebUiTest
    {
        private string _address = "https://localhost:7138/";

        public static List<Object[]> _webDrivers = new List<object[]>
        {
            new object[] { () => new ChromeDriver() },
            new object[] { () => new EdgeDriver() }  
        };
        [Theory]
        [MemberData(nameof(_webDrivers))]
        public void TestLoadWebpage(Func<IWebDriver> Setup)
        {
            using IWebDriver webDriver = Setup();

            webDriver.Navigate().GoToUrl(_address);

            Assert.Equal("Hjem - TodoWork", webDriver.Title);
        }
        [Theory]
        [MemberData(nameof(_webDrivers))]
        public void TestLoginUi(Func<IWebDriver> Setup)
        {
            using IWebDriver webDriver = Setup();

            webDriver.Navigate().GoToUrl(_address);

            Login(webDriver);

            Assert.Equal("https://localhost:7138/UserIndex", webDriver.Url);
        }
        [Theory]
        [MemberData(nameof(_webDrivers))]
        public void TestCreateTodoUi(Func<IWebDriver> Setup)
        {
            using IWebDriver webDriver = Setup();

            webDriver.Navigate().GoToUrl(_address);

            Login(webDriver);
            Thread.Sleep(2000);

            var createBtn = webDriver.FindElement(By.XPath("/html/body/div/main/header/nav/div/div/ul/li[2]/a"));
            createBtn.Click();
            Thread.Sleep(2000);

            var titleInput = webDriver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div/form/div[1]/input[2]"));
            var descriptionInput = webDriver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div/form/div[1]/textarea"));
            var submitBtn = webDriver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div/form/div[2]/button"));

            titleInput.SendKeys("Test");
            descriptionInput.SendKeys("TestTest");
            submitBtn.Click();
            Thread.Sleep(2000);

            var titelLabel = webDriver.FindElement(By.XPath("/html/body/div/main/details[2]/form/div/div/div[1]/h5"));
            var descriptionDiv = webDriver.FindElement(By.XPath("/html/body/div/main/details[2]/form/div/div/div[1]/div[1]"));
            var deleteBtn = webDriver.FindElement(By.XPath("/html/body/div/main/details[2]/form/div/div/div[2]/a[2]"));

            Assert.Equal("Test", titelLabel.Text);
            Assert.Equal("TestTest", descriptionDiv.Text);
            
            deleteBtn.Click();
            Thread.Sleep(2000);
            var comfirmDeleteBtn = webDriver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div/form/div[2]/button"));
            comfirmDeleteBtn.Click();
            Thread.Sleep(1000);
        }

        public void Login(IWebDriver webDriver) {
            var usernameInput = webDriver.FindElement(By.XPath("/html/body/div/main/div[1]/form/input[1]"));
            var loginInput = webDriver.FindElement(By.XPath("/html/body/div/main/div[1]/form/input[2]"));
            var submitButton = webDriver.FindElement(By.XPath("/html/body/div/main/div[1]/form/div/button"));

            usernameInput.SendKeys("test@test.dk");
            loginInput.SendKeys("linkin");
            submitButton.Submit();
        }

    }
}
