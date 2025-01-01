// Ioannis Kalaitzidis, 2120067, Thesis

using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class EndScene : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SendScoreToServer(int score);

    [SerializeField] TextMeshProUGUI finalScoreText;
    private ScoreKeeper scoreKeeper;
    private bool scoreSent = false; // Flag για να διασφαλίσουμε ότι η μέθοδος καλείται μόνο μία φορά

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        float scorePercentage = scoreKeeper.CalculateScore(); // Υπολογισμός του ποσοστού επίδοσης
        string message = "";

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

        // Κλήση για αποστολή του σκορ στον server
        SendScoreToFirebase(Mathf.RoundToInt(scorePercentage));
    }

    public void SendScoreToFirebase(int scorePercentage)
    {
        if (scoreSent) return; // Έλεγχος αν το score έχει ήδη σταλεί για αποφυγή διπλής αποστολής
        scoreSent = true; // Flag ώστε να αποφευχθεί η επαναλαμβανόμενη αποστολή

        // Καταγραφή του score στο Debug log
        Debug.Log($"Sending score: {scorePercentage}");

        #if UNITY_WEBGL && !UNITY_EDITOR
            SendScoreToServer(scorePercentage); // Κλήση της συνάρτησης αποστολής στον server σε περιβάλλον WebGL
        #else
            Debug.Log("SendScoreToServer can only be called in WebGL builds."); // Εμφάνιση μηνύματος στο Debug log αν η πλατφόρμα δεν είναι WebGL
        #endif
    }
}
