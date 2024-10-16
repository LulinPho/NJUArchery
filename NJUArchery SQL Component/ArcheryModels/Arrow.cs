using System;
using System.Collections.Generic;

namespace NJUArchery_SQL_Component.ArcheryModels;

public partial class Arrow
{
    public int ArrowId { get; set; }

    public int? RoundId { get; set; }

    public sbyte? ArrowNum { get; set; }

    public string? Score { get; set; }

    public virtual Round? Round { get; set; }
}
