﻿using System;
using System.Collections.Generic;

namespace NJUArchery_SQL_Component.ArcheryModels;

public partial class Competition
{
    public int CompetitionId { get; set; }

    public string? Title { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public short? PlayerNum { get; set; }

    public string? Hosting { get; set; }

    public string? CompetitionLevel { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
