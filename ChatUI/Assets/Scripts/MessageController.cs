using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;
using System.Linq;
using System;

namespace UnityScripts
{
    public class MessageController : MonoBehaviour
    {

        public Image MyMessageTemplate;
        public Image OtherMessageTemplate;
        public ApplicationManager appManger;

        void Awake()
        {
            this.appManger = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
        }

        // Update is called once per frame
        void Update()
        {
            this.GenerateMyMessage();

        }

        public void GenerateMyMessage()
        {
            appManger.messages.Reverse();
            if (this.transform.childCount == this.appManger.messages.Count)
            {
                return;
            }
            var children = new List<GameObject>();
            foreach (Transform child in transform) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
            for (int i = 0; i < appManger.messages.Count; i++)
            {

                var mTemplate = appManger.messages[i];
                Image message;

                if (mTemplate.IsMyMessage == 1)
                {
                    message = Instantiate(MyMessageTemplate);
                }
                else
                {
                    message = Instantiate(OtherMessageTemplate);
                }
                message.transform.SetParent(this.gameObject.transform, false);
                var text = message.GetComponentInChildren<Text>();
                text.text = appManger.messages[i].Message;
                var id = message.GetComponent<MessageManager>();

                id.Id = appManger.messages[i].Id;
            }

        }
    }
}



