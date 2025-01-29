using System.Threading.Tasks;

namespace FillWords.Root.ServiceInterfaces
{
    public interface IPrewarmableService
    {
        public Task Prewarm();
    }
}
