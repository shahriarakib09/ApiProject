using Microsoft.AspNetCore.Mvc;
using ApiProject.Features.Repositories.StudentRepository;
using ApiProject.Views.StudentView;
using System.Collections.Generic;
using System.Data;
using System.Transactions;

namespace ApiProject.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository newStudentRepository)
        {
            _studentRepository = newStudentRepository;
        }

        [HttpGet(Name = "GetAllStudentList")]
        [ProducesResponseType(typeof(List<StudentView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllStudentList()
        {
            var res = _studentRepository.GetAllStudentListAsync();
            return new OkObjectResult(res.Result.ToList());
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        //public async Task<string> Create(PurchaseBillView purchaseBill)
        //{
        public async Task<ActionResult<string>> Create(StudentView studentView)
        {
            if (studentView == null)
            {
                //return BadRequest();
            }
            var result = await _studentRepository.CreateStudents(studentView);
            return Ok(result);
            //return await _purchaseBillRepository.CreatePurchaseBill(purchaseBill);
        }

    }
}
