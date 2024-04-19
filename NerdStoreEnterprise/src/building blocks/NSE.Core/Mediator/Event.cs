using MediatR;
using NSE.Core.Messages;

namespace NSE.Core.Mediator;

public class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.Now;
    }
}