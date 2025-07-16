﻿namespace Activities.Application.Messaging;
public interface IPipelineBehavior<in TInput, TOutput>
{
    Task<TOutput> HandleAsync(TInput input, Func<Task<TOutput>> next, CancellationToken cancellationToken = default);
}