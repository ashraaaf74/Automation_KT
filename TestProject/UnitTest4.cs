using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics.CodeAnalysis;

namespace TestProject
{
    [SuppressMessage("NUnit.Analyzers", "NUnit1032:FieldIsNotDisposed", Justification = "driver.Quit() is used for disposal.")]
    public class Test4
    {
         WebDriver driver;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
           Console.WriteLine("Database Setup ....");
        }
        [SetUp]
        public void setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login"); //base URL for the tests
        }
        [Test, Order(1)]
        [Category("valid")]
        [Category("smoke")]
        public void ValidLogin()
        {
            driver.FindElement(By.Id("username")).SendKeys("tomsmith");
            driver.FindElement(By.Id("password")).SendKeys("SuperSecretPassword!");
            driver.FindElement(By.ClassName("radius")).Click();
            Assert.AreEqual("https://the-internet.herokuapp.com/secure",driver.Url,"Url is not as expected"); //wait
        }
        [Test, Order(2)]
        [Category("invalid")]
        [Category("smoke")]

        public void InValidLogin()
        {
            driver.FindElement(By.Id("username")).SendKeys("ahmed");
            driver.FindElement(By.Id("password")).SendKeys("SuperSecretPassword!");
            driver.FindElement(By.ClassName("radius")).Click();
            string errorMessage = driver.FindElement(By.Id("flash")).Text;
            Assert.True(errorMessage.Contains("Your username is invalid!"), "Error message is not appeared");
        }
        [Test, Order(3)]
        [Category("invalid")]
        [Category("smoke")]
        public void InValidLoginPassword()
        {
            driver.FindElement(By.Id("username")).SendKeys("tomsmith");
            driver.FindElement(By.Id("password")).SendKeys("sdifohodf");
            driver.FindElement(By.ClassName("radius")).Click();
            string expectedUrl = "https://the-internet.herokuapp.com/login";
            Assert.That(driver.Url, Is.EqualTo(expectedUrl));
        }

        [TearDown]
        public void tearDown()
        {
                driver.Quit();  //driver = null; //close the browser after each test
           // driver.Dispose(); // Dispose the driver to free resources
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("Database Cleaning ....");
        }
    }
}