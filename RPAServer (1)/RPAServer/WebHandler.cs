using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreServer
{
    class WebHandler
    {
        public string OpenURL(string url)
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            driver.Url = url;

            GlobalObject global = GlobalObject.Instance;

            
                Guid id = Guid.NewGuid();

            global.fileIndex.Add(id.ToString(), driver);

            return id.ToString();
        }

        public int EnterText(string id,string element, string elementId, string text)
        {



            //IWebDriver driver;
            //driver = new ChromeDriver();
            //driver.Url = url;

            //driver.FindElement(By.Id(elementId)).SendKeys(text);
            //  driver.FindElement(By.Id("pass")).SendKeys(text);
            // ClickAndWaitForPageToLoad(driver, By.Id("loginbutton"));
           

            //id = "https://support.google.com/accounts/answer/6010255?hl=en";
            //elementId = "csi";
            //text="World!";
            
            GlobalObject global = GlobalObject.Instance;

            IWebDriver driver;

            Object tempObject;

            global.fileIndex.TryGetValue(id, out tempObject);

            if (tempObject is IWebDriver)
            {
                driver = (IWebDriver)tempObject;
            }
            else
            {
                return Result.NOK;
            }
            driver.FindElement(By.XPath("//input[@"+element+"='" + elementId + "']")).SendKeys(text);
            //driver.FindElement(By.Name(elementId)).SendKeys(text);
            

            return Result.OK;
        }

        public string ReadText(string id,string element, string elementId)
        {

            //IWebDriver driver;
            //driver = new ChromeDriver();
            //driver.Url = url;

            //string text = driver.FindElement(By.Id(elementId)).Text;
            //return text;
            GlobalObject global = GlobalObject.Instance;

            IWebDriver driver;

            Object tempObject;

            global.fileIndex.TryGetValue(id, out tempObject);

            if (tempObject is IWebDriver)
            {
                driver = (IWebDriver)tempObject;
            }
            else
            {
                return "";
            }
           string answer= driver.FindElement(By.XPath(element)).Text;
           
            return answer;
        }

        public int ClickButton(string id, string elementId)
        {
            //IWebDriver driver;
            //driver = new ChromeDriver();
            //driver.Url = url;

            ///// driver.FindElement(By.Id(elementId)).SendKeys(text);
            ////  driver.FindElement(By.Id("pass")).SendKeys(text);

            //   return ClickAndWaitForPageToLoad(driver, By.Id(elementId));



            GlobalObject global = GlobalObject.Instance;

            IWebDriver driver;

            Object tempObject;

            global.fileIndex.TryGetValue(id, out tempObject);

            if (tempObject is IWebDriver)
            {
                driver = (IWebDriver)tempObject;
            }
            else
            {  
               
                return Result.NOK;
            }

            // driver.FindElement(By.XPath("//input[@type='" + elementId + "']")).Click();
           ClickAndWaitForPageToLoad(driver, By.Id(elementId));
            return Result.OK;
        }

public int CloseBrowser(string id)
{
    GlobalObject global = GlobalObject.Instance;

    IWebDriver driver;

    Object tempObject;

    global.fileIndex.TryGetValue(id, out tempObject);

    if (tempObject is IWebDriver)
    {
        driver = (IWebDriver)tempObject;
    }
    else
    {
        return Result.NOK;
    }

    driver.Close();
    driver.Dispose();

    global.fileIndex.Remove(id);

    return Result.OK;
}


private int ClickAndWaitForPageToLoad(IWebDriver driver,
    By elementLocator, int timeout = 10)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            var elements = driver.FindElements(elementLocator);
            if (elements.Count == 0)
            {
                            return Result.NOK;
            }
            var element = elements.FirstOrDefault(e => e.Displayed);
            element.Click();
            //wait.Until(ExpectedConditions.StalenessOf(element));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine(
                "Element with locator: '" + elementLocator + "' was not found.");
                        return Result.NOK;
        }
                return Result.OK;
        }
    }   

}


