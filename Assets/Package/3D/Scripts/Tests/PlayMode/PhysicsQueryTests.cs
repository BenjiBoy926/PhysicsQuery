using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PhysicsQuery.PlayModeTests
{
    public class PhysicsQueryTests
    {
        PhysicsQuery _query;

        [UnityTest]
        public IEnumerator TestRaySpaceProperty()
        {
            Space space = Space.World;
            CreatePhysicsQuery();
            _query.Space = space;
            Assert.AreEqual(_query.Space, space);
            yield return null;
        }
        [UnityTest]
        public IEnumerator TestRayProperty()
        {
            Ray ray = new Ray(Vector3.down, Vector3.up);
            CreatePhysicsQuery();
            _query.Ray = ray;
            Assert.AreEqual(_query.Ray, ray);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestMaxDistanceProperty()
        {
            float maxDistance = 10;
            CreatePhysicsQuery();

            _query.MaxDistance = maxDistance;
            Assert.AreEqual(_query.MaxDistance, maxDistance);

            maxDistance = 0;
            _query.MaxDistance = maxDistance;
            Assert.AreEqual(_query.MaxDistance, maxDistance);

            Assert.Throws<InvalidOperationException>(() => _query.MaxDistance = -1);
            Assert.AreEqual(_query.MaxDistance, maxDistance);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestLayerMaskProperty()
        {
            LayerMask mask = 0;
            CreatePhysicsQuery();
            _query.LayerMask = mask;
            Assert.AreEqual(_query.LayerMask, mask);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestQueryTriggerInteractionProperty()
        {
            QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Collide;
            CreatePhysicsQuery();
            _query.TriggerInteraction = triggerInteraction;
            Assert.AreEqual(_query.TriggerInteraction, triggerInteraction);
            yield return null;
        }
        [UnityTest]
        public IEnumerator TestMaxCachedHitsProperty()
        {
            int maxCachedHits = 16;
            CreatePhysicsQuery();

            _query.MaxCachedHits = maxCachedHits;
            Assert.AreEqual(_query.MaxCachedHits, maxCachedHits);

            maxCachedHits = 0;
            _query.MaxCachedHits = maxCachedHits;
            Assert.AreEqual(_query.MaxCachedHits, maxCachedHits);

            Assert.Throws<InvalidOperationException>(() => _query.MaxCachedHits = -1);
            Assert.AreEqual(_query.MaxCachedHits, maxCachedHits);

            yield return null;
        }
        [UnityTest]
        public IEnumerator TestGetHitCache()
        {
            CreatePhysicsQuery();
            RaycastHit[] cache = _query.GetHitCache();
            Assert.AreEqual(cache.Length, _query.MaxCachedHits);

            _query.MaxCachedHits++;
            cache = _query.GetHitCache();
            Assert.AreEqual(cache.Length, _query.MaxCachedHits);

            _query.MaxCachedHits -= 2;
            cache = _query.GetHitCache();
            Assert.AreEqual(cache.Length, _query.MaxCachedHits);

            yield return null;
        }
        [UnityTest]
        public IEnumerator TestGetWorldRay()
        {
            CreatePhysicsQuery();

            Ray ray = new Ray(Vector3.zero, Vector3.one);
            _query.Ray = ray;
            _query.transform.position = Vector3.zero;
            _query.transform.rotation = Quaternion.identity;
            _query.transform.localScale = Vector3.one;

            Assert.AreEqual(ray, _query.GetWorldRay());

            ray = new Ray(Vector3.one, Vector3.one);
            _query.transform.position = Vector3.one;
            AssertRay(ray);
            _query.transform.position = Vector3.zero;

            ray = new Ray(Vector3.zero, new Vector3(1, 1, -1));
            _query.transform.rotation = Quaternion.Euler(0, 90, 0);
            AssertRay(ray);
            _query.transform.rotation = Quaternion.identity;

            ray = new Ray(Vector3.zero, Vector3.one * 2);
            _query.transform.localScale = Vector3.one * 2;
            AssertRay(ray);
            _query.transform.localScale = Vector3.one;

            yield return null;
        }

        private void CreatePhysicsQuery()
        {
            GameObject container = new GameObject("Physics Caster");
            _query = container.AddComponent<EmptyQuery>();
        }
        private void AssertRay(Ray transformedRay)
        {
            _query.Space = Space.Self;
            Assert.IsTrue(ApproximatelyEqual(transformedRay, _query.GetWorldRay()));
            _query.Space = Space.World;
            Assert.IsTrue(ApproximatelyEqual(_query.Ray, _query.GetWorldRay()));
        }
        private bool ApproximatelyEqual(Ray ray1, Ray ray2)
        {
            return ApproximatelyEqual(ray1.origin, ray2.origin) &&
                ApproximatelyEqual(ray1.direction, ray2.direction);
        }
        private bool ApproximatelyEqual(Vector3 vector1, Vector3 vector2)
        {
            return Mathf.Approximately(vector1.x, vector2.x) &&
                Mathf.Approximately(vector1.y, vector2.y) &&
                Mathf.Approximately(vector1.z, vector2.z);
        }
    }
}
