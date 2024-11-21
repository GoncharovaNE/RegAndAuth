using System;
using System.Collections.Generic;

namespace RegAndAuth.Models;

public partial class User
{
    public int Iduser { get; set; }

    public string Fio { get; set; } = null!;

    public string Login { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public byte[]? Image { get; set; }

    public int Role { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;
}
