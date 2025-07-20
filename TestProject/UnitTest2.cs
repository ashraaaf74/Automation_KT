using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics.CodeAnalysis;

namespace TestProject
{
    [SuppressMessage("NUnit.Analyzers", "NUnit1032:FieldIsNotDisposed", Justification = "driver.Quit() is used for disposal.")]
    public class Test2
    {
        IWebDriver driver;
        WebDriverWait wait;


         [Test]
        public void implicitWait()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); //upper limit for waiting
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
            driver.FindElement(By.TagName("button")).Click();
            string text= driver.FindElement(By.CssSelector("#finish > h4")).Text;
            Console.WriteLine("Text after loading: " + text);
        }

        [Test]
        public void explicitWait()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
            driver.FindElement(By.TagName("button")).Click();
            wait= new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector("#finish > h4")).Displayed); //true
            //wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#finish > h4")))
            string text = driver.FindElement(By.CssSelector("#finish > h4")).Text;
            Console.WriteLine("Text after loading: " + text);
        }
        [Test]
        public void shadowDom()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/shadowdom");
           string msg= driver.FindElement(By.TagName("my-paragraph")).GetShadowRoot().FindElement(By.CssSelector("[name=\"my-text\"]")).Text;
              Console.WriteLine("Text from Shadow DOM: " + msg);
        }
        [Test]
        public void frames()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/nested_frames");
            // default frame > top frame > left frame
            driver.SwitchTo().Frame("frame-top").SwitchTo().Frame("frame-left"); //Switch to top frame
            string text = driver.FindElement(By.TagName("body")).Text; //get text from left frame
            Console.WriteLine("Text from Left Frame: " + text);
           // driver.SwitchTo().ParentFrame();
            driver.SwitchTo().DefaultContent(); //Switch back to default content
        }

        [Test]
        public void window()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
            driver.FindElement(By.CssSelector(".example > a")).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last()); //Switch to the new window
            string text = driver.FindElement(By.CssSelector(".example > h3")).Text; //Click on the link to open new window
            Console.WriteLine("Text from New Window: " + text);
        }

    }
}