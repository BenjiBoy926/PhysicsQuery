using UnityEngine;

namespace PhysicsQuery
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
        public static PreviewResults Cast(Result<RaycastHit> castResult)
        {
            return new(castResult, new());
        }
        public static PreviewResults Overlap(Result<Collider> overlapResult)
        {
            return new(new(), overlapResult);
        }
    }
}