﻿
namespace QRCodeAttendance.Application.Dashboard;

public interface IDashboardService
{
    Task<StatEmployee> GetStatisticEmployee();
    Task<List<StatDepartment>> GetStatisticDepartment();
    Task<DailyAttendanceStat> GetDailyAttendanceStat(DateTime date);
}