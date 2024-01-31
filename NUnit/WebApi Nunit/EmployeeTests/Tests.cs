using Microsoft.AspNetCore.Mvc.Testing;
using EmployeeManagement.Controllers;
using System.Net;
using EmployeeManagement.Models.DbModels;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace EmployeeTests
{
    public class Tests
    {
        private string baseUrl;
        private WebApplicationFactory<EmployeeController> employeeCtr;
        private HttpClient client;

        public Tests()
        {
            employeeCtr = new WebApplicationFactory<EmployeeController>();
            baseUrl = "http://localhost:5282/Employee/";
        }

        [SetUp]
        public void LoadResource()
        {
            client = employeeCtr.CreateClient();
        }


        [Test]
        public async Task Testing_View()
        {
            int getId = 3;

            var response = await this.client.GetAsync($"{baseUrl}GetEmployee/{getId}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                Employee employeeContent = JsonConvert.DeserializeObject<Employee>(content);

                Assert.That(employeeContent.EmployeeId, Is.EqualTo(getId));
            }
            else
            {
                Assert.Fail();
            }

        }

        // [Test]
        // public async Task Test_DeletingProperly()
        // {
        //     int deleteId = 2;

        //     var response = await this.client.DeleteAsync($"{baseUrl}RemoveEmployee/{deleteId}");

        //     if(response.StatusCode == HttpStatusCode.OK)
        //     {
        //         Assert.Pass();
        //     }
        //     else
        //     {
        //         Assert.Fail();
        //     }

        // }

        [Test]
        public async Task Test_EditProperly()
        {
            var content = new
            {
                employeeId = 14,
                name = "JerlinLijo",
                age = 23,
                dateofBirth = "0001-01-01",
                emailId = "barunkumar19@karunya.edu.in",
                mobileNumber = "1234567890",
                roleId = 0,
                address = new
                {
                    id = 12,
                    doorNumber = "7/44",
                    street = "Testing",
                    city = "Testing",
                    district = "Testing",
                    state = "Testing",
                    postalCode = "Testing"
                }
            };

            var response = await this.client.PutAsJsonAsync($"{baseUrl}EditEmployee", content);

            TestContext.WriteLine($"Response : \n{response.StatusCode.ToString()}\n");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test]
        public async Task Test_AddEmployeeProperly()
        {

            var content = new
            {
                name = "Subramani",
                age = 23,
                dateofBirth = "0001-01-01",
                emailId = "barunkumar19@karunya.edu.in",
                mobileNumber = "1234567890",
                address = new
                {
                    doorNumber = "7/44",
                    street = "Testing",
                    city = "Testing",
                    district = "Testing",
                    state = "Testing",
                    postalCode = "Testing"
                }
            };

            var response = await this.client.PostAsJsonAsync($"{baseUrl}NewEmployee", content);

            TestContext.WriteLine($"Response : \n{response.StatusCode.ToString()}\n");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

    }

}

