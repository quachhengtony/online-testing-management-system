using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace WebApp.Pages.Questions
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IQuestionRepository questionRepository;
		public List<Question> Question { get; set; } = new();

		[FromQuery(Name = "search")]
        public string ContentSearch { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IQuestionRepository questionRepository)
        {
            this.logger = logger;
            this.questionRepository = questionRepository;
        }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(ContentSearch))
            {
                Question = await questionRepository.GetAllByContent(ContentSearch);
            } 
            else
            {
                Question = await questionRepository.GetAllAsync();
            }
        }
    }
}
