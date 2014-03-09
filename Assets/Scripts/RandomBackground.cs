using UnityEngine;
using System.Collections;

public class RandomBackground : MonoBehaviour {

	public Sprite[] CheckerTilesA;
	public Sprite[] CheckerTilesB;
	public Sprite[] DecorationTiles;
	public int[] ChancesOfDecoration;
	public int ChanceOfNoDecoration;
	private int TotalChancesOfDecoration;
	public GameObject MotherTile;

	private Vector3 StartingPoint {
		get {
			float x = transform.position.x - transform.localScale.x / 2f + 0.5f;
			float y = transform.position.y - transform.localScale.y / 2f + 0.5f;
			float z = transform.position.z;
			return new Vector3(x,y,z);
		}
	}

	void Start () {
		renderer.enabled = false;
		if(CheckerTilesA.Length < 1 || CheckerTilesB.Length < 1)
			throw new UnityException("The Random Background needs tile sprites.");
		if(!MotherTile)
			throw new UnityException("The Random Background needs the Mother Tile prefab.");
		if(DecorationTiles.Length != ChancesOfDecoration.Length)
			throw new UnityException("The Random Background needs a chance per decoration.");

		Vector3 storeScale = transform.parent.transform.localScale;
		transform.parent.transform.localScale = Vector3.one;

		TotalChancesOfDecoration = ChanceOfNoDecoration;
		foreach(int chance in ChancesOfDecoration)
			TotalChancesOfDecoration += chance;

		SpawnCheckerTiles();
		SpawnDecorationTiles();

		transform.parent.transform.localScale = storeScale;
	}
	
	void SpawnCheckerTiles() {
		for( int i = 0; i < transform.localScale.x; i++ ) {
			bool TileA = i%2 == 0;
			for( int j = 0; j < transform.localScale.y; j++ ) {
				SpawnCheckerTile(TileA, i, j);
				TileA = !TileA;
			}
		}
	}
	
	void SpawnDecorationTiles() {
		for( int i = 0; i < transform.localScale.x; i++ ) {
			for( int j = 0; j < transform.localScale.y; j++ ) {
				SpawnDecorationTile(i, j);
			}
		}
	}

	void SpawnCheckerTile(bool TileA, int i, int j) {
		int randomTile = TileA ? RandomTileA() : RandomTileB();
		Sprite tileSprite = TileA ? CheckerTilesA[randomTile] : CheckerTilesB[randomTile];
		GameObject tile = (GameObject)Instantiate(MotherTile, SpawnPositionCheckerTile(i, j), Quaternion.identity);
		tile.transform.parent = this.transform.parent;
		SpriteRenderer tileSpriteRenderer = (SpriteRenderer)tile.GetComponent("SpriteRenderer");
		tileSpriteRenderer.sprite = tileSprite;
	}

	void SpawnDecorationTile(int i, int j) {
		int randomTile = RandomDecorationTile();
		if(randomTile == -1)
			return;
		Sprite tileSprite = DecorationTiles[randomTile];
		GameObject tile = (GameObject)Instantiate(MotherTile, SpawnPositionDecorationTile(i, j), Quaternion.identity);
		tile.transform.parent = this.transform.parent;
		SpriteRenderer tileSpriteRenderer = (SpriteRenderer)tile.GetComponent("SpriteRenderer");
		tileSpriteRenderer.sprite = tileSprite;
	}

	int RandomTileA() {
		return Random.Range(0, CheckerTilesA.Length-1);
	}

	int RandomTileB() {
		return Random.Range(0, CheckerTilesB.Length-1);
	}

	int RandomDecorationTile() {
		int randomNumber = Random.Range(0, TotalChancesOfDecoration);
		int chanceCounter = 0;
		for(int i = 0; i < ChancesOfDecoration.Length; i++) {
			int chance = ChancesOfDecoration[i];
			chanceCounter += chance;
			if(chanceCounter >= randomNumber)
				return i;
		}
		return -1;
	}

	Vector3 SpawnPositionCheckerTile(int i, int j) {
		float x = StartingPoint.x + i;
		float y = StartingPoint.y + j;
		float z = StartingPoint.z - 1;
		return new Vector3(x,y,z);
	}

	Vector3 SpawnPositionDecorationTile(int i, int j) {
		float x = StartingPoint.x + i;
		float y = StartingPoint.y + j;
		float z = StartingPoint.z - 2;
		return new Vector3(x,y,z);
	}
}
