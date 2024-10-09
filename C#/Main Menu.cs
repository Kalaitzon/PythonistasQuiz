// Ioannis Kalaitzidis, 2120067, Thesis

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Φόρτωση της επόμενης σκηνής, που είναι το Quiz
    public void PlayGame()
    {
        
        SceneManager.LoadSceneAsync(1);             // '1' αντιπροσωπεύει τον δείκτη της σκηνής στην οποία θέλουμε να πάμε (από τη δηλωμένη ιεραρχία στο Unity)
    }

    // Εξοδος από το παιχνίδι
    public void ExitGame()
    {

        Application.Quit();                         // Κλείνει την εφαρμογή πλήρως
    }
}
