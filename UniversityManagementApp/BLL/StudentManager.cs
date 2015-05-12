using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementApp.DAL;

namespace UniversityManagementApp.BLL
{
    public class StudentManager
    {
        StudentGateway gateway = new StudentGateway();
        public bool IsRegNoExists(string regNo)
        {
            

            Student student = gateway.GetStudentByRegNo(regNo);

            if (student!=null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Insert(Student student)
        {
            bool isRegNoExist = IsRegNoExists(student.RegNo);
            if (isRegNoExist)
            {
                return false;
            }
            return gateway.Insert(student) > 0;
        }

        public Student GetStudentById(int studentId)
        {
            Student student =  gateway.GetStudentById(studentId);

            return student;
        }

        public bool Update(Student student)
        {
            return gateway.Update(student) > 0;
        }

        public List<Student> GetAllStudents()
        {
            return gateway.GetAllStudents();
        }

        public bool Delete(Student student)
        {
            if (student == null)
            {
                return false;
            }
            // check if student exists
            Student aStudent = GetStudentById(student.Id);

            if (aStudent == null)
            {
                return false;
            }
            //delete from db
            int rowAffected =  gateway.Delete(student);
            return rowAffected > 0;
        }
    }
}
