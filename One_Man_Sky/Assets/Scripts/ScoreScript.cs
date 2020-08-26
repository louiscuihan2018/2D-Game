using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ScoreScript : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The text component that is displaying the score. The text value " +
       "of this component will change with the score.")]
    private Text m_UIText = null;
    #endregion

    #region Non-Editor Variables
    private int m_Score;
    #endregion

    #region Singletons
    private static ScoreScript st;
    #endregion

    #region First Time Initialization and Set Up
    private void Awake()
    {
        st = this;
    }

    public void Start()
    {
        m_Score = 0;
        AddScore(0);
    }
    #endregion

    #region Accessors and Mutators
    public static ScoreScript Singleton
    {
        get { return st; }
    }
    #endregion

    #region Score Modification Methods
    public void AddScore(int add)
    {
        m_Score += add;
        m_UIText.text = "Score: " + m_Score;
    }
    #endregion
}
