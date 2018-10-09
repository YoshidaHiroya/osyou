using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	
	public int MAX_ORB = 10;

	public GameObject orbPrefab;

	public GameObject canvasGame;

	public GameObject textScore;

	int score=0;
	int nextscore=100;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < MAX_ORB; i++) {
			CreateOrb ();
		}
		RefreshScoreText ();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void CreateOrb(){
		GameObject orb = (GameObject)Instantiate (orbPrefab);
		orb.transform.SetParent (canvasGame.transform,false);
		orb.transform.position = new Vector3 (Random.Range(200,300),Random.Range(500,600),0);

	}
	public void GetOrb(){
		score += 1;
		RefreshScoreText ();
}
	void RefreshScoreText(){
		textScore.GetComponent<Text> ().text =
			"徳:" + score + "/" + nextscore;
	}
}