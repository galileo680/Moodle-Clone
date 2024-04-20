﻿using MoodleClone.Domain.Entities;

namespace MoodleClone.Domain.Repositories;

public interface ICoursesRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<int> Create(Course entity);
    Task Delete(Course entity);
    Task SaveChanges();
}