using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

public partial class AssetProjectContext : DbContext
{
    public AssetProjectContext()
    {
    }

    public AssetProjectContext(DbContextOptions<AssetProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetRequest> AssetRequests { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAssetsView> EmployeeAssetsViews { get; set; }

    public virtual DbSet<EmployeeHavingAsset> EmployeeHavingAssets { get; set; }

    public virtual DbSet<VwAssetRequestsAndEmployeeInfo> VwAssetRequestsAndEmployeeInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AARYAMANSPC\\SQLEXPRESS;Database=AssetProject;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.ToTable("Asset");

            entity.Property(e => e.AssetId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Asset_Id");
            entity.Property(e => e.AssetName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Asset_name");
            entity.Property(e => e.MakeCompany)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Make_Company");
            entity.Property(e => e.ModelNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Model_No");
            entity.Property(e => e.SerialNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Serial_No");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AssetRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.ToTable("Asset_Request");

            entity.Property(e => e.RequestId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Request_Id");
            entity.Property(e => e.AssetDescription)
                .IsUnicode(false)
                .HasColumnName("Asset_description");
            entity.Property(e => e.DateOfRequest)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_request");
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Emp_Id");
            entity.Property(e => e.Reason).IsUnicode(false);
            entity.Property(e => e.ReqAssetName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ReqAsset_Name");

            entity.HasOne(d => d.Emp).WithMany(p => p.AssetRequests)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_Request_Employee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Id");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Name");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeAssetsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmployeeAssetsView");

            entity.Property(e => e.AssetId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Asset_Id");
            entity.Property(e => e.AssetName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Asset_name");
            entity.Property(e => e.DateOfAssign)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_assign");
            entity.Property(e => e.DateOfRequest)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_request");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Id");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Name");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReqId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Req_Id");
        });

        modelBuilder.Entity<EmployeeHavingAsset>(entity =>
        {
            entity.HasKey(e => e.AssetId);

            entity.ToTable("Employee_having_assets");

            entity.Property(e => e.AssetId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Asset_Id");
            entity.Property(e => e.DateOfAssign)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_assign");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Id");
            entity.Property(e => e.ReqId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Req_Id");
        });

        modelBuilder.Entity<VwAssetRequestsAndEmployeeInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_AssetRequestsAndEmployeeInfo");

            entity.Property(e => e.AssetDescription)
                .IsUnicode(false)
                .HasColumnName("Asset_description");
            entity.Property(e => e.DateOfRequest)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_request");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Emp_Id");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Name");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Reason).IsUnicode(false);
            entity.Property(e => e.ReqAssetName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ReqAsset_Name");
            entity.Property(e => e.RequestId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Request_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
