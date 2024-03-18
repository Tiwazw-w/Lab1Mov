using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler_1 : MonoBehaviour
{
    public GameObject figuraPrefab;
    public Color colorFigura;

    private float lastTapTime;
    private Vector2 lastTapPosition;

    void Update()
    {
        // Verificar si hay toques en la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Solo consideramos el primer toque

            // Verificar si es un toque que acaba de comenzar
            if (touch.phase == TouchPhase.Began)
            {
                // Obtener la posición del toque
                Vector2 tapPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // Verificar si es un double tap
                if (Time.time - lastTapTime < 1.00f && (tapPosition - lastTapPosition).sqrMagnitude < 1.00f)
                {
                    // Lanzar un rayo desde la posición del toque
                    RaycastHit2D hit = Physics2D.Raycast(tapPosition, Vector2.zero);

                    // Verificar si el rayo intersecta con algún collider
                    if (hit.collider != null)
                    {
                        // Eliminar el objeto intersectado
                        Destroy(hit.collider.gameObject);
                        Debug.Log("Se eliminó el objeto.");
                    }
                }
                else
                {
                    // Si no es un double tap, crear una figura en esa posición
                    CrearFigura(tapPosition);
                }

                // Actualizar el tiempo y la posición del último tap
                lastTapTime = Time.time;
                lastTapPosition = tapPosition;
            }
        }
    }

    void CrearFigura(Vector2 posicion)
    {
        GameObject nuevaFigura = Instantiate(figuraPrefab, posicion, Quaternion.identity);

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