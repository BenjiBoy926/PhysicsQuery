using System.Collections.Generic;
using UnityEngine;

namespace PQuery.Editor
{
    public static class Preferences
    {
        private const string PreviewIndexPropertyNamePrefix = "PreviewIndex";

        public static float HitNormalLength => HitSphereRadius.Value * 5;

        public static readonly PreferenceProperty<Color> HitColor = new(nameof(HitColor), Color.green);
        public static readonly PreferenceProperty<Color> CacheFullColor = new(nameof(CacheFullColor), Color.red);
        public static readonly PreferenceProperty<Color> MissColor = new(nameof(MissColor), Color.gray);
        public static readonly PreferenceProperty<Color> ResultItemColor = new(nameof(ResultItemColor), Color.blue);
        public static readonly PreferenceProperty<float> HitSphereRadius = new(nameof(HitSphereRadius), 0.05f);
        public static readonly PreferenceProperty<bool> AlwaysDrawGizmos = new(nameof(AlwaysDrawGizmos), false);
        public static readonly List<PreferenceProperty> Properties = new()
        {
            HitColor, CacheFullColor, MissColor, ResultItemColor, HitSphereRadius, AlwaysDrawGizmos
        };

        private static readonly List<PreferenceProperty<int>> _previewIndices = new();

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
            for (int i = 0; i < Properties.Count; i++)
            {
                Properties[i].Reset();
            }
        }

        public static int GetPreviewIndex(PhysicsQuery query)
        {
            PreferenceProperty<int> property = GetPreviewIndexProperty(query);
            int value = property.Value;
            return ClampPreviewIndex(value);
        }
        public static void SetPreviewIndex(PhysicsQuery query, int index)
        {
            PreferenceProperty<int> property = GetPreviewIndexProperty(query);
            property.Value = ClampPreviewIndex(index);
        }
        private static PreferenceProperty<int> GetPreviewIndexProperty(PhysicsQuery query)
        {
            string name = GetPreviewIndexPropertyName(query);
            PreferenceProperty<int> property = _previewIndices.Find(x => x.Name == name);
            return property ?? AddNewPreviewIndexProperty(query);
        }
        private static PreferenceProperty<int> AddNewPreviewIndexProperty(PhysicsQuery query)
        {
            PreferenceProperty<int> newProperty = new(GetPreviewIndexPropertyName(query), 0);
            _previewIndices.Add(newProperty);
            return newProperty;
        }
        private static string GetPreviewIndexPropertyName(PhysicsQuery query)
        {
            return $"{PreviewIndexPropertyNamePrefix}{query.GetInstanceID()}";
        }
        private static int ClampPreviewIndex(int index)
        {
            return Mathf.Clamp(index, 0, Preview.Count - 1);
        }
    }
}