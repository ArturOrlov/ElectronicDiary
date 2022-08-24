using AutoMapper;
using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.Extensions.Caching.Memory;

namespace ElectronicDiary.Services;

public class HomeworkService : IHomeworkService
{
    private readonly IHomeworkRepository _homeworkRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public HomeworkService(IHomeworkRepository homeworkRepository,
        ISubjectRepository subjectRepository,
        ISchoolClassRepository schoolClassRepository,
        IMemoryCache memoryCache,
        IMapper mapper)
    {
        _homeworkRepository = homeworkRepository;
        _subjectRepository = subjectRepository;
        _schoolClassRepository = schoolClassRepository;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetHomeworkDto>> GetHomeworkByIdAsync(int homeworkId)
    {
        var response = new BaseResponse<GetHomeworkDto>();

        if (_memoryCache.TryGetValue(homeworkId, out GetHomeworkDto cacheHomework))
        {
            response.Data = cacheHomework;
            return response;
        }

        var homework = await _homeworkRepository.GetByIdAsync(homeworkId);

        if (homework == null)
        {
            response.IsError = true;
            response.Description = $"Домашнее задание с id - {homeworkId} не найдено";
            return response;
        }

        var mapHomework = _mapper.Map<GetHomeworkDto>(homework);

        _memoryCache.Set(homeworkId, mapHomework, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
        
        response.Data = mapHomework;
        return response;
    }

    public async Task<BaseResponse<List<GetHomeworkDto>>> GetHomeworkByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetHomeworkDto>>();

        var homework = _homeworkRepository.Get(_ => true, request);

        if (homework == null || !homework.Any())
        {
            return response;
        }

        var mapHomework = _mapper.Map<List<GetHomeworkDto>>(homework);

        response.Data = mapHomework;
        return response;
    }

    public async Task<BaseResponse<GetHomeworkDto>> CreateHomeworkAsync(CreateHomeworkDto request)
    {
        var response = new BaseResponse<GetHomeworkDto>();

        if (string.IsNullOrEmpty(request.HomeworkDescription))
        {
            response.IsError = true;
            response.Description = "Описание домашнего задание отсутствует";
            return response;
        }

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмета с id - {request.SubjectId} не найден";
            return response;
        }

        var schoolClass = await _schoolClassRepository.GetByIdAsync(request.SchoolClassId);

        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класса с id - {request.SchoolClassId} не найден";
            return response;
        }

        if (request.ForDateAt.Date <= DateTime.Now.Date)
        {
            response.IsError = true;
            response.Description = $"Дата сдачи домашнего задания - {request.ForDateAt.Date} установлено неверно";
            return response;
        }

        var result = CheckHomeworkOnRepeat(request.SchoolClassId, request.SubjectId, request.ForDateAt);

        if (result.Item2)
        {
            response.IsError = true;
            response.Description = result.Item1;
            return response;
        }

        var homework = _mapper.Map<Homework>(request);

        await _homeworkRepository.CreateAsync(homework);

        var mapHomework = _mapper.Map<GetHomeworkDto>(homework);

        response.Data = mapHomework;
        return response;
    }

    public async Task<BaseResponse<GetHomeworkDto>> UpdateHomeworkByIdAsync(int homeworkId, UpdateHomeworkDto request)
    {
        var response = new BaseResponse<GetHomeworkDto>();

        var homework = await _homeworkRepository.GetByIdAsync(homeworkId);

        if (homework == null)
        {
            response.IsError = true;
            response.Description = $"Домашнее задание с id - {homeworkId} не найдено";
            return response;
        }

        if (string.IsNullOrEmpty(request.HomeworkDescription))
        {
            response.IsError = true;
            response.Description = "Описание домашнего задание отсутствует";
            return response;
        }

        if (request.SubjectId.HasValue)
        {
            var subject = await _subjectRepository.GetByIdAsync((int)request.SubjectId);

            if (subject == null)
            {
                response.IsError = true;
                response.Description = $"Предмета с id - {request.SubjectId} не найден";
                return response;
            }

            homework.SubjectId = (int)request.SubjectId;
        }

        if (request.SchoolClassId.HasValue)
        {
            var schoolClass = await _schoolClassRepository.GetByIdAsync((int)request.SchoolClassId);

            if (schoolClass == null)
            {
                response.IsError = true;
                response.Description = $"Класса с id - {request.SchoolClassId} не найден";
                return response;
            }

            homework.SchoolClassId = (int)request.SchoolClassId;
        }

        if (request.ForDateAt != null)
        {
            if (request.ForDateAt.Value.Date <= DateTime.Now.Date)
            {
                response.IsError = true;
                response.Description = $"Дата сдачи домашнего задания - {request.ForDateAt.Value.Date} установлено неверно";
                return response;
            }

            homework.ForDateAt = (DateTime)request.ForDateAt;
        }
        
        var result = CheckHomeworkOnRepeat(homework.SchoolClassId, homework.SubjectId, homework.ForDateAt);

        if (result.Item2)
        {
            response.IsError = true;
            response.Description = result.Item1;
            return response;
        }

        await _homeworkRepository.UpdateAsync(homework);

        var mapHomework = _mapper.Map<GetHomeworkDto>(homework);
        
        _memoryCache.Set(homeworkId, mapHomework, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));

        response.Data = mapHomework;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteHomeworkByIdAsync(int homeworkId)
    {
        var response = new BaseResponse<string>();

        var homework = await _homeworkRepository.GetByIdAsync(homeworkId);

        if (homework == null)
        {
            response.IsError = true;
            response.Description = $"Домашнее задание с id - {homeworkId} не найдено";
            return response;
        }

        await _homeworkRepository.DeleteAsync(homework);
        
        _memoryCache.Remove(homeworkId);

        response.Data = "Удалено";
        return response;
    }

    private (string, bool) CheckHomeworkOnRepeat(int schoolClassId, int subjectId,  DateTime forDateAt)
    {
        var homeworks = _homeworkRepository.Get(h => h.SubjectId == subjectId
                                                && h.SchoolClassId == schoolClassId
                                                && h.ForDateAt.Date == forDateAt.Date).ToList();

        if (homeworks.Any())
        {
            return ($"Для указанного класса {schoolClassId} по предмету {subjectId} в указанное время {forDateAt.Date} уже есть ДЗ", true);
        }
        
        return ("Совпадений не обнаружено", false);
    }
}