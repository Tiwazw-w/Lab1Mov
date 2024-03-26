using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject figuraPrefab;
    public Color colorFigura;

    private float lastTapTime;
    private Vector2 lastTapPosition;
    private Transform cositos;
    private Vector2 INT;
    private Vector2 ENDT;
    [SerializeField]
    private List<GameObject> array;
    

    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 tapPosition = Camera.main.ScreenToWorldPoint(touch.position);
            transform.position = tapPosition;

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                INT = touch.position;
                RaycastHit2D hit = Physics2D.Raycast(tapPosition, Vector2.zero);

                if (touch.tapCount == 1)
                {
                    if (hit.collider != null)
                    {
                        cositos = hit.collider.transform;
                    }
                    else{
                        CrearFigura(tapPosition);
                    }
                }
                else if (touch.tapCount == 2 && hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
            }

            if(touch.phase == UnityEngine.TouchPhase.Moved)
            {
                if (cositos != null)
                {
                    cositos.transform.position = tapPosition;
                }
            }else if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                cositos = null;
                ENDT = touch.position;
                Vector2 swipeD = ENDT - INT;

                float swipeM = swipeD.magnitude;
                if (swipeM > 0.6f && array != null)
                {
                    for(int i = 0; i < array.Count; i++)
                    {
                        Destroy(array[i]);
                       
                    }
                    array.Clear();
                }
            }
        }

    }

    void CrearFigura(Vector2 posicion)
    {
        GameObject nuevaFigura = Instantiate(figuraPrefab, posicion, Quaternion.identity);
        array.Add(nuevaFigura);

        SpriteRenderer spriteRenderer = nuevaFigura.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = colorFigura;
        }
        else
        {
            Debug.LogError("El prefab de la figura no tiene un SpriteRenderer.");
        }
    }
}
