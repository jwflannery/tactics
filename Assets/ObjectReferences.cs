using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CreativeSpore.SuperTilemapEditor;

public class ObjectReferences : MonoBehaviour {

    public static ObjectReferences Instance;
    public GameObject ForegroundTilemap;
    public GameObject BackgroundTilemap;
    public GameObject UnitTilemap;
    public GameObject ColliderTilemap;

    public Text TileInfoText;
    public Text UnitInfoText;
    public TurnTextScript TurnTextScript;
    
    public TextMeshProUGUI DialogueText;
    public Image PortraitImage;
    public DialogueHandler DialogueHandlerScript;

    public static Vector2 CellSize = new Vector2(0.16f, 0.16f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GetTilemapReferences();
        GetUserInterfaceReferences();
        GetDialogueReferences();
    }

    // Use this for initialization
    void Start () {

    }

    void GetTilemapReferences()
    {
        ForegroundTilemap = GameObject.Find("/Test Map");
        BackgroundTilemap = ForegroundTilemap;
        UnitTilemap = ForegroundTilemap;
    }

    void GetUserInterfaceReferences()
    {
        TileInfoText = GameObject.Find("/Canvas/Tile Info Panel/Tile Info Text").GetComponent<Text>();
        UnitInfoText = GameObject.Find("Canvas/Unit Info Panel/Unit Info Text").GetComponent<Text>();

        TurnTextScript = GameObject.Find("Canvas/Turn Text").GetComponent<TurnTextScript>();

    }

    void GetDialogueReferences()
    {
        DialogueText = GameObject.Find("/Canvas/Dialogue Panel/TextMeshPro Text").GetComponent<TextMeshProUGUI>();
        PortraitImage = GameObject.Find("/Canvas/Dialogue Panel/Portrait").GetComponent<Image>();
        DialogueHandlerScript = GameObject.Find("/Canvas/Dialogue Panel").GetComponent<DialogueHandler>();
    }

}
