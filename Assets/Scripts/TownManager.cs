using UnityEngine;
using UnityEngine.AI;

public class TownManager : MonoBehaviour {

	public PlayerManager playerManager;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();

		playerManager.CreatePlayer(true);

		playerManager.localPlayer.GetComponent<NavMeshAgent>().Warp(gameObject.transform.position);

        playerManager.localPlayer.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

        SoundManager.instance.PlayMusic(3);

	}
}
