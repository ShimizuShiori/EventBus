namespace Reface.EventBus
{
    public interface IDefaultEventBusOptions
    {
        ICache Cache { get;  }
        IEventListenerTypeFinder EventListenerFinder { get;  }
        IMapper Mapper { get;  }
    }
}