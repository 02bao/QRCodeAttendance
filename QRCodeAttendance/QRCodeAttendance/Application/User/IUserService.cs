﻿
namespace QRCodeAttendance.Application.User;

public interface IUserService
{
    Task<UserAuthenticate> Login(string Email, string Password);
}