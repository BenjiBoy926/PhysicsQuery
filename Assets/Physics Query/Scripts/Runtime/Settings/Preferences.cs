using UnityEngine;

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

        private static Color GetColor(string propertyName, Color defaultColor)
        {
#if UNITY_EDITOR
            string serializedDefaultColor = Serialize(defaultColor);
            string key = GetPrefKey(propertyName);
            string current = EditorPrefs.GetString(key, serializedDefaultColor);
            return Deserialize(current);
#else
            return default;
#endif
        }
        private static void SetColor(string propertyName, Color color)
        {
#if UNITY_EDITOR
            string serializedColor = Serialize(color);
            string key = GetPrefKey(propertyName);
            EditorPrefs.SetString(key, serializedColor);
#endif
        }
        private static string GetPrefKey(string propertyName)
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
    }
}