using UnityEngine;

namespace PureMVC.Core
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns;
    using System;
    using System.Collections.Generic;

    public class Controller : IController
    {
        //字典记录已注册的命令(key是命令的名称,value是继承命令类型(实现命令接口)的类型)
        protected IDictionary<string, Type> m_commandMap = new Dictionary<string, Type>();
        protected static volatile IController m_instance;
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();
        protected IView m_view;

        protected Controller()
        {
            this.InitializeController();
        }

        public virtual void ExecuteCommand(INotification note)
        {
            Type type = null;
            lock (this.m_syncRoot)
            {
                if (!this.m_commandMap.ContainsKey(note.Name))
                {
                    return;
                }
                type = this.m_commandMap[note.Name];
            }
            object obj2 = Activator.CreateInstance(type);
            if (obj2 is ICommand)
            {
                ((ICommand) obj2).Execute(note);
            }
        }

        public virtual bool HasCommand(string notificationName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_commandMap.ContainsKey(notificationName);
            }
        }

        protected virtual void InitializeController()
        {
            //记录IView接口,以此来执行View中的通知,IView在构造Controller时赋值
            this.m_view = View.Instance;
        }

        public virtual void RegisterCommand(string notificationName, Type commandType)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_commandMap.ContainsKey(notificationName))
                {
                    this.m_view.RegisterObserver(notificationName, new Observer("executeCommand", this));
                }
                this.m_commandMap[notificationName] = commandType;
            }
        }

        public virtual void RemoveCommand(string notificationName)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_commandMap.ContainsKey(notificationName))
                {
                    this.m_view.RemoveObserver(notificationName, this);
                    this.m_commandMap.Remove(notificationName);
                }
            }
        }

        public static IController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new Controller();
                        }
                    }
                }
                return m_instance;
            }
        }
    }
}