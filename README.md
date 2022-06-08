# 事件总线

这是一个参照 *Java Spring* 中的 ApplicationContext.publish(message) 的方法实现的事件总线

## 1 基本信息

### 1.1 安装

通过 Nuget 你就可以安装使用它。

* [.NetFramework](https://www.nuget.org/packages/Reface.EventBus/)
* [.NetCore](https://www.nuget.org/packages/Reface.Core.EventBus/3.2.11)

### 1.2 运行环境

Reface.EventBus 工作在 .NetFramework 上

Reface.Core.EventBus 工作在 .NetCore 上

### 1.3 依赖项

Reface.EventBus 无依赖项

Reface.Core.EventBus 依赖 Microsoft.Extensions.DependencyInjection

## 2 使用方法


### 2.1 消息发布

**定义事件**
```csharp
/// <summary>
/// 控制台启动后的事件
/// </summary>
public class ConsoleStarted : Reface.EventBus.Event
{
    public ConsoleStarted(object source) : base(source)
    {
        Console.WriteLine("控件台启动完毕");
    }
}
```

**在控制台启动后触发事件**
```csharp
class Program
{
    static void Main(string[] args)
    {
        // 构造事件总线
        IEventBus eventBus = new DefaultEventBus();

        // 发布消息
        eventBus.Publish(new ConsoleStarted());
    }
}
```
### 2.2 消息监听


#### 2.2.1 如何监听

实现 IEventListener<TEvent> 即可成为监听者

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
#### 2.2.2 如何添加监听者

为了松耦合，我们不会要求手动的将监听者添加到 EventBus 实例中去。
比较简单的方法是通过 config 文件注册监听者。

**在配置文件中定义事件监听者**
1. 添加 section
2. 添加监听者
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
