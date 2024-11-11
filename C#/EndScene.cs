// Ioannis Kalaitzidis, 2120067, Thesis

using UnityEngine;
using TMPro; // Για χρήση των TextMesh Pro UI components
using System.Runtime.InteropServices; // Για επικοινωνία με το JavaScript

public class EndScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText; // Για την εμφάνιση του τελικού score στην UI
    ScoreKeeper scoreKeeper; // Αναφορά του score

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // Αναζητά τη μεταβλητή στη σκηνή
    }

    public void ShowFinalScore()
    {
        float scorePercentage = scoreKeeper.CalculateScore(); // Υπολογισμός του ποσοστού επίδοσης
        string message = ""; // Μήνυμα που θα εμφανιστεί βάσει του score

        // Επιλογή μηνύματος βάσει του ποσοστού επίδοσης
        if (scorePercentage >= 85)
        {
            message = "Συγχαρητήρια! Εξαιρετική απόδοση!";
        }
        else if (scorePercentage >= 70)
        {
            message = "Μπράβο! Πολύ καλή δουλειά!";
        }
        else if (scorePercentage >= 55)
        {
            message = "Καλή προσπάθεια! Μπορείς και καλύτερα!";
        }
        else
        {
            message = "Χρειάζεται περισσότερη δουλειά. Μην τα παρατάς!";
        }

        finalScoreText.text = message + "\nΤο score σου είναι: " + scorePercentage + "%";
    }
}
