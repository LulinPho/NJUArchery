using System;
using System.Collections.Generic;

namespace NJUArchery_SQL_Component.ArcheryModels;

public partial class Round
{
    public int RoundId { get; set; }

    public int? GameId { get; set; }

    public int? PlayerId { get; set; }

    public sbyte? RoundNum { get; set; }

    public sbyte? ScoreSum { get; set; }

    public virtual ICollection<Arrow> Arrows { get; set; } = new List<Arrow>();

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
