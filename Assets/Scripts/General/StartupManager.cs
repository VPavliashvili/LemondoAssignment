using System.Collections;
using UnityEngine;

public class StartupManager : MonoBehaviour {

    [SerializeField]
    private BoolReference hasSeenInstructions;
    [SerializeField]
    private BoolReference hasWon;
    [SerializeField]
    private GameObject Blur;
    [SerializeField]
    private GameObject winningPanel;

    void Awake() {
        if (hasSeenInstructions) {
            Blur.SetActive(false);
        }    
    }

    void Start() {
        if (!hasSeenInstructions)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void Update() {
        if (hasWon && Time.timeScale > 0) {

            StartCoroutine(WinRoutine());
        }
        if (Input.GetKeyDown(KeyCode.Return) && !hasSeenInstructions) {
            SeeInstructions();
        }

    }

    public void SeeInstructions() {
        hasSeenInstructions.Value = true;
        Blur.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator WinRoutine() {
        yield return new WaitForSeconds(1.5f);
        winningPanel.SetActive(true);
        Time.timeScale = 0;
    }

}