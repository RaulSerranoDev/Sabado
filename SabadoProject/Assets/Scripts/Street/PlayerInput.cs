using UnityEngine;

namespace Sabado
{
    public class PlayerInput : MonoBehaviour
    {
        private void Update()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                // Handle screen touches.
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    // Ha colisionado con algo
                    if (hit)
                    {
                        Place touchedPlace = hit.collider.GetComponent<Place>();

                        //Ha colisionado con un lugar
                        if (touchedPlace)
                        {
                            Debug.Log("oleee");
                        }
                    }
                }
            }
        }
    }
}
