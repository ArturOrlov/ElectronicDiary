using AutoMapper;
using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class HomeworkService : IHomeworkService
{
    private readonly IHomeworkRepository _homeworkRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IMapper _mapper;

    public HomeworkService(IHomeworkRepository homeworkRepository,
        ISubjectRepository subjectRepository, 
        ISchoolClassRepository schoolClassRepository,
        IMapper mapper)
    {
        _homeworkRepository = homeworkRepository;
        _subjectRepository = subjectRepository;
        _schoolClassRepository = schoolClassRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetHomeworkDto>> GetHomeworkByIdAsync(int homeworkId)
    {
        var response = new BaseResponse<GetHomeworkDto>();

        var homework = await _homeworkRepository.GetByIdAsync(homeworkId);

        if (homework == null)
        {
            response.IsError = true;
            response.Description = $"Домашнее задание с id - {homeworkId} не найдено";
            return response;
        }

        var mapHomework = _mapper.Map<GetHomeworkDto>(homework);

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

        if (request.ForDateAt <= DateTimeOffset.Now)
        {
            response.IsError = true;
            response.Description = $"Дата сдачи домашнего задания - {request.ForDateAt} установлено неверно";
            return response; 
        }

        var homework = _mapper.Map<Homework>(request);
        // var homework = new Homework()
        // {
        //     ForDateAt = request.ForDateAt,
        //     HomeworkDescription = request.HomeworkDescription,
        //     SubjectId = request.SubjectId,
        //     SchoolClassId = request.SchoolClassId,
        // };

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
            if (request.ForDateAt <= DateTimeOffset.Now)
            {
                response.IsError = true;
                response.Description = $"Дата сдачи домашнего задания - {request.ForDateAt} установлено неверно";
                return response; 
            }
            
            homework.ForDateAt = request.ForDateAt;
        }

        await _homeworkRepository.UpdateAsync(homework);
        
        var mapHomework = _mapper.Map<GetHomeworkDto>(homework);

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

        response.Data = "Удалено";
        return response;
    }
}