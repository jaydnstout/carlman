using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject food;


    private void Start()
    {
        food.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.name == "player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);


                food.SetActive(true);
            }
            
        }


    }

}
