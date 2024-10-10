// Ioannis Kalaitzidis, 2120067, Thesis

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Προσθήκη για χρήση Coroutines

public class MainMenu : MonoBehaviour
{
    // Συστατικό AudioSource για το ηχητικό εφέ του κουμπιού
    [SerializeField] private AudioSource playButtonSound;

    // Φόρτωση της επόμενης σκηνής, που είναι το Quiz
    public void PlayGame()
    {
        StartCoroutine(PlaySoundAndLoadScene());                        // Χρήση Coroutine για την αναπαραγωγή ήχου και φόρτωση σκηνής
    }
    
    // Για να προλάβει να ολοκληρωθεί ο ήχος του εφέ και μετά να περάσει στην επόμενη σκηνή
    IEnumerator PlaySoundAndLoadScene()
    {
        playButtonSound.Play();                                         // Παίζει τον ήχο
        yield return new WaitForSeconds(Mathf.Max(0, playButtonSound.clip.length - 2.3f)); // Αναμονή για το μήκος του ήχου μειωμένο κατά 2.3 δευτερόλεπτα
        SceneManager.LoadSceneAsync(1);                                 // '1' αντιπροσωπεύει τον δείκτη της σκηνής στην οποία θέλουμε να πάμε (από τη δηλωμένη ιεραρχία στο Unity)
    }

    // Εξοδος από το παιχνίδι
    public void ExitGame()
    {
        Application.Quit();                                             // Κλείνει την εφαρμογή πλήρως
    }
}
