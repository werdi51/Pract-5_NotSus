using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesTests
{
    public class LogInTests : IDisposable
    {
        public IWebDriver _EdgeDriver = new EdgeDriver();

        public const int Sleep = 1000;

        public const string AuthUsernameId = "authUsername";
        public const string AuthPasswordId = "authPassword";
        public const string AuthSubmitId = "authSubmit";
        public const string WelcomeTextId = "welcomeText";
        public const string LogoutBtnId = "logoutBtn";
        public const string LoginTabId = "loginTab";
        public const string NewNoteBtnId = "newNoteBtn";
        public const string SearchInputId = "searchInput";
        public const string NotesListId = "notesList";
        public const string NextPageTabId = "welcomeText";
        public const string RegisterTabId = "registerTab";


        public const string ErrorMessageXPath = "//*[@id=\"message\"]/span";
        public const string FirstNoteItemXPath = "//*[@id=\"notesList\"]/li";

        public const string LoginUser = "qwerty";
        public const string PasswordUser = "123456789";

        public const string WrongInput = "zxc123";
        public void Dispose()
        {
            _EdgeDriver.Dispose();
        }

        [Fact]
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

        // сценарии завершения работы пользователя;
        [Fact]
        public void SucsessLogOut()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            IWebElement Exit = _EdgeDriver.FindElement(By.Id(LogoutBtnId));
            Exit.Click();

            Thread.Sleep(Sleep);

            IWebElement CheckPage = _EdgeDriver.FindElement(By.Id(LoginTabId));
            Assert.Equal("Вход", CheckPage.Text);
        }

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
