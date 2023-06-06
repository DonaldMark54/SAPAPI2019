using StudentAdminPortal.API.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        // This is the interface to the database. This is the 
        // definition of this method.

        // Next line is way slow babes.
        //List<Student> GetStudents();

        public Task<List<Student>> GetStudentsAsync();

        public Task<Student> GetStudentAsync(Guid studentId);

        Task<List<Gender>>GetGendersAsync();

        Task<bool> Exists(Guid studentId);

        Task<Student>UpdateStudent(Guid studentId, Student request);
        Task<Student> DeleteStudent(Guid studentId);

        Task<Student>AddStudent(Student request);

        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);

    }
}
