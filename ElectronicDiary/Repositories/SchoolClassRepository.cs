using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Repositories;

public class SchoolClassRepository : ISchoolClassRepository
{
    private readonly ElectronicDiaryDbContext _context;

    public SchoolClassRepository(ElectronicDiaryDbContext context)
    {
        _context = context;
    }
    
    public List<SchoolClass> GetStudents()
    {
        return _context.Class.ToList();
    }

    public SchoolClass GetStudentById(int id)
    {
        return _context.Class.Find(id);
    }

    public void Insert(SchoolClass student)
    {
        _context.Class.Add(student);
        Save();
    }

    public void Delete(SchoolClass class1)
    {
        _context.Class.Remove(class1);
        Save();
    }

    public void Update(SchoolClass student)
    {
        _context.Entry(student).State = EntityState.Modified;
        Save();
    }
    
    public void Save()
    {
        _context.SaveChanges();
    }
}