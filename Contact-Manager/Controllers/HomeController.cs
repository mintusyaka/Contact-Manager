using Contact_Manager.Data;
using Contact_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Contact_Manager.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Contacts.ToList());
        }

        public IActionResult AddContact()
        {
            return View();
        }

        public IActionResult EditContact(int? Id)
        {
            if (Id == null)
                return NotFound();

            Contact? contact = _db.Contacts.Find(Id);

            if (contact == null)
                return NotFound();

            return View(contact);
        }

		public IActionResult RemoveContact(int? Id)
		{
            if (Id == null)
                return NotFound();

            Contact? contact = _db.Contacts.Find(Id);

            if (contact == null)
                return NotFound();

            return View(contact);
		}

		[HttpPost]
		public IActionResult AddContact(Contact newContact)
		{
            if (ModelState.IsValid)
            {
                _db.Contacts.Add(newContact);
                _db.SaveChanges();  
                return RedirectToAction("Index", "Home");
            }
            
			return View(newContact);
		}

        [HttpPost]
        public IActionResult EditContact(Contact contactToEdit)
        {
            if (ModelState.IsValid)
            {
                _db.Contacts.Update(contactToEdit);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(contactToEdit);

        }

        [HttpPost]
        public IActionResult RemoveContact(Contact contactToEdit)
        {
            _db.Contacts.Remove(contactToEdit);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> ReadCSV(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads";

                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    var filePath = Path.Combine(uploadDirectory, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string fileContent;
                    using (var reader = new StreamReader(filePath))
                    {
                        fileContent = await reader.ReadToEndAsync();
                    }

                    string[] separatedRows = fileContent.Split('\n');

                    foreach (string s in separatedRows)
                    {
                        string[] separatedFields = s.Split(';');
                        if (separatedFields.Length == 5)
                        {
                            Contact newContact = new Contact
                            {
                                Name = separatedFields[0],
                                Birthday = DateTime.Parse(separatedFields[1]),
                                Married = bool.Parse(separatedFields[2]),
                                Phone = separatedFields[3],
                                Salary = decimal.Parse(separatedFields[4])
                            };

                            AddContact(newContact);

                        }
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Home");
				}
            }


			return RedirectToAction("Index", "Home");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
