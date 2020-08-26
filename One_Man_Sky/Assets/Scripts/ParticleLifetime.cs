using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifetime : MonoBehaviour
{
    #region particle_variables
    float timer = 0f;
    float duration;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        duration = this.GetComponent<ParticleSystem>().main.duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration + 1)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
