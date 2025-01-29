using FillWords.Utils;
using System;
using System.Collections.Generic;
using R3;

namespace FillWords.Root.UI.Tabs
{
    public class ParentTabModel : ITabModel
    {
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        readonly Dictionary<Type, ITabModel> tabsMap;
        protected ITabModel openedTab;
        public ParentTabModel()
        {
            tabsMap = new Dictionary<Type, ITabModel>();
            OpeningEvent = new();
            ClosingEvent = new();
        }
        public virtual void Open(Action callback)
        {
            OpeningEvent.OnNext(callback);
        }
        public virtual void Close(Action callback)
        {
            if(openedTab != null)
            {
                openedTab.Close(() => ClosingEvent.OnNext(callback));
                openedTab = null;
            }
            else
            {
                ClosingEvent.OnNext(null);
            }
        }
        public void OpenTab<T>() where T : ITabModel
        {
            Type tabType = typeof(T);
            if (!tabsMap.ContainsKey(tabType))
            {
                DebugUtil.Log("You are trying to open an unregistered tab. Tab type: " + tabType, LogType.Error);
                return;
            }
            if (openedTab == null || openedTab != null && tabType == openedTab.GetType())
            {
                openedTab = tabsMap[tabType];
                tabsMap[tabType].Open(null);
            }
            else
            {
                openedTab?.Close(() =>
                {
                    openedTab = tabsMap[tabType];
                    tabsMap[tabType].Open(null); 
                });
            }
        }
        public void OpenPopup<T>() where T : ITabModel
        {
            if (tabsMap.TryGetValue(typeof(T), out ITabModel tabModel))
            {
                tabModel.Open(null);
            }
            else
            {
                DebugUtil.Log("You are trying to open an unregistered popup tab. Tab type: " + typeof(T), LogType.Error);
            }
        }
        public void ClosePopup<T>() where T : ITabModel
        {
            if (tabsMap.TryGetValue(typeof(T), out ITabModel tabModel))
            {
                tabModel.Close(null);
            }
            else
            {
                DebugUtil.Log("You are trying to close an unregistered popup tab. Tab type: " + typeof(T), LogType.Error);
            }
        }
        public void RegisterTab<T>(T tab) where T : ITabModel
        {
            if (!tabsMap.ContainsKey(typeof(T)))
            {
                tabsMap.Add(typeof(T), tab);
                tab.Close(null);
            }
            else
            {
                DebugUtil.Log("The tab with type: " + typeof(T) + " is already registered", LogType.Warning);
            }
        }
    }
}