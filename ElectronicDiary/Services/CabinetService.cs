using AutoMapper;
using ElectronicDiary.Dto.Cabinet;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class CabinetService : ICabinetService
{
    private readonly ICabinetRepository _cabinetRepository;
    private readonly IMapper _mapper;

    public CabinetService(ICabinetRepository cabinetRepository, 
        IMapper mapper)
    {
        _cabinetRepository = cabinetRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetCabinetDto>> GetCabinetByIdAsync(int cabinetId)
    {
        var response = new BaseResponse<GetCabinetDto>();

        var cabinet = await _cabinetRepository.GetByIdAsync(cabinetId);

        if (cabinet == null)
        {
            response.IsError = true;
            response.Description = $"Кабинет с id - {cabinetId} не найден";
            return response;
        }

        var mapSubject = _mapper.Map<GetCabinetDto>(cabinet);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<List<GetCabinetDto>>> GetCabinetByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetCabinetDto>>();

        var cabinet = _cabinetRepository.Get(_ => true, request);

        if (cabinet == null || !cabinet.Any())
        {
            return response;
        }
        
        var mapSubject = _mapper.Map<List<GetCabinetDto>>(cabinet);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<GetCabinetDto>> CreateCabinetAsync(CreateCabinetDto request)
    {
        var response = new BaseResponse<GetCabinetDto>();

        if (string.IsNullOrEmpty(request.Number))
        {
            response.IsError = true;
            response.Description = "Номер передан неверно";
            return response;
        }

        if (string.IsNullOrEmpty(request.Campus))
        {
            response.IsError = true;
            response.Description = "Корпус передан неверно";
            return response;
        }
        
        if (string.IsNullOrEmpty(request.Floor))
        {
            response.IsError = true;
            response.Description = "Этаж передан неверно";
            return response;
        }

        var cabinet = _mapper.Map<Cabinet>(request);
        
        await _cabinetRepository.CreateAsync(cabinet);
        
        var mapSubject = _mapper.Map<GetCabinetDto>(cabinet);
        
        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<GetCabinetDto>> UpdateCabinetByIdAsync(int cabinetId, UpdateCabinetDto request)
    {
        var response = new BaseResponse<GetCabinetDto>();
        
        var cabinet = await _cabinetRepository.GetByIdAsync(cabinetId);

        if (cabinet == null)
        {
            response.IsError = true;
            response.Description = $"Кабинет с id - {cabinetId} не найден";
            return response;
        }

        if (!string.IsNullOrEmpty(request.Number))
        {
            var checkNumber = _cabinetRepository.Get(s => s.Number == request.Number).FirstOrDefault();
            
            if (checkNumber != null)
            {
                response.IsError = true;
                response.Description = "Предмет с таким названием уже есть";
                return response;
            }
            
            cabinet.Number = request.Number;
        }
        
        if (!string.IsNullOrEmpty(request.Campus))
        {
            var checkNumber = _cabinetRepository.Get(s => s.Number == request.Number 
                                                          && s.Campus == request.Campus).FirstOrDefault();
            
            if (checkNumber != null)
            {
                response.IsError = true;
                response.Description = $"В корпусе - {request.Campus} уже есть кабинет - {request.Number}";
                return response;
            }
            
            cabinet.Campus = request.Campus;
        }
        
        if (!string.IsNullOrEmpty(request.Floor))
        {
            var checkNumber = _cabinetRepository.Get(s => s.Number == request.Number 
                                                          && s.Campus == request.Campus
                                                          && s.Floor == request.Floor).FirstOrDefault();
            
            if (checkNumber != null)
            {
                response.IsError = true;
                response.Description = $"В корпусе - {request.Campus} на этаже - {request.Floor} уже есть кабинет - {request.Number}";
                return response;
            }
            
            cabinet.Floor = request.Floor;
        }

        cabinet.UpdatedAt = DateTimeOffset.Now;
        await _cabinetRepository.UpdateAsync(cabinet);
        
        var mapSubject = _mapper.Map<GetCabinetDto>(cabinet);
        
        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteCabinetByIdAsync(int cabinetId)
    {
        var response = new BaseResponse<string>();

        var cabinet = await _cabinetRepository.GetByIdAsync(cabinetId);

        if (cabinet == null)
        {
            response.IsError = true;
            response.Description = $"Кабинет с id - {cabinetId} не найден";
            return response;
        }

        await _cabinetRepository.DeleteAsync(cabinet);

        response.Data = "Удалено";
        return response;
    }
}