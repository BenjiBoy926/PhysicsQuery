using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace PQuery.Editor
{
    public abstract class ScenePreview
    {
        public delegate void ColliderClickHandler(Collider collider);
        public static event ColliderClickHandler ColliderClicked = delegate { };

        private static readonly List<PhysicsQuery3D> _selectedQueries = new();

        protected static void ClickCollider(Collider collider)
        {
            EditorGUIUtility.PingObject(collider);
            ExpandRaycastHitsWithCollider(collider);
            ColliderClicked(collider);
        }
        private static void ExpandRaycastHitsWithCollider(Collider collider)
        {
            List<PhysicsQuery3D> queries = GetSelectedQueries();
            for (int i = 0; i < queries.Count; i++)
            {
                Preferences.CollapseAllRaycastHitFoldouts(queries[i]);
                Preferences.ExpandRaycastHitFoldout(queries[i], collider);
            }
        }
        private static List<PhysicsQuery3D> GetSelectedQueries()
        {
            Transform[] selected = Selection.transforms;
            _selectedQueries.Clear();
            for (int i = 0; i < selected.Length; i++)
            {
                AddQuery(selected[i], _selectedQueries);
            }
            return _selectedQueries;
        }
        private static void AddQuery(Transform transform, List<PhysicsQuery3D> queries)
        {
            if (transform.TryGetComponent(out PhysicsQuery3D query))
            {
                queries.Add(query);
            }
        }

        public abstract void DrawSceneGUI(PhysicsQuery3D query);
    }
}