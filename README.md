# EventBus

This is an EventBus which refered the `ApplicationContext.publish(message)`, which is used to publish an Event in `Java Spring`.

Normally, EventHandler should be coupled with the Event directly, which is not a good pratice for multi-solution application.
This lib provides another different use-case of the Event-Listener parttern: you can separate your EventHandler and Event into different solution or project, and the EventBus can dispatch the event to the **corresponding** EventHandlers.

**What is a correspocorresponding EventHandler?**

_Suppose_ you have an event named `BaseEvent`,
There is no doubt an EventHandler like the following code can be invoked by the EventBus
```csharp
public class BaseHandler: IEventListener<BaseEvent>
```

Further for, if you have another Event which is an extend of `BaseEvent`, take the following code as an example:
```csharp
public class FooEvent: BaseEvent
```
It is not only a `FooEvent`, but also a `BaseEvent`, so the EventListener above can handle it as well.

This is the characteristic of this library

## 1 Basic

### 1.1 Install

You can install it via `nuget`

* [.NetFramework](https://www.nuget.org/packages/Reface.EventBus/)
* [.NetCore](https://www.nuget.org/packages/Reface.Core.EventBus/3.2.11)

### 1.2 Runtime

* Reface.EventBus is working based on .NetFramework
* Reface.Core.EventBus is working based on .NetCore

### 1.3 Dependencies

Reface.EventBus 
- _Nothing_

Reface.Core.EventBus 
- Microsoft.Extensions.DependencyInjection

## 2 Usage

### 2.1 Publish a Message

**Define a New Event**

```csharp
/// <summary>
/// This Event will be publish after the console started
/// </summary>
public class ConsoleStarted : Reface.EventBus.Event
{
    public ConsoleStarted(object source) : base(source)
    {
        Console.WriteLine("Console Started");
    }
}
```

**Publish Event**

```csharp
class Program
{
    static void Main(string[] args)
    {
        // Create an instance of EventBus
        IEventBus eventBus = new DefaultEventBus();

        // Publish the message
        eventBus.Publish(new ConsoleStarted());
    }
}
```
### 2.2 Event Handler


#### 2.2.1 How to listener an event

And class who implements the interface `IEventListener<TEvent>` would be an event listener.

```csharp
using ConsoleApp1.Events;
using Reface.EventBus;

namespace ConsoleApp1.Listeners
{
    public class OnConsoleStarted : IEventListener<ConsoleStarted>
    {
        public void Handle(ConsoleStarted @event)
        {
            Console.WriteLine("Console Started");
        }
    }
}
```
#### 2.2.2 How to register a Listener

In order to decouple the code,
You can register the Listener in the config file

**Register listener in web.config / app.config**
1. add section
2. add listener
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="eventBus" type="Reface.EventBus.Configuration.EventBusSection, Reface.EventBus"/>
  </configSections>
  <eventBus>
    <listeners>
      <add type="ConsoleApp1.Listeners.OnConsoleStarted, ConsoleApp1" />
    </listeners>
  </eventBus>
</configuration>
```
#### 2.2.3 其它方法添加监听者

除了通过 config 文件的方法，我们还提供了其它方法来注册监听者。

只要实现了 **Reface.EventBus.IEventListenerFinder** 并在构造 DefaultEventBus 时作为参数传入，便可以订制的方式注册监听者。
目前自带的注册方式有：
* **Reface.EventBus.EventListenerFinders.ConfigurationEventListenerFinder** 通过 config 文件来注册
* **Reface.EventBus.EventListenerFinders.AssembliesEventListenerFinder** 通过注册程序集，并返反射其中的类型来得到所有实现了 **Reface.EventBus.IEventListenerFinder** 的成员
* **Reface.EventBus.EventListenerFinders.DefaultEventListenerFinder** 通过编码的方式注册监听者

#### 2.2.4 定义执行顺序

向 **IEventListener<TEvent>** 的实现类再添加 IPrioritized 接口，并实现 **Priority** 属性，便可以指定执行的顺序。
* Priority 的值越小，越先执行
* 未实现 IPrioritized 的 IEventListener 认为 Priority = 0

# 3. 集成

## 3.1 .NetCore 与 ServiceCollection 集成

与 *IOC/DI* 组件的集成，可以免去对监听者一一注册的过程。
在 .NetCore 中，通过为 ServiceCollection 注册必要组件和按程序集注册监听器，可以实现这些功能：

```csharp
var provider = new ServiceCollection()
  .AddEventBus() // 添加 EventBus 功能
  .AddEventListeners(this.GetType().Assembly)
  .AddEventListeners(typeof(IService).Assembly)
  .BuildServiceProvider();
IEventBus eventBus = provider.GetService<IEventBus>();
eventBus.Publish(new TestEvent());
```
