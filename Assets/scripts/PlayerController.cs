using Assets.scripts;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public PlayerMotor motor;


    public void Start()
    {
        //motor = new PlayerMotor();
    }

    public void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            // Don't move if cursor over Inventory
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 2.0f))
            {
                //Debug.Log("You clicked a " + hit.GetType());
                //Debug.Log("You selected the " + hit.transform.tag);

                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    Debug.Log("That is interactable!");
                    SetFocus(interactable);
                }
                else
                {
                    Debug.Log("That is not interactable");
                }
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            // Not focussed on anything now...
            RemoveFocus();
        }
    }


    private void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null) focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
        if(motor != null) motor.FollowTarget(newFocus);
    }

    private void RemoveFocus()
    {
        if(focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        if (motor != null) motor.StopFollowingTarget();
    }


}

