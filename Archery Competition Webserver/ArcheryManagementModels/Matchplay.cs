using System;
using System.Collections.Generic;

namespace Archery_Competition_Webserver.ArcheryManagementModels;

public partial class Matchplay
{
    public int MatchplayId { get; set; }

    public int? CompetitionId { get; set; }

    public int? PlayerId1 { get; set; }

    public int? PlayerId2 { get; set; }

    public int? WinnerId { get; set; }

    public sbyte? PlayerScore1 { get; set; }

    public sbyte? PalyerScore2 { get; set; }
}
