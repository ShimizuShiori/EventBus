namespace Reface.EventBus
{
    public interface IDefaultEventBusOptions
    {
        ICache Cache { get;  }
        IEventListenerFinder EventListenerFinder { get;  }
        IMapper Mapper { get;  }
    }
}