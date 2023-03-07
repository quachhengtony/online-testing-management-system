using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using WebApp.Validators;

namespace WebApp.DTO
{
	public record UpdateTestDTO
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 3)]
		public string Name { get; set; }

		[Required]
		[DisplayName("Key Code")]
		[StringLength(100, MinimumLength = 3)]
		public string KeyCode { get; set; }

		[Required]
		[DisplayName("Start Time")]
		[DataType(DataType.DateTime)]
		[BeforeEndDate(EndDatePropertyName = "EndTime", StartDatePropertyName = "StartTime")]
		public DateTime StartTime { get; set; }

		[Required]
		[DisplayName("End Time")]
		[DataType(DataType.DateTime)]
		[BeforeEndDate(EndDatePropertyName = "GradeReleaseDate", StartDatePropertyName = "EndTime")]
		public DateTime EndTime { get; set; }

		[Required]
		[DisplayName("Grade Release Date")]
		[DataType(DataType.DateTime)]
		[BeforeEndDate(EndDatePropertyName = "GradeFinalizationDate", StartDatePropertyName = "GradeReleaseDate")]
		public DateTime GradeReleaseDate { get; set; }

		[Required]
		[DisplayName("Grade Finalization Date")]
		[DataType(DataType.DateTime)]
		public DateTime GradeFinalizationDate { get; set; }

		[Required]
		[Range(1, 255)]
		public byte Duration { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 3)]
		public string Batch { get; set; }

		[Required]
		[DisplayName("Test Category")]
		public byte? TestCategoryId { get; set; }

		public Guid TestCreatorId { get; set; }
	}
}
