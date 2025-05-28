using Microsoft.AspNetCore.Mvc;

namespace EtlProject.Core.Dtos;

public class CustomProblemDetails : ProblemDetails
{
    public string ErrorCode { get; set; }
    public IEnumerable<object> Errors { get; set; }
}