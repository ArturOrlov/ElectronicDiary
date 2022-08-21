using ElectronicDiary.Entities.Base;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDiary.Extension;

public class ControllerBaseExtension : ControllerBase
{
    protected new ObjectResult Response<T>(BaseResponse<T> response) where T : class
    {
        if (response.IsError)
        {
            return BadRequest(response.Description);
        }

        return Ok(response.Data);
    }
}