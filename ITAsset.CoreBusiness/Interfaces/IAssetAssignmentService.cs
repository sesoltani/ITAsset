using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface IAssetAssignmentService
{
    Task AssignAsync(
        int assetId,
        int employeeId,
        int assignedByUserId,
        AssetCondition conditionOnAssign,
        DateTime? expectedReturnDate);

    Task ReturnAsync(
        int assignmentId,
        int performedByUserId,
        AssetCondition conditionOnReturn);
}