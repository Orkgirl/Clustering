using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImputNo : MonoBehaviour {

    public InputField Input;
    public int CurrentValue;
    public int MaxValue;
    public int MinValue;
    public int ValueStep;

	public void OnClickHandler (int value)
    {
        CurrentValue = CurrentValue + value * ValueStep;
        if (CurrentValue < MinValue)
        {
            CurrentValue = MinValue;
        }
        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }

        Input.text = CurrentValue.ToString();
    }

    void Start()
    {
        CurrentValue = MinValue;
        Input.text = CurrentValue.ToString();
    }
}
