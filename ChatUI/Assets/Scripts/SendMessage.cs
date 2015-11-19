using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using LitJson;
using System.IO;
using UnityScipts;
using Assets.Scripts.Models;

namespace UnityScripts
{
    public class SendMessage : MonoBehaviour
    {

        public ApplicationManager appManager;
        // Use this for initialization
        private Text text;

        void Awake()
        {
            this.appManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
            this.text = this.GetComponentInChildren<Text>();
        }

        void Update()
        {

        }

        public void OnEnter()
        {
            var request = WebRequest.Create(ServerInfo.GetSendMessageRoute()) as HttpWebRequest;

            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + appManager.Token);
            request.Method = "POST";

            var jsonData = JsonMapper.ToJson(this.MakeMessageModel());
            Debug.Log(jsonData);

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonData);
            }

            var response = (HttpWebResponse)request.GetResponse();
            Debug.Log(response.StatusCode);
            //var reader = new StreamReader(response.GetResponseStream());
            //var result = reader.ReadToEnd();
            //reader.Close();
            //var token = JsonMapper.ToObject(result)["access_token"].ToString();
        }

        private SendMessageModel MakeMessageModel()
        {
            return new SendMessageModel()
            {
                Message = this.text.text,
                Receiver = this.appManager.CurrentReciever
            };
        }

    }
}