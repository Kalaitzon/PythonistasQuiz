// // Ioannis Kalaitzidis, 2120067, Thesis

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;                   // Αναφορά στην πηγή ήχου για τη μουσική

    private static AudioManager instance;                               // Μοναδική instance του AudioManager για τη διασφάλιση ενός μόνο αντικειμένου

    private void Awake()
    {
        // Έλεγχος για την ύπαρξη πολλαπλών instances του AudioManager (να μην παίζει ο ήχος παραπάνω από δύο φορές στην κάθε σκηνή)
        if (instance != null && instance != this)
        {
            Destroy(gameObject);                                        // Καταστροφή του περιττού αντικειμένου AudioManager
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);                              // Διατηρεί το AudioManager διαθέσιμο και μη καταστρέψιμο κατά την αλλαγή σκηνών
        }
    }

    void Start()
    {
        // Αυτόματη έναρξη αναπαραγωγής μουσικής κατά την εκκίνηση, αν δεν παίζει ήδη
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();                                         // Ξεκινάει την αναπαραγωγή της μουσικής
        }
    }

    // Συνάρτηση για την εναλλαγή της κατάστασης σίγασης
    public void ToggleMute()
    {
        if (musicSource != null)
        {
            musicSource.mute = !musicSource.mute;                       // Εναλλάσσει το mute
        }
    }

    // Συνάρτηση για την επανεκκίνηση της μουσικής
    public void RestartMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();                                         // Διακόπτει την τρέχουσα αναπαραγωγή
            musicSource.Play();                                         // Ξεκινάει την αναπαραγωγή της μουσικής από την αρχή
        }
    }
}
