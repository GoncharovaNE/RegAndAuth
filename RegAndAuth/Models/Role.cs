using System;
using System.Collections.Generic;

namespace RegAndAuth.Models;

public partial class Role
{
    public int Idrole { get; set; }

    public string Namerole { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
