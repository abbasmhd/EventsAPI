using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsAPI.Helpers
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(params string[] errors)
        {
            return new Result(false, errors);
        }
    }

    public class Result<T> : Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors, T data)
            : base(succeeded, errors)
        {
            Data = data;
        }

        public T Data { get; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, Array.Empty<string>(), data);
        }

        public static Result<T> Failure(params string[] errors)
        {
            return new Result<T>(false, errors, default);
        }

        public static implicit operator Result<T>(T data)
        {
            return new Result<T>(true, Array.Empty<string>(), data);
        }

    }
}
