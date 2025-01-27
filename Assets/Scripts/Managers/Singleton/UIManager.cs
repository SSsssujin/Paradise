using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Paradise.UI
{
    public class UIManager
    {
        private readonly HashSet<UI_Base> _sceneUISet = new();
        private readonly Stack<UI_Base> _activatedUI = new();

        public async Task Initialize()
        {
            // 씬 내의 모든 UI를 초기화
            var uiObjects = Object.FindObjectsByType<UI_Base>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var ui in uiObjects)
            {
                if (ui.Load())
                    _sceneUISet.Add(ui);
                await Task.Yield(); // 프레임 분할 처리
            }
        }

        private T GetSceneUI<T>() where T : UI_Base
        {
            return _sceneUISet.FirstOrDefault(x => x is T) as T;
        }

        public void ShowPopup<T>() where T : UI_Base
        {
            var ui = GetSceneUI<T>();
            _activatedUI.Push(ui);
            ui.Show();
        }

        public void ClosePopup()
        {
            var ui = _activatedUI.Pop();
            ui.Hide();
        }

        public void Destroy()
        {
            foreach (var ui in _sceneUISet)
            {
                ui.Destroy();
            }
            _sceneUISet.Clear();
            _activatedUI.Clear();
        }
    }
}