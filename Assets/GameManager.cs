using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour {
	
	private const int MAX_ORB = 10;

	private const int RESPAWN_TIME=1;

	private const int MAX_LEVEL=2;

	public GameObject orbPrefab;

	public GameObject smokePrefab;

	public GameObject kusudamaPrefab;

	public GameObject canvasGame;

	public GameObject textScore;

	public GameObject ImageTemple;



	int score=0;
	int nextscore=10;
	int currentOrb=0;
	int templelevel=0;

	DateTime lastDateTime;

	int[] nextScoreTable =new int[] {10,10,10};
	// Use this for initialization
	void Start () {
		currentOrb=10;

		for (int i = 0; i < currentOrb; i++) {
			CreateOrb();
		}
		lastDateTime = DateTime.UtcNow;
		nextscore=nextScoreTable [templelevel];
		ImageTemple.GetComponent<TempleManager> ().SetTemplePicture (templelevel);
		ImageTemple.GetComponent<TempleManager> ().SetTempleScale (score,nextscore);
		RefreshScoreText();

	}
	
	// Update is called once per frame
		void Update () {
			// 現在のオーブの表示数が最大数未満なら
			if (currentOrb < MAX_ORB)
			{
				// 最後にオーブを生成した時間との差分を取得して
				TimeSpan timeSpan = DateTime.UtcNow - lastDateTime;
				// 設定した間隔以上経過していれば
				if (timeSpan >= TimeSpan.FromSeconds(RESPAWN_TIME))
				{
					// 経過時間に対応した数のオーブを生成する
					while(timeSpan >= TimeSpan.FromSeconds(RESPAWN_TIME))
					{
						// 新しいオーブを１つ生成する
						CreateNewOrb();
						// timeSpanを間隔分減算する
						timeSpan -= TimeSpan.FromSeconds(RESPAWN_TIME);
					}
				}
			}
		}
					


	public void CreateNewOrb(){
		lastDateTime = DateTime.UtcNow;
		if (currentOrb >= MAX_ORB) {
			return;
		}
		CreateOrb ();
		currentOrb++;
	}




	public void CreateOrb(){
		GameObject orb = (GameObject)Instantiate (orbPrefab);
		orb.transform.SetParent (canvasGame.transform,false);
		orb.transform.position = new Vector3 (UnityEngine.Random.Range(200,300),UnityEngine.Random.Range(500,600),0);

	}
	public void GetOrb(){
		score += 1;
		if (score > nextscore) {
			score = nextscore;
		}
		TempleLevelUp ();
		RefreshScoreText ();
		ImageTemple.GetComponent<TempleManager> ().SetTempleScale (score,nextscore);
		if((score==nextscore)&&(templelevel==MAX_LEVEL)){
			ClearEffect();
		}
			currentOrb--;
			}
			
	void RefreshScoreText(){
		textScore.GetComponent<Text> ().text =
			"徳:" + score + "/" + nextscore;
		
	}
	void TempleLevelUp(){
		if(score>=nextscore){
			if(templelevel<MAX_LEVEL){
				templelevel++;
				score = 0;
				TempleLevelUpEffect ();
				nextscore = nextScoreTable [templelevel];
				ImageTemple.GetComponent<TempleManager> ().SetTemplePicture (templelevel);
	}
		}
	}
		void TempleLevelUpEffect(){
		GameObject smoke = (GameObject)Instantiate (smokePrefab);
		smoke.transform.SetParent (canvasGame.transform,false);
		smoke.transform.SetSiblingIndex (2);

		Destroy(smoke ,0.5f);
		}

	void ClearEffect(){
		GameObject kusudama = (GameObject)Instantiate (kusudamaPrefab);
		kusudama.transform.SetParent(canvasGame.transform,false);
	}
}