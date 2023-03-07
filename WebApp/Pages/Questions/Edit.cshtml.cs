//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using BusinessObjects.DbContexts;
//using BusinessObjects.Models;

//namespace WebApp.Pages.Questions
//{
//    public class EditModel : PageModel
//    {
//        private readonly BusinessObjects.DbContexts.OnlineTestingManagementSystemDbContext _context;

//        public EditModel(BusinessObjects.DbContexts.OnlineTestingManagementSystemDbContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public Question Question { get; set; }

//        public async Task<IActionResult> OnGetAsync(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            Question = await _context.Questions
//                .Include(q => q.QuestionCategory)
//                .Include(q => q.QuestionCreator).FirstOrDefaultAsync(m => m.Id == id);

//            if (Question == null)
//            {
//                return NotFound();
//            }
//           ViewData["QuestionCategoryId"] = new SelectList(_context.QuestionCategories, "Id", "Category");
//           ViewData["QuestionCreatorId"] = new SelectList(_context.TestCreators, "Id", "Id");
//            return Page();
//        }

//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see https://aka.ms/RazorPagesCRUD.
//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            _context.Attach(Question).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!QuestionExists(Question.Id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return RedirectToPage("./Index");
//        }

//        private bool QuestionExists(string id)
//        {
//            return _context.Questions.Any(e => e.Id == id);
//        }
//    }
//}
