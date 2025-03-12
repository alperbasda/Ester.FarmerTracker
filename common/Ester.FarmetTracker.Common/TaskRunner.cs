using Microsoft.Extensions.DependencyInjection;

namespace Ester.FarmetTracker.Common;

public interface ITaskRunner
{
    void RunAsync(Func<IServiceProvider, Task> taskFunc, Action<Exception>? onError = null);
}

public class TaskRunner : ITaskRunner
{
    private readonly IServiceScopeFactory _providerFactory;
    public TaskRunner(IServiceScopeFactory providerFactory)
    {
        _providerFactory = providerFactory;
    }
    public void RunAsync(Func<IServiceProvider, Task> taskFunc, Action<Exception>? onError = null)
    {
        Task.Run(async () =>
        {
            try
            {
                using var scope = _providerFactory.CreateScope();
                var scopedProvider = scope.ServiceProvider;
                if (taskFunc == null)
                    throw new ArgumentNullException(nameof(taskFunc));

                await taskFunc(scopedProvider);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex); // Hata yönetimi
            }
        });
    }
}

