﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeStash.Application;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                          Exception exception,
                                          CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails()
        {
            Instance = httpContext.Request.Path,
            Status = httpContext.Response.StatusCode,
            Detail = exception.Message
        };

        logger.LogError(exception, "{@ProblemDetails}", problemDetails);

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }
}