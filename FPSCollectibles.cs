using TMPro;
using UnityEngine;

public class FPSCollectibles : MonoBehaviour
{
    private bool onInspector;
    private FPSGunController gunController;

    public TextMeshProUGUI txtCollectMessage, txtObjectName, txtObjectDescription;
    public Transform inspector;

    void Start()
    {
        gunController = GetComponent<FPSGunController>();
    }
    void Update()
    {
        FPSProperties.canMove = !onInspector;
        FPSProperties.canMoveCam = !onInspector;

        var closestObject = SetCollect();

        if (closestObject != null)
        {
            if (onInspector && Input.GetKeyDown(KeyCode.F))
            {
                CloseInspector(closestObject);
            }
        }
    }

    public GameObject SetCollect()
    {
        var objects = GameObject.FindGameObjectsWithTag("collectibleObject");

        if(objects.Length > 0)
        {
            var closestObject = Generic.GetClosestObject(transform, objects);

            var distance = Vector3.Distance(transform.position, closestObject.transform.position);
            txtCollectMessage.gameObject.SetActive(distance <= 1.0f);

            if (!onInspector && Input.GetKeyDown(KeyCode.E) && distance <= 1.0f)          
            {
                txtCollectMessage.text = "Press [F] to return";

                OpenInspector(closestObject);
                CollectItem(closestObject);
            }

            return closestObject;
        }
        else
        {
            txtCollectMessage.gameObject.SetActive(false);
        }

        return null;
    }

    public void CollectItem(GameObject obj) 
    {
        var item = obj.GetComponent<Collectible>();

        if (item.type == Enums.ItemType.Ammo)
        {
            foreach (var gun in gunController.inventory)
            {
                if (gun.gunName == item.weaponName)
                {
                    gun.ammunitionToReload += item.value;
                    return;
                }
            }
        }
    }

    public void OpenInspector(GameObject obj)
    {
        onInspector = true;

        DestroyObjectsOfInspector();

        var properties = obj.GetComponent<Collectible>();
        txtObjectName.text = properties.objectName;
        txtObjectDescription.text = properties.objectDescription;
        
        var collectible = Instantiate(obj, inspector.position, inspector.rotation);
        collectible.tag = "collectible";
        collectible.AddComponent<CollectibleController>();
    }

    public void CloseInspector(GameObject closestObject)
    {
        DestroyObjectsOfInspector();
        txtObjectName.text = "";
        txtObjectDescription.text = "";
        txtCollectMessage.text = "Press [E] to collect the item";
        onInspector = false;

        Destroy(closestObject);
    }

    private void DestroyObjectsOfInspector() 
    {
        var objectsInScene = GameObject.FindGameObjectsWithTag("collectible");
        if (objectsInScene.Length > 0)
        {
            foreach (var elem in objectsInScene)
            {
                Destroy(elem);
            }
        }
    }

    
}
