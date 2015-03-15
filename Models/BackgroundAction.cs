using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    public abstract class BackgroundAction
    {
        protected abstract void InvokeCoreAsync();

        public Task InvokeAsync()
        {
            return Task.Run(() => InvokeCoreAsync());
        }
    }
}