using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using LitJson;
using System.IO;
using Assets.Scripts.Models;
using System.Collections.Generic;

namespace UnityScripts
{
    public class SendMessage : MonoBehaviour
    {

        public ApplicationManager appManager;
        public int messageId = -1;
        // Use this for initialization
        private InputField input;
        private GameObject messageControllObj;
        private MessageController controlls;

        void Awake()
        {
            this.appManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
            this.messageControllObj = GameObject.FindGameObjectWithTag("MWindow");
            this.controlls = messageControllObj.GetComponent<MessageController>();
            this.input = this.gameObject.GetComponent<InputField>();
        }

        void Update()
        {

        }

        public void OnEnter()
        {
            if (this.messageId == -1)
            {
                NewMessage();
            }
            else
            {
                this.EditMessage(this.messageId);
            }

            //this.appManager.GetMessagesFromServer();
        }

        private void NewMessage()
        {
            var request = WebRequest.Create(ServerInfo.GetSendMessageRoute()) as HttpWebRequest;

            request.ContentType = ServerInfo.JsonContetnType;
            request.Headers.Add("Authorization", "Bearer " + appManager.Token);
            request.Method = "POST";

            var jsonData = JsonMapper.ToJson(this.MakeMessageModel());
            Debug.Log(jsonData);

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonData);
            }

            var response = (HttpWebResponse)request.GetResponse();
            this.input.text = string.Empty;
            this.messageId = -1;
        }

        private void EditMessage(int id)
        {
            var request = WebRequest.Create(ServerInfo.GetEditMessegesRoute(id.ToString())) as HttpWebRequest;

            request.ContentType = ServerInfo.JsonContetnType;
            request.Headers.Add("Authorization", "Bearer " + appManager.Token);
            request.Method = "PUT";

            var jsonData = JsonMapper.ToJson(this.MakeEditMessage());
            Debug.Log(jsonData);

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonData);
            }

            var response = (HttpWebResponse)request.GetResponse();
            this.input.text = string.Empty;
            this.messageId = -1;

            var children = new List<GameObject>();
            foreach (Transform child in messageControllObj.transform) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));

            this.appManager.GetMessagesFromServer();
            this.controlls.GenerateMyMessage();
        }

        private void EditMessage()
        {

        }

        private SendMessageModel MakeMessageModel()
        {
            return new SendMessageModel()
            {
                Message = this.input.text,
                Receiver = this.appManager.CurrentReciever
            };
        }

        private EditMessageModel MakeEditMessage()
        {
            return new EditMessageModel()
            {
                Message = this.input.text,
                IsChangingDate = true
            };
        }

    }
}