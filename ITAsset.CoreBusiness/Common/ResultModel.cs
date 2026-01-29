namespace ITAsset.Domain.Common;

public class ResultModel<T>
{
    public bool IsSuccess { get; set; } // موفقیت یا شکست
    public string Message { get; set; } = string.Empty; // پیام برای UI
    public T? Data { get; set; } // داده (اختیاری)

    // متدهای کمک کننده برای راحتی
    public static ResultModel<T> Success(T data, string message = "") =>
        new ResultModel<T> { IsSuccess = true, Data = data, Message = message };

    public static ResultModel<T> Fail(string message = "") =>
        new ResultModel<T> { IsSuccess = false, Message = message };
}