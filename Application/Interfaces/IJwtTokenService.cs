﻿namespace HealthMed.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string email, string role);
    }

}
