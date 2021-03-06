﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Net;
using System.IO;
using LitJson;
using System.Linq;
using Assets.Scripts.Models;

namespace UnityScripts
{
    public class ApplicationManager : MonoBehaviour
    {

        public static ApplicationManager instance = null;

        public List<MessageTemplate> messages;
        public string Token;
        public string CurrentReciever;

        private float currentTime;
        private float intervalTime;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            this.messages = new List<MessageTemplate>();
            DontDestroyOnLoad(gameObject);

            this.currentTime = 0;
            this.intervalTime = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Application.loadedLevelName == "Chat" && this.CurrentReciever != string.Empty)
            {
                this.currentTime += Time.deltaTime;

                if (currentTime >= this.intervalTime)
                {
                    this.currentTime = 0;
                    this.GetMessagesFromServer();
                }
            }
        }



        public void GetMessagesFromServer()
        {
            //Debug.Log("Hello");
            this.messages = new List<MessageTemplate>();
            //Debug.Log(ServerInfo.GetMessegesRoute(this.CurrentReciever));
            var request = WebRequest.Create(ServerInfo.GetMessegesRoute(this.CurrentReciever)) as HttpWebRequest;

            request.ContentType = ServerInfo.JsonContetnType;
            request.Headers.Add("Authorization", "Bearer " + Token);
            request.Method = "GET";
            request.GetResponse();

            var response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            reader.Close();

            var resultJ = JsonMapper.ToObject<List<MessageTemplate>>(result);

            foreach (var item in resultJ)
            {
                this.messages.Add(item);
            }

            //this.messages.OrderBy(x => x.Id);
        }


    }
}
