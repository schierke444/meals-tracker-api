﻿namespace Auth.API.Models;

public class UserDetailsDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
