using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject creditScreenPrefab;
    private int clickCounter = 0;
    private GameObject creditScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        clickCounter++;

        if (clickCounter >= 5)
        {
            creditScreen = Instantiate(creditScreenPrefab, GameObject.Find("Screen").transform);
            clickCounter = 0;
        }
    }
    
    public void CloseClick()
    {
        Window window = creditScreen.GetComponent<Window>();
        if (window != null)
        {
            window.CloseWindow();
        }
    }
}
