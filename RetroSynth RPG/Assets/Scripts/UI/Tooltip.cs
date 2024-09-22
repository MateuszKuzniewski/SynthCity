
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;



public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;

    public Canvas popupCanvasObject;
    public GameObject popupObject;
    public TextMeshProUGUI infoText;
    public Vector3 offset;
    public float paddingLeft;
    public float paddingRight;
    public float paddingTop;

    private RectTransform rct;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        CloseToolTip();
        rct = popupObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;

        float rightEdge = Screen.width - (newPos.x + rct.rect.width * popupCanvasObject.scaleFactor / 2) - paddingRight;
        if (rightEdge < 0)
            newPos.x += rightEdge;

        float leftEdge = 0 - (newPos.x - rct.rect.width * popupCanvasObject.scaleFactor / 2) + paddingLeft;
        if (leftEdge > 0)
            newPos.x += leftEdge;

        float topEdge = Screen.height - (newPos.y + rct.rect.height * popupCanvasObject.scaleFactor / 2) - paddingTop;
        if (topEdge < 0)
            newPos.y += topEdge;

        popupObject.transform.position = newPos;

    }

    public static void ShowToolTip(Item data)
    {
        UpdateToolTip(data);
        instance.popupObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(instance.rct);

    }

    public static void CloseToolTip()
    {
        instance.popupObject.SetActive(false);
    }

    private void updateTT(Item data)
    {
        StringBuilder builder = new StringBuilder();

        if (data != null)
            builder.Append(data.itemDescription);
        else
            CloseToolTip();

        instance.infoText.text = builder.ToString();
    }

    public static void UpdateToolTip(Item data)
    {
        
        instance.updateTT(data);
        LayoutRebuilder.ForceRebuildLayoutImmediate(instance.rct);
    }
}
