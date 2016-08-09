using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour {
	
	Image logo;
	GameObject objeto, objeto2;
	Canvas canvas;
	public int movimiento;
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(2);
		logo = GameObject.Find("Logo").GetComponent<Image>();
		objeto = GameObject.Find("StartJugarIA");
		objeto2 = GameObject.Find("StartJugarOnline");

		logo.DOFade(1, 2);
		Vector3 original = objeto.transform.position;
		Sequence s =  DOTween.Sequence();
		s.Append(objeto.transform.DOMoveY(30,1.5f).SetRelative(true).SetEase(Ease.InBounce));
		s.Append(objeto.transform.DOMove(original, 0.7f).SetEase(Ease.InBack));
		s.SetLoops(1);

		objeto2.transform.DOLocalMove(new Vector3(-160, -105, 0), 10).From().SetRelative(false);



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
