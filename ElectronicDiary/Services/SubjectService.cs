using AutoMapper;
using ElectronicDiary.Dto.Report;
using ElectronicDiary.Dto.Subject;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IPerformanceRatingRepository _performanceRatingRepository;
    private readonly IUserClassRepository _userClassRepository;
    private readonly IMapper _mapper;

    public SubjectService(ISubjectRepository subjectRepository, 
        ISchoolClassRepository schoolClassRepository,
        IPerformanceRatingRepository performanceRatingRepository,
        IUserClassRepository userClassRepository,
        IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _schoolClassRepository = schoolClassRepository;
        _performanceRatingRepository = performanceRatingRepository;
        _userClassRepository = userClassRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetSubjectDto>> GetSubjectByIdAsync(int subjectId)
    {
        var response = new BaseResponse<GetSubjectDto>();

        var subject = await _subjectRepository.GetByIdAsync(subjectId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмет с id - {subjectId} не найден";
            return response;
        }

        var mapSubject = _mapper.Map<GetSubjectDto>(subject);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<List<GetSubjectDto>>> GetSubjectByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetSubjectDto>>();

        var subject = _subjectRepository.Get(_ => true, request);

        if (subject == null || !subject.Any())
        {
            return response;
        }
        
        var mapSubject = _mapper.Map<List<GetSubjectDto>>(subject);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<List<ResponseSubjectReportDto>>> GetSubjectReportAsync(GetSubjectReportDto request)
    {
        var response = new BaseResponse<List<ResponseSubjectReportDto>>();

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмет с id - {request.SubjectId} не найден";
            return response;
        }
        
        // Получаем классы
        var schoolClasses = await _schoolClassRepository.GetRangeAsync();
        
        if (!schoolClasses.Any())
        {
            response.IsError = true;
            response.Description = "Классы не найдены";
            return response;
        }
        
        // Получаем учеников
        var schoolClassesIds = schoolClasses.Select(sc => sc.Id);
        var userClasses = _userClassRepository.Get(us => schoolClassesIds.Contains(us.SchoolClassId));
        
        // Получаем оценки
        var userIds = userClasses.Select(u => u.UserId);
        var performanceRatings = _performanceRatingRepository.Get(pr => userIds.Contains(pr.StudentId));
        
        if (!performanceRatings.Any())
        {
            response.IsError = true;
            response.Description = $"Оценки не найдены";
            return response;
        }
        
        var report = new List<ResponseSubjectReportDto>();

        foreach (var schoolClass in schoolClasses)
        {
            var pr = performanceRatings.Where(pr => pr.SchoolClassId == schoolClass.Id).ToList();

            var count = pr.Count;
            var total = 0f;

            foreach (var rating in pr)
            {
                total += rating.Valuation;
            }
            
            report.Add(new ResponseSubjectReportDto()
            {
                SchoolClassId = schoolClass.Id,
                SubjectId = request.SubjectId,
                Gpa = total / count
            });
        }

        report.Sort((x, y) => x.Gpa.CompareTo(y.Gpa));
        response.Data = report;
        return response;
    }

    public async Task<BaseResponse<GetSubjectDto>> CreateSubjectAsync(CreateSubjectDto request)
    {
        var response = new BaseResponse<GetSubjectDto>();

        if (string.IsNullOrEmpty(request.Name))
        {
            response.IsError = true;
            response.Description = "Название предмета отсутствует";
            return response;
        }

        var subjects = _subjectRepository.Get(s => s.Name == request.Name).FirstOrDefault();

        if (subjects != null)
        {
            response.IsError = true;
            response.Description = "Предмет с таким названием уже есть";
            return response;
        }

        var subject = _mapper.Map<Subject>(request);
        
        await _subjectRepository.CreateAsync(subject);
        
        var mapSubject = _mapper.Map<GetSubjectDto>(subject);
        
        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<GetSubjectDto>> UpdateSubjectByIdAsync(int subjectId, UpdateSubjectDto request)
    {
        var response = new BaseResponse<GetSubjectDto>();
        
        var subject = await _subjectRepository.GetByIdAsync(subjectId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмет с id - {subjectId} не найден";
            return response;
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            var subjects = _subjectRepository.Get(s => s.Name == request.Name).FirstOrDefault();

            if (subjects != null)
            {
                response.IsError = true;
                response.Description = "Предмет с таким названием уже есть";
                return response;
            }

            subject.Name = request.Name;
        }

        subject.UpdatedAt = DateTimeOffset.Now;
        await _subjectRepository.UpdateAsync(subject);
        
        var mapSubject = _mapper.Map<GetSubjectDto>(subject);
        
        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteSubjectByIdAsync(int subjectId)
    {
        var response = new BaseResponse<string>();

        var subject = await _subjectRepository.GetByIdAsync(subjectId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмет с id - {subjectId} не найден";
            return response;
        }

        await _subjectRepository.DeleteAsync(subject);

        response.Data = "Удалено";
        return response;
    }
}