using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace IMMJ_CRUD
{
    public class Tests
    {

        IWebDriver driver;
        String test_url = "http://computer-database.herokuapp.com/computers";

        //INITIALISING THE CHROME BROWER BEFORE EACH OPERATION
        [SetUp]
        public void Setup()
        {
            //Initialise Chrome before each operation
            driver = new ChromeDriver();

            //Maximise Chrome browser window
            driver.Manage().Window.Maximize();

        }
       /* ***************************************************************************************************************************/
        // TESTS TO VERIFY ADDING A NEW COMPUTER
        [Test]
        [Obsolete]
        public void TestAddNewComputer()
        {


            driver.Url = test_url;

            //Specify a wait object
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            //Click on Add a new computer
            IWebElement Add_Button = driver.FindElement(By.Id("add"));
            Add_Button.Click();

            //Wait for Page to Load
            var AddPage = wait.Until(ExpectedConditions.UrlContains("http://computer-database.herokuapp.com/computers/new"));
            Thread.Sleep(5000);
            //Enter a computer name
            IWebElement Computer_name = wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@id='name' and @type='text']")));
            Computer_name.SendKeys("Asus333");

            //Enter an introduced date
            IWebElement Introduced_date = wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@id='introduced' and @type='text']")));

            Introduced_date.SendKeys("1981-08-01");

            //Enter a discontinued date
            IWebElement Discontinued_date = wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@id='discontinued' and @type='text']")));
            Discontinued_date.SendKeys("1988-09-09");

            //Select  a company from the dropdown
            IWebElement company = wait.Until(ExpectedConditions.ElementExists(By.Id("company")));

            SelectElement selectcompany = new SelectElement(company);

            selectcompany.SelectByIndex(2);

            //Click on the Create button
            IWebElement Create_btn = driver.FindElement(By.XPath("//*[@id='main']/form/div/input"));
            Create_btn.Click();

            //Verify the message on successful creation

            var expected = "Done! Computer Asus333 has been created";
            var message = driver.FindElement(By.XPath("//*[@id='main']/div[1]"));
            var actual = message.Text;
            Assert.AreEqual(expected, actual);

            //Verify if the newly added computer is in the list by using Filter search box

            IWebElement Filter_textbox = wait.Until(ExpectedConditions.ElementExists(By.Id("searchbox")));
            Filter_textbox.SendKeys("Asus333");

            //Click Filter button
            IWebElement Filter_button = driver.FindElement(By.Id("searchsubmit"));
            Filter_button.Click();

            //Verify the new computer name is displayed
            IWebElement result = driver.FindElement(By.XPath("//*[@id='main']/table/tbody/tr/td[1]/a"));
            Assert.AreEqual(result.Text, "Asus333");

        }


        /* ***************************************************************************************************************************/


        //TESTS TO VERIFY EDITING EXISTING COMPUTER DETAILS
        [Test]
        [Obsolete]
        public void TestEditComputerDetails()
        {


            driver.Url = test_url;
          
            //Specify a wait object
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            //Click on an existing computer name
            IWebElement Existing_computer = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main']/table/tbody/tr[1]/td[1]/a")));
            Existing_computer.Click();
            //Edit the computer name on Edit page
            IWebElement Exist_Computer_name = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='name']")));
            Exist_Computer_name.Clear();
            Thread.Sleep(5000);
            Exist_Computer_name.SendKeys("ACE-NEWet1234");

            //Save the Changes
            IWebElement Save = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main']/form[1]/div/input")));
            Save.Click();


            //Verify the message on successful edit

            var expected = "Done! Computer ACE-NEWet1234 has been updated";
            var message = driver.FindElement(By.XPath("//*[@id='main']/div[1]"));
            var actual = message.Text;
            Assert.AreEqual(expected, actual);

            //Verify if the edited computer name is displayed in the list by using Filter search box

            IWebElement Filter_textbox = wait.Until(ExpectedConditions.ElementExists(By.Id("searchbox")));
            Filter_textbox.SendKeys("ACE-NEWet1234");

            //Click Filter button
            IWebElement Filter_button = driver.FindElement(By.Id("searchsubmit"));
            Filter_button.Click();

            //Verify the edited computer name is displayed
            IWebElement result = driver.FindElement(By.XPath("//*[@id='main']/table/tbody/tr/td[1]/a"));
            Assert.AreEqual(result.Text, "ACE-NEWet1234");
            Assert.Pass();
        }


        /* ***************************************************************************************************************************/

        //TESTS TO VERIFY DELETING A COMPUTER FROM THE DATABASE
        [Test]
        [Obsolete]
            public void TestDeleteComputerDetails()
        {


            driver.Url = test_url;

            //Specify a wait object
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            //Wait for page to load
            var AddPage = wait.Until(ExpectedConditions.UrlContains("http://computer-database.herokuapp.com/computers"));
            Thread.Sleep(5000);

            //Click on an existing computer name
            IWebElement Existing_computer = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main']/table/tbody/tr[8]/td[1]/a")));
            Existing_computer.Click();
            //Click on the Delete button
            IWebElement Delete_btn = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@type='submit' and @value='Delete this computer']")));
            Delete_btn.Submit();


            //Type in the deleted Computer name in Filter text box and Search
            IWebElement Filter_textbox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("searchbox")));
            Filter_textbox.SendKeys("Acorn System 2");

            //Click Filter button
            IWebElement Filter_button = driver.FindElement(By.Id("searchsubmit"));
            Filter_button.Submit();

            //Verify if the deleted computer name is not displayed
            IWebElement result = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main']/h1")));
                Assert.AreEqual(result.Text, "No computers found");
            Assert.Pass();

        }

        //CLOSING BROWSER AFTER EACH OPERATION
        [TearDown]

        public void TearDown()

        {
            //Close the browser after each operation

            driver.Close();


        }
    }
    }

