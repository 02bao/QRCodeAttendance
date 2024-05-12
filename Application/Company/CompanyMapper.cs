using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Company;

public static class CompanyMapper
{
    public static CompanyDTO ToDTO(this  SqlCompany entity)
    {
        return new CompanyDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Images = entity.Images?.Path ?? "",
            StartTime = entity.StartTime,
            MaxLateTime = entity.MaxLateTime,
        };
    }
}
