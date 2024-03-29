using Microsoft.Extensions.Logging;
using System;

namespace TgBotFramework.UpdatePipeline
{
    public interface IBotPipelineBuilder<TContext> where TContext : IUpdateContext
    {
        ILogger<IBotPipelineBuilder<TContext>> Logger { get; }

        IBotPipelineBuilder<TContext> Use(Func<UpdateDelegate<TContext>, UpdateDelegate<TContext>> middleware);
        IBotPipelineBuilder<TContext> Use<THandler>()
            where THandler : IUpdateHandler<TContext>;
        IBotPipelineBuilder<TContext> Use<THandler>(THandler handler)
            where THandler : IUpdateHandler<TContext>;


        IBotPipelineBuilder<TContext> UseWhen<THandler>(Predicate<TContext> predicate)
            where THandler : IUpdateHandler<TContext>;
        IBotPipelineBuilder<TContext> UseWhen(Predicate<TContext> predicate,
            Action<IBotPipelineBuilder<TContext>> configure);


        IBotPipelineBuilder<TContext> MapWhen(Predicate<TContext> predicate,
            Action<IBotPipelineBuilder<TContext>> configure);
        IBotPipelineBuilder<TContext> MapWhen<THandler>(Predicate<TContext> predicate)
            where THandler : IUpdateHandler<TContext>;

        IBotPipelineBuilder<TContext> UseCommand<TCommand>(string command)
            where TCommand : CommandBase<TContext>;

        UpdateDelegate<TContext> Build();
    }
}