using AutoMapper;
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
    private readonly IMapper _mapper;

    public SubjectService(ISubjectRepository subjectRepository,
        IMapper mapper)
    {
        _subjectRepository = subjectRepository;
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

        if (string.IsNullOrEmpty(request.Name))
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