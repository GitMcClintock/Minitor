using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts
{
    public class PlayerMotor : MonoBehaviour
    {
        public Transform target;
        public MyAgent agent;

        public void Start()
        {
            agent = new MyAgent();
        }

        public void Update()
        {
            if(target != null)
            {
                agent.SetDestination(target.position);
                FaceTarget();
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            agent.SetDestination(point);
        }

        public void FollowTarget(Interactable newTarget)
        {
            if (agent == null) return;
            agent.stoppingDistance = newTarget.radius * 0.8f;
            agent.updateRotation = false;
            target = newTarget.interactionTransform;
        }

        public void StopFollowingTarget()
        {
            if (agent == null) return;
            agent.stoppingDistance = 0f;
            agent.updateRotation = true;
        }

        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);   // smoothing
        }

    }
}
