using UnityEngine;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PhysicsQuery
{
    public static class Preferences
    {
        private const string EditorPrefKeyPrefix = nameof(PhysicsQuery) + nameof(Preferences) + ".";

        public static Color HitColor
        {
            get => GetColor(nameof(HitColor), Color.green);
            set => SetColor(nameof(HitColor), value);
        }
        public static Color CacheFullColor
        {
            get => GetColor(nameof(CacheFullColor), Color.red);
            set => SetColor(nameof(CacheFullColor), value);
        }
        public static Color MissColor
        {
            get => GetColor(nameof(MissColor), Color.gray);
            set => SetColor(nameof(MissColor), value);
        }
        public static Color ResultItemColor
        {
            get => GetColor(nameof(ResultItemColor), Color.blue); 
            set => SetColor(nameof(ResultItemColor), value);
        }
        public static float HitNormalLength
        {
            get => GetFloat(nameof(HitNormalLength), 0.3f);
            set => SetFloat(nameof(HitNormalLength), value);
        }
        public static float HitSphereRadiusProportion
        {
            get => GetFloat(nameof(HitSphereRadiusProportion), 0.2f);
            set => SetFloat(nameof(HitSphereRadiusProportion), value);
        }
        public static float HitSphereRadius => HitNormalLength * HitSphereRadiusProportion;

        public static Color GetColorForResult<TElement>(Result<TElement> result)
        {
            if (result.IsFull)
            {
                return Color.red;
            }
            else if (result.IsEmpty)
            {
                return Color.gray;
            }
            return Color.green;
        }

        public static void Clear()
        {
            PropertyInfo[] properties = typeof(Preferences).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                string key = GetKey(properties[i].Name);
                DeleteKeyImpl(key);
            }
        }

        private static Color GetColor(string propertyName, Color defaultColor)
        {
            string serializedDefaultColor = Serialize(defaultColor);
            string key = GetKey(propertyName);
            string current = GetStringImpl(key, serializedDefaultColor);
            return Deserialize(current);
        }
        private static void SetColor(string propertyName, Color color)
        {
            string serializedColor = Serialize(color);
            string key = GetKey(propertyName);
            SetStringImpl(key, serializedColor);
        }
        private static float GetFloat(string propertyName, float defaultValue)
        {
            string key = GetKey(propertyName);
            return GetFloatImpl(key, defaultValue);
        }
        private static void SetFloat(string propertyName, float value)
        {
            string key = GetKey(propertyName);
            SetFloatImpl(key, value);
        }

        private static string GetKey(string propertyName)
        {
            return $"{EditorPrefKeyPrefix}{propertyName}";
        }
        private static string Serialize(Color deserializedColor)
        {
            return JsonUtility.ToJson(deserializedColor);
        }
        private static Color Deserialize(string serializedColor)
        {
            return JsonUtility.FromJson<Color>(serializedColor);
        }

        private static float GetFloatImpl(string key, float defaultValue)
        {
#if UNITY_EDITOR
            return EditorPrefs.GetFloat(key, defaultValue);
#else
            return defaultValue;
#endif
        }
        private static void SetFloatImpl(string key, float value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat(key, value);
#endif
        }
        private static string GetStringImpl(string key, string defaultValue)
        {
#if UNITY_EDITOR
            return EditorPrefs.GetString(key, defaultValue);
#else
            return defaultValue;
#endif
        }
        private static void SetStringImpl(string key, string value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetString(key, value);
#endif
        }
        private static void DeleteKeyImpl(string key)
        {
#if UNITY_EDITOR
            EditorPrefs.DeleteKey(key);
#endif
        }
    }
}