namespace Reface.EventBus
{
    /// <summary>
    /// 表示事件允许执行一个值来确定它的执行顺序
    /// </summary>
    public interface IPrioritized
    {
        /// <summary>
        /// 优先级，执行时将会按照从小到大的顺序进行执行
        /// </summary>
        int Priority { get; }
    }
}
