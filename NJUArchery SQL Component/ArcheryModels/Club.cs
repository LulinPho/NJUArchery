using System;
using System.Collections.Generic;

namespace NJUArchery_SQL_Component.ArcheryModels;

public partial class Club
{
    public int ClubId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
