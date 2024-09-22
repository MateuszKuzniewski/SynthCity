using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private Camera cam;

    private GameObject selectedObject;
    private GameObject selectedEnemy;

    public Button weaponAbilitySlot, abilitySlot1, abilitySlot2, abilitySlot3, abilitySlot4, abilitySlot5;

    private PlayerMovement pMoves;
    private bool isWeaponPrepared;
    private bool canUseWeapon = true;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        cam = Camera.main;

        pMoves = Player.instance.gameObject.GetComponent<PlayerMovement>();

    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            //SelectTarget(CastRay());
            
        }
        else if(Input.GetMouseButtonUp(1))
        {
            pMoves.PrepareWeapon(false);
            isWeaponPrepared = false;
        }

        // Aim weapon
        if(Input.GetMouseButton(1))
        {
       
            pMoves.PrepareWeapon(true);
            isWeaponPrepared = true;
           
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(isWeaponPrepared)
            {
                Weapons weapon = (Weapons)Inventory.instance.GetEquippedWeapon();
                if (weapon != null && canUseWeapon)
                {
                    weapon.UseWeapon();
                    canUseWeapon = false;
                    StartCoroutine(AttackDelay());
                }
                else if(weapon == null)
                    FloatingText.DisplayFloatingText("No weapon equipped");

            }
            else
                SelectTarget(CastRay());
        }

        // Clear Selected Object
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearTarget();

        }


        if (Input.GetKeyDown(KeyCode.B))
            UI.instance.ToggleInventory();

        if (Input.GetKeyDown(KeyCode.E))
            weaponAbilitySlot.GetComponent<AbilitySlot>().UseAbility();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            abilitySlot1.GetComponent<AbilitySlot>().UseAbility();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            abilitySlot2.GetComponent<AbilitySlot>().UseAbility();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            abilitySlot3.GetComponent<AbilitySlot>().UseAbility();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            abilitySlot4.GetComponent<AbilitySlot>().UseAbility();

        if (Input.GetKeyDown(KeyCode.Alpha5))
            abilitySlot5.GetComponent<AbilitySlot>().UseAbility();
    }

    public GameObject CastRay()
    {
        GameObject temp = null;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            temp = hit.transform.gameObject;
        }
        return temp;
    }

    public void SelectTarget(GameObject target)
    {
        if(selectedObject != target)
        {
            DeselectTarget();
            selectedObject = target;
            var enemyComponent = target.GetComponent<Enemy>();

            if (null != enemyComponent)
            {
                selectedEnemy = selectedObject;
                UI.instance.EnemyPortait.SetActive(true);
                UI.instance.UpdateEnemyHealthBar(selectedObject);
                selectedObject.GetComponent<Enemy>().targetMarker.SetActive(true);
            }
        }
    }

    public void DeselectTarget()
    {
       
        if (selectedEnemy != null)
        {
            UI.instance.EnemyPortait.SetActive(false);
            selectedObject.GetComponent<Enemy>().targetMarker.SetActive(false);
            selectedEnemy = null;
            selectedObject = null;
        }
       
    }

    public void ClearTarget()
    {
        UI.instance.EnemyPortait.SetActive(false);
        selectedObject.GetComponent<Enemy>().targetMarker.SetActive(false);
        selectedEnemy = null;
        selectedObject = null;
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    // Only for abilities
    public GameObject GetSelectedEnemy()
    {
        return selectedEnemy;
    }

    private IEnumerator AttackDelay()
    {
        Weapons weapon = (Weapons)Inventory.instance.GetEquippedWeapon();
        if (weapon != null)
            yield return new WaitForSeconds(weapon.weaponSpeed);

        canUseWeapon = true;
    }
}
