using UnityEngine;

namespace PhysicsQuery.Editor
{
    public readonly struct PreviewResults
    {
        public Result<RaycastHit> CastResult => _castResult;
        public Result<Collider> OverlapResult => _overlapResult;

        private readonly Result<RaycastHit> _castResult;
        private readonly Result<Collider> _overlapResult;

        public PreviewResults(Result<RaycastHit> castResult, Result<Collider> overlapResult)
        {
            _castResult = castResult;
            _overlapResult = overlapResult;
        }
    }
}