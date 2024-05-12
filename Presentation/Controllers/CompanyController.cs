using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Company;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Presentation.Filters;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class CompanyController(
    ICompanyService _companyService) : BaseController
{
    [HttpPost("")]
    [Role("Admin")]
    public async Task<IActionResult> CreateCompany(CompanyCreateModel Model)
    {
        bool IsSuccess = await _companyService.CreateNew(Model.Name, Model.Email, Model.StartTime, Model.MaxLateTime);
        return IsSuccess ? Ok(Model) : BadRequest();
    }

   
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        List<CompanyDTO> dtos = await _companyService.GetAll();
        return Ok(dtos);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult>? GetById(long Id)
    {
        CompanyDTO? dto = await _companyService.GetById(Id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(long Id, CompanyUpdateModel Model)
    {

        bool IsSuccess = await _companyService.Update(Id, Model.Name,Model.Email, Model.StartTime, Model.MaxLateTime,Model.FileId );
        return IsSuccess ? Ok(Model) : BadRequest();
    }

    [HttpDelete("{id}")]
    [Role("Admin")]
    public async Task<IActionResult> Delete(long id)
    {
        bool success = await _companyService.Delete(id);
        return success ? Ok() : BadRequest();
    }
}
