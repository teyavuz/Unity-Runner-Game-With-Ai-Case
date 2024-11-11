using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiningObject : Obstacles
{
    [SerializeField] private float moveLastLocation;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private float speed;

    private int ourPlayerLayer;

    private void Start()
    {
        MoveObstacle(moveLastLocation, gameObject.transform, speed);

        ourPlayerLayer = LayerMask.NameToLayer("OurPlayer");
    }
    void Update()
    {
        RotateObstacle(gameObject.transform, 0.1f, 0, 1, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var mainModule = ps.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(GetRandomColor(Color.white));



            CharacterBase playerController = other.gameObject.GetComponent<CharacterBase>();

            if (playerController != null)
            {
                playerController.TeleportStartPosition();
            }

            if (other.gameObject.layer == ourPlayerLayer)
            {
                DeathManager.Instance.DeathCountUpdater();
            }
        }
    }

    private Color GetRandomColor(Color currentColor)
{
    List<Color> colors = new List<Color>
    {
        Color.red,            // Kırmızı
        new Color(1f, 0.5f, 0f), // Turuncu
        Color.yellow,         // Sarı
        Color.green,          // Yeşil
        new Color(1f, 0.08f, 0.58f), // Pembe
        Color.blue,           // Mavi
        new Color(0.5f, 0f, 0.5f) // Mor
    };

    Color selectedColor;
    
    do
    {
        selectedColor = colors[Random.Range(0, colors.Count)];
    } 
    while (selectedColor == currentColor);
    
    return selectedColor;
}

}
