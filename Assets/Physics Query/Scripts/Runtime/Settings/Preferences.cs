using UnityEngine;

namespace PhysicsQuery
{
    public static class Preferences
    {
        public static float HitSphereRadius => HitNormalLength.Value * HitSphereRadiusProportion.Value;
        
        public static readonly PreferenceProperty<Color> HitColor = new(nameof(HitColor), Color.green);
        public static readonly PreferenceProperty<Color> CacheFullColor = new(nameof(CacheFullColor), Color.red);
        public static readonly PreferenceProperty<Color> MissColor = new(nameof(MissColor), Color.gray);
        public static readonly PreferenceProperty<Color> ResultItemColor = new(nameof(ResultItemColor), Color.blue);
        public static readonly PreferenceProperty<float> HitNormalLength = new(nameof(HitNormalLength), 0.3f);
        public static readonly PreferenceProperty<float> HitSphereRadiusProportion = new(nameof(HitSphereRadiusProportion), 0.2f);
        public static readonly PreferenceProperty[] Properties = new PreferenceProperty[]
        {
            HitColor, CacheFullColor, MissColor, ResultItemColor, HitNormalLength, HitSphereRadiusProportion
        };

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
    }
}