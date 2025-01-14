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

        private void CreatePhysicsQuery()
        {
            GameObject container = new("Physics Caster");
            _query = container.AddComponent<EmptyQuery>();
        }
    }
}
