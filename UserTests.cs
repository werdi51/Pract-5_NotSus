using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace NotesTests
{
    public class UserTests : IDisposable
    {
        public IWebDriver _EdgeDriver = new EdgeDriver();

        public const int Sleep = 1000;
        public const string AuthUsernameId = "authUsername";
        public const string AuthPasswordId = "authPassword";
        public const string AuthSubmitId = "authSubmit";
        public const string WelcomeTextId = "welcomeText";
        public const string LoginTabId = "loginTab";

        public const string LogoutBtnId = "logoutBtn";
        public const string SearchInputId = "searchInput";
        public const string NewNoteBtnId = "newNoteBtn";
        public const string NoteScopeFilterId = "noteScopeFilter";
        public const string NoteTitleInputId = "noteTitle";
        public const string NoteContentInputId = "noteContent";
        public const string SaveBtnBtnId = "saveBtn";

        public const string ErrorMessageXPath = "//*[@id=\"message\"]/span";
        public const string SecondNoteItemXPath = "//*[@id=\"noteScopeFilter\"]/option[2]";
        public const string FirstNoteItemXPath = "//*[@id=\"noteScopeFilter\"]/option[1]";
        public const string ThirdNoteItemXPath = "//*[@id=\"noteScopeFilter\"]/option[3]";
        public const string EmptyListItemXPath = "//*[@id=\"notesList\"]/li";


        public const string LoginUser = "qwerty";
        public const string PasswordUser = "123456789";

        public const string WrongInput = "zxc123";


        public void Dispose()
        {
            _EdgeDriver.Dispose();
        }

        // тест доступность основных элементов начального экрана
        [Theory]
        [InlineData(WelcomeTextId)]
        [InlineData(LogoutBtnId)]
        [InlineData(SearchInputId)]
        [InlineData(NewNoteBtnId)]
        [InlineData(NoteScopeFilterId)]
        [InlineData(NoteTitleInputId)]
        [InlineData(NoteContentInputId)]
        [InlineData(SaveBtnBtnId)]
        public void AcsessToInterfaceAfterLogin(string elementId)
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            _EdgeDriver.FindElement(By.Id(AuthUsernameId)).SendKeys(LoginUser);
            _EdgeDriver.FindElement(By.Id(AuthPasswordId)).SendKeys(PasswordUser);
            _EdgeDriver.FindElement(By.Id(AuthSubmitId)).Click();

            Thread.Sleep(Sleep); 

            IWebElement element = _EdgeDriver.FindElement(By.Id(elementId));
            Assert.True(element.Enabled);
        }

        [Fact]
        public void InterfaceCheck1()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            IWebElement CheckPage = _EdgeDriver.FindElement(By.Id(NewNoteBtnId));
            Assert.Equal("Новая заметка", CheckPage.Text);
        }

        [Fact]
        public void WrongSearchCheck()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            IWebElement Find = _EdgeDriver.FindElement(By.Id(SearchInputId));
            Find.SendKeys(WrongInput);

            Thread.Sleep(Sleep);

            IWebElement List = _EdgeDriver.FindElement(By.XPath(EmptyListItemXPath));

            Assert.Equal("Нет заметок. Создайте первую заметку.", List.Text);
        }

        [Fact]
        public void FilterCheck1()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            _EdgeDriver.FindElement(By.Id(NoteScopeFilterId)).Click();
            Thread.Sleep(Sleep);
            _EdgeDriver.FindElement(By.XPath(SecondNoteItemXPath)).Click();


            var selectElement = _EdgeDriver.FindElement(By.Id(NoteScopeFilterId));
            var selectedOption = selectElement.FindElement(By.CssSelector("option:checked"));
            Assert.Equal("Мои", selectedOption.Text);

        }
        [Fact]

        public void FilterCheck2()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            _EdgeDriver.FindElement(By.Id(NoteScopeFilterId)).Click();
            Thread.Sleep(Sleep);
            _EdgeDriver.FindElement(By.XPath(ThirdNoteItemXPath)).Click();


            var selectElement = _EdgeDriver.FindElement(By.Id(NoteScopeFilterId));
            var selectedOption = selectElement.FindElement(By.CssSelector("option:checked"));
            Assert.Equal("Общие", selectedOption.Text);

        }
    }
}
