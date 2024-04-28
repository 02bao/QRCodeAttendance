namespace QRCodeAttendance.Application.Department
{
    public class DepartmentDTO
    {
    }
    public class DepartmentCreate
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public class DepartmentUpdate
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int TotalEmployees { get; set; } = 0;
        public int TotalPositions { get; set; } = 0;
    }

}
