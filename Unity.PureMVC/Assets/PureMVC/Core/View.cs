using UnityEngine;

namespace PureMVC.Core
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns;
    using System;
    using System.Collections.Generic;

    public class View : IView
    {
        protected static volatile IView m_instance;
        //1.字典记录已注册的中介者(key是中介者名字，value是中介者接口)
        protected IDictionary<string, IMediator> m_mediatorMap = new Dictionary<string, IMediator>();
        //2.字典记录已注册的观察者(key是事件名称,value是观察者接口)
        protected IDictionary<string, IList<IObserver>> m_observerMap = new Dictionary<string, IList<IObserver>>();
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();

        protected View()
        {
            this.InitializeView();
        }

        public virtual bool HasMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_mediatorMap.ContainsKey(mediatorName);
            }
        }

        protected virtual void InitializeView()
        {
        }

        //4.核心方法通知观察者的方法NotifyObervers
        public virtual void NotifyObservers(INotification notification)
        {
            IList<IObserver> list = null;
            lock (this.m_syncRoot)
            {
                if (this.m_observerMap.ContainsKey(notification.Name))
                {
                    IList<IObserver> collection = this.m_observerMap[notification.Name];
                    list = new List<IObserver>(collection);
                }
            }
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].NotifyObserver(notification);
                }
            }
        }
        //3.提供注册/注销中介者和观察者的方法
        public virtual void RegisterMediator(IMediator mediator)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_mediatorMap.ContainsKey(mediator.MediatorName))
                {
                    return;
                }
                this.m_mediatorMap[mediator.MediatorName] = mediator;
                IList<string> list = mediator.ListNotificationInterests();
                if (list.Count > 0)
                {
                    IObserver observer = new Observer("handleNotification", mediator);
                    for (int i = 0; i < list.Count; i++)
                    {
                        //将方法名注册给观察者
                        this.RegisterObserver(list[i].ToString(), observer);
                    }
                }
            }
            mediator.OnRegister();
        }

        public virtual void RegisterObserver(string notificationName, IObserver observer)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_observerMap.ContainsKey(notificationName))
                {
                    this.m_observerMap[notificationName] = new List<IObserver>();
                }
                this.m_observerMap[notificationName].Add(observer);
            }
        }

        public virtual IMediator RemoveMediator(string mediatorName)
        {
            IMediator notifyContext = null;
            lock (this.m_syncRoot)
            {
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                notifyContext = this.m_mediatorMap[mediatorName];
                IList<string> list = notifyContext.ListNotificationInterests();
                for (int i = 0; i < list.Count; i++)
                {
                    this.RemoveObserver(list[i], notifyContext);
                }
                this.m_mediatorMap.Remove(mediatorName);
            }
            if (notifyContext != null)
            {
                notifyContext.OnRemove();
            }
            return notifyContext;
        }

        public virtual void RemoveObserver(string notificationName, object notifyContext)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_observerMap.ContainsKey(notificationName))
                {
                    IList<IObserver> list = this.m_observerMap[notificationName];
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].CompareNotifyContext(notifyContext))
                        {
                            list.RemoveAt(i);
                            break;
                        }
                    }
                    if (list.Count == 0)
                    {
                        this.m_observerMap.Remove(notificationName);
                    }
                }
            }
        }

        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                return this.m_mediatorMap[mediatorName];
            }
        }

        //5.实现View单例
        public static IView Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new View();
                        }
                    }
                }
                return m_instance;
            }
        }
    }
}

