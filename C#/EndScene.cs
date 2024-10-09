// Ioannis Kalaitzidis, 2120067, Thesis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;            // Για την εμφάνιση του τελικού score στην UI
    ScoreKeeper scoreKeeper;                                    // Αναφορά του score

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();          // Αναζητά τη μεταβλητή στη σκηνή
    }

    public void ShowFinalScore()
    {
        float scorePercentage = scoreKeeper.CalculateScore();   // Υπολογισμός του ποσοστού επίδοσης
        string message = "";                                    // Μήνυμα που θα εμφανιστεί βάσει του score

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

        // Ρύθμιση του κειμένου στην UI με το επιλεγμένο μήνυμα και το ποσοστό του score
        finalScoreText.text = message + "\nΤο score σου είναι: " + scorePercentage + "%";
    }
}

