using System.Collections.Generic;
using System.Linq;

namespace EventsAPI.Helpers
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<ErrorModel> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public IList<ErrorModel> Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new List<ErrorModel>());
        }

        public static Result Failure(params ErrorModel[] errors)
        {
            return new Result(false, errors);
        }
    }

    public class Result<T> : Result
    {
        internal Result(bool succeeded, IEnumerable<ErrorModel> errors, T data)
            : base(succeeded, errors)
        {
            Data = data;
        }

        public T Data { get; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, new List<ErrorModel>(), data);
        }

        public static Result<T> Failure(params ErrorModel[] errors)
        {
            return new Result<T>(false, errors, default);
        }

        public static implicit operator Result<T>(T data)
        {
            return new Result<T>(true, new List<ErrorModel>(), data);
        }

    }
}
