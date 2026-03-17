using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesTests
{
    public class NoteValidationTests : IDisposable
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
        public const string DeleteBtnId = "deleteBtn";

        public const string ErrorMessageXPath = "//*[@id=\"message\"]/span";
        public const string SecondNoteItemXPath = "//*[@id=\"noteScopeFilter\"]/option[2]";
        public const string FirstNoteItemXPath = "//*[@id=\"noteScopeFilter\"]/option[1]";
        public const string ThirdNoteItemXPath = "//*[@id=\"noteScopeFilter\"]/option[3]";
        public const string EmptyListItemXPath = "//*[@id=\"notesList\"]/li";

        public const string FirstNameListXPath = "//*[@id=\"notesList\"]/li[1]/strong";

        public const string LoginUser = "qwerty";
        public const string PasswordUser = "123456789";

        public const string Test = "test";
        public const string TestRedacted = "testR";


        public const string WrongInput = "zxc123";

        [Fact]
        public void AddNote()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            _EdgeDriver.FindElement(By.Id(NoteTitleInputId)).SendKeys(Test);
            _EdgeDriver.FindElement(By.Id(NoteContentInputId)).SendKeys(Test);

            _EdgeDriver.FindElement(By.Id(SaveBtnBtnId)).Click();

            Thread.Sleep(Sleep);
            Thread.Sleep(Sleep);


            IWebElement NewNote = _EdgeDriver.FindElement(By.XPath(FirstNameListXPath));

            Assert.Equal(Test, NewNote.Text);
        }

        [Fact]
        public void RedactNote()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            IWebElement Note = _EdgeDriver.FindElement(By.XPath(FirstNameListXPath));

            var NoteOld= Note.Text;

            _EdgeDriver.FindElement(By.XPath(EmptyListItemXPath)).Click();

            _EdgeDriver.FindElement(By.Id(NoteTitleInputId)).SendKeys(TestRedacted);
            _EdgeDriver.FindElement(By.Id(NoteContentInputId)).SendKeys(TestRedacted);

            _EdgeDriver.FindElement(By.Id(SaveBtnBtnId)).Click();

            Thread.Sleep(Sleep);

            IWebElement RedactedNote = _EdgeDriver.FindElement(By.XPath(FirstNameListXPath));

            Assert.NotEqual(NoteOld, RedactedNote.Text);
        }

        [Fact]
        public void TryToDelete()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";

            IWebElement Login = _EdgeDriver.FindElement(By.Id(AuthUsernameId));
            Login.SendKeys(LoginUser);

            IWebElement Password = _EdgeDriver.FindElement(By.Id(AuthPasswordId));
            Password.SendKeys(PasswordUser);

            IWebElement Reg = _EdgeDriver.FindElement(By.Id(AuthSubmitId));
            Reg.Click();

            Thread.Sleep(Sleep);

            IWebElement DelBtn = _EdgeDriver.FindElement(By.Id(DeleteBtnId));

            Assert.False(DelBtn.Enabled);
        }


        public void Dispose()
        {
            _EdgeDriver.Dispose();
        }
    }
}
