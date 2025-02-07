using System;
using UnityEngine;
using UnityEditor;

namespace PQuery.Editor
{
    public abstract class PreferenceProperty
    {
        private const string KeyPrefix = nameof(PhysicsQuery3D) + nameof(Preferences) + ".";
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
            EditorPrefs.DeleteKey(Key);
        }

        protected bool GetBool(bool defaultValue)
        {
            return EditorPrefs.GetBool(Key, defaultValue);
        }
        protected void SetBool(bool value)
        {
            EditorPrefs.SetBool(Key, value);
        }

        protected float GetFloat(float defaultValue)
        {
            return EditorPrefs.GetFloat(Key, defaultValue);
        }
        protected void SetFloat(float value)
        {
            EditorPrefs.SetFloat(Key, value);
        }

        protected int GetInt(int defaultValue)
        {
            return EditorPrefs.GetInt(Key, defaultValue);
        }
        protected void SetInt(int value)
        {
            EditorPrefs.SetInt(Key, value);
        }

        protected string GetString(string defaultValue)
        {
            return EditorPrefs.GetString(Key, defaultValue);
        }
        protected void SetString(string value)
        {
            EditorPrefs.SetString(Key, value);
        }

        protected object GetObject(object defaultValue, Type type)
        {
            string serializedDefaultValue = Serialize(defaultValue);
            string value = EditorPrefs.GetString(Key, serializedDefaultValue);
            return Deserialize(value, type);
        }
        protected void SetObject(object value)
        {
            string serializedValue = Serialize(value);
            EditorPrefs.SetString(Key, serializedValue);
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