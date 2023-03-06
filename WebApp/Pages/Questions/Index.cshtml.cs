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
using WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Pages.Questions
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IQuestionRepository questionRepository;
        private readonly IConfiguration configuration;

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Question> QuestionList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IQuestionRepository questionRepository)
        {
            this.logger = logger;
            this.questionRepository = questionRepository;
            this.configuration = configuration;
        }

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            List<Question> questionList;
            int pageSize = configuration.GetValue("PageSize", 10);
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                questionList = await questionRepository.GetAllByContent(searchString);
            } 
            else
            {
                questionList = await questionRepository.GetAllAsync();
            }
            QuestionList = PaginatedList<Question>.CreateAsync(questionList, pageIndex ?? 1, pageSize);
        }
    }
}
