using ElectronicDiary.Dto.Role;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<Entities.DbModels.Role> _roleManager;

    public RoleService(RoleManager<Entities.DbModels.Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<BaseResponse<GetRoleDto>> GetRoleByIdAsync(int roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<List<GetRoleDto>>> GetRoleByPaginationAsync(BasePagination request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetRoleDto>> CreateRoleAsync(CreateRoleDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetRoleDto>> UpdateRoleByIdAsync(int roleId, UpdateRoleDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetRoleDto>> DeleteRoleByIdAsync(int roleId)
    {
        throw new NotImplementedException();
    }
}