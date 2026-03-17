using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace NotesTests
{
    public class LoginPage : IDisposable
    {
        public IWebDriver _EdgeDriver = new EdgeDriver();

        public const int Sleep = 1000;

        public const string AuthSubmitId = "authSubmit";
        public const string AuthUsernameId = "authUsername";
        public const string AuthPasswordId = "authPassword";
        public const string LoginTabId = "loginTab";
        public const string RegisterTabId = "registerTab";
        public const string NextPageTabId = "welcomeText";
        
        public const string HeaderXPath = "//*[@id=\"mainView\"]/header/div[1]/h1";
        public const string ErrorMessageXPath = "//*[@id=\"message\"]/span";

        public const string LoginUser = "qwerty";
        public const string PasswordUser = "123456789";
        public const string WrongInput = "zxc123";

        public void Dispose()
        {
            _EdgeDriver.Dispose();
        }

        // стартовое состояние приложения;
        [Fact]
        public void CorrectWebStart()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";
            IWebElement find = _EdgeDriver.FindElement(By.XPath(HeaderXPath));

            Assert.Equal("📝 Сервис заметок", find.Text);
        }

        // тест доступность основных элементов начального экрана
        [Theory]
        [InlineData(AuthSubmitId)]
        [InlineData(AuthUsernameId)]
        [InlineData(AuthPasswordId)]
        [InlineData(LoginTabId)]
        [InlineData(RegisterTabId)]
        public void AcsessToInterface(string elementId)
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";
            string xpath = $"//*[@id='{elementId}']";
            IWebElement element = _EdgeDriver.FindElement(By.XPath(xpath));

            Assert.True(element.Enabled);
        }

        // переходы между режимами, доступными неавторизованному пользователю;
        [Fact]
        public void LoginOrRegCheck1()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";
            IWebElement ChangeReg = _EdgeDriver.FindElement(By.Id(RegisterTabId));
            ChangeReg.Click();

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Assert.Equal("Зарегистрироваться", Reg.Text);
        }

        [Fact]
        public void LoginOrRegCheck2()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";
            IWebElement ChangeReg = _EdgeDriver.FindElement(By.Id(RegisterTabId));
            ChangeReg.Click();

            IWebElement ChangeLog = _EdgeDriver.FindElement(By.Id(LoginTabId));
            ChangeLog.Click();

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Assert.Equal("Зарегистрироваться", Reg.Text);
        }

        [Fact]
        // поведение интерфейса при корректных и некорректных действиях на начальном этапе
        public void EmptyLoginAndPasswordCheck()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";
            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            IWebElement CheckPage = _EdgeDriver.FindElement(By.Id(LoginTabId));
            Assert.Equal("Вход", CheckPage.Text);
        }

        [Fact]
        public void EmptyLoginCheck()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Passw = _EdgeDriver.FindElement(By.Id(AuthPasswordId));

            Passw.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            IWebElement CheckPage = _EdgeDriver.FindElement(By.Id(LoginTabId));
            Assert.Equal("Вход", CheckPage.Text);
        }

        [Fact]
        public void EmptyPasswordCheck()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(LoginTabId));

            Login.SendKeys(LoginUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            IWebElement CheckPage = _EdgeDriver.FindElement(By.Id(LoginTabId));
            Assert.Equal("Вход", CheckPage.Text);
        }


        [Fact]
        public void PasswordLessThan3Simbols()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";
            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement CheckPage = _EdgeDriver.FindElement(By.Id(LoginTabId));
            Assert.Equal("Вход", CheckPage.Text);
        }

        [Fact]
        public void SucsessLogin()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));

            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));

            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));

            Reg.Click();

            Thread.Sleep(Sleep);

            IWebElement CheckPage = _EdgeDriver.FindElement(By.Id(NextPageTabId));

            Assert.Equal("Здравствуйте, qwerty!", CheckPage.Text);
        }

        // реакцию интерфейса на ошибочные действия;
        [Fact]
        public void ErrorLogin()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(WrongInput);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            IWebElement Error = _EdgeDriver.FindElement(By.XPath(ErrorMessageXPath));
            Assert.Equal("Неверный логин или пароль.", Error.Text);
        }

        [Fact]
        public void ErrorReg()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement RegInput = _EdgeDriver.FindElement(By.Id(RegisterTabId));

            RegInput.Click();

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            IWebElement Error = _EdgeDriver.FindElement(By.XPath(ErrorMessageXPath));
            Assert.Equal("Пользователь с таким логином уже существует.", Error.Text);
        }
    }
}
