﻿namespace QRCodeAttendance.Presentation.Models;

public class DepartmentModel
{
}
public class DepartmentCreateModel
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}
public class DepartmentUpdateModel
{
    public string? Name { get; set; } = "";
    public string? Description { get; set; } = "";
}