using System;

namespace ConsoleApp1
{
    public interface IEventBase
    {
        /// <summary>
        ///     事件发布时间
        /// </summary>
        DateTimeOffset EventAt { get; }

        /// <summary>
        ///     事件编号
        /// </summary>
        string EventId { get; }
    }
}