using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "GameState", order = 0)]
public class GameState : ScriptableObject {
	public bool InPast;
	public bool GameIsPaused;
	public GameObject Player;
	public int MaxShifts;
	public int CurrentShifts;
}