using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject
{
    public class Tests
    {
        WebDriver driver;
        By username = By.Id("user-name");
        By password = By.CssSelector("[placeholder=\"Password\"]");
        By loginButton = By.Id("login-button");
        By pageLogo = By.ClassName("login_logo");
        [Test]
        public void ValidLogin()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/v1/");
            string title = driver.Title;
            Console.WriteLine("Page Title: " + title);
            string url = driver.Url;
            Console.WriteLine("Page URL: " + url);
            bool isDisplayed = driver.FindElement(pageLogo).Displayed;
            Console.WriteLine("Is Page Logo Displayed: " + isDisplayed);
            driver.FindElement(username).SendKeys("standard_user");
            driver.FindElement(password).SendKeys("secret_sauce");
            driver.FindElement(loginButton).Click();
            //Steps
            //driver.Quit();
        }
        [Test]
        public void InValidLogin()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/v1/");
            string title = driver.Title;
            Console.WriteLine("Page Title: " + title);
            string url = driver.Url;
            Console.WriteLine("Page URL: " + url);
            driver.FindElement(username).SendKeys("standard_user");
            driver.FindElement(password).SendKeys("secret_sauce");
            driver.FindElement(username).Clear();
            //driver.FindElement(loginButton).Click();
            //Steps
            //driver.Quit();
        }
    }
}