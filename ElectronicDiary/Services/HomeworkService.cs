using AutoMapper;
using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;
using ExcelLibrary.SpreadSheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ElectronicDiary.Services;

public class HomeworkService : IHomeworkService
{
    private readonly IHomeworkRepository _homeworkRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IUserClassRepository _userClassRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public HomeworkService(IHomeworkRepository homeworkRepository,
        ISubjectRepository subjectRepository, 
        ISchoolClassRepository schoolClassRepository, 
        IUserClassRepository userClassRepository, 
        IUserInfoRepository userInfoRepository, 
        IUserRepository userRepository, 
        IMemoryCache memoryCache,
        IMapper mapper)
    {
        _homeworkRepository = homeworkRepository;
        _subjectRepository = subjectRepository;
        _schoolClassRepository = schoolClassRepository;
        _userClassRepository = userClassRepository;
        _userInfoRepository = userInfoRepository;
        _userRepository = userRepository;
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

    public async Task<BaseResponse<List<GetHomeworkDto>>> GetHomeworkBySelfAsync(UserDataDto userData)
    {
        var response = new BaseResponse<List<GetHomeworkDto>>();

        if (Constants.Role.Parent != userData.Role || Constants.Role.Student != userData.Role)
        {
            response.IsError = true;
            response.Description = $"Пользователь с ролью - {userData.Role} не может получить своё домашние задание";
            return response;
        }
        
        List<GetHomeworkDto> mapHomeworks;

        if (Constants.Role.Parent == userData.Role)
        {
            var userInfo = await _userInfoRepository.GetByUserId(int.Parse(userData.Id));
            var parentChild = _userRepository.Get(u => userInfo.ChildrenUserId.Contains(u.Id));

            var homeworks = new List<Homework>();
            
            foreach (var child in parentChild)
            {
                // Получаем данные в каком классе находится ученик
                var userClass = await _userClassRepository.GetByUserId(child.Id);

                if (userClass == null)
                {
                    continue;
                }
                
                var b = _homeworkRepository.Get(pr => pr.SchoolClassId == userClass.SchoolClassId).ToList();
                
                if (!b.Any())
                {
                    return response;
                }
                
                homeworks.AddRange(b);
            }
            
            if (!homeworks.Any())
            {
                return response;
            }

            homeworks.Sort((x, y) => x.Id.CompareTo(y.Id));

            mapHomeworks = _mapper.Map<List<GetHomeworkDto>>(homeworks);
        }
        else
        {
            // Получаем данные в каком классе находится ученик
            var userClass = await _userClassRepository.GetByUserId(int.Parse(userData.Id));

            if (userClass == null)
            {
                response.IsError = true;
                response.Description = $"Пользователь с id - {userData.Id} не находится в классе";
                return response;
            }
            
            var homeworks = _homeworkRepository.Get(pr => pr.SchoolClassId == userClass.SchoolClassId);

            if (homeworks == null || !homeworks.Any())
            {
                return response;
            }

            mapHomeworks = _mapper.Map<List<GetHomeworkDto>>(homeworks);
        }

        response.Data = mapHomeworks;
        return response;
    }

    public async Task<BaseResponse<FileContentResult>> GetReportHomeworkBySelfAsync(UserDataDto userData)
    {
        var response = new BaseResponse<FileContentResult>();

        var homework = await GetHomeworkBySelfAsync(userData);

        if (homework.IsError)
        {
            response.IsError = true;
            response.Description = homework.Description;
            return response;
        }
        
        var result = await HomeworkInExcel(homework.Data);

        response.Data = ReturnResult(result, "Расписание");
        return response;
    }
    
    private async Task<MemoryStream> HomeworkInExcel(List<GetHomeworkDto> homeworks)
    {
        var workbook = new Workbook();
        var worksheet = new Worksheet("First Sheet")
        {
            Cells =
            {
                [0, 0] = new Cell("Урок"),
                [1, 0] = new Cell("Число сдачи"),
                [2, 0] = new Cell("Описание задания")
            }
        };

        homeworks.Sort((x, y) => DateTimeOffset.Compare(x.ForDateAt, y.ForDateAt));

        var a = _subjectRepository.Get(_ => true);

        for (var i = 0; i < homeworks.Count; i++)
        {
            var homework = homeworks[i];

            var subject = a.FirstOrDefault(s => s.Id == homework.SubjectId);

            if (subject != null)
            {
                worksheet.Cells[i + 1, 0] = new Cell($"{subject.Name}");
            }
            else
            {
                worksheet.Cells[i + 1, 0] = new Cell($"НАЗВАНИЕ ПРЕДМЕТА ОТСУСТВУЕТ");
            }
            
            worksheet.Cells[i + 1, 1] = new Cell($"{homework.ForDateAt}");
            worksheet.Cells[i + 1, 2] = new Cell($"{homework.HomeworkDescription}");
        }

        workbook.Worksheets.Add(worksheet);

        using var stream = new MemoryStream();
        workbook.SaveToStream(stream);
        stream.Flush();
        return stream;
    }

    private FileContentResult ReturnResult(MemoryStream stream, string fileName)
    {
        return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"{fileName}_{DateTime.UtcNow.ToShortDateString()}.xlsx"
        };
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

        if (request.ForDateAt.Date <= DateTimeOffset.Now.Date)
        {
            response.IsError = true;
            response.Description = $"Дата сдачи домашнего задания - {request.ForDateAt.Date} установлено неверно. " +
                                   $"Время сдачи не может быть меньше или равно текущему времени";
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

        if (!string.IsNullOrEmpty(request.HomeworkDescription))
        {
            response.IsError = true;
            response.Description = "Описание домашнего задание отсутствует";
            return response;
        }

        if (request.SubjectId.HasValue  && request.SubjectId != 0)
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

        if (request.SchoolClassId.HasValue  && request.SchoolClassId != 0)
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
            if (request.ForDateAt.Value.Date <= DateTimeOffset.Now.Date)
            {
                response.IsError = true;
                response.Description = $"Дата сдачи домашнего задания - {request.ForDateAt.Value.Date} установлено неверно. " +
                                       $"Время сдачи не может быть меньше или равно текущему времени";
                return response;
            }

            homework.ForDateAt = (DateTimeOffset)request.ForDateAt;
        }
        
        var result = CheckHomeworkOnRepeat(homework.SchoolClassId, homework.SubjectId, homework.ForDateAt);

        if (result.Item2)
        {
            response.IsError = true;
            response.Description = result.Item1;
            return response;
        }

        homework.UpdatedAt = DateTimeOffset.Now;
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

    private (string, bool) CheckHomeworkOnRepeat(int schoolClassId, int subjectId,  DateTimeOffset forDateAt)
    {
        // todo баг. если сделать запрос, то почему то возвращается запись с параметрами переданными в метод когда в бд такой записи нет
        var homeworks = _homeworkRepository.Get(h => h.SubjectId == subjectId
                                                && h.SchoolClassId == schoolClassId
                                                && h.ForDateAt.Date == forDateAt.Date).ToList();

        if (homeworks.Any())
        {
            return ($"Для указанного класса с id - {schoolClassId} по предмету с id - {subjectId} в указанное время - {forDateAt.Date} уже есть ДЗ", true);
        }
        
        return ("Совпадений не обнаружено", false);
    }
}