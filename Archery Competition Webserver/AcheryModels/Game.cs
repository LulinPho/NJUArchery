using System;
using System.Collections.Generic;

namespace Archery_Competition_Webserver.AcheryModels;

public partial class Game
{
    public int GameId { get; set; }

    public int? CompetitionId { get; set; }

    public string? GameType { get; set; }

    /// <summary>
    /// 
    /// 
    /// </summary>
    public int? PlayerAId { get; set; }

    public int? PlayerBId { get; set; }

    /// <summary>
    /// qualification
    /// </summary>
    public short? ScoreSum { get; set; }

    /// <summary>
    /// qualification
    /// </summary>
    public string? Half { get; set; }

    public double? Distance { get; set; }

    public int? TargetType { get; set; }

    /// <summary>
    /// matchplay
    /// </summary>
    public sbyte? ASetScore { get; set; }

    /// <summary>
    /// matchplay
    /// </summary>
    public sbyte? BSetScore { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual Player? PlayerA { get; set; }

    public virtual Player? PlayerB { get; set; }

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}
