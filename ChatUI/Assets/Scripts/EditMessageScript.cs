using UnityEngine;
using System.Collections;
using UnityScripts;
using UnityEngine.UI;

namespace UnityScripts
{
    public class EditMessageScript : MonoBehaviour
    {

        public int id;
        private MessageManager messageObj;
        private SendMessage messageInput;
        private ApplicationManager appManager;
        private GameObject parrent;

        void Awake()
        {
            this.appManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
            parrent = transform.parent.gameObject;
            this.messageObj = parrent.GetComponent<MessageManager>();
            this.id = messageObj.Id;
            this.messageInput = GameObject.FindGameObjectWithTag("MSender").GetComponent<SendMessage>();
        }

        void Update()
        {
            this.id = messageObj.Id;
        }

        public void OnEditClick()
        {
            this.messageInput.messageId = this.id;
        }
    }
}

