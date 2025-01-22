using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PhysicsQuery
{
    [CreateAssetMenu(fileName = PackageQualifiedName, menuName = MenuName)]
    public class Settings : ScriptableObject
    {
        private const string ProjectFolder = "Assets";
        public const string PackageFolderName = "Physics Query";
        private const string ResourceFolderName = "Resources";
        private const string PackageFolderPath = ProjectFolder + "/" + PackageFolderName;
        private const string ResourceFolderPath = PackageFolderPath + "/" + ResourceFolderName;
        private const string InstancePath = ResourceFolderPath + "/" + PackageQualifiedName + ".asset";
        public const string Name = nameof(Settings);
        public const string PackageQualifiedName = PackageFolderName + " " + Name;
        private const string MenuName = PackageFolderName + "/" + Name;

        public static Space DefaultQuerySpace => GetInstance()._defaultQuerySpace;
        public static float DefaultMaxDistance => GetInstance()._defaultMaxDistance;
        public static LayerMask DefaultLayerMask => GetInstance()._defaultLayerMask;
        public static QueryTriggerInteraction DefaultTriggerInteraction => GetInstance()._defaultTriggerInteraction;
        public static int DefaultCacheCapacity => GetInstance()._defaultCacheCapacity;

        [SerializeField]
        private Space _defaultQuerySpace = Space.Self;
        [SerializeField]
        private float _defaultMaxDistance = 10;
        [SerializeField]
        private LayerMask _defaultLayerMask = Physics.DefaultRaycastLayers;
        [SerializeField]
        private QueryTriggerInteraction _defaultTriggerInteraction = QueryTriggerInteraction.UseGlobal;
        [SerializeField]
        private int _defaultCacheCapacity = 8;
        private static Settings _instance;

        internal static Settings GetInstance()
        {
            if (_instance == null)
            {
                _instance = LoadInstance();
            }
            if (_instance == null)
            {
                _instance = CreateInstance();
            }
            return _instance;
        }
        private static Settings LoadInstance()
        {
            return Resources.Load<Settings>(PackageQualifiedName);
        }
        private static Settings CreateInstance()
        {
            Settings instance = CreateInstance<Settings>();
            SaveToAssets(instance);
            return instance;
        }
        private static void SaveToAssets(Settings instance)
        {
#if UNITY_EDITOR
            if (!AssetDatabase.IsValidFolder(PackageFolderPath))
            {
                AssetDatabase.CreateFolder(ProjectFolder, PackageFolderName);
                AssetDatabase.Refresh();
            }
            if (!AssetDatabase.IsValidFolder(ResourceFolderPath))
            {
                AssetDatabase.CreateFolder(PackageFolderPath, ResourceFolderName);
                AssetDatabase.Refresh();
            }
            AssetDatabase.CreateAsset(instance, InstancePath);
            AssetDatabase.SaveAssets();
#endif
        }
    }
}