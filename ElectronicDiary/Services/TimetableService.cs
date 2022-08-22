﻿using AutoMapper;
using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class TimetableService : ITimetableService
{
    private readonly ITimetableRepository _timetableRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IMapper _mapper;

    public TimetableService(ITimetableRepository timetableRepository,
        ISubjectRepository subjectRepository,
        ISchoolClassRepository schoolClassRepository, 
        IMapper mapper)
    {
        _timetableRepository = timetableRepository;
        _subjectRepository = subjectRepository;
        _schoolClassRepository = schoolClassRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetTimetableDto>> GetTimetableByIdAsync(int timetableId)
    {
        var response = new BaseResponse<GetTimetableDto>();

        var timetable = await _timetableRepository.GetByIdAsync(timetableId);

        if (timetable == null)
        {
            response.Description = $"Расписание с id - {timetableId} не найдено";
            response.IsError = true;
            return response;
        }

        var mapTimetable = _mapper.Map<GetTimetableDto>(timetable);

        response.Data = mapTimetable;
        return response;
    }

    public async Task<BaseResponse<List<GetTimetableDto>>> GetTimetableByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetTimetableDto>>();

        var timetables = _timetableRepository.Get(_ => true, request);

        if (timetables == null || !timetables.Any())
        {
            return response;
        }
        
        var mapSubject = _mapper.Map<List<GetTimetableDto>>(timetables);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<GetTimetableDto>> CreateTimetableAsync(CreateTimetableDto request)
    {
        var response = new BaseResponse<GetTimetableDto>();

        var subject = await _subjectRepository.GetByIdAsync(request.SchoolClassId);
        
        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмета с id - {request.SchoolClassId} не найден";
            return response;
        }

        var schoolClass = await _schoolClassRepository.GetByIdAsync(request.ClassId);
        
        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класса с id - {request.SchoolClassId} не найден";
            return response;
        }

        // if (request.ForDateAt <= DateTimeOffset.Now)
        // {
        //     response.IsError = true;
        //     response.Description = $"Дата сдачи домашнего задания - {request.ForDateAt} установлено неверно";
        //     return response; 
        // }

        var timetable = _mapper.Map<Timetable>(request);
        
        await _timetableRepository.CreateAsync(timetable);
        
        var mapSubject = _mapper.Map<GetTimetableDto>(timetable);
        
        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<GetTimetableDto>> UpdateTimetableByIdAsync(int timetableId, UpdateTimetableDto request)
    {
        var response = new BaseResponse<GetTimetableDto>();
        
        var timetable = await _timetableRepository.GetByIdAsync(timetableId);
        
        if (timetable == null)
        {
            response.IsError = true;
            response.Description = $"Расписание с id - {timetableId} не найдено";
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

            timetable.SubjectId = (int)request.SubjectId;
        }

        if (request.ClassId.HasValue)
        {
            var schoolClass = await _schoolClassRepository.GetByIdAsync((int)request.ClassId);
        
            if (schoolClass == null)
            {
                response.IsError = true;
                response.Description = $"Класса с id - {request.ClassId} не найден";
                return response;
            }
            
            timetable.ClassId = (int)request.ClassId;
        }

        if (request.StartedAt != null)
        {
            if (request.StartedAt <= DateTimeOffset.Now)
            {
                response.IsError = true;
                response.Description = $"Дата начала урока - {request.StartedAt} установлено неверно";
                return response; 
            }
            
            timetable.StartedAt = request.StartedAt;
        }

        await _timetableRepository.UpdateAsync(timetable);
        
        var mapTimetable = _mapper.Map<GetTimetableDto>(timetable);

        response.Data = mapTimetable;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteTimetableByIdAsync(int timetableId)
    {
        var response = new BaseResponse<string>();

        var subject = await _timetableRepository.GetByIdAsync(timetableId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмет с id - {timetableId} не найден";
            return response;
        }

        await _timetableRepository.DeleteAsync(subject);

        response.Data = "Удалено";
        return response;
    }
}