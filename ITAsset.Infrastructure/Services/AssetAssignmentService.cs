using ITAsset.Data.Data;
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

    public async Task AssignAsync(int assetId,
        int employeeId,
        int assignedByUserId,
        AssetCondition conditionOnAssign,
        DateTime? expectedReturnDate)
    {
        // در صورت بروز هر خطا، هیچ تغییری در دیتابیس ذخیره نخواهد شد.
        using var tx = await _context.Database.BeginTransactionAsync();

        try
        {
            var hasActiveAssignment = await _context.AssetAssignments
                .AnyAsync(a => a.AssetId == assetId && a.ActualReturnDate == null);

            if (hasActiveAssignment)
                throw new InvalidOperationException("این دارایی در حال حاضر تحویل داده شده است.");

            var assignment = new AssetAssignment
            {
                AssetId = assetId,
                EmployeeId = employeeId,
                AssignedByUserId = assignedByUserId,
                ConditionOnAssign = conditionOnAssign,
                ExpectedReturnDate = expectedReturnDate
            };

            _context.AssetAssignments.Add(assignment);
            await _context.SaveChangesAsync();

            // در صورت بروز هر خطا، هیچ تغییری در دیتابیس ذخیره نخواهد شد.

            await tx.CommitAsync();
        }
        catch (Exception)
        {
            await tx.RollbackAsync();
            throw;
        }


    }

    public async Task ReturnAsync(int assignmentId,
        int performedByUserId,
        AssetCondition conditionOnReturn)
    {
        using var tx = await _context.Database.BeginTransactionAsync();

        try
        {
            var assignment = await _context.AssetAssignments
                .FirstOrDefaultAsync(a => a.Id == assignmentId && a.ActualReturnDate == null);

            if (assignment == null)
                throw new InvalidOperationException("Assignment فعال پیدا نشد.");

            assignment.ActualReturnDate = DateTime.UtcNow;
            assignment.ConditionOnReturn = conditionOnReturn;

            var history = new AssetAssignmentHistory
            {
                AssetId = assignment.AssetId,
                EmployeeId = assignment.EmployeeId,
                Action = AssignmentAction.Return,
                ConditionOnReturn = conditionOnReturn,
                PerformedByUserId = performedByUserId,
                Description = "عودت دارایی از کارمند"
            };
            _context.AssetAssignmentHistories.Add(history);

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}