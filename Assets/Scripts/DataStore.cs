using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStore : MonoBehaviour
{
    private string[] sde_questions = {
        "What is the time complexity of a linear search algorithm?",
"What is the difference between an abstract class and an interface in Java?",
"How do you reverse a linked list?",
"What is the difference between stack and queue data structures?",
"What is the purpose of the 'this' keyword in Java?",
"How do you find the middle element of a linked list in one pass?",
"What is a binary search tree and how does it differ from a regular binary tree?",
"What is the difference between a static method and an instance method in Java?",
"What is the time complexity of a bubble sort algorithm?",
"How do you check if a string is a palindrome?",
"What is the difference between a join and a union in SQL?",
"How do you implement a binary search algorithm?",
"What is a hash table and how does it work?",
"How do you find the maximum value in an array?",
"What is the difference between a private and a protected method in Java?",
"How do you remove duplicate elements from an array?",
"What is a callback function in JavaScript?",
"How do you implement a queue using two stacks?",
"What is the difference between a primary key and a foreign key in SQL?",
"How do you find the second largest element in an array?"
    };

    private string[] mle_questions = {
        "What is overfitting in machine learning?",
"What is regularization and why is it used in machine learning?",
"What is cross-validation and how is it used in machine learning?",
"What is the difference between supervised and unsupervised learning?",
"What is gradient descent and how is it used in machine learning?",
"What is bias-variance tradeoff in machine learning?",
"What is precision and recall in binary classification?",
"What is the difference between L1 and L2 regularization in machine learning?",
"What is the curse of dimensionality in machine learning?",
"What is a decision tree and how is it used in machine learning?",
"What is k-fold cross-validation and how does it work?",
"What is a confusion matrix and how is it used in machine learning?",
"What is the difference between batch gradient descent and stochastic gradient descent?",
"What is a random forest and how is it used in machine learning?",
"What is the difference between logistic regression and linear regression?",
"What is a neural network and how is it used in machine learning?",
"What is the difference between classification and regression in machine learning?",
"What is the difference between parametric and non-parametric models in machine learning?",
"What is ensemble learning and how is it used in machine learning?",
"What is the difference between precision and accuracy in machine learning?"
    };

    private string[] xr_questions = {
        "What is the difference between VR and AR?",
"What is the FOV and why is it important in VR/AR?",
"What is the difference between positional and rotational tracking in VR/AR?",
"What is stereoscopic vision and how is it used in VR/AR?",
"What is the difference between inside-out and outside-in tracking in VR/AR?",
"What is occlusion and how is it handled in AR?",
"What is presence in VR/AR and why is it important?",
"What is the refresh rate and why is it important in VR/AR?",
"What is the difference between gaze-based and hand-based interaction in VR/AR?",
"What is latency and why is it important in VR/AR?",
"What is haptic feedback and how is it used in VR/AR?",
"What is room-scale tracking and how is it used in VR?",
"What is the difference between augmented reality and mixed reality?",
"What is foveated rendering and how is it used in VR/AR?",
"What is the difference between 3DoF and 6DoF tracking in VR/AR?",
"What is the difference between a VR headset and an AR headset?",
"What is the role of SLAM in AR?",
"What is the difference between a tethered and untethered VR/AR headset?",
"What is the role of eye-tracking in VR/AR?",
"What is the difference between pass-through and see-through AR?"
    };

    string[] questions_asked = new string[5];
    string[] responses = new string[5];
    string[] feedbacks = new string[5];
    float[] timings = new float[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
    }

    public string[] GetSDEQuestions()
    {
        return sde_questions;
    }
    
    public string[] GetMLEQuestions()
    {
        return mle_questions;
    }

    public string[] GetXRQuestions()
    {
        return xr_questions;
    }

    public string[] GetQuestionsAsked()
    {
        return questions_asked;
    }

    public string[] GetFeedbacks()
    {
        return feedbacks;
    }

    public string[] GetResponses()
    {
        return responses;
    }

    public float[] GetTimings()
    {
        return timings;
    }

    public void SetQuestionsAskedElement(int element, string stri)
    {
        questions_asked[element] = stri;
    }

    public void SetFeedbacksElement(int element, string stri)
    {
        feedbacks[element] = stri;
    }

    public void SetResponsesElement(int element, string stri)
    {
        responses[element] = stri;
    }

    public void SetTimingsElement(int element, float stri)
    {
        timings[element] = stri;
    }
}
