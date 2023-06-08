namespace CarRentX.Utility.BaseResponse
{
	public class BaseResponse<TData>
	{
		public bool Status { get; set; }
		public string? Message { get; set; }
		public TData? Data { get; set; }

		public static BaseResponse<TData> Success(TData data, string message)
		{
			return new BaseResponse<TData> {Status = true, Message = message, Data = data};
		}
		public static BaseResponse<TData> Error(string message)
		{
			return new BaseResponse<TData> { Status = false, Message = message };
		}
	}
}
