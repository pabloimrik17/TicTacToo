using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

  private static GameMaster instance;
  public TextAsset stringFile;

  public static GameMaster Instance {
        get {
            if (instance == null) {
                instance = new GameMaster ();
            }

            return instance;
        }
    }
	// Use this for initialization
	void Start () {
        Translator a = new Translator(stringFile);
        string[] texto = a.GetText("uniqueID", "en");
        print(texto[0]);
        print(SceneManager.GetActiveScene().name);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
