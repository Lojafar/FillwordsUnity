using FillWords.Root.UI.Tabs;
using FillWords.Utils;
using System;
using System.Collections.Generic;

namespace FillWords.Root.UI
{
    public class TabsHandler
    {
        Type openedTab;
        readonly Dictionary<Type, ParentTabModel> modelsMap;
        bool anyTabInAction = false;
        event Action OnTabFinished;
        public TabsHandler()
        {
            modelsMap = new Dictionary<Type, ParentTabModel>();
            OnTabFinished = null;
        }
        public void RegisterTabModel<T>(T tabModel) where T : ParentTabModel
        {
            Type tabType = typeof(T);
            if (!modelsMap.ContainsKey(tabType))
            {
                modelsMap.Add(tabType, tabModel);
                tabModel.Close(null);
            }
            else
            {
                DebugUtil.Log("The model with type " + tabType + " is already registered", LogType.Warning);
            }
        }
        public void OpenTab<T>() where T : ParentTabModel
        {
            OpenTab(typeof(T));
        }
        public void OpenTab(Type tabType) 
        {
            if (!modelsMap.ContainsKey(tabType))
            {
                DebugUtil.Log("The model with type " + tabType + " is not registered", LogType.Warning);
                return;
            }
            if (anyTabInAction)
            {
                OnTabFinished += () => OnOpenEvent(tabType);
                return;
            }
            if (openedTab != null)
            {
                OnTabFinished += () => OnOpenEvent(tabType);
                CloseTab(openedTab);
                return;
            }

            OnTabFinished = null;
            anyTabInAction = true;
            openedTab = tabType;
            modelsMap[tabType].Open(() => OnTabCompleted());
        }
        void OnOpenEvent(Type tabType)
        {
            OpenTab(tabType);
        }
        public void CloseTab<T>() where T : ParentTabModel
        {
            CloseTab(typeof(T));
        }
        public void CloseTab(Type tabType)
        {
            if (!modelsMap.ContainsKey(tabType))
            {
                DebugUtil.Log("The model with type " + tabType + " is not registered", LogType.Warning);
                return;
            }
            if (anyTabInAction)
            {
                OnTabFinished += () => OnCloseEvent(tabType);
                return;
            }

          //  OnTabFinished = null;
            anyTabInAction = true;
            openedTab = null;
            modelsMap[tabType].Close(() => OnTabCompleted());
        }
        void OnCloseEvent(Type tabType)
        {
            OpenTab(tabType);
        }
        void OnTabCompleted()
        {
            anyTabInAction = false;
            OnTabFinished?.Invoke();
            OnTabFinished = null;
        }
    }
}
