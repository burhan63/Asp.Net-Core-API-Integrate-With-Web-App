using CoreWebAppWithCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CoreWebAppWithCrud.Controllers
{
    public class StudentController : Controller
    {
        //Pass URL of API
        private String url = "https://localhost:7072/api/CRUDOperation/";

        //API response kai liye client object
        private HttpClient client = new HttpClient(); 
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Student>>(result); //yeh json ko list of students mai convert kare ga

                if (data != null)
                {
                    students = data;
                }
            }
            return View(students);
        }

        //implementation of create new
        [HttpGet]
        public IActionResult Create()
        {          
            return View();
        }

        //Insert in Creat new Operation Form
        public IActionResult Create(Student std)
        {
            string data = JsonConvert.SerializeObject(std); //humare json data format ko serialize kare ga
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json"); //StringContent means jo bh data ka reponse ata hai wo content ki form mai ata hai 
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["Insert_Message"] = "Student Added Sucessfully..!!";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Load Data in terms of ID on Edit View
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result; //because url + id jarahi hai humare api mai
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);

                if(data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }

        //After Edit Update Data in Database
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            var data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + std.stdId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Update_Message"] = "Student data Updated..!!!";
                return RedirectToAction("Index");
            }
            return View(std);
        }

        //See Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result; //because url + id jarahi hai humare api mai
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);

                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }

        //Load Data in terms of ID on Delete View
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result; //because url + id jarahi hai humare api mai
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);

                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }

        //Delete Data in Database
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)    
        {
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Delete_Message"] = "Student Record Deleted Sucessfully..!!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
