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

        Task<StudentView> GetStudentByIdListAsync(Int32 _Id);


        Task<string> CreateStudents(StudentView _studentView);

        Task<string> UpdateStudents(int _Id, StudentView _studentView);

        Task<string> DeleteStudents(int _Id);
    }
}
