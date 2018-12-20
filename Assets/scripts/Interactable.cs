using UnityEngine;

namespace Assets.scripts
{

    [RequireComponent(typeof(ColorOnHover))]
    public class Interactable : MonoBehaviour
    {
        public float radius = 3f;
        public Transform interactionTransform;

        bool isFocus = false;
        Transform player;

        bool hasInteracted = false;

        public virtual void Interact()
        {
            Debug.Log("Interacting with " + transform.name);
        }

        public void OnFocused(Transform playerTransform)
        {
            isFocus = true;
            player = playerTransform;
            hasInteracted = false;
        }

        public void OnDefocused()
        {
            isFocus = false;
            player = null;
            hasInteracted = false;
        }

        public void Update()
        {
           if(isFocus && !hasInteracted)
            {
                float distance = Vector2.Distance(player.position, interactionTransform.position);
                //Debug.Log("distance = " + distance);
                if(distance <= radius)
                {
                    Debug.Log("Interacting with " + transform.name);
                    Interact();
                    hasInteracted = true;
                }
            }
        }

        public void OnDrawGizmosSelected()
        {
            if(interactionTransform == null)
            {
                interactionTransform = transform;
            }
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(interactionTransform.position, radius);
        }

    }
}
