namespace Reface.Core.EventBus
{
    /// <summary>
    /// Implement this interface means the EventListener has execute priority
    /// </summary>
    public interface IPrioritized
    {
        /// <summary>
        /// EventListener will be executed first when this value is small
        /// </summary>
        int Priority { get; }
    }
}
