using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Audio.GeneratorInstance;

public class TeleportController : MonoBehaviour
{
    //Configuración importante en Unity:
    //   - Capa de obstáculos(Obstacle Layer) : Crea una "Layer" llamada por ejemplo Obstaculos (Esto en el tilemap) en Unity y asígnasela a tus paredes o elementos sólidos.

    //   - Asignar la capa en el Inspector: En el componente de este script, verás un campo llamado Obstacle Layer.Selecciona ahí la capa que acabas de crear para que el script sepa qué debe evitar.

    //   - Radio de verificación(Check Radius) : Ajústalo según el tamaño de tu personaje para que la zona comprobada cubra su cuerpo.
    [Header("Área de Teletransporte")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    [Header("Detección de Obstáculos")]
    public LayerMask obstacleLayer;
    public float checkRadius;
    public int maxAttempts = 30;

    public void Teleport()
    {
        bool success = false;
        Vector2 randomPosition = Vector2.zero;

        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            randomPosition = new Vector2(randomX, randomY);

            Collider2D hit = Physics2D.OverlapCircle(randomPosition, checkRadius, obstacleLayer);

            if (hit == null)
            {
                success = true;
                break;
            }
        }

        if (success)
        {
            transform.position = randomPosition;
            Debug.Log("Teletransportado con éxito a: " + randomPosition);
        }
        else
        {
            Debug.LogWarning("No se encontró una posición libre sin obstáculos después de varios intentos.");
        }
    }
}