﻿using MoodleClone.Domain.Entities;

namespace MoodleClone.Domain.Repositories;

public interface ICoursesRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<int> Create(Course entity);
    Task Delete(Course entity);
    Task<string?> GetCourseOwnerSurnameByIdAsync(int courseId);
    Task<bool> IsStudentAcceptedInCourseAsync(string userId, int courseId);
    Task<IEnumerable<Course>> GetStudentCoursesAsync(string studentId);
    Task<IEnumerable<Course>> GetTeacherCoursesAsync(string teacherId);
    Task<List<CourseUser>> GetPendingStudentsAsync(int courseId);
    Task<bool> IsStudentEnrolledAsync(int courseId, string userId);
    Task SaveChanges();
}
