using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class StartScreen : MonoBehaviour {
        public void Update()
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
        }
    }
}
