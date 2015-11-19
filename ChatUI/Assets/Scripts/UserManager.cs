using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityScipts;

namespace UnityScripts
{
    public class UserManager : MonoBehaviour
    {


        private ApplicationManager appManger;
        private Text text;
        // Use this for initialization
        void Start()
        {
            this.appManger = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
            this.text = this.gameObject.GetComponentInChildren<Text>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GetConversation()
        {
            Debug.Log("click");

            var userName = text.text;
            Debug.Log(userName);
            this.appManger.CurrentReciever = userName;
            Debug.Log(appManger.CurrentReciever);
        }
    }
}

