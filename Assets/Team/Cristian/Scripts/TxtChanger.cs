using UnityEngine;
using TMPro;
public class TxtChanger : MonoBehaviour
{

    public TextMeshProUGUI textMesh;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Holding.... Nothing. "))
        {
            textMesh.text = "Holding flashLight";
        }
    }




}
