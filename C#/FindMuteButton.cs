// Ioannis Kalaitzidis, 2120067, Thesis

using UnityEngine;

public class MuteButtonHandler : MonoBehaviour
{
    private AudioManager audioManager;                      // Δημιουργία μεταβλητής για τη διαχείριση του AudioManager

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();    // Εύρεση του instance του AudioManager στη σκηνή
    }

    public void OnMuteButtonPressed()
    {
        // Ελέγχει αν το αντικείμενο AudioManager είναι διαθέσιμο
        if (audioManager != null)
        {
            audioManager.ToggleMute();                      // Καλεί τη συνάρτηση για εναλλαγή σίγασης του ήχου
        }
        else
        {
            // Εμφανίζει προειδοποιητικό μήνυμα στην κονσόλα αν δεν βρεθεί το AudioManager
            Debug.LogWarning("AudioManager not found!");
        }
    }
}

