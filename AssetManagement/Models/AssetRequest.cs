﻿using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class AssetRequest
{
    public string RequestId { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public string ReqAssetName { get; set; } = null!;

    public string AssetDescription { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public DateTime DateOfRequest { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
