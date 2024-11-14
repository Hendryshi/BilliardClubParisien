using BCP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Api.Controllers
{
	[ApiController]
	[Route("BCP/Licences/")]
	public class LicencesController : ControllerBase
	{
		private readonly ILogger<LicencesController> _logger;

		public LicencesController(ILogger<LicencesController> logger)
		{
			_logger = logger;
		}

		[HttpGet("AllForms")]
		public ActionResult<List<LicenceForm>> GetAllForms()
		{
			var listForms = new List<LicenceForm>()
			{
				new LicenceForm() { Id = 1, FirstName = "John", LastName = "Smith", Email = "test1@test.com", Phone="123456"},
				new LicenceForm() { Id = 2, FirstName = "Ding", LastName = "Junhui", Email = "test2@test.com", Phone="65187"},
				new LicenceForm() { Id = 3, FirstName = "Liang", LastName = "wenbo", Email = "test3@test.com", Phone="8765615"},
				new LicenceForm() { Id = 4, FirstName = "Jane", LastName = "Smith", Email = "test4@test.com", Phone="77778888"}
			};
			return listForms;
		}
	}
}
