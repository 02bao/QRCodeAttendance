namespace QRCodeAttendance.Application.Dashboard;

public class DashboardDTO
{
}
public class StatEmployee
{
    public int TotalEmployee { get; set; } = 0;
    public int WomanCount { get; set; } = 0;
    public int ManCount { get => TotalEmployee - WomanCount; }
}

public class StatDepartment
{
    public string Department { get; set; } = "";
    public int TotalEmployee { get; set; } = 0;
    public int TotalAttendance { get; set; } = 0;
}

public class DailyAttendanceStat
{
    public int OnTimeCount { get; set; }
    public int LateCount { get; set; }
    public int AbsentCount { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow.Date;
}