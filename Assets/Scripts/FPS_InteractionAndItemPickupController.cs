using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPS_InteractionAndItemPickupController : MonoBehaviour
{

    public Container inv;

    public UI_InteractionIndicator interactionIndicator;
    public Canvas invUI;

    private Character player;

    [SerializeField]
    private float raycastDistance;

    private int layer_mask;

    void Start()
    {
        layer_mask = LayerMask.GetMask("Item", "Interactable");
        player = gameObject.transform.parent.gameObject.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (invUI != null) {
                invUI.enabled = !invUI.enabled;
                Cursor.visible = invUI.enabled;
                Cursor.lockState = invUI.enabled ? CursorLockMode.None : CursorLockMode.Locked;
            }
            
        }
    }

    private void FixedUpdate()
    {
        doRayCast();
    }

    private void doRayCast()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastDistance, Color.yellow);
        if (Physics.Raycast(transform.position, fwd, out hit, raycastDistance, layer_mask)) {

            var hitGameObject = hit.transform.gameObject;
            if (hitGameObject == null) return;

            if (hitGameObject.tag == "Item") {
                Item item = hitGameObject.GetComponent<PhysicalItem>()?.GetItem();

                interactionIndicator.Display(item.GetDisplayMessage()) ;

                if (Input.GetKey(KeyCode.F)) {
                    
                    hitGameObject.GetComponent<PhysicalItem>()?.RemoveItem();
                    this.inv.AddItem(item);
                }
                

            } else {
                interactionIndicator.Display();
            }

        } else {
            interactionIndicator.Hide();
        }
    }
}
