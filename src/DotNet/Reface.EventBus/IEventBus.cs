namespace Reface.EventBus
{
    public interface IEventBus
    {
        /// <summary>
        /// Publish a event to related EventListeners
        /// </summary>s
        /// <param name="event"></param>
        void Publish(Event @event);
    }
}
