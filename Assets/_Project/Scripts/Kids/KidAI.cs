using Pathfinding;
using UnityEngine;

namespace gishadev.gmtk.kids
{
    [RequireComponent(typeof(FollowerEntity))]
    public class KidAI : MonoBehaviour
    {
        private FollowerEntity _followerEntity;

        public bool ReachedDestination => _followerEntity.reachedDestination;

        private void Awake()
        {
            _followerEntity = GetComponent<FollowerEntity>();
        }

        public void MoveToPOI(IPOI poi)
        {
            _followerEntity.destination = poi.transform.position;
        }

        public void Stop()
        {
            _followerEntity.destination = transform.position;
        }
    }
}
