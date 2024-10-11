using System;
using System.Collections.Generic;

namespace Archery_Competition_Webserver.AcheryModels;

public partial class Bow
{
    public int BowId { get; set; }

    public string? Name { get; set; }

    public string? Tension { get; set; }

    public int? PlayerId { get; set; }

    public int? ClubId { get; set; }

    public string? Type { get; set; }

    public virtual Club? Club { get; set; }

    public virtual Player? Player { get; set; }
}
