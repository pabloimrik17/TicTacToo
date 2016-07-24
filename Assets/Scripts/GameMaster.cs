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


	void Awake() {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start() {
		Screen.fullScreen = false;
		string[] texto = Translator.Instance.GetText ("StartJugarIA", "es");
		print (texto [0]);
		print (SceneManager.GetActiveScene ().name);
		//TraduceUI ();


	}
	
	// Update is called once per frame
	void Update() {
	
	}

	void OnLevelWasLoaded(int nivel) {
		print ("llego");
		if (nivel == 1) {
			TraduceUI ();
			print ("hola");
		}
	}

	public void TraduceUI() {
		GameObject[] arrUI = GameObject.FindGameObjectsWithTag ("UITraducible");
		string valor = "";
		foreach (GameObject ui in arrUI) {
			valor = Translator.Instance.GetText (ui.name, "es") [0];
			if (valor != "") {
				ui.GetComponent<UnityEngine.UI.Text> ().text = valor;
			}
		}    
	}
}
