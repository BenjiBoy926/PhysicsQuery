using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PQuery.Editor
{
    public static class Preferences
    {
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

        private static readonly PreferencePropertyCollection<int> _previewIndices = new(0);
        private static readonly PreferencePropertyCollection<bool> _raycastHitFoldout = new(false);

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
            string name = GetPreviewIndexPropertyName(query);
            int value = _previewIndices.GetValue(name);
            return ClampPreviewIndex(value);
        }
        public static void SetPreviewIndex(PhysicsQuery query, int index)
        {
            string name = GetPreviewIndexPropertyName(query);
            _previewIndices.SetValue(name, ClampPreviewIndex(index));
        }

        public static void ExpandRaycastHitFoldout(PhysicsQuery query, Collider collider)
        {
            SetRaycastHitFoldout(query, collider, true);
        }
        public static void CollapseAllRaycastHitFoldouts()
        {
            _raycastHitFoldout.SetAllValues(false);
        }
        public static bool GetRaycastHitFoldout(PhysicsQuery query, Collider collider)
        {
            string name = GetRaycastHitFoldoutPropertyName(query, collider);
            return _raycastHitFoldout.GetValue(name);
        }
        public static void SetRaycastHitFoldout(PhysicsQuery query, Collider collider, bool foldout)
        {
            string name = GetRaycastHitFoldoutPropertyName(query, collider);
            _raycastHitFoldout.SetValue(name, foldout);
        }

        private static string GetRaycastHitFoldoutPropertyName(PhysicsQuery query, Collider collider)
        {
            return $"RaycastHitFoldout-{query.GetInstanceID()}-{collider.GetInstanceID()}";
        }
        private static string GetPreviewIndexPropertyName(PhysicsQuery query)
        {
            return $"PreviewIndex-{query.GetInstanceID()}";
        }
        private static int ClampPreviewIndex(int index)
        {
            return Mathf.Clamp(index, 0, Preview.Count - 1);
        }
    }
}