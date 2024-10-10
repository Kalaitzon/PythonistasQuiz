// Ioannis Kalaitzidis, 2120067, Thesis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;                          // Το UI στοιχείο για την εμφάνιση του κειμένου της ερώτησης
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();   // Λίστα με τις ερωτήσεις του κουίζ
    QuestionSO currentQuestion;                                             // Η τρέχουσα ερώτηση

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;                            // Τα κουμπιά για τις απαντήσεις
    int correctAnswerIndex;                                                 // Ο δείκτης της σωστής απάντησης
    bool hasAnsweredEarly = true;                                           // Έλεγχος αν ο χρήστης απάντησε νωρίτερα

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;                            // Το προεπιλεγμένο sprite για τα κουμπιά απάντησης
    [SerializeField] Sprite correctAnswerSprite;                            // Το sprite για την ένδειξη της σωστής απάντησης
    [SerializeField] Sprite falseAnswerSprite;                              // Το sprite για την ένδειξη της λάθος απάντησης

    [Header("Timer")]
    [SerializeField] Image timerImage;                                      // Η εικόνα που δείχνει τον χρονομετρητή
    Timer timer;                                                            // Ο χρονομετρητής

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;                             // Το UI στοιχείο για την εμφάνιση του score
    ScoreKeeper scoreKeeper;                                                // Ο διαχειριστής του score

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;                                    // Η γραμμή προόδου του quiz

    public bool isComplete;                                                 // Έλεγχος αν το quiz έχει ολοκληρωθεί

    void Awake()
    {
        timer = FindObjectOfType<Timer>();                                  // Αναζήτηση του χρονομετρητή στη σκηνή
        scoreKeeper = FindObjectOfType<ScoreKeeper>();                      // Αναζήτηση του διαχειριστή score στη σκηνή
        progressBar.maxValue = questions.Count;                             // Ορισμός της μέγιστης τιμής της γραμμής προόδου
        progressBar.value = 0;                                              // Αρχικοποίηση της γραμμής προόδου
        Application.runInBackground = true;                                 // Επιτρέπει την εκτέλεση της εφαρμογής στο παρασκήνιο για να μην κάνει pause το παιχνίδι αν πατήσει click ο χρήστης σε κάποιο άλλο σημείο της ιστοσελίδας
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;                         // Ενημέρωση της γέμισης του χρονοδιαγράμματος βάσει του χρόνου

        if (timer.loadNextQuestion)                                         // Έλεγχος αν έχει ολοκληρωθεί ο χρόνος απάντησης και αν πρέπει να φορτωθεί η επόμενη ερώτηση
        {
                                                                            // Ελέγχουμε αν έχουμε φτάσει στο τέλος των ερωτήσεων του quiz
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;                                          // Ορισμός της ολοκλήρωσης του quiz
                return;                                                     // Έξοδος από την ενημέρωση για να μην εκτελεστούν άλλες εντολές
            }
            hasAnsweredEarly = false;                                       // Επαναφορά της κατάστασης απάντησης
            GetNextQuestion();                                              // Κλήση της μεθόδου για φόρτωση της επόμενης ερώτησης
            timer.loadNextQuestion = false;                                 // Επαναφορά του σήματος για τη φόρτωση ερώτησης
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)           // Εμφάνιση της σωστής απάντησης αν ο χρήστης δεν απάντησε εντός του δοσμένου χρόνου
        {
            DisplayAnswer(-1);                                              // Κλήση με -1 ως παράμετρο για να δείξει ότι ο χρήστης δεν επέλεξε απάντηση
            SetButtonState(false);                                          // Απενεργοποίηση των κουμπιών για να αποφευχθεί περαιτέρω αλληλεπίδραση
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;                                            // Ο χρήστης απάντησε στην ερώτηση
        DisplayAnswer(index);                                               // Εμφανίζει το αποτέλεσμα της απάντησης
        SetButtonState(false);                                              // Απενεργοποιεί τα κουμπιά απάντησης
        timer.CancelTimer();                                                // Διακόπτει τον χρονομέτρο
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";    // Ενημερώνει το κείμενο του score
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == -1)  // Ο χρήστης δεν απάντησε εντός του χρόνου
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();   //Αναζητά το index της σωστής απάντησης στη συγκεκριμένη ερώτηση
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Δεν απάντησες εντός χρόνου. Η σωστή απάντηση είναι:\n" + correctAnswer; //Εμφανίζει τη σωστή απάντηση
            questionText.alignment = TextAlignmentOptions.Center;           // Κεντραρισμένη στοίχιση για σωστή απάντηση

            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;                       // Ενημέρωση του sprite του κουμπιού που αντιστοιχεί στη σωστή απάντηση
        }
        else if(index == currentQuestion.GetCorrectAnswerIndex())           // Ελέγχουμε αν η επιλεγμένη απάντηση είναι η σωστή
        {
            questionText.text = "Σωστή απάντηση!";
            questionText.alignment = TextAlignmentOptions.Center;           // Κεντραρισμένη στοίχιση για σωστή απάντηση
            buttonImage = answerButtons[index].GetComponent<Image>();       // Ενημέρωση του sprite του κουμπιού που αντιστοιχεί στη σωστή απάντηση
            buttonImage.sprite = correctAnswerSprite;                       // Αλλάζει την εικόνα του κουμπιού σε εικόνα σωστής απάντησης
            scoreKeeper.IncrementCorrectAnswers();                          // Αυξάνει τον μετρητή των σωστών απαντήσεων
        }
        else                                                                // Αν η απάντηση είναι λανθασμένη, εντοπίζουμε τη σωστή και την εμφανίζουμε
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();   //Αναζητά το index της σωστής απάντησης στη συγκεκριμένη ερώτηση
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex); 
            questionText.text = "Λάθος! Η σωστή απάντηση είναι:\n" + correctAnswer; //Εμφανίζει τη σωστή απάντηση
            questionText.alignment = TextAlignmentOptions.Center;           // Κεντραρισμένη στοίχιση για λάθος απάντηση
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>(); // Ενημέρωση του sprite του κουμπιού που αντιστοιχεί στη λάθος απάντηση
            buttonImage.sprite = correctAnswerSprite;                       // Αλλάζει την εικόνα του κουμπιού σε εικόνα λάθος απάντησης
            
            // Ενημέρωση του κουμπιού της λανθασμένης απάντησης, αν το index είναι εντός ορίων
            if (index >= 0 && index < answerButtons.Length) {
                buttonImage = answerButtons[index].GetComponent<Image>();   // Ορίζει το sprite του κουμπιού που πάτησε ο χρήστης σε κόκκινο αν η απάντηση είναι λανθασμένη
                buttonImage.sprite = falseAnswerSprite;                     // Σημαδεύει το κουμπί με τη λανθασμένη απάντηση
            }
        }
    }

    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);                                           // Ενεργοποιεί τα κουμπιά απάντησης
            SetDefaultButtonSprites();                                      // Επαναφέρει τα sprites των κουμπιών στην προεπιλεγμένη εικόνα
            GetRandomQuestion();                                            // Επιλέγει τυχαία μια ερώτηση από τη λίστα
            DisplayQuestion();                                              // Εμφανίζει την επόμενη ερώτηση
            progressBar.value++;                                            // Αυξάνει την τιμή της γραμμής προόδου
            scoreKeeper.IncrementQuestionsSeen();                           // Αυξάνει τον μετρητή των ερωτήσεων που έχουν παρουσιαστεί
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);                              // Αφαιρεί την ερώτηση από τη λίστα για να μην επαναληφθεί
        }
    }

    void DisplayQuestion()
    {
        questionText.alignment = TextAlignmentOptions.Justified;            // Ορίζει τη στοίχιση του κειμένου της ερώτησης ως δικαιολογημένη
        questionText.text = currentQuestion.GetQuestion();                  // Εμφανίζει το κείμενο της τρέχουσας ερώτησης

        for (int i = 0; i<answerButtons.Length; i++)                        // Διαπερνά όλα τα κουμπιά απαντήσεων
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>(); // Παίρνει το component TextMeshProUGUI του κουμπιού
            buttonText.text = currentQuestion.GetAnswer(i);                 // Ορίζει το κείμενο του κουμπιού στην αντίστοιχη απάντηση
        }
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)                       // Διαπερνά όλα τα κουμπιά απαντήσεων
        {
            Button button = answerButtons[i].GetComponent<Button>();        // Παίρνει το component Button του κουμπιού
            button.interactable = state;                                    // Ορίζει τη διαδραστικότητα του κουμπιού (ενεργό ή ανενεργό)
        }
    }

    void SetDefaultButtonSprites()
    {
        for(int i = 0; i < answerButtons.Length; i++)                       // Διαπερνά όλα τα κουμπιά απαντήσεων
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();     // Παίρνει το component Image του κουμπιού
            buttonImage.sprite = defaultAnswerSprite;                       // Ορίζει το sprite του κουμπιού στην προεπιλεγμένη εικόνα
        }
    }
}