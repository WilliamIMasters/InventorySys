using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPS_InteractionAndItemPickupController : MonoBehaviour
{
    public Character player;
    private Container inv;



    public UI_InteractionIndicator interactionIndicator;
    public Canvas invUI;
    public Canvas foreignContainerUI;


    public ContainerManager invManager;
    [SerializeField]
    private ContainerManager foreignContainerManager;
    [SerializeField]
    private float raycastDistance;

    private int layer_mask;

    void Start()
    {
        layer_mask = LayerMask.GetMask("Item", "Intractable");
        player = this.player ?? gameObject.transform.parent.gameObject.GetComponent<Character>();
        inv = player.inventory;
        invManager.SetContainer(inv);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (invUI != null) {

                if(!invUI.enabled) {
                    ShowInventory();
                    HideForeignContainer();
                } else {
                    HideInventory();
                    HideForeignContainer();
                }
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
            interactionIndicator.Display(hit.transform.gameObject.GetComponent<IIntractable>().GetDisplayMessage());
            var hitGameObject = hit.transform.gameObject;
            if (hitGameObject == null) return;

            if (hitGameObject.tag == "Item") {
               
                if (Input.GetKey(KeyCode.F)) {
                    Item item = hitGameObject.GetComponent<PhysicalItem>()?.GetItem();
                    hitGameObject.GetComponent<PhysicalItem>()?.RemoveItem();
                    this.inv.AddItem(item);
                }


            } else if (hitGameObject.tag == "Container") {
                if (Input.GetKey(KeyCode.F)) {

                    PhysicalContainer container = hitGameObject.GetComponent<PhysicalContainer>();
                    ShowForeignContainer(container.container);
                }
                
            } else {
                //interactionIndicator.Display();
            }

        } else {
            interactionIndicator.Hide();
        }
    }


    private void ShowInventory()
    {
        invUI.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void HideInventory()
    {
        invUI.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void ShowForeignContainer(Container container)
    {
        ShowInventory();
        foreignContainerManager.SetContainer(container);
        foreignContainerUI.enabled = true;
    }
    private void HideForeignContainer()
    {
        foreignContainerUI.enabled = false;
    }

}
