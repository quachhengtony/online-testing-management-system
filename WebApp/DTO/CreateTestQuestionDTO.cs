using System.Collections.Generic;

namespace WebApp.DTO
{
	public record CreateTestQuestionDTO
	{
		public List<string> QuestionIds { get; set; }
	}
}
