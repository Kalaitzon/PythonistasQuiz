// Ioannis Kalaitzidis, 2120067, Thesis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;                                              // Διαχείριση του Quiz component
    EndScene endScene;                                      // Διαχείριση του EndScene component

    void Awake()
    {
        // Εύρεση των components στη σκηνή
        quiz = FindObjectOfType<Quiz>();
        endScene = FindObjectOfType<EndScene>();
    }
    
    void Start()
    {
        // Ενεργοποίηση του Quiz και απενεργοποίηση της EndScene στην έναρξη του παιχνιδιού για να μη συγκρούονται τα περιεχόμενα των δύο σκηνών
        quiz.gameObject.SetActive(true);
        endScene.gameObject.SetActive(false);
    }

    void Update()
    {
        // Έλεγχος αν το Quiz έχει ολοκληρωθεί
        if (quiz.isComplete)
        {
            // Απενεργοποίηση του Quiz και ενεργοποίηση της EndScene
            quiz.gameObject.SetActive(false);
            endScene.gameObject.SetActive(true);
            endScene.ShowFinalScore();                      // Εμφάνιση του τελικού σκορ
        }
    }

    public void OnReplayLevel()
    {
        // Εύρεση και επανεκκίνηση της μουσικής μέσω του AudioManager
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.RestartMusic();                    // Επανεκκίνηση της μουσικής
        }

        // Επανεκκίνηση της τρέχουσας σκηνής για έναρξη νέου γύρου σύμφωνα με τη δηλωμένη ιεραρχία στο Unity
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}