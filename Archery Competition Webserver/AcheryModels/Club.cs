﻿using System;
using System.Collections.Generic;

namespace Archery_Competition_Webserver.AcheryModels;

public partial class Club
{
    public int ClubId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Bow> Bows { get; set; } = new List<Bow>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
