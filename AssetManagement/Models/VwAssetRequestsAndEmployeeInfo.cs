using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class VwAssetRequestsAndEmployeeInfo
{
    public string RequestId { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public string ReqAssetName { get; set; } = null!;

    public string AssetDescription { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public DateTime DateOfRequest { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Department { get; set; } = null!;

}

