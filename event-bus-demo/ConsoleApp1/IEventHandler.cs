using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IEventHandler<in TEvent> where TEvent : IEventBase
    {
        /// <summary>
        ///     事件处理着
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task Handle(TEvent @event);
    }
}