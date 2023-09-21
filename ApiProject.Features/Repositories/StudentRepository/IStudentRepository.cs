using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiProject.Views.StudentView;

namespace ApiProject.Features.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        Task<IReadOnlyList<StudentView>> GetAllStudentListAsync();

        Task<string> CreateStudents(StudentView _studentView);
    }
}
