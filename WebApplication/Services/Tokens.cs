using System;

namespace WebApplication.Services
{
    public sealed class RefreshToken
    {
        public string Token { get; set; }
        
        public DateTime Expires { get; set; }
        
        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
    
    internal sealed class AuthResponse
    {
        public string Password { get; set; }
        
        public RefreshToken LatestRefreshToken { get; set; }
    }
    
    public sealed class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

}