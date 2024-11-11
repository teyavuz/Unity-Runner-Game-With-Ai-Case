using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pencil : MonoBehaviour
{
    [Header("Pencil")]
    [SerializeField] private Texture2D brush;
    [SerializeField] private Color32 yellowPenColor;
    [SerializeField] private Color32 redPenColor;
    [SerializeField] private Color32 bluePenColor;

    private Color32 penColor;
    private float penSize = 10f;
    private Color32 mainColor;
    private Texture2D texture;

    [Header("MeshRenderer etc.")]
    private MeshRenderer meshRenderer;

    [Header("Camera")]
    [SerializeField] private Camera usinCamera;

    [Header("PenSize")]
    [SerializeField] private Slider slider;

    [Header("Hesap Kitap")]
    private int totalPixels;
    private int paintedPixels;
    private HashSet<Vector2> paintedPixelCoordinates = new HashSet<Vector2>();

    [Header("Percent Text")]
    [SerializeField] private TextMeshProUGUI percentText;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        mainColor = new Color32(255, 255, 255, 255);

        texture = new Texture2D(512, 512);
        texture.filterMode = FilterMode.Point;

        totalPixels = texture.width * texture.height;
        paintedPixels = 0;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texture.SetPixel(x, y, mainColor);
            }
        }

        texture.Apply();
        meshRenderer.material.mainTexture = texture;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameStates.painting)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = usinCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector2 textureCoord = hit.textureCoord;

                    int x = Mathf.FloorToInt(textureCoord.x * texture.width);
                    int y = Mathf.FloorToInt(textureCoord.y * texture.height);

                    Paint(x, y);
                }
            }
        }
    }

    private void Paint(int x, int y)
    {
        int halfBrushSize = Mathf.FloorToInt(penSize / 2);

        for (int i = -halfBrushSize; i < halfBrushSize; i++)
        {
            for (int j = -halfBrushSize; j < halfBrushSize; j++)
            {
                int pixelX = x + i;
                int pixelY = y + j;

                if (pixelX >= 0 && pixelX < texture.width && pixelY >= 0 && pixelY < texture.height)
                {
                    // Boyanmışsa sayılmsn diye burası
                    if (!paintedPixelCoordinates.Contains(new Vector2(pixelX, pixelY)))
                    {
                        paintedPixelCoordinates.Add(new Vector2(pixelX, pixelY));
                        paintedPixels++;
                    }

                    texture.SetPixel(pixelX, pixelY, penColor);
                }
            }
        }

        texture.Apply();

        // Yuvarlama(DAHA İDEALİ VAR MI BAK!)
        int paintedPercentage = Mathf.FloorToInt((float)paintedPixels / totalPixels * 100f);
        Debug.Log($"Percent: {paintedPercentage}%");
        percentText.text = $"{paintedPercentage}%";

        if (paintedPercentage >= 99.75)
        {
            AudioManager.Instance.PauseMusic();
            AudioManager.Instance.PlayEffect(1);
            GameManager.Instance.gameState = GameManager.GameStates.end;
            percentText.text = "100%";
            GameManager.Instance.CloseAllCanvases();
            GameManager.Instance.OpenSpecificCanvas(2);
            Time.timeScale = 0f;
        }
    }


    //Pen Size Function
    public void PenSizeChanger()
    {
        penSize = slider.value;
    }

    // Color Button Functions
    public void SetYellowPen()
    {
        penColor = yellowPenColor;
    }

    public void SetRedPen()
    {
        penColor = redPenColor;
    }

    public void SetBluePen()
    {
        penColor = bluePenColor;
    }
}
