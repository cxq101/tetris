using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    public class InputManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameManager.Instance.OnInputMove(-1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                GameManager.Instance.OnInputMove(1);
            }
        }
    }
}
