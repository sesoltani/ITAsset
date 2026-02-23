using ITAsset.Data.Data;
using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class AssetAssignmentService : IAssetAssignmentService
{
    private readonly AppDbContext _context;

    public AssetAssignmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AssignAsync(
        int assetId,
        int employeeId,
        int assignedByUserId,
        AssetCondition conditionOnAssign,
        DateTime? expectedReturnDate)
    {
        using var tx = await _context.Database.BeginTransactionAsync();

        try
        {
            var asset = await _context.Assets.FindAsync(assetId);

            if (asset == null)
                throw new InvalidOperationException("دارایی یافت نشد.");

            if (asset.Status == AssetStatus.Assigned)
                throw new InvalidOperationException("این دارایی قبلاً تحویل داده شده است.");

            var assignment = new AssetAssignment
            {
                AssetId = assetId,
                EmployeeId = employeeId,
                AssignedByUserId = assignedByUserId,
                ConditionOnAssign = conditionOnAssign,
                ExpectedReturnDate = expectedReturnDate
            };

            _context.AssetAssignments.Add(assignment);

            // 🔥 بخش مهم
            asset.Status = AssetStatus.Assigned;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }


    public async Task ReturnAsync(
        int assignmentId,
        int performedByUserId,
        AssetCondition conditionOnReturn)
    {
        using var tx = await _context.Database.BeginTransactionAsync();

        try
        {
            var assignment = await _context.AssetAssignments
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(a =>
                    a.Id == assignmentId &&
                    a.ActualReturnDate == null);

            if (assignment == null)
                throw new InvalidOperationException("Assignment فعال پیدا نشد.");

            assignment.ActualReturnDate = DateTime.UtcNow;
            assignment.ConditionOnReturn = conditionOnReturn;

            // 🔥 اینجا وضعیت برگرده به Available
            assignment.Asset!.Status = AssetStatus.Available;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }


    public async Task<AssetAssignment?> GetActiveAssignmentAsync(int assetId)
    {
        return await _context.AssetAssignments
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a =>
                a.AssetId == assetId && a.ActualReturnDate == null);
    }

}