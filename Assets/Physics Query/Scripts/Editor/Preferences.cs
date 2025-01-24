using System.Collections.Generic;
using UnityEngine;

namespace PhysicsQuery
{
    public static class Preferences
    {
        private const string PreviewIndexPropertyNamePrefix = "PreviewIndex";

        public static float HitSphereRadius => HitNormalLength.Value * HitSphereRadiusProportion.Value;
        
        public static readonly PreferenceProperty<Color> HitColor = new(nameof(HitColor), Color.green);
        public static readonly PreferenceProperty<Color> CacheFullColor = new(nameof(CacheFullColor), Color.red);
        public static readonly PreferenceProperty<Color> MissColor = new(nameof(MissColor), Color.gray);
        public static readonly PreferenceProperty<Color> ResultItemColor = new(nameof(ResultItemColor), Color.blue);
        public static readonly PreferenceProperty<float> HitNormalLength = new(nameof(HitNormalLength), 0.3f);
        public static readonly PreferenceProperty<float> HitSphereRadiusProportion = new(nameof(HitSphereRadiusProportion), 0.2f);
        public static readonly PreferenceProperty<bool> AlwaysDrawGizmos = new(nameof(AlwaysDrawGizmos), false);
        public static readonly PreferenceProperty[] Properties = new PreferenceProperty[]
        {
            HitColor, CacheFullColor, MissColor, ResultItemColor, HitNormalLength, HitSphereRadiusProportion, AlwaysDrawGizmos
        };

        private static readonly Dictionary<PhysicsQuery, PreferenceProperty<int>> _currentPreviewIndex = new();

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
            for (int i = 0; i < Properties.Length; i++)
            {
                Properties[i].Reset();
            }
        }

        public static int GetPreviewIndex(PhysicsQuery query)
        {
            AddToDictionary(query);
            int value = _currentPreviewIndex[query].Value;
            return ClampPreviewIndex(value);
        }
        public static void SetPreviewIndex(PhysicsQuery query, int index)
        {
            AddToDictionary(query);
            _currentPreviewIndex[query].Value = ClampPreviewIndex(index);
        }
        private static void AddToDictionary(PhysicsQuery query)
        {
            if (_currentPreviewIndex.ContainsKey(query))
            {
                return;
            }
            PreferenceProperty<int> newProperty = new(GetPreviewIndexPropertyName(query), 0);
            _currentPreviewIndex.Add(query, newProperty);
        }
        private static string GetPreviewIndexPropertyName(PhysicsQuery query)
        {
            return $"{PreviewIndexPropertyNamePrefix}{query.GetInstanceID()}";
        }
        private static int ClampPreviewIndex(int index)
        {
            return Mathf.Clamp(index, 0, GizmoPreview.Count - 1);
        }
    }
}