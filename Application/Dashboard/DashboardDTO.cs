﻿namespace QRCodeAttendance.Application.Dashboard;

public class DashboardDTO
{
}
public class StatEmployee
{
    public int TotalEmployee { get; set; } = 0;
    public int WomanCount { get; set; } = 0;
    public int ManCount { get => TotalEmployee - WomanCount; }
}