using UnityEngine;

public class HoleMaker : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int column = Random.Range(1, 6);

        if (Random.Range(0,2) == 0)
        {
            hide(column + 15);
            hide(column + 20);
        }
        else
        {
            hide(column + 5);
            hide(column + 10);
            hide(column + 15);
        }
    }

    
    void hide (int number)
    {
        transform.Find(number.ToString()).gameObject.SetActive(false);
    }


}
