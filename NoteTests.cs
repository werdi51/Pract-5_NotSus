using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace NotesTests
{

    public class NoteTests : IDisposable
    {

        public IWebDriver _EdgeDriver = new EdgeDriver();

        [Fact]
        public void CorrectWebStart()
        {
            _EdgeDriver.Url = "https://test.webmx.ru/";
            const string Path = "//*[@id=\"mainView\"]/header/div[1]/h1";
            IWebElement find = _EdgeDriver.FindElement(By.XPath(Path));

            Assert.Equal("📝 Сервис заметок", find.Text);
        }

        public void Dispose()
        {
            _EdgeDriver.Quit();
        }
    }
}
