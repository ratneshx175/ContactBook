using ContactBook;
using ContactBook.Data;
using ContactBook.Models;
using ContactBook.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics.Contracts;

namespace ContactBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDBContext dbContext;

        public ContactsController(ApplicationDBContext DbContext)
        {
            dbContext = DbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddContactViewModel viewModel) 
        {
            var Contact = new Contact
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Favorite = viewModel.Favorite
            };
            await dbContext.Contacts.AddAsync(Contact);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Contacts");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Contacts = await dbContext.Contacts.ToListAsync();

            return View(Contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var Contact = await dbContext.Contacts.FindAsync(id);

            return View(Contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact viewModel)
        {
            var Contact = await dbContext.Contacts.FindAsync(viewModel.Id);
            {
                if(Contact != null)
                {
                    Contact.Name = viewModel.Name;
                    Contact.Email = viewModel.Email;
                    Contact.Phone = viewModel.Phone;
                    Contact.Favorite = viewModel.Favorite;

                    await dbContext.SaveChangesAsync();
                }

                return RedirectToAction("List", "Contacts");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact viewModel)
        {
            var Contact = await dbContext.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if(Contact != null)
            {
                dbContext.Contacts.Remove(viewModel);
                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Contacts");

        }
    }
}
