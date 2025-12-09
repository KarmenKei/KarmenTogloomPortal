using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class middlescript : MonoBehaviour
{
    public script lc;
    void Start()
    {
        lc = GameObject.FindGameObjectWithTag("logic").GetComponent<script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) { 
            lc.addscore(1);
        }
    }
}
