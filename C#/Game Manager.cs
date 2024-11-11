// Ioannis Kalaitzidis, 2120067, Thesis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource replaySound;       //Ήχος εφέ replay button
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
        StartCoroutine(ReplayLevelAfterSound());            // Χρήση Coroutine για την αναπαραγωγή ήχου και φόρτωση σκηνής
    }

    // Για να προλάβει να ολοκληρωθεί ο ήχος του εφέ και μετά να περάσει στην επόμενη σκηνή
     IEnumerator ReplayLevelAfterSound()
    {
        if (replaySound != null)
        {
            replaySound.Play();                             // Παίζει τον ήχο
            yield return new WaitForSeconds(Mathf.Max(0, replaySound.clip.length - 2.3f)); // Αναμονή για το μήκος του ήχου μειωμένο κατά 2.3 δευτερόλεπτα
        }

        // Επανεκκίνηση της σκηνής
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}