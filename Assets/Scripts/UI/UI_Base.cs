using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Paradise.UI
{
    public abstract class UI_Base : MonoBehaviour
    {
        private Dictionary<Type, Object[]> _objects = new();
        private bool _isInit;

        [SerializeField]
        private bool _isOpened = false;

        public bool Load()
        {
            gameObject.SetActive(_isOpened);
            return _Initialize();
        }

        protected virtual bool _Initialize()
        {
            if (_isInit)
                return false;
            
            /* Do initialize */
            
            _isInit = true;
            return true;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            OnHide();
            gameObject.SetActive(false);
        }
        
        public void Destroy()
        {
            _Destroy();
        }
        
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }

        protected void BindObject(Type type) { Bind<GameObject>(type); }
        protected void BindImage(Type type) { Bind<Image>(type); }
        protected void BindText(Type type) { Bind<TMP_Text>(type); }
        protected void BindButton(Type type) { Bind<Button>(type); }
        protected void BindToggle(Type type) { Bind<Toggle>(type); }

        protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
        protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
        protected Button GetButton(int idx) { return Get<Button>(idx); }
        protected Image GetImage(int idx) { return Get<Image>(idx); }
        protected Toggle GetToggle(int idx) { return Get<Toggle>(idx); }
        
        private void Bind<T>(Type type) where T : Object
        {
            string[] names = Enum.GetNames(type);
            var objects = new Object[names.Length];
            _objects.Add(typeof(T), objects);

            for (int i = 0; i < names.Length; i++)
            {
                if (typeof(T) == typeof(GameObject))
                    objects[i] = Utils.FindChild(gameObject, names[i], true);
                else
                    objects[i] = Utils.FindChild<T>(gameObject, names[i], true);

                if (objects[i] == null)
                    Debug.Log($"Failed to bind({names[i]})");
            }
        }

        private T Get<T>(int idx) where T : UnityEngine.Object
        {
            Object[] objects = null;
            if (_objects.TryGetValue(typeof(T), out objects) == false)
                return null;

            return objects[idx] as T;
        }
        
        protected void BindEvent(GameObject go, Action action = null, Action<BaseEventData> dragAction = null, UIEvent type = UIEvent.Click)
        {
            UI_EventHandler @event = Utils.DemandComponent<UI_EventHandler>(go);

            switch (type)
            {
                case UIEvent.Click:
                    @event.OnClickHandler -= action;
                    @event.OnClickHandler += action;
                    break;
                case UIEvent.Pressed:
                    @event.OnPressedHandler -= action;
                    @event.OnPressedHandler += action;
                    break;
                case UIEvent.PointerDown:
                    @event.OnPointerDownHandler -= action;
                    @event.OnPointerDownHandler += action;
                    break;
                case UIEvent.PointerUp:
                    @event.OnPointerUpHandler -= action;
                    @event.OnPointerUpHandler += action;
                    break;
                case UIEvent.Drag:
                    @event.OnDragHandler -= dragAction;
                    @event.OnDragHandler += dragAction;
                    break;
                case UIEvent.BeginDrag:
                    @event.OnBeginDragHandler -= dragAction;
                    @event.OnBeginDragHandler += dragAction;
                    break;
                case UIEvent.EndDrag:
                    @event.OnEndDragHandler -= dragAction;
                    @event.OnEndDragHandler += dragAction;
                    break;
            }
        }

        private void _Destroy()
        {
            FieldInfo[] info = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in info)
            {
                Type fieldType = field.FieldType;
 
                if (typeof(IList).IsAssignableFrom(fieldType))
                {
                    IList list = field.GetValue(this) as IList;
                    if (list != null)
                    {
                        list.Clear();
                    }
                }
 
                if (typeof(IDictionary).IsAssignableFrom(fieldType))
                {
                    IDictionary dictionary = field.GetValue(this) as IDictionary;
                    if (dictionary != null)
                    {
                        dictionary.Clear();
                    }
                }
 
                if (!fieldType.IsPrimitive)
                {
                    field.SetValue(this, null);
                }
            }
        }
    }
}