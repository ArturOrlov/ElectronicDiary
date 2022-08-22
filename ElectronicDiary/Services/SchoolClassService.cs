using AutoMapper;
using ElectronicDiary.Dto.SchoolClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class SchoolClassService : ISchoolClassService
{
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IMapper _mapper;

    public SchoolClassService(ISchoolClassRepository schoolClassRepository, 
        IMapper mapper)
    {
        _schoolClassRepository = schoolClassRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetSchoolClassDto>> GetSchoolClassByIdAsync(int schoolClassIdId)
    {
        var response = new BaseResponse<GetSchoolClassDto>();

        var schoolClass = await _schoolClassRepository.GetByIdAsync(schoolClassIdId);

        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класс с id - {schoolClassIdId} не найден";
            return response;
        }

        var mapSchoolClass = _mapper.Map<GetSchoolClassDto>(schoolClass);

        response.Data = mapSchoolClass;
        return response;
    }

    public async Task<BaseResponse<List<GetSchoolClassDto>>> GetSchoolClassByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetSchoolClassDto>>();

        var schoolClass = _schoolClassRepository.Get(_ => true, request);

        if (schoolClass == null || !schoolClass.Any())
        {
            return response;
        }
        
        var mapSchoolClass = _mapper.Map<List<GetSchoolClassDto>>(schoolClass);

        response.Data = mapSchoolClass;
        return response;
    }

    // todo недоделано
    public async Task<BaseResponse<GetSchoolClassDto>> CreateSchoolClassAsync(CreateSchoolClassDto request)
    {
        var response = new BaseResponse<GetSchoolClassDto>();

        if (string.IsNullOrEmpty(request.Symbol))
        {
            response.IsError = true;
            response.Description = "Описание домашнего задание отсутствует";
            return response;
        }
        
        // if (request.ForDateAt <= DateTimeOffset.Now)
        // {
        //     response.IsError = true;
        //     response.Description = $"Дата сдачи домашнего задания - {request.ForDateAt} установлено неверно";
        //     return response; 
        // }
        //
        // await _schoolClassRepository.CreateAsync(homework);
        //
        // response.Data = mapHomework;
        return response;
    }

    // todo недоделано
    public async Task<BaseResponse<GetSchoolClassDto>> UpdateSchoolClassByIdAsync(int schoolClassIdId, UpdateSchoolClassDto request)
    {
        var response = new BaseResponse<GetSchoolClassDto>();
        
        var schoolClass = await _schoolClassRepository.GetByIdAsync(schoolClassIdId);

        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класс с id - {schoolClassIdId} не найден";
            return response;
        }

        if (!string.IsNullOrEmpty(request.Symbol))
        {
            schoolClass.Symbol = request.Symbol;
        }
        
        if (request.ClassCreateTime != null)
        {
            schoolClass.CreatedAt = request.ClassCreateTime;
        }

        await _schoolClassRepository.UpdateAsync(schoolClass);

        var mapSchoolClass = _mapper.Map<GetSchoolClassDto>(schoolClass);
        
        response.Data = mapSchoolClass;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteSchoolClassByIdAsync(int schoolClassIdId)
    {
        var response = new BaseResponse<string>();

        var schoolClass = await _schoolClassRepository.GetByIdAsync(schoolClassIdId);

        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класс с id - {schoolClassIdId} не найден";
            return response;
        }

        await _schoolClassRepository.DeleteAsync(schoolClass);

        response.Data = "Удалено";
        return response;
    }
}