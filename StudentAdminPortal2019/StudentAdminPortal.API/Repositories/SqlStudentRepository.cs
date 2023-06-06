using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student
                .Include(nameof(Gender)).Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.Id == studentId); 
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await context.Student.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student>UpdateStudent(Guid studentId, Student request)
        {
            var existingStudent = await GetStudentAsync(studentId); 
            if(existingStudent !=null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;

                await context.SaveChangesAsync();
                return existingStudent;
            }

            return null;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            // If you accidentally leave out the await it will not compile
            // because the student you get from GetStudentAsync is an async
            // student and is not really the same type.  By putting the await
            // there you are essentially converting it from a Task Student to
            // a non-Task Student. This became clear when I swapped the var 
            // with Student.
            
            Student student = await GetStudentAsync(studentId);

            if (student != null)
            {
                context.Student.Remove(student);
                await context.SaveChangesAsync();
                return student;
            }

            return null;
        }

        public async Task<Student> AddStudent(Student request)
        {
            var student = await context.Student.AddAsync(request);
            await context.SaveChangesAsync();
            return student.Entity;

        }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetStudentAsync(studentId);

            if(student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
