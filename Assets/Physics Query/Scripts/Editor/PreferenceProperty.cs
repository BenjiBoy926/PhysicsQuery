using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PhysicsQuery
{
    public abstract class PreferenceProperty
    {
        private const string KeyPrefix = nameof(PhysicsQuery) + nameof(Preferences) + ".";
        private string Key => $"{KeyPrefix}{_name}";
        public string Name => _name;
        public abstract Type PropertyType { get; }
        public abstract object ObjectValue { get; set; }

        private readonly string _name;

        public PreferenceProperty(string name)
        {
            _name = name;
        }

        public void Reset()
        {
#if UNITY_EDITOR
            EditorPrefs.DeleteKey(Key);
#endif
        }

        protected bool GetBool(bool defaultValue)
        {
#if UNITY_EDITOR
            return EditorPrefs.GetBool(Key, defaultValue);
#else
            return defaultValue;
#endif
        }
        protected void SetBool(bool value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetBool(Key, value);
#endif
        }

        protected float GetFloat(float defaultValue)
        {
#if UNITY_EDITOR
            return EditorPrefs.GetFloat(Key, defaultValue);
#else
            return defaultValue;
#endif
        }
        protected void SetFloat(float value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat(Key, value);
#endif
        }

        protected int GetInt(int defaultValue)
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt(Key, defaultValue);
#else
            return defaultValue;
#endif
        }
        protected void SetInt(int value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt(Key, value);
#endif
        }

        protected string GetString(string defaultValue)
        {
#if UNITY_EDITOR
            return EditorPrefs.GetString(Key, defaultValue);
#else
            return defaultValue;
#endif
        }
        protected void SetString(string value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetString(Key, value);
#endif
        }

        protected object GetObject(object defaultValue, Type type)
        {
#if UNITY_EDITOR
            string serializedDefaultValue = Serialize(defaultValue);
            string value = EditorPrefs.GetString(Key, serializedDefaultValue);
            return Deserialize(value, type);
#else
            return defaultValue;
#endif
        }
        protected void SetObject(object value)
        {
#if UNITY_EDITOR
            string serializedValue = Serialize(value);
            EditorPrefs.SetString(Key, serializedValue);
#endif
        }

        private string Serialize(object value)
        {
            return JsonUtility.ToJson(value);
        }
        private object Deserialize(string serializedValue, Type type)
        {
            return JsonUtility.FromJson(serializedValue, type);
        }
    }

    public class PreferenceProperty<TValue> : PreferenceProperty
    {
        public override Type PropertyType => typeof(TValue);
        public override object ObjectValue
        {
            get => Value;
            set => Value = (TValue)value;
        }
        public TValue Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        private readonly TValue _defaultValue;

        public PreferenceProperty(string name, TValue defaultValue) : base(name)
        {
            _defaultValue = defaultValue;
        }

        private TValue GetValue()
        {
            if (_defaultValue is bool defaultBool)
            {
                return (TValue)(object)GetBool(defaultBool);
            }
            else if (_defaultValue is float defaultFloat)
            {
                return (TValue)(object)GetFloat(defaultFloat);
            }
            else if (_defaultValue is int defaultInt)
            {
                return (TValue)(object)GetInt(defaultInt);
            }
            else if (_defaultValue is string defaultString)
            {
                return (TValue)(object)GetString(defaultString);
            }
            else
            {
                return (TValue)GetObject(_defaultValue, typeof(TValue));
            }
        }
        private void SetValue(TValue value)
        {
            if (value is bool boolValue)
            {
                SetBool(boolValue);
            }
            else if (value is float floatValue)
            {
                SetFloat(floatValue);
            }
            else if (value is int intValue)
            {
                SetInt(intValue);
            }
            else if (value is string stringValue)
            {
                SetString(stringValue);
            }
            else
            {
                SetObject(value);
            }
        }
    }
}