using Reface.EventBus;

namespace Demo.Autofac.Events
{
    public class ModelCreatedEvent<TModel> : Event
    {
        public TModel Model { get; private set; }
        public ModelCreatedEvent(object source,TModel model) : base(source)
        {
            this.Model = model;
        }
    }
}
