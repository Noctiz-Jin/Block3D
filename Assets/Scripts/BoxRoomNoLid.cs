using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRoomNoLid : MonoBehaviour {

	// 3 dimensions of the box room
	public int lx;
	public int ly;
	public int lz;

	//obstacle radius
	public int obs1;
	public int obs2;

	// blocks
	public GameObject dirt;
	public GameObject grass;
	public GameObject stone;
	public GameObject invisible;

	//Parent to all GameObjects
	private GameObject boardHolder;
	private GameObject obstacleHolder;


	// boundary of the scene
	private int nx;
	private int px;
	private int nz;
	private int pz;

	public void SetupScene() {
		nx = -lx / 2;
		px = lx - lx / 2 - 1;
		nz = -lz / 2;
		pz = lz - lz / 2 - 1;

		boardHolder = new GameObject ("GroundLevel");
		obstacleHolder = new GameObject ("ObstacleLevel");

		SetupGround ();
		SetupObstacle (obs1, dirt);
		SetupObstacle (obs2, grass);
	}

	private void SetupObstacle (int obs, GameObject go) {
		for (int x = nx + obs; x < px - obs + 1; x++) {
			CreateObstacleBlock(x, 1, nz + obs, go, obstacleHolder);
			CreateObstacleBlock(x, 1, pz - obs, go, obstacleHolder);
		}

		for (int z = nz + obs; z < pz - obs + 1; z++) {
			CreateObstacleBlock (nz + obs, 1, z, go, obstacleHolder);
			CreateObstacleBlock (px - obs, 1, z, go, obstacleHolder);
		}
	}

	private void SetupGround() {
		// build ground
		for (int x = nx; x < px + 1; x++) {
			for (int z = nz; z < pz + 1; z++) {
				CreateStaticBlock (x, 0, z, stone, boardHolder);
			}
		}

		// build wall invisible
		for (int x = nx; x < px + 1; x++) {
			CreateStaticBlock (x, 1, nz - 1, invisible, boardHolder);
			CreateStaticBlock (x, 1, pz + 1, invisible, boardHolder);
			CreateStaticBlock (x, 2, nz - 1, invisible, boardHolder);
			CreateStaticBlock (x, 2, pz + 1, invisible, boardHolder);
			CreateStaticBlock (x, 3, nz - 1, invisible, boardHolder);
			CreateStaticBlock (x, 3, pz + 1, invisible, boardHolder);
		}
		for (int z = nz; z < pz + 1; z++) {
			CreateStaticBlock (nx - 1, 1, z, invisible, boardHolder);
			CreateStaticBlock (px + 1, 1, z, invisible, boardHolder);
			CreateStaticBlock (nx - 1, 2, z, invisible, boardHolder);
			CreateStaticBlock (px + 1, 2, z, invisible, boardHolder);
			CreateStaticBlock (nx - 1, 3, z, invisible, boardHolder);
			CreateStaticBlock (px + 1, 3, z, invisible, boardHolder);
		}
	}
			
	private void CreateStaticBlock(int x, int y, int z, GameObject material, GameObject parent){
		CreateBlock (x, y, z, material, parent, true, "Static");
	}

	private void CreateObstacleBlock(int x, int y, int z, GameObject material, GameObject parent){
		CreateBlock (x, y, z, material, parent, false, "Obstacle");
	}

	private void CreateBlock(int x, int y, int z, GameObject material, GameObject parent, bool isStatic, string tag) {
		GameObject block = Instantiate (material, new Vector3 (x, y, z), Quaternion.identity);
		block.transform.SetParent (parent.transform);
		block.isStatic = isStatic;
		block.tag = tag;
	}



//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
}
