﻿namespace HahnSoftware.API.DTO.Authentication;

public class UserLogin
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; } = false;
}
