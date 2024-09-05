using System;

namespace USERS.API.Models;

public class Token
{
    public required string AccessToken { get; set; }
    public DateTime Expires { get; set; }
}
