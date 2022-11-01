namespace Ocdata.Operations.Helpers.ResponseHelper
{
    public class Result<T>
    {
        internal Result(bool succeeded, IEnumerable<string> errors, object data)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Data = (T)data;
        }

        public bool Succeeded { get; set; }

        public T Data { get; set; }

        public string[] Errors { get; set; }

        public static async Task<Result<T>> SuccessAsync(object data = null)
        {
            return await Task.Run(() =>
            {
                return new Result<T>(true, Array.Empty<string>(), (T)data);
            });
        }

        public static async Task<Result<T>> FailureAsync(IEnumerable<string> errors)
        {
            return await Task.Run(() =>
            {
                return new Result<T>(false, errors, null);
            });
        }

        public static async Task<Result<T>> FailureAsync(string error)
        {
            return await Task.Run(() =>
            {
                return new Result<T>(false, new List<string>() { error }, null);
            });
        }
    }
}
