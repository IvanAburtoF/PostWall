﻿namespace PostWall.API.Models.DTO.User;

public class RegisterUserDTO
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}