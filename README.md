# 事件总线

这是一个参照 *Java Sprint* 中的 ApplicationContext.publish(message) 的方法实现的事件总线

## 1 基本信息

### 1.1 安装

```cmd
PM> Install-Package Reface.EventBus -Version 3.2.0
```

### 1.2 运行环境

* DotNet 4.0

### 1.3 依赖项

* 无

## 2 使用方法

### 2.1 什么是事件总线



### 2.2 消息发布

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
### 2.3 消息监听


#### 2.3.1 如何监听

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
#### 2.3.2 如何添加监听者

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
#### 2.3.3 其它方法添加监听者

除了通过 config 文件的方法，我们还提供了其它方法来注册监听者。

只要实现了 **Reface.EventBus.IEventListenerFinder** 并在构造 DefaultEventBus 时作为参数传入，便可以订制的方式注册监听者。
目前自带的注册方式有：
* **Reface.EventBus.EventListenerFinders.ConfigurationEventListenerFinder** 通过 config 文件来注册
* **Reface.EventBus.EventListenerFinders.AssembliesEventListenerFinder** 通过注册程序集，并返反射其中的类型来得到所有实现了 **Reface.EventBus.IEventListenerFinder** 的成员
* **Reface.EventBus.EventListenerFinders.DefaultEventListenerFinder** 通过编码的方式注册监听者

#### 2.3.4 定义执行顺序

向 **IEventListener<TEvent>** 的实现类再添加 IPrioritized 接口，并实现 **Priority** 属性，便可以指定执行的顺序。
* Priority 的值越小，越先执行
* 未实现 IPrioritized 的 IEventListener 认为 Priority = 0

# 3 与 autofac 集成

比较简单的方法是使用 Autofac 先将 DefaultEventBus 注册，再以程序集的形式注册所有继承于 Event 的类型。
然后创建一个新的 **Reface.EventBus.IEventListenerFinder** 实现类，大致如下：

```c#
    public class AutofacEventListenerFinder : IEventListenerFinder
    {
        private readonly ILifetimeScope lifetimeScope;

        public AutofacEventListenerFinder(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IEnumerable<IEventListener> CreateAllEventListeners()
        {
            return this.lifetimeScope.Resolve<IEnumerable<IEventListener>>();
        }
    }
```

这样就可以通过 Autofac 的容器创建 IEventBus 实例，然后 Pulish 一个事件，被注册的程序集中的所有 Listener 就都会被触发