using System;
using System.Collections.Generic;

namespace Archery_Competition_Webserver.ArcheryManagementModels;

public partial class Qualifying
{
    public int QualifyingId { get; set; }

    public int? CompetitionId { get; set; }

    public int? PlayerId { get; set; }

    public short? ScoreSum { get; set; }

    public sbyte? ArrowSum { get; set; }

    public short? Ranking { get; set; }
}
