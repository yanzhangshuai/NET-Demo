using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public interface IEventStore
    {
        bool AddSubscription<TEvent, TEventHandler>()
            where TEvent : IEventBase
            where TEventHandler : IEventHandler<TEvent>;

        bool RemoveSubscription<TEvent, TEventHandler>()
            where TEvent : IEventBase
            where TEventHandler : IEventHandler<TEvent>;

        bool HasSubscription<TEvent>() where TEvent : IEventBase;

        ICollection<Type> GetEventHandlerTypes<TEvent>() where TEvent : IEventBase;

        bool Clean();

        string GetEventKey<TEvent>();
    }
}