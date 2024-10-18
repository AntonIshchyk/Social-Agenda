using Microsoft.AspNetCore.Mvc;

[Route("Calender - Website")]
public class AttendanceControllers : Controller
{
    readonly AttendanceService AS;
    public AttendanceControllers(AttendanceService attendanceService)
    {
        AS = attendanceService;
    }

    [HttpPost("attend")]
    [LoggedInFilter]
    public async Task<IActionResult> MakeAttendance(Attendance attendance)
    {
        if (attendance is null) return BadRequest("No data found. ");
        if (attendance.Date == "") return BadRequest("Please, give a date next time. ");

        Guid UserId;
        bool converted = Guid.TryParse(HttpContext.Session.GetString("UserId"), out UserId);
        if (!converted) return BadRequest("Something went wrong. ");
        attendance.UserId = UserId;
        attendance.Id = Guid.NewGuid();

        bool IsAttendanceSaved = await AS.SaveAttendance(attendance);
        if (!IsAttendanceSaved) return BadRequest("Attendance is not saved. ");
        return Created();
    }

    [HttpGet("check-own-attendances")]
    [LoggedInFilter]
    public async Task<IActionResult> GetOwnAttendances()
    {
        Guid Id;
        bool converted = Guid.TryParse(HttpContext.Session.GetString("UserId"), out Id);
        if (!converted) return BadRequest("Something went wrong. ");

        List<Attendance> attendancesOfUser = await AS.GetAttendancesOfUser(Id);
        return Ok(attendancesOfUser.ToArray());
    }
}