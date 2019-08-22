using System.Collections.Generic;
using System.Linq;

namespace HealthyWork.API.Contracts.Models
{
    public class ResultData<T> where T : class
    {
        public T Content { get; set; }

        public List<Result> Results { get; }

        public bool HasErrors { get; set; } = false;

        public ResultData()
        {
            Results = new List<Result>();
        }

        public void AddResult(int code, string description, ResultType type)
        {
            if (type == ResultType.Error || type == ResultType.Exception) HasErrors = true;
            Results.Add(new Result() { Code = code, Description = description, Type = type });
        }

        public void AddResultRange(List<Result> lista)
        {
            if (lista.FirstOrDefault(x => x.Type == ResultType.Error || x.Type == ResultType.Exception) != null) HasErrors = true;
            Results.AddRange(lista);
        }

    }
}
public class Result
{
    public int Code { get; set; }
    public string Description { get; set; }
    public ResultType Type { get; set; }


}
public enum ResultType
{
    Success,
    Error,
    Exception
}

